@echo off
call DeployWindows-RaspiAir.bat
pushd ..\artifacts\Windows
RaspiAir.exe
popd
pause