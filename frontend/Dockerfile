#stage 1: build
FROM node:18.16.0-alpine AS build

WORKDIR /app

COPY InfoLimpieza/package*.json .

RUN npm install -g @angular/cli@16.0.1 && npm install


COPY InfoLimpieza/. .

RUN ng build 
RUN ls -alt

#stage 2:
FROM nginx:alpine

#ADD default.conf /etc/nginx/conf.d/default.conf

COPY --from=build /app/dist/info-limpieza /usr/share/nginx/html
#COPY --from=build /app/nginx.conf /etc/nginx/conf.d/default.conf
COPY nginx.conf /etc/nginx/conf.d/default.conf

EXPOSE 80

CMD ["nginx","-g","daemon off;"]
