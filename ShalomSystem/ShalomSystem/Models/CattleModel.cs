using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessEntities;
using System.Web.Mvc;

namespace ShalomSystem.Models
{
    public sealed class CattleModel:Cattle
    {
        
        //input values for html page
        public List<SelectListItem> GetFrameSize()
        {
            //enumerables for lists 
            List<SelectListItem> frameSizeLs = new List<SelectListItem>();
            //gender
            for (int i = 1; i <= Enum.GetValues(typeof(FrameSize)).Length; i++)
            {
                frameSizeLs.Add(new SelectListItem
                {
                    Text = Enum.GetName(typeof(FrameSize), i),
                    Value = Enum.GetName(typeof(FrameSize), i)
                });
            }
            return frameSizeLs;
        }
        public List<SelectListItem> GetGender()
        {
            //enumerables for lists 
            List<SelectListItem> gender = new List<SelectListItem>();
            //gender
            for (int i = 1; i <= Enum.GetValues(typeof(GenderEnum)).Length; i++)
            {
                gender.Add(new SelectListItem
                {
                    Text = Enum.GetName(typeof(GenderEnum), i),
                    Value = Enum.GetName(typeof(GenderEnum), i)
                });
            }
            return gender;
        }
        public List<SelectListItem> GetStatus()
        {
            //enumerables for lists 
            List<SelectListItem> status = new List<SelectListItem>();
           //status
            for (int i = 1; i <= Enum.GetValues(typeof(StatusEnum)).Length; i++)
            {
                status.Add(new SelectListItem
                {
                    Text = Enum.GetName(typeof(StatusEnum), i),
                    Value = Enum.GetName(typeof(StatusEnum), i)
                });
            }
            return status;
        }
        public List<SelectListItem> GetBreedingStatus()
        {
            //enumerables for lists 
            List<SelectListItem> breedingstatus = new List<SelectListItem>();
            //gender
            for (int i = 1; i <= Enum.GetValues(typeof(BreedingStatus)).Length; i++)
            {
                breedingstatus.Add(new SelectListItem
                {
                    Text = Enum.GetName(typeof(BreedingStatus), i),
                    Value = Enum.GetName(typeof(BreedingStatus), i)
                });
            }
            return breedingstatus;
        }
        public List<SelectListItem> GetBreedType()
        {
            //enumerables for lists 
            List<SelectListItem> breedtype = new List<SelectListItem>();
            //gender
            //get breed types from the database
            List<string> breedtypesLs = new List<string>(new string[] { "Brahman","Boran","Nguni","Afrikanner"});
            for (int i = 0; i < breedtypesLs.Count; i++)
            {
                breedtype.Add(new SelectListItem
                {
                    Text = breedtypesLs[i],
                    Value = breedtypesLs[i]
                });
            }
            return breedtype;
        }
        public List<SelectListItem> GetColourType()
        {
            //enumerables for lists 
            List<SelectListItem> colourType = new List<SelectListItem>();
            //gender
            //get breed types from the database
            List<string> colourTypeLs = new List<string>(new string[] { "Red", "Gray", "Black", "White" });
            for (int i = 0; i < colourTypeLs.Count; i++)
            {
                colourType.Add(new SelectListItem
                {
                    Text = colourTypeLs[i],
                    Value = colourTypeLs[i]
                });
            }
            return colourType;
        }
        public List<SelectListItem> GetMotherID(int OwnNum)
        {
            List<SelectListItem> MothersIDLS = new List<SelectListItem>();
            List<Cattle> data = GetCattleIDs(OwnNum);
            for (int i = 0; i < data.Count; i++)
            {
                if (data[i].AGender == GenderEnum.Female)
                {
                    MothersIDLS.Add(new SelectListItem { Text = data[i].AId.ToString(), Value = data[i].AId.ToString() });
                }
                
            }
            return MothersIDLS;
        }
        public List<SelectListItem> GetFatherID(int OwnNum)
        {
            List<SelectListItem> FatherIDLS = new List<SelectListItem>();
            List<Cattle> data = GetCattleIDs(OwnNum);
            for (int i = 0; i < data.Count; i++)
            {
                if (data[i].AGender == GenderEnum.Male)
                {
                    FatherIDLS.Add(new SelectListItem { Text = data[i].AId.ToString(), Value = data[i].AId.ToString() });
                }

            }
            return FatherIDLS;
        }

        public CattleModel()
            : base()
        {

        }

        //all cattle info
        public static List<Cattle> CattleData = null;

        public static Cattle GetCattleInfo(int OwnerID, Person person){
            //get data from data model 
            Cattle cattleSingu =null;
            CattleData = new List<Cattle>();
            CattleData.Clear();
            if (CattleData == null || CattleData.Count == 0)
            {
               
                List<Cattle> cattle = DataAccessData.GetUserAnimals(OwnerID, person);
                if (cattle.Count > 0)
                {
                    cattleSingu = cattle.ElementAt(0);
                    CattleData = cattle;
                }
                
            }

            
            
            return cattleSingu;

        }
        public static Cattle GetCattleInfo(int AID)
        {
            //get data from data model 
            Cattle cattleSingu = null;
          
                //apply divide an concure
                foreach (Cattle item in CattleData)
                {
                    if (item.AId==AID)
                    {
                        cattleSingu = item;
                        break;
                    }

                }
            


            return cattleSingu;

        }
        //one cattle info
        public static List<Cattle> GetCattleIDs( int OwnerID)
        {
            List<Cattle> cattle = CattleData;

            return cattle;
        }
     

        //sequence iD from database to add new Animal
        public static int  GetNewCattleID()
        {
            int id = DataAccessData.GetSeqAddNewAnimal();
            return id;
        }

       public static  bool AddNewAnimal(Cattle P)
       {
           bool state = DataAccessData.AddNewAnimal(P.PId,P);
           return state;
       }
    }
}