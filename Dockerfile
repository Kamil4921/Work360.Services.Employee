FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

#COPY src/Work360.Services.Employee.Api/*.csproj src/Work360.Services.Employee.Application/*.csproj src/Work360.Services.Employee.Core/*.csproj src/Work360.Services.Employee.Infrastructure/*.csproj  ./
#RUN dotnet restore Work360.Services.Employee.*.csproj

COPY . .
RUN dotnet publish src/Work360.Services.Employee.Api -c Release -o out

FROM mcr.microsoft.com/core/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet", "Work360.Services.Employee.Api.dll" ]