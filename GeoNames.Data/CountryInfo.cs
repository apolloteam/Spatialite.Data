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
        [FieldTrim(TrimMode.Both)]
        [FieldNullValue(null)]
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Requerido por FileHelpers.")]
        public decimal? Area;

        /// <summary>The buffer.</summary>
        [FieldOptional]
        [FieldNullValue(null)]
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Requerido por FileHelpers.")]
        public string Buffer;

        /// <summary>The capital.</summary>
        [FieldTrim(TrimMode.Both)]
        [FieldNullValue(null)]
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Requerido por FileHelpers.")]
        public string Capital;

        /// <summary>The continent.</summary>
        [FieldTrim(TrimMode.Both)]
        [FieldNullValue(null)]
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Requerido por FileHelpers.")]
        public string Continent;

        /// <summary>The country.</summary>
        [FieldTrim(TrimMode.Both)]
        [FieldNullValue(null)]
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Requerido por FileHelpers.")]
        public string Country;

        /// <summary>The currency code.</summary>
        [FieldTrim(TrimMode.Both)]
        [FieldNullValue(null)]
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Requerido por FileHelpers.")]
        public string CurrencyCode;

        /// <summary>The currency name.</summary>
        [FieldTrim(TrimMode.Both)]
        [FieldNullValue(null)]
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Requerido por FileHelpers.")]
        public string CurrencyName;

        /// <summary>The equivalent fips code.</summary>
        [FieldTrim(TrimMode.Both)]
        [FieldNullValue(null)]
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Requerido por FileHelpers.")]
        public string EquivalentFipsCode;

        /// <summary>The fips.</summary>
        [FieldTrim(TrimMode.Both)]
        [FieldNullValue(null)]
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Requerido por FileHelpers.")]
        public string Fips;

        /// <summary>The geoname id.</summary>
        [FieldTrim(TrimMode.Both)]
        [FieldNullValue(null)]
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Requerido por FileHelpers.")]
        public string GeonameId;

        /// <summary>The iso.</summary>
        [FieldTrim(TrimMode.Both)]
        [FieldNullValue(null)]
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Requerido por FileHelpers.")]
        public string ISO;

        /// <summary>The is o 3.</summary>
        [FieldTrim(TrimMode.Both)]
        [FieldNullValue(null)]
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Requerido por FileHelpers.")]
        public string ISO3;

        /// <summary>The iso numeric.</summary>
        [FieldTrim(TrimMode.Both)]
        [FieldNullValue(null)]
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Requerido por FileHelpers.")]
        public string ISONumeric;

        /// <summary>The languages.</summary>
        [FieldTrim(TrimMode.Both)]
        [FieldNullValue(null)]
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Requerido por FileHelpers.")]
        public string Languages;

        /// <summary>The neighbours.</summary>
        [FieldTrim(TrimMode.Both)]
        [FieldNullValue(null)]
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Requerido por FileHelpers.")]
        public string Neighbours;

        /// <summary>The phone.</summary>
        [FieldTrim(TrimMode.Both)]
        [FieldNullValue(null)]
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Requerido por FileHelpers.")]
        public string Phone;

        /// <summary>The population.</summary>
        [FieldTrim(TrimMode.Both)]
        [FieldNullValue(null)]
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Requerido por FileHelpers.")]
        public decimal? Population;

        /// <summary>The postal code format.</summary>
        [FieldTrim(TrimMode.Both)]
        [FieldNullValue(null)]
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Requerido por FileHelpers.")]
        public string PostalCodeFormat;

        /// <summary>The postal code regex.</summary>
        [FieldTrim(TrimMode.Both)]
        [FieldNullValue(null)]
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Requerido por FileHelpers.")]
        public string PostalCodeRegex;

        /// <summary>The top level domain.</summary>
        [FieldTrim(TrimMode.Both)]
        [FieldNullValue(null)]
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Requerido por FileHelpers.")]
        public string TopLevelDomain;

        #endregion
    }
}