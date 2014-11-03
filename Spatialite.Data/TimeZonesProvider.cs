namespace Spatialite.Data.TimeZones
{
    using System;
    using System.Data.SQLite;
    using System.Globalization;

    /// <summary>The time zones fields.</summary>
    internal enum TimeZonesFields
    {
        /// <summary>Country code.</summary>
        CountryCode = 0, 

        /// <summary>TimeZone Id.</summary>
        TimeZoneId = 1, 

        /// <summary>TimeZone name.</summary>
        TimeZoneName = 2, 

        /// <summary>TimeZone daylight name.</summary>
        TimeZoneDaylightName = 3, 

        /// <summary>Gmt offset.</summary>
        GmtOffset = 4, 

        /// <summary>Dst offset.</summary>
        DstOffset = 5, 

        /// <summary>Raw offset.</summary>
        RawOffset = 6
    }

    /// <summary>Timezones provider.</summary>
    public class TimeZonesProvider
    {
        #region Constants

        /// <summary>The query sql.</summary>
        private const string QuerySql = @"SELECT 
                                            CountryCode, 
                                            TimeZoneId, 
                                            TimeZoneName, 
                                            TimeZoneDaylightName, 
                                            GmtOffset, 
                                            DstOffset, 
                                            RawOffset 
                                          FROM TimeZones 
                                          WHERE Contains( Geometry, MakePoint( {1}, {0} ) ) = 1;";

        /// <summary>The libspatialite dll.</summary>
        private const string LibspatialiteDll = "libspatialite-2.dll";

        #endregion

        #region Fields

        /// <summary>The connection.</summary>
        private readonly string connection;

        #endregion

        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="TimeZonesProvider" /> class.</summary>
        public TimeZonesProvider()
            : this(DataConfiguration.ConnectionString)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="TimeZonesProvider"/> class.</summary>
        /// <param name="connection">The connection.</param>
        public TimeZonesProvider(string connection)
        {
            this.connection = connection;
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>The get time zone.</summary>
        /// <param name="latitude">The latitude.</param>
        /// <param name="longitude">The longitude.</param>
        /// <returns>The <see cref="TimeZoneInfo"/>.</returns>
        public TimeZoneInfo GetTimeZone(decimal latitude, decimal longitude)
        {
            TimeZoneInfo tz = null;
            using (SQLiteConnection conn = new SQLiteConnection(this.connection))
            {
                conn.Open();
                conn.EnableExtensions(true);
                conn.LoadExtension(LibspatialiteDll);
                using (SQLiteCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format(CultureInfo.InvariantCulture, QuerySql, latitude, longitude);
                    cmd.Prepare();
                    using (SQLiteDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            tz = new TimeZoneInfo();
                            tz.CountryCode = dr.GetString((int)TimeZonesFields.CountryCode);
                            tz.TimeZoneId = dr.GetString((int)TimeZonesFields.TimeZoneId);
                            tz.TimeZoneName = dr.GetString((int)TimeZonesFields.TimeZoneName);
                            tz.TimeZoneDaylightName = dr.GetString((int)TimeZonesFields.TimeZoneDaylightName);
                            object aux = dr.GetValue((int)TimeZonesFields.GmtOffset);
                            tz.GmtOffset = aux == DBNull.Value ? null : (decimal?)Convert.ToDecimal(aux);
                            aux = dr.GetValue((int)TimeZonesFields.DstOffset);
                            tz.DstOffset = aux == DBNull.Value ? null : (decimal?)Convert.ToDecimal(aux);
                            aux = dr.GetValue((int)TimeZonesFields.RawOffset);
                            tz.RawOffset = aux == DBNull.Value ? null : (decimal?)Convert.ToDecimal(aux);
                        }
                    }
                }
            }

            return tz;
        }

        #endregion
    }
}