using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DatabaseAccessLayer;
using BusinessEntities;
using System.Data;
using System.IO;
using System.Text;

namespace ShalomSystem.Models
{
    public static class DataAccessData
    {
        private static DataAccess ac = DataAccess.getInstance();
       // private static DataAccess acv = DataAccess.getInstance();

        public static bool TestsConnection()
        {
          bool state  = true;
          return state;
        }
        //validate user on database
        public static bool ValidateUserLogin(string Uname, string Upassword, bool Uaccess)
        {
            ac.OpenConnection();
            int accessBit = 0;
            if (Uaccess == true)
            {
                accessBit = 1;
            }
            else { accessBit = 0; }
            
            string[] variables = new string[] { "@Uname", "@Upassword", "@access" };
            string[] values = new string[] { Uname, Upassword, accessBit.ToString() };
            int id = 0;
            try
            {
               // DataTable t = ac.Select("select * from Provincetbl");
                //foreach (DataRow row in t.Rows)
                //{
                //    User p = new User(row[1].ToString(), row[2].ToString(), true);
                ////}
                //int ii  = t.Rows.Count;

                id = int.Parse(""+ ac.ExecuteOneValReturnProcedure<int>("ValidateUserProc", variables, values, "@UIDF", SqlDbType.Int, ac));
            
            }
            catch (Exception E)
            {

                string EX = E.Message;
            }
       if (id == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        //get user ID
        public static int LoginUser(string Uname, string Upassword, bool uAccess)
        {
            ac.OpenConnection();
            //parameter and variable of this Procedure
            string[] variables = new string[] { "@Uname", "@Upassword", "@access" };
            string[] values = new string[] { Uname, Upassword, uAccess.ToString() };

            int id = int.Parse(""+ac.ExecuteOneValReturnProcedure<int>("LoginUser", variables, values, "@UIDF", SqlDbType.Int, ac));
            return id;
        
        }
        //get user Details
        public static Person  GetUserDetails(int UId)
        {
            ac.OpenConnection();
            Person  p  = new Person();
            //parameter and variable of this Procedure
            string[] variables = new string[] { "@UID" };
            string[] values = new string[] { "" + UId };
            DataTable data = ac.ExecuteDataTableProcedure("GetUserProfile", variables, values, ac);
           
            //convert datatable info to Person Object
            foreach (DataRow row in data.Rows)
            {
            //    P.personid 0, P.personname 1,P.personSurname 2,P.personGender 3,P.personDOB 4,P.AreaCode 5,P.Address 6,P.Phone 7,U.UserAccess 8,
           //  Ct.cityname 9, Ct.GpsCoOrdinatesLong 10, Ct.GpsCoOrdinatesLat 11,Pc.ProvinceName 12,cy.countryname 13,Cy.[continent] 14


              //  DateTime dd = DateTime.Parse(row[4].ToString());
                //GenderEnum cc = (GenderEnum)Enum.Parse(typeof(GenderEnum), row[3].ToString());
                p = new Person(int.Parse(row[0].ToString()),
                               row[1].ToString(),
                               row[2].ToString(),
                               DateTime.Parse(row[4].ToString()),
                               (GenderEnum)Enum.Parse(typeof(GenderEnum), row[3].ToString()), 
                      
                               row[5].ToString(),
                               row[6].ToString(),
                               row[7].ToString(),
                               bool.Parse(row[8].ToString()),

                               row[12].ToString(),
                               row[9].ToString(),
                               row[13].ToString(),
                               row[14].ToString(),
                               row[11].ToString(),
                               row[10].ToString(),row[15].ToString()
                               );
            }
            return p;
        }
        //get one user LiveStock 

        //get all User   LiveStock
        public static List<Cattle> GetUserAnimals( int UId,Person person)
        {
            ac.OpenConnection();
            List<Cattle> cattle = new List<Cattle>();

            //parameter and variable of this Procedure
            string[] variables = new string [] { "@UID" };
            string[] values = new    string [] { "" + UId };

            //get data from  database 
            DataTable data = ac.ExecuteDataTableProcedure("GetAllAnimals", variables, values, ac);


            if (data.Rows.Count != 0)
            {

                foreach (DataRow row in data.Rows)
                {
                       //A.PERSONID,
                       //A.AnimalTid	        0, A.AnimalType 1,	 A.AnimalBreed 2, A.AnimalCustomID 3,	 A.AnimalCurOwnerSignature 4,	 A.AnimalGender        5,
                       //A.AnimalAge            6, A.AnimalYear 7,	 A.AnimalMonth 8, A.AnimalDay      9,	 A.AnimalStatus            10,	 A.IdentificationChar  11,
                       //C.CattleParentFatherID 12,	C.CattleParentMotherID	13,C.CattleImage	14,C.CattleScotralSize	15,C.CattleColor	16,C.CattleBreedingStatus	17,
                       //C.CattleFrameSize	18,C.CattleBirthWeight	19,C.CattleWeaningWeight	20,C.CattlePostWeaningWeight	21,C.CattleAdultSxWeight 22,	
                       //C.CattleCurrentAdultWeight 23,	C.CattleCurrentWeightDateTaken 24	

                    //offspring
                    List<Cattle> offSpring = new List<Cattle>();
                    #region Enums
                    //frame size
                    FrameSize frmeSize = (FrameSize)Enum.Parse(typeof(FrameSize), row[19].ToString());
                    //Breeding status
                    BreedingStatus bStatus = (BreedingStatus)Enum.Parse(typeof(BreedingStatus), row[18].ToString());
                    //Status Enum
                    StatusEnum sStatus = (StatusEnum)Enum.Parse(typeof(StatusEnum), row[11].ToString());
                    //Gender Enum
                    GenderEnum gEnum = (GenderEnum)Enum.Parse(typeof(GenderEnum), row[6].ToString());
                    //animal type
                    AnimalTypeEnum animalType = (AnimalTypeEnum)Enum.Parse(typeof(AnimalTypeEnum), row[2].ToString());
                    #endregion

                    //weights
                    Dictionary<DateTime, double> weights = DataAccessData.AnimalWeightHistory(person.PId, int.Parse(row[1].ToString()));
                    List<double> weightsnum = weights.Values.ToList();
                    List<DateTime> weightDate = weights.Keys.ToList();
                    cattle.Add
                        (
                        new Cattle
                        (
                               int.Parse(row[1].ToString()), row[3].ToString(), gEnum, double.Parse(row[7].ToString()),
                               int.Parse(row[8].ToString()), int.Parse(row[9].ToString()), int.Parse(row[10].ToString()),
                               animalType, row[4].ToString(), row[5].ToString(), person.PId, person.PId, person.PName, person.PSurname, person.Dob, 14478899, int.Parse(row[13].ToString()), int.Parse(row[14].ToString()),
                             row[15].ToString()
                               ,DataAccessData.GetAnimalsOffSpring(person.PId, int.Parse(row[1].ToString()),gEnum.ToString(),person),
                               sStatus, double.Parse(row[16].ToString()), row[17].ToString(),
                              bStatus, frmeSize,

                              double.Parse(row[20].ToString()), double.Parse(row[21].ToString()), double.Parse(row[22].ToString()),
                              double.Parse(row[23].ToString()), double.Parse(row[24].ToString()), DateTime.Parse(row[25].ToString()), null, weightsnum, weightDate,
                              DateTime.Parse( row[8].ToString()+'/'+ row[9].ToString()+'/'+row[10].ToString())

                        ));

                }
            }



            return cattle;
        }
        //get Offspring
        public static List<Cattle> GetAnimalsOffSpring(int UId, int AnimalID, string gender, Person person)
        {
            ac.OpenConnection();

//            create procedure GetAnimalOffspring
//@UID Bigint,
//@AID Bigint,
//@ParentGender varchar(100)

            List<Cattle> cattle = new List<Cattle>();

            //parameter and variable of this Procedure
            string[] variables = new string[] { "@UID", "@AID", "@ParentGender" };
            string[] values = new string[] { "" + UId, AnimalID.ToString(),gender };

            //get data from  database 
            DataTable data = ac.ExecuteDataTableProcedure("GetAnimalOffspring", variables, values, ac);



            if (data.Rows.Count != 0)
            {

                foreach (DataRow row in data.Rows)
                {
                    //A.PERSONID,
                    //A.AnimalTid	        0, A.AnimalType 1,	 A.AnimalBreed 2, A.AnimalCustomID 3,	 A.AnimalCurOwnerSignature 4,	 A.AnimalGender        5,
                    //A.AnimalAge            6, A.AnimalYear 7,	 A.AnimalMonth 8, A.AnimalDay      9,	 A.AnimalStatus            10,	 A.IdentificationChar  11,
                    //C.CattleParentFatherID 12,	C.CattleParentMotherID	13,C.CattleImage	14,C.CattleScotralSize	15,C.CattleColor	16,C.CattleBreedingStatus	17,
                    //C.CattleFrameSize	18,C.CattleBirthWeight	19,C.CattleWeaningWeight	20,C.CattlePostWeaningWeight	21,C.CattleAdultSxWeight 22,	
                    //C.CattleCurrentAdultWeight 23,	C.CattleCurrentWeightDateTaken 24	

                    //offspring
                    List<Cattle> offSpring = new List<Cattle>();
                    #region Enums
                    //frame size
                    FrameSize frmeSize = (FrameSize)Enum.Parse(typeof(FrameSize), row[19].ToString());
                    //Breeding status
                    BreedingStatus bStatus = (BreedingStatus)Enum.Parse(typeof(BreedingStatus), row[18].ToString());
                    //Status Enum
                    StatusEnum sStatus = (StatusEnum)Enum.Parse(typeof(StatusEnum), row[11].ToString());
                    //Gender Enum
                    GenderEnum gEnum = (GenderEnum)Enum.Parse(typeof(GenderEnum), row[6].ToString());
                    //animal type
                    AnimalTypeEnum animalType = (AnimalTypeEnum)Enum.Parse(typeof(AnimalTypeEnum), row[2].ToString());
                    #endregion

                    //weights
                    List<double> weights = DataAccessData.AnimalWeightHistory(person.PId, int.Parse(row[1].ToString())).Values.ToList();
                    List<DateTime> weightDate = DataAccessData.AnimalWeightHistory(person.PId, int.Parse(row[1].ToString())).Keys.ToList();
                    cattle.Add
                        (
                        new Cattle
                        (
                               int.Parse(row[1].ToString()), row[3].ToString(), gEnum, double.Parse(row[7].ToString()),
                               int.Parse(row[8].ToString()), int.Parse(row[9].ToString()), int.Parse(row[10].ToString()),
                               animalType, row[4].ToString(), row[5].ToString(), person.PId, person.PId, person.PName, person.PSurname, person.Dob, 14478899, int.Parse(row[13].ToString()), int.Parse(row[14].ToString()),
                              row[15].ToString()
                               ,null,
                               sStatus, double.Parse(row[16].ToString()), row[17].ToString(),
                              bStatus, frmeSize,

                              double.Parse(row[20].ToString()), double.Parse(row[21].ToString()), double.Parse(row[22].ToString()),
                              double.Parse(row[23].ToString()), double.Parse(row[24].ToString()), DateTime.Parse(row[25].ToString()), null, weights, weightDate

                        ));

                }
            }


            return cattle;
        }
        //get Animal Weight History
        public static Dictionary<DateTime, double> AnimalWeightHistory(int UId,int AnimalID)
        { 
            //Create Procedure GetSpecAnimalHsWeights
            //@AID BIGINT,
            //@UID BIGINT

            ac.OpenConnection();
            Dictionary<DateTime, double>cattleWeights = new Dictionary<DateTime, double>();

            //parameter and variable of this Procedure
            string[] variables = new string [] { "@AID" ,"@UID"};
            string[] values = new    string [] { AnimalID.ToString(),UId.ToString() };

            //get data from  database 
            DataTable data = ac.ExecuteDataTableProcedure("GetSpecAnimalHsWeights", variables, values, ac);


            if (data.Rows.Count != 0)
            {
                foreach (DataRow row in data.Rows)
                {
                    cattleWeights.Add(DateTime.Parse(row[1].ToString()),double.Parse(row[0].ToString()));
                }
            }
            return cattleWeights;
        }
        public static List<Cattle> GetUserAnimals(int UId ,int AnimalID)
        {
            ac.OpenConnection();


            List<Cattle> cattle = new List<Cattle>();

            //parameter and variable of this Procedure
            string[] variables = new string[] { "@UID", "@AnimalID" };
            string[] values = new string[] { "" + UId, "" + AnimalID };

            //get data from  database 
            DataTable data = ac.ExecuteDataTableProcedure("GetOneAnimal", variables, values, ac);


            if (data.Rows.Count != 0)
            {

                foreach (DataRow row in data.Rows)
                {
                    //offspring
                    List<Cattle> offSpring = new List<Cattle>();

                    //frame size
                    FrameSize frmeSize = (FrameSize)Enum.Parse(typeof(FrameSize), row["CattleFrameSize"].ToString());
                    //Breeding status
                    BreedingStatus bStatus = (BreedingStatus)Enum.Parse(typeof(BreedingStatus), row["CattleBreedingStatus"].ToString());
                    //Status Enum
                    StatusEnum sStatus = (StatusEnum)Enum.Parse(typeof(StatusEnum), row["AnimalStatus"].ToString());
                    //Gender Enum
                    GenderEnum gEnum = (GenderEnum)Enum.Parse(typeof(GenderEnum), row["AnimalGender"].ToString());
                    //animal type
                    AnimalTypeEnum animalType = (AnimalTypeEnum)Enum.Parse(typeof(AnimalTypeEnum), row["AnimalType"].ToString());


                    cattle.Add
                        (
                        new Cattle
                        (
                               int.Parse(row["AnimalTid"].ToString()), row["AnimalBreed"].ToString(), gEnum, double.Parse(row["AnimalAge"].ToString()),
                               int.Parse(row["AnimalYear"].ToString()), int.Parse(row["AnimalMonth"].ToString()), int.Parse(row["AnimalDay"].ToString()),
                               animalType, row[4].ToString(), row[5].ToString(), (int)(Math.Round(double.Parse(row["AnimalAge"].ToString()) / 24)), int.Parse(row["personid"].ToString()),
                               row["personname"].ToString(), row["personSurname"].ToString(), DateTime.Parse(row["personDOB"].ToString()),


                              int.Parse(row["CattleID"].ToString()), int.Parse(row["CattleParentFatherID"].ToString()), int.Parse(row["CattleParentMotherID"].ToString()), (row["CattleImage"].ToString())
                             , offSpring, sStatus, double.Parse(row["CattleScotralSize"].ToString()), row["CattleColor"].ToString(),
                              bStatus, frmeSize,


                              double.Parse(row["CattleBirthWeight"].ToString()), double.Parse(row["CattleWeaningWeight"].ToString()), double.Parse(row["CattlePostWeaningWeight"].ToString()),
                              double.Parse(row["CattleAdultSxWeight"].ToString()), double.Parse(row["CattleCurrentAdultWeight"].ToString()), DateTime.Parse(row["CattleCurrentWeightDateTaken"].ToString()), null, null, null

                        ));

                }
            }



            return cattle;
        }

        public static int GetSeqAddNewAnimal()
        {
            ac.OpenConnection();
            //parameter and variable of this Procedure
            string[] variables = new string[] {};
            string[] values = new string[] {"Sequence"};

            int id = int.Parse("" + ac.ExecuteOneValReturnProcedure<int>("GetNewAnimalID", variables, values, "@NewNum", SqlDbType.Int, ac));
            return id;
        
        }

        public static bool AddNewAnimal(int PersonId  ,Cattle P)
        {
     
            string[] variables = new string[] { "@AnimalTid" ,"@AnimalType" ,"@AnimalBreed" ,"@AnimalCustomID"  ,"@AnimalCurOwnerSignature" ,
                                                "@AnimalGender" ,"@AnimalAge" ,"@AnimalYear" ,"@AnimalMonth" ,"@AnimalDay" ,"@AnimalStatus" ,

                                                "@CattleParentFatherID","@CattleParentMotherID","@CattleImage","@CattleScotralSize",
                                                "@CattleColor","@CattleBreedingStatus","@CattleFrameSize",
                                                "@CattleBirthWeight","@CattleWeaningWeight","@CattlePostWeaningWeight",
                                                "@CattleAdultSxWeight","@CattleCurrentAdultWeight","@CattleCurrentWeightDateTaken"
                                               
                                               };
            string[] values = new string[] { P.AId.ToString(), P.AType.ToString(), P.ABreed, P.ACustomeIdentifaction, P.AIdentifactionChar, P.AGender.ToString(), P.AAge.ToString(),
                                             P.Dob.Year.ToString(), P.Dob.Month.ToString(),P.Dob.Day.ToString(),P.Status.ToString(),
                                             P.CParentFId.ToString(),P.CParentMId.ToString(),P.CImagePath,P.CScotralSize.ToString(),
                                             P.CColor.ToString(),P.CBreedingStatus.ToString(),P.CFrameSize.ToString(),
                                             P.CBirthWeight.ToString(),P.CWeaningWeight.ToString(),P.CPostWeaningWeight.ToString(),
                                             P.CAdultSxWeight.ToString(),P.CCurrentAdultWeight.ToString(),P.CCurrentWeightDateTaken.ToString()

                                           };



            int state = int.Parse("" + ac.ExecuteOneValReturnProcedure<int>("AddnewCattle", variables, values, "@state", SqlDbType.Int, ac));
            return true;
        }
    }
}