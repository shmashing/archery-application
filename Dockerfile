FROM mcr.microsoft.com/dotnet/core/sdk:3.0-alpine as build

WORKDIR /app

COPY . .
RUN dotnet publish ArcheryTracker.sln -c Release -o /app/out/

FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-alpine as run
WORKDIR /app

COPY --from=build /app/out/ ./
EXPOSE 80
ENTRYPOINT ["dotnet","./ArcheryTracker.App.dll"]