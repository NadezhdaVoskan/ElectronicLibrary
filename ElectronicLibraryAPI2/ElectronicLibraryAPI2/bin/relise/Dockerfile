# Официальный образ ASP.NET Core
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
# Рабочая папка
WORKDIR /app

# Образ для сборки
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
# Рабочая папка
WORKDIR /src
# Копирвоание твоего приложения YourAspNetApp
COPY ["ElectronicLibraryAPI2/ElectronicLibraryAPI2.csproj", "ElectronicLibraryAPI2/"]
#Запуск
RUN dotnet restore "ElectronicLibraryAPI2/ElectronicLibraryAPI2.csproj"
#Копирование билда
COPY . .
WORKDIR "/src/ElectronicLibraryAPI2"
#Копирование запуск
RUN dotnet build "ElectronicLibraryAPI2.csproj" -c Release -o /app/build

#Публикация
FROM build AS publish
RUN dotnet publish "ElectronicLibraryAPI2.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ElectronicLibraryAPI2.dll"]