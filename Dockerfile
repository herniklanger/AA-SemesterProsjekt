FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
COPY ["Fleet/Fleet.csproj", "Fleet/"]
COPY ["FleetTest/FleetTest.csproj", "FleetTest/"]
RUN dotnet restore "Fleet/Fleet.csproj"
RUN dotnet restore "FleetTest/FleetTest.csproj"
COPY . .
RUN dotnet build "Fleet/Fleet.csproj" -c Release -o /app/build

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS test
COPY --from=build . .
RUN dotnet test "FleetTest/FleetTest.csproj"

FROM build AS publish
RUN dotnet publish "Fleet/Fleet.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Fleet.dll"]
