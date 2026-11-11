# Prueba tecnica consolide

## Paso 1 | Clonar el repositorio

	git clone 

## Paso 2 | Correr docker compose.yaml para crear la base de datos

Abrir el proyecto en la terminal y ejecutar:

	docker compose up

## Paso 3 | Conectarse a la base de datos por defecto

Abrir el gestor de base de datos de preferecia y poner las siguientes credenciales:

	Host: localhost
	Port: 1433
	Database/Schema: master
	Username: sa
	Password: root1234YT$

## Paso 4 | Crear la base de datos para el proyecto

	Database Name: TecTestDB

## Paso 5 | Abrir Tools -> NuGet Package Manager -> Package Manager Console y ejecutar:

	Add-Migration InitialCreate

Y luego (la bd debe estar creada levantada):

	Update-Database

## Paso 6 | Correr el proyecto -> Container(Dockerfile)


