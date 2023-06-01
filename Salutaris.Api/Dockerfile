FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Salutaris.Api/salutaris.csproj", "Salutaris.Api/"]
RUN dotnet restore "Salutaris.Api/salutaris.csproj"
COPY . .
WORKDIR "/src/Salutaris.Api"
RUN dotnet build "salutaris.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "salutaris.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "salutaris.dll"]
