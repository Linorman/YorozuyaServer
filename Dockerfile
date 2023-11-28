FROM mcr.microsoft.com/dotnet/sdk:6.0.417-1-bullseye-slim-amd64 as build
WORKDIR /source

COPY *.sln .
COPY *.csproj .
RUN dotnet restore

COPY . .
RUN dotnet publish -c release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:6.0.25-bullseye-slim-amd64
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "YorozuyaServer.dll"]
