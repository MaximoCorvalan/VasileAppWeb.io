FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copia el archivo de solución
COPY ["MiVasile/MiVasile.sln", "./MiVasile/"]

# Copia los archivos de proyecto
COPY ["MiVasile/MiVasile.csproj", "MiVasile/"]
COPY ["MiVasile.Domain/MiVasile.Domain.csproj", "MiVasile.Domain/"]
COPY ["MiVasile.Infrastructure/MiVasile.Infrastructure.csproj", "MiVasile.Infrastructure/"]

# Restaura las dependencias
RUN dotnet restore MiVasile/MiVasile.sln

# Copia el resto de los archivos de los proyectos
COPY . .

# Compila la aplicación
RUN dotnet build MiVasile/MiVasile.csproj -c Release -o out

# Publica la aplicación
FROM build AS publish
RUN dotnet publish MiVasile/MiVasile.csproj -c Release -o out

# Crea la imagen final
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=publish /app/out ./

# Expone el puerto donde se ejecutará la aplicación
EXPOSE 80

# Comando para ejecutar la aplicación
ENTRYPOINT ["dotnet", "MiVasile.dll"]

