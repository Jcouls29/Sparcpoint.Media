using System;
using System.Text.RegularExpressions;

namespace Sparcpoint.Media
{
    public struct FileSize
    {
        public long Bytes { get; set; }

        public override string ToString()
            => Bytes.ToUnitString() + "B";

        public static implicit operator FileSize(long bytes)
            => new FileSize { Bytes = bytes };

        public static implicit operator long(FileSize size)
            => size.Bytes;
        
        private static Regex PATTERN = new Regex(@"^(?<Value>[\d\.]+)\s*(?<Prefix>(G|M|k|K))?(b|B)?$");
        public static bool TryParse(string value, out FileSize result)
        {
            result = default;

            var returnResult = PATTERN.TryParse(value, out long longResult);
            if (!returnResult)
                return false;

            result = new FileSize { Bytes = longResult };
            return true;
        }

        public static FileSize Parse(string value)
        {
            if (TryParse(value, out FileSize result))
                return result;

            throw new Exception("Parse Error.");
        }
    }
}
