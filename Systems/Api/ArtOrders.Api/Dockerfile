#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Systems/Api/ArtOrders.Api/ArtOrders.Api.csproj", "Systems/Api/ArtOrders.Api/"]
COPY ["Data/ArtOrders.Context/ArtOrders.Context.csproj", "Data/ArtOrders.Context/"]
COPY ["Services/ArtOrders.Services.Users/ArtOrders.Services.Users.csproj", "Services/ArtOrders.Services.Users/"]
COPY ["Data/ArtOrders.Context.Entities/ArtOrders.Context.Entities.csproj", "Data/ArtOrders.Context.Entities/"]
COPY ["Shared/ArtOrders.Common/ArtOrders.Common.csproj", "Shared/ArtOrders.Common/"]
COPY ["Services/ArtOrders.Services.Tasks/ArtOrders.Services.Tasks.csproj", "Services/ArtOrders.Services.Tasks/"]
COPY ["Shared/ArtOrders.Consts/ArtOrders.Consts.csproj", "Shared/ArtOrders.Consts/"]
COPY ["Services/ArtOrders.Services.EmailSender/ArtOrders.Services.EmailSender.csproj", "Services/ArtOrders.Services.EmailSender/"]
COPY ["Services/ArtOrders.Services.RabbitMq/ArtOrders.Services.RabbitMq.csproj", "Services/ArtOrders.Services.RabbitMq/"]
COPY ["Shared/ArtOrders.Settings/ArtOrders.Settings.csproj", "Shared/ArtOrders.Settings/"]
COPY ["Data/Migrations/ArtOrders.Context.MigrationsPostgreSQL/ArtOrders.Context.MigrationsPostgreSQL.csproj", "Data/Migrations/ArtOrders.Context.MigrationsPostgreSQL/"]
COPY ["Services/ArtOrders.Services.Cache/ArtOrders.Services.Cache.csproj", "Services/ArtOrders.Services.Cache/"]
COPY ["Services/ArtOrders.Services.Chats/ArtOrders.Services.Chats.csproj", "Services/ArtOrders.Services.Chats/"]
COPY ["Services/ArtOrders.Services.Orders/ArtOrders.Services.Orders.csproj", "Services/ArtOrders.Services.Orders/"]
COPY ["Services/ArtOrders.Services.Settings/ArtOrders.Services.Settings.csproj", "Services/ArtOrders.Services.Settings/"]
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