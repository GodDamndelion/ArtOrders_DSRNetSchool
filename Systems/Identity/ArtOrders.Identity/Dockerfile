#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Systems/Identity/ArtOrders.Identity/ArtOrders.Identity.csproj", "Systems/Identity/ArtOrders.Identity/"]
COPY ["Data/ArtOrders.Context.Entities/ArtOrders.Context.Entities.csproj", "Data/ArtOrders.Context.Entities/"]
COPY ["Data/ArtOrders.Context/ArtOrders.Context.csproj", "Data/ArtOrders.Context/"]
COPY ["Shared/ArtOrders.Settings/ArtOrders.Settings.csproj", "Shared/ArtOrders.Settings/"]
COPY ["Shared/ArtOrders.Common/ArtOrders.Common.csproj", "Shared/ArtOrders.Common/"]
RUN dotnet restore "Systems/Identity/ArtOrders.Identity/ArtOrders.Identity.csproj"
COPY . .
WORKDIR "/src/Systems/Identity/ArtOrders.Identity"
RUN dotnet build "ArtOrders.Identity.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ArtOrders.Identity.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ArtOrders.Identity.dll"]