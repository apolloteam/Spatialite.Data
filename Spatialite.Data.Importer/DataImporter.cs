namespace Spatialite.Data.Importer
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SQLite;
    using System.Diagnostics;
    using System.Globalization;

    using DotSpatial.Data;
    using DotSpatial.Topology;
    using DotSpatial.Topology.Simplify;
    using DotSpatial.Topology.Utilities;

    using GeoNames.Data;

    /// <summary>The data importer.</summary>
    public class DataImporter
    {
        #region Fields

        /// <summary>The connection.</summary>
        private readonly SQLiteConnection connection;

        #endregion

        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="DataImporter"/> class.</summary>
        /// <param name="connection">The connection.</param>
        public DataImporter(string connection)
            : this(new SQLiteConnection(connection))
        {
        }

        /// <summary>Initializes a new instance of the <see cref="DataImporter"/> class.</summary>
        /// <param name="connection">The connection.</param>
        protected DataImporter(SQLiteConnection connection)
        {
            this.connection = connection;
            this.connection.Open();
            this.connection.EnableExtensions(true);
            this.connection.LoadExtension("libspatialite-2.dll");
        }

        #endregion

        #region Methods

        /// <summary>
        /// Importa los shapes de los timezones y les agrega información adicional.
        /// </summary>
        /// <param name="paths">Lista de archivos a importar.</param>
        public void ImportTimeZones(params string[] paths)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            Debug.Flush();
            using (SQLiteTransaction txn = this.connection.BeginTransaction())
            {
                using (SQLiteCommand cmd = this.connection.CreateCommand())
                {
                    cmd.Transaction = txn;
                    cmd.CommandText = "DROP TABLE IF EXISTS TimeZoneInfo;";
                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                }

                using (SQLiteCommand cmd = this.connection.CreateCommand())
                {
                    cmd.Transaction = txn;
                    cmd.CommandText = @"CREATE TABLE IF NOT EXISTS TimeZoneInfo ( 
                                            TimeZoneId TEXT NOT NULL,                                           
                                            TimeZoneName TEXT NULL,
                                            TimeZoneDaylightName TEXT NULL,
                                            Shape TEXT NOT NULL, 
                                            CountryCode TEXT NULL, 
                                            GmtOffset DECIMAL NULL,
                                            DstOffset DECIMAL NULL,
                                            RawOffset DECIMAL NULL,
                                        );";
                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                }

                txn.Commit();
            }

            int cnt = 0;
            SQLiteBulkInsert sbi = new SQLiteBulkInsert(this.connection, "TimeZoneInfo");
            sbi.AddParameter("TimeZoneId", DbType.String);
            sbi.AddParameter("TimeZoneName", DbType.String);
            sbi.AddParameter("TimeZoneDaylightName", DbType.String);
            sbi.AddParameter("Shape", DbType.String);
            sbi.AddParameter("CountryCode", DbType.String);
            sbi.AddParameter("GmtOffset", DbType.Decimal);
            sbi.AddParameter("DstOffset", DbType.Decimal);
            sbi.AddParameter("RawOffset", DbType.Decimal);

            foreach (string path in paths)
            {
                using (IFeatureSet fs = FeatureSet.Open(path))
                {
                    WktWriter writer = new WktWriter();
                    int numRows = fs.NumRows();
                    for (int i = 0; i < numRows; i++)
                    {
                        Shape shape = fs.GetShape(i, false);
                        string timeZoneId = (string)shape.Attributes[0];

                        if (string.IsNullOrWhiteSpace(timeZoneId)
                            || timeZoneId.Equals("uninhabited", StringComparison.OrdinalIgnoreCase))
                        {
                            continue;
                        }

                        // Get the shape as a geometry.
                        IGeometry geometry = shape.ToGeometry();

                        // Simplify the geometry.
                        IGeometry simplified = null;
                        if (geometry.Area < 0.1)
                        {
                            // For very small regions, use a convex hull.
                            simplified = geometry.ConvexHull();
                        }
                        else
                        {
                            // Simplify the polygon if necessary. Reduce the tolerance incrementally until we have a valid polygon.
                            double tolerance = 0.05;
                            while (simplified == null || !(simplified is Polygon) || !simplified.IsValid
                                   || simplified.IsEmpty)
                            {
                                simplified = TopologyPreservingSimplifier.Simplify(geometry, tolerance);
                                tolerance -= 0.005;
                            }
                        }

                        // Convert it to WKT.
                        string shapeWkt = writer.Write((Geometry)simplified);
                        string timeZoneName = TimezoneConverter.TimezoneConverterProvider.OlsonToWindows(timeZoneId);
                        string timeZoneDaylightName =
                            TimezoneConverter.TimezoneConverterProvider.OlsonToWindows(timeZoneId, true);
                        GeoNames.Data.TimeZoneInfo geoname = GeoNamesProvider.GetTimeZoneInfo(timeZoneId);
                        bool hasValue = geoname != null;
                        sbi.Insert(
                            new object[]
                                {
                                    timeZoneId, timeZoneName, timeZoneDaylightName, shapeWkt,
                                    hasValue ? geoname.CountryCode : null, hasValue ? geoname.GmtOffset : null,
                                    hasValue ? geoname.DstOffset : null, hasValue ? geoname.RawOffset : null
                                });
                        cnt++;
                    }
                }
            }

            sbi.Flush();

            using (SQLiteTransaction txn = this.connection.BeginTransaction())
            {
                using (SQLiteCommand cmd = this.connection.CreateCommand())
                {
                    cmd.Transaction = txn;
                    cmd.CommandText = @"SELECT AddGeometryColumn ('TimeZoneInfo', 'Geometry', 4326, 'Geometry', 2);";
                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                }

                using (SQLiteCommand cmd = this.connection.CreateCommand())
                {
                    cmd.Transaction = txn;
                    cmd.CommandText = @"UPDATE TimeZoneInfo SET Geometry = GeomFromText(Shape, 4326);";
                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                }

                using (SQLiteCommand cmd = this.connection.CreateCommand())
                {
                    cmd.Transaction = txn;
                    cmd.CommandText = @"SELECT CreateSpatialIndex('TimeZoneInfo', 'Geometry');";
                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                }

                txn.Commit();
                sw.Stop();
                Debug.WriteLine("Imported {0} shapes in {1:N1} seconds.", cnt, sw.ElapsedMilliseconds / 1000D);
            }
        }

        /// <summary>
        /// Crea la tabla con la información de los paises.
        /// </summary>
        public void ImportCountries()
        {
            using (SQLiteTransaction txn = this.connection.BeginTransaction())
            {
                using (SQLiteCommand cmd = this.connection.CreateCommand())
                {
                    cmd.Transaction = txn;
                    cmd.CommandText = "DROP TABLE IF EXISTS CountryInfo;";
                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                }

                using (SQLiteCommand cmd = this.connection.CreateCommand())
                {
                    cmd.Transaction = txn;
                    cmd.CommandText = @"CREATE TABLE IF NOT EXISTS CountryInfo ( 
                                            ISO TEXT NULL,
                                            ISO3 TEXT NULL,
                                            ISONumeric TEXT NULL,
                                            Fips TEXT NULL,
                                            Country TEXT NULL,
                                            Capital TEXT NULL,
                                            Area REAL NULL,
                                            Population REAL NULL,
                                            Continent TEXT NULL,
                                            TopLevelDomain TEXT NULL,
                                            CurrencyCode TEXT NULL,
                                            CurrencyName TEXT NULL,
                                            Phone TEXT NULL,
                                            PostalCodeFormat TEXT NULL,
                                            PostalCodeRegex TEXT NULL,
                                            Languages TEXT NULL,
                                            GeonameId TEXT NULL,
                                            Neighbours TEXT NULL,
                                            EquivalentFipsCode TEXT NULL);";
                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                }

                txn.Commit();
            }

            IEnumerable<GeoNames.Data.CountryInfo> countries = GeoNamesProvider.GetCountries();

            SQLiteBulkInsert sbi = new SQLiteBulkInsert(this.connection, "CountryInfo");
            sbi.AddParameter("ISO", DbType.String);
            sbi.AddParameter("ISO3", DbType.String);
            sbi.AddParameter("ISONumeric", DbType.String);
            sbi.AddParameter("Fips", DbType.String);
            sbi.AddParameter("Country", DbType.String);
            sbi.AddParameter("Capital", DbType.String);
            sbi.AddParameter("Area", DbType.Decimal);
            sbi.AddParameter("Population", DbType.Decimal);
            sbi.AddParameter("Continent", DbType.String);
            sbi.AddParameter("TopLevelDomain", DbType.String);
            sbi.AddParameter("CurrencyCode", DbType.String);
            sbi.AddParameter("CurrencyName", DbType.String);
            sbi.AddParameter("Phone", DbType.String);
            sbi.AddParameter("PostalCodeFormat", DbType.String);
            sbi.AddParameter("PostalCodeRegex", DbType.String);
            sbi.AddParameter("Languages", DbType.String);
            sbi.AddParameter("GeonameId", DbType.String);
            sbi.AddParameter("Neighbours", DbType.String);
            sbi.AddParameter("EquivalentFipsCode", DbType.String);

            foreach (GeoNames.Data.CountryInfo country in countries)
            {
                sbi.Insert(
                    new object[]
                        {
                            country.ISO, country.ISO3, country.ISONumeric, country.Fips, country.Country, country.Capital,
                            country.Area, country.Population, country.Continent, country.TopLevelDomain,
                            country.CurrencyCode, country.CurrencyName, country.Phone, country.PostalCodeFormat,
                            country.PostalCodeRegex, country.Languages, country.GeonameId, country.Neighbours,
                            country.EquivalentFipsCode
                        });
            }

            sbi.Flush();

            using (SQLiteTransaction txn = this.connection.BeginTransaction())
            {
                using (SQLiteCommand cmd = this.connection.CreateCommand())
                {
                    cmd.Transaction = txn;
                    cmd.CommandText = @"CREATE UNIQUE INDEX IF NOT EXISTS ux_CountryInfo_country ON CountryInfo ( ISO );";
                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                }

                txn.Commit();
            }
        }

        #endregion
    }
}