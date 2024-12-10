# Use o SDK do .NET 8.0 para compilar a aplicação
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Definir o diretório de trabalho para o build
WORKDIR /src

# Copiar o arquivo .csproj e restaurar as dependências
COPY GoldenRaspberryAwards.Api.csproj ./  
RUN dotnet restore "GoldenRaspberryAwards.Api.csproj"

# Copiar o restante dos arquivos do projeto
COPY . .

# Publicar a aplicação com otimização
RUN dotnet publish "GoldenRaspberryAwards.Api.csproj" -c Release -o /app

# Usar uma imagem mais leve do .NET para execução
FROM mcr.microsoft.com/dotnet/aspnet:8.0

# Definir o diretório de trabalho na imagem final
WORKDIR /app

# Copiar os artefatos publicados do estágio de build
COPY --from=build /app .

# Certificar que o arquivo CSV seja copiado corretamente
COPY Artefatos/movielist.csv /app/movielist.csv

# Expor a porta 8080, conforme configurado no código
EXPOSE 8080

# Comando para iniciar a aplicação
ENTRYPOINT ["dotnet", "GoldenRaspberryAwards.Api.dll"]
