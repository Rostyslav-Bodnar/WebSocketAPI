#!/bin/bash
set -e

echo "Waiting for SQL Server..."
/app/wait-for-mssql.sh db:1433

echo "Running EF Core migrations..."
dotnet ef database update --no-build

echo "Starting the API..."
exec dotnet "WebSocket API.dll"
