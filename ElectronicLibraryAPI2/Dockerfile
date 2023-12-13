# Официальный образ ASP.NET Core
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base

WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR /home

COPY ["ElectronicLibraryAPI2/ElectronicLibraryAPI2.csproj", "ElectronicLibraryAPI2/"]

RUN dotnet restore "ElectronicLibraryAPI2/ElectronicLibraryAPI2.csproj"

COPY . .
WORKDIR "/home/ElectronicLibraryAPI2"

RUN dotnet build "ElectronicLibraryAPI2.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ElectronicLibraryAPI2.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ElectronicLibraryAPI2.dll"]