using System;
using Xunit;
using Xunit.Categories;

namespace Sparcpoint.Media.Tests
{
    [Category("BitRate")]
    public class BitRateTests
    {
        [Theory(DisplayName = "Passes with Different Formats")]
        [InlineData("9.8 MB/s", 10276045)]
        [InlineData("192 KB/s", 196608)]
        [InlineData("9.8 b/s", 10)]
        [InlineData("192K", 196608)]
        [InlineData("9.8Mb/s", 10276045)]
        [InlineData("50", 50)]
        public void SuccessfulParseScenarios(string text, long value)
        {
            Assert.True(ByteRate.TryParse(text, out ByteRate result));
            Assert.Equal(value, result.BytesPerSecond);
        }

        [Theory(DisplayName = "Fails on Extra Data")]
        [InlineData("9.8MB/s+")]
        [InlineData("a9.8MB/s")]
        [InlineData("9.8 MBC/s")]
        [InlineData("9.8E3 MB/s+")]
        public void FailsWithExtraData(string text)
        {
            Assert.False(ByteRate.TryParse(text, out ByteRate result));
        }
    }

    [Category("FileSize")]
    public class FileSizeTests
    {
        [Theory(DisplayName = "Passes with Different Formats")]
        [InlineData("9.8 MB", 10276045)]
        [InlineData("192 KB", 196608)]
        [InlineData("9.8 b", 10)]
        [InlineData("192K", 196608)]
        [InlineData("9.8Mb", 10276045)]
        [InlineData("50", 50)]
        public void SuccessfulParseScenarios(string text, long value)
        {
            Assert.True(FileSize.TryParse(text, out FileSize result));
            Assert.Equal(value, result.Bytes);
        }

        [Theory(DisplayName = "Fails on Extra Data")]
        [InlineData("9.8MB+")]
        [InlineData("a9.8MB")]
        [InlineData("9.8 MBC")]
        [InlineData("9.8E3 MB+")]
        public void FailsWithExtraData(string text)
        {
            Assert.False(FileSize.TryParse(text, out FileSize result));
        }
    }
}
