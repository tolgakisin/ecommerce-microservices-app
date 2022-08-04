#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Services/ProductService/ProductService.API/ProductService.API.csproj", "Services/ProductService/ProductService.API/"]
COPY ["Services/ProductService/ProductService.Command/ProductService.Command.csproj", "Services/ProductService/ProductService.Command/"]
COPY ["Services/ProductService/ProductService.Core/ProductService.Core.csproj", "Services/ProductService/ProductService.Core/"]
COPY ["Services/ProductService/ProductService.Query/ProductService.Query.csproj", "Services/ProductService/ProductService.Query/"]
RUN dotnet restore "Services/ProductService/ProductService.API/ProductService.API.csproj"
COPY . .
WORKDIR "/src/Services/ProductService/ProductService.API"
RUN dotnet build "ProductService.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ProductService.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProductService.API.dll"]
