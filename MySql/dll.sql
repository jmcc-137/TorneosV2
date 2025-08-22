DROP DATABASE IF EXISTS TorneosGestor;
CREATE DATABASE TorneosGestor;
USE TorneosGestor;

-- Tabla Torneo
CREATE TABLE IF NOT EXISTS Torneo (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Nombre VARCHAR(100),
    Pais VARCHAR(100),
    Ciudad VARCHAR(100),
    Ifecha DATE NOT NULL,
    Ffecha DATE NOT NULL
);

-- Tabla Equipo
CREATE TABLE IF NOT EXISTS Equipo (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    Pais VARCHAR(100) NOT NULL
);

-- Relación N:M entre Torneo y Equipo
CREATE TABLE IF NOT EXISTS TorneoEquipos (
    TorneoId INT,
    EquipoId INT,
    PRIMARY KEY (TorneoId, EquipoId),
    CONSTRAINT FK_Torneo FOREIGN KEY (TorneoId) REFERENCES Torneo(Id),
    CONSTRAINT FK_Equipo FOREIGN KEY (EquipoId) REFERENCES Equipo(Id)
);
