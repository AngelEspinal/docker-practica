# stage 1: build
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY Alumnos/Alumnos/*.csproj .
RUN dotnet restore --use-current-runtime

# copy everything else and build app
COPY Alumnos/Alumnos/. .
RUN dotnet publish -c Release -o out

# stage 2: final image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app


# copy the published app from stage 1
COPY --from=build /source/out .

EXPOSE 80

# set the entrypoint
ENTRYPOINT ["dotnet", "Alumnos.dll"]
