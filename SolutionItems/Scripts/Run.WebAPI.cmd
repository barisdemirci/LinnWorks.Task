@echo off
set PROJPATH=%~dp0\..\..\src
cd %PROJPATH%\Api\LinnWorks.Task.WebApi
start "WebAPI - http://localhost:5001/" dotnet run
exit
