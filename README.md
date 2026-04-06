🚀 Calendarium API - Dockerized Backend

Este proyecto es una API REST desarrollada en .NET 8 diseñada para ser robusta, escalable y fácil de desplegar. La arquitectura está totalmente dockerizada, lo que permite levantar todo el entorno de desarrollo (API + Base de Datos PostgreSQL) con un solo comando.

🛠️ Stack Tecnológico

    Framework: .NET 8 (ASP.NET Core)

    Lenguaje: C#

    Base de Datos: PostgreSQL

    ORM: Entity Framework Core (EF Core)

    Contenedores: Docker & Docker Compose

    Autenticación: [Menciona si usas JWT o Identity]

🏗️ Arquitectura e Infraestructura

El proyecto utiliza una estrategia de infraestructura como código mediante Docker Compose, separando las responsabilidades en dos servicios principales:

    api: Contenedor basado en .NET que contiene la lógica de negocio y las migraciones.

    db: Instancia persistente de PostgreSQL que utiliza volúmenes de Docker para asegurar que los datos no se pierdan al apagar los contenedores.

Características destacadas:

    Auto-Migrations: Al iniciar el contenedor de la API, el sistema detecta automáticamente el estado de la base de datos y aplica las migraciones pendientes. Cero 		    configuración manual de tablas.

    Variables de Entorno: Gestión segura de credenciales mediante archivos .env, evitando el hardcoding de secretos en el código fuente.

    Service Discovery: La comunicación entre la API y la DB se realiza mediante nombres de servicio internos de Docker, eliminando la dependencia de IPs locales.

🚀 Instalación y Ejecución

Sigue estos pasos para tener el proyecto corriendo en menos de un minuto:
1. Prerrequisitos

    Tener instalado Docker Desktop.

2. Configuración

	Clona el repositorio y crea un archivo .env en la raíz del proyecto basándote en el archivo de ejemplo:
		cp .env.example .env
	
	
	Edita el archivo .env y define tus propias credenciales para DB_PASSWORD, DB_USER y DB_NAME

3. Levantar el entorno

	Ejecuta el siguiente comando en la terminal:

	docker-compose up --build

	La API estará disponible en: http://localhost:5247

📂 Estructura del Proyecto

    /Endpoints: Endpoints de la API.

    /Models: Entidades de datos.

    /Data: Contexto de Base de Datos y configuración de EF.

    /Migrations: Historial de cambios de la base de datos (se aplican automáticamente en el contenedor).

    Dockerfile: Receta de construcción de la imagen de la API.

    docker-compose.yml: Orquestación de servicios.

💡 Notas para Desarrolladores

	Si deseas realizar cambios en los modelos de la base de datos, recuerda crear una nueva migración localmente antes de buildear el contenedor:

	dotnet ef migrations add NombreDeTuMigracion

Desarrollado por Joaquin "J" Cerruti

[![LinkedIn](https://img.shields.io/badge/LinkedIn-0077B5?style=for-the-badge&logo=linkedin&logoColor=white)](https://www.linkedin.com/in/joaquin-cerruti-lerech-82369b1ab/)
[![GitHub](https://img.shields.io/badge/GitHub-100000?style=for-the-badge&logo=github&logoColor=white)](https://github.com/Epileptic-Experience)
[![Gmail](https://img.shields.io/badge/Gmail-D14836?style=for-the-badge&logo=gmail&logoColor=white)](mailto:joaquincerruti54@gmail.com)
	
