CREATE DATABASE PortalInfoObrasPublicas;

USE PortalInfoObrasPublicas;

CREATE TABLE Usuario (
    IdUsuario INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) UNIQUE NOT NULL,
    PasswordHash NVARCHAR(255) NOT NULL,
    Rol NVARCHAR(50) NOT NULL
);

-- TABLA OBRA

CREATE TABLE Obra (
    IdObra INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(150) NOT NULL,
    Ubicacion NVARCHAR(200),
    Estado NVARCHAR(50),
    Presupuesto DECIMAL(12,2),
    FechaInicio DATE,
    FechaFin DATE
);

-- TABLA REPORTE
CREATE TABLE Reporte (
    IdReporte INT PRIMARY KEY IDENTITY(1,1),
    IdObra INT NOT NULL,
    Descripcion NVARCHAR(MAX),

    CONSTRAINT FK_Reporte_Obra FOREIGN KEY (IdObra)
    REFERENCES Obra(IdObra)
);

-- TABLA IMAGEN
CREATE TABLE ObraImagen (
    Id INT PRIMARY KEY IDENTITY(1,1),
    IdObra INT NOT NULL,
    RutaImagen NVARCHAR(255),

    CONSTRAINT FK_Imagen_Obra FOREIGN KEY (IdObra)
    REFERENCES Obra(IdObra)
);

SELECT name AS Usuarios, create_date
FROM Sys.tables