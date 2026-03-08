# Stage 1: Build Blazor WebAssembly
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /app

# Copiar apenas o arquivo de projeto primeiro (cache do Docker)
COPY WebSocket/*.csproj ./WebSocket/
RUN dotnet restore WebSocket/YourProject.csproj

# Copiar todo o código
COPY WebSocket/. ./WebSocket/

# Publicar Blazor WASM
RUN dotnet publish WebSocket/YourProject.csproj -c Release -o /app/publish

# Stage 2: Nginx para servir o Blazor
FROM nginx:alpine
COPY --from=build /app/publish/wwwroot /usr/share/nginx/html
COPY nginx.conf /etc/nginx/conf.d/default.conf

EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]