CREATE DATABASE UserManagement;
Go

USE UserManagement;
GO

CREATE TABLE UserRole(
	RoleId INT PRIMARY KEY IDENTITY(350,1),
	RoleName nvarchar(100)
	);
	GO

CREATE TABLE UserDetail(
	UserId INT PRIMARY KEY IDENTITY(3500,1),
	UserEmail nvarchar(20),
	Password nvarchar(30),
	RoleId INT FOREIGN KEY REFERENCES UserRole(RoleId)
	);
	GO
	
INSERT INTO UserRole(RoleName)
Values('Admin'),
('Doctor'),
('Patient');
GO
-----------------------------------------------------------------
CREATE DATABASE Doctor;
Go

USE Doctor;
GO

CREATE TABLE DoctorDetail(
	UserId INT PRIMARY KEY,
	Name nvarchar(20),
	Contact BigInt,
	Address nvarchar(200),
	Gender nvarchar(20),
	Age INT,
	Qualification nvarchar(100)
	);
	GO
CREATE TABLE Speciality(
	specialityId INT PRIMARY KEY Identity(250,1),
	SpecialityName Nvarchar(100)
	);
	GO

CREATE TABLE DoctorSpecialities(
	Id INT PRIMARY KEY Identity(1,1), 
	UserId INT FOREIGN KEY REFERENCES DoctorDetail(UserId),
	specialityId INT FOREIGN KEY REFERENCES Speciality(SpecialityId)
);
GO


INSERT INTO Speciality(SpecialityName)
VALUES('Dermatology'),
('Pediatrics'),
('Orthopedics'),
('Ophthalmology'),
('ENT'),
('Pathology');
GO
--------------------------------------------------------------------------------------------------------------
CREATE DATABASE Patient;
Go

USE Patient;
GO

CREATE TABLE PatientDetail(
	UserId INT PRIMARY KEY,
	Name nvarchar(20),
	Contact BigInt,
	Address nvarchar(200),
	Gender nvarchar(20),
	Age INT,
	Qualification nvarchar(100)
	);
	Go
-------------------------------------------------------------------------------------------------------
CREATE DATABASE Appointment;
Go

USE Appointment;
Go

CREATE TABLE Appointment(
AppointmentId INT IDENTITY(5000,1) PRIMARY KEY,
PatientUserId INT,
DoctorUserId INT,
Date DateTime,
HealthIssue nvarchar(200),
Status nvarchar(30)
);
Go

CREATE TABLE Prescription(
ID INT IDENTITY(1,1) PRIMARY KEY,
AppointmentId INT Foreign Key References AppointMent(AppointmentId),
Prescription nvarchar(1000));
Go

CREATE TABLE Feedback(
ID INT IDENTITY(1,1) PRIMARY KEY,
AppointmentId INT Foreign Key References AppointMent(AppointmentId),
Rating smallint,
Review nvarchar(100));
Go
-------------------------------------------------------------------------------------