@echo off
set PROJPATH=%~dp0\..\..\src
cd %PROJPATH%\FrontEnd\LinnWorks.Taks.Web
start "Web - http://localhost:5000/" dotnet run
exit
