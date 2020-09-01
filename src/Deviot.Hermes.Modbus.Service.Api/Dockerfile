#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 5000
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Deviot.Hermes.Modbus.Service.Api/Deviot.Hermes.Modbus.Service.Api.csproj", "Deviot.Hermes.Modbus.Service.Api/"]
COPY ["Deviot.Hermes.Modbus.Infra.CrossCutting.IoC/Deviot.Hermes.Modbus.Infra.CrossCutting.IoC.csproj", "Deviot.Hermes.Modbus.Infra.CrossCutting.IoC/"]
COPY ["Deviot.Hermes.Mosquitto.Infra.CrossCutting.Driver/Deviot.Hermes.Mosquitto.Infra.CrossCutting.Driver.csproj", "Deviot.Hermes.Mosquitto.Infra.CrossCutting.Driver/"]
COPY ["Deviot.Hermes.Modbus.Domain/Deviot.Hermes.Modbus.Domain.csproj", "Deviot.Hermes.Modbus.Domain/"]
COPY ["Deviot.Hermes.Common/Deviot.Hermes.Common.csproj", "Deviot.Hermes.Common/"]
COPY ["Deviot.Hermes.Modbus.Infra.CrossCutting.Driver/Deviot.Hermes.Modbus.Infra.CrossCutting.Driver.csproj", "Deviot.Hermes.Modbus.Infra.CrossCutting.Driver/"]
COPY ["Deviot.Hermes.Modbus.Application/Deviot.Hermes.Modbus.Application.csproj", "Deviot.Hermes.Modbus.Application/"]
COPY ["Deviot.Hermes.Common.BaseService/Deviot.Hermes.Common.BaseService.csproj", "Deviot.Hermes.Common.BaseService/"]
COPY ["Deviot.Hermes.Modbus.Infra.Data/Deviot.Hermes.Modbus.Infra.Data.csproj", "Deviot.Hermes.Modbus.Infra.Data/"]
COPY ["Deviot.Hermes.Common.BaseController/Deviot.Hermes.Common.BaseController.csproj", "Deviot.Hermes.Common.BaseController/"]
RUN dotnet restore "Deviot.Hermes.Modbus.Service.Api/Deviot.Hermes.Modbus.Service.Api.csproj"
COPY . .
WORKDIR "/src/Deviot.Hermes.Modbus.Service.Api"
RUN dotnet build "Deviot.Hermes.Modbus.Service.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Deviot.Hermes.Modbus.Service.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Deviot.Hermes.Modbus.Service.Api.dll"]