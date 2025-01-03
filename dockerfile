FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "ProductManagementAPI/ProductManagementAPI.csproj"
RUN dotnet publish "ProductManagementAPI/ProductManagementAPI.csproj" -c Release -o /app/publish

FROM node:16 AS frontend
WORKDIR /frontend
COPY Frontend/ .
RUN npm install
RUN npm run build

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
COPY --from=frontend /frontend/build ./wwwroot
ENTRYPOINT ["dotnet", "ProductManagementAPI.dll"]
