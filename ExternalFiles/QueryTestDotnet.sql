CREATE DATABASE Catalogos;
USE Catalogos;

-- Tabla Proyecto
CREATE TABLE Proyecto
(
	ProyectoID int not null identity(1,1)
  , Nombre varchar(50) not null
  , constraint PK_Proyecto 
		primary key(ProyectoID)
);

-- Tabla Producto
CREATE TABLE Producto
(
	ProductoID int not null identity(1,1)
  , Descripcion varchar(50) not null
  , constraint PK_Producto 
		primary key(ProductoID)
);

-- Tabla Relación Proyecto y Producto
CREATE TABLE Producto_Proyecto
(
	Producto_ProyectoID int not null identity(1,1)
  , ProyectoID int not null
  , ProductoID int not null
  , constraint PK_Producto_Proyecto
		primary key(Producto_ProyectoID)
  , constraint FK_Proyecto_FK_Producto
		foreign key(ProyectoID) references Proyecto(ProyectoID)
  , constraint FK_Producto_FK_Proyecto
		foreign key(ProductoID) references Producto(ProductoID)
);

-- Tabla Tipo de Mensaje
CREATE TABLE Tipo
(
	Cod_Tipo int not null identity(1,1)
  , Nombre varchar(50)
  , constraint PK_TipoMensaje
		primary key(Cod_Tipo)
);

-- Tabla Tipo Información
CREATE TABLE Tipo_Informacion
(
	Cod_Tipo_Informacion int not null identity(1,1)
  , Nombre varchar(50)
  , constraint PK_TipoInformacion
		primary key(Cod_Tipo_Informacion)
);

-- Tabla Formato de Mensaje
CREATE TABLE Formato_Mensaje
(
	Cod_Formato_Mensaje int not null identity(1,1)
  , Cod_Tipo int not null
  , Cod_Tipo_Informacion int not null
  , Nombre varchar(50)
  , Remitente varchar(50)
  , Asunto varchar(50)
  , constraint PK_FormatoInformacion
		primary key(Cod_Formato_Mensaje)
  , constraint FK_CodTipo
		foreign key(Cod_Tipo) references Tipo(Cod_Tipo)
  , constraint FK_CodTipoInformacion
		foreign key(Cod_Tipo_Informacion) references Tipo_Informacion(Cod_Tipo_Informacion)
);

-- Tabla Mensaje
CREATE TABLE Mensaje
(
	Cod_Mensaje int not null identity(1,1)
  , Cod_Formato_Mensaje int not null
  , Producto_ProyectoID int not null
  , constraint PK_Mensaje
		primary key(Cod_Mensaje)
  , constraint FK_CodFormatoMensaje
		foreign key(Cod_Formato_Mensaje) references Formato_Mensaje(Cod_Formato_Mensaje)
  , constraint FK_ProductoProyecto
		foreign key(Producto_ProyectoID) references Producto_Proyecto(Producto_ProyectoID)
);

-- Insertar los Proyectos
INSERT INTO Proyecto(Nombre) 
VALUES
	('Premia')
   ,('Konmi')
   ,('Yujule');

-- Insertar los Productos
INSERT INTO Producto(Descripcion) 
VALUES
	('Premia Clásica')
   ,('Premia Oro')
   ,('Premia Platinum')
   ,('Konmi Clásica')
   ,('Konmi Oro')
   ,('Konmi Platinum');   

-- Insertar los Tipos de Mensajes
INSERT INTO Tipo(Nombre) 
VALUES
	('SMS')
   ,('Mail')
   ,('Mensaje en el estado de cuenta');


-- Insertar los Tipo Información
INSERT INTO Tipo_Informacion(Nombre) 
VALUES
	('Mensaje de Bienvenida')
   ,('Mensaje de Mora')
   ,('Mensaje de Promoción')
   ,('Otros');

-- Insertar los Formatos de Mensaje
INSERT INTO Formato_Mensaje(Cod_Tipo, Cod_Tipo_Informacion, Nombre, Remitente, Asunto)
VALUES
	(1, 1, 'Fernando Aguilar', 'Promerica', 'Bienvenida')
   ,(2, 1, 'Elvis Dueñas', 'Promerica', 'Saludo')
   ,(3, 1, 'María Esquivel', 'Promerica', 'Información')
   ,(2, 2, 'Oscar Morales', 'Promerica', 'Mora')
   ,(3, 2, 'Daniel Gonzalez', 'Promerica', 'Mora')
   ,(2, 3, 'Rafael Hernandez', 'Promerica', 'Promoción')
   ,(3, 3, 'Denis Rosales', 'Promerica', 'Promoción')
   ,(1, 1, 'Walter Ríos', 'Promerica', 'Bienvenida')
   ,(3, 1, 'Fredy Cabrera', 'Promerica', 'Información')
   ,(1, 1, 'Luis Perez', 'Promerica', 'Bienvenida')
   ,(2, 1, 'Juan Gómez', 'Promerica', 'Saludo');

-- Insertar en la Relación Producto-Proyecto
INSERT INTO Producto_Proyecto(ProyectoID, ProductoID)
VALUES
	(1,1)
   ,(1,2)
   ,(1,3)
   ,(2,1)
   ,(2,2)
   ,(2,3);

-- Insertar en la tabla Mensaje
INSERT INTO Mensaje(Cod_Formato_Mensaje, Producto_ProyectoID)
VALUES
	(1,1)
   ,(2,2)
   ,(3,3)
   ,(4,4)
   ,(2,1)
   ,(3,1);


-- A. Consulta en SQL que devuelva el nombre del proyecto y sus productos correspondientes del proyecto Premia cuyo código es 1.
SELECT PP.Producto_ProyectoID, Proyecto.Nombre AS Proyecto, Producto.Descripcion AS Producto 
	FROM Producto_Proyecto PP
		INNER JOIN Proyecto Proyecto ON PP.ProyectoID = Proyecto.ProyectoID
		INNER JOIN Producto Producto ON PP.ProductoID = Producto.ProductoID
			WHERE Proyecto.ProyectoID = 1;

-- B. Consulta SQL que devuelva los distintos mensajes que hay, indicando a qué proyecto y producto pertenecen.
SELECT M.Cod_Mensaje, 
	   FM.Cod_Tipo, Tipo.Nombre AS TipoMensaje, 
	   FM.Cod_Tipo_Informacion, TipoInformacion.Nombre AS TipoInformacion,
	   FM.Nombre, FM.Remitente, FM.Asunto,
	   M.Producto_ProyectoID, Proyecto.Nombre AS Proyecto, Producto.Descripcion AS Producto
	FROM Mensaje M 
		INNER JOIN Formato_Mensaje FM ON M.Cod_Formato_Mensaje = FM.Cod_Formato_Mensaje
		INNER JOIN Tipo Tipo ON FM.Cod_Tipo = Tipo.Cod_Tipo
		INNER JOIN Tipo_Informacion TipoInformacion ON FM.Cod_Tipo_Informacion = TipoInformacion.Cod_Tipo_Informacion
		INNER JOIN Producto_Proyecto PP ON M.Producto_ProyectoID = PP.Producto_ProyectoID
		INNER JOIN Proyecto Proyecto ON PP.ProyectoID = Proyecto.ProyectoID
		INNER JOIN Producto Producto ON PP.ProductoID = Producto.ProductoID		