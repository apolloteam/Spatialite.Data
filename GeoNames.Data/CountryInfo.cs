using System.Diagnostics.CodeAnalysis;

[module: SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Requerido por FileHelpers.")]

namespace GeoNames.Data
{
    using FileHelpers;

    /// <summary>The country info.</summary>
    [DelimitedRecord("\t")]
    [ConditionalRecord(RecordCondition.ExcludeIfBegins, "#")]
    public class CountryInfo
    {
        #region Fields

        /// <summary>The area.</summary>
        [FieldOrder(7)]
        [FieldTrim(TrimMode.Both)]
        [FieldNullValue(null)]
        public decimal? Area;

        /// <summary>The buffer.</summary>
        [FieldOrder(20)]
        [FieldOptional]
        [FieldNullValue(null)]
        public string Buffer;

        /// <summary>The capital.</summary>
        [FieldOrder(6)]
        [FieldTrim(TrimMode.Both)]
        [FieldNullValue(null)]
        public string Capital;

        /// <summary>The continent.</summary>
        [FieldOrder(9)]
        [FieldTrim(TrimMode.Both)]
        [FieldNullValue(null)]
        public string Continent;

        /// <summary>The country.</summary>
        [FieldOrder(5)]
        [FieldTrim(TrimMode.Both)]
        [FieldNullValue(null)]
        public string Country;

        /// <summary>The currency code.</summary>
        [FieldOrder(11)]
        [FieldTrim(TrimMode.Both)]
        [FieldNullValue(null)]
        public string CurrencyCode;

        /// <summary>The currency name.</summary>
        [FieldOrder(12)]
        [FieldTrim(TrimMode.Both)]
        [FieldNullValue(null)]
        public string CurrencyName;

        /// <summary>The equivalent fips code.</summary>
        [FieldOrder(19)]
        [FieldTrim(TrimMode.Both)]
        [FieldNullValue(null)]
        public string EquivalentFipsCode;

        /// <summary>The fips.</summary>
        [FieldOrder(4)]
        [FieldTrim(TrimMode.Both)]
        [FieldNullValue(null)]
        public string Fips;

        /// <summary>The geoname id.</summary>
        [FieldOrder(17)]
        [FieldTrim(TrimMode.Both)]
        [FieldNullValue(null)]
        public string GeonameId;

        /// <summary>The iso.</summary>
        [FieldOrder(1)]
        [FieldTrim(TrimMode.Both)]
        [FieldNullValue(null)]
        public string Iso;

        /// <summary>The is o 3.</summary>
        [FieldOrder(2)]
        [FieldTrim(TrimMode.Both)]
        [FieldNullValue(null)]
        public string Iso3;

        /// <summary>The iso numeric.</summary>
        [FieldOrder(3)]
        [FieldTrim(TrimMode.Both)]
        [FieldNullValue(null)]
        public string IsoNumeric;

        /// <summary>The languages.</summary>
        [FieldOrder(16)]
        [FieldTrim(TrimMode.Both)]
        [FieldNullValue(null)]
        public string Languages;

        /// <summary>The neighbours.</summary>
        [FieldOrder(18)]
        [FieldTrim(TrimMode.Both)]
        [FieldNullValue(null)]
        public string Neighbours;

        /// <summary>The phone.</summary>
        [FieldOrder(13)]
        [FieldTrim(TrimMode.Both)]
        [FieldNullValue(null)]
        public string Phone;

        /// <summary>The population.</summary>
        [FieldOrder(8)]
        [FieldTrim(TrimMode.Both)]
        [FieldNullValue(null)]
        public decimal? Population;

        /// <summary>The postal code format.</summary>
        [FieldOrder(14)]
        [FieldTrim(TrimMode.Both)]
        [FieldNullValue(null)]
        public string PostalCodeFormat;

        /// <summary>The postal code regex.</summary>
        [FieldOrder(15)]
        [FieldTrim(TrimMode.Both)]
        [FieldNullValue(null)]
        public string PostalCodeRegex;

        /// <summary>The top level domain.</summary>
        [FieldOrder(10)]
        [FieldTrim(TrimMode.Both)]
        [FieldNullValue(null)]
        public string TopLevelDomain;

        #endregion
    }
}