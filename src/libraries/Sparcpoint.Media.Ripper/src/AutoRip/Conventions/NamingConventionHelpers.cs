using System;
using System.IO;

namespace Sparcpoint.Media.Ripper
{
    internal static class NamingConventionHelpers
    {
        public const string DISC_NO_PLACEHOLDER = "{DiscIndex}";
        public const string TITLE_INDEX_PLACEHOLDER = "{TitleIndex}";
        public const string SESSION_INDEX_PLACEHOLDER = "{SessionIndex}";
        
        private const string INDEX_PLACEHOLDER_FORMAT = "00";

        public static void EnsureProperNamingConvention(string namingConvention)
        {
            if (string.IsNullOrWhiteSpace(namingConvention))
                throw new ArgumentException("Naming convention must not be null or whitespace.", nameof(namingConvention));

            if (!namingConvention.Contains(TITLE_INDEX_PLACEHOLDER) && !namingConvention.Contains(SESSION_INDEX_PLACEHOLDER))
                throw new ArgumentException($"Naming convention must contain the placeholder '{TITLE_INDEX_PLACEHOLDER}' or '{SESSION_INDEX_PLACEHOLDER}' otherwise naming collisions will occur.");

            if (ContainsInvalidFileNameCharacters(namingConvention))
                throw new ArgumentException("Naming convention contains invalid file name characters.");
        }

        public static string ReplaceIndex(string namingConvention, int index, int sessionIndexId, int discNo)
            => namingConvention
                .Replace(TITLE_INDEX_PLACEHOLDER, (index + 1).ToString(INDEX_PLACEHOLDER_FORMAT))
                .Replace(SESSION_INDEX_PLACEHOLDER, (sessionIndexId + 1).ToString(INDEX_PLACEHOLDER_FORMAT))
                .Replace(DISC_NO_PLACEHOLDER, discNo.ToString(INDEX_PLACEHOLDER_FORMAT))
            ;

        public static bool ContainsInvalidFileNameCharacters(string namingConvention)
            => ReplaceIndex(namingConvention, 0, 0, 0).IndexOfAny(Path.GetInvalidFileNameChars()) >= 0;
    }
}
