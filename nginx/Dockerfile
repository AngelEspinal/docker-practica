# Usamos la imagen base de nginx:alpine
FROM nginx:alpine

# Copiamos el archivo de configuración de nginx al contenedor
COPY nginx.conf /etc/nginx/nginx.conf

# Comando para iniciar nginx en primer plano
CMD ["nginx", "-g", "daemon off;"]
