@echo off

java -cp TestyIO.jar Tests.Main

if %ERRORLEVEL% == 0 (
echo Testy przeszly pomyslnie
) else (
echo Testy zakonczone niepowodzeniem
)

pause