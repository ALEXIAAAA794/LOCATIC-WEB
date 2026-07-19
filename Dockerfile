FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY . .

RUN dotnet restore "Locatic.sln"
RUN dotnet publish "Locatic.Web/Locatic.Web.csproj" \
    -c Release \
    -o /app/publish \
    /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

COPY --from=build /app/publish .

# Création d'un utilisateur non privilégié
RUN adduser --disabled-password --gecos "" appuser \
    && chown -R appuser /app

USER appuser

EXPOSE 8080

ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "Locatic.Web.dll"]