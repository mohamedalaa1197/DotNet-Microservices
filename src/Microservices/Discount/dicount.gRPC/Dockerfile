#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Microservices/Discount/dicount.gRPC/dicount.gRPC.csproj", "Microservices/Discount/dicount.gRPC/"]
RUN dotnet restore "Microservices/Discount/dicount.gRPC/dicount.gRPC.csproj"
COPY . .
WORKDIR "/src/Microservices/Discount/dicount.gRPC"
RUN dotnet build "dicount.gRPC.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "dicount.gRPC.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "dicount.gRPC.dll"]
