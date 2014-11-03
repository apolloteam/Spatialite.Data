namespace GeoNames.Data
{
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;

    using FileHelpers;

    /// <summary>The geo names provider.</summary>
    public sealed class GeoNamesProvider
    {
        #region Static Fields

        /// <summary>The sync lock.</summary>
        private static readonly object SyncLock = new object();

        /// <summary>The map geo name country info file.</summary>
        private static IDictionary<string, CountryInfo> mapGeoNameCountryInfoFile;

        /// <summary>The map geo name time zone file.</summary>
        private static IDictionary<string, TimeZoneInfo> mapGeoNameTimeZoneFile;

        #endregion

        #region Properties

        /// <summary>Gets the map geo name country info file.</summary>
        private static IDictionary<string, CountryInfo> MapGeoNameCountryInfoFile
        {
            get
            {
                if (mapGeoNameCountryInfoFile == null)
                {
                    lock (SyncLock)
                    {
                        if (mapGeoNameCountryInfoFile == null)
                        {
                            IDictionary<string, CountryInfo> geoNameCountryInfoFile = LoadCountryInfoFromFile();
                            mapGeoNameCountryInfoFile =
                                new ConcurrentDictionary<string, CountryInfo>(geoNameCountryInfoFile);
                        }
                    }
                }

                return mapGeoNameCountryInfoFile;
            }
        }

        /// <summary>Gets the map geo name time zone file.</summary>
        private static IDictionary<string, TimeZoneInfo> MapGeoNameTimeZoneFile
        {
            get
            {
                if (mapGeoNameTimeZoneFile == null)
                {
                    lock (SyncLock)
                    {
                        if (mapGeoNameTimeZoneFile == null)
                        {
                            IDictionary<string, TimeZoneInfo> geoNameTimeZoneFile = LoadTimeZonesFromFile();
                            mapGeoNameTimeZoneFile = new ConcurrentDictionary<string, TimeZoneInfo>(geoNameTimeZoneFile);
                        }
                    }
                }

                return mapGeoNameTimeZoneFile;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>Get list of countries.</summary>
        /// <returns>List of countryInfo. <see cref="IEnumerable"/></returns>
        public static IEnumerable<CountryInfo> GetCountries()
        {
            return MapGeoNameCountryInfoFile.Select(kvp => kvp.Value);
        }

        /// <summary>Returns a timezoneinfo object or null for the timezoneid request .</summary>
        /// <param name="countryCode">Código del pais.</param>
        /// <returns>A TimeZoneInfo object or null. <see cref="TimeZoneInfo"/>.</returns>
        public static CountryInfo GetCountryInfo(string countryCode)
        {
            CountryInfo countryInfo = null;
            MapGeoNameCountryInfoFile.TryGetValue(countryCode, out countryInfo);
            return countryInfo;
        }

        /// <summary>Returns a timezoneinfo object or null for the timezoneid request .</summary>
        /// <param name="timeZoneId">The TimeZoneId (olson).</param>
        /// <returns>A TimeZoneInfo object or null. <see cref="TimeZoneInfo"/>.</returns>
        public static TimeZoneInfo GetTimeZoneInfo(string timeZoneId)
        {
            TimeZoneInfo timeZoneInfo = null;
            MapGeoNameTimeZoneFile.TryGetValue(timeZoneId, out timeZoneInfo);
            return timeZoneInfo;
        }

        #endregion

        #region Methods

        /// <summary>Load the file timeZones.txt.</summary>
        /// <returns>Dictionary of timezones index by timezoneId (olson).<see cref="IDictionary" />.</returns>
        private static IDictionary<string, CountryInfo> LoadCountryInfoFromFile()
        {
            IDictionary<string, CountryInfo> data = new Dictionary<string, CountryInfo>();
            using (FileHelperAsyncEngine<CountryInfo> engine = new FileHelperAsyncEngine<CountryInfo>())
            {
                string fileCsv = GeoNamesConfiguration.CountryInfoFile;
                engine.BeginReadFile(fileCsv);
                foreach (CountryInfo item in engine)
                {
                    data.Add(item.Country, item);
                }
            }

            return data;
        }

        /// <summary>Load the file timeZones.txt.</summary>
        /// <returns>Dictionary of timezones index by timezoneId (olson).<see cref="IDictionary" />.</returns>
        private static IDictionary<string, TimeZoneInfo> LoadTimeZonesFromFile()
        {
            IDictionary<string, TimeZoneInfo> data = new Dictionary<string, TimeZoneInfo>();
            using (FileHelperAsyncEngine<TimeZoneInfo> engine = new FileHelperAsyncEngine<TimeZoneInfo>())
            {
                string fileCsv = GeoNamesConfiguration.TimeZonesFile;
                engine.BeginReadFile(fileCsv);
                foreach (TimeZoneInfo item in engine)
                {
                    data.Add(item.TimeZoneId, item);
                }
            }

            return data;
        }

        #endregion
    }
}