using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace GAIA.Common
{
    public static class Extensions
    {
        #region -- String --------------------------------------------------------------------------
        public static bool IsNullOrEmpty(this string s)
        {
            return string.IsNullOrEmpty(s);
        }

        public static int ToInt(this string s)
        {
            return Convert.ToInt32(s);
        }

        public static bool Compare(this string s, string compareTo)
        {
            return string.Compare(s, compareTo, CultureInfo.InvariantCulture, System.Globalization.CompareOptions.OrdinalIgnoreCase) == 0;
        }
        #endregion ---------------------------------------------------------------------------------

        #region -- Dictionary ----------------------------------------------------------------------
        public static Dictionary<TKey, TValue> Merge<TKey, TValue>(this Dictionary<TKey, TValue> mainDictionary, Dictionary<TKey, TValue> secondaryDictionary)
        {
            return mainDictionary.Concat(secondaryDictionary).GroupBy(d => d.Key).ToDictionary(d => d.Key, d => d.First().Value); ;
        }
        #endregion ---------------------------------------------------------------------------------
    }
}
