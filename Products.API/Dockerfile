#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Products.API/Products.API.csproj", "Products.API/"]
COPY ["Products.Common/Products.Common.csproj", "Products.Common/"]
COPY ["Products.Infrastructure.Data/Products.Infrastructure.Data.csproj", "Products.Infrastructure.Data/"]
COPY ["Products.Core/Products.Core.csproj", "Products.Core/"]
RUN dotnet restore "Products.API/Products.API.csproj"
COPY . .
WORKDIR "/src/Products.API"
RUN dotnet build "Products.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Products.API.csproj" -c Release -o /app/publish


FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Products.API.dll"]