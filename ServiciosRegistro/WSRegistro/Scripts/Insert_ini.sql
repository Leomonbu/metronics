USE [bdMetronics]

GO
INSERT INTO [dbo].[Roles]
(NameRol, DescriptionRol)
SELECT 'Administrador', 'Tiene acceso a todas las funciones de la aplicacion'
UNION ALL
SELECT 'Estudiante', 'Tiene acceso el registro de materias'
GO

INSERT INTO [dbo].[Profesor]
(NombreProfesor, ApellidosProfesor, EstadoProfesor)
SELECT 'Pedro Eduardo', 'Caceres', 1
UNION ALL
SELECT 'Camilo Federico', 'Perez', 1
UNION ALL
SELECT 'Agustin', 'Maquiavelo', 1
UNION ALL
SELECT 'Carolina', 'Rodriguez', 1
UNION ALL
SELECT 'Diana Daniela', 'Camelo', 1
GO

INSERT INTO [dbo].[Materia]
(NombreMateria,	Creditos,	EstadoMateria,	IdProfesor)
SELECT 'Constitucion Politica', 3, 1, 1
UNION ALL
SELECT 'Derecho constitucional', 3, 1, 1
UNION ALL
SELECT 'Informatica básica', 3, 1, 2
UNION ALL
SELECT 'Informatica avanzada', 3, 1, 2
UNION ALL
SELECT 'Estadistica', 3, 1, 3
UNION ALL
SELECT 'Probabilidad', 3, 1, 3
UNION ALL
SELECT 'Matematica básica', 3, 1, 4
UNION ALL
SELECT 'Matematica avanzada', 3, 1, 4
UNION ALL
SELECT 'Fisica electronica', 3, 1, 5
UNION ALL
SELECT 'Fisica relativa', 3, 1, 5
GO

INSERT INTO [dbo].[Semestre]
(NombreSemestre, FechaInicial, FechaFinal)
VALUES
('2025-II', '2025-07-01 00:00:00', '2025-12-01 23:59:59')
GO