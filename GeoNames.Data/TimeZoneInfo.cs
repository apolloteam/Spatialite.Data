namespace GeoNames.Data
{
    using System.Diagnostics.CodeAnalysis;

    using FileHelpers;

    /// <summary>The geo name data.</summary>
    [DelimitedRecord("\t")]
    [IgnoreFirst(1)]
    public class TimeZoneInfo
    {
        #region Fields

        /// <summary>The country code.</summary>
        [FieldTrim(TrimMode.Both)]
        [FieldOrder(1)]
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Requerido por FileHelpers.")]
        public string CountryCode;

        /// <summary>The dst offset.</summary>
        [FieldOrder(4)]
        [FieldTrim(TrimMode.Both)]
        [FieldConverter(ConverterKind.Decimal)]
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Requerido por FileHelpers.")]
        public decimal? DstOffset;

        /// <summary>The gmt offset.</summary>
        [FieldOrder(3)]
        [FieldTrim(TrimMode.Both)]
        [FieldConverter(ConverterKind.Decimal)]
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Requerido por FileHelpers.")]
        public decimal? GmtOffset;

        /// <summary>The raw offset.</summary>
        [FieldOrder(5)]
        [FieldTrim(TrimMode.Both)]
        [FieldConverter(ConverterKind.Decimal)]
        [FieldNullValue(null)]
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Requerido por FileHelpers.")]
        public decimal? RawOffset;

        /// <summary>The time zone id.</summary>
        [FieldOrder(2)]
        [FieldTrim(TrimMode.Both)]
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Requerido por FileHelpers.")]
        public string TimeZoneId;

        #endregion
    }
}