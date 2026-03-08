# Estágio de build
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /app

# Copia o arquivo de projeto e restaura dependências
COPY *.csproj .
RUN dotnet restore

# Copia todo o código e publica
COPY . .
RUN dotnet publish -c Release -o /app/publish

# Estágio de produção
FROM nginx:alpine
WORKDIR /usr/share/nginx/html

# Remove a página padrão do nginx
RUN rm -rf /usr/share/nginx/html/*

# Copia a configuração customizada do nginx
COPY nginx.conf /etc/nginx/nginx.conf

# Copia os arquivos publicados para o local correto
COPY --from=build /app/publish/wwwroot /usr/share/nginx/html

EXPOSE 80