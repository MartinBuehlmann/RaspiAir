@echo off
if '%runtime%' == '' set runtime=win-x64
pushd ..\source
dotnet publish %1 -c Debug -r %runtime% --self-contained -o ..\artifacts\Windows  /p:DebugType=None /p:DebugSymbols=false
popd