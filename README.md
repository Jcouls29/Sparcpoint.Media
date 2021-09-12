# Sparcpoint Media

A media-based library and CLI for archiving various file formats 
(like DVD Video, BluRay, and Music)

> Note: Currently only supports MakeMKV

## Installation

1. Download and Install [MakeMKV](http://makemkv.com/)
2. Clone Repository
3. Build `Sparcpoint.Media.Ripper.CLI`

## Usage

```bash
ripper <options>

Options
-------
  -e, --exe            (Default: C:\Program Files (x86)\MakeMKV\makemkvcon.exe) Path to the MKV Command Line EXE
  -m, --mode           Required. (Default: Standard) Naming convention for the output titles
  -t, --title          (Default: Title) Title to use during naming
  -o, --output         Required. Output folder to store ripped files.

  --season             (Default: 0) Season number for the current rip session. Only used when a TV mode is selected.
  -d, --disc-number    (Default: 1) The starting disc number.
  --episode            (Default: 1) The starting episode number for this session.
  --min-length         (Default: 0) Minimum title length to record. Typically used with custom conventions
  --max-length         (Default: 2147483647) Maximum title length to record. Typically used with custom conventions

  --google-username    Google Username to send notifications.
  --google-password    Google Password to send notifications.
  --mobile             Mobile number for MMS text sending.
  --mobile-provider    (Default: Verizon) Mobile provider for MMS text sending.

  --help               Display this help screen.
  --version            Display version information.
```

### Mode
There are several modes that can be used with the ripper. 
If ripping to Plex use `PlexTV_20min` or `PlexTV_45min`. If a custom
time frame is required, let's use `PlexTV_Custom`.

| Mode | Min Length (minutes) | Max Length (minutes) | File Name Convention |
| ---- | -------------------- | -------------------- | -------------------- |
| Standard | 0.5 | N/A | `$"{options.Title} - {{TitleIndex}}.mkv"` |
| Movie | 90 | N/A | **Feature:** `$"{options.Title} - {{TitleIndex}}.mkv"`<br>**Extras:** `"Extras-{TitleIndex}.mkv"` |
| PlexTV_20min | 19 | 23 | **Episodes:** `$"{options.Title} - s{seasonNumber}e{episodeNumber}.mkv"`<br>**Extras:** `$"{options.Title} - Extras {sessionIndex}"` |
| PlexTV_45min | 42 | 48 | **Episodes:** `$"{options.Title} - s{seasonNumber}e{episodeNumber}.mkv"`<br>**Extras:** `$"{options.Title} - Extras {sessionIndex}"` |
| PlexTV_Custom | `--min-length` | `--max-length` | **Episodes:** `$"{options.Title} - s{seasonNumber}e{episodeNumber}.mkv"`<br>**Extras:** `$"{options.Title} - Extras {sessionIndex}"` |

### Example

```bash
-- TV Show
ripper -m PlexTV_45min -o "C:\Temp\Videos" -t "Psych" --season 7

-- Movie
ripper -m Movie -o "C:\Temp\Videos\Avengers" -t "Avengers"

-- Custom Length
ripper -m Custom -o "C:\Temp\Videos" -t "Home Movies" --min-length 95 --max-length 125
```
