using Microsoft.Extensions.Logging;
using Sparcpoint.Media.Extensions.Logging;
using Sparcpoint.Media.Ripper;
using System;

namespace Sparcpoint.Media
{
    public static class LoggerExtensions
    {
        public static IAutoRipService Wrap(this IAutoRipService service, ILoggerFactory factory)
            => new LoggedAutoRipService(factory.CreateLogger<LoggedAutoRipService>(), service);

        public static ICommandLineExecutor Wrap(this ICommandLineExecutor service, ILoggerFactory factory)
            => new LoggedCommandLineExecutor(factory.CreateLogger<LoggedCommandLineExecutor>(), service);

        public static IDiscDriveAccess Wrap(this IDiscDriveAccess service, ILoggerFactory factory)
            => new LoggedDiscDriveAccess(factory.CreateLogger<LoggedDiscDriveAccess>(), service);

        public static IDiscTitleReader Wrap(this IDiscTitleReader service, ILoggerFactory factory)
            => new LoggedDiscTitleReader(factory.CreateLogger<LoggedDiscTitleReader>(), service);

        public static IDiscTitleSaver Wrap(this IDiscTitleSaver service, ILoggerFactory factory)
            => new LoggedDiscTitleSaver(factory.CreateLogger<LoggedDiscTitleSaver>(), service);

        public static IDriveQuery Wrap(this IDriveQuery service, ILoggerFactory factory)
            => new LoggedDriveQuery(factory.CreateLogger<LoggedDriveQuery>(), service);

        public static IDisposable Measure(this ILogger logger, string message)
            => StopwatchLog.Start(logger, message);
    }
}
