# Etapa 1: build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copiar csproj e restaurar depend�ncias
COPY *.csproj ./
RUN dotnet restore

# Copiar todo o c�digo e compilar
COPY . ./
RUN dotnet publish -c Release -o out

# Etapa 2: runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .

# Porta que sua API vai escutar
ENV ASPNETCORE_URLS=http://+:8080

# Start da aplica��o
ENTRYPOINT ["dotnet", "MoviesAPI.dll"]
