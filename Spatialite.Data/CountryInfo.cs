namespace Spatialite.Data.Countries
{
    using System.Collections.Generic;

    /// <summary>The country info.</summary>
    public class CountryInfo
    {
        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="CountryInfo" /> class.</summary>
        public CountryInfo()
        {
            this.Languages = new List<string>();
        }

        #endregion

        #region Public Properties

        /// <summary>Gets or sets the area.</summary>
        public decimal? Area { get; set; }

        /// <summary>Gets or sets the capital area.</summary>
        public string Capital { get; set; }

        /// <summary>Gets or sets the continent.</summary>
        public string Continent { get; set; }

        /// <summary>Gets or sets the country.</summary>
        public string Country { get; set; }

        /// <summary>Gets or sets the currency code.</summary>
        public string CurrencyCode { get; set; }

        /// <summary>Gets or sets the currency name.</summary>
        public string CurrencyName { get; set; }

        /// <summary>Gets or sets the equivalent fips code.</summary>
        public string EquivalentFipsCode { get; set; }

        /// <summary>Gets or sets the fips.</summary>
        public string Fips { get; set; }

        /// <summary>Gets or sets the iso.</summary>
        // ReSharper disable InconsistentNaming
        public string ISO { get; set; }

        // ReSharper restore InconsistentNaming

        /// <summary>Gets or sets the is o 3.</summary>
        // ReSharper disable InconsistentNaming
        public string ISO3 { get; set; }

        // ReSharper restore InconsistentNaming

        /// <summary>Gets or sets the iso numeric.</summary>
        // ReSharper disable InconsistentNaming
        public string ISONumeric { get; set; }

        // ReSharper restore InconsistentNaming

        /// <summary>Gets the languages.</summary>
        public IList<string> Languages { get; private set; }

        /// <summary>Gets or sets the phone.</summary>
        public string Phone { get; set; }

        /// <summary>Gets or sets the population.</summary>
        public decimal? Population { get; set; }

        /// <summary>Gets or sets the postal code format.</summary>
        public string PostalCodeFormat { get; set; }

        /// <summary>Gets or sets the postal code regex.</summary>
        public string PostalCodeRegex { get; set; }

        /// <summary>Gets or sets the tld.</summary>
        public string TopLevelDomain { get; set; }

        #endregion
    }
}