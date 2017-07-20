use ShalomSystemsAnimalDB
go
ALTER DATABASE ShalomSystemsAnimalDB
SET COMPATIBILITY_LEVEL = 110;  --   //100  = 2008 ;110 = 2012
GO
SELECT @@VERSION

SELECT compatibility_level  
FROM sys.databases WHERE name = 'ShalomSystemsAnimalDB'; 


Use ShalomSystemsAnimalDB

if OBJECT_ID('') is not null
drop table

 CREATE SCHEMA ShalomSystemsAnimalDB AUTHORIZATION dbo;
 go

 ALTER AUTHORIZATION ON SCHEMA::ShalomSystemsAnimalDB TO dbo;    
GO

----------------------------------------------------------------------------
 USE ShalomSystemsAnimalDB
 GO


 
 CREATE SEQUENCE ShalomSystemsAnimalDB.CountySeq as  BIGINT
  START WITH 100
  INCREMENT BY 1
  MINVALUE 1
  NO MAXVALUE
  NO CYCLE
  CACHE 10;

 IF OBJECT_ID('countrytbl','U')IS NOT NULL
 DROP TABLE  countrytbl
 create table countrytbl
 (
 countryid   BIGINT  PRIMARY KEY not null DEFAULT(NEXT VALUE FOR ShalomSystemsAnimalDB.CountySeq),
 countryname varchar (30) not null,
 continent varchar(30) not null
 )
 select * from countrytbl

 insert into countrytbl values(NEXT VALUE FOR ShalomSystemsAnimalDB.CountySeq,'South Africa','Africa');
 insert into countrytbl values(NEXT VALUE FOR ShalomSystemsAnimalDB.CountySeq,'South Sudan','Africa');
 insert into countrytbl values(NEXT VALUE FOR ShalomSystemsAnimalDB.CountySeq,'Zimbabwe','Africa');
 
 -------------------------------------------------------------------------------
  drop sequence ShalomSystemsAnimalDB.ProvinceSeq
  CREATE SEQUENCE ShalomSystemsAnimalDB.ProvinceSeq as  BIGINT	
  START WITH 100
  INCREMENT BY 1
  MINVALUE 1
  NO MAXVALUE
  NO CYCLE
  CACHE 10;
 -- ALTER SEQUENCE ShalomSystemsAnimalDB.ProvinceSeq
 -- RESTART  

 IF OBJECT_ID('Provincetbl','U')IS NOT NULL
 DROP TABLE  Provincetbl
 create table Provincetbl 
 (
  Provinceid              BIGINT  PRIMARY KEY not null DEFAULT(NEXT VALUE FOR ShalomSystemsAnimalDB.ProvinceSeq),
  Provincename            varchar (30) not null,
  countryID       BIGINT references countryTbl(countryid) not null
 )
 insert into Provincetbl values(NEXT VALUE FOR ShalomSystemsAnimalDB.ProvinceSeq,'Limpopo',100);
 insert into Provincetbl values(NEXT VALUE FOR ShalomSystemsAnimalDB.ProvinceSeq,'Gauteng',101);
 insert into Provincetbl values(NEXT VALUE FOR ShalomSystemsAnimalDB.ProvinceSeq,'Freestate',102);
 select * from Provincetbl;


 -------------------------------------------------------------------------------
 drop sequence ShalomSystemsAnimalDB.CitySeq
  CREATE SEQUENCE ShalomSystemsAnimalDB.CitySeq as  BIGINT	
  START WITH 100
  INCREMENT BY 1
  MINVALUE 1
  NO MAXVALUE
  NO CYCLE
  CACHE 10;
  ALTER SEQUENCE ShalomSystemsAnimalDB.CitySeq
  RESTART  

 IF OBJECT_ID('citytbl','U')IS NOT NULL
 DROP TABLE  citytbl
 create table citytbl 
 (
  cityid              BIGINT  PRIMARY KEY not null DEFAULT(NEXT VALUE FOR ShalomSystemsAnimalDB.CitySeq),
  cityname            varchar (30) not null,
  GpsCoOrdinatesLong  varchar (30),
  GpsCoOrdinatesLat   varchar (30),
  Provinceid       BIGINT references Provincetbl(Provinceid) not null
 )

 insert into citytbl values(NEXT VALUE FOR ShalomSystemsAnimalDB.CitySeq,'Polokwane','23.8962° S','29.4486° E',100);
 insert into citytbl values(NEXT VALUE FOR ShalomSystemsAnimalDB.CitySeq,'Tzaneen','30.8962° S','29.4486° E',101);
 insert into citytbl values(NEXT VALUE FOR ShalomSystemsAnimalDB.CitySeq,'Musina','50.8962° S','29.4486° E',102);


--ALTER TABLE citytbl
--ADD ProvinceID BIGINT references Provincetbl(Provinceid) 

--ALTER TABLE citytbl
--Drop Column ProvinceID 

  select * from citytbl;
 -------------------------------------------------------------------------------

  CREATE SEQUENCE ShalomSystemsAnimalDB.PersonSeq as  bigint
  START WITH 100
  INCREMENT BY 1
  MINVALUE 1
  NO MAXVALUE
  NO CYCLE
  CACHE 10;
  ALTER SEQUENCE ShalomSystemsAnimalDB.PersonSeq
  RESTART 

 IF OBJECT_ID('PersonTbl','U')IS NOT NULL
 DROP TABLE PersonTbl
 create table PersonTbl
 (
  personid BIGINT PRIMARY KEY not null DEFAULT (NEXT VALUE FOR ShalomSystemsAnimalDB.PersonSeq),
  personname      varchar (30) not null,
  personSurname   varchar (30) not null,
  personGender    varchar (30) not null,
  personDOB       varchar (30) not null,
  AreaCode int NULL,
  Address varchar(50) NULL,
  Phone varchar(50) NULL,
  cityid          BIGINT references cityTbl(cityid) not null
 )
 insert into  PersonTbl values(NEXT VALUE FOR ShalomSystemsAnimalDB.PersonSeq,'Stervanos','Semosa','Male','1994/04/13',null,'--','0836647059',100)
 insert into  PersonTbl values(NEXT VALUE FOR ShalomSystemsAnimalDB.PersonSeq,'John','Doe','Male','1994/04/13',null,'<>','0836647079',101)
 insert into  PersonTbl values(NEXT VALUE FOR ShalomSystemsAnimalDB.PersonSeq,'Ted','Brown','Male','1994/04/13',null,'[]','0836647099',102)
 select * from  PersonTbl;
 
 -------------------------------------------------------------------------------

  IF OBJECT_ID('UserTbl','U')IS NOT NULL
 DROP TABLE UserTbl
 create table UserTbl
 (
  Userid BIGINT PRIMARY KEY not null DEFAULT (NEXT VALUE FOR ShalomSystemsAnimalDB.PersonSeq),
  Username      varchar (30) not null,
  UserPassword  varchar (30) not null,
  UserAccess    bit not null,
  personid      BIGINT references PersonTbl(personid) not null
 )
 insert into  UserTbl values(NEXT VALUE FOR ShalomSystemsAnimalDB.PersonSeq,'Stervanos','Semosa',0,100)
 insert into  UserTbl values(NEXT VALUE FOR ShalomSystemsAnimalDB.PersonSeq,'John','Doe',0,101)
 insert into  UserTbl values(NEXT VALUE FOR ShalomSystemsAnimalDB.PersonSeq,'Ted','Brown',1,102)

 -------------------------------------------------------------------------------------
 IF OBJECT_ID('UserLogTbl','U')IS NOT NULL
 DROP TABLE UserLogTbl
 create table UserLogTbl
 (
  logid BIGINT IDENTITY(1,1) PRIMARY KEY,
  logidDesc      nvarchar (500) not null,
  logiddatetime  datetime DEFAULT null,
  personid      BIGINT FOREIGN KEY references PersonTbl(personid) not null
 )
 
 insert into  UserLogTbl(logidDesc,personid) values('Stervanos logged',100)
 insert into  UserLogTbl values(null,'John Doe',0,101)
 insert into  UserLogTbl values(null,'Ted  Brown',1,102)

 select * from UserLogTbl;

 ------------------------------------------------------------------------------------------------
ALTER SEQUENCE ShalomSystemsAnimalDB.AnimalSeq
RESTART  
CREATE SEQUENCE ShalomSystemsAnimalDB.AnimalSeq as  BIGINT
START WITH 100
INCREMENT BY 1
MINVALUE 1
NO MAXVALUE
NO CYCLE
CACHE 10;

USE ShalomSystemsAnimalDB
GO
If OBJECT_ID('AnimalTbl','U') is not null
drop table AnimalTbl
create table AnimalTbl
(
--animal details
AnimalTid                BIGINT PRIMARY KEY not null DEFAULT (NEXT VALUE FOR ShalomSystemsAnimalDB.AnimalSeq),
AnimalType               VARCHAR(30),
AnimalBreed              VARCHAR(30),
AnimalCustomID	         VARCHAR(30),
AnimalCurOwnerSignature  VARCHAR(30),
AnimalGender             varchar(10), ---age to be saved in months
AnimalAge                decimal(3,2),
AnimalYear               int,
AnimalMonth              int,
AnimalDay                int,
AnimalStatus             VARCHAR(30),
--owner details
IdentificationChar      varchar(30),
PERSONID                BIGINT FOREIGN KEY REFERENCES PERSONTbl(personid) NOT NULL

)
insert into AnimalTbl values(NEXT VALUE FOR ShalomSystemsAnimalDB.AnimalSeq,'CATTLE','Brahman','AJS1202','AJS','Male',2.02,2012,02,02,'Alive','AJS',(SELECT TOP 1 personid FROM PersonTbl))
insert into AnimalTbl values(NEXT VALUE FOR ShalomSystemsAnimalDB.AnimalSeq,'CATTLE','Brahman','AJS1203','AJS','Male',2.02,2012,02,02,'Alive','AJS',(SELECT TOP 1 personid FROM PersonTbl))
insert into AnimalTbl values(NEXT VALUE FOR ShalomSystemsAnimalDB.AnimalSeq,'CATTLE','Brahman','AJS1204','AJS','Female',2.02,2012,02,02,'Alive','AJS',(SELECT TOP 1 personid FROM PersonTbl))
insert into AnimalTbl values(NEXT VALUE FOR ShalomSystemsAnimalDB.AnimalSeq,'CATTLE','Brahman','AJS1205','AJS','Female',2.02,2012,02,02,'Alive','AJS',(SELECT TOP 1 personid FROM PersonTbl))
insert into AnimalTbl values(NEXT VALUE FOR ShalomSystemsAnimalDB.AnimalSeq,'CATTLE','Brahman','AJS1204','AJS','Male',2.02,2012,02,02,'Alive','AJS',(SELECT TOP 1 personid FROM PersonTbl))
insert into AnimalTbl values(NEXT VALUE FOR ShalomSystemsAnimalDB.AnimalSeq,'CATTLE','Boran'  ,'AJS12055','AJS','Female',2.02,2012,02,02,'Alive','AJS',(SELECT TOP 1 personid FROM PersonTbl))
insert into AnimalTbl values(NEXT VALUE FOR ShalomSystemsAnimalDB.AnimalSeq,'CATTLE','Brahman','AJS12045','AJS','Female',2.02,2012,02,02,'Alive','AJS',(SELECT TOP 1 personid FROM PersonTbl))
insert into AnimalTbl values(NEXT VALUE FOR ShalomSystemsAnimalDB.AnimalSeq,'CATTLE','Brahman','AJS12054','AJS','Female',2.02,2012,02,02,'Dead','AJS',(SELECT TOP 1 personid FROM PersonTbl))
insert into AnimalTbl values(NEXT VALUE FOR ShalomSystemsAnimalDB.AnimalSeq,'CATTLE','Boran'  ,'AJS120457','AJS','Male',2.02,2012,02,02,'Dead','AJS',(SELECT TOP 1 personid FROM PersonTbl))
select * from AnimalTbl

-------------------------------------------------------------------------------------------------------------------------


If OBJECT_ID('CattleTbl','U') is not null
drop table CattleTbl
create table CattleTbl
(
--Cattle details
CattleID   BIGINT IDENTITY(1,1) PRIMARY KEY,
CattleParentFatherID   BIGINT,
CattleParentMotherID   BIGINT,
CattleImage            varbinary(max) ,      
CattleScotralSize            decimal(5,2),
CattleColor                  varchar(30),
CattleBreedingStatus         varchar(30),
CattleFrameSize              varchar(30),
CattleBirthWeight            decimal(5,2),
CattleWeaningWeight          decimal(5,2),
CattlePostWeaningWeight      decimal(5,2),
CattleAdultSxWeight          decimal(8,2),
CattleCurrentAdultWeight     decimal(8,2),
CattleCurrentWeightDateTaken Datetime ,
--Animal Details
AnimalTid         BIGINT REFERENCES AnimalTbl(AnimalTid) NOT NULL
)

-----parents
 insert into CattleTbl(CattleParentFatherID   ,CattleParentMotherID    ,CattleImage  ,       CattleScotralSize      ,CattleColor             ,CattleBreedingStatus   ,CattleFrameSize           ,CattleBirthWeight   ,   CattleWeaningWeight    ,CattlePostWeaningWeight ,CattleAdultSxWeight    ,CattleCurrentAdultWeight  ,	   CattleCurrentWeightDateTaken , AnimalTid )
 values (0,0, (SELECT BulkColumn FROM Openrowset( Bulk 'C:\Users\StervanosMatomeSemos\Downloads\Pictures\Bull400.jpg', Single_Blob) as image) , 130.20,'Grey','Breeding','Large', 10.2,100.2,400.2,900.8,800.8, (SELECT SYSDATETIME()),100)

  insert into CattleTbl(CattleParentFatherID   ,CattleParentMotherID    ,CattleImage            ,   CattleScotralSize      ,CattleColor             ,CattleBreedingStatus   ,CattleFrameSize           ,CattleBirthWeight   ,CattleWeaningWeight    ,CattlePostWeaningWeight ,CattleAdultSxWeight    ,CattleCurrentAdultWeight  , CattleCurrentWeightDateTaken , AnimalTid )
 values (0,0, (SELECT BulkColumn FROM Openrowset( Bulk 'C:\Users\StervanosMatomeSemos\Downloads\Pictures\JVZ13218.JPG', Single_Blob) as image), 130.20,'Red','Breeding','Large', 10.2,100.2,400.2,900.8,800.8, (SELECT SYSDATETIME()),101 )


  insert into CattleTbl(CattleParentFatherID   ,CattleParentMotherID    ,CattleImage            ,    CattleScotralSize      ,CattleColor             ,CattleBreedingStatus   ,CattleFrameSize           ,CattleBirthWeight   ,CattleWeaningWeight    ,CattlePostWeaningWeight ,CattleAdultSxWeight    ,CattleCurrentAdultWeight  ,CattleCurrentWeightDateTaken , AnimalTid )
 values (0,0, (SELECT BulkColumn FROM Openrowset( Bulk 'C:\Users\StervanosMatomeSemos\Downloads\Pictures\403.jpg', Single_Blob) as image), 130.20,'Grey','Breeding','Large', 10.2,100.2,400.2,900.8,800.8, (SELECT SYSDATETIME()),102 )
   
 insert into CattleTbl(CattleParentFatherID   ,CattleParentMotherID    ,CattleImage            ,  CattleScotralSize      ,CattleColor             ,CattleBreedingStatus   ,CattleFrameSize           ,CattleBirthWeight   , CattleWeaningWeight    ,CattlePostWeaningWeight ,CattleAdultSxWeight    ,CattleCurrentAdultWeight  ,  CattleCurrentWeightDateTaken , AnimalTid )
 values (0,0, (SELECT BulkColumn FROM Openrowset( Bulk 'C:\Users\StervanosMatomeSemos\Downloads\Pictures\402.jpg', Single_Blob) as image), 130.20,'Red','Breeding','Large', 10.2,100.2,400.2,900.8,800.8, (SELECT SYSDATETIME()),103 )

 ---Children
 insert into CattleTbl(CattleParentFatherID   ,CattleParentMotherID    ,CattleImage            ,  CattleScotralSize      ,CattleColor             ,CattleBreedingStatus   ,CattleFrameSize           ,CattleBirthWeight   , CattleWeaningWeight    ,CattlePostWeaningWeight ,CattleAdultSxWeight    ,CattleCurrentAdultWeight  ,  CattleCurrentWeightDateTaken , AnimalTid )
 values (100,102, (SELECT BulkColumn FROM Openrowset( Bulk 'C:\Users\StervanosMatomeSemos\Downloads\Pictures\1B.jpg', Single_Blob) as image), 130.20,'Grey','Breeding','Large', 10.2,100.2,400.2,900.8,800.8, (SELECT SYSDATETIME()),104 )

 insert into CattleTbl(CattleParentFatherID   ,CattleParentMotherID    ,CattleImage            ,  CattleScotralSize      ,CattleColor             ,CattleBreedingStatus   ,CattleFrameSize           ,CattleBirthWeight   , CattleWeaningWeight    ,CattlePostWeaningWeight ,CattleAdultSxWeight    ,CattleCurrentAdultWeight  ,  CattleCurrentWeightDateTaken , AnimalTid )
 values (101,103, (SELECT BulkColumn FROM Openrowset( Bulk 'C:\Users\StervanosMatomeSemos\Downloads\Pictures\mas6-08.jpg', Single_Blob) as image), 130.20,'Red','Breeding','Large', 10.2,100.2,400.2,900.8,800.8, (SELECT SYSDATETIME()),108 )

 insert into CattleTbl(CattleParentFatherID   ,CattleParentMotherID    ,CattleImage            ,  CattleScotralSize      ,CattleColor             ,CattleBreedingStatus   ,CattleFrameSize           ,CattleBirthWeight   , CattleWeaningWeight    ,CattlePostWeaningWeight ,CattleAdultSxWeight    ,CattleCurrentAdultWeight  ,  CattleCurrentWeightDateTaken , AnimalTid )
 values (100,102, (SELECT BulkColumn FROM Openrowset( Bulk 'C:\Users\StervanosMatomeSemos\Downloads\Pictures\1F.jpg', Single_Blob) as image), 130.20,'Grey','Breeding','Large', 10.2,100.2,400.2,900.8,800.8, (SELECT SYSDATETIME()),105 )

 insert into CattleTbl(CattleParentFatherID   ,CattleParentMotherID    ,CattleImage            ,  CattleScotralSize      ,CattleColor             ,CattleBreedingStatus   ,CattleFrameSize           ,CattleBirthWeight   , CattleWeaningWeight    ,CattlePostWeaningWeight ,CattleAdultSxWeight    ,CattleCurrentAdultWeight  ,  CattleCurrentWeightDateTaken , AnimalTid )
 values (100,103, (SELECT BulkColumn FROM Openrowset( Bulk 'C:\Users\StervanosMatomeSemos\Downloads\Pictures\2F.jpg', Single_Blob) as image), 130.20,'Grey','Breeding','Large', 10.2,100.2,400.2,900.8,800.8, (SELECT SYSDATETIME()),106 )

 insert into CattleTbl(CattleParentFatherID   ,CattleParentMotherID    ,CattleImage            ,  CattleScotralSize      ,CattleColor             ,CattleBreedingStatus   ,CattleFrameSize           ,CattleBirthWeight   , CattleWeaningWeight    ,CattlePostWeaningWeight ,CattleAdultSxWeight    ,CattleCurrentAdultWeight  ,  CattleCurrentWeightDateTaken , AnimalTid )
 values (100,102, (SELECT BulkColumn FROM Openrowset( Bulk 'C:\Users\StervanosMatomeSemos\Downloads\Pictures\3F.jpg', Single_Blob) as image), 130.20,'Grey','Breeding','Large', 10.2,100.2,400.2,900.8,800.8, (SELECT SYSDATETIME()),107 )

 select * from CattleTbl;
-------------------------------------------------------------------------------------------------------------------------




If OBJECT_ID('AnimalWeightsTbl','U') is not null
drop table AnimalWeightsTbl
create table AnimalWeightsTbl
(
  AnimalWeightID   BIGINT IDENTITY(1,1) PRIMARY KEY,
  AnimalWeight     decimal(5,2) not null,
  AnimalWeightDate DateTime   null,
  CattleID BIGINT REFERENCES CattleTbl(CattleID) NOT NULl
)
insert into AnimalWeightsTbl(AnimalWeight,AnimalWeightDate, CattleID ) values((select CattleAdultSxWeight from CattleTbl where CattleID=1 ),(select CattleCurrentWeightDateTaken from CattleTbl where CattleID=1 ),1)
insert into AnimalWeightsTbl(AnimalWeight,AnimalWeightDate, CattleID) values((select CattleAdultSxWeight from CattleTbl where CattleID=2 ),(select CattleCurrentWeightDateTaken from CattleTbl where CattleID=2 ),2)
insert into AnimalWeightsTbl(AnimalWeight,AnimalWeightDate, CattleID) values((select CattleAdultSxWeight from CattleTbl where CattleID=3 ),(select CattleCurrentWeightDateTaken from CattleTbl where CattleID=3 ),3)
insert into AnimalWeightsTbl(AnimalWeight,AnimalWeightDate, CattleID) values((select CattleAdultSxWeight from CattleTbl where CattleID=4 ),(select CattleCurrentWeightDateTaken from CattleTbl where CattleID=4 ),4)
insert into AnimalWeightsTbl(AnimalWeight,AnimalWeightDate, CattleID) values((select CattleAdultSxWeight from CattleTbl where CattleID=5 ),(select CattleCurrentWeightDateTaken from CattleTbl where CattleID=5 ),5)
insert into AnimalWeightsTbl(AnimalWeight,AnimalWeightDate, CattleID) values((select CattleAdultSxWeight from CattleTbl where CattleID=6 ),(select CattleCurrentWeightDateTaken from CattleTbl where CattleID=6 ),6)
insert into AnimalWeightsTbl(AnimalWeight,AnimalWeightDate, CattleID) values((select CattleAdultSxWeight from CattleTbl where CattleID=7 ),(select CattleCurrentWeightDateTaken from CattleTbl where CattleID=7 ),7)
insert into AnimalWeightsTbl(AnimalWeight,AnimalWeightDate, CattleID) values((select CattleAdultSxWeight from CattleTbl where CattleID=8 ),(select CattleCurrentWeightDateTaken from CattleTbl where CattleID=8 ),8)
insert into AnimalWeightsTbl(AnimalWeight,AnimalWeightDate, CattleID) values((select CattleAdultSxWeight from CattleTbl where CattleID=9 ),(select CattleCurrentWeightDateTaken from CattleTbl where CattleID=9 ),9)

insert into AnimalWeightsTbl(AnimalWeight,AnimalWeightDate, CattleID ) values((select CattleCurrentAdultWeight from CattleTbl where CattleID=1 ),(select CattleCurrentWeightDateTaken from CattleTbl where CattleID=1 ),1)
insert into AnimalWeightsTbl(AnimalWeight,AnimalWeightDate, CattleID) values((select  CattleCurrentAdultWeight from CattleTbl where CattleID=2 ),(select CattleCurrentWeightDateTaken from CattleTbl where CattleID=2 ),2)
insert into AnimalWeightsTbl(AnimalWeight,AnimalWeightDate, CattleID) values((select  CattleCurrentAdultWeight from CattleTbl where CattleID=3 ),(select CattleCurrentWeightDateTaken from CattleTbl where CattleID=3 ),3)
insert into AnimalWeightsTbl(AnimalWeight,AnimalWeightDate, CattleID) values((select CattleCurrentAdultWeight from CattleTbl where CattleID=4 ),(select CattleCurrentWeightDateTaken from CattleTbl where CattleID=4 ),4)
insert into AnimalWeightsTbl(AnimalWeight,AnimalWeightDate, CattleID) values((select CattleCurrentAdultWeight from CattleTbl where CattleID=5 ),(select CattleCurrentWeightDateTaken from CattleTbl where CattleID=5 ),5)
insert into AnimalWeightsTbl(AnimalWeight,AnimalWeightDate, CattleID) values((select CattleCurrentAdultWeight from CattleTbl where CattleID=6 ),(select CattleCurrentWeightDateTaken from CattleTbl where CattleID=6 ),6)
insert into AnimalWeightsTbl(AnimalWeight,AnimalWeightDate, CattleID) values((select CattleCurrentAdultWeight from CattleTbl where CattleID=7 ),(select CattleCurrentWeightDateTaken from CattleTbl where CattleID=7 ),7)
insert into AnimalWeightsTbl(AnimalWeight,AnimalWeightDate, CattleID) values((select CattleCurrentAdultWeight from CattleTbl where CattleID=8 ),(select CattleCurrentWeightDateTaken from CattleTbl where CattleID=8 ),8)
insert into AnimalWeightsTbl(AnimalWeight,AnimalWeightDate, CattleID) values((select CattleCurrentAdultWeight from CattleTbl where CattleID=9 ),(select CattleCurrentWeightDateTaken from CattleTbl where CattleID=9 ),9)


 select * from AnimalWeightsTbl
-------------------------------------------------------------------------------------------------------------------------








----------------------------------Procedure Queries
---Proc 1 
select * from userTbl
select personid from [dbo].[UserTbl] where Username='Stervanos' and  Userpassword='Semosa' and  UserAccess = '0'  ---validate user

---Proc 2
select * from personTbl;
select P.personid, P.personname,P.personSurname,P.personGender,P.personDOB,P.AreaCode,P.Address,P.Phone,U.UserAccess,
       Ct.cityname, Ct.GpsCoOrdinatesLong, Ct.GpsCoOrdinatesLat,Pc.ProvinceName,cy.countryname,Cy.[continent]
from PersonTBL as P
inner Join userTbl as U
on P.personid = U.personid
inner join citytbl  as Ct 
on  Ct.cityid = P.cityid
inner join Provincetbl  as PC
on  PC.provinceid = Ct.provinceid
inner join countrytbl  as Cy
on  Cy.countryid = Ct.countryid
where  U.personid = '100'              ---------------Get user profiles

--select * from citytbl
--select * from Provincetbl
--select * from countrytbl


---Proc 3
select * from AnimalTBL
select A.PERSONID,
       A.AnimalTid	, A.AnimalType,	 A.AnimalBreed,	 A.AnimalCustomID,	 A.AnimalCurOwnerSignature,	 A.AnimalGender,
       A.AnimalAge,	 A.AnimalYear,	 A.AnimalMonth	, A.AnimalDay,	 A.AnimalStatus,	 A.IdentificationChar,
       C.CattleParentFatherID,	C.CattleParentMotherID	,C.CattleImage	,C.CattleScotralSize	,C.CattleColor	,C.CattleBreedingStatus	,
	   C.CattleFrameSize	,C.CattleBirthWeight	,C.CattleWeaningWeight	,C.CattlePostWeaningWeight	,C.CattleAdultSxWeight,	
	   C.CattleCurrentAdultWeight,	C.CattleCurrentWeightDateTaken	
from  AnimalTbl as A
inner Join PersonTBL as P
on P.personid = A.personid
inner Join cattleTBL as C
on C.AnimalTid = A.AnimalTid
where A.personid = '100'               --------------- Get user Animals 

---Proc 4
------Get All Animal Weights
select AnimalWeight,AnimalWeightDate 
from AnimalWeightsTbl 
where CattleID ='1'


---Proc 5
select * from cattleTBL
select A.PERSONID,
       A.AnimalTid	, A.AnimalType,	 A.AnimalBreed,	 A.AnimalCustomID,	 A.AnimalCurOwnerSignature,	 A.AnimalGender,
       A.AnimalAge,	 A.AnimalYear,	 A.AnimalMonth	, A.AnimalDay,	 A.AnimalStatus,	 A.IdentificationChar,
       C.CattleParentFatherID,	C.CattleParentMotherID	,C.CattleImage	,C.CattleScotralSize	,C.CattleColor	,C.CattleBreedingStatus	,
	   C.CattleFrameSize	,C.CattleBirthWeight	,C.CattleWeaningWeight	,C.CattlePostWeaningWeight	,C.CattleAdultSxWeight,	
	   C.CattleCurrentAdultWeight,	C.CattleCurrentWeightDateTaken	
from  AnimalTbl as A
inner Join PersonTBL as P
on P.personid = A.personid
inner Join cattleTBL as C
on C.AnimalTid = A.AnimalTid
where A.AnimalTid = '100'             ------------- Get Specific Cattle

---Proc 4 Ref
------Get All Animal Weights
select AnimalWeight,AnimalWeightDate 
from AnimalWeightsTbl 
where CattleID ='1'


create table ValidateProcTbl
( 
 data varchar(30)
)

------------------------------Create Procedures----------------------
---validate user

drop procedure ValidateUserProc;
Create procedure ValidateUserProc
@Uname  varchar(100),
@Upassword varchar (100),
@access varchar(2)
as
--variable to store userID
declare @UID varchar(100)
declare @UIDF int
set @UID = ( select ISNULL(personid ,'RED')
							from UserTbl
							where Username    = @Uname     and  
							Userpassword= @Upassword and 
						    UserAccess  = @access )
                      
insert into ValidateProcTbl values(@UID+' : '+(SELECT convert(varchar(25), getdate(), 120)));
if(@UID is null) ----Fix Validation
   Begin
   --user does not exist 
	      set @UIDF=0
   end
else
   Begin
   --user exists
          set @UIDF=1
   end

insert into ValidateProcTbl values('Trigger : '+(SELECT convert(varchar(25), getdate(), 120)));

return @UIDF

---

---Login Procedure

use ShalomSystemsAnimalDB
go
create procedure LoginUser
@Uname  varchar(100),
@Upassword varchar (100),
@access bit
as
--variable to store userID
declare @UID int
set @UID = (select personid 
            from UserTbl
            where Username    = @Uname     and  
			      Userpassword= @Upassword and 
				  UserAccess  = @access 
          );
return @UID
go



---Get user Profiles
-- drop procedure  GetUserProfile
Create Procedure GetUserProfile
@UID Bigint
as

select P.personid, P.personname,P.personSurname,P.personGender,P.personDOB,P.AreaCode,P.Address,P.Phone,U.UserAccess,
       Ct.cityname, Ct.GpsCoOrdinatesLong, Ct.GpsCoOrdinatesLat,Pc.ProvinceName,cy.countryname,Cy.[continent],P.RegSeq
from PersonTBL as P
inner Join userTbl as U
on P.personid = U.personid
inner join citytbl  as Ct 
on  Ct.cityid = P.cityid
inner join Provincetbl  as PC
on  PC.provinceid = Ct.provinceid
inner join countrytbl  as Cy
on  Cy.countryid = PC.countryid
where  U.personid = 100              
go

select * from  Provincetbl
select * from citytbl; 

--Get UserAnimal
use ShalomSystemsAnimalDB
go
--drop procedure GetAllAnimals;
create procedure GetAllAnimals
@UID Bigint
as

select A.PERSONID,
       A.AnimalTid	, A.AnimalType,	 A.AnimalBreed,	 A.AnimalCustomID,	 A.AnimalCurOwnerSignature,	 A.AnimalGender,
       A.AnimalAge,	 A.AnimalYear,	 A.AnimalMonth	, A.AnimalDay,	 A.AnimalStatus,	 A.AnimalCurOwnerSignature,
       C.CattleParentFatherID,	C.CattleParentMotherID	,C.CattleImage	,C.CattleScotralSize	,C.CattleColor	,C.CattleBreedingStatus	,
	   C.CattleFrameSize	,C.CattleBirthWeight	,C.CattleWeaningWeight	,C.CattlePostWeaningWeight	,C.CattleAdultSxWeight,	
	   C.CattleCurrentAdultWeight,	C.CattleCurrentWeightDateTaken	
from  AnimalTbl as A
inner Join PersonTBL as P
on P.personid = A.personid
inner Join cattleTBL as C
on C.AnimalTid = A.AnimalTid
where A.personid = @UID
go

--Get Offspring
use ShalomSystemsAnimalDB
go
--drop procedure GetAnimalOffspring
create procedure GetAnimalOffspring
@UID Bigint,
@AID Bigint,
@ParentGender varchar(100)
as
if(@ParentGender = 'Male')
	begin

		select A.PERSONID,
			   A.AnimalTid	, A.AnimalType,	 A.AnimalBreed,	 A.AnimalCustomID,	 A.AnimalCurOwnerSignature,	 A.AnimalGender,
			   A.AnimalAge,	 A.AnimalYear,	 A.AnimalMonth	, A.AnimalDay,	 A.AnimalStatus,	 A.AnimalCurOwnerSignature,
			   C.CattleParentFatherID,	C.CattleParentMotherID	,C.CattleImage	,C.CattleScotralSize	,C.CattleColor	,C.CattleBreedingStatus	,
			   C.CattleFrameSize	,C.CattleBirthWeight	,C.CattleWeaningWeight	,C.CattlePostWeaningWeight	,C.CattleAdultSxWeight,	
			   C.CattleCurrentAdultWeight,	C.CattleCurrentWeightDateTaken	
		from  AnimalTbl as A
		inner Join PersonTBL as P
		on P.personid = A.personid
		inner Join cattleTBL as C
		on C.AnimalTid = A.AnimalTid
		where A.personid = @UID and  C.CattleParentFatherID = @AID 
	end 
else
	begin
		select A.PERSONID,
			   A.AnimalTid	, A.AnimalType,	 A.AnimalBreed,	 A.AnimalCustomID,	 A.AnimalCurOwnerSignature,	 A.AnimalGender,
			   A.AnimalAge,	 A.AnimalYear,	 A.AnimalMonth	, A.AnimalDay,	 A.AnimalStatus,	 A.AnimalCurOwnerSignature,
			   C.CattleParentFatherID,	C.CattleParentMotherID	,C.CattleImage	,C.CattleScotralSize	,C.CattleColor	,C.CattleBreedingStatus	,
			   C.CattleFrameSize	,C.CattleBirthWeight	,C.CattleWeaningWeight	,C.CattlePostWeaningWeight	,C.CattleAdultSxWeight,	
			   C.CattleCurrentAdultWeight,	C.CattleCurrentWeightDateTaken	
		from  AnimalTbl as A
		inner Join PersonTBL as P
		on P.personid = A.personid
		inner Join cattleTBL as C
		on C.AnimalTid = A.AnimalTid
		where A.personid = @UID and  C.CattleParentMotherID = @AID 
	end 
go


---Get Animal Specific Weights   
Create Procedure GetSpecAnimalHsWeights
@AID BIGINT,
@UID BIGINT
as
select AWT.ANIMALWEIGHT,AWT.ANIMALWEIGHTDATE
from AnimalWeightsTbl as AWT
inner join CattleTbl as CT
on CT.CattleID  = AWT.CattleID
inner join ANIMALTBL as A
on CT.AnimalTid = A.AnimalTid
where A.AnimalTid =@AID and A.PERSONID=@UID
go

---Get Specific Animal 
Create Procedure GetSpecAnimal
@AID BIGINT
as
select A.PERSONID,
       A.AnimalTid	, A.AnimalType,	 A.AnimalBreed,	 A.AnimalCustomID,	 A.AnimalCurOwnerSignature,	 A.AnimalGender,
       A.AnimalAge,	 A.AnimalYear,	 A.AnimalMonth	, A.AnimalDay,	 A.AnimalStatus,	 A.IdentificationChar,
       C.CattleParentFatherID,	C.CattleParentMotherID	,C.CattleImage	,C.CattleScotralSize	,C.CattleColor	,C.CattleBreedingStatus	,
	   C.CattleFrameSize	,C.CattleBirthWeight	,C.CattleWeaningWeight	,C.CattlePostWeaningWeight	,C.CattleAdultSxWeight,	
	   C.CattleCurrentAdultWeight,	C.CattleCurrentWeightDateTaken	
from  AnimalTbl as A
inner Join PersonTBL as P
on P.personid = A.personid
inner Join cattleTBL as C
on C.AnimalTid = A.AnimalTid
where A.AnimalTid = @AID
go

use [ShalomSystemsAnimalDB]
go
select  (NEXT VALUE FOR [ShalomSystemsAnimalDB].[AnimalSeq]) as nextID
go


use [ShalomSystemsAnimalDB]
go
--drop procedure  GetNewAnimalID
create procedure GetNewAnimalID
as

  --get new sequence id to insert a new user with 
  Declare  @SQL  Nvarchar(1000)
  Declare  @NewNum Bigint

  --selection 
  Set @SQL  = 'SELECT @NewNum = Next Value for [ShalomSystemsAnimalDB].[AnimalSeq] '
  Exec sp_executesql @Sql,N'@NewNum bigint output',@NewNum output 
  return @NewNum

go







---insert animal into database
create procedure InsertNewAnimal




as
go






 USE ShalomSystemsAnimalDB
 GO
 create table InsertQueryTest
 (
AnimalTid Bigint,AnimalType varchar(30),AnimalBreed varchar(30),AnimalCustomID varchar(30) ,AnimalCurOwnerSignature varchar(30),
AnimalGender varchar(30),AnimalAge  decimal(3,2),AnimalYear int,AnimalMonth int,AnimalDay int,AnimalStatus varchar(30)
 )

 select  * from InsertQueryTest
 drop table InsertQueryTest

 select * from AnimalTbl;
 select * from cattleTbl;