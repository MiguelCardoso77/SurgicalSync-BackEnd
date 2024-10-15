@echo off

echo Cleaning the project...
dotnet clean

echo Rebuilding the project...
dotnet build

echo Running the project...
dotnet run --launch-profile DDDSample1

pause