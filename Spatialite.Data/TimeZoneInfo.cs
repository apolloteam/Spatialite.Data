namespace Spatialite.Data.TimeZones
{
    /// <summary>The time zone.</summary>
    public class TimeZoneInfo
    {
        #region Public Properties

        /// <summary>Gets or sets the country code.</summary>
        public string CountryCode { get; set; }

        /// <summary>Gets or sets the dst offset.</summary>
        public decimal? DstOffset { get; set; }

        /// <summary>Gets or sets the gmt offset.</summary>
        public decimal? GmtOffset { get; set; }

        /// <summary>Gets or sets the raw offset.</summary>
        public decimal? RawOffset { get; set; }

        /// <summary>Gets or sets the time zone daylight name.</summary>
        public string TimeZoneDaylightName { get; set; }

        /// <summary>Gets or sets the time zone id.</summary>
        public string TimeZoneId { get; set; }

        /// <summary>Gets or sets the time zone name.</summary>
        public string TimeZoneName { get; set; }

        #endregion
    }
}