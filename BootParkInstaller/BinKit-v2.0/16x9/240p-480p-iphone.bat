@echo off

IF %1.==. GOTO empty

:: Batch path
SET THIS_BPATH=%~d0%~p0

call %THIS_BPATH%240p.bat %1 
call %THIS_BPATH%480p.bat %1
call %THIS_BPATH%iPhone.bat %1

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
