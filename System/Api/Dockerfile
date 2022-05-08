#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["System/Api/InteractiveHelper.Api.csproj", "System/Api/"]
COPY ["Shared/Common/InteractiveHelper.Common.csproj", "Shared/Common/"]
COPY ["Services/Settings/InteractiveHelper.Settings.csproj", "Services/Settings/"]
COPY ["Data/Db.Context/InteractiveHelper.Db.Context.csproj", "Data/Db.Context/"]
COPY ["Data/Db.Entities/InteractiveHelper.Db.Entities.csproj", "Data/Db.Entities/"]
RUN dotnet restore "System/Api/InteractiveHelper.Api.csproj"
COPY . .
WORKDIR "/src/System/Api"
RUN dotnet build "InteractiveHelper.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "InteractiveHelper.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app

## Add the wait script to the image
ADD https://raw.githubusercontent.com/vishnubob/wait-for-it/master/wait-for-it.sh /wait-for-it.sh
RUN chmod +x /wait-for-it.sh

COPY --from=publish /app/publish .
ENTRYPOINT ["/wait-for-it.sh", "sqlserver:1433", "-s", "-t", "60", "--", "dotnet", "InteractiveHelper.Api.dll"]