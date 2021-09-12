using System;

namespace Sparcpoint.Media.Ripper.MakeMKV
{
    public class MakeMKVCommandLinePath
    {
        public MakeMKVCommandLinePath(string path)
        {
            Path = path ?? throw new ArgumentNullException(nameof(path));
        }

        public string Path { get; }
        public bool DoesExist => Path == null ? false : System.IO.File.Exists(Path);

        public override string ToString()
            => Path;
        public override int GetHashCode()
            => Path?.GetHashCode() ?? 0;

        public static implicit operator MakeMKVCommandLinePath(string path)
            => new MakeMKVCommandLinePath(path);
        public static implicit operator string(MakeMKVCommandLinePath path)
            => path?.Path;
    }
}
