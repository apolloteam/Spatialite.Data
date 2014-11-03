namespace GeoNames.Data
{
    using System.Diagnostics.CodeAnalysis;

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
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Requerido por FileHelpers.")]
        public decimal? Area;

        /// <summary>The buffer.</summary>
        [FieldOrder(20)]
        [FieldOptional]
        [FieldNullValue(null)]
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Requerido por FileHelpers.")]
        public string Buffer;

        /// <summary>The capital.</summary>
        [FieldOrder(6)]
        [FieldTrim(TrimMode.Both)]
        [FieldNullValue(null)]
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Requerido por FileHelpers.")]
        public string Capital;

        /// <summary>The continent.</summary>
        [FieldOrder(9)]
        [FieldTrim(TrimMode.Both)]
        [FieldNullValue(null)]
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Requerido por FileHelpers.")]
        public string Continent;

        /// <summary>The country.</summary>
        [FieldOrder(5)]
        [FieldTrim(TrimMode.Both)]
        [FieldNullValue(null)]
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Requerido por FileHelpers.")]
        public string Country;

        /// <summary>The currency code.</summary>
        [FieldOrder(11)]
        [FieldTrim(TrimMode.Both)]
        [FieldNullValue(null)]
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Requerido por FileHelpers.")]
        public string CurrencyCode;

        /// <summary>The currency name.</summary>
        [FieldOrder(12)]
        [FieldTrim(TrimMode.Both)]
        [FieldNullValue(null)]
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Requerido por FileHelpers.")]
        public string CurrencyName;

        /// <summary>The equivalent fips code.</summary>
        [FieldOrder(19)]
        [FieldTrim(TrimMode.Both)]
        [FieldNullValue(null)]
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Requerido por FileHelpers.")]
        public string EquivalentFipsCode;

        /// <summary>The fips.</summary>
        [FieldOrder(4)]
        [FieldTrim(TrimMode.Both)]
        [FieldNullValue(null)]
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Requerido por FileHelpers.")]
        public string Fips;

        /// <summary>The geoname id.</summary>
        [FieldOrder(17)]
        [FieldTrim(TrimMode.Both)]
        [FieldNullValue(null)]
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Requerido por FileHelpers.")]
        public string GeonameId;

        /// <summary>The iso.</summary>
        [FieldOrder(1)]
        [FieldTrim(TrimMode.Both)]
        [FieldNullValue(null)]
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Requerido por FileHelpers.")]
        public string ISO;

        /// <summary>The is o 3.</summary>
        [FieldOrder(2)]
        [FieldTrim(TrimMode.Both)]
        [FieldNullValue(null)]
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Requerido por FileHelpers.")]
        public string ISO3;

        /// <summary>The iso numeric.</summary>
        [FieldOrder(3)]
        [FieldTrim(TrimMode.Both)]
        [FieldNullValue(null)]
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Requerido por FileHelpers.")]
        public string ISONumeric;

        /// <summary>The languages.</summary>
        [FieldOrder(16)]
        [FieldTrim(TrimMode.Both)]
        [FieldNullValue(null)]
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Requerido por FileHelpers.")]
        public string Languages;

        /// <summary>The neighbours.</summary>
        [FieldOrder(18)]
        [FieldTrim(TrimMode.Both)]
        [FieldNullValue(null)]
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Requerido por FileHelpers.")]
        public string Neighbours;

        /// <summary>The phone.</summary>
        [FieldOrder(13)]
        [FieldTrim(TrimMode.Both)]
        [FieldNullValue(null)]
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Requerido por FileHelpers.")]
        public string Phone;

        /// <summary>The population.</summary>
        [FieldOrder(8)]
        [FieldTrim(TrimMode.Both)]
        [FieldNullValue(null)]
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Requerido por FileHelpers.")]
        public decimal? Population;

        /// <summary>The postal code format.</summary>
        [FieldOrder(14)]
        [FieldTrim(TrimMode.Both)]
        [FieldNullValue(null)]
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Requerido por FileHelpers.")]
        public string PostalCodeFormat;

        /// <summary>The postal code regex.</summary>
        [FieldOrder(15)]
        [FieldTrim(TrimMode.Both)]
        [FieldNullValue(null)]
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Requerido por FileHelpers.")]
        public string PostalCodeRegex;

        /// <summary>The top level domain.</summary>
        [FieldOrder(10)]
        [FieldTrim(TrimMode.Both)]
        [FieldNullValue(null)]
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Requerido por FileHelpers.")]
        public string TopLevelDomain;

        #endregion
    }
}