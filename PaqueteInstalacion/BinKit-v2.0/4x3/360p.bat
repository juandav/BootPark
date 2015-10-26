@echo off
::
:: Encoding settings for 360p
:: You can change any VALUE below this line:
::


:: Video Size - Dimensions divisible by 16
:: Use 640x368 for widescreen video and 480x352 for standard 4:3 video
SET SIZE=480x352

:: Image Aspect Ratio
:: use 16:9 for widescreen video and 4:3 for standar video
SET ASPECT=4:3

:: Frame Rate - for 29.97 use 30000/1001 and for 23.976 use 24000/1001
SET FRAME_RATE=30000/1001

:: Keyframe every # frames, recomended use the same frame rate value but as an integer
SET KEYFRAME=30

:: Bitrate in kilobites per second, NOT kilobytes, 1 kilobyte = 8 kilobits
SET BITRATE=768

:: Max bit rate
SET MAXRATE=1024

:: Audio channels, use 1 for mono and 2 for stereo
SET AUDIO_CHANNELS=2

:: Audio rate, Flash can handle up to 44100
SET AUDIO_RATE=44100

:: Audio bitrate in kilobites
SET AUDIO_BITRATE=128










:: ----------------------------------------------------------------------------
:: WARNING!
:: DONT TOUCH OR CHANGE NOTHING BELOW THIS LINE!
:: ----------------------------------------------------------------------------

IF %1.==. GOTO empty

:: Input File:
SET INPUT=%1

:: Output
SET OUTPUT_FILE_NAME_NOT_INJECTED="%~d1%~p1%~n1.360pni.flv"
SET OUTPUT_FILE_NAME_INJECTED="%~d1%~p1%~n1.360p.flv"

:: Buffer size
SET /a BUFSIZE=%MAXRATE%*2

:: Batch path
SET THIS_PATH=%~d0%~p0..\

:: Bin path
SET BIN_PATH=%THIS_PATH%bin\

:: FFmpeg command
echo First Pass Encoding.
%BIN_PATH%ffmpeg.exe -y -i %INPUT% -threads 0 -s %SIZE% -aspect %ASPECT% -r %FRAME_RATE% -b %BITRATE%k -maxrate %MAXRATE%k -bufsize %BUFSIZE%k -vcodec libx264 -g %KEYFRAME% -pass 1 -vpre %BIN_PATH%fastfirstpass.ffpreset -an -f rawvideo NUL
echo Second Pass Encoding.
%BIN_PATH%ffmpeg.exe -y -i %INPUT% -threads 0 -s %SIZE% -aspect %ASPECT% -r %FRAME_RATE% -b %BITRATE%k -maxrate %MAXRATE%k -bufsize %BUFSIZE%k -vcodec libx264 -g %KEYFRAME% -pass 2 -vpre %BIN_PATH%hq.ffpreset -acodec libfaac -ac %AUDIO_CHANNELS% -ar %AUDIO_RATE% -ab %AUDIO_BITRATE%k %OUTPUT_FILE_NAME_NOT_INJECTED%

:: Remove the logs
echo Removing pass logs.
del ffmpeg2pass-0.log
del x264_2pass.log

:: inject the FLV video with metadata
%BIN_PATH%flvmeta.exe %OUTPUT_FILE_NAME_NOT_INJECTED% %OUTPUT_FILE_NAME_INJECTED% && del %OUTPUT_FILE_NAME_NOT_INJECTED%
echo Creation of %OUTPUT_FILE_NAME_INJECTED% Done!

GOTO end
:empty
echo.
echo  ERROR!
echo  No input file specified.
echo  You can set an input by draggin a video file to this script or by calling 
echo  the script on the command prompt and setting the full file path as 
echo  the first param.
echo.
pause
:end
