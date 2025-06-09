USE [bdMetronics]
GO

-- ========================
--         ROLES
-- ========================
CREATE TABLE [dbo].[Roles] (
	[IdRol][int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [NameRol][VARCHAR](50) NOT NULL,
    [DescriptionRol][VARCHAR] (MAX)
);
GO
CREATE UNIQUE INDEX idx_roles_name ON roles(NameRol);
GO

-- ========================
--      ESTUDIANTE
-- ========================
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

-- ================================
--      ESTUDIANTE - ROL
-- ================================
CREATE TABLE  [dbo].[Estudiante_Roles] (
	[IdEstudiante][INT] NOT NULL,
    [IdRole][INT] NOT NULL,
    PRIMARY KEY (IdEstudiante, IdRole),
    FOREIGN KEY (IdRole) REFERENCES Roles(IdRol) ON DELETE CASCADE
);
GO
CREATE INDEX idx_student_roles_user_id ON Estudiante_Roles(IdEstudiante);
GO
CREATE INDEX idx_student_roles_role_id ON Estudiante_Roles(IdRole);
GO

-- ========================
--       PROFESOR
-- ========================
CREATE TABLE [dbo].[Profesor](
	[IdProfesor] [int] IDENTITY(1,1) NOT NULL,
	[NombreProfesor] [varchar](100) NOT NULL,
	[ApellidosProfesor] [varchar](100) NOT NULL,
	[EstadoProfesor] [int] NOT NULL,
	PRIMARY KEY (IdProfesor)
);
GO
CREATE UNIQUE INDEX idx_profesor_id ON profesor(IdProfesor);
GO

-- ========================
--	      MATERIA
-- ========================
CREATE TABLE [dbo].[Materia](
	[IdMateria] [int] IDENTITY(1,1) NOT NULL,
	[NombreMateria] [varchar](70) NOT NULL,
	[Creditos] [int] NOT NULL,
	[EstadoMateria] [int] NOT NULL,
	[IdProfesor] [int] NOT NULL,
	PRIMARY KEY (IdMateria),
    FOREIGN KEY (IdProfesor) REFERENCES Profesor(IdProfesor) ON DELETE CASCADE
);
GO

CREATE UNIQUE INDEX idx_materia_codigo ON materia(IdMateria);
GO
CREATE INDEX idx_materia_profesor_id ON materia(IdProfesor);
GO

-- ========================
--       SEMESTRE
-- ========================
CREATE TABLE [dbo].[Semestre](
	[IdSemestre] [int] IDENTITY(1,1) NOT NULL,
	[NombreSemestre] [varchar](70) NOT NULL,    -- Ej: "2025-I", "2025-II"
	[FechaInicial] [datetime] NOT NULL,
	[FechaFinal] [datetime] NOT NULL,
CONSTRAINT [PK_semestre] PRIMARY KEY CLUSTERED
 (
	[IdSemestre] ASC
 )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE UNIQUE INDEX idx_semestre_nombre ON semestre(IdSemestre);
GO

-- ========================
--		INSCRIPCIÓN
-- ========================
CREATE TABLE [dbo].[Inscripcion](
	[IdInscripcion] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[IdEstudiante] [int] NOT NULL,
	[IdMateria] [int] NOT NULL,
	[IdSemestre] [int] NOT NULL,
	[FechaInscripcion] [datetime] NOT NULL DEFAULT GETDATE(),
	[FechaModificacion] [datetime],
	[EstadoInscripcion] [int] NOT NULL,
	FOREIGN KEY (IdEstudiante) REFERENCES estudiante(IdEstudiante) ON DELETE CASCADE,
    FOREIGN KEY (IdMateria) REFERENCES materia(IdMateria) ON DELETE CASCADE,
    FOREIGN KEY (IdSemestre) REFERENCES semestre(IdSemestre) ON DELETE CASCADE
	);
GO
CREATE INDEX idx_inscripcion_estudiante ON inscripcion(IdEstudiante);
GO
CREATE INDEX idx_inscripcion_materia ON inscripcion(IdMateria);
GO
CREATE INDEX idx_inscripcion_semestre ON inscripcion(IdSemestre);
GO