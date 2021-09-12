using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Sparcpoint.Media.Ripper
{
    public class AutoRipOptions
    {
        public ICollection<IAutoRipNamingConvention> NamingConventions { get; set; } = new List<IAutoRipNamingConvention>();

        public string RootPath { get; set; }
        public int StartingDiscNumber { get; set; } = 1;

        internal void EnsureValidOptions()
        {
            if (NamingConventions == null)
                return;
            if (NamingConventions.Any(t => t == null))
                throw new ArgumentException("Null-valued Title Naming convention not allowed.");

            if (string.IsNullOrWhiteSpace(RootPath))
                throw new ArgumentException("Root path is required.");

            if (!Directory.Exists(RootPath))
                Directory.CreateDirectory(RootPath);
        }

        internal string CreateSubFolder(string subFolder)
        {
            if (string.IsNullOrWhiteSpace(subFolder))
                return RootPath;

            var path = Path.Combine(RootPath, subFolder);
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            return path;
        }

        internal NamingConventionResult GetNewFileName(DiscTitleRecord record)
        {
            foreach(var convention in NamingConventions)
            {
                if (convention.ShouldInclude(record))
                    return convention.GetNextFileName(record);
            }

            return null;
        }
    }
}
