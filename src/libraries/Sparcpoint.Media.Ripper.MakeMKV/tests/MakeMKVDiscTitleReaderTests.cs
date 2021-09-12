﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Xunit.Categories;

namespace Sparcpoint.Media.Ripper.MakeMKV.Tests
{
    [Category("MakeMKV")]
    public class MakeMKVDiscTitleReaderTests
    {
        private const string SAMPLE1 = @"
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
MSG:2003,0,3,""Error 'Scsi error - ILLEGAL REQUEST:READ OF SCRAMBLED SECTOR WITHOUT AUTHENTICATION' occurred while reading 'BD-RE HL-DT-ST BD-RE BP50NB40 1.03' at offset '1048576'"",""Error '%1' occurred while reading '%2' at offset '%3'"",""Scsi error - ILLEGAL REQUEST:READ OF SCRAMBLED SECTOR WITHOUT AUTHENTICATION"",""BD-RE HL-DT-ST BD-RE BP50NB40 1.03"",""1048576""
MSG:3007,0,0,""Using direct disc access mode"",""Using direct disc access mode""
MSG:3024,4096,2,""Complex multiplex encountered - 6 cells and 5419 VOBUs have to be scanned. This may take some time, please be patient - it can't be avoided."",""Complex multiplex encountered - %1 cells and %2 VOBUs have to be scanned. This may take some time, please be patient - it can't be avoided."",""6"",""5419""
MSG:3028,0,3,""Title #1 was added (6 cell(s), 0:45:08)"",""Title #%1 was added (%2 cell(s), %3)"",""1"",""6"",""0:45:08""
MSG:3024,4096,2,""Complex multiplex encountered - 6 cells and 5326 VOBUs have to be scanned. This may take some time, please be patient - it can't be avoided."",""Complex multiplex encountered - %1 cells and %2 VOBUs have to be scanned. This may take some time, please be patient - it can't be avoided."",""6"",""5326""
MSG:3028,0,3,""Title #2 was added (6 cell(s), 0:44:22)"",""Title #%1 was added (%2 cell(s), %3)"",""2"",""6"",""0:44:22""
MSG:3024,4096,2,""Complex multiplex encountered - 6 cells and 5327 VOBUs have to be scanned. This may take some time, please be patient - it can't be avoided."",""Complex multiplex encountered - %1 cells and %2 VOBUs have to be scanned. This may take some time, please be patient - it can't be avoided."",""6"",""5327""
MSG:3028,0,3,""Title #3 was added (6 cell(s), 0:44:22)"",""Title #%1 was added (%2 cell(s), %3)"",""3"",""6"",""0:44:22""
MSG:3024,4096,2,""Complex multiplex encountered - 6 cells and 5336 VOBUs have to be scanned. This may take some time, please be patient - it can't be avoided."",""Complex multiplex encountered - %1 cells and %2 VOBUs have to be scanned. This may take some time, please be patient - it can't be avoided."",""6"",""5336""
MSG:3028,0,3,""Title #4 was added (6 cell(s), 0:44:26)"",""Title #%1 was added (%2 cell(s), %3)"",""4"",""6"",""0:44:26""
MSG:5011,0,0,""Operation successfully completed"",""Operation successfully completed""
TCOUNT:4
CINFO:1,6206,""DVD disc""
CINFO:2,0,""CSI: Season 1, Disc 1""
CINFO:28,0,""eng""
CINFO:29,0,""English""
CINFO:30,0,""CSI: Season 1, Disc 1""
CINFO:31,6119,""<b>Source information</b><br>""
CINFO:32,0,""DVD_VIDEO""
CINFO:33,0,""0""
TINFO:0,2,0,""CSI: Season 1, Disc 1""
TINFO:0,8,0,""6""
TINFO:0,9,0,""0:45:08""
TINFO:0,10,0,""1.8 GB""
TINFO:0,11,0,""1992210432""
TINFO:0,24,0,""01""
TINFO:0,25,0,""1""
TINFO:0,26,0,""1-6""
TINFO:0,27,0,""CSI- Season 1, Disc 1_t00.mkv""
TINFO:0,28,0,""eng""
TINFO:0,29,0,""English""
TINFO:0,30,0,""CSI: Season 1, Disc 1 - 6 chapter(s) , 1.8 GB""
TINFO:0,31,6120,""<b>Title information</b><br>""
TINFO:0,33,0,""0""
SINFO:0,0,1,6201,""Video""
SINFO:0,0,5,0,""V_MPEG2""
SINFO:0,0,6,0,""Mpeg2""
SINFO:0,0,7,0,""Mpeg2""
SINFO:0,0,13,0,""9.8 Mb/s""
SINFO:0,0,19,0,""720x480""
SINFO:0,0,20,0,""4:3""
SINFO:0,0,21,0,""29.97 (30000/1001)""
SINFO:0,0,22,0,""0""
SINFO:0,0,28,0,""eng""
SINFO:0,0,29,0,""English""
SINFO:0,0,30,0,""Mpeg2""
SINFO:0,0,31,6121,""<b>Track information</b><br>""
SINFO:0,0,33,0,""0""
SINFO:0,0,38,0,""""
SINFO:0,0,42,5088,""( Lossless conversion )""
SINFO:0,1,1,6202,""Audio""
SINFO:0,1,2,5091,""Stereo""
SINFO:0,1,3,0,""eng""
SINFO:0,1,4,0,""English""
SINFO:0,1,5,0,""A_AC3""
SINFO:0,1,6,0,""DD""
SINFO:0,1,7,0,""Dolby Digital""
SINFO:0,1,13,0,""192 Kb/s""
SINFO:0,1,14,0,""2""
SINFO:0,1,17,0,""48000""
SINFO:0,1,22,0,""0""
SINFO:0,1,28,0,""eng""
SINFO:0,1,29,0,""English""
SINFO:0,1,30,0,""DD Stereo English""
SINFO:0,1,31,6121,""<b>Track information</b><br>""
SINFO:0,1,33,0,""90""
SINFO:0,1,38,0,""d""
SINFO:0,1,39,0,""Default""
SINFO:0,1,40,0,""stereo""
SINFO:0,1,42,5088,""( Lossless conversion )""
SINFO:0,2,1,6202,""Audio""
SINFO:0,2,2,5091,""Stereo""
SINFO:0,2,3,0,""spa""
SINFO:0,2,4,0,""Spanish""
SINFO:0,2,5,0,""A_AC3""
SINFO:0,2,6,0,""DD""
SINFO:0,2,7,0,""Dolby Digital""
SINFO:0,2,13,0,""192 Kb/s""
SINFO:0,2,14,0,""2""
SINFO:0,2,17,0,""48000""
SINFO:0,2,22,0,""0""
SINFO:0,2,28,0,""eng""
SINFO:0,2,29,0,""English""
SINFO:0,2,30,0,""DD Stereo Spanish""
SINFO:0,2,31,6121,""<b>Track information</b><br>""
SINFO:0,2,33,0,""100""
SINFO:0,2,38,0,""""
SINFO:0,2,40,0,""stereo""
SINFO:0,2,42,5088,""( Lossless conversion )""
SINFO:0,3,1,6203,""Subtitles""
SINFO:0,3,3,0,""eng""
SINFO:0,3,4,0,""English""
SINFO:0,3,5,0,""S_CC608/DVD""
SINFO:0,3,6,0,""CC""
SINFO:0,3,7,0,""Closed Captions""
SINFO:0,3,13,0,""9.8 Mb/s""
SINFO:0,3,22,0,""0""
SINFO:0,3,28,0,""eng""
SINFO:0,3,29,0,""English""
SINFO:0,3,30,0,""CCΓåÆText English ( Lossy conversion )""
SINFO:0,3,31,6121,""<b>Track information</b><br>""
SINFO:0,3,33,0,""90""
SINFO:0,3,34,0,""Text subtitles ( Lossy conversion )""
SINFO:0,3,38,0,""d""
SINFO:0,3,39,0,""Default""
SINFO:0,3,41,0,""Text""
SINFO:0,3,42,5087,""( Lossy conversion )""
TINFO:1,2,0,""CSI: Season 1, Disc 1""
TINFO:1,8,0,""6""
TINFO:1,9,0,""0:44:22""
TINFO:1,10,0,""1.8 GB""
TINFO:1,11,0,""1957296128""
TINFO:1,24,0,""02""
TINFO:1,25,0,""1""
TINFO:1,26,0,""1-6""
TINFO:1,27,0,""CSI- Season 1, Disc 1_t01.mkv""
TINFO:1,28,0,""eng""
TINFO:1,29,0,""English""
TINFO:1,30,0,""CSI: Season 1, Disc 1 - 6 chapter(s) , 1.8 GB""
TINFO:1,31,6120,""<b>Title information</b><br>""
TINFO:1,33,0,""0""
SINFO:1,0,1,6201,""Video""
SINFO:1,0,5,0,""V_MPEG2""
SINFO:1,0,6,0,""Mpeg2""
SINFO:1,0,7,0,""Mpeg2""
SINFO:1,0,13,0,""9.8 Mb/s""
SINFO:1,0,19,0,""720x480""
SINFO:1,0,20,0,""4:3""
SINFO:1,0,21,0,""29.97 (30000/1001)""
SINFO:1,0,22,0,""0""
SINFO:1,0,28,0,""eng""
SINFO:1,0,29,0,""English""
SINFO:1,0,30,0,""Mpeg2""
SINFO:1,0,31,6121,""<b>Track information</b><br>""
SINFO:1,0,33,0,""0""
SINFO:1,0,38,0,""""
SINFO:1,0,42,5088,""( Lossless conversion )""
SINFO:1,1,1,6202,""Audio""
SINFO:1,1,2,5091,""Stereo""
SINFO:1,1,3,0,""eng""
SINFO:1,1,4,0,""English""
SINFO:1,1,5,0,""A_AC3""
SINFO:1,1,6,0,""DD""
SINFO:1,1,7,0,""Dolby Digital""
SINFO:1,1,13,0,""192 Kb/s""
SINFO:1,1,14,0,""2""
SINFO:1,1,17,0,""48000""
SINFO:1,1,22,0,""0""
SINFO:1,1,28,0,""eng""
SINFO:1,1,29,0,""English""
SINFO:1,1,30,0,""DD Stereo English""
SINFO:1,1,31,6121,""<b>Track information</b><br>""
SINFO:1,1,33,0,""90""
SINFO:1,1,38,0,""d""
SINFO:1,1,39,0,""Default""
SINFO:1,1,40,0,""stereo""
SINFO:1,1,42,5088,""( Lossless conversion )""
SINFO:1,2,1,6202,""Audio""
SINFO:1,2,2,5091,""Stereo""
SINFO:1,2,3,0,""spa""
SINFO:1,2,4,0,""Spanish""
SINFO:1,2,5,0,""A_AC3""
SINFO:1,2,6,0,""DD""
SINFO:1,2,7,0,""Dolby Digital""
SINFO:1,2,13,0,""192 Kb/s""
SINFO:1,2,14,0,""2""
SINFO:1,2,17,0,""48000""
SINFO:1,2,22,0,""0""
SINFO:1,2,28,0,""eng""
SINFO:1,2,29,0,""English""
SINFO:1,2,30,0,""DD Stereo Spanish""
SINFO:1,2,31,6121,""<b>Track information</b><br>""
SINFO:1,2,33,0,""100""
SINFO:1,2,38,0,""""
SINFO:1,2,40,0,""stereo""
SINFO:1,2,42,5088,""( Lossless conversion )""
SINFO:1,3,1,6203,""Subtitles""
SINFO:1,3,3,0,""eng""
SINFO:1,3,4,0,""English""
SINFO:1,3,5,0,""S_CC608/DVD""
SINFO:1,3,6,0,""CC""
SINFO:1,3,7,0,""Closed Captions""
SINFO:1,3,13,0,""9.8 Mb/s""
SINFO:1,3,22,0,""0""
SINFO:1,3,28,0,""eng""
SINFO:1,3,29,0,""English""
SINFO:1,3,30,0,""CCΓåÆText English ( Lossy conversion )""
SINFO:1,3,31,6121,""<b>Track information</b><br>""
SINFO:1,3,33,0,""90""
SINFO:1,3,34,0,""Text subtitles ( Lossy conversion )""
SINFO:1,3,38,0,""d""
SINFO:1,3,39,0,""Default""
SINFO:1,3,41,0,""Text""
SINFO:1,3,42,5087,""( Lossy conversion )""
TINFO:2,2,0,""CSI: Season 1, Disc 1""
TINFO:2,8,0,""6""
TINFO:2,9,0,""0:44:22""
TINFO:2,10,0,""1.8 GB""
TINFO:2,11,0,""2025877504""
TINFO:2,24,0,""03""
TINFO:2,25,0,""1""
TINFO:2,26,0,""1-6""
TINFO:2,27,0,""CSI- Season 1, Disc 1_t02.mkv""
TINFO:2,28,0,""eng""
TINFO:2,29,0,""English""
TINFO:2,30,0,""CSI: Season 1, Disc 1 - 6 chapter(s) , 1.8 GB""
TINFO:2,31,6120,""<b>Title information</b><br>""
TINFO:2,33,0,""0""
SINFO:2,0,1,6201,""Video""
SINFO:2,0,5,0,""V_MPEG2""
SINFO:2,0,6,0,""Mpeg2""
SINFO:2,0,7,0,""Mpeg2""
SINFO:2,0,13,0,""9.8 Mb/s""
SINFO:2,0,19,0,""720x480""
SINFO:2,0,20,0,""4:3""
SINFO:2,0,21,0,""29.97 (30000/1001)""
SINFO:2,0,22,0,""0""
SINFO:2,0,28,0,""eng""
SINFO:2,0,29,0,""English""
SINFO:2,0,30,0,""Mpeg2""
SINFO:2,0,31,6121,""<b>Track information</b><br>""
SINFO:2,0,33,0,""0""
SINFO:2,0,38,0,""""
SINFO:2,0,42,5088,""( Lossless conversion )""
SINFO:2,1,1,6202,""Audio""
SINFO:2,1,2,5091,""Stereo""
SINFO:2,1,3,0,""eng""
SINFO:2,1,4,0,""English""
SINFO:2,1,5,0,""A_AC3""
SINFO:2,1,6,0,""DD""
SINFO:2,1,7,0,""Dolby Digital""
SINFO:2,1,13,0,""192 Kb/s""
SINFO:2,1,14,0,""2""
SINFO:2,1,17,0,""48000""
SINFO:2,1,22,0,""0""
SINFO:2,1,28,0,""eng""
SINFO:2,1,29,0,""English""
SINFO:2,1,30,0,""DD Stereo English""
SINFO:2,1,31,6121,""<b>Track information</b><br>""
SINFO:2,1,33,0,""90""
SINFO:2,1,38,0,""d""
SINFO:2,1,39,0,""Default""
SINFO:2,1,40,0,""stereo""
SINFO:2,1,42,5088,""( Lossless conversion )""
SINFO:2,2,1,6202,""Audio""
SINFO:2,2,2,5091,""Stereo""
SINFO:2,2,3,0,""spa""
SINFO:2,2,4,0,""Spanish""
SINFO:2,2,5,0,""A_AC3""
SINFO:2,2,6,0,""DD""
SINFO:2,2,7,0,""Dolby Digital""
SINFO:2,2,13,0,""192 Kb/s""
SINFO:2,2,14,0,""2""
SINFO:2,2,17,0,""48000""
SINFO:2,2,22,0,""0""
SINFO:2,2,28,0,""eng""
SINFO:2,2,29,0,""English""
SINFO:2,2,30,0,""DD Stereo Spanish""
SINFO:2,2,31,6121,""<b>Track information</b><br>""
SINFO:2,2,33,0,""100""
SINFO:2,2,38,0,""""
SINFO:2,2,40,0,""stereo""
SINFO:2,2,42,5088,""( Lossless conversion )""
SINFO:2,3,1,6203,""Subtitles""
SINFO:2,3,3,0,""eng""
SINFO:2,3,4,0,""English""
SINFO:2,3,5,0,""S_CC608/DVD""
SINFO:2,3,6,0,""CC""
SINFO:2,3,7,0,""Closed Captions""
SINFO:2,3,13,0,""9.8 Mb/s""
SINFO:2,3,22,0,""0""
SINFO:2,3,28,0,""eng""
SINFO:2,3,29,0,""English""
SINFO:2,3,30,0,""CCΓåÆText English ( Lossy conversion )""
SINFO:2,3,31,6121,""<b>Track information</b><br>""
SINFO:2,3,33,0,""90""
SINFO:2,3,34,0,""Text subtitles ( Lossy conversion )""
SINFO:2,3,38,0,""d""
SINFO:2,3,39,0,""Default""
SINFO:2,3,41,0,""Text""
SINFO:2,3,42,5087,""( Lossy conversion )""
TINFO:3,2,0,""CSI: Season 1, Disc 1""
TINFO:3,8,0,""6""
TINFO:3,9,0,""0:44:26""
TINFO:3,10,0,""1.8 GB""
TINFO:3,11,0,""2022735872""
TINFO:3,24,0,""04""
TINFO:3,25,0,""1""
TINFO:3,26,0,""1-6""
TINFO:3,27,0,""CSI- Season 1, Disc 1_t03.mkv""
TINFO:3,28,0,""eng""
TINFO:3,29,0,""English""
TINFO:3,30,0,""CSI: Season 1, Disc 1 - 6 chapter(s) , 1.8 GB""
TINFO:3,31,6120,""<b>Title information</b><br>""
TINFO:3,33,0,""0""
SINFO:3,0,1,6201,""Video""
SINFO:3,0,5,0,""V_MPEG2""
SINFO:3,0,6,0,""Mpeg2""
SINFO:3,0,7,0,""Mpeg2""
SINFO:3,0,13,0,""9.8 Mb/s""
SINFO:3,0,19,0,""720x480""
SINFO:3,0,20,0,""4:3""
SINFO:3,0,21,0,""29.97 (30000/1001)""
SINFO:3,0,22,0,""0""
SINFO:3,0,28,0,""eng""
SINFO:3,0,29,0,""English""
SINFO:3,0,30,0,""Mpeg2""
SINFO:3,0,31,6121,""<b>Track information</b><br>""
SINFO:3,0,33,0,""0""
SINFO:3,0,38,0,""""
SINFO:3,0,42,5088,""( Lossless conversion )""
SINFO:3,1,1,6202,""Audio""
SINFO:3,1,2,5091,""Stereo""
SINFO:3,1,3,0,""eng""
SINFO:3,1,4,0,""English""
SINFO:3,1,5,0,""A_AC3""
SINFO:3,1,6,0,""DD""
SINFO:3,1,7,0,""Dolby Digital""
SINFO:3,1,13,0,""192 Kb/s""
SINFO:3,1,14,0,""2""
SINFO:3,1,17,0,""48000""
SINFO:3,1,22,0,""0""
SINFO:3,1,28,0,""eng""
SINFO:3,1,29,0,""English""
SINFO:3,1,30,0,""DD Stereo English""
SINFO:3,1,31,6121,""<b>Track information</b><br>""
SINFO:3,1,33,0,""90""
SINFO:3,1,38,0,""d""
SINFO:3,1,39,0,""Default""
SINFO:3,1,40,0,""stereo""
SINFO:3,1,42,5088,""( Lossless conversion )""
SINFO:3,2,1,6202,""Audio""
SINFO:3,2,2,5091,""Stereo""
SINFO:3,2,3,0,""spa""
SINFO:3,2,4,0,""Spanish""
SINFO:3,2,5,0,""A_AC3""
SINFO:3,2,6,0,""DD""
SINFO:3,2,7,0,""Dolby Digital""
SINFO:3,2,13,0,""192 Kb/s""
SINFO:3,2,14,0,""2""
SINFO:3,2,17,0,""48000""
SINFO:3,2,22,0,""0""
SINFO:3,2,28,0,""eng""
SINFO:3,2,29,0,""English""
SINFO:3,2,30,0,""DD Stereo Spanish""
SINFO:3,2,31,6121,""<b>Track information</b><br>""
SINFO:3,2,33,0,""100""
SINFO:3,2,38,0,""""
SINFO:3,2,40,0,""stereo""
SINFO:3,2,42,5088,""( Lossless conversion )""
SINFO:3,3,1,6203,""Subtitles""
SINFO:3,3,3,0,""eng""
SINFO:3,3,4,0,""English""
SINFO:3,3,5,0,""S_CC608/DVD""
SINFO:3,3,6,0,""CC""
SINFO:3,3,7,0,""Closed Captions""
SINFO:3,3,13,0,""9.8 Mb/s""
SINFO:3,3,22,0,""0""
SINFO:3,3,28,0,""eng""
SINFO:3,3,29,0,""English""
SINFO:3,3,30,0,""CCΓåÆText English ( Lossy conversion )""
SINFO:3,3,31,6121,""<b>Track information</b><br>""
SINFO:3,3,33,0,""90""
SINFO:3,3,34,0,""Text subtitles ( Lossy conversion )""
SINFO:3,3,38,0,""d""
SINFO:3,3,39,0,""Default""
SINFO:3,3,41,0,""Text""
SINFO:3,3,42,5087,""( Lossy conversion )""
";

        [Fact]
        public async Task ProperlyParsesMakeMKVRobotRead()
        {
            var executor = new StaticResultCommandLineExecutor(SAMPLE1);
            var reader = new MakeMKVDiscTitleReader(executor);
            var actuals = (await reader.ReadTitleMetadata(
                new DriveInformation { DriveId = 0 },
                ReadTitleMetadataOptions
                    .WithMinimumLength(TimeSpan.FromMinutes(30))
                    .WithMaximumLength(TimeSpan.FromMinutes(60))
            )).ToArray();

            Assert.NotEmpty(actuals);
            Assert.Equal(4, actuals.Count());

            DiscTitleRecord[] expected = new[]
            {
                new DiscTitleRecord
                {
                    FriendlyFileSize = FileSize.Parse("1.8 GB"),
                    Id = "0",
                    RawFileSize = 1992210432,
                    Length = TimeSpan.Parse("0:45:08"),
                    OriginalFileName = "CSI- Season 1, Disc 1_t00.mkv",
                    Title = "CSI: Season 1, Disc 1"
                },
                new DiscTitleRecord
                {
                    FriendlyFileSize = FileSize.Parse("1.8 GB"),
                    Id = "1",
                    RawFileSize = 1957296128,
                    Length = TimeSpan.Parse("0:44:22"),
                    OriginalFileName = "CSI- Season 1, Disc 1_t01.mkv",
                    Title = "CSI: Season 1, Disc 1"
                }, new DiscTitleRecord
                {
                    FriendlyFileSize = FileSize.Parse("1.8 GB"),
                    Id = "2",
                    RawFileSize = 2025877504,
                    Length = TimeSpan.Parse("0:44:22"),
                    OriginalFileName = "CSI- Season 1, Disc 1_t02.mkv",
                    Title = "CSI: Season 1, Disc 1"
                }, new DiscTitleRecord
                {
                    FriendlyFileSize = FileSize.Parse("1.8 GB"),
                    Id = "3",
                    RawFileSize = 2022735872,
                    Length = TimeSpan.Parse("0:44:26"),
                    OriginalFileName = "CSI- Season 1, Disc 1_t03.mkv",
                    Title = "CSI: Season 1, Disc 1"
                }
            };

            for(int i = 0; i < 4; i++)
            {
                AssertEqual(expected[i], actuals[i]);
            }
        }

        [Fact]
        public async Task MaxLengthSetTooLow_NoTitlesReturned()
        {
            var executor = new StaticResultCommandLineExecutor(SAMPLE1);
            var reader = new MakeMKVDiscTitleReader(executor);
            var actuals = (await reader.ReadTitleMetadata(
                new DriveInformation { DriveId = 0 }, 
                ReadTitleMetadataOptions.WithMaximumLength(TimeSpan.FromMinutes(30))
            )).ToArray();

            Assert.Empty(actuals);
        }

        [Fact]
        public async Task MinLengthSetTooHigh_NoTitlesReturned()
        {
            var executor = new StaticResultCommandLineExecutor(SAMPLE1);
            var reader = new MakeMKVDiscTitleReader(executor);
            var actuals = (await reader.ReadTitleMetadata(
                new DriveInformation { DriveId = 0 },
                ReadTitleMetadataOptions.WithMinimumLength(TimeSpan.FromMinutes(60))
            )).ToArray();

            Assert.Empty(actuals);
        }

        private void AssertEqual(DiscTitleRecord expected, DiscTitleRecord actual)
        {
            Assert.NotNull(actual);
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.FriendlyFileSize, actual.FriendlyFileSize);
            Assert.Equal(expected.Length, actual.Length);
            Assert.Equal(expected.OriginalFileName, actual.OriginalFileName);
            Assert.Equal(expected.RawFileSize, actual.RawFileSize);
            Assert.Equal(expected.Title, actual.Title);
        }
    }
}
