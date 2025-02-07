@echo off
if '%runtime%' == '' set runtime=linux-arm64
pushd ..\source
dotnet publish %1 -c Release -r %runtime% --self-contained -o ..\artifacts\Linux  /p:DebugType=None /p:DebugSymbols=false
popd