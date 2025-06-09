USE [bdMetronics]

GO
SELECT * FROM [dbo].[Estudiante]		GO
SELECT * FROM [dbo].[Materia]			GO
SELECT * FROM [dbo].[Profesor]			GO
SELECT * FROM [dbo].[Roles]				GO
SELECT * FROM [dbo].[Estudiante_Roles]	GO
SELECT * FROM [dbo].[Semestre]			GO
SELECT * FROM [dbo].[Inscripcion]		
GO

EXEC [dbo].[SelectClassmatesUser] @studentId = 2, @materiaId = 5

-- Blanquear data de la tabla
--TRUNCATE TABLE [dbo].[Estudiante]			GO
--TRUNCATE TABLE [dbo].[Materia]			GO
--TRUNCATE TABLE [dbo].[Profesor]			GO
--TRUNCATE TABLE [dbo].[Roles]				GO
--TRUNCATE TABLE [dbo].[Estudiante_Roles]	GO
--TRUNCATE TABLE [dbo].[Semestre]			GO
--TRUNCATE TABLE [dbo].[Inscripcion]	
--GO

-- Eliminar tabla
--DROP TABLE [dbo].[Estudiante]			GO
--DROP TABLE [dbo].[Materia]			GO
--DROP TABLE [dbo].[Profesor]			GO
--DROP TABLE [dbo].[Roles]				GO
--DROP TABLE [dbo].[Estudiante_Roles]	GO
--DROP TABLE [dbo].[Semestre]			GO
--DROP TABLE [dbo].[Inscripcion]	
--GO

-- Validar los indices de la tabla
GO
SELECT name, object_id, type_desc FROM sys.indexes WHERE object_id = OBJECT_ID('Estudiante');
GO
SELECT name, object_id, type_desc FROM sys.indexes WHERE object_id = OBJECT_ID('Materia');
GO
SELECT name, object_id, type_desc FROM sys.indexes WHERE object_id = OBJECT_ID('Profesor');
GO
SELECT name, object_id, type_desc FROM sys.indexes WHERE object_id = OBJECT_ID('Roles');
GO
SELECT name, object_id, type_desc FROM sys.indexes WHERE object_id = OBJECT_ID('Estudiante_Roles');
GO
SELECT name, object_id, type_desc FROM sys.indexes WHERE object_id = OBJECT_ID('Semestre');
GO
SELECT name, object_id, type_desc FROM sys.indexes WHERE object_id = OBJECT_ID('Inscripcion');
GO