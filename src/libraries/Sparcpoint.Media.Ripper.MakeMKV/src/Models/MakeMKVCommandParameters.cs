using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CliWrap;

namespace Sparcpoint.Media.Ripper.MakeMKV
{
    internal abstract class MakeMKVCommandParameters
    {
        public bool UseRobotMode { get; set; } = true;
        public int CacheSizeInMB { get; set; } = 512;
        public bool UseDirectIO { get; set; } = false;
        public bool NoScan { get; set; } = false;
        public int MinimumLengthInSeconds { get; set; } = 0;
        public bool PerformDecrypt { get; set; } = false;
        
        public MakeMKVCommand Command { get; set; }

        protected abstract IEnumerable<string> GetCommandArguments();

        public async Task<CommandLineResult> RunCommand(ICommandLineExecutor executor, CancellationToken cancelToken = default)
        {
            List<string> arguments = new List<string>();

            if (UseRobotMode)
                arguments.Add("-r");
            if (CacheSizeInMB > 0)
                arguments.Add($"--cache={CacheSizeInMB}");
            if (UseDirectIO)
                arguments.Add("--directio=true");
            if (NoScan)
                arguments.Add("--noscan");
            if (PerformDecrypt)
                arguments.Add("--decrypt");
            if (MinimumLengthInSeconds > 0)
                arguments.Add($"--minlength={MinimumLengthInSeconds}");

            arguments.AddRange(GetCommandArguments());

            return await executor.ExecuteAsync(arguments, cancelToken);
        }
    }
}
