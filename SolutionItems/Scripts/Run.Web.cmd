@echo off
set PROJPATH=%~dp0\..\..\src
cd %PROJPATH%\FrontEnd\LinnWorks.Task.FrontEnd
start "Web - http://localhost:5000/" dotnet run
exit
