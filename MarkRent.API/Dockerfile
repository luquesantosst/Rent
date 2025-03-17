#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["MarkRent/MarkRent.API.csproj", "MarkRent/"]
COPY ["MarkRent.Application/MarkRent.Application.csproj", "MarkRent.Application/"]
COPY ["MarkRent.Domain/MarkRent.Domain.csproj", "MarkRent.Domain/"]
COPY ["MarkRent.Infra/MarkRent.Infra.csproj", "MarkRent.Infra/"]
RUN dotnet restore "./MarkRent/./MarkRent.API.csproj"
COPY . .
WORKDIR "/src/MarkRent"
RUN dotnet build "./MarkRent.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MarkRent.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MarkRent.API.dll"]