using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DatabaseAccessLayer;
using System.ComponentModel.DataAnnotations;

namespace BusinessEntities
{
    public class Cattle : Animal
    {
        private int cId;
        private int cParentFId;
        private int cParentMId;
        private string cImagePath;
        private List<Cattle> cOffSpring;
        private int tempID;
        private string imagebase64;

        
        private double cScotralSize;
        private string cColor;
        private BreedingStatus cBreedingStatus;
        private FrameSize cFrameSize;
        private double cBirthWeight;
        private double cWeaningWeight;
        private double cPostWeaningWeight;
        private double cAdultSxWeight;
        private double cCurrentAdultWeight;
        private DateTime cCurrentWeightDateTaken;
        //----------------------------------------
        private List<string> cDiagnoses;
        private List<double> cAdultWeightHistory;
        private List<DateTime> cAdultWeightDateHistory;

        private DateTime aDOB;

    
        //--------------------------------------



        #region Properties

        [Required]
        [Display(Name = "Date Of Birth")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime ADOB
        {
            get { return aDOB; }
            set { aDOB = value; }
        }



        [Required(ErrorMessage = "Cattle Image Required")]
        [Display(Name = "Cattle Image")]
        public string Imagebase64
        {
            get { return imagebase64; }
            set { imagebase64 = value; }
        }
        public int TempID
        {
            get { return tempID; }
            set { tempID = value; }
        }

        [Required(ErrorMessage = "Cattle Id Required")]
        [Display(Name = "Cattle Id")]
        public int CId
        {
            get { return cId; }
            set { cId = value; }
        }
        [Required(ErrorMessage = "Cattle Parent Id Required")]
        [Display(Name = "Cattle Father Id")]
        public int CParentFId
        {
            get { return cParentFId; }
            set { cParentFId = value; }
        }
        [Required(ErrorMessage = "Cattle Parent Id Required")]
        [Display(Name = "Cattle Mother Id")]
        public int CParentMId
        {
            get { return cParentMId; }
            set { cParentMId = value; }
        }


        public string CImagePath
        {
            get { return cImagePath; }
            set { cImagePath = value; }
        }

        public List<Cattle> COffSpring
        {
            get { return cOffSpring; }
            set { cOffSpring = value; }
        }


        [Required(ErrorMessage = "Cattle Scotral Size Required")]
        [Display(Name = "Cattle Scotral Size")]
        public double CScotralSize
        {
            get { return cScotralSize; }
            set { cScotralSize = value; }
        }
        [Required(ErrorMessage = "Cattle Colour Required")]
        [Display(Name = "Cattle Colour")]
        public string CColor
        {
            get { return cColor; }
            set { cColor = value; }
        }

        [Required(ErrorMessage = "Cattle Breeding Status Required")]
        [Display(Name = "Cattle Breeding Status")]
        public BreedingStatus CBreedingStatus
        {
            get { return cBreedingStatus; }
            set { cBreedingStatus = value; }
        }
        [Required(ErrorMessage = "Cattle Frame Size Required")]
        [Display(Name = "Cattle Frame Size ")]
        public FrameSize CFrameSize
        {
            get { return cFrameSize; }
            set { cFrameSize = value; }
        }
        [Required(ErrorMessage = "Cattle Birth Weight Required")]
        [Display(Name = "Cattle Birth Weight ")]
        public double CBirthWeight
        {
            get { return cBirthWeight; }
            set { cBirthWeight = value; }
        }
        [Required(ErrorMessage = "Cattle Weaning Weight Required")]
        [Display(Name = "Cattle Weaning Weight ")]
        public double CWeaningWeight
        {
            get { return cWeaningWeight; }
            set { cWeaningWeight = value; }
        }
        [Required(ErrorMessage = "Cattle Post Weight Required")]
        [Display(Name = "Cattle Post Weight ")]
        public double CPostWeaningWeight
        {
            get { return cPostWeaningWeight; }
            set { cPostWeaningWeight = value; }
        }
        [Required(ErrorMessage = "Cattle Adult Weight Required")]
        [Display(Name = "Cattle Adult Weight ")]
        public double CAdultSxWeight
        {
            get { return cAdultSxWeight; }
            set { cAdultSxWeight = value; }
        }
        [Required(ErrorMessage = "Cattle Current Adult Weight Required")]
        [Display(Name = "Cattle Current Adult Weight ")]
        public double CCurrentAdultWeight
        {
            get { return cCurrentAdultWeight; }
            set { cCurrentAdultWeight = value; }
        }
        [Required(ErrorMessage = "Cattle Current Weight Date Taken Required")]
        [Display(Name = "Cattle Current Weight Date  ")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime CCurrentWeightDateTaken
        {
            get { return cCurrentWeightDateTaken; }
            set { cCurrentWeightDateTaken = value; }
        }


        //-----------------------------------
        public List<string> CDiagnoses
        {
            get { return cDiagnoses; }
            set { cDiagnoses = value; }
        }

        public List<double> CAdultWeightHistory
        {
            get { return cAdultWeightHistory; }
            set { cAdultWeightHistory = value; }
        }


        public List<DateTime> CAdultWeightDateHistory
        {
            get { return cAdultWeightDateHistory; }
            set { cAdultWeightDateHistory = value; }
        }
        //--------------------------------------------
        List<string> errors;
        public List<string> Errors
        {
            get { return errors; }
            set { errors = value; }
        }
        #endregion




        public Cattle()
        {

        }



        public Cattle
            (
            int aId, string aBreed, GenderEnum aGender, double aAge, int aYear,    int aMonth, int aDay,
            AnimalTypeEnum aType, string aIdentifactionChar, string aCustomeIdentifaction,
            int oId,
            
            int pId, string pName, string pSurname, DateTime dob,

            int cId, int cParentFId, int cParentMId, string cImagePath, List<Cattle> cOffSpring,
            
            StatusEnum status, double cScotralSize, string cColor, BreedingStatus cBreedingStatus,
            
            FrameSize cFrameSize, double cBirthWeight,double cWeaningWeight,double cPostWeaningWeight,

            double cAdultSxWeight, double cCurrentAdultWeight, DateTime cCurrentWeightDateTaken, List<string> cDiagnoses, List<double> cAdultWeightHistory, List<DateTime> cAdultWeightDateHistory



            )
            : base(aId, aBreed, aGender, aAge, aYear, aMonth, aDay, aType,
             oId, pId, pName, pSurname, dob, status,aIdentifactionChar,aCustomeIdentifaction)
        {
            this.cId = cId;
            this.cParentFId = cParentFId;
            this.cParentMId = cParentMId;
            this.cImagePath = cImagePath;
            this.cOffSpring = cOffSpring;


            this.cScotralSize = cScotralSize;
            this.cColor = cColor;
            this.cBreedingStatus = cBreedingStatus;
            this.cFrameSize = cFrameSize;
            this.cBirthWeight = cBirthWeight;
            this.cWeaningWeight = cWeaningWeight;
            this.cPostWeaningWeight = cPostWeaningWeight;
            this.cAdultSxWeight = cAdultSxWeight;
            this.cCurrentAdultWeight = cCurrentAdultWeight;
            this.cCurrentWeightDateTaken = cCurrentWeightDateTaken;

            //------
            this.cDiagnoses = cDiagnoses;
            this.cAdultWeightDateHistory = cAdultWeightDateHistory;
            this.cAdultWeightHistory = cAdultWeightHistory;
            //------
            

        }

        public Cattle
           (
           int aId, string aBreed, GenderEnum aGender, double aAge, int aYear, int aMonth, int aDay,
           AnimalTypeEnum aType, string aIdentifactionChar, string aCustomeIdentifaction,
           int oId,

           int pId, string pName, string pSurname, DateTime dob,

           int cId, int cParentFId, int cParentMId, string cImagePath, List<Cattle> cOffSpring,

           StatusEnum status, double cScotralSize, string cColor, BreedingStatus cBreedingStatus,

           FrameSize cFrameSize, double cBirthWeight, double cWeaningWeight, double cPostWeaningWeight,

           double cAdultSxWeight, double cCurrentAdultWeight, DateTime cCurrentWeightDateTaken, List<string> cDiagnoses, List<double> cAdultWeightHistory, List<DateTime> cAdultWeightDateHistory,DateTime aDOB



           )
            : base(aId, aBreed, aGender, aAge, aYear, aMonth, aDay, aType,
             oId, pId, pName, pSurname, dob, status, aIdentifactionChar, aCustomeIdentifaction)
        {
            this.cId = cId;
            this.cParentFId = cParentFId;
            this.cParentMId = cParentMId;
            this.cImagePath = cImagePath;
            this.cOffSpring = cOffSpring;


            this.cScotralSize = cScotralSize;
            this.cColor = cColor;
            this.cBreedingStatus = cBreedingStatus;
            this.cFrameSize = cFrameSize;
            this.cBirthWeight = cBirthWeight;
            this.cWeaningWeight = cWeaningWeight;
            this.cPostWeaningWeight = cPostWeaningWeight;
            this.cAdultSxWeight = cAdultSxWeight;
            this.cCurrentAdultWeight = cCurrentAdultWeight;
            this.cCurrentWeightDateTaken = cCurrentWeightDateTaken;

            //------
            this.cDiagnoses = cDiagnoses;
            this.cAdultWeightDateHistory = cAdultWeightDateHistory;
            this.cAdultWeightHistory = cAdultWeightHistory;
            //------
            this.aDOB = aDOB;

        }


        public override string ToString()
        {
            return base.ToString();
        }


    }
}
