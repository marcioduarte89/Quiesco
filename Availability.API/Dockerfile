#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Availability.API/Availability.API.csproj", "Availability.API/"]
COPY ["Availability.Common/Availability.Common.csproj", "Availability.Common/"]
COPY ["Availability.Core/Availability.Core.csproj", "Availability.Core/"]
COPY ["Availability.Infrastructure.Data/Availability.Infrastructure.Data.csproj", "Availability.Infrastructure.Data/"]
RUN dotnet restore "Availability.API/Availability.API.csproj"
COPY . .
WORKDIR "/src/Availability.API"
RUN dotnet build "Availability.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Availability.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Availability.API.dll"]