#!/bin/bash

echo "Updating the DB..."

read -p "Please insert the migration name: " migrationName

dotnet ef migrations add "$migrationName"

dotnet ef database update

echo "DataBase update completed!"