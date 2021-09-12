using CliWrap;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sparcpoint.Media
{
    public class CliWrapCommandLineExecutor : ICommandLineExecutor
    {
        private readonly string _ExePath;

        public CliWrapCommandLineExecutor(string exePath)
        {
            _ExePath = exePath ?? throw new ArgumentNullException(nameof(exePath));
        }

        public async Task<CommandLineResult> ExecuteAsync(IEnumerable<string> arguments, CancellationToken cancelToken = default)
        {
            var outBuffer = new StringBuilder();
            var errBuffer = new StringBuilder();

            var result = await Cli.Wrap(_ExePath)
                .WithArguments(arguments)
                .WithStandardOutputPipe(PipeTarget.ToStringBuilder(outBuffer))
                .WithStandardErrorPipe(PipeTarget.ToStringBuilder(errBuffer))
                .ExecuteAsync(cancelToken);

            var stdOut = outBuffer.ToString();
            var errOut = errBuffer.ToString();

            return new CommandLineResult(result.ExitCode, result.StartTime, result.ExitTime)
            {
                StandardOutput = stdOut,
                StandardError = errOut,
            };
        }
    }
}
