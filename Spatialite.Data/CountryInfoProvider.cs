namespace Spatialite.Data.Countries
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Data.SQLite;
    using System.Globalization;

    /// <summary>The country info field.</summary>
    internal enum CountryInfoField
    {
        /// <summary>The iso.</summary>
        // ReSharper disable InconsistentNaming
        ISO = 0,

        // ReSharper restore InconsistentNaming

        /// <summary>The is o 3.</summary>
        // ReSharper disable InconsistentNaming
        ISO3 = 1,

        // ReSharper restore InconsistentNaming

        /// <summary>The iso numeric.</summary>
        // ReSharper disable InconsistentNaming
        ISONumeric = 2,

        // ReSharper restore InconsistentNaming

        /// <summary>The fips.</summary>
        Fips = 3,

        /// <summary>The country.</summary>
        Country = 4,

        /// <summary>The capital area.</summary>
        Capital = 5,

        /// <summary>The area.</summary>
        Area = 6,

        /// <summary>The population.</summary>
        Population = 7,

        /// <summary>The continent.</summary>
        Continent = 8,

        /// <summary>Top Level Domain.</summary>
        TopLevelDomain = 9,

        /// <summary>The currency code.</summary>
        CurrencyCode = 10,

        /// <summary>The currency name.</summary>
        CurrencyName = 11,

        /// <summary>The phone.</summary>
        Phone = 12,

        /// <summary>The postal code format.</summary>
        PostalCodeFormat = 13,

        /// <summary>The postal code regex.</summary>
        PostalCodeRegex = 14,

        /// <summary>The languages.</summary>
        Languages = 15,

        /// <summary>The geoname id.</summary>
        GeonameId = 16,

        /// <summary>The neighbours.</summary>
        Neighbours = 17,

        /// <summary>The equivalent fips code.</summary>
        EquivalentFipsCode = 18
    }

    /// <summary>The country info provider.</summary>
    public class CountryInfoProvider
    {
        #region Constants

        /// <summary>The query sql.</summary>
        private const string QuerySql = @"SELECT c.* FROM CountryInfo c 
                                    INNER JOIN TimeZones t 
                                    ON c.ISO = t.CountryCode 
                                    WHERE Contains( t.Geometry, MakePoint( {1}, {0} ) ) = 1;";

        #endregion

        #region Fields

        /// <summary>The connection.</summary>
        private readonly string connection;

        /// <summary>The sync lock.</summary>
        private readonly object syncLock = new object();

        /// <summary>The map country info.</summary>
        private IDictionary<string, CountryInfo> mapCountryInfo;

        #endregion

        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="CountryInfoProvider" /> class.</summary>
        public CountryInfoProvider()
            : this(DataConfiguration.ConnectionString)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="CountryInfoProvider"/> class.</summary>
        /// <param name="connection">The connection.</param>
        public CountryInfoProvider(string connection)
        {
            this.connection = connection;
        }

        #endregion

        #region Properties

        /// <summary>Gets the map country info.</summary>
        private IDictionary<string, CountryInfo> MapCountryInfo
        {
            get
            {
                if (this.mapCountryInfo == null)
                {
                    lock (this.syncLock)
                    {
                        if (this.mapCountryInfo == null)
                        {
                            object valueAux;
                            this.mapCountryInfo = new ConcurrentDictionary<string, CountryInfo>();
                            using (SQLiteConnection conn = new SQLiteConnection(this.connection))
                            {
                                conn.Open();
                                using (SQLiteCommand cmd = conn.CreateCommand())
                                {
                                    cmd.CommandText = @"SELECT * FROM CountryInfo;";
                                    cmd.Prepare();
                                    using (SQLiteDataReader dr = cmd.ExecuteReader())
                                    {
                                        while (dr.Read())
                                        {
                                            CountryInfo country = new CountryInfo();
                                            country.ISO = dr.GetString((int)CountryInfoField.ISO);
                                            country.ISO3 = dr.GetString((int)CountryInfoField.ISO3);
                                            country.ISONumeric = dr.GetString((int)CountryInfoField.ISONumeric);
                                            country.Fips = dr.GetString((int)CountryInfoField.Fips);
                                            country.Country = dr.GetString((int)CountryInfoField.Country);
                                            country.Capital = dr.GetString((int)CountryInfoField.Capital);
                                            valueAux = dr.GetValue((int)CountryInfoField.Area);
                                            country.Area = valueAux == DBNull.Value ? null : (decimal?)Convert.ToDecimal(valueAux);
                                            valueAux = dr.GetValue((int)CountryInfoField.Population);
                                            country.Population = valueAux == DBNull.Value ? null : (decimal?)Convert.ToDecimal(valueAux);
                                            country.Continent = dr.GetString((int)CountryInfoField.Continent);
                                            country.TopLevelDomain = dr.GetString((int)CountryInfoField.TopLevelDomain);
                                            country.CurrencyCode = dr.GetString((int)CountryInfoField.CurrencyCode);
                                            country.CurrencyName = dr.GetString((int)CountryInfoField.CurrencyName);
                                            country.Phone = dr.GetString((int)CountryInfoField.Phone);
                                            country.PostalCodeFormat =
                                                dr.GetString((int)CountryInfoField.PostalCodeFormat);
                                            country.PostalCodeRegex = dr.GetString(
                                                (int)CountryInfoField.PostalCodeRegex);
                                            string[] langs =
                                                dr.GetString((int)CountryInfoField.Languages)
                                                    .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                                            foreach (string lang in langs)
                                            {
                                                country.Languages.Add(lang);
                                            }

                                            country.EquivalentFipsCode =
                                                dr.GetString((int)CountryInfoField.EquivalentFipsCode);
                                            this.mapCountryInfo.Add(country.ISO, country);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                return this.mapCountryInfo;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>The get country.</summary>
        /// <param name="countryCode">The country code.</param>
        /// <returns>The <see cref="CountryInfo"/>.</returns>
        public CountryInfo GetCountry(string countryCode)
        {
            CountryInfo country;
            this.MapCountryInfo.TryGetValue(countryCode, out country);
            return country;
        }

        /// <summary>The get country.</summary>
        /// <param name="latitude">The latitude.</param>
        /// <param name="longitude">The longitude.</param>
        /// <returns>The <see cref="CountryInfo"/>.</returns>
        public CountryInfo GetCountry(decimal latitude, decimal longitude)
        {
            CountryInfo country = null;
            object valueAux;
            using (SQLiteConnection conn = new SQLiteConnection(this.connection))
            {
                conn.Open();
                conn.EnableExtensions(true);
                conn.LoadExtension("libspatialite-2.dll");
                using (SQLiteCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format(CultureInfo.InvariantCulture, QuerySql, latitude, longitude);
                    cmd.Prepare();
                    using (SQLiteDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            country = new CountryInfo();
                            country.ISO = dr.GetString((int)CountryInfoField.ISO);
                            country.ISO3 = dr.GetString((int)CountryInfoField.ISO3);
                            country.ISONumeric = dr.GetString((int)CountryInfoField.ISONumeric);
                            country.Fips = dr.GetString((int)CountryInfoField.Fips);
                            country.Country = dr.GetString((int)CountryInfoField.Country);
                            country.Capital = dr.GetString((int)CountryInfoField.Capital);
                            valueAux = dr.GetValue((int)CountryInfoField.Area);
                            country.Area = valueAux == DBNull.Value ? null : (decimal?)Convert.ToDecimal(valueAux);
                            valueAux = dr.GetValue((int)CountryInfoField.Population);
                            country.Population = valueAux == DBNull.Value ? null : (decimal?)Convert.ToDecimal(valueAux);
                            country.Continent = dr.GetString((int)CountryInfoField.Continent);
                            country.TopLevelDomain = dr.GetString((int)CountryInfoField.TopLevelDomain);
                            country.CurrencyCode = dr.GetString((int)CountryInfoField.CurrencyCode);
                            country.CurrencyName = dr.GetString((int)CountryInfoField.CurrencyName);
                            country.Phone = dr.GetString((int)CountryInfoField.Phone);
                            country.PostalCodeFormat = dr.GetString((int)CountryInfoField.PostalCodeFormat);
                            country.PostalCodeRegex = dr.GetString((int)CountryInfoField.PostalCodeRegex);
                            string[] langs = dr.GetString((int)CountryInfoField.Languages)
                                .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                            foreach (string lang in langs)
                            {
                                country.Languages.Add(lang);
                            }

                            country.EquivalentFipsCode = dr.GetString((int)CountryInfoField.EquivalentFipsCode);
                        }
                    }
                }
            }

            return country;
        }

        #endregion
    }
}