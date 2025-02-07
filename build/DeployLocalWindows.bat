@echo off
pushd ..\source
dotnet publish RaspiRobot.Lights.Server -c Debug -r win-x64 --self-contained -o ..\artifacts\Windows  /p:DebugType=None /p:DebugSymbols=false
dotnet publish RaspiRobot.Lights.UI -c Debug -r win-x64 --self-contained -o ..\artifacts\Windows  /p:DebugType=None /p:DebugSymbols=false
popd
pause