USE master;
GO

-- crea la bd si no existe
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'PruebaSeven')
BEGIN
    CREATE DATABASE PruebaSeven;
    PRINT 'Base de datos PruebaSeven creada.';
END
GO

USE PruebaSeven;
GO

-- crear la tabla CAT_ESTADO_CIVIL
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'CAT_ESTADO_CIVIL')
BEGIN
    CREATE TABLE CAT_ESTADO_CIVIL (
        id_estado INT PRIMARY KEY IDENTITY(1,1),
        descripcion VARCHAR(50) NOT NULL
    );
    INSERT INTO CAT_ESTADO_CIVIL (descripcion) VALUES ('Soltero/a'), ('Casado/a'), ('Viudo/a'), ('Divorciado/a');
    PRINT 'Tabla CAT_ESTADO_CIVIL creada.';
END
GO

-- crea la tabla USUARIOS
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'USUARIOS')
BEGIN
    CREATE TABLE USUARIOS (
        id_usuario INT PRIMARY KEY IDENTITY(1,1),
        username VARCHAR(50) NOT NULL UNIQUE,
        password VARCHAR(50) NOT NULL
    );
    INSERT INTO USUARIOS (username, password) 
    VALUES ('admin', '1234'), ('fernando', 'seven2026');
    PRINT 'Tabla USUARIOS creada e inicializada.';
END
GO

-- crea la tabla SEVECLIE
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'SEVECLIE')
BEGIN
    CREATE TABLE SEVECLIE(
        id_clie INT PRIMARY KEY IDENTITY(1,1),
        cedula VARCHAR(10) NOT NULL UNIQUE,
        nombre VARCHAR(100) NOT NULL,
        genero CHAR(1) CHECK (genero IN ('M', 'F')),
        fecha_nac DATE NOT NULL,
        id_estado_civil INT FOREIGN KEY REFERENCES CAT_ESTADO_CIVIL(id_estado)
    );

    INSERT INTO SEVECLIE (cedula, nombre, genero, fecha_nac, id_estado_civil)
    VALUES ('0924836480', 'Fernando Reyes', 'M', '1988-04-24', 1);
    PRINT 'Tabla SEVECLIE creada.';
END
GO

-- procedimiento almacenado sp_ValidarUsuario
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_ValidarUsuario')
    DROP PROCEDURE sp_ValidarUsuario;
GO

CREATE PROCEDURE sp_ValidarUsuario
    @user VARCHAR(50),
    @pass VARCHAR(50)
AS
BEGIN
    SELECT id_usuario, username, password 
    FROM USUARIOS 
    WHERE username = @user AND password = @pass;
END
GO

-- procedimiento almacenado sp_MantenimientoCliente
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_MantenimientoCliente')
    DROP PROCEDURE sp_MantenimientoCliente;
GO

CREATE PROCEDURE sp_MantenimientoCliente
    @id_clie INT = 0,
    @cedula VARCHAR(10) = NULL,
    @nombre VARCHAR(100) = NULL,
    @genero CHAR(1) = NULL,
    @fecha_nac DATE = NULL,
    @id_estado_civil INT = NULL,
    @accion VARCHAR(10)
AS
BEGIN
    IF @accion = 'INSERT'
    BEGIN
        INSERT INTO SEVECLIE (cedula, nombre, genero, fecha_nac, id_estado_civil)
        VALUES (@cedula, @nombre, @genero, @fecha_nac, @id_estado_civil);
    END
    ELSE IF @accion = 'UPDATE'
    BEGIN
        UPDATE SEVECLIE 
        SET cedula = @cedula, nombre = @nombre, genero = @genero, 
            fecha_nac = @fecha_nac, id_estado_civil = @id_estado_civil
        WHERE id_clie = @id_clie;
    END
    ELSE IF @accion = 'DELETE'
    BEGIN
        DELETE FROM SEVECLIE WHERE id_clie = @id_clie;
    END
    ELSE IF @accion = 'SELECT'
    BEGIN
        SELECT c.id_clie, c.cedula, c.nombre, c.genero, c.fecha_nac, c.id_estado_civil, e.descripcion as EstadoCivil
        FROM SEVECLIE c
        INNER JOIN CAT_ESTADO_CIVIL e ON c.id_estado_civil = e.id_estado
        WHERE c.nombre LIKE '%' + ISNULL(@nombre,'') + '%' 
           OR c.cedula LIKE '%' + ISNULL(@nombre,'') + '%';
    END
END
GO

PRINT 'Configuración finalizada correctamente.';