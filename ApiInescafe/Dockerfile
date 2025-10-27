# --- Estágio 1: Build (Linux) ---
# ATENÇÃO: Convertido para Linux para funcionar com o SQL Server.
# Use a imagem SDK .NET 9.0 (Linux)
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# O 'context' no docker-compose é '.', então os caminhos de CÓPIA são a partir da raiz do projeto
COPY ["ApiInescafe/ApiInescafe.csproj", "ApiInescafe/"]
RUN dotnet restore "ApiInescafe/ApiInescafe.csproj"

COPY . .
WORKDIR "/src/ApiInescafe"
RUN dotnet build "ApiInescafe.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ApiInescafe.csproj" -c Release -o /app/publish /p:UseAppHost=false

# --- Estágio 2: Runtime (Linux) ---
# Use a imagem ASP.NET 9.0 (Linux)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Expõe a porta 5183 (conforme seu Dockerfile original)
EXPOSE 5183
# Ouve em qualquer IP na porta 5183
ENV ASPNETCORE_URLS=http://+:5183

ENTRYPOINT ["dotnet", "ApiInescafe.dll"]
