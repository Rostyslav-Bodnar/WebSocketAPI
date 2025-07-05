#!/bin/bash

host="$1"

until /opt/mssql-tools/bin/sqlcmd -S $host -U sa -P MyStrongP@ssword123 -Q "SELECT 1" > /dev/null 2>&1; do
  >&2 echo "SQL Server is unavailable - waiting..."
  sleep 2
done

>&2 echo "SQL Server is up - continuing"
