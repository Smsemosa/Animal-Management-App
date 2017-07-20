use master
go

--if db_id('ShalomSystemsAnimalDB')is not null
--drop database ShalomSystemsAnimalBD
 

CREATE DATABASE ShalomSystemsAnimalDB
ON PRIMARY
(NAME=ShalomAnimalsDBMaster,
FILENAME=N'C:\Users\StervanosMatomeSemos\Desktop\TechShifter-Projects\Database\DataFiles\ShalomSystemsAnimal.mdf',
SIZE=3MB,
MAXSIZE=UNLIMITED,
FILEGROWTH=2%
)

LOG ON
(NAME=ShalomAnimalsDBldf,
FILENAME = N'C:\Users\StervanosMatomeSemos\Desktop\TechShifter-Projects\Database\DataFiles\ShalomSystemsAnimal.ldf',
SIZE=3MB,
MAXSIZE=UNLIMITED,
FILEGROWTH=2%
)

use master
go
ALTER DATABASE  ShalomSystemsAnimalDB
ADD FILE
(
NAME=ShalomAnimalsDBndf,
FILENAME=N'C:\Users\StervanosMatomeSemos\Desktop\TechShifter-Projects\Database\DataFiles\ShalomSystemsAnimal.ndf',
SIZE=3MB,
MAXSIZE=UNLIMiTED,
FILEGROWTH=2%
)

USE ShalomSystemsAnimalDB
GO
SELECT compatibility_level
FROM sys.databases WHERE name = 'ShalomSystemsAnimalDB';
GO

select * from Provincetbl
--//changes compatibility_level
use ShalomSystemsAnimalDB
go
ALTER DATABASE ShalomSystemsAnimalDB
SET COMPATIBILITY_LEVEL = 110;  --   //100  = 2008 ;110 = 2012
GO