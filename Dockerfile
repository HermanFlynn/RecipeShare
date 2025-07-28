
# Stage 1
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-dotnet

RUN apt-get update && apt-get install -y --no-install-recommends \
    apt-utils libc6 libgcc1 libgssapi-krb5-2 libicu72 libssl3 libstdc++6 zlib1g && \
    rm -rf /var/lib/apt/lists/*

ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["RecipeShare/", "RecipeShare.API/"]
COPY ["RecipeShare.Util/", "RecipeShare.Util/"]

WORKDIR "/src/RecipeShare.API"
RUN dotnet restore "./RecipeShare.API.csproj"

RUN dotnet build "./RecipeShare.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build-dotnet AS publish-dotnet
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./RecipeShare.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false
RUN dotnet publish "../RecipeShare.Util/RecipeShare.Util.csproj" -c $BUILD_CONFIGURATION -o /app/publish-utilities /p:UseAppHost=false

# Stage 2
FROM node:latest AS build-react

WORKDIR /usr/local/app

COPY frontend/package*.json ./
RUN npm install

COPY frontend/ /usr/local/app/

RUN npm run build

# Stage 3
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final

RUN apt-get update && apt-get install -y nginx supervisor telnet

COPY resolv.conf /etc/resolv.conf

RUN mkdir -p /var/lib/nginx/body /var/lib/nginx/proxy /var/lib/nginx/fastcgi /var/lib/nginx/uwsgi && \
    chown -R www-data:www-data /var/lib/nginx

RUN mkdir -p /var/log/supervisor

COPY --from=publish-dotnet /app/publish /app
COPY --from=publish-dotnet /app/publish-utilities /app-utilities

COPY --from=build-react /usr/local/app/dist /usr/share/nginx/html

COPY nginx.conf /etc/nginx/nginx.conf

COPY supervisord.conf /etc/supervisor/supervisord.conf

COPY entrypoint.sh /usr/local/bin/entrypoint.sh
RUN echo "entrypoint.sh file copied" && ls -l /usr/local/bin/entrypoint.sh

EXPOSE 4200
EXPOSE 5000
EXPOSE 5345

ENTRYPOINT ["/usr/local/bin/entrypoint.sh"]