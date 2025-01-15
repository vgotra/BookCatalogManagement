FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

COPY *.csproj .
RUN dotnet restore

COPY . .
RUN dotnet publish -c Release -o /publish

FROM nginx:alpine AS runtime

COPY --from=build /publish/wwwroot /usr/share/nginx/html

# Uncomment the following line if you have an nginx.conf file
# COPY nginx.conf /etc/nginx/nginx.conf

EXPOSE 80

# Start nginx
CMD ["nginx", "-g", "daemon off;"]