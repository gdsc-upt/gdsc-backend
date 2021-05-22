#!/bin/sh

dotnet dotnet-ef.dll database update

exec dotnet GdscBackend.dll
