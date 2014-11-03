namespace TimezoneConverter
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Xml;

    /// <summary>
    /// Convierte los distintos formatos de timezone.
    /// </summary>
    public sealed class TimezoneConverterProvider
    {
        #region Static Fields

        /// <summary>The _sync lock.</summary>
        private static readonly object SyncLock = new object();

        /// <summary>Map olzon TimeZoneId to windows Daylight timezone names.</summary>
        private static IDictionary<string, string> mapDaylightNames;

        /// <summary>The m_map from xml.</summary>
        private static IDictionary<string, string> mapFromXml;

        /// <summary>Map olzon TimeZoneId to windows standar timezone names.</summary>
        private static IDictionary<string, string> mapStandarNames;

        #endregion

        #region Properties

        /// <summary>Gets the map daylight.</summary>
        private static IDictionary<string, string> MapDaylight
        {
            get
            {
                if (mapDaylightNames == null)
                {
                    lock (SyncLock)
                    {
                        if (mapDaylightNames == null)
                        {
                            IDictionary<string, string> dayligthNames = GetDayligthNames();
                            mapDaylightNames = new ConcurrentDictionary<string, string>(dayligthNames);
                        }
                    }
                }

                return mapDaylightNames;
            }
        }

        /// <summary>Gets the map from xml.</summary>
        private static IDictionary<string, string> MapFromXml
        {
            get
            {
                if (mapFromXml == null)
                {
                    lock (SyncLock)
                    {
                        if (mapFromXml == null)
                        {
                            mapFromXml = LoadFromXml();
                        }
                    }
                }

                return mapFromXml;
            }
        }

        /// <summary>Gets the map std.</summary>
        private static IDictionary<string, string> MapStandarNames
        {
            get
            {
                if (mapStandarNames == null)
                {
                    lock (SyncLock)
                    {
                        if (mapStandarNames == null)
                        {
                            IDictionary<string, string> standarNames = GetStandarNames();
                            mapStandarNames = new ConcurrentDictionary<string, string>(standarNames);
                        }
                    }
                }

                return mapStandarNames;
            }
        }

        #endregion

        #region Methods

        /// <summary>Devuelve el windows timezoneId correspondiente o null.</summary>
        /// <param name="timezoneId">Olson TimezonId.</param>
        /// <param name="daylight">Indica si se devuelve el nombre Standard o Daylight.</param>
        /// <returns>Windows TimezoneId o null.</returns>
        public static string OlsonToWindows(string timezoneId, bool daylight = false)
        {
            string ret = null;
            IDictionary<string, string> map = daylight ? MapDaylight : MapStandarNames;
            map.TryGetValue(timezoneId, out ret);
            return ret;
        }

        /// <summary>The load dayligth names.</summary>
        /// <returns>DayligthNames map.</returns>
        private static IDictionary<string, string> GetDayligthNames()
        {
            IDictionary<string, string> dayligthNames = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
                                                            {
                                                                {
                                                                    "Africa/Abidjan", 
                                                                    "Greenwich Daylight Time"
                                                                }, 
                                                                {
                                                                    "Africa/Accra", 
                                                                    "Greenwich Daylight Time"
                                                                }, 
                                                                {
                                                                    "Africa/Addis_Ababa", 
                                                                    "E. Africa Daylight Time"
                                                                }, 
                                                                {
                                                                    "Africa/Algiers", 
                                                                    "Central Europe Daylight Time"
                                                                }, 
                                                                {
                                                                    "Africa/Asmara", 
                                                                    "E. Africa Daylight Time"
                                                                }, 
                                                                {
                                                                    "Africa/Bamako", 
                                                                    "Greenwich Daylight Time"
                                                                }, 
                                                                {
                                                                    "Africa/Bangui", 
                                                                    "W. Central Africa Daylight Time"
                                                                }, 
                                                                {
                                                                    "Africa/Banjul", 
                                                                    "Greenwich Daylight Time"
                                                                }, 
                                                                {
                                                                    "Africa/Bissau", 
                                                                    "Greenwich Daylight Time"
                                                                }, 
                                                                {
                                                                    "Africa/Blantyre", 
                                                                    "South Africa Daylight Time"
                                                                }, 
                                                                {
                                                                    "Africa/Brazzaville", 
                                                                    "W. Central Africa Daylight Time"
                                                                }, 
                                                                {
                                                                    "Africa/Bujumbura", 
                                                                    "South Africa Daylight Time"
                                                                }, 
                                                                {
                                                                    "Africa/Cairo", 
                                                                    "Egypt Daylight Time"
                                                                }, 
                                                                {
                                                                    "Africa/Casablanca", 
                                                                    "Greenwich Daylight Time"
                                                                }, 
                                                                {
                                                                    "Africa/Ceuta", 
                                                                    "Central Europe Daylight Time"
                                                                }, 
                                                                {
                                                                    "Africa/Conakry", 
                                                                    "Greenwich Daylight Time"
                                                                }, 
                                                                {
                                                                    "Africa/Dakar", 
                                                                    "Greenwich Daylight Time"
                                                                }, 
                                                                {
                                                                    "Africa/Dar_es_Salaam", 
                                                                    "E. Africa Daylight Time"
                                                                }, 
                                                                {
                                                                    "Africa/Djibouti", 
                                                                    "E. Africa Daylight Time"
                                                                }, 
                                                                {
                                                                    "Africa/Douala", 
                                                                    "W. Central Africa Daylight Time"
                                                                }, 
                                                                {
                                                                    "Africa/El_Aaiun", 
                                                                    "Greenwich Daylight Time"
                                                                }, 
                                                                {
                                                                    "Africa/Freetown", 
                                                                    "Greenwich Daylight Time"
                                                                }, 
                                                                {
                                                                    "Africa/Gaborone", 
                                                                    "South Africa Daylight Time"
                                                                }, 
                                                                {
                                                                    "Africa/Harare", 
                                                                    "South Africa Daylight Time"
                                                                }, 
                                                                {
                                                                    "Africa/Johannesburg", 
                                                                    "South Africa Daylight Time"
                                                                }, 
                                                                {
                                                                    "Africa/Kampala", 
                                                                    "E. Africa Daylight Time"
                                                                }, 
                                                                {
                                                                    "Africa/Khartoum", 
                                                                    "E. Africa Daylight Time"
                                                                }, 
                                                                {
                                                                    "Africa/Kigali", 
                                                                    "South Africa Daylight Time"
                                                                }, 
                                                                {
                                                                    "Africa/Kinshasa", 
                                                                    "W. Central Africa Daylight Time"
                                                                }, 
                                                                {
                                                                    "Africa/Lagos", 
                                                                    "W. Central Africa Daylight Time"
                                                                }, 
                                                                {
                                                                    "Africa/Libreville", 
                                                                    "W. Central Africa Daylight Time"
                                                                }, 
                                                                {
                                                                    "Africa/Lome", 
                                                                    "Greenwich Daylight Time"
                                                                }, 
                                                                {
                                                                    "Africa/Luanda", 
                                                                    "W. Central Africa Daylight Time"
                                                                }, 
                                                                {
                                                                    "Africa/Lubumbashi", 
                                                                    "South Africa Daylight Time"
                                                                }, 
                                                                {
                                                                    "Africa/Lusaka", 
                                                                    "South Africa Daylight Time"
                                                                }, 
                                                                {
                                                                    "Africa/Malabo", 
                                                                    "W. Central Africa Daylight Time"
                                                                }, 
                                                                {
                                                                    "Africa/Maputo", 
                                                                    "South Africa Daylight Time"
                                                                }, 
                                                                {
                                                                    "Africa/Maseru", 
                                                                    "South Africa Daylight Time"
                                                                }, 
                                                                {
                                                                    "Africa/Mbabane", 
                                                                    "South Africa Daylight Time"
                                                                }, 
                                                                {
                                                                    "Africa/Mogadishu", 
                                                                    "E. Africa Daylight Time"
                                                                }, 
                                                                {
                                                                    "Africa/Monrovia", 
                                                                    "Greenwich Daylight Time"
                                                                }, 
                                                                {
                                                                    "Africa/Nairobi", 
                                                                    "E. Africa Daylight Time"
                                                                }, 
                                                                {
                                                                    "Africa/Ndjamena", 
                                                                    "W. Central Africa Daylight Time"
                                                                }, 
                                                                {
                                                                    "Africa/Niamey", 
                                                                    "W. Central Africa Daylight Time"
                                                                }, 
                                                                {
                                                                    "Africa/Nouakchott", 
                                                                    "Greenwich Daylight Time"
                                                                }, 
                                                                {
                                                                    "Africa/Ouagadougou", 
                                                                    "Greenwich Daylight Time"
                                                                }, 
                                                                {
                                                                    "Africa/Porto-Novo", 
                                                                    "W. Central Africa Daylight Time"
                                                                }, 
                                                                {
                                                                    "Africa/Sao_Tome", 
                                                                    "Greenwich Daylight Time"
                                                                }, 
                                                                {
                                                                    "Africa/Tripoli", 
                                                                    "South Africa Daylight Time"
                                                                }, 
                                                                {
                                                                    "Africa/Tunis", 
                                                                    "Central Europe Daylight Time"
                                                                }, 
                                                                {
                                                                    "Africa/Windhoek", 
                                                                    "W. Central Africa Daylight Time"
                                                                }, 
                                                                {
                                                                    "America/Adak", 
                                                                    "Hawaiian Daylight Time"
                                                                }, 
                                                                {
                                                                    "America/Anchorage", 
                                                                    "Alaskan Daylight Time"
                                                                }, 
                                                                {
                                                                    "America/Anguilla", 
                                                                    "Atlantic Daylight Time"
                                                                }, 
                                                                {
                                                                    "America/Antigua", 
                                                                    "Atlantic Daylight Time"
                                                                }, 
                                                                {
                                                                    "America/Araguaina", 
                                                                    "E. South America Daylight Time"
                                                                }, 
                                                                {
                                                                    "America/Argentina/Buenos_Aires", 
                                                                    "Argentina Daylight Time"
                                                                }, 
                                                                {
                                                                    "America/Argentina/Catamarca", 
                                                                    "Argentina Daylight Time"
                                                                }, 
                                                                {
                                                                    "America/Argentina/Cordoba", 
                                                                    "Argentina Daylight Time"
                                                                }, 
                                                                {
                                                                    "America/Argentina/Jujuy", 
                                                                    "Argentina Daylight Time"
                                                                }, 
                                                                {
                                                                    "America/Argentina/La_Rioja", 
                                                                    "Argentina Daylight Time"
                                                                }, 
                                                                {
                                                                    "America/Argentina/Mendoza", 
                                                                    "Argentina Daylight Time"
                                                                }, 
                                                                {
                                                                    "America/Argentina/Rio_Gallegos", 
                                                                    "Argentina Daylight Time"
                                                                }, 
                                                                {
                                                                    "America/Argentina/San_Juan", 
                                                                    "Argentina Daylight Time"
                                                                }, 
                                                                {
                                                                    "America/Argentina/San_Luis", 
                                                                    "Argentina Daylight Time"
                                                                }, 
                                                                {
                                                                    "America/Argentina/Tucuman", 
                                                                    "Argentina Daylight Time"
                                                                }, 
                                                                {
                                                                    "America/Argentina/Ushuaia", 
                                                                    "Argentina Daylight Time"
                                                                }, 
                                                                {
                                                                    "America/Argentina/Salta", 
                                                                    "Argentina Daylight Time"
                                                                }, 
                                                                {
                                                                    "America/Aruba", 
                                                                    "Atlantic Daylight Time"
                                                                }, 
                                                                {
                                                                    "America/Asuncion", 
                                                                    "Central Brazilian Daylight Time"
                                                                }, 
                                                                {
                                                                    "America/Atikokan", 
                                                                    "Eastern Daylight Time"
                                                                }, 
                                                                {
                                                                    "America/Bahia", 
                                                                    "E. South America Daylight Time"
                                                                }, 
                                                                {
                                                                    "America/Barbados", 
                                                                    "Atlantic Daylight Time"
                                                                }, 
                                                                {
                                                                    "America/Belem", 
                                                                    "E. South America Daylight Time"
                                                                }, 
                                                                {
                                                                    "America/Belize", 
                                                                    "Central Daylight Time (Mexico)"
                                                                }, 
                                                                {
                                                                    "America/Blanc-Sablon", 
                                                                    "Atlantic Daylight Time"
                                                                }, 
                                                                {
                                                                    "America/Boa_Vista", 
                                                                    "Central Brazilian Daylight Time"
                                                                }, 
                                                                {
                                                                    "America/Bogota", 
                                                                    "SA Pacific Daylight Time"
                                                                }, 
                                                                {
                                                                    "America/Boise", 
                                                                    "Mountain Daylight Time"
                                                                }, 
                                                                {
                                                                    "America/Cambridge_Bay", 
                                                                    "Mountain Daylight Time"
                                                                }, 
                                                                {
                                                                    "America/Campo_Grande", 
                                                                    "SA Western Daylight Time"
                                                                }, 
                                                                {
                                                                    "America/Cancun", 
                                                                    "Central Daylight Time (Mexico)"
                                                                }, 
                                                                {
                                                                    "America/Caracas", 
                                                                    "Newfoundland Standard Time"
                                                                }, 
                                                                {
                                                                    "America/Cayenne", 
                                                                    "SA Eastern Daylight Time"
                                                                }, 
                                                                {
                                                                    "America/Cayman", 
                                                                    "Eastern Daylight Time"
                                                                }, 
                                                                {
                                                                    "America/Chicago", 
                                                                    "Central Daylight Time"
                                                                }, 
                                                                {
                                                                    "America/Chihuahua", 
                                                                    "Mountain Daylight Time (Mexico)"
                                                                }, 
                                                                {
                                                                    "America/Costa_Rica", 
                                                                    "Central Daylight Time (Mexico)"
                                                                }, 
                                                                {
                                                                    "America/Cuiaba", 
                                                                    "SA Western Daylight Time"
                                                                }, 
                                                                {
                                                                    "America/Curacao", 
                                                                    "Atlantic Daylight Time"
                                                                }, 
                                                                {
                                                                    "America/Danmarkshavn", 
                                                                    "Greenwich Daylight Time"
                                                                }, 
                                                                {
                                                                    "America/Dawson", 
                                                                    "Pacific Daylight Time"
                                                                }, 
                                                                {
                                                                    "America/Dawson_Creek", 
                                                                    "Mountain Daylight Time"
                                                                }, 
                                                                {
                                                                    "America/Denver", 
                                                                    "Mountain Daylight Time"
                                                                }, 
                                                                {
                                                                    "America/Detroit", 
                                                                    "Eastern Daylight Time"
                                                                }, 
                                                                {
                                                                    "America/Dominica", 
                                                                    "Atlantic Daylight Time"
                                                                }, 
                                                                {
                                                                    "America/Edmonton", 
                                                                    "Mountain Daylight Time"
                                                                }, 
                                                                {
                                                                    "America/Eirunepe", 
                                                                    "SA Pacific Daylight Time"
                                                                }, 
                                                                {
                                                                    "America/El_Salvador", 
                                                                    "Central Daylight Time (Mexico)"
                                                                }, 
                                                                {
                                                                    "America/Fortaleza", 
                                                                    "E. South America Daylight Time"
                                                                }, 
                                                                {
                                                                    "America/Glace_Bay", 
                                                                    "Atlantic Daylight Time"
                                                                }, 
                                                                {
                                                                    "America/Godthab", 
                                                                    "Greenland Daylight Time"
                                                                }, 
                                                                {
                                                                    "America/Goose_Bay", 
                                                                    "Atlantic Daylight Time"
                                                                }, 
                                                                {
                                                                    "America/Grand_Turk", 
                                                                    "Eastern Daylight Time"
                                                                }, 
                                                                {
                                                                    "America/Grenada", 
                                                                    "Atlantic Daylight Time"
                                                                }, 
                                                                {
                                                                    "America/Guadeloupe", 
                                                                    "Atlantic Daylight Time"
                                                                }, 
                                                                {
                                                                    "America/Guatemala", 
                                                                    "Central Daylight Time (Mexico)"
                                                                }, 
                                                                {
                                                                    "America/Guayaquil", 
                                                                    "SA Pacific Daylight Time"
                                                                }, 
                                                                {
                                                                    "America/Guyana", 
                                                                    "SA Western Daylight Time"
                                                                }, 
                                                                {
                                                                    "America/Halifax", 
                                                                    "Atlantic Daylight Time"
                                                                }, 
                                                                {
                                                                    "America/Havana", 
                                                                    "SA Pacific Daylight Time"
                                                                }, 
                                                                {
                                                                    "America/Hermosillo", 
                                                                    "Mountain Daylight Time"
                                                                }, 
                                                                {
                                                                    "America/Indiana/Indianapolis", 
                                                                    "Eastern Daylight Time"
                                                                }, 
                                                                {
                                                                    "America/Indiana/Knox", 
                                                                    "Central Daylight Time"
                                                                }, 
                                                                {
                                                                    "America/Indiana/Marengo", 
                                                                    "Eastern Daylight Time"
                                                                }, 
                                                                {
                                                                    "America/Indiana/Petersburg", 
                                                                    "Eastern Daylight Time"
                                                                }, 
                                                                {
                                                                    "America/Indiana/Tell_City", 
                                                                    "Central Daylight Time"
                                                                }, 
                                                                {
                                                                    "America/Indiana/Vevay", 
                                                                    "Eastern Daylight Time"
                                                                }, 
                                                                {
                                                                    "America/Indiana/Vincennes", 
                                                                    "Eastern Daylight Time"
                                                                }, 
                                                                {
                                                                    "America/Indiana/Winamac", 
                                                                    "Eastern Daylight Time"
                                                                }, 
                                                                {
                                                                    "America/Inuvik", 
                                                                    "Mountain Daylight Time"
                                                                }, 
                                                                {
                                                                    "America/Iqaluit", 
                                                                    "Eastern Daylight Time"
                                                                }, 
                                                                {
                                                                    "America/Jamaica", 
                                                                    "Eastern Daylight Time"
                                                                }, 
                                                                {
                                                                    "America/Juneau", 
                                                                    "Alaskan Daylight Time"
                                                                }, 
                                                                {
                                                                    "America/Kentucky/Louisville", 
                                                                    "Eastern Daylight Time"
                                                                }, 
                                                                {
                                                                    "America/Kentucky/Monticello", 
                                                                    "Eastern Daylight Time"
                                                                }, 
                                                                {
                                                                    "America/La_Paz", 
                                                                    "SA Western Daylight Time"
                                                                }, 
                                                                {
                                                                    "America/Lima", 
                                                                    "SA Pacific Daylight Time"
                                                                }, 
                                                                {
                                                                    "America/Los_Angeles", 
                                                                    "Pacific Daylight Time"
                                                                }, 
                                                                {
                                                                    "America/Maceio", 
                                                                    "E. South America Daylight Time"
                                                                }, 
                                                                {
                                                                    "America/Managua", 
                                                                    "Central Daylight Time (Mexico)"
                                                                }, 
                                                                {
                                                                    "America/Manaus", 
                                                                    "Central Brazilian Daylight Time"
                                                                }, 
                                                                {
                                                                    "America/Martinique", 
                                                                    "Atlantic Daylight Time"
                                                                }, 
                                                                {
                                                                    "America/Mazatlan", 
                                                                    "Mountain Daylight Time (Mexico)"
                                                                }, 
                                                                {
                                                                    "America/Menominee", 
                                                                    "Central Daylight Time (Mexico)"
                                                                }, 
                                                                {
                                                                    "America/Merida", 
                                                                    "Central Daylight Time (Mexico)"
                                                                }, 
                                                                {
                                                                    "America/Mexico_City", 
                                                                    "Central Daylight Time (Mexico)"
                                                                }, 
                                                                {
                                                                    "America/Miquelon", 
                                                                    "Greenland Daylight Time"
                                                                }, 
                                                                {
                                                                    "America/Moncton", 
                                                                    "Atlantic Daylight Time"
                                                                }, 
                                                                {
                                                                    "America/Monterrey", 
                                                                    "Central Daylight Time (Mexico)"
                                                                }, 
                                                                {
                                                                    "America/Montevideo", 
                                                                    "Montevideo Daylight Time"
                                                                }, 
                                                                {
                                                                    "America/Montreal", 
                                                                    "Eastern Daylight Time"
                                                                }, 
                                                                {
                                                                    "America/Montserrat", 
                                                                    "Atlantic Daylight Time"
                                                                }, 
                                                                {
                                                                    "America/Nassau", 
                                                                    "Eastern Daylight Time"
                                                                }, 
                                                                {
                                                                    "America/New_York", 
                                                                    "Eastern Daylight Time"
                                                                }, 
                                                                {
                                                                    "America/Nipigon", 
                                                                    "Eastern Daylight Time"
                                                                }, 
                                                                {
                                                                    "America/Nome", 
                                                                    "Alaskan Daylight Time"
                                                                }, 
                                                                {
                                                                    "America/Noronha", 
                                                                    "Mid-Atlantic Daylight Time"
                                                                }, 
                                                                {
                                                                    "America/North_Dakota/Center", 
                                                                    "Central Daylight Time"
                                                                }, 
                                                                {
                                                                    "America/North_Dakota/New_Salem", 
                                                                    "Central Daylight Time"
                                                                }, 
                                                                {
                                                                    "America/Panama", 
                                                                    "Eastern Daylight Time"
                                                                }, 
                                                                {
                                                                    "America/Pangnirtung", 
                                                                    "Eastern Daylight Time"
                                                                }, 
                                                                {
                                                                    "America/Paramaribo", 
                                                                    "SA Eastern Daylight Time"
                                                                }, 
                                                                {
                                                                    "America/Phoenix", 
                                                                    "US Mountain Daylight Time"
                                                                }, 
                                                                {
                                                                    "America/Port_of_Spain", 
                                                                    "Atlantic Daylight Time"
                                                                }, 
                                                                {
                                                                    "America/Port-au-Prince", 
                                                                    "Eastern Daylight Time"
                                                                }, 
                                                                {
                                                                    "America/Porto_Velho", 
                                                                    "Central Brazilian Daylight Time"
                                                                }, 
                                                                {
                                                                    "America/Puerto_Rico", 
                                                                    "Atlantic Daylight Time"
                                                                }, 
                                                                {
                                                                    "America/Rainy_River", 
                                                                    "Central Daylight Time"
                                                                }, 
                                                                {
                                                                    "America/Rankin_Inlet", 
                                                                    "Central Daylight Time"
                                                                }, 
                                                                {
                                                                    "America/Recife", 
                                                                    "E. South America Daylight Time"
                                                                }, 
                                                                {
                                                                    "America/Regina", 
                                                                    "Central Daylight Time"
                                                                }, 
                                                                {
                                                                    "America/Resolute", 
                                                                    "Eastern Daylight Time"
                                                                }, 
                                                                {
                                                                    "America/Rio_Branco", 
                                                                    "SA Pacific Standard Time"
                                                                }, 
                                                                {
                                                                    "America/Santiago", 
                                                                    "SA Western Daylight Time"
                                                                }, 
                                                                {
                                                                    "America/Santo_Domingo", 
                                                                    "Atlantic Daylight Time"
                                                                }, 
                                                                {
                                                                    "America/Sao_Paulo", 
                                                                    "E. South America Daylight Time"
                                                                }, 
                                                                {
                                                                    "America/Scoresbysund", 
                                                                    "Azores Daylight Time"
                                                                }, 
                                                                {
                                                                    "America/St_Johns", 
                                                                    "Newfoundland Daylight Time"
                                                                }, 
                                                                {
                                                                    "America/St_Kitts", 
                                                                    "Atlantic Daylight Time"
                                                                }, 
                                                                {
                                                                    "America/St_Lucia", 
                                                                    "Atlantic Daylight Time"
                                                                }, 
                                                                {
                                                                    "America/St_Thomas", 
                                                                    "Atlantic Daylight Time"
                                                                }, 
                                                                {
                                                                    "America/St_Vincent", 
                                                                    "Atlantic Daylight Time"
                                                                }, 
                                                                {
                                                                    "America/Swift_Current", 
                                                                    "Central Daylight Time (Mexico)"
                                                                }, 
                                                                {
                                                                    "America/Tegucigalpa", 
                                                                    "Central Daylight Time (Mexico)"
                                                                }, 
                                                                {
                                                                    "America/Thule", 
                                                                    "Atlantic Daylight Time"
                                                                }, 
                                                                {
                                                                    "America/Thunder_Bay", 
                                                                    "Eastern Daylight Time"
                                                                }, 
                                                                {
                                                                    "America/Tijuana", 
                                                                    "Pacific Daylight Time (Mexico)"
                                                                }, 
                                                                {
                                                                    "America/Toronto", 
                                                                    "Eastern Daylight Time"
                                                                }, 
                                                                {
                                                                    "America/Tortola", 
                                                                    "Atlantic Daylight Time"
                                                                }, 
                                                                {
                                                                    "America/Vancouver", 
                                                                    "Pacific Daylight Time"
                                                                }, 
                                                                {
                                                                    "America/Whitehorse", 
                                                                    "Pacific Daylight Time"
                                                                }, 
                                                                {
                                                                    "America/Winnipeg", 
                                                                    "Central Daylight Time"
                                                                }, 
                                                                {
                                                                    "America/Yakutat", 
                                                                    "Alaskan Daylight Time"
                                                                }, 
                                                                {
                                                                    "America/Yellowknife", 
                                                                    "Mountain Daylight Time"
                                                                }, 
                                                                {
                                                                    "Antarctica/Casey", 
                                                                    "W. Australia Daylight Time"
                                                                }, 
                                                                {
                                                                    "Antarctica/Davis", 
                                                                    "SE Asia Daylight Time"
                                                                }, 
                                                                {
                                                                    "Antarctica/DumontDUrville", 
                                                                    "E. Australia Daylight Time"
                                                                }, 
                                                                {
                                                                    "Antarctica/Mawson", 
                                                                    "Central Asia Daylight Time"
                                                                }, 
                                                                {
                                                                    "Antarctica/McMurdo", 
                                                                    "New Zealand Daylight Time"
                                                                }, 
                                                                {
                                                                    "Antarctica/Palmer", 
                                                                    "SA Western Daylight Time"
                                                                }, 
                                                                {
                                                                    "Antarctica/Rothera", 
                                                                    "SA Eastern Daylight Time"
                                                                }, 
                                                                {
                                                                    "Antarctica/Syowa", 
                                                                    "E. Africa Daylight Time"
                                                                }, 
                                                                {
                                                                    "Antarctica/Vostok", 
                                                                    "Central Asia Daylight Time"
                                                                }, 
                                                                {
                                                                    "Asia/Aden", 
                                                                    "Arabic Daylight Time"
                                                                }, 
                                                                {
                                                                    "Asia/Almaty", 
                                                                    "Central Asia Daylight Time"
                                                                }, 
                                                                {
                                                                    "Asia/Amman", 
                                                                    "Jordan Daylight Time"
                                                                }, 
                                                                {
                                                                    "Asia/Anadyr", 
                                                                    "New Zealand Daylight Time"
                                                                }, 
                                                                {
                                                                    "Asia/Aqtau", 
                                                                    "West Asia Daylight Time"
                                                                }, 
                                                                {
                                                                    "Asia/Aqtobe", 
                                                                    "West Asia Daylight Time"
                                                                }, 
                                                                {
                                                                    "Asia/Ashgabat", 
                                                                    "West Asia Daylight Time"
                                                                }, 
                                                                {
                                                                    "Asia/Baghdad", 
                                                                    "Arabic Daylight Time"
                                                                }, 
                                                                {
                                                                    "Asia/Bahrain", 
                                                                    "Arabic Daylight Time"
                                                                }, 
                                                                {
                                                                    "Asia/Baku", 
                                                                    "Azerbaijan Daylight Time"
                                                                }, 
                                                                {
                                                                    "Asia/Bangkok", 
                                                                    "SE Asia Daylight Time"
                                                                }, 
                                                                {
                                                                    "Asia/Beirut", 
                                                                    "Middle East Daylight Time"
                                                                }, 
                                                                {
                                                                    "Asia/Bishkek", 
                                                                    "Central Asia Daylight Time"
                                                                }, 
                                                                {
                                                                    "Asia/Brunei", 
                                                                    "North Asia East Daylight Time"
                                                                }, 
                                                                {
                                                                    "Asia/Choibalsan", 
                                                                    "Yakutsk Daylight Time"
                                                                }, 
                                                                {
                                                                    "Asia/Chongqing", 
                                                                    "China Daylight Time"
                                                                }, 
                                                                {
                                                                    "Asia/Colombo", 
                                                                    "Sri Lanka Daylight Time"
                                                                }, 
                                                                {
                                                                    "Asia/Damascus", 
                                                                    "Middle East Daylight Time"
                                                                }, 
                                                                {
                                                                    "Asia/Dhaka", 
                                                                    "N. Central Asia Daylight Time"
                                                                }, 
                                                                {
                                                                    "Asia/Dili", 
                                                                    "Korea Daylight Time"
                                                                }, 
                                                                {
                                                                    "Asia/Dubai", 
                                                                    "Arabian Daylight Time"
                                                                }, 
                                                                {
                                                                    "Asia/Dushanbe", 
                                                                    "West Asia Daylight Time"
                                                                }, 
                                                                {
                                                                    "Asia/Gaza", 
                                                                    "Middle East Daylight Time"
                                                                }, 
                                                                {
                                                                    "Asia/Harbin", 
                                                                    "China Daylight Time"
                                                                }, 
                                                                {
                                                                    "Asia/Ho_Chi_Minh", 
                                                                    "SE Asia Daylight Time"
                                                                }, 
                                                                {
                                                                    "Asia/Hong_Kong", 
                                                                    "Malay Peninsula Daylight Time"
                                                                }, 
                                                                {
                                                                    "Asia/Hovd", 
                                                                    "SE Asia Daylight Time"
                                                                }, 
                                                                {
                                                                    "Asia/Irkutsk", 
                                                                    "North Asia East Daylight Time"
                                                                }, 
                                                                {
                                                                    "Asia/Jakarta", 
                                                                    "SE Asia Daylight Time"
                                                                }, 
                                                                {
                                                                    "Asia/Jayapura", 
                                                                    "Korea Daylight Time"
                                                                }, 
                                                                {
                                                                    "Asia/Jerusalem", 
                                                                    "Middle East Daylight Time"
                                                                }, 
                                                                {
                                                                    "Asia/Kabul", 
                                                                    "Afghanistan Daylight Time"
                                                                }, 
                                                                {
                                                                    "Asia/Kamchatka", 
                                                                    "Fiji Daylight Time"
                                                                }, 
                                                                {
                                                                    "Asia/Karachi", 
                                                                    "West Asia Daylight Time"
                                                                }, 
                                                                {
                                                                    "Asia/Kashgar", 
                                                                    "China Daylight Time"
                                                                }, 
                                                                {
                                                                    "Asia/Katmandu", 
                                                                    "Nepal Daylight Time"
                                                                }, 
                                                                {
                                                                    "Asia/Kolkata", 
                                                                    "India Daylight Time"
                                                                }, 
                                                                {
                                                                    "Asia/Krasnoyarsk", 
                                                                    "North Asia Daylight Time"
                                                                }, 
                                                                {
                                                                    "Asia/Kuala_Lumpur", 
                                                                    "Malay Peninsula Daylight Time"
                                                                }, 
                                                                {
                                                                    "Asia/Kuching", 
                                                                    "Malay Peninsula Daylight Time"
                                                                }, 
                                                                {
                                                                    "Asia/Kuwait", 
                                                                    "Arabic Daylight Time"
                                                                }, 
                                                                {
                                                                    "Asia/Macau", 
                                                                    "China Daylight Time"
                                                                }, 
                                                                {
                                                                    "Asia/Magadan", 
                                                                    "Central Pacific Daylight Time"
                                                                }, 
                                                                {
                                                                    "Asia/Makassar", 
                                                                    "Malay Peninsula Daylight Time"
                                                                }, 
                                                                {
                                                                    "Asia/Manila", 
                                                                    "Malay Peninsula Daylight Time"
                                                                }, 
                                                                {
                                                                    "Asia/Muscat", 
                                                                    "Arabian Daylight Time"
                                                                }, 
                                                                {
                                                                    "Asia/Nicosia", 
                                                                    "Middle East Daylight Time"
                                                                }, 
                                                                {
                                                                    "Asia/Novosibirsk", 
                                                                    "N. Central Asia Daylight Time"
                                                                }, 
                                                                {
                                                                    "Asia/Omsk", 
                                                                    "N. Central Asia Daylight Time"
                                                                }, 
                                                                {
                                                                    "Asia/Oral", 
                                                                    "Ekaterinburg Daylight Time"
                                                                }, 
                                                                {
                                                                    "Asia/Phnom_Penh", 
                                                                    "SE Asia Daylight Time"
                                                                }, 
                                                                {
                                                                    "Asia/Pontianak", 
                                                                    "SE Asia Daylight Time"
                                                                }, 
                                                                {
                                                                    "Asia/Pyongyang", 
                                                                    "Korea Daylight Time"
                                                                }, 
                                                                {
                                                                    "Asia/Qatar", 
                                                                    "Arabic Daylight Time"
                                                                }, 
                                                                {
                                                                    "Asia/Qyzylorda", 
                                                                    "N. Central Asia Daylight Time"
                                                                }, 
                                                                {
                                                                    "Asia/Rangoon", 
                                                                    "Myanmar Daylight Time"
                                                                }, 
                                                                {
                                                                    "Asia/Riyadh", 
                                                                    "Arabic Daylight Time"
                                                                }, 
                                                                {
                                                                    "Asia/Sakhalin", 
                                                                    "Vladivostok Daylight Time"
                                                                }, 
                                                                {
                                                                    "Asia/Samarkand", 
                                                                    "West Asia Daylight Time"
                                                                }, 
                                                                {
                                                                    "Asia/Seoul", 
                                                                    "Korea Daylight Time"
                                                                }, 
                                                                {
                                                                    "Asia/Shanghai", 
                                                                    "China Daylight Time"
                                                                }, 
                                                                {
                                                                    "Asia/Singapore", 
                                                                    "Malay Peninsula Daylight Time"
                                                                }, 
                                                                {
                                                                    "Asia/Taipei", 
                                                                    "China Daylight Time"
                                                                }, 
                                                                {
                                                                    "Asia/Tashkent", 
                                                                    "West Asia Daylight Time"
                                                                }, 
                                                                {
                                                                    "Asia/Tbilisi", 
                                                                    "Arabian Daylight Time"
                                                                }, 
                                                                {
                                                                    "Asia/Tehran", 
                                                                    "Iran Daylight Time"
                                                                }, 
                                                                {
                                                                    "Asia/Thimphu", 
                                                                    "N. Central Asia Daylight Time"
                                                                }, 
                                                                {
                                                                    "Asia/Tokyo", 
                                                                    "Tokyo Daylight Time"
                                                                }, 
                                                                {
                                                                    "Asia/Ulaanbaatar", 
                                                                    "North Asia East Daylight Time"
                                                                }, 
                                                                {
                                                                    "Asia/Urumqi", 
                                                                    "China Daylight Time"
                                                                }, 
                                                                {
                                                                    "Asia/Vientiane", 
                                                                    "SE Asia Daylight Time"
                                                                }, 
                                                                {
                                                                    "Asia/Vladivostok", 
                                                                    "Vladivostok Daylight Time"
                                                                }, 
                                                                {
                                                                    "Asia/Yakutsk", 
                                                                    "Yakutsk Daylight Time"
                                                                }, 
                                                                {
                                                                    "Asia/Yekaterinburg", 
                                                                    "Ekaterinburg Daylight Time"
                                                                }, 
                                                                {
                                                                    "Asia/Yerevan", 
                                                                    "Caucasus Daylight Time"
                                                                }, 
                                                                {
                                                                    "Atlantic/Azores", 
                                                                    "Azores Daylight Time"
                                                                }, 
                                                                {
                                                                    "Atlantic/Bermuda", 
                                                                    "Atlantic Daylight Time"
                                                                }, 
                                                                {
                                                                    "Atlantic/Canary", 
                                                                    "W. Europe Daylight Time"
                                                                }, 
                                                                {
                                                                    "Atlantic/Cape_Verde", 
                                                                    "Cape Verde Daylight Time"
                                                                }, 
                                                                {
                                                                    "Atlantic/Faroe", 
                                                                    "W. Europe Daylight Time"
                                                                }, 
                                                                {
                                                                    "Atlantic/Madeira", 
                                                                    "W. Europe Daylight Time"
                                                                }, 
                                                                {
                                                                    "Atlantic/Reykjavik", 
                                                                    "Greenwich Daylight Time"
                                                                }, 
                                                                {
                                                                    "Atlantic/South_Georgia", 
                                                                    "Mid-Atlantic Daylight Time"
                                                                }, 
                                                                {
                                                                    "Atlantic/St_Helena", 
                                                                    "Greenwich Daylight Time"
                                                                }, 
                                                                {
                                                                    "Atlantic/Stanley", 
                                                                    "Pacific SA Daylight Time"
                                                                }, 
                                                                {
                                                                    "Australia/Adelaide", 
                                                                    "Cen. Australia Daylight Time"
                                                                }, 
                                                                {
                                                                    "Australia/Brisbane", 
                                                                    "E. Australia Daylight Time"
                                                                }, 
                                                                {
                                                                    "Australia/Broken_Hill", 
                                                                    "Cen. Australia Daylight Time"
                                                                }, 
                                                                {
                                                                    "Australia/Currie", 
                                                                    "Tasmania Daylight Time"
                                                                }, 
                                                                {
                                                                    "Australia/Darwin", 
                                                                    "AUS Central Daylight Time"
                                                                }, 
                                                                {
                                                                    "Australia/Eucla", 
                                                                    "Cen. Australia Daylight Time"
                                                                }, 
                                                                {
                                                                    "Australia/Hobart", 
                                                                    "Tasmania Daylight Time"
                                                                }, 
                                                                {
                                                                    "Australia/Lindeman", 
                                                                    "AUS Eastern Daylight Time"
                                                                }, 
                                                                {
                                                                    "Australia/Lord_Howe", 
                                                                    "Central Pacific Daylight Time"
                                                                }, 
                                                                {
                                                                    "Australia/Melbourne", 
                                                                    "AUS Eastern Daylight Time"
                                                                }, 
                                                                {
                                                                    "Australia/Perth", 
                                                                    "W. Australia Daylight Time"
                                                                }, 
                                                                {
                                                                    "Australia/Sydney", 
                                                                    "AUS Eastern Daylight Time"
                                                                }, 
                                                                {
                                                                    "CET", 
                                                                    "Central Europe Daylight Time"
                                                                }, 
                                                                {
                                                                    "CST6CDT", 
                                                                    "Central Daylight Time"
                                                                }, 
                                                                {
                                                                    "EET", 
                                                                    "E. Europe Daylight Time"
                                                                }, 
                                                                {
                                                                    "EST", 
                                                                    "Eastern Daylight Time"
                                                                }, 
                                                                {
                                                                    "EST5EDT", 
                                                                    "Eastern Daylight Time"
                                                                }, 
                                                                {
                                                                    "Etc/GMT", 
                                                                    "GMT Daylight Time"
                                                                }, 
                                                                {
                                                                    "Etc/UCT", 
                                                                    "GMT Daylight Time"
                                                                }, 
                                                                {
                                                                    "Etc/UTC", 
                                                                    "GMT Daylight Time"
                                                                }, 
                                                                {
                                                                    "Europe/Amsterdam", 
                                                                    "W. Europe Daylight Time"
                                                                }, 
                                                                {
                                                                    "Europe/Andorra", 
                                                                    "Central Europe Daylight Time"
                                                                }, 
                                                                {
                                                                    "Europe/Athens", 
                                                                    "E. Europe Daylight Time"
                                                                }, 
                                                                {
                                                                    "Europe/Belgrade", 
                                                                    "Central Europe Daylight Time"
                                                                }, 
                                                                {
                                                                    "Europe/Berlin", 
                                                                    "W. Europe Daylight Time"
                                                                }, 
                                                                {
                                                                    "Europe/Brussels", 
                                                                    "Central European Daylight Time"
                                                                }, 
                                                                {
                                                                    "Europe/Bucharest", 
                                                                    "E. Europe Daylight Time"
                                                                }, 
                                                                {
                                                                    "Europe/Budapest", 
                                                                    "Central European Daylight Time"
                                                                }, 
                                                                {
                                                                    "Europe/Chisinau", 
                                                                    "E. Europe Daylight Time"
                                                                }, 
                                                                {
                                                                    "Europe/Copenhagen", 
                                                                    "Central European Daylight Time"
                                                                }, 
                                                                {
                                                                    "Europe/Dublin", 
                                                                    "GMT Daylight Time"
                                                                }, 
                                                                {
                                                                    "Europe/Gibraltar", 
                                                                    "Central European Daylight Time"
                                                                }, 
                                                                {
                                                                    "Europe/Helsinki", 
                                                                    "E. Europe Daylight Time"
                                                                }, 
                                                                {
                                                                    "Europe/Istanbul", 
                                                                    "E. Europe Daylight Time"
                                                                }, 
                                                                {
                                                                    "Europe/Kaliningrad", 
                                                                    "E. Europe Daylight Time"
                                                                }, 
                                                                {
                                                                    "Europe/Kiev", 
                                                                    "E. Europe Daylight Time"
                                                                }, 
                                                                {
                                                                    "Europe/Lisbon", 
                                                                    "W. Europe Daylight Time"
                                                                }, 
                                                                {
                                                                    "Europe/London", 
                                                                    "GMT Daylight Time"
                                                                }, 
                                                                {
                                                                    "Europe/Luxembourg", 
                                                                    "Central European Daylight Time"
                                                                }, 
                                                                {
                                                                    "Europe/Madrid", 
                                                                    "Central European Daylight Time"
                                                                }, 
                                                                {
                                                                    "Europe/Malta", 
                                                                    "Central European Daylight Time"
                                                                }, 
                                                                {
                                                                    "Europe/Minsk", 
                                                                    "E. Europe Daylight Time"
                                                                }, 
                                                                {
                                                                    "Europe/Monaco", 
                                                                    "Central European Daylight Time"
                                                                }, 
                                                                {
                                                                    "Europe/Moscow", 
                                                                    "Russian Daylight Time"
                                                                }, 
                                                                {
                                                                    "Europe/Oslo", 
                                                                    "Central European Daylight Time"
                                                                }, 
                                                                {
                                                                    "Europe/Paris", 
                                                                    "Central European Daylight Time"
                                                                }, 
                                                                {
                                                                    "Europe/Prague", 
                                                                    "Central European Daylight Time"
                                                                }, 
                                                                {
                                                                    "Europe/Riga", 
                                                                    "E. Europe Daylight Time"
                                                                }, 
                                                                {
                                                                    "Europe/Rome", 
                                                                    "W. Europe Daylight Time"
                                                                }, 
                                                                {
                                                                    "Europe/Samara", 
                                                                    "Caucasus Daylight Time"
                                                                }, 
                                                                {
                                                                    "Europe/Simferopol", 
                                                                    "E. Europe Daylight Time"
                                                                }, 
                                                                {
                                                                    "Europe/Sofia", 
                                                                    "E. Europe Daylight Time"
                                                                }, 
                                                                {
                                                                    "Europe/Stockholm", 
                                                                    "W. Europe Daylight Time"
                                                                }, 
                                                                {
                                                                    "Europe/Tallinn", 
                                                                    "E. Europe Daylight Time"
                                                                }, 
                                                                {
                                                                    "Europe/Tirane", 
                                                                    "Central European Daylight Time"
                                                                }, 
                                                                {
                                                                    "Europe/Uzhgorod", 
                                                                    "E. Europe Daylight Time"
                                                                }, 
                                                                {
                                                                    "Europe/Vaduz", 
                                                                    "W. Europe Daylight Time"
                                                                }, 
                                                                {
                                                                    "Europe/Vienna", 
                                                                    "Central European Daylight Time"
                                                                }, 
                                                                {
                                                                    "Europe/Vilnius", 
                                                                    "E. Europe Daylight Time"
                                                                }, 
                                                                {
                                                                    "Europe/Volgograd", 
                                                                    "Russian Standard Time"
                                                                }, 
                                                                {
                                                                    "Europe/Warsaw", 
                                                                    "Central European Daylight Time"
                                                                }, 
                                                                {
                                                                    "Europe/Zaporozhye", 
                                                                    "E. Europe Daylight Time"
                                                                }, 
                                                                {
                                                                    "Europe/Zurich", 
                                                                    "Central European Daylight Time"
                                                                }, 
                                                                {
                                                                    "HST", 
                                                                    "Hawaiian Daylight Time"
                                                                }, 
                                                                {
                                                                    "Indian/Antananarivo", 
                                                                    "E. Africa Daylight Time"
                                                                }, 
                                                                {
                                                                    "Indian/Chagos", 
                                                                    "Central Asia Daylight Time"
                                                                }, 
                                                                {
                                                                    "Indian/Christmas", 
                                                                    "SE Asia Daylight Time"
                                                                }, 
                                                                {
                                                                    "Indian/Cocos", 
                                                                    "Myanmar Daylight Time"
                                                                }, 
                                                                {
                                                                    "Indian/Comoro", 
                                                                    "E. Africa Daylight Time"
                                                                }, 
                                                                {
                                                                    "Indian/Kerguelen", 
                                                                    "West Asia Daylight Time"
                                                                }, 
                                                                {
                                                                    "Indian/Mahe", 
                                                                    "Arabian Daylight Time"
                                                                }, 
                                                                {
                                                                    "Indian/Maldives", 
                                                                    "West Asia Daylight Time"
                                                                }, 
                                                                {
                                                                    "Indian/Mauritius", 
                                                                    "Azerbaijan Daylight Time"
                                                                }, 
                                                                {
                                                                    "Indian/Mayotte", 
                                                                    "E. Africa Daylight Time"
                                                                }, 
                                                                {
                                                                    "Indian/Reunion", 
                                                                    "Arabian Daylight Time"
                                                                }, 
                                                                {
                                                                    "MET", 
                                                                    "Central European Daylight Time"
                                                                }, 
                                                                {
                                                                    "MST", 
                                                                    "Mountain Daylight Time"
                                                                }, 
                                                                {
                                                                    "MST7MDT", 
                                                                    "US Mountain Daylight Time"
                                                                }, 
                                                                {
                                                                    "Pacific/Apia", 
                                                                    "Samoa Daylight Time"
                                                                }, 
                                                                {
                                                                    "Pacific/Auckland", 
                                                                    "New Zealand Daylight Time"
                                                                }, 
                                                                {
                                                                    "Pacific/Chatham", 
                                                                    "Fiji Daylight Time"
                                                                }, 
                                                                {
                                                                    "Pacific/Easter", 
                                                                    "Central Daylight Time (Mexico)"
                                                                }, 
                                                                {
                                                                    "Pacific/Efate", 
                                                                    "Central Pacific Daylight Time"
                                                                }, 
                                                                {
                                                                    "Pacific/Enderbury", 
                                                                    "Tonga Daylight Time"
                                                                }, 
                                                                {
                                                                    "Pacific/Fakaofo", 
                                                                    "Hawaiian Daylight Time"
                                                                }, 
                                                                {
                                                                    "Pacific/Fiji", 
                                                                    "Fiji Daylight Time"
                                                                }, 
                                                                {
                                                                    "Pacific/Funafuti", 
                                                                    "Fiji Daylight Time"
                                                                }, 
                                                                {
                                                                    "Pacific/Galapagos", 
                                                                    "Central Daylight Time"
                                                                }, 
                                                                {
                                                                    "Pacific/Gambier", 
                                                                    "Alaskan Daylight Time"
                                                                }, 
                                                                {
                                                                    "Pacific/Guadalcanal", 
                                                                    "Central Pacific Daylight Time"
                                                                }, 
                                                                {
                                                                    "Pacific/Guam", 
                                                                    "West Pacific Daylight Time"
                                                                }, 
                                                                {
                                                                    "Pacific/Honolulu", 
                                                                    "Hawaiian Daylight Time"
                                                                }, 
                                                                {
                                                                    "Pacific/Johnston", 
                                                                    "Hawaiian Daylight Time"
                                                                }, 
                                                                {
                                                                    "Pacific/Kiritimati", 
                                                                    "Tonga Daylight Time"
                                                                }, 
                                                                {
                                                                    "Pacific/Kosrae", 
                                                                    "Central Pacific Daylight Time"
                                                                }, 
                                                                {
                                                                    "Pacific/Kwajalein", 
                                                                    "Fiji Daylight Time"
                                                                }, 
                                                                {
                                                                    "Pacific/Majuro", 
                                                                    "Fiji Daylight Time"
                                                                }, 
                                                                {
                                                                    "Pacific/Marquesas", 
                                                                    "Pacific Daylight Time (Mexico)"
                                                                }, 
                                                                {
                                                                    "Pacific/Midway", 
                                                                    "Samoa Daylight Time"
                                                                }, 
                                                                {
                                                                    "Pacific/Nauru", 
                                                                    "Fiji Daylight Time"
                                                                }, 
                                                                {
                                                                    "Pacific/Niue", 
                                                                    "Samoa Daylight Time"
                                                                }, 
                                                                {
                                                                    "Pacific/Norfolk", 
                                                                    "Central Pacific Daylight Time"
                                                                }, 
                                                                {
                                                                    "Pacific/Noumea", 
                                                                    "Central Pacific Daylight Time"
                                                                }, 
                                                                {
                                                                    "Pacific/Pago_Pago", 
                                                                    "Samoa Daylight Time"
                                                                }, 
                                                                {
                                                                    "Pacific/Palau", 
                                                                    "Tokyo Daylight Time"
                                                                }, 
                                                                {
                                                                    "Pacific/Pitcairn", 
                                                                    "Pacific Daylight Time"
                                                                }, 
                                                                {
                                                                    "Pacific/Ponape", 
                                                                    "Central Pacific Daylight Time"
                                                                }, 
                                                                {
                                                                    "Pacific/Port_Moresby", 
                                                                    "West Pacific Daylight Time"
                                                                }, 
                                                                {
                                                                    "Pacific/Rarotonga", 
                                                                    "Hawaiian Daylight Time"
                                                                }, 
                                                                {
                                                                    "Pacific/Saipan", 
                                                                    "West Pacific Daylight Time"
                                                                }, 
                                                                {
                                                                    "Pacific/Tahiti", 
                                                                    "Hawaiian Daylight Time"
                                                                }, 
                                                                {
                                                                    "Pacific/Tarawa", 
                                                                    "Fiji Daylight Time"
                                                                }, 
                                                                {
                                                                    "Pacific/Tongatapu", 
                                                                    "Tonga Daylight Time"
                                                                }, 
                                                                {
                                                                    "Pacific/Truk", 
                                                                    "West Pacific Daylight Time"
                                                                }, 
                                                                {
                                                                    "Pacific/Wake", 
                                                                    "Fiji Daylight Time"
                                                                }, 
                                                                {
                                                                    "Pacific/Wallis", 
                                                                    "Fiji Daylight Time"
                                                                }, 
                                                                {
                                                                    "PST8PDT", 
                                                                    "Pacific Daylight Time"
                                                                }, 
                                                                {
                                                                    "WET", 
                                                                    "W. Europe Daylight Time"
                                                                }, 
                                                                {
                                                                    "Etc/GMT+9", 
                                                                    "Alaskan Daylight Time"
                                                                }, 
                                                                {
                                                                    "Asia/Riyadh87", 
                                                                    "Arabic Daylight Time"
                                                                }, 
                                                                {
                                                                    "Asia/Riyadh88", 
                                                                    "Arabic Daylight Time"
                                                                }, 
                                                                {
                                                                    "Asia/Riyadh89", 
                                                                    "Arabic Daylight Time"
                                                                }, 
                                                                {
                                                                    "Etc/GMT+4", 
                                                                    "Atlantic Daylight Time"
                                                                }, 
                                                                {
                                                                    "Etc/GMT-10", 
                                                                    "AUS Eastern Daylight Time"
                                                                }, 
                                                                {
                                                                    "Etc/GMT-4", 
                                                                    "Azerbaijan Daylight Time"
                                                                }, 
                                                                {
                                                                    "Etc/GMT+1", 
                                                                    "Azores Daylight Time"
                                                                }, 
                                                                {
                                                                    "Etc/GMT-1", 
                                                                    "Central Europe Daylight Time"
                                                                }, 
                                                                {
                                                                    "Etc/GMT-11", 
                                                                    "Central Pacific Daylight Time"
                                                                }, 
                                                                {
                                                                    "Etc/GMT+6", 
                                                                    "Central Daylight Time"
                                                                }, 
                                                                {
                                                                    "Etc/GMT+12", 
                                                                    "Dateline Standard Time"
                                                                }, 
                                                                {
                                                                    "Etc/GMT-2", 
                                                                    "E. Europe Daylight Time"
                                                                }, 
                                                                {
                                                                    "Etc/GMT+3", 
                                                                    "E. South America Daylight Time"
                                                                }, 
                                                                {
                                                                    "Etc/GMT+5", 
                                                                    "Eastern Daylight Time"
                                                                }, 
                                                                {
                                                                    "Etc/GMT-5", 
                                                                    "Ekaterinburg Daylight Time"
                                                                }, 
                                                                {
                                                                    "Etc/GMT+10", 
                                                                    "Hawaiian Daylight Time"
                                                                }, 
                                                                {
                                                                    "Etc/GMT+2", 
                                                                    "Mid-Atlantic Daylight Time"
                                                                }, 
                                                                {
                                                                    "Etc/GMT+7", 
                                                                    "Mountain Daylight Time"
                                                                }, 
                                                                {
                                                                    "Etc/GMT-6", 
                                                                    "N. Central Asia Daylight Time"
                                                                }, 
                                                                {
                                                                    "Etc/GMT-12", 
                                                                    "New Zealand Daylight Time"
                                                                }, 
                                                                {
                                                                    "Etc/GMT-7", 
                                                                    "North Asia Daylight Time"
                                                                }, 
                                                                {
                                                                    "Etc/GMT+8", 
                                                                    "Pacific Daylight Time"
                                                                }, 
                                                                {
                                                                    "Etc/GMT-3", 
                                                                    "Russian Daylight Time"
                                                                }, 
                                                                {
                                                                    "Etc/GMT+11", 
                                                                    "Samoa Daylight Time"
                                                                }, 
                                                                {
                                                                    "Etc/GMT-13", 
                                                                    "Tonga Daylight Time"
                                                                }, 
                                                                {
                                                                    "Etc/GMT-14", 
                                                                    "Tonga Daylight Time"
                                                                }, 
                                                                {
                                                                    "Etc/GMT-8", 
                                                                    "W. Australia Daylight Time"
                                                                }, 
                                                                {
                                                                    "Etc/GMT-9", 
                                                                    "Yakutsk Daylight Time"
                                                                }
                                                            };

            // DAE 2013-03-28 - Fix 
            dayligthNames.Remove("Asia/Hong_Kong");
            dayligthNames.Remove("Asia/Kuala_Lumpur");
            dayligthNames.Remove("Asia/Kuching");
            dayligthNames.Remove("Asia/Makassar");
            dayligthNames.Remove("Asia/Manila");
            dayligthNames.Remove("Asia/Singapore");

            // Merge de los diccionarios.
            IDictionary<string, string> dictXml = MapFromXml;
            foreach (KeyValuePair<string, string> item in dictXml)
            {
                dayligthNames[item.Key] = item.Value;
            }

            return dayligthNames;
        }

        /// <summary>
        /// Lee los datos del archivo xml windowsZones.xml
        /// Crea un diccionario de busqueda por TZ Olson devolviendo la zona equivalente
        /// de Windows.
        /// Descargado de http://unicode.org/repos/cldr-tmp/trunk/diff/supplemental/zone_tzid.html
        /// </summary>
        /// <returns>Mapeo de TZ Olson to Windows</returns>
        private static IDictionary<string, string> LoadFromXml()
        {
            char[] sep = { ' ' };
            IDictionary<string, string> ret = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            XmlDocument xdoc = new XmlDocument();

            // Lee y valida la configuración.
            string fileXml = TimezoneConverterConfiguration.File;
            if (string.IsNullOrWhiteSpace(fileXml))
            {
                throw new ConfigurationErrorsException();
            }

            // Carga el archivo Xml.
            xdoc.Load(fileXml);

            // Selecciona los nodos.
            XmlNodeList listMapZones = xdoc.SelectNodes("//mapZone");

            // Recorre los nodos.
            if (listMapZones != null)
            {
                foreach (XmlNode mapZone in listMapZones)
                {
                    // Agrega los elementos.
                    if (mapZone.Attributes != null)
                    {
                        string olson = mapZone.Attributes["type"].Value;
                        string[] listOlson = olson.Split(sep);
                        string winTimeZone = mapZone.Attributes["other"].Value;

                        foreach (string olsonTimeZone in listOlson)
                        {
                            if (!ret.ContainsKey(olsonTimeZone))
                            {
                                ret.Add(olsonTimeZone, winTimeZone);
                            }
                        }
                    }
                }
            }

            return ret;
        }

        /// <summary>The load standar names.</summary>
        /// <returns>Standar names map.</returns>
        private static IDictionary<string, string> GetStandarNames()
        {
            IDictionary<string, string> aux = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
                                                  {
                                                      {
                                                          "Africa/Abidjan", 
                                                          "Greenwich Standard Time"
                                                      }, 
                                                      {
                                                          "Africa/Accra", 
                                                          "Greenwich Standard Time"
                                                      }, 
                                                      {
                                                          "Africa/Addis_Ababa", 
                                                          "E. Africa Standard Time"
                                                      }, 
                                                      {
                                                          "Africa/Algiers", 
                                                          "Central Europe Standard Time"
                                                      }, 
                                                      {
                                                          "Africa/Asmara", 
                                                          "E. Africa Standard Time"
                                                      }, 
                                                      {
                                                          "Africa/Bamako", 
                                                          "Greenwich Standard Time"
                                                      }, 
                                                      {
                                                          "Africa/Bangui", 
                                                          "W. Central Africa Standard Time"
                                                      }, 
                                                      {
                                                          "Africa/Banjul", 
                                                          "Greenwich Standard Time"
                                                      }, 
                                                      {
                                                          "Africa/Bissau", 
                                                          "Greenwich Standard Time"
                                                      }, 
                                                      {
                                                          "Africa/Blantyre", 
                                                          "South Africa Standard Time"
                                                      }, 
                                                      {
                                                          "Africa/Brazzaville", 
                                                          "W. Central Africa Standard Time"
                                                      }, 
                                                      {
                                                          "Africa/Bujumbura", 
                                                          "South Africa Standard Time"
                                                      }, 
                                                      {
                                                          "Africa/Cairo", 
                                                          "Egypt Standard Time"
                                                      }, 
                                                      {
                                                          "Africa/Casablanca", 
                                                          "Greenwich Standard Time"
                                                      }, 
                                                      {
                                                          "Africa/Ceuta", 
                                                          "Central Europe Standard Time"
                                                      }, 
                                                      {
                                                          "Africa/Conakry", 
                                                          "Greenwich Standard Time"
                                                      }, 
                                                      {
                                                          "Africa/Dakar", 
                                                          "Greenwich Standard Time"
                                                      }, 
                                                      {
                                                          "Africa/Dar_es_Salaam", 
                                                          "E. Africa Standard Time"
                                                      }, 
                                                      {
                                                          "Africa/Djibouti", 
                                                          "E. Africa Standard Time"
                                                      }, 
                                                      {
                                                          "Africa/Douala", 
                                                          "W. Central Africa Standard Time"
                                                      }, 
                                                      {
                                                          "Africa/El_Aaiun", 
                                                          "Greenwich Standard Time"
                                                      }, 
                                                      {
                                                          "Africa/Freetown", 
                                                          "Greenwich Standard Time"
                                                      }, 
                                                      {
                                                          "Africa/Gaborone", 
                                                          "South Africa Standard Time"
                                                      }, 
                                                      {
                                                          "Africa/Harare", 
                                                          "South Africa Standard Time"
                                                      }, 
                                                      {
                                                          "Africa/Johannesburg", 
                                                          "South Africa Standard Time"
                                                      }, 
                                                      {
                                                          "Africa/Kampala", 
                                                          "E. Africa Standard Time"
                                                      }, 
                                                      {
                                                          "Africa/Khartoum", 
                                                          "E. Africa Standard Time"
                                                      }, 
                                                      {
                                                          "Africa/Kigali", 
                                                          "South Africa Standard Time"
                                                      }, 
                                                      {
                                                          "Africa/Kinshasa", 
                                                          "W. Central Africa Standard Time"
                                                      }, 
                                                      {
                                                          "Africa/Lagos", 
                                                          "W. Central Africa Standard Time"
                                                      }, 
                                                      {
                                                          "Africa/Libreville", 
                                                          "W. Central Africa Standard Time"
                                                      }, 
                                                      {
                                                          "Africa/Lome", 
                                                          "Greenwich Standard Time"
                                                      }, 
                                                      {
                                                          "Africa/Luanda", 
                                                          "W. Central Africa Standard Time"
                                                      }, 
                                                      {
                                                          "Africa/Lubumbashi", 
                                                          "South Africa Standard Time"
                                                      }, 
                                                      {
                                                          "Africa/Lusaka", 
                                                          "South Africa Standard Time"
                                                      }, 
                                                      {
                                                          "Africa/Malabo", 
                                                          "W. Central Africa Standard Time"
                                                      }, 
                                                      {
                                                          "Africa/Maputo", 
                                                          "South Africa Standard Time"
                                                      }, 
                                                      {
                                                          "Africa/Maseru", 
                                                          "South Africa Standard Time"
                                                      }, 
                                                      {
                                                          "Africa/Mbabane", 
                                                          "South Africa Standard Time"
                                                      }, 
                                                      {
                                                          "Africa/Mogadishu", 
                                                          "E. Africa Standard Time"
                                                      }, 
                                                      {
                                                          "Africa/Monrovia", 
                                                          "Greenwich Standard Time"
                                                      }, 
                                                      {
                                                          "Africa/Nairobi", 
                                                          "E. Africa Standard Time"
                                                      }, 
                                                      {
                                                          "Africa/Ndjamena", 
                                                          "W. Central Africa Standard Time"
                                                      }, 
                                                      {
                                                          "Africa/Niamey", 
                                                          "W. Central Africa Standard Time"
                                                      }, 
                                                      {
                                                          "Africa/Nouakchott", 
                                                          "Greenwich Standard Time"
                                                      }, 
                                                      {
                                                          "Africa/Ouagadougou", 
                                                          "Greenwich Standard Time"
                                                      }, 
                                                      {
                                                          "Africa/Porto-Novo", 
                                                          "W. Central Africa Standard Time"
                                                      }, 
                                                      {
                                                          "Africa/Sao_Tome", 
                                                          "Greenwich Standard Time"
                                                      }, 
                                                      {
                                                          "Africa/Tripoli", 
                                                          "South Africa Standard Time"
                                                      }, 
                                                      {
                                                          "Africa/Tunis", 
                                                          "Central Europe Standard Time"
                                                      }, 
                                                      {
                                                          "Africa/Windhoek", 
                                                          "W. Central Africa Standard Time"
                                                      }, 
                                                      {
                                                          "America/Adak", 
                                                          "Hawaiian Standard Time"
                                                      }, 
                                                      {
                                                          "America/Anchorage", 
                                                          "Alaskan Standard Time"
                                                      }, 
                                                      {
                                                          "America/Anguilla", 
                                                          "Atlantic Standard Time"
                                                      }, 
                                                      {
                                                          "America/Antigua", 
                                                          "Atlantic Standard Time"
                                                      }, 
                                                      {
                                                          "America/Araguaina", 
                                                          "E. South America Standard Time"
                                                      }, 
                                                      {
                                                          "America/Argentina/Buenos_Aires", 
                                                          "Argentina Standard Time"
                                                      }, 
                                                      {
                                                          "America/Argentina/Catamarca", 
                                                          "Argentina Standard Time"
                                                      }, 
                                                      {
                                                          "America/Argentina/Cordoba", 
                                                          "Argentina Standard Time"
                                                      }, 
                                                      {
                                                          "America/Argentina/Jujuy", 
                                                          "Argentina Standard Time"
                                                      }, 
                                                      {
                                                          "America/Argentina/La_Rioja", 
                                                          "Argentina Standard Time"
                                                      }, 
                                                      {
                                                          "America/Argentina/Mendoza", 
                                                          "Argentina Standard Time"
                                                      }, 
                                                      {
                                                          "America/Argentina/Rio_Gallegos", 
                                                          "Argentina Standard Time"
                                                      }, 
                                                      {
                                                          "America/Argentina/San_Juan", 
                                                          "Argentina Standard Time"
                                                      }, 
                                                      {
                                                          "America/Argentina/San_Luis", 
                                                          "Argentina Standard Time"
                                                      }, 
                                                      {
                                                          "America/Argentina/Tucuman", 
                                                          "Argentina Standard Time"
                                                      }, 
                                                      {
                                                          "America/Argentina/Ushuaia", 
                                                          "Argentina Standard Time"
                                                      }, 
                                                      {
                                                          "America/Aruba", 
                                                          "Atlantic Standard Time"
                                                      }, 
                                                      {
                                                          "America/Asuncion", 
                                                          "Central Brazilian Standard Time"
                                                      }, 
                                                      {
                                                          "America/Atikokan", 
                                                          "Eastern Standard Time"
                                                      }, 
                                                      {
                                                          "America/Bahia", 
                                                          "E. South America Standard Time"
                                                      }, 
                                                      {
                                                          "America/Barbados", 
                                                          "Atlantic Standard Time"
                                                      }, 
                                                      {
                                                          "America/Belem", 
                                                          "E. South America Standard Time"
                                                      }, 
                                                      {
                                                          "America/Belize", 
                                                          "Central Standard Time (Mexico)"
                                                      }, 
                                                      {
                                                          "America/Blanc-Sablon", 
                                                          "Atlantic Standard Time"
                                                      }, 
                                                      {
                                                          "America/Boa_Vista", 
                                                          "Central Brazilian Standard Time"
                                                      }, 
                                                      {
                                                          "America/Bogota", 
                                                          "SA Pacific Standard Time"
                                                      }, 
                                                      {
                                                          "America/Boise", 
                                                          "Mountain Standard Time"
                                                      }, 
                                                      {
                                                          "America/Cambridge_Bay", 
                                                          "Mountain Standard Time"
                                                      }, 
                                                      {
                                                          "America/Campo_Grande", 
                                                          "SA Western Standard Time"
                                                      }, 
                                                      {
                                                          "America/Cancun", 
                                                          "Central Standard Time (Mexico)"
                                                      }, 
                                                      {
                                                          "America/Caracas", 
                                                          "Newfoundland Standard Time"
                                                      }, 
                                                      {
                                                          "America/Cayenne", 
                                                          "SA Eastern Standard Time"
                                                      }, 
                                                      {
                                                          "America/Cayman", 
                                                          "Eastern Standard Time"
                                                      }, 
                                                      {
                                                          "America/Chicago", 
                                                          "Central Standard Time"
                                                      }, 
                                                      {
                                                          "America/Chihuahua", 
                                                          "Mountain Standard Time (Mexico)"
                                                      }, 
                                                      {
                                                          "America/Costa_Rica", 
                                                          "Central Standard Time (Mexico)"
                                                      }, 
                                                      {
                                                          "America/Cuiaba", 
                                                          "SA Western Standard Time"
                                                      }, 
                                                      {
                                                          "America/Curacao", 
                                                          "Atlantic Standard Time"
                                                      }, 
                                                      {
                                                          "America/Danmarkshavn", 
                                                          "Greenwich Standard Time"
                                                      }, 
                                                      {
                                                          "America/Dawson", 
                                                          "Pacific Standard Time"
                                                      }, 
                                                      {
                                                          "America/Dawson_Creek", 
                                                          "Mountain Standard Time"
                                                      }, 
                                                      {
                                                          "America/Denver", 
                                                          "Mountain Standard Time"
                                                      }, 
                                                      {
                                                          "America/Detroit", 
                                                          "Eastern Standard Time"
                                                      }, 
                                                      {
                                                          "America/Dominica", 
                                                          "Atlantic Standard Time"
                                                      }, 
                                                      {
                                                          "America/Edmonton", 
                                                          "Mountain Standard Time"
                                                      }, 
                                                      {
                                                          "America/Eirunepe", 
                                                          "SA Pacific Standard Time"
                                                      }, 
                                                      {
                                                          "America/El_Salvador", 
                                                          "Central Standard Time (Mexico)"
                                                      }, 
                                                      {
                                                          "America/Fortaleza", 
                                                          "E. South America Standard Time"
                                                      }, 
                                                      {
                                                          "America/Glace_Bay", 
                                                          "Atlantic Standard Time"
                                                      }, 
                                                      {
                                                          "America/Godthab", 
                                                          "Greenland Standard Time"
                                                      }, 
                                                      {
                                                          "America/Goose_Bay", 
                                                          "Atlantic Standard Time"
                                                      }, 
                                                      {
                                                          "America/Grand_Turk", 
                                                          "Eastern Standard Time"
                                                      }, 
                                                      {
                                                          "America/Grenada", 
                                                          "Atlantic Standard Time"
                                                      }, 
                                                      {
                                                          "America/Guadeloupe", 
                                                          "Atlantic Standard Time"
                                                      }, 
                                                      {
                                                          "America/Guatemala", 
                                                          "Central Standard Time (Mexico)"
                                                      }, 
                                                      {
                                                          "America/Guayaquil", 
                                                          "SA Pacific Standard Time"
                                                      }, 
                                                      {
                                                          "America/Guyana", 
                                                          "SA Western Standard Time"
                                                      }, 
                                                      {
                                                          "America/Halifax", 
                                                          "Atlantic Standard Time"
                                                      }, 
                                                      {
                                                          "America/Havana", 
                                                          "SA Pacific Standard Time"
                                                      }, 
                                                      {
                                                          "America/Hermosillo", 
                                                          "Mountain Standard Time"
                                                      }, 
                                                      {
                                                          "America/Indiana/Indianapolis", 
                                                          "Eastern Standard Time"
                                                      }, 
                                                      {
                                                          "America/Indiana/Knox", 
                                                          "Central Standard Time"
                                                      }, 
                                                      {
                                                          "America/Indiana/Marengo", 
                                                          "Eastern Standard Time"
                                                      }, 
                                                      {
                                                          "America/Indiana/Petersburg", 
                                                          "Eastern Standard Time"
                                                      }, 
                                                      {
                                                          "America/Indiana/Tell_City", 
                                                          "Central Standard Time"
                                                      }, 
                                                      {
                                                          "America/Indiana/Vevay", 
                                                          "Eastern Standard Time"
                                                      }, 
                                                      {
                                                          "America/Indiana/Vincennes", 
                                                          "Eastern Standard Time"
                                                      }, 
                                                      {
                                                          "America/Indiana/Winamac", 
                                                          "Eastern Standard Time"
                                                      }, 
                                                      {
                                                          "America/Inuvik", 
                                                          "Mountain Standard Time"
                                                      }, 
                                                      {
                                                          "America/Iqaluit", 
                                                          "Eastern Standard Time"
                                                      }, 
                                                      {
                                                          "America/Jamaica", 
                                                          "Eastern Standard Time"
                                                      }, 
                                                      {
                                                          "America/Juneau", 
                                                          "Alaskan Standard Time"
                                                      }, 
                                                      {
                                                          "America/Kentucky/Louisville", 
                                                          "Eastern Standard Time"
                                                      }, 
                                                      {
                                                          "America/Kentucky/Monticello", 
                                                          "Eastern Standard Time"
                                                      }, 
                                                      {
                                                          "America/La_Paz", 
                                                          "SA Western Standard Time"
                                                      }, 
                                                      {
                                                          "America/Lima", 
                                                          "SA Pacific Standard Time"
                                                      }, 
                                                      {
                                                          "America/Los_Angeles", 
                                                          "Pacific Standard Time"
                                                      }, 
                                                      {
                                                          "America/Maceio", 
                                                          "E. South America Standard Time"
                                                      }, 
                                                      {
                                                          "America/Managua", 
                                                          "Central Standard Time (Mexico)"
                                                      }, 
                                                      {
                                                          "America/Manaus", 
                                                          "Central Brazilian Standard Time"
                                                      }, 
                                                      {
                                                          "America/Martinique", 
                                                          "Atlantic Standard Time"
                                                      }, 
                                                      {
                                                          "America/Mazatlan", 
                                                          "Mountain Standard Time (Mexico)"
                                                      }, 
                                                      {
                                                          "America/Menominee", 
                                                          "Central Standard Time (Mexico)"
                                                      }, 
                                                      {
                                                          "America/Merida", 
                                                          "Central Standard Time (Mexico)"
                                                      }, 
                                                      {
                                                          "America/Mexico_City", 
                                                          "Central Standard Time (Mexico)"
                                                      }, 
                                                      {
                                                          "America/Miquelon", 
                                                          "Greenland Standard Time"
                                                      }, 
                                                      {
                                                          "America/Moncton", 
                                                          "Atlantic Standard Time"
                                                      }, 
                                                      {
                                                          "America/Monterrey", 
                                                          "Central Standard Time (Mexico)"
                                                      }, 
                                                      {
                                                          "America/Montevideo", 
                                                          "Montevideo Standard Time"
                                                      }, 
                                                      {
                                                          "America/Montreal", 
                                                          "Eastern Standard Time"
                                                      }, 
                                                      {
                                                          "America/Montserrat", 
                                                          "Atlantic Standard Time"
                                                      }, 
                                                      {
                                                          "America/Nassau", 
                                                          "Eastern Standard Time"
                                                      }, 
                                                      {
                                                          "America/New_York", 
                                                          "Eastern Standard Time"
                                                      }, 
                                                      {
                                                          "America/Nipigon", 
                                                          "Eastern Standard Time"
                                                      }, 
                                                      {
                                                          "America/Nome", 
                                                          "Alaskan Standard Time"
                                                      }, 
                                                      {
                                                          "America/Noronha", 
                                                          "Mid-Atlantic Standard Time"
                                                      }, 
                                                      {
                                                          "America/North_Dakota/Center", 
                                                          "Central Standard Time"
                                                      }, 
                                                      {
                                                          "America/North_Dakota/New_Salem", 
                                                          "Central Standard Time"
                                                      }, 
                                                      {
                                                          "America/Panama", 
                                                          "Eastern Standard Time"
                                                      }, 
                                                      {
                                                          "America/Pangnirtung", 
                                                          "Eastern Standard Time"
                                                      }, 
                                                      {
                                                          "America/Paramaribo", 
                                                          "SA Eastern Standard Time"
                                                      }, 
                                                      {
                                                          "America/Phoenix", 
                                                          "US Mountain Standard Time"
                                                      }, 
                                                      {
                                                          "America/Port_of_Spain", 
                                                          "Atlantic Standard Time"
                                                      }, 
                                                      {
                                                          "America/Port-au-Prince", 
                                                          "Eastern Standard Time"
                                                      }, 
                                                      {
                                                          "America/Porto_Velho", 
                                                          "Central Brazilian Standard Time"
                                                      }, 
                                                      {
                                                          "America/Puerto_Rico", 
                                                          "Atlantic Standard Time"
                                                      }, 
                                                      {
                                                          "America/Rainy_River", 
                                                          "Central Standard Time"
                                                      }, 
                                                      {
                                                          "America/Rankin_Inlet", 
                                                          "Central Standard Time"
                                                      }, 
                                                      {
                                                          "America/Recife", 
                                                          "E. South America Standard Time"
                                                      }, 
                                                      {
                                                          "America/Regina", 
                                                          "Central Standard Time"
                                                      }, 
                                                      {
                                                          "America/Resolute", 
                                                          "Eastern Standard Time"
                                                      }, 
                                                      {
                                                          "America/Rio_Branco", 
                                                          "SA Pacific Standard Time"
                                                      }, 
                                                      {
                                                          "America/Santiago", 
                                                          "SA Western Standard Time"
                                                      }, 
                                                      {
                                                          "America/Santo_Domingo", 
                                                          "Atlantic Standard Time"
                                                      }, 
                                                      {
                                                          "America/Sao_Paulo", 
                                                          "E. South America Standard Time"
                                                      }, 
                                                      {
                                                          "America/Scoresbysund", 
                                                          "Azores Standard Time"
                                                      }, 
                                                      {
                                                          "America/St_Johns", 
                                                          "Newfoundland Standard Time"
                                                      }, 
                                                      {
                                                          "America/St_Kitts", 
                                                          "Atlantic Standard Time"
                                                      }, 
                                                      {
                                                          "America/St_Lucia", 
                                                          "Atlantic Standard Time"
                                                      }, 
                                                      {
                                                          "America/St_Thomas", 
                                                          "Atlantic Standard Time"
                                                      }, 
                                                      {
                                                          "America/St_Vincent", 
                                                          "Atlantic Standard Time"
                                                      }, 
                                                      {
                                                          "America/Swift_Current", 
                                                          "Central Standard Time (Mexico)"
                                                      }, 
                                                      {
                                                          "America/Tegucigalpa", 
                                                          "Central Standard Time (Mexico)"
                                                      }, 
                                                      {
                                                          "America/Thule", 
                                                          "Atlantic Standard Time"
                                                      }, 
                                                      {
                                                          "America/Thunder_Bay", 
                                                          "Eastern Standard Time"
                                                      }, 
                                                      {
                                                          "America/Tijuana", 
                                                          "Pacific Standard Time (Mexico)"
                                                      }, 
                                                      {
                                                          "America/Toronto", 
                                                          "Eastern Standard Time"
                                                      }, 
                                                      {
                                                          "America/Tortola", 
                                                          "Atlantic Standard Time"
                                                      }, 
                                                      {
                                                          "America/Vancouver", 
                                                          "Pacific Standard Time"
                                                      }, 
                                                      {
                                                          "America/Whitehorse", 
                                                          "Pacific Standard Time"
                                                      }, 
                                                      {
                                                          "America/Winnipeg", 
                                                          "Central Standard Time"
                                                      }, 
                                                      {
                                                          "America/Yakutat", 
                                                          "Alaskan Standard Time"
                                                      }, 
                                                      {
                                                          "America/Yellowknife", 
                                                          "Mountain Standard Time"
                                                      }, 
                                                      {
                                                          "Antarctica/Casey", 
                                                          "W. Australia Standard Time"
                                                      }, 
                                                      {
                                                          "Antarctica/Davis", 
                                                          "SE Asia Standard Time"
                                                      }, 
                                                      {
                                                          "Antarctica/DumontDUrville", 
                                                          "E. Australia Standard Time"
                                                      }, 
                                                      {
                                                          "Antarctica/Mawson", 
                                                          "Central Asia Standard Time"
                                                      }, 
                                                      {
                                                          "Antarctica/McMurdo", 
                                                          "New Zealand Standard Time"
                                                      }, 
                                                      {
                                                          "Antarctica/Palmer", 
                                                          "SA Western Standard Time"
                                                      }, 
                                                      {
                                                          "Antarctica/Rothera", 
                                                          "SA Eastern Standard Time"
                                                      }, 
                                                      {
                                                          "Antarctica/Syowa", 
                                                          "E. Africa Standard Time"
                                                      }, 
                                                      {
                                                          "Antarctica/Vostok", 
                                                          "Central Asia Standard Time"
                                                      }, 
                                                      {
                                                          "Asia/Aden", 
                                                          "Arabic Standard Time"
                                                      }, 
                                                      {
                                                          "Asia/Almaty", 
                                                          "Central Asia Standard Time"
                                                      }, 
                                                      {
                                                          "Asia/Amman", 
                                                          "Jordan Standard Time"
                                                      }, 
                                                      {
                                                          "Asia/Anadyr", 
                                                          "New Zealand Standard Time"
                                                      }, 
                                                      {
                                                          "Asia/Aqtau", 
                                                          "West Asia Standard Time"
                                                      }, 
                                                      {
                                                          "Asia/Aqtobe", 
                                                          "West Asia Standard Time"
                                                      }, 
                                                      {
                                                          "Asia/Ashgabat", 
                                                          "West Asia Standard Time"
                                                      }, 
                                                      {
                                                          "Asia/Baghdad", 
                                                          "Arabic Standard Time"
                                                      }, 
                                                      {
                                                          "Asia/Bahrain", 
                                                          "Arabic Standard Time"
                                                      }, 
                                                      {
                                                          "Asia/Baku", 
                                                          "Azerbaijan Standard Time"
                                                      }, 
                                                      {
                                                          "Asia/Bangkok", 
                                                          "SE Asia Standard Time"
                                                      }, 
                                                      {
                                                          "Asia/Beirut", 
                                                          "Middle East Standard Time"
                                                      }, 
                                                      {
                                                          "Asia/Bishkek", 
                                                          "Central Asia Standard Time"
                                                      }, 
                                                      {
                                                          "Asia/Brunei", 
                                                          "North Asia East Standard Time"
                                                      }, 
                                                      {
                                                          "Asia/Choibalsan", 
                                                          "Yakutsk Standard Time"
                                                      }, 
                                                      {
                                                          "Asia/Chongqing", 
                                                          "China Standard Time"
                                                      }, 
                                                      {
                                                          "Asia/Colombo", 
                                                          "Sri Lanka Standard Time"
                                                      }, 
                                                      {
                                                          "Asia/Damascus", 
                                                          "Middle East Standard Time"
                                                      }, 
                                                      {
                                                          "Asia/Dhaka", 
                                                          "N. Central Asia Standard Time"
                                                      }, 
                                                      {
                                                          "Asia/Dili", 
                                                          "Korea Standard Time"
                                                      }, 
                                                      {
                                                          "Asia/Dubai", 
                                                          "Arabian Standard Time"
                                                      }, 
                                                      {
                                                          "Asia/Dushanbe", 
                                                          "West Asia Standard Time"
                                                      }, 
                                                      {
                                                          "Asia/Gaza", 
                                                          "Middle East Standard Time"
                                                      }, 
                                                      {
                                                          "Asia/Harbin", 
                                                          "China Standard Time"
                                                      }, 
                                                      {
                                                          "Asia/Ho_Chi_Minh", 
                                                          "SE Asia Standard Time"
                                                      }, 
                                                      {
                                                          "Asia/Hong_Kong", 
                                                          "Malay Peninsula Standard Time"
                                                      }, 
                                                      {
                                                          "Asia/Hovd", 
                                                          "SE Asia Standard Time"
                                                      }, 
                                                      {
                                                          "Asia/Irkutsk", 
                                                          "North Asia East Standard Time"
                                                      }, 
                                                      {
                                                          "Asia/Jakarta", 
                                                          "SE Asia Standard Time"
                                                      }, 
                                                      {
                                                          "Asia/Jayapura", 
                                                          "Korea Standard Time"
                                                      }, 
                                                      {
                                                          "Asia/Jerusalem", 
                                                          "Middle East Standard Time"
                                                      }, 
                                                      {
                                                          "Asia/Kabul", 
                                                          "Afghanistan Standard Time"
                                                      }, 
                                                      {
                                                          "Asia/Kamchatka", 
                                                          "Fiji Standard Time"
                                                      }, 
                                                      {
                                                          "Asia/Karachi", 
                                                          "West Asia Standard Time"
                                                      }, 
                                                      {
                                                          "Asia/Kashgar", 
                                                          "China Standard Time"
                                                      }, 
                                                      {
                                                          "Asia/Katmandu", 
                                                          "Nepal Standard Time"
                                                      }, 
                                                      {
                                                          "Asia/Kolkata", 
                                                          "India Standard Time"
                                                      }, 
                                                      {
                                                          "Asia/Krasnoyarsk", 
                                                          "North Asia Standard Time"
                                                      }, 
                                                      {
                                                          "Asia/Kuala_Lumpur", 
                                                          "Malay Peninsula Standard Time"
                                                      }, 
                                                      {
                                                          "Asia/Kuching", 
                                                          "Malay Peninsula Standard Time"
                                                      }, 
                                                      {
                                                          "Asia/Kuwait", 
                                                          "Arabic Standard Time"
                                                      }, 
                                                      {
                                                          "Asia/Macau", 
                                                          "China Standard Time"
                                                      }, 
                                                      {
                                                          "Asia/Magadan", 
                                                          "Central Pacific Standard Time"
                                                      }, 
                                                      {
                                                          "Asia/Makassar", 
                                                          "Malay Peninsula Standard Time"
                                                      }, 
                                                      {
                                                          "Asia/Manila", 
                                                          string
                                                          .Empty
                                                      }, 
                                                      {
                                                          "Asia/Muscat", 
                                                          "Arabian Standard Time"
                                                      }, 
                                                      {
                                                          "Asia/Nicosia", 
                                                          "Middle East Standard Time"
                                                      }, 
                                                      {
                                                          "Asia/Novosibirsk", 
                                                          "N. Central Asia Standard Time"
                                                      }, 
                                                      {
                                                          "Asia/Omsk", 
                                                          "N. Central Asia Standard Time"
                                                      }, 
                                                      {
                                                          "Asia/Oral", 
                                                          "Ekaterinburg Standard Time"
                                                      }, 
                                                      {
                                                          "Asia/Phnom_Penh", 
                                                          "SE Asia Standard Time"
                                                      }, 
                                                      {
                                                          "Asia/Pontianak", 
                                                          "SE Asia Standard Time"
                                                      }, 
                                                      {
                                                          "Asia/Pyongyang", 
                                                          "Korea Standard Time"
                                                      }, 
                                                      {
                                                          "Asia/Qatar", 
                                                          "Arabic Standard Time"
                                                      }, 
                                                      {
                                                          "Asia/Qyzylorda", 
                                                          "N. Central Asia Standard Time"
                                                      }, 
                                                      {
                                                          "Asia/Rangoon", 
                                                          "Myanmar Standard Time"
                                                      }, 
                                                      {
                                                          "Asia/Riyadh", 
                                                          "Arabic Standard Time"
                                                      }, 
                                                      {
                                                          "Asia/Sakhalin", 
                                                          "Vladivostok Standard Time"
                                                      }, 
                                                      {
                                                          "Asia/Samarkand", 
                                                          "West Asia Standard Time"
                                                      }, 
                                                      {
                                                          "Asia/Seoul", 
                                                          "Korea Standard Time"
                                                      }, 
                                                      {
                                                          "Asia/Shanghai", 
                                                          "China Standard Time"
                                                      }, 
                                                      {
                                                          "Asia/Singapore", 
                                                          "Malay Peninsula Standard Time"
                                                      }, 
                                                      {
                                                          "Asia/Taipei", 
                                                          "China Standard Time"
                                                      }, 
                                                      {
                                                          "Asia/Tashkent", 
                                                          "West Asia Standard Time"
                                                      }, 
                                                      {
                                                          "Asia/Tbilisi", 
                                                          "Arabian Standard Time"
                                                      }, 
                                                      {
                                                          "Asia/Tehran", 
                                                          "Iran Standard Time"
                                                      }, 
                                                      {
                                                          "Asia/Thimphu", 
                                                          "N. Central Asia Standard Time"
                                                      }, 
                                                      {
                                                          "Asia/Tokyo", 
                                                          "Tokyo Standard Time"
                                                      }, 
                                                      {
                                                          "Asia/Ulaanbaatar", 
                                                          "North Asia East Standard Time"
                                                      }, 
                                                      {
                                                          "Asia/Urumqi", 
                                                          "China Standard Time"
                                                      }, 
                                                      {
                                                          "Asia/Vientiane", 
                                                          "SE Asia Standard Time"
                                                      }, 
                                                      {
                                                          "Asia/Vladivostok", 
                                                          "Vladivostok Standard Time"
                                                      }, 
                                                      {
                                                          "Asia/Yakutsk", 
                                                          "Yakutsk Standard Time"
                                                      }, 
                                                      {
                                                          "Asia/Yekaterinburg", 
                                                          "Ekaterinburg Standard Time"
                                                      }, 
                                                      {
                                                          "Asia/Yerevan", 
                                                          "Caucasus Standard Time"
                                                      }, 
                                                      {
                                                          "Atlantic/Azores", 
                                                          "Azores Standard Time"
                                                      }, 
                                                      {
                                                          "Atlantic/Bermuda", 
                                                          "Atlantic Standard Time"
                                                      }, 
                                                      {
                                                          "Atlantic/Canary", 
                                                          "W. Europe Standard Time"
                                                      }, 
                                                      {
                                                          "Atlantic/Cape_Verde", 
                                                          "Cape Verde Standard Time"
                                                      }, 
                                                      {
                                                          "Atlantic/Faroe", 
                                                          "W. Europe Standard Time"
                                                      }, 
                                                      {
                                                          "Atlantic/Madeira", 
                                                          "W. Europe Standard Time"
                                                      }, 
                                                      {
                                                          "Atlantic/Reykjavik", 
                                                          "Greenwich Standard Time"
                                                      }, 
                                                      {
                                                          "Atlantic/South_Georgia", 
                                                          "Mid-Atlantic Standard Time"
                                                      }, 
                                                      {
                                                          "Atlantic/St_Helena", 
                                                          "Greenwich Standard Time"
                                                      }, 
                                                      {
                                                          "Atlantic/Stanley", 
                                                          "Pacific SA Standard Time"
                                                      }, 
                                                      {
                                                          "Australia/Adelaide", 
                                                          "Cen. Australia Standard Time"
                                                      }, 
                                                      {
                                                          "Australia/Brisbane", 
                                                          "E. Australia Standard Time"
                                                      }, 
                                                      {
                                                          "Australia/Broken_Hill", 
                                                          "Cen. Australia Standard Time"
                                                      }, 
                                                      {
                                                          "Australia/Currie", 
                                                          "Tasmania Standard Time"
                                                      }, 
                                                      {
                                                          "Australia/Darwin", 
                                                          "AUS Central Standard Time"
                                                      }, 
                                                      {
                                                          "Australia/Eucla", 
                                                          "Cen. Australia Standard Time"
                                                      }, 
                                                      {
                                                          "Australia/Hobart", 
                                                          "Tasmania Standard Time"
                                                      }, 
                                                      {
                                                          "Australia/Lindeman", 
                                                          "AUS Eastern Standard Time"
                                                      }, 
                                                      {
                                                          "Australia/Lord_Howe", 
                                                          "Central Pacific Standard Time"
                                                      }, 
                                                      {
                                                          "Australia/Melbourne", 
                                                          "AUS Eastern Standard Time"
                                                      }, 
                                                      {
                                                          "Australia/Perth", 
                                                          "W. Australia Standard Time"
                                                      }, 
                                                      {
                                                          "Australia/Sydney", 
                                                          "AUS Eastern Standard Time"
                                                      }, 
                                                      {
                                                          "CET", 
                                                          "Central Europe Standard Time"
                                                      }, 
                                                      {
                                                          "CST6CDT", 
                                                          "Central Standard Time"
                                                      }, 
                                                      {
                                                          "EET", 
                                                          "E. Europe Standard Time"
                                                      }, 
                                                      {
                                                          "EST", 
                                                          "Eastern Standard Time"
                                                      }, 
                                                      {
                                                          "EST5EDT", 
                                                          "Eastern Standard Time"
                                                      }, 
                                                      {
                                                          "Etc/GMT", 
                                                          "GMT Standard Time"
                                                      }, 
                                                      {
                                                          "Etc/UCT", 
                                                          "GMT Standard Time"
                                                      }, 
                                                      {
                                                          "Etc/UTC", 
                                                          "GMT Standard Time"
                                                      }, 
                                                      {
                                                          "Europe/Amsterdam", 
                                                          "W. Europe Standard Time"
                                                      }, 
                                                      {
                                                          "Europe/Andorra", 
                                                          "Central Europe Standard Time"
                                                      }, 
                                                      {
                                                          "Europe/Athens", 
                                                          "E. Europe Standard Time"
                                                      }, 
                                                      {
                                                          "Europe/Belgrade", 
                                                          "Central Europe Standard Time"
                                                      }, 
                                                      {
                                                          "Europe/Berlin", 
                                                          "W. Europe Standard Time"
                                                      }, 
                                                      {
                                                          "Europe/Brussels", 
                                                          "Central European Standard Time"
                                                      }, 
                                                      {
                                                          "Europe/Bucharest", 
                                                          "E. Europe Standard Time"
                                                      }, 
                                                      {
                                                          "Europe/Budapest", 
                                                          "Central European Standard Time"
                                                      }, 
                                                      {
                                                          "Europe/Chisinau", 
                                                          "E. Europe Standard Time"
                                                      }, 
                                                      {
                                                          "Europe/Copenhagen", 
                                                          "Central European Standard Time"
                                                      }, 
                                                      {
                                                          "Europe/Dublin", 
                                                          "GMT Standard Time"
                                                      }, 
                                                      {
                                                          "Europe/Gibraltar", 
                                                          "Central European Standard Time"
                                                      }, 
                                                      {
                                                          "Europe/Helsinki", 
                                                          "E. Europe Standard Time"
                                                      }, 
                                                      {
                                                          "Europe/Istanbul", 
                                                          "E. Europe Standard Time"
                                                      }, 
                                                      {
                                                          "Europe/Kaliningrad", 
                                                          "E. Europe Standard Time"
                                                      }, 
                                                      {
                                                          "Europe/Kiev", 
                                                          "E. Europe Standard Time"
                                                      }, 
                                                      {
                                                          "Europe/Lisbon", 
                                                          "W. Europe Standard Time"
                                                      }, 
                                                      {
                                                          "Europe/London", 
                                                          "GMT Standard Time"
                                                      }, 
                                                      {
                                                          "Europe/Luxembourg", 
                                                          "Central European Standard Time"
                                                      }, 
                                                      {
                                                          "Europe/Madrid", 
                                                          "Central European Standard Time"
                                                      }, 
                                                      {
                                                          "Europe/Malta", 
                                                          "Central European Standard Time"
                                                      }, 
                                                      {
                                                          "Europe/Minsk", 
                                                          "E. Europe Standard Time"
                                                      }, 
                                                      {
                                                          "Europe/Monaco", 
                                                          "Central European Standard Time"
                                                      }, 
                                                      {
                                                          "Europe/Moscow", 
                                                          "Russian Standard Time"
                                                      }, 
                                                      {
                                                          "Europe/Oslo", 
                                                          "Central European Standard Time"
                                                      }, 
                                                      {
                                                          "Europe/Paris", 
                                                          "Central European Standard Time"
                                                      }, 
                                                      {
                                                          "Europe/Prague", 
                                                          "Central European Standard Time"
                                                      }, 
                                                      {
                                                          "Europe/Riga", 
                                                          "E. Europe Standard Time"
                                                      }, 
                                                      {
                                                          "Europe/Rome", 
                                                          "W. Europe Standard Time"
                                                      }, 
                                                      {
                                                          "Europe/Samara", 
                                                          "Caucasus Standard Time"
                                                      }, 
                                                      {
                                                          "Europe/Simferopol", 
                                                          "E. Europe Standard Time"
                                                      }, 
                                                      {
                                                          "Europe/Sofia", 
                                                          "E. Europe Standard Time"
                                                      }, 
                                                      {
                                                          "Europe/Stockholm", 
                                                          "W. Europe Standard Time"
                                                      }, 
                                                      {
                                                          "Europe/Tallinn", 
                                                          "E. Europe Standard Time"
                                                      }, 
                                                      {
                                                          "Europe/Tirane", 
                                                          "Central European Standard Time"
                                                      }, 
                                                      {
                                                          "Europe/Uzhgorod", 
                                                          "E. Europe Standard Time"
                                                      }, 
                                                      {
                                                          "Europe/Vaduz", 
                                                          "W. Europe Standard Time"
                                                      }, 
                                                      {
                                                          "Europe/Vienna", 
                                                          "Central European Standard Time"
                                                      }, 
                                                      {
                                                          "Europe/Vilnius", 
                                                          "E. Europe Standard Time"
                                                      }, 
                                                      {
                                                          "Europe/Volgograd", 
                                                          "Russian Standard Time"
                                                      }, 
                                                      {
                                                          "Europe/Warsaw", 
                                                          "Central European Standard Time"
                                                      }, 
                                                      {
                                                          "Europe/Zaporozhye", 
                                                          "E. Europe Standard Time"
                                                      }, 
                                                      {
                                                          "Europe/Zurich", 
                                                          "Central European Standard Time"
                                                      }, 
                                                      {
                                                          "HST", 
                                                          "Hawaiian Standard Time"
                                                      }, 
                                                      {
                                                          "Indian/Antananarivo", 
                                                          "E. Africa Standard Time"
                                                      }, 
                                                      {
                                                          "Indian/Chagos", 
                                                          "Central Asia Standard Time"
                                                      }, 
                                                      {
                                                          "Indian/Christmas", 
                                                          "SE Asia Standard Time"
                                                      }, 
                                                      {
                                                          "Indian/Cocos", 
                                                          "Myanmar Standard Time"
                                                      }, 
                                                      {
                                                          "Indian/Comoro", 
                                                          "E. Africa Standard Time"
                                                      }, 
                                                      {
                                                          "Indian/Kerguelen", 
                                                          "West Asia Standard Time"
                                                      }, 
                                                      {
                                                          "Indian/Mahe", 
                                                          "Arabian Standard Time"
                                                      }, 
                                                      {
                                                          "Indian/Maldives", 
                                                          "West Asia Standard Time"
                                                      }, 
                                                      {
                                                          "Indian/Mauritius", 
                                                          "Azerbaijan Standard Time"
                                                      }, 
                                                      {
                                                          "Indian/Mayotte", 
                                                          "E. Africa Standard Time"
                                                      }, 
                                                      {
                                                          "Indian/Reunion", 
                                                          "Arabian Standard Time"
                                                      }, 
                                                      {
                                                          "MET", 
                                                          "Central European Standard Time"
                                                      }, 
                                                      {
                                                          "MST", 
                                                          "Mountain Standard Time"
                                                      }, 
                                                      {
                                                          "MST7MDT", 
                                                          "US Mountain Standard Time"
                                                      }, 
                                                      {
                                                          "Pacific/Apia", 
                                                          "Samoa Standard Time"
                                                      }, 
                                                      {
                                                          "Pacific/Auckland", 
                                                          "New Zealand Standard Time"
                                                      }, 
                                                      {
                                                          "Pacific/Chatham", 
                                                          "Fiji Standard Time"
                                                      }, 
                                                      {
                                                          "Pacific/Easter", 
                                                          "Central Standard Time (Mexico)"
                                                      }, 
                                                      {
                                                          "Pacific/Efate", 
                                                          "Central Pacific Standard Time"
                                                      }, 
                                                      {
                                                          "Pacific/Enderbury", 
                                                          "Tonga Standard Time"
                                                      }, 
                                                      {
                                                          "Pacific/Fakaofo", 
                                                          "Hawaiian Standard Time"
                                                      }, 
                                                      {
                                                          "Pacific/Fiji", 
                                                          "Fiji Standard Time"
                                                      }, 
                                                      {
                                                          "Pacific/Funafuti", 
                                                          "Fiji Standard Time"
                                                      }, 
                                                      {
                                                          "Pacific/Galapagos", 
                                                          "Central Standard Time"
                                                      }, 
                                                      {
                                                          "Pacific/Gambier", 
                                                          "Alaskan Standard Time"
                                                      }, 
                                                      {
                                                          "Pacific/Guadalcanal", 
                                                          "Central Pacific Standard Time"
                                                      }, 
                                                      {
                                                          "Pacific/Guam", 
                                                          "West Pacific Standard Time"
                                                      }, 
                                                      {
                                                          "Pacific/Honolulu", 
                                                          "Hawaiian Standard Time"
                                                      }, 
                                                      {
                                                          "Pacific/Johnston", 
                                                          "Hawaiian Standard Time"
                                                      }, 
                                                      {
                                                          "Pacific/Kiritimati", 
                                                          "Tonga Standard Time"
                                                      }, 
                                                      {
                                                          "Pacific/Kosrae", 
                                                          "Central Pacific Standard Time"
                                                      }, 
                                                      {
                                                          "Pacific/Kwajalein", 
                                                          "Fiji Standard Time"
                                                      }, 
                                                      {
                                                          "Pacific/Majuro", 
                                                          "Fiji Standard Time"
                                                      }, 
                                                      {
                                                          "Pacific/Marquesas", 
                                                          "Pacific Standard Time (Mexico)"
                                                      }, 
                                                      {
                                                          "Pacific/Midway", 
                                                          "Samoa Standard Time"
                                                      }, 
                                                      {
                                                          "Pacific/Nauru", 
                                                          "Fiji Standard Time"
                                                      }, 
                                                      {
                                                          "Pacific/Niue", 
                                                          "Samoa Standard Time"
                                                      }, 
                                                      {
                                                          "Pacific/Norfolk", 
                                                          "Central Pacific Standard Time"
                                                      }, 
                                                      {
                                                          "Pacific/Noumea", 
                                                          "Central Pacific Standard Time"
                                                      }, 
                                                      {
                                                          "Pacific/Pago_Pago", 
                                                          "Samoa Standard Time"
                                                      }, 
                                                      {
                                                          "Pacific/Palau", 
                                                          "Tokyo Standard Time"
                                                      }, 
                                                      {
                                                          "Pacific/Pitcairn", 
                                                          "Pacific Standard Time"
                                                      }, 
                                                      {
                                                          "Pacific/Ponape", 
                                                          "Central Pacific Standard Time"
                                                      }, 
                                                      {
                                                          "Pacific/Port_Moresby", 
                                                          "West Pacific Standard Time"
                                                      }, 
                                                      {
                                                          "Pacific/Rarotonga", 
                                                          "Hawaiian Standard Time"
                                                      }, 
                                                      {
                                                          "Pacific/Saipan", 
                                                          "West Pacific Standard Time"
                                                      }, 
                                                      {
                                                          "Pacific/Tahiti", 
                                                          "Hawaiian Standard Time"
                                                      }, 
                                                      {
                                                          "Pacific/Tarawa", 
                                                          "Fiji Standard Time"
                                                      }, 
                                                      {
                                                          "Pacific/Tongatapu", 
                                                          "Tonga Standard Time"
                                                      }, 
                                                      {
                                                          "Pacific/Truk", 
                                                          "West Pacific Standard Time"
                                                      }, 
                                                      {
                                                          "Pacific/Wake", 
                                                          "Fiji Standard Time"
                                                      }, 
                                                      {
                                                          "Pacific/Wallis", 
                                                          "Fiji Standard Time"
                                                      }, 
                                                      {
                                                          "PST8PDT", 
                                                          "Pacific Standard Time"
                                                      }, 
                                                      {
                                                          "WET", 
                                                          "W. Europe Standard Time"
                                                      }, 
                                                      {
                                                          "Etc/GMT+9", 
                                                          "Alaskan Standard Time"
                                                      }, 
                                                      {
                                                          "Asia/Riyadh87", 
                                                          "Arabic Standard Time"
                                                      }, 
                                                      {
                                                          "Asia/Riyadh88", 
                                                          "Arabic Standard Time"
                                                      }, 
                                                      {
                                                          "Asia/Riyadh89", 
                                                          "Arabic Standard Time"
                                                      }, 
                                                      {
                                                          "Etc/GMT+4", 
                                                          "Atlantic Standard Time"
                                                      }, 
                                                      {
                                                          "Etc/GMT-10", 
                                                          "AUS Eastern Standard Time"
                                                      }, 
                                                      {
                                                          "Etc/GMT-4", 
                                                          "Azerbaijan Standard Time"
                                                      }, 
                                                      {
                                                          "Etc/GMT+1", 
                                                          "Azores Standard Time"
                                                      }, 
                                                      {
                                                          "Etc/GMT-1", 
                                                          "Central Europe Standard Time"
                                                      }, 
                                                      {
                                                          "Etc/GMT-11", 
                                                          "Central Pacific Standard Time"
                                                      }, 
                                                      {
                                                          "Etc/GMT+6", 
                                                          "Central Standard Time"
                                                      }, 
                                                      {
                                                          "Etc/GMT+12", 
                                                          "Dateline Standard Time"
                                                      }, 
                                                      {
                                                          "Etc/GMT-2", 
                                                          "E. Europe Standard Time"
                                                      }, 
                                                      {
                                                          "Etc/GMT+3", 
                                                          "E. South America Standard Time"
                                                      }, 
                                                      {
                                                          "Etc/GMT+5", 
                                                          "Eastern Standard Time"
                                                      }, 
                                                      {
                                                          "Etc/GMT-5", 
                                                          "Ekaterinburg Standard Time"
                                                      }, 
                                                      {
                                                          "Etc/GMT+10", 
                                                          "Hawaiian Standard Time"
                                                      }, 
                                                      {
                                                          "Etc/GMT+2", 
                                                          "Mid-Atlantic Standard Time"
                                                      }, 
                                                      {
                                                          "Etc/GMT+7", 
                                                          "Mountain Standard Time"
                                                      }, 
                                                      {
                                                          "Etc/GMT-6", 
                                                          "N. Central Asia Standard Time"
                                                      }, 
                                                      {
                                                          "Etc/GMT-12", 
                                                          "New Zealand Standard Time"
                                                      }, 
                                                      {
                                                          "Etc/GMT-7", 
                                                          "North Asia Standard Time"
                                                      }, 
                                                      {
                                                          "Etc/GMT+8", 
                                                          "Pacific Standard Time"
                                                      }, 
                                                      {
                                                          "Etc/GMT-3", 
                                                          "Russian Standard Time"
                                                      }, 
                                                      {
                                                          "Etc/GMT+11", 
                                                          "Samoa Standard Time"
                                                      }, 
                                                      {
                                                          "Etc/GMT-13", 
                                                          "Tonga Standard Time"
                                                      }, 
                                                      {
                                                          "Etc/GMT-14", 
                                                          "Tonga Standard Time"
                                                      }, 
                                                      {
                                                          "Etc/GMT-8", 
                                                          "W. Australia Standard Time"
                                                      }, 
                                                      {
                                                          "Etc/GMT-9", 
                                                          "Yakutsk Standard Time"
                                                      }
                                                  };

            // DAE 2013-03-28 - Resolución Caso 10
            aux.Remove("Asia/Hong_Kong");
            aux.Remove("Asia/Kuala_Lumpur");
            aux.Remove("Asia/Kuching");
            aux.Remove("Asia/Makassar");
            aux.Remove("Asia/Manila");
            aux.Remove("Asia/Singapore");

            // Merge de los diccionarios.
            IDictionary<string, string> dictXml = MapFromXml;
            foreach (KeyValuePair<string, string> item in dictXml)
            {
                aux[item.Key] = item.Value;
            }

            return aux;
        }

        #endregion
    }
}