using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Sparcpoint.Media.Ripper.MakeMKV.Tests
{
    internal class StaticResultCommandLineExecutor : ICommandLineExecutor
    {
        private readonly string _StandardOutput;
        private readonly string _StandardError;

        public StaticResultCommandLineExecutor(string stdOutput, string stdError = "")
        {
            _StandardOutput = stdOutput?.Trim() ?? throw new ArgumentNullException(nameof(stdOutput));
            _StandardError = stdError?.Trim() ?? throw new ArgumentNullException(nameof(stdError));
        }

        public Task<CommandLineResult> ExecuteAsync(IEnumerable<string> arguments, CancellationToken cancelToken = default)
        {
            return Task.FromResult(new CommandLineResult(0, DateTimeOffset.Now, DateTimeOffset.Now)
            {
                StandardOutput = _StandardOutput,
                StandardError = _StandardError
            });
        }
    }
}
