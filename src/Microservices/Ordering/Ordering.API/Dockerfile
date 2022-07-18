#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Microservices/Ordering/Ordering.API/Ordering.API.csproj", "Microservices/Ordering/Ordering.API/"]
COPY ["BuildingBlocks/EventBus.Message/EventBus.Message.csproj", "BuildingBlocks/EventBus.Message/"]
COPY ["Microservices/Ordering/Ordering.Application/Ordering.Application.csproj", "Microservices/Ordering/Ordering.Application/"]
COPY ["Microservices/Ordering/Ordering.Domain/Ordering.Domain.csproj", "Microservices/Ordering/Ordering.Domain/"]
COPY ["Microservices/Ordering/Ordering.Infrastructure/Ordering.Infrastructure.csproj", "Microservices/Ordering/Ordering.Infrastructure/"]
RUN dotnet restore "Microservices/Ordering/Ordering.API/Ordering.API.csproj"
COPY . .
WORKDIR "/src/Microservices/Ordering/Ordering.API"
RUN dotnet build "Ordering.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ordering.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ordering.API.dll"]