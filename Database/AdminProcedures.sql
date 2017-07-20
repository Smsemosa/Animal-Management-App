

use ShalomAnimalsDBC
go
create procedure LoginUser
@Uname  varchar(100),
@Upassword varchar (100)
as

--variable to store userID
declare @UID int
set @UID = (select p.personid from dbo.PersonTbl as p where @Uname = p.loginUname and @Upassword = p.loginPass);
return @UID
go

--EXEC dbo.LoginUser @Uname='Stervanos' ,@Upassword='12345'
--select * from dbo.PersonTbl

--get user details
create procedure GetUserDetails
@UID  varchar(100)
as
select * from dbo.PersonTbl as p where p.personid = @UID;
go 

 IF OBJECT_ID('GetAllTypesOfAnimals','P')IS NOT NULL
 DROP Procedure  GetAllTypesOfAnimals
create  procedure GetAllTypesOfAnimals
@UID  varchar(100)
as 
select *
from AnimalTbl as a 
inner join CattleTbl as c
on c.AnimalTid=a.AnimalTid
inner join PersonTbl as p
on p.personid = a.PERSONID

where a.PERSONID = @UID
go



 IF OBJECT_ID('GetOneAnimal','P')IS NOT NULL
 DROP Procedure  GetOneAnimal

create procedure GetOneAnimal
@UID  varchar(100),
@AnimalID varchar(100)
as 
select *
from AnimalTbl as a 
inner join CattleTbl as c
on c.AnimalTid=a.AnimalTid
inner join PersonTbl as p
on p.personid = a.personid
where a.PERSONID = @UID and a.AnimalTid = @AnimalID
go



 IF OBJECT_ID('GetOffspring','P')IS NOT NULL
 DROP Procedure  GetOffspring
 EXEC dbo.GetOffSpring @UID='100' ,@AnimalID='100',@Gender='Male'

create procedure GetOffSpring
@UID  varchar(100),
@AnimalID varchar(100),
@Gender varchar(100)
as 

IF(@Gender = 'Male')
	select *
	from AnimalTbl as a 
	inner join CattleTbl as c
	on c.AnimalTid=a.AnimalTid
	inner join PersonTbl as p
	on p.personid = a.personid
	where a.PERSONID =  @UID and  c.CattleParentFatherID=@AnimalID;


ELSE IF (@Gender = 'Female')
	select *
	from AnimalTbl as a 
	inner join CattleTbl as c
	on c.AnimalTid=a.AnimalTid
	inner join PersonTbl as p
	on p.personid = a.personid
	where a.PERSONID =  @UID and  c.CattleParentMotherID=@AnimalID;
else

	select *
	from AnimalTbl as a 
	inner join CattleTbl as c
	on c.AnimalTid=a.AnimalTid
	inner join PersonTbl as p
	on p.personid = a.personid
	where a.PERSONID = 0;

go
commit

 USE ShalomSystemsAnimalDB
 GO
drop procedure AddnewCattle;
 USE ShalomSystemsAnimalDB
 GO
create procedure  AddnewCattle
@AnimalTid Bigint,@AnimalType varchar(30),@AnimalBreed varchar(30),@AnimalCustomID varchar(30) ,@AnimalCurOwnerSignature varchar(30),
@AnimalGender varchar(30),@AnimalAge decimal,@AnimalYear int,@AnimalMonth int,@AnimalDay int,@AnimalStatus varchar(30),

@CattleParentFatherID	bigint,
@CattleParentMotherID	bigint,
@CattleImage	varchar(max),
@CattleScotralSize	decimal,
@CattleColor	varchar(30),
@CattleBreedingStatus	varchar(30),
@CattleFrameSize	varchar(30),
@CattleBirthWeight	decimal,
@CattleWeaningWeight	decimal,
@CattlePostWeaningWeight	decimal,
@CattleAdultSxWeight	decimal,
@CattleCurrentAdultWeight	decimal,
@CattleCurrentWeightDateTaken	datetime


as 
declare @state int



insert into AnimalTbl (AnimalTid,AnimalType,AnimalBreed,AnimalCustomID,AnimalCurOwnerSignature,AnimalGender,
                       AnimalAge,AnimalYear,AnimalMonth,AnimalDay,AnimalStatus,PERSONID)
               values (@AnimalTid,@AnimalType,@AnimalBreed,@AnimalCustomID,@AnimalCurOwnerSignature,@AnimalGender
			          ,@AnimalAge ,@AnimalYear,@AnimalMonth  ,@AnimalDay ,@AnimalStatus,100);

 insert into CattleTbl(CattleParentFatherID,CattleParentMotherID,
					   CattleImage,CattleScotralSize,CattleColor,CattleBreedingStatus,
					   CattleFrameSize,CattleBirthWeight,CattleWeaningWeight,
					   CattlePostWeaningWeight,CattleAdultSxWeight,CattleCurrentAdultWeight,
					   CattleCurrentWeightDateTaken,AnimalTid)
				values( @CattleParentFatherID	,@CattleParentMotherID	,@CattleImage,@CattleScotralSize	,
						@CattleColor	,@CattleBreedingStatus,@CattleFrameSize,
						@CattleBirthWeight	,@CattleWeaningWeight	,@CattlePostWeaningWeight	,
						@CattleAdultSxWeight	,@CattleCurrentAdultWeight	,convert(datetime,@CattleCurrentWeightDateTaken),@AnimalTid);



set @state = @@ROWCOUNT;
return @state
--insert  into the cattle table

--CAST(YourVarcharCol AS INT) 
--select * from  PersonTbl
--alter table AnimalTbl
--drop column IdentificationChar

-- USE ShalomSystemsAnimalDB
-- GO
--select * from AnimalTbl;


go















------------------------------
exec sp_columns AnimalTbl

 USE ShalomSystemsAnimalDB
 GO
 EXEC sp_help CattleTbl;

  USE ShalomSystemsAnimalDB
 GO
 EXEC sp_help AnimalTbl;
   USE ShalomSystemsAnimalDB
 GO
 alter table cattleTbl
 alter column CattleImage  nvarchar(max)