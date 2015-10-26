@echo off
::
:: Script to remux a MP4/M4V Video File to a Injected FLV Video File
::
:: It's important to know taht your MP4/M4V video file HAS to be encoded
:: using H.264 for video and AAC for audio.
::

IF %1.==. GOTO empty

:: Input File:
SET INPUT=%1

:: Output
SET OUTPUT_FILE_NAME_NOT_INJECTED="%~d1%~p1%~n1.240pni.flv"
SET OUTPUT_FILE_NAME_INJECTED="%~d1%~p1%~n1.240p.flv"

:: Batch path
SET THIS_PATH=%~d0%~p0

:: Bin path
SET BIN_PATH=%THIS_PATH%bin\

:: FFmpeg command
echo Remuxing MP4 to FLV

%BIN_PATH%ffmpeg.exe -y -i %INPUT%  -vcodec copy -acodec copy %OUTPUT_FILE_NAME_NOT_INJECTED%

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
