USE MASTER
GO

IF EXISTS(SELECT NAME FROM sys.sysdatabases WHERE NAME = 'HotelManagement')
DROP DATABASE HotelManagement
GO

CREATE DATABASE HotelManagement
ON
(
	NAME='HotelManagement_data',
	FILENAME='E:\HotelManagement_data.mdf',
	Size=10MB,
	MaxSize=1GB,
	FileGrowth=10%
)
LOG ON
(
	NAME='HotelManagement_log',
	FILENAME='E:\HotelManagement_log.ldf',
	Size=10MB,
	MaxSize=1GB,
	FileGrowth=10%
)
GO

USE HotelManagement
GO

CREATE TABLE tblRoomType 
(
	RoomType_ID INT PRIMARY KEY IDENTITY,
	RoomType VARCHAR(50) NOT NULL,
)
GO

CREATE TABLE tblDesignation
(
	DesignationId INT PRIMARY KEY IDENTITY,
	Designation VARCHAR(30) NOT NULL
)
GO

CREATE TABLE tblCustomer
(
	Customer_ID INT PRIMARY KEY IDENTITY,
	Customer_Name VARCHAR(50) NOT NULL,
	Phone_Number VARCHAR(15) NOT NULL,
	Email VARCHAR(50) NOT NULL,
	Gender VARCHAR(50) NOT NULL,
	Customer_Address VARCHAR(200) NOT NULL,
	CheckIn DATETIME NOT NULL,
	CheckOut DATETIME NOT NULL,
	Room_No INT NOT NULL,
	RoomType_ID INT REFERENCES tblRoomType(RoomType_ID),
	Picture VARBINARY(MAX) NULL
)
GO

CREATE TABLE tblEmployee 
(
	Employee_ID INT PRIMARY KEY IDENTITY,
	Employee_Name VARCHAR(50) NOT NULL,
	Phone_Number VARCHAR(15) NOT NULL,
	Email VARCHAR(50) NOT NULL,
	Gender VARCHAR(50) NOT NULL,
	Employee_Address VARCHAR(200) NOT NULL,
	Joining_Date DATETIME NOT NULL,
	Salary MONEY NOT NULL,
	DesignationId INT REFERENCES tblDesignation(DesignationId),
	Picture VARBINARY(MAX) NULL
)
GO

CREATE TABLE tblReservation
(
	Reservation_ID INT PRIMARY KEY IDENTITY,
	Name VARCHAR(50) NOT NULL,
	Phone int NOT NULL,
	CheckIn DATETIME NOT NULL,
	CheckOut DATETIME NOT NULL,
	TotalDays INT NOT NULL,
	TotalCost MONEY NOT NULL,
	RoomType_ID INT REFERENCES tblRoomType(RoomType_ID)
)
GO

CREATE TABLE tblUser
(
	UserId INT PRIMARY KEY IDENTITY,
	Username VARCHAR(30) UNIQUE NOT NULL,
	Phone int NOT NULL,
	Email VARCHAR(30) NOT NULL,
	Password VARCHAR(32) NOT NULL,
	ReTypePassword VARCHAR(32) NOT NULL,
	Picture VARBINARY(MAX) NULL
)
GO

-------=======*******=======-------
--Some Data Insertation For Text

INSERT INTO tblRoomType VALUES ('--SELECT ONE--'),('Single'),('Double'),('Quad')
GO

INSERT INTO tblDesignation VALUES ('--SELECT ONE--'),('Director '),('Hotel Manager'),('HR Manager'),('Customer Representative'),('Chefs'),('Hotel Staff')
GO


INSERT INTO tblReservation VALUES ('Nazim Uddin',0171556699,'02/05/2020','02/10/2020',5,'8000',1)
INSERT INTO tblReservation VALUES ('Mahtab Khan',01655772244,'02/01/2020','02/07/2020',6,'10000',2)
INSERT INTO tblReservation VALUES ('Shahin Khan',01955446622,'02/05/2020','02/09/2020',4,'12000',3)
INSERT INTO tblReservation VALUES ('Roman Shekh',01599662211,'02/03/2020','02/10/2020',7,'7000',1)
INSERT INTO tblReservation VALUES ('Selim Mia',01866558877,'02/02/2020','02/10/2020',8,'24000',2)
INSERT INTO tblReservation VALUES ('Nazim Uddin',0171556699,'02/05/2020','02/10/2020',5,'8000',1)
INSERT INTO tblReservation VALUES ('Mahtab Khan',01655772244,'02/01/2020','02/07/2020',6,'10000',2)
INSERT INTO tblReservation VALUES ('Shahin Khan',01955446622,'02/05/2020','02/09/2020',4,'12000',3)
INSERT INTO tblReservation VALUES ('Roman Shekh',01599662211,'02/03/2020','02/10/2020',7,'7000',1)
INSERT INTO tblReservation VALUES ('Selim Mia',01866558877,'02/02/2020','02/10/2020',8,'24000',2)
INSERT INTO tblReservation VALUES ('Nazim Uddin',0171556699,'02/05/2020','02/10/2020',5,'8000',1)
INSERT INTO tblReservation VALUES ('Mahtab Khan',01655772244,'02/01/2020','02/07/2020',6,'10000',2)
INSERT INTO tblReservation VALUES ('Shahin Khan',01955446622,'02/05/2020','02/09/2020',4,'12000',3)
INSERT INTO tblReservation VALUES ('Roman Shekh',01599662211,'02/03/2020','02/10/2020',7,'7000',1)
INSERT INTO tblReservation VALUES ('Selim Mia',01866558877,'02/02/2020','02/10/2020',8,'24000',2)
INSERT INTO tblReservation VALUES ('Nazim Uddin',0171556699,'02/05/2020','02/10/2020',5,'8000',1)
INSERT INTO tblReservation VALUES ('Mahtab Khan',01655772244,'02/01/2020','02/07/2020',6,'10000',2)
INSERT INTO tblReservation VALUES ('Shahin Khan',01955446622,'02/05/2020','02/09/2020',4,'12000',3)
INSERT INTO tblReservation VALUES ('Roman Shekh',01599662211,'02/03/2020','02/10/2020',7,'7000',1)
INSERT INTO tblReservation VALUES ('Selim Mia',01866558877,'02/02/2020','02/10/2020',8,'24000',2)
GO

INSERT INTO tblUser VALUES ('Nayon',01855996644,'nayon@gmail.com','1234','1234',null)
INSERT INTO tblUser VALUES ('Uzzal',01655882211,'uzzal@gmail.com','u123','u123',null)
INSERT INTO tblUser VALUES ('Rubel',01988664422,'rubel@gmail.com','r123','r123',null)
GO

SELECT * FROM tblDesignation
SELECT * FROM tblRoomType
SELECT * FROM tblCustomer
SELECT * FROM tblReservation
SELECT * FROM tblEmployee
SELECT * FROM tblUser
GO






