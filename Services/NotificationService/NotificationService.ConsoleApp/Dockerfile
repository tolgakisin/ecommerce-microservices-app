#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Services/NotificationService/NotificationService.ConsoleApp/NotificationService.ConsoleApp.csproj", "Services/NotificationService/NotificationService.ConsoleApp/"]
COPY ["BuildingBlocks/EventBus/EventBus.RabbitMQ/EventBus.RabbitMQ.csproj", "BuildingBlocks/EventBus/EventBus.RabbitMQ/"]
COPY ["BuildingBlocks/EventBus/EventBus.Core/EventBus.Core.csproj", "BuildingBlocks/EventBus/EventBus.Core/"]
RUN dotnet restore "Services/NotificationService/NotificationService.ConsoleApp/NotificationService.ConsoleApp.csproj"
COPY . .
WORKDIR "/src/Services/NotificationService/NotificationService.ConsoleApp"
RUN dotnet build "NotificationService.ConsoleApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NotificationService.ConsoleApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NotificationService.ConsoleApp.dll"]
