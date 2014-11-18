namespace Spatialite.Data
{
    using System;
    using System.Configuration;
    using System.IO;
    using System.Reflection;

    /// <summary>The time zones provider configuration.</summary>
    public static class DataConfiguration
    {
        #region Constants

        /// <summary>The libspatialite dll.</summary>
        //// private const string LibSpatialiteDll = "libspatialite-2";
        private const string LibSpatialiteDll = "spatialite.dll";

        #endregion

        #region Static Fields

        /// <summary>Synck lock.</summary>
        private static readonly object SynckLock = new object();

        /// <summary>Library runtime path.</summary>
        private static string libPath;

        #endregion

        #region Properties

        /// <summary>Gets the connection string.</summary>
        public static string ConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["SpatialiteData"].ConnectionString;
            }
        }

        /// <summary>Spatialite path.</summary>
        /// <returns>Dll Path.<see cref="string" />.</returns>
        public static string LibSpatialite
        {
            get
            {
                if (libPath == null)
                {
                    lock (SynckLock)
                    {
                        string architecture = Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE") ?? string.Empty;
                        if (architecture == "AMD64")
                        {
                            architecture = "x64";
                        }
                        string location = Assembly.GetExecutingAssembly().Location;
                        string lib = Path.Combine(architecture, LibSpatialiteDll);
                        libPath = lib;
                        if (true && !File.Exists(libPath))
                        {
                            libPath = Path.Combine(location, lib);
                            if (!File.Exists(libPath))
                            {
                                libPath = Path.Combine(location, "bin", lib);
                                if (!File.Exists(libPath))
                                {
                                    libPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, lib);
                                    if (!File.Exists(libPath))
                                    {
                                        libPath = Path.Combine(
                                            AppDomain.CurrentDomain.BaseDirectory,
                                            "bin",
                                            lib);
                                    }
                                }
                            }
                        }
                    }
                }

                return libPath;
            }
        }

        #endregion
    }
}