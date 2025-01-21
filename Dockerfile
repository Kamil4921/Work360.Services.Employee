FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

RUN dotnet tool install --global dotnet-ef

ENV PATH="$PATH:/root/.dotnet/tools"

COPY ["src/Work360.Services.Employee.Api/Work360.Services.Employee.Api.csproj", "src/Work360.Services.Employee.Api/"]
COPY ["src/Work360.Services.Employee.Application/Work360.Services.Employee.Application.csproj", "src/Work360.Services.Employee.Application/"]
COPY ["src/Work360.Services.Employee.Core/Work360.Services.Employee.Core.csproj", "src/Work360.Services.Employee.Core/"]
COPY ["src/Work360.Services.Employee.Infrastructure/Work360.Services.Employee.Infrastructure.csproj", "src/Work360.Services.Employee.Infrastructure/"]
RUN dotnet restore "src/Work360.Services.Employee.Api/Work360.Services.Employee.Api.csproj"

COPY . .
WORKDIR "src/Work360.Services.Employee.Api"
RUN dotnet build "Work360.Services.Employee.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Work360.Services.Employee.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY src/Work360.Services.Employee.Infrastructure/Migrations/ ./Migrations/

RUN dotnet ef database update --project /app

ENTRYPOINT ["dotnet", "Work360.Services.Employee.Api.dll"]