# Usando uma imagem base do Node.js para construir a aplicação Angular
FROM node:18 AS build

# Definir o diretório de trabalho
WORKDIR /app

# Copiar o package.json e package-lock.json para instalar as dependências
COPY package.json package-lock.json ./

# Instalar as dependências
RUN npm install

# Copiar o restante dos arquivos do frontend
COPY . .

# Construir o aplicativo Angular para produção
RUN npm run build -- --configuration=production

# Usar uma imagem do Nginx para servir o conteúdo estático
FROM nginx:alpine

# Copiar o build gerado pela aplicação Angular (pasta `browser`) para o diretório do Nginx
COPY --from=build /app/dist/golden-raspberry/browser /usr/share/nginx/html

# Expor a porta 80 para que o Nginx possa ser acessado
EXPOSE 80

# Iniciar o Nginx em primeiro plano
CMD ["nginx", "-g", "daemon off;"]
