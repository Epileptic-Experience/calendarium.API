# 1. SDK para compilar
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# 2. Copiamos TODOS los archivos .csproj de la raíz
# Esto incluye el de la API y el que termina en User
COPY *.csproj ./

# 3. Restauramos específicamente el de la API
# (Esto restaurará automáticamente el otro si es una dependencia)
RUN dotnet restore "calendarium.API.csproj"

# 4. Ahora copiamos todo el resto del código
COPY . .

# 5. Publicamos la API
RUN dotnet publish "calendarium.API.csproj" -c Release -o /app/publish

# 6. Imagen final de ejecución
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 8080
# Verifica que el nombre de la DLL sea este (suele ser igual al csproj)
ENTRYPOINT ["dotnet", "calendarium.API.dll"]