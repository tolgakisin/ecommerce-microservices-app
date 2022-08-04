#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Services/BasketService/BasketService.API/BasketService.API.csproj", "Services/BasketService/BasketService.API/"]
COPY ["Services/BasketService/BasketService.Common/BasketService.Common.csproj", "Services/BasketService/BasketService.Common/"]
COPY ["Services/BasketService/BasketService.Business.Contracts/BasketService.Business.Contracts.csproj", "Services/BasketService/BasketService.Business.Contracts/"]
COPY ["BuildingBlocks/EventBus/EventBus.RabbitMQ/EventBus.RabbitMQ.csproj", "BuildingBlocks/EventBus/EventBus.RabbitMQ/"]
COPY ["BuildingBlocks/EventBus/EventBus.Core/EventBus.Core.csproj", "BuildingBlocks/EventBus/EventBus.Core/"]
COPY ["Services/BasketService/BasketService.Data.Contracts/BasketService.Data.Contracts.csproj", "Services/BasketService/BasketService.Data.Contracts/"]
COPY ["Services/BasketService/BasketService.Business/BasketService.Business.csproj", "Services/BasketService/BasketService.Business/"]
COPY ["Services/BasketService/BasketService.Data/BasketService.Data.csproj", "Services/BasketService/BasketService.Data/"]
COPY ["Services/BasketService/BasketService.API.Contracts/BasketService.API.Contracts.csproj", "Services/BasketService/BasketService.API.Contracts/"]
RUN dotnet restore "Services/BasketService/BasketService.API/BasketService.API.csproj"
COPY . .
WORKDIR "/src/Services/BasketService/BasketService.API"
RUN dotnet build "BasketService.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BasketService.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BasketService.API.dll"]
