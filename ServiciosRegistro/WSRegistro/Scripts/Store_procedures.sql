USE [bdMetronics]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ======================================================
-- Author:		Leonardo Monroy	
-- Description:	Consulta estudiantes compañeros del user
-- ======================================================

CREATE PROCEDURE [dbo].[SelectClassmatesUser]
	-- Add the parameters for the stored procedure here
		@studentId INT,
		@materiaId INT
	AS
BEGIN
	SET NOCOUNT ON;
	SELECT [Ins].[IdInscripcion], [Mat].[NombreMateria], [Est].[NombresEstudiante] + ' ' + [Est].[ApellidosEstudiante] AS [NombreEstudiante]
	FROM [dbo].[Inscripcion] [Ins] 
		INNER JOIN [dbo].[Estudiante] [Est] ON [Ins].[IdEstudiante] = [Est].[IdEstudiante]
		INNER JOIN [dbo].[Materia] [Mat] ON [Ins].[IdMateria] = [Mat].[IdMateria]	
	WHERE [Ins].[IdEstudiante] != @studentId AND [Mat].[IdMateria] = @materiaId
		AND [Ins].[EstadoInscripcion] = 1 AND [Mat].[EstadoMateria] = 1
END
GO