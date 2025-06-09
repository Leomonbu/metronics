# Proyecto registro de materias

## Como funciona la aplicacion ##
[Manual de usuario aplicacion](https://github.com/Leomonbu/metronics/blob/Suport_files/Manual%20de%20usuario.pdf)

## Diagrama de base de datos ##
![image](https://github.com/user-attachments/assets/c3d0e2bd-459e-484c-9494-ae9aad782654)

## Instrucciones de instalación y ejecución ##
### Clonar el repositorio ###

```bash
   git clone https://github.com/Leomonbu/pruebalink.git
 ```

### Si se desea crear como microservicio con Docker ###
```bash
   docker-compose up --build
```
- Esto ejecuta:
     - WSRegistro en http://localhost:5120
     - SQL Server interno accesible por ambas APIs

- Acceder a Swagger UI
     - Registro API: http://localhost:5120/swagger

### Crear base de datos y tablas ###
- Ingresar por una terminal a SQLCMD
```bash
   sqlcmd -S localhost -U sa -P Tu_Pass123!
```   
- Crear la base de datos
```bash
   CREATE DATABASE [bdMetronics]
   GO
```
- Direccionar a la base de datos
```bash
   USE bdMetronics
   GO
```
- Crear las tablas de la aplicacion los scripts encuentran en la siguiente ruta **"\Mectronics\ServiciosRegistro\WSRegistro\Scripts"**, un ejemplo:
```bash
  CREATE TABLE [dbo].[Estudiante](
	[IdEstudiante] [int] IDENTITY(1,1) NOT NULL,
	[TipoDocumento] [int] NOT NULL,
	[DocumentoEstudiante] [bigint] NOT NULL,
	[NombresEstudiante] [varchar](70) NOT NULL,
	[ApellidosEstudiante] [varchar](70) NOT NULL,
	[EmailEstudiante] [varchar](250) NOT NULL,
	[Password_hash] [TEXT] NOT NULL,
	[Fecha_nacimiento] [datetime] NOT NULL,
	[IdRol][int] NOT NULL,
	[EstadoEstudiante] [int] NOT NULL,
	[CreoUsuario][DATETIME] DEFAULT GETDATE()
	PRIMARY KEY (IdEstudiante),
    FOREIGN KEY (IdRol) REFERENCES Roles(IdRol) ON DELETE CASCADE
);
GO
CREATE UNIQUE INDEX idx_estudiante_email ON estudiante(EmailEstudiante);
GO
CREATE UNIQUE INDEX idx_estudiante_documento ON estudiante(DocumentoEstudiante);
GO
```
<br>

## Descripción de la arquitectura ##

### Estructura general ###
Solución con una APIs desarrolladas en ASP.NET Core, tiene la configuracion para dockerizar pero la aplicacion para que funcione sera local.

### Componentes principales ###
| Componente                |    Descripcion                                          |
|---------------------------|---------------------------------------------------------|
| WSRegistro              |  Expone endpoints para la administracion de la aplicacion   |
| Base de datos SQL Server  |  Administra la data de la aplicacion          |

### Tecnologias y herramientas utilizadas ###
* Backend: ASP.NET Core Web API (con Entity Framework Core)
* Frontend: React (con React Router, Axios, Formik, etc.)
* Base de datos: SQL Server
* Autenticación: JWT (JSON Web Tokens)
* Autorización: Claims + Roles
* Logging: ILogger
* Pruebas: xUnit + Moq + TestServer (para integración)
* ORM: Entity Framework Core con patrón Repository

### Paquetes necesarios API ###
* WSRegistgro :
    	- dotnet add package Microsoft.EntityFrameworkCore.SqlServer
	- dotnet add package Microsoft.EntityFrameworkCore.Design
	- dotnet add package AutoMapper.Extensions.Microsoft.DependencyInjection
	- dotnet add package Swashbuckle.AspNetCore
	- dotnet add package FluentValidation
	- dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
* WSRegistgro.Tests:
	- dotnet add package Moq
	- dotnet add package xunit
* WSRegistgro.Integration.Tests:
 	- dotnet add Microsoft.AspNetCore.Mvc.Testing
	- dotnet add Microsoft.EntityFrameworkCore.InMemory
<br>
