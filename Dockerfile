FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY GdscBackend/GdscBackend.csproj .
RUN dotnet restore

# Copy everything else and build
COPY docker-entrypoint.sh .
COPY GdscBackend/ .

#RUN dotnet build GdscBackend.csproj -c Release -o /app/build
RUN dotnet publish GdscBackend.csproj -c Release -o /app/out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build-env /app/out .

RUN chmod +x docker-entrypoint.sh
CMD ["./docker-entrypoint.sh"]

# Link image with github repo
LABEL org.opencontainers.image.source=https://github.com/dsc-upt/gdsc-backend
