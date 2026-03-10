FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
WORKDIR /src

COPY ["NotificationsAPI.sln", "."]
COPY ["src/Notifications.Domain/Notifications.Domain.csproj", "src/Notifications.Domain/"]
COPY ["src/Notifications.Application/Notifications.Application.csproj", "src/Notifications.Application/"]
COPY ["src/Notifications.Infrastructure/Notifications.Infrastructure.csproj", "src/Notifications.Infrastructure/"]
COPY ["src/Notifications.API/Notifications.API.csproj", "src/Notifications.API/"]
RUN dotnet restore "src/Notifications.API/Notifications.API.csproj"

COPY . .
RUN dotnet publish "src/Notifications.API/Notifications.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
RUN apk upgrade --no-cache
WORKDIR /app
COPY --from=build /app/publish .
USER app
ENTRYPOINT ["dotnet", "Notifications.API.dll"]
