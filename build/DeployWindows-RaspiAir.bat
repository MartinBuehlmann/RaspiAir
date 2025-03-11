@echo off
rd ..\artifacts\Windows /s /q
call DeployLocalWindows.bat "RaspiAir"
call DeployLocalWindows.bat "RaspiAir.Web.UI"
pause