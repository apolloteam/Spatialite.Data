namespace GeoNames.Data
{
    using System.Configuration;

    /// <summary>The geo names configuration.</summary>
    internal static class GeoNamesConfiguration
    {
        #region Properties

        /// <summary>Gets the country info file.</summary>
        public static string CountryInfoFile
        {
            get
            {
                return ConfigurationManager.AppSettings["GeoNamesCountryInfoFile"] ?? "countryInfo.txt";
            }
        }

        /// <summary>Gets the time zones file.</summary>
        public static string TimeZonesFile
        {
            get
            {
                return ConfigurationManager.AppSettings["GeoNamesTimeZonesFile"] ?? "timeZones.txt";
            }
        }

        #endregion
    }
}