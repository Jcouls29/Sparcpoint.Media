using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Sparcpoint.Media.Extensions.Logging
{
    public sealed class LoggedCommandLineExecutor : ICommandLineExecutor
    {
        private readonly ILogger<LoggedCommandLineExecutor> _Logger;
        private readonly ICommandLineExecutor _InnerService;

        public LoggedCommandLineExecutor(ILogger<LoggedCommandLineExecutor> logger, ICommandLineExecutor innerService)
        {
            _Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _InnerService = innerService ?? throw new ArgumentNullException(nameof(innerService));
        }

        public async Task<CommandLineResult> ExecuteAsync(IEnumerable<string> arguments, CancellationToken cancelToken = default)
        {
            using(_Logger.Measure("Command Executed"))
            {
                _Logger.LogDebug("Executing Command with arguments: {Arguments}", arguments);
                var results = await _InnerService.ExecuteAsync(arguments, cancelToken);
                _Logger.LogDebug("Exit Code: {ExitCode}, Elapsed Time: {ElapsedTime}", results.ExitCode, results.RunTime);
                return results;
            }
        }
    }
}
