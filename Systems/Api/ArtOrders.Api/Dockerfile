#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Systems/Api/ArtOrders.Api/ArtOrders.Api.csproj", "Systems/Api/ArtOrders.Api/"]
COPY ["Services/ArtOrders.Services.Settings/ArtOrders.Services.Settings.csproj", "Services/ArtOrders.Services.Settings/"]
COPY ["Shared/ArtOrders.Settings/ArtOrders.Settings.csproj", "Shared/ArtOrders.Settings/"]
COPY ["Shared/ArtOrders.Common/ArtOrders.Common.csproj", "Shared/ArtOrders.Common/"]
COPY ["Systems/Api/ArtOrders.Api.Settings/ArtOrders.Api.Settings.csproj", "Systems/Api/ArtOrders.Api.Settings/"]
RUN dotnet restore "Systems/Api/ArtOrders.Api/ArtOrders.Api.csproj"
COPY . .
WORKDIR "/src/Systems/Api/ArtOrders.Api"
RUN dotnet build "ArtOrders.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ArtOrders.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ArtOrders.Api.dll"]