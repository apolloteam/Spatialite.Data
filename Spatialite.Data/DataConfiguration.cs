namespace Spatialite.Data
{
    using System.Configuration;

    /// <summary>The time zones provider configuration.</summary>
    internal static class DataConfiguration
    {
        #region Properties

        /// <summary>Gets the connection string.</summary>
        public static string ConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["SpatialiteData"].ConnectionString;
            }
        }

        #endregion
    }
}