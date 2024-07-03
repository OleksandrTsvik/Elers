FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source
EXPOSE 8080

# copy .csproj and restore as distinct layers
COPY Elers.sln .
COPY API/API.csproj API/
COPY Application/Application.csproj Application/
COPY Domain/Domain.csproj Domain/
COPY Infrastructure/Infrastructure.csproj Infrastructure/
COPY Persistence/Persistence.csproj Persistence/

RUN dotnet restore

# copy everything else
COPY . .

# install web dependencies
FROM node:20 AS web-build
WORKDIR /source/Web
COPY Web/package*.json .
RUN npm install

# copy everything else and build web app
COPY Web/ .
RUN npm run build

# build app
FROM build AS publish
WORKDIR /source
COPY --from=web-build /source/API/wwwroot ./API/wwwroot
RUN dotnet publish -c Release -o out

# build a runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=publish /source/out .
ENTRYPOINT [ "dotnet", "API.dll" ]
