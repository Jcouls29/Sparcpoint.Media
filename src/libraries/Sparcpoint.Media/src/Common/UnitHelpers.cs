using System;
using System.Text.RegularExpressions;

namespace Sparcpoint.Media
{
    internal static class UnitHelpers
    {
        public const long KILO = 1024;
        public const long MEGA = KILO * 1024;
        public const long GIGA = MEGA * 1024;
        public const long TERA = GIGA * 1024;

        public static string ToUnitString(this long count)
        {
            if (count < KILO)
                return count.ToString() + " ";
            if (count < MEGA)
                return Math.Round((decimal)count / KILO, 2).ToString() + " K";
            if (count < GIGA)
                return Math.Round((decimal)count / MEGA, 2).ToString() + " M";

            return Math.Round((decimal)count / GIGA, 2).ToString() + " G";
        }

        public static bool TryParse(this Regex PATTERN, string value, out long result)
        {
            result = default;

            var match = PATTERN.Match(value);
            if (match.Success)
            {
                var rawValue = decimal.Parse(match.Groups["Value"].Value);
                var multiplier = match.Groups["Prefix"].Value?.ToUpper() ?? "";

                switch (multiplier)
                {
                    case "G":
                        result = (long)Math.Round(rawValue * UnitHelpers.GIGA);
                        return true;
                    case "M":
                        result = (long)Math.Round(rawValue * UnitHelpers.MEGA);
                        return true;
                    case "K":
                        result = (long)Math.Round(rawValue * UnitHelpers.KILO);
                        return true;
                    default:
                        result = (long)Math.Round(rawValue);
                        return true;
                }
            }

            return false;
        }
    }
}
