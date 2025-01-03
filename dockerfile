# Gebruik een officiële .NET SDK image om te bouwen
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Kopieer alle bestanden naar de container
COPY . .

# Herstel afhankelijkheden
RUN dotnet restore "ProductManagementAPI.csproj"

# Bouw de applicatie
RUN dotnet publish "ProductManagementAPI.csproj" -c Release -o /app/publish

# Gebruik een kleinere runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

# Kopieer de gepubliceerde output naar de runtime image
COPY --from=build /app/publish .

# Stel de startopdracht in
ENTRYPOINT ["dotnet", "ProductManagementAPI.dll"]
