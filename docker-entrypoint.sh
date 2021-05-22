#!/bin/sh

dotnet ef database update

exec dotnet GdscBackend.dll --migrate
