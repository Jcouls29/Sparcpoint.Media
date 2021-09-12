using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Sparcpoint.Media
{
    public interface ICommandLineExecutor
    {
        Task<CommandLineResult> ExecuteAsync(IEnumerable<string> arguments, CancellationToken cancelToken = default);
    }

    public class CommandLineResult
    {
        public CommandLineResult(int exitCode, DateTimeOffset startTime, DateTimeOffset exitTime)
        {
            ExitCode = exitCode;
            StartTime = startTime;
            ExitTime = exitTime;
        }

        public int ExitCode { get; set; }
        public DateTimeOffset StartTime { get; set; }
        public DateTimeOffset ExitTime { get; set; }
        public TimeSpan RunTime => ExitTime - StartTime;

        public string StandardOutput { get; set; }
        public string StandardError { get; set; }

        public IEnumerable<string> Messages => StandardOutput?.Split('\n')?.Select(value => value.Trim())?.ToArray();
        public IEnumerable<string> Errors => StandardError?.Split('\n')?.Select(value => value.Trim())?.ToArray();
    }

    public static class CommandLineResultExtensions
    {
        public static void EnsureSuccessResult(this CommandLineResult result)
        {
            if (result == null)
                throw new ArgumentNullException(nameof(result));

            if (result.ExitCode != 0)
                throw new Exception($"Process failed with exit code {result.ExitCode}");
        }
    }
}
