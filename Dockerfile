ARG TARGETPLATFORM=amd64

FROM --platform=${TARGETPLATFORM} mcr.microsoft.com/dotnet/sdk:6.0.417-1-bullseye-slim-amd64 as build-amd64
WORKDIR /source

COPY *.sln .
COPY *.csproj .
RUN dotnet restore

COPY . .
RUN dotnet publish -c release -o /app --no-restore

FROM --platform=${TARGETPLATFORM} mcr.microsoft.com/dotnet/aspnet:6.0.25-bullseye-slim-amd64 as runtime-amd64
WORKDIR /app
COPY --from=build-amd64 /app ./
ENTRYPOINT ["dotnet", "YorozuyaServer.dll"]

FROM --platform=${TARGETPLATFORM} mcr.microsoft.com/dotnet/sdk:6.0.417-1-bullseye-slim-arm64v8 as build-arm64
WORKDIR /source

COPY *.sln .
COPY *.csproj .
RUN dotnet restore

COPY . .
RUN dotnet publish -c release -o /app --no-restore

FROM --platform=${TARGETPLATFORM} mcr.microsoft.com/dotnet/aspnet:6.0.25-bullseye-slim-arm64v8 as runtime-arm64
WORKDIR /app
COPY --from=build-arm64 /app ./
ENTRYPOINT ["dotnet", "YorozuyaServer.dll"]
