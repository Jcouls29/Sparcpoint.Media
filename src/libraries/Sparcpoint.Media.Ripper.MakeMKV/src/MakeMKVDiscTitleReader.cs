using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Sparcpoint.Media.Ripper.MakeMKV
{
    public class MakeMKVDiscTitleReader : IDiscTitleReader
    {
        private readonly ICommandLineExecutor _Executor;
        private readonly CsvConfiguration _CsvConfiguration;

        public MakeMKVDiscTitleReader(ICommandLineExecutor executor)
        {
            _Executor = executor ?? throw new ArgumentNullException(nameof(executor));
            _CsvConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false
            };
        }
        
        public async Task<IEnumerable<DiscTitleRecord>> ReadTitleMetadata(DriveInformation drive, ReadTitleMetadataOptions options = null, CancellationToken cancelToken = default)
        {
            options = options ?? ReadTitleMetadataOptions.Default();

            var cmd = new ReadTitlesCommand(drive.DriveId);
            if (options.MinimumLength.TotalSeconds > 0)
                cmd.MinimumLengthInSeconds = (int)options.MinimumLength.TotalSeconds;

            var cmdResult = await (new ReadTitlesCommand(drive.DriveId)).RunCommand(_Executor, cancelToken);
            cmdResult.EnsureSuccessResult();

            IEnumerable<CINFO> cinfo = ParseCsv<CINFO>(CombineMessages("CINFO:", cmdResult.Messages));
            IEnumerable<TINFO> tinfo = ParseCsv<TINFO>(CombineMessages("TINFO:", cmdResult.Messages));
            IEnumerable<SINFO> sinfo = ParseCsv<SINFO>(CombineMessages("SINFO:", cmdResult.Messages));

            DiscRecord discRecord = new DiscRecord
            {
                Number = drive.DriveId,
                Id = $"disc:{drive.DriveId}",
                Type = cinfo.Find(MakeMKVCode.RecordType),
                DriveName = cinfo.Find(MakeMKVCode.DriveName),
                Title = cinfo.Find(MakeMKVCode.Title),
            };

            if (cinfo.TryGet(MakeMKVCode.Language, out string language))
                discRecord.Languages = new[] { language };

            var titles = tinfo.GroupBy(i => i.Id);
            var titles_sinfo = sinfo.GroupBy(i => i.TitleId);

            List<DiscTitleRecord> results = new List<DiscTitleRecord>();
            foreach(var title in titles)
            {
                FileSize.TryParse(title.Get(MakeMKVCode.FriendlyFileSize), out var friendlyFileSize);
                FileSize.TryParse(title.Get(MakeMKVCode.FileSize), out var fileSize);

                var title_sinfo = titles_sinfo.FirstOrDefault(si => si.Key == title.Key);

                results.Add(new DiscTitleRecord
                {
                    Id = title.Key.ToString(),
                    Index = title.Key,
                    Length = TimeSpan.Parse(title.Get(MakeMKVCode.TitleLength)),
                    FriendlyFileSize = friendlyFileSize,
                    RawFileSize = fileSize,
                    DiscRecord = discRecord,
                    Title = title.Find(MakeMKVCode.Title),
                    OriginalFileName = title.Find(MakeMKVCode.FileName),
                    VideoStreams = GetVideo(title_sinfo),
                    AudioStreams = GetAudio(title_sinfo),
                    SubtitleStreams = GetSubtitle(title_sinfo),
                });
            }

            return results
                .Where(r => r.Length >= options.MinimumLength && r.Length <= options.MaximumLength)
                .ToArray();
        }

        private IEnumerable<TitleVideoStreamRecord> GetVideo(IEnumerable<SINFO> info)
        {
            var streamIds = info
                .Where(i => i.Code == MakeMKVCode.RecordType && i.Value == "Video")
                .Select(i => i.Id)
                .Distinct()
                .ToArray();

            var streams = info.Where(i => streamIds.Contains(i.Id)).GroupBy(i => i.Id);
            foreach(var stream in streams)
            {
                ByteRate.TryParse(stream.Get(MakeMKVCode.BitRate), out var bitRate);
                
                string videoSize = stream.Find(MakeMKVCode.VideoSize);
                (int height, int width) = ParseVideoSize(videoSize);

                var record = new TitleVideoStreamRecord
                {
                    Id = stream.Key.ToString(),
                    BitRate = bitRate,
                    Encoding = stream.Find(MakeMKVCode.Encoding),
                    FriendlyEncodingName = stream.Find(MakeMKVCode.FriendlyEncodingName),
                    Height = height,
                    Width = width
                };

                if (stream.TryGet(MakeMKVCode.Language, out string language))
                    record.Language = language;

                yield return record;
            }
        }

        private IEnumerable<TitleAudioStreamRecord> GetAudio(IEnumerable<SINFO> info)
        {
            var streamIds = info
                .Where(i => i.Code == MakeMKVCode.RecordType && i.Value == "Audio")
                .Select(i => i.Id)
                .Distinct()
                .ToArray();

            var streams = info.Where(i => streamIds.Contains(i.Id)).GroupBy(i => i.Id);
            foreach(var stream in streams)
            {
                ByteRate.TryParse(stream.Get(MakeMKVCode.BitRate), out var bitRate);

                var record = new TitleAudioStreamRecord
                {
                    BitRate = bitRate,
                    AudioType = stream.Find(MakeMKVCode.AudioType),
                    Encoding = stream.Find(MakeMKVCode.Encoding),
                    FriendlyEncodingName = stream.Find(MakeMKVCode.FriendlyEncodingName),
                    Id = stream.Key.ToString(),
                };

                if (stream.TryGet(MakeMKVCode.Language, out string language))
                    record.Language = language;

                yield return record;
            }
        }

        private IEnumerable<TitleSubtitleStreamRecord> GetSubtitle(IEnumerable<SINFO> info)
        {
            var streamIds = info
                .Where(i => i.Code == MakeMKVCode.RecordType && i.Value == "Subtitles")
                .Select(i => i.Id)
                .Distinct()
                .ToArray();

            var streams = info.Where(i => streamIds.Contains(i.Id)).GroupBy(i => i.Id);
            foreach (var stream in streams)
            {
                ByteRate.TryParse(stream.Get(MakeMKVCode.BitRate), out var bitRate);

                var record = new TitleSubtitleStreamRecord
                {
                    BitRate = bitRate,
                    Encoding = stream.Find(MakeMKVCode.Encoding),
                    FriendlyEncodingName = stream.Find(MakeMKVCode.FriendlyEncodingName),
                    Id = stream.Key.ToString(),
                };

                if (stream.TryGet(MakeMKVCode.Language, out string language))
                    record.Language = language;

                yield return record;
            }
        }

        private (int, int) ParseVideoSize(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return (0, 0);

            try
            {
                int width = 0;
                int height = 0;

                if (value.Contains("x"))
                {
                    var split = value.Split('x');
                    width = int.Parse(split[0]);
                    height = int.Parse(split[1]);
                }

                return (height, width);
            } catch
            {
                return (0, 0);
            }
        }

        private IEnumerable<T> ParseCsv<T>(string csv)
        {
            using (var strReader = new StringReader(csv))
            using (var csvReader = new CsvReader(strReader, _CsvConfiguration))
                return csvReader.GetRecords<T>().ToArray();
        }

        private string CombineMessages(string prefix, IEnumerable<string> messages)
        {
            var found = messages
                .Where(msg => msg.StartsWith(prefix))
                .Select(msg => msg.Substring(prefix.Length))
                .ToArray();
            return string.Join("\n", found);
        }
    }

    internal interface IINFO
    {
        MakeMKVCode Code { get; set; }
        int SubCode { get; set; }
        string Value { get; set; }
    }

    internal class CINFO : IINFO
    {
        [Index(0)]
        public MakeMKVCode Code { get; set; }
        [Index(1)]
        public int SubCode { get; set; }
        [Index(2)]
        public string Value { get; set; }
    }

    internal class TINFO : IINFO
    {
        [Index(0)]
        public int Id { get; set; }
        [Index(1)]
        public MakeMKVCode Code { get; set; }
        [Index(2)]
        public int SubCode { get; set; }
        [Index(3)]
        public string Value { get; set; }
    }

    internal class SINFO : IINFO
    {
        [Index(0)]
        public int TitleId { get; set; }
        [Index(1)]
        public int Id { get; set; }
        [Index(2)]
        public MakeMKVCode Code { get; set; }
        [Index(3)]
        public int SubCode { get; set; }
        [Index(4)]
        public string Value { get; set; }
    }

    internal enum MakeMKVCode : int
    {
        RecordType = 1,
        Title = 2,
        AudioShortLanguage = 3,
        AudioLanguage = 4,
        Encoding = 5,
        FriendlyEncodingAbbr = 6,
        FriendlyEncodingName = 7,
        Unknown8 = 8,
        TitleLength = 9,
        FriendlyFileSize = 10,
        FileSize = 11,
        Unknown12 = 12,
        BitRate = 13,
        AudioChannels = 14,
        Unknown15 = 15,
        Unknown16 = 16,
        SamplingRate = 17,
        Unknown18 = 18,
        VideoSize = 19,
        AspectRatio = 20,
        FrameRate = 21,
        Unknown22 = 22,
        Unknown23 = 23,
        Unknown24 = 24,
        Unknown25 = 25,
        Unknown26 = 26,
        FileName = 27,
        ShortLanguage = 28,
        Language = 29,
        Summary = 30,
        TitleInformationHeader = 31,
        DriveName = 32,
        Unknown33 = 33,
        Unkonwn34 = 34,
        Unknown35 = 35,
        Unknown36 = 36,
        Unknown37 = 37,
        Unknown38 = 38,
        Unknown39 = 39,
        AudioType = 40,
        Unknown41 = 41,
        Unknown42 = 42,
    }

    internal static class INFOExtensions
    {
        public static string Get(this IEnumerable<IINFO> info, MakeMKVCode code)
            => info.First(i => i.Code == code).Value;

        public static bool TryGet(this IEnumerable<IINFO> info, MakeMKVCode code, out string value)
        {
            value = info.Find(code);
            return value != null;
        }

        public static string Find(this IEnumerable<IINFO> info, MakeMKVCode code)
            => info.FirstOrDefault(i => i.Code == code)?.Value;
    }
}
