namespace TimezoneConverter
{
    using System.Configuration;

    /// <summary>The timezone converter configuration.</summary>
    internal static class TimezoneConverterConfiguration
    {
        #region Public Properties

        /// <summary>Gets the file path.</summary>
        public static string File
        {
            get
            {
                return ConfigurationManager.AppSettings["OlsonToWindowsTZMapFile"] ?? "windowsZones.xml";
            }
        }

        #endregion
    }
}