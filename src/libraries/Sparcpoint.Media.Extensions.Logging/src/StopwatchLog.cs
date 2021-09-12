using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;

namespace Sparcpoint.Media.Extensions.Logging
{
    internal class StopwatchLog : IDisposable
    {
        private readonly ILogger _Logger;
        private readonly string _Message;
        private readonly Stopwatch _Watch;

        private StopwatchLog(ILogger logger, string message)
        {
            _Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _Message = message ?? throw new ArgumentNullException(nameof(message));
            _Watch = Stopwatch.StartNew();
        }

        public void Dispose()
        {
            _Watch.Stop();
            _Logger.LogDebug($"{_Message} (Elapsed: {{Elapsed}})", _Watch.Elapsed);
        }

        public static StopwatchLog Start(ILogger logger, string message)
            => new StopwatchLog(logger, message);
    }
}
