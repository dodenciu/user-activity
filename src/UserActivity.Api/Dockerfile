FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/UserActivity.Api/UserActivity.Api.csproj", "src/UserActivity.Api/"]
COPY ["src/UserActivity.Application/UserActivity.Application.csproj", "src/UserActivity.Application/"]
COPY ["src/UserActivity.Domain/UserActivity.Domain.csproj", "src/UserActivity.Domain/"]
COPY ["src/UserActivity.Infrastructure/UserActivity.Infrastructure.csproj", "src/UserActivity.Infrastructure/"]
RUN dotnet restore "./src/UserActivity.Api/./UserActivity.Api.csproj"
COPY . .
WORKDIR "/src/src/UserActivity.Api"
RUN dotnet build "./UserActivity.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./UserActivity.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UserActivity.Api.dll"]