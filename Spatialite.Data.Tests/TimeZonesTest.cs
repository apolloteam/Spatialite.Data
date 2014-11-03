namespace Spatialite.Data.TimeZones.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>The unit test 1.</summary>
    [TestClass]
    public class TimeZonesTest
    {
        #region Methods

        /// <summary>The test method 1.</summary>
        [TestMethod]
        public void TestMethod1()
        {
            string connStr = @"Data Source=N:\Data\Timezones.db;Version=3;New=True;Pooling=True;Max Pool Size=1000;";
            TimeZonesProvider tzp = new TimeZonesProvider(connStr);
            TimeZoneInfo ret;
            ret = tzp.GetTimeZone(-34.6379425M, -58.3756365M);
            ret = tzp.GetTimeZone(33.45M, -112.066667M);
            ret = tzp.GetTimeZone(-24.1931095M, -65.4455425M);
            ret = tzp.GetTimeZone(-34.6158527M, -58.4332985M);
            ret = tzp.GetTimeZone(-34.8198798M, -56.2303067M);
            ret = tzp.GetTimeZone(-34.8198798M, -56.2303067M);
            ret = tzp.GetTimeZone(-32.9264482M, -68.813779M);
            ret = tzp.GetTimeZone(-26.8285851M, -65.2515487M);
            ret = tzp.GetTimeZone(-33.6682982M, -70.363372M);
            ret = tzp.GetTimeZone(-41.2443701M, 174.7618546M);
            ret = tzp.GetTimeZone(40.4378271M, -3.6795367M);
            ret = tzp.GetTimeZone(25.8265645M, -80.229947M);
        }

        #endregion
    }
}