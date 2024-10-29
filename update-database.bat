@echo off

echo Updating the DB...

set /p migrationName=Please insert the migration name:

dotnet ef migrations add %migrationName%

dotnet ef database update

echo DataBase update completed!