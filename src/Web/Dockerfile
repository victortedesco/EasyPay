FROM node:latest AS build

WORKDIR /app

COPY src/Web/package*.json ./
RUN npm install
COPY src/Web/ .
RUN npm run build --prod

FROM nginx:latest
COPY src/Web/nginx.conf /etc/nginx/nginx.conf
COPY --from=build /app/dist/web/browser /usr/share/nginx/html

EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
