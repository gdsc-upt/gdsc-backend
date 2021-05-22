#!/usr/bin/sh

dotnet ef database update

dotnet GdscBackend.dll
