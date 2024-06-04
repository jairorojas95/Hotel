-- Crear la base de datos si no existe
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'HotelManagementDb')
BEGIN
    CREATE DATABASE HotelManagementDb;
END
GO

-- Usar la base de datos
USE HotelManagementDb;
GO

-- Crear el esquema si no existe
IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = 'dbo')
BEGIN
    EXEC('CREATE SCHEMA dbo');
END
GO

-- Crear tabla Hotel
CREATE TABLE dbo.Hotel (
	hotelId int IDENTITY(1,1) NOT NULL,
	name nvarchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	description nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	active bit DEFAULT 1 NOT NULL,
	CONSTRAINT PK_Hotel PRIMARY KEY (hotelId)
);
GO

-- Crear tabla Passenger
CREATE TABLE dbo.Passenger (
	passengerId int IDENTITY(1,1) NOT NULL,
	firstName nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	lastName nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	birthDate date NOT NULL,
	gender nvarchar(10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	documentType nvarchar(20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	documentNumber nvarchar(20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	email nvarchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	phoneNumber nvarchar(10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	fullNameEmergency varchar(250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	phoneNumberEmergency varchar(10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	CONSTRAINT PK_Passenger PRIMARY KEY (passengerId)
);
GO

-- Crear tabla Room
CREATE TABLE dbo.Room (
	roomId int IDENTITY(1,1) NOT NULL,
	hotelId int NOT NULL,
	baseCost decimal(10,2) NOT NULL,
	taxes decimal(10,2) NOT NULL,
	roomType nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	location nvarchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	active bit DEFAULT 1 NOT NULL,
	CONSTRAINT PK_Room PRIMARY KEY (roomId),
	CONSTRAINT FK_Room_Hotel FOREIGN KEY (hotelId) REFERENCES dbo.Hotel(hotelId)
);
GO

-- Crear tabla Reservations
CREATE TABLE dbo.Reservations (
	reservationId int IDENTITY(1,1) NOT NULL,
	roomId int NOT NULL,
	checkInDate date NOT NULL,
	checkOutDate date NOT NULL,
	numPersons int NOT NULL,
	destinationCity nvarchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	reservationStatus nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS DEFAULT 'Reservado' NOT NULL,
	passengerId int NULL,
	CONSTRAINT PK_Reservations PRIMARY KEY (reservationId),
	CONSTRAINT FK_Reservation_Passenger FOREIGN KEY (passengerId) REFERENCES dbo.Passenger(passengerId),
	CONSTRAINT FK_Reservation_Room FOREIGN KEY (roomId) REFERENCES dbo.Room(roomId)
);
GO
