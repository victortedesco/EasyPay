FROM node:22 AS build

WORKDIR /app

COPY src/Web/ .
RUN npm --loglevel verbose ci
RUN npm run build --prod

FROM nginx:1.27
COPY src/Web/nginx.conf /etc/nginx/nginx.conf
COPY src/Web/mime.types /etc/nginx/mime.types
COPY --from=build /app/dist/web/browser /usr/share/nginx/html

EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
