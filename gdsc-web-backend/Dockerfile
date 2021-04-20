FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80


FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["gdsc-web-backend.csproj", "gdsc-web-backend/"]
RUN dotnet restore "gdsc-web-backend/gdsc-web-backend.csproj"
COPY . .

WORKDIR "/src/gdsc-web-backend"
RUN dotnet build "gdsc-web-backend.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "gdsc-web-backend.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "gdsc-web-backend.dll"]
