﻿namespace Spatialite.Data.Console
{
    using System.Collections.Generic;

    using Spatialite.Data.Countries;
    using Spatialite.Data.Importer;
    using Spatialite.Data.TimeZones;

    /// <summary>The program.</summary>
    internal class Program
    {
        #region Methods

        /// <summary>The main.</summary>
        /// <param name="args">The args.</param>
        private static void Main(string[] args)
        {
            string connStr = @"Data Source=N:\Data\Booksys\TimeZonesAndCountryInfo.db;Version=3;New=True;Pooling=True;Max Pool Size=1000;";
            DataImporter di = new DataImporter(connStr);
            di.ImportTimeZones(@"N:\Data\Local\world\tz_world.shp");
            di.ImportCountries();

            // Prueba los archivos importados.
            TimeZonesProvider tzp = new TimeZonesProvider(connStr);
            TimeZoneInfo timeZone;
            timeZone = tzp.GetTimeZone(-34.6379425M, -58.3756365M);
            timeZone = tzp.GetTimeZone(33.45M, -112.066667M);
            timeZone = tzp.GetTimeZone(-24.1931095M, -65.4455425M);
            timeZone = tzp.GetTimeZone(-34.6158527M, -58.4332985M);
            timeZone = tzp.GetTimeZone(-34.8198798M, -56.2303067M);
            timeZone = tzp.GetTimeZone(-34.8198798M, -56.2303067M);
            timeZone = tzp.GetTimeZone(-32.9264482M, -68.813779M);
            timeZone = tzp.GetTimeZone(-26.8285851M, -65.2515487M);
            timeZone = tzp.GetTimeZone(-33.6682982M, -70.363372M);
            timeZone = tzp.GetTimeZone(-41.2443701M, 174.7618546M);
            timeZone = tzp.GetTimeZone(40.4378271M, -3.6795367M);
            timeZone = tzp.GetTimeZone(25.8265645M, -80.229947M);

            CountryInfoProvider cip = new CountryInfoProvider(connStr);
            CountryInfo country;
            country = cip.GetCountry("AR");
            country = cip.GetCountry(-34.6379425M, -58.3756365M);
            country = cip.GetCountry(33.45M, -112.066667M);
            country = cip.GetCountry(-24.1931095M, -65.4455425M);
            country = cip.GetCountry(-34.6158527M, -58.4332985M);
            country = cip.GetCountry(-34.8198798M, -56.2303067M);
            country = cip.GetCountry(-34.8198798M, -56.2303067M);
            country = cip.GetCountry(-32.9264482M, -68.813779M);
            country = cip.GetCountry(-26.8285851M, -65.2515487M);
            country = cip.GetCountry(-33.6682982M, -70.363372M);
            country = cip.GetCountry(-41.2443701M, 174.7618546M);
            country = cip.GetCountry(40.4378271M, -3.6795367M);
            country = cip.GetCountry(25.8265645M, -80.229947M);
        }

        #endregion
    }
}