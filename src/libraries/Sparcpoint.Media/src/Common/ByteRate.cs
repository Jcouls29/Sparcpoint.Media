using System;
using System.Text.RegularExpressions;

namespace Sparcpoint.Media
{
    public struct ByteRate
    {
        public long BytesPerSecond { get; set; }

        public override string ToString()
            => BytesPerSecond.ToUnitString() + "B/s";

        public static implicit operator ByteRate(long bytes)
            => new ByteRate { BytesPerSecond = bytes };

        public static implicit operator long(ByteRate rate)
            => rate.BytesPerSecond;

        private static Regex PATTERN = new Regex(@"^(?<Value>[\d\.]+)\s*(?<Prefix>(G|M|k|K))?(b|B)?/?s?$");
        public static bool TryParse(string value, out ByteRate result)
        {
            result = default;

            var returnResult = PATTERN.TryParse(value, out long longResult);
            if (!returnResult)
                return false;

            result = new ByteRate { BytesPerSecond = longResult };
            return true;
        }
    }
}
