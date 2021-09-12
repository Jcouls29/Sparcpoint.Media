using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Xunit.Categories;

namespace Sparcpoint.Media.Ripper.MakeMKV.Tests
{
    [Category("MakeMKV")]
    public class MakeMKVDriveQueryTests
    {
        [Fact]
        public async Task ProperlyParsesMakeMKVRobotSearch()
        {
            var executor = new StaticResultCommandLineExecutor(@"
MSG:1005,0,1,""MakeMKV v1.16.3 win(x86-release) started"",""%1 started"",""MakeMKV v1.16.3 win(x86-release)""
MSG:2003,0,3,""Error 'Scsi error - ILLEGAL REQUEST:READ OF SCRAMBLED SECTOR WITHOUT AUTHENTICATION' occurred while reading 'BD-RE HL-DT-ST BD-RE BP50NB40 1.03' at offset '1048576'"",""Error '%1' occurred while reading '%2' at offset '%3'"",""Scsi error - ILLEGAL REQUEST:READ OF SCRAMBLED SECTOR WITHOUT AUTHENTICATION"",""BD-RE HL-DT-ST BD-RE BP50NB40 1.03"",""1048576""
MSG:2003,0,3,""Error 'Scsi error - ILLEGAL REQUEST:READ OF SCRAMBLED SECTOR WITHOUT AUTHENTICATION' occurred while reading 'BD-RE HL-DT-ST BD-RE BP50NB40 1.03' at offset '1048576'"",""Error '%1' occurred while reading '%2' at offset '%3'"",""Scsi error - ILLEGAL REQUEST:READ OF SCRAMBLED SECTOR WITHOUT AUTHENTICATION"",""BD-RE HL-DT-ST BD-RE BP50NB40 1.03"",""1048576""
DRV:0,2,999,1,""BD-RE HL-DT-ST BD-RE BP50NB40 1.03"",""DVD_VIDEO"",""E:""
DRV:1,256,999,0,"""","""",""""
DRV:2,256,999,0,"""","""",""""
DRV:3,256,999,0,"""","""",""""
DRV:4,256,999,0,"""","""",""""
DRV:5,256,999,0,"""","""",""""
DRV:6,256,999,0,"""","""",""""
DRV:7,256,999,0,"""","""",""""
DRV:8,256,999,0,"""","""",""""
DRV:9,256,999,0,"""","""",""""
DRV:10,256,999,0,"""","""",""""
DRV:11,256,999,0,"""","""",""""
DRV:12,256,999,0,"""","""",""""
DRV:13,256,999,0,"""","""",""""
DRV:14,256,999,0,"""","""",""""
DRV:15,256,999,0,"""","""",""""
MSG:5010,0,0,""Failed to open disc"",""Failed to open disc""
TCOUNT:0
");
            var query = new MakeMKVDriveQuery(executor);
            var drives = await query.QueryAllDrivesAsync();
            Assert.NotEmpty(drives);
            Assert.Single(drives);

            var drive0 = drives.First();

            Assert.Equal(0, drive0.DriveId);
            Assert.Equal("BD-RE HL-DT-ST BD-RE BP50NB40 1.03", drive0.DeviceName);
            Assert.Equal("DVD_VIDEO", drive0.DiscName);
            Assert.Equal("E:", drive0.DriveName);
        }
    }
}
