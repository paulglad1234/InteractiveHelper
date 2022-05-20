#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["System/InteractiveHelper.CatalogAdminPanel/InteractiveHelper.CatalogAdminPanel.csproj", "System/InteractiveHelper.CatalogAdminPanel/InteractiveHelper.CatalogAdminPanel.csproj"]
RUN dotnet restore "System/InteractiveHelper.CatalogAdminPanel/InteractiveHelper.CatalogAdminPanel.csproj"
COPY . .
WORKDIR "/src/System/InteractiveHelper.CatalogAdminPanel"
RUN dotnet build "InteractiveHelper.CatalogAdminPanel.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "InteractiveHelper.CatalogAdminPanel.csproj" -c Release -o /app/publish

FROM nginx:alpine
WORKDIR /war/www/web/catalog

COPY --from=publish /app/publish/wwwroot .
COPY System/InteractiveHelper.CatalogAdminPanel/nginx.conf /etc/nginx/nginx.conf
EXPOSE 80