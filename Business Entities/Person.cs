using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace BusinessEntities
{
    public class Person
    {
        //fields
        private int pId;
        private string pName;
        private string pSurname;
        private DateTime dob;

        private GenderEnum pGender;
        private string pAreaCode;
        private string pAddress;
        private string pPhoneNum;
        private bool   pUserAccess;
        private string pProvinceName;
        private string pCityName;
        private string pCountryname;
        private string pContinent;
        private string pGpsCoOrdinatesLat;
        private string pGpsCoOrdinatesLong;
        private string pNationAnimalID;

      
        

        #region properties
        [Required(ErrorMessage = " Person Id Required ")]
        [Display(Name = "Person Id")]
        public int PId
        {
            get { return pId; }
            set { pId = value; }
        }

        [Required(ErrorMessage = " Person Name Required ")]
        [Display(Name = "Person Name")]
        public string PName
        {
            get { return pName; }
            set { pName = value; }
        }

        [Required(ErrorMessage = " Person Surname Required ")]
        [Display(Name = "Person Surname")]
        public string PSurname
        {
            get { return pSurname; }
            set { pSurname = value; }
        }


        [Required(ErrorMessage = " Person Date of Birth Required ")]
        [Display(Name = "Person DOB")]
        public DateTime Dob
        {
            get { return dob; }
            set { dob = value; }
        }

        [Required(ErrorMessage = " Person Gender Required ")]
        [Display(Name = "Person Gender")]
        public GenderEnum PGender
        {
            get { return pGender; }
            set { pGender = value; }
        }
        [Required(ErrorMessage = " Person Area Code Required ")]
        [Display(Name = "Person Area Code")]
        public string PAreaCode
        {
            get { return pAreaCode; }
            set { pAreaCode = value; }
        }
        [Required(ErrorMessage = " Person Address Required ")]
        [Display(Name = "Person Area Code")]
        public string PAddress
        {
            get { return pAddress; }
            set { pAddress = value; }
        }
        [Required(ErrorMessage = " Person Phone Number Required ")]
        [Display(Name = "Person Phone Number")]
        public string PPhoneNum
        {
            get { return pPhoneNum; }
            set { pPhoneNum = value; }
        }
        [Required(ErrorMessage = " Person User Access Required ")]
        [Display(Name = "Person Login User Access")]
        public bool PUserAccess
        {
            get { return pUserAccess; }
            set { pUserAccess = value; }
        }
        [Required(ErrorMessage = " Person City Name Required ")]
        [Display(Name = "Person City Name ")]
        public string PCityName
        {
            get { return pCityName; }
            set { pCityName = value; }
        }
        [Required(ErrorMessage = " Person Province Name Required ")]
        [Display(Name = "Person Province Name ")]
        public string PProvinceName
        {
            get { return pProvinceName; }
            set { pProvinceName = value; }
        }

        [Required(ErrorMessage = " Person Country Name Required ")]
        [Display(Name = "Person Country Name ")]
        public string PCountryname
        {
            get { return pCountryname; }
            set { pCountryname = value; }
        }

        [Required(ErrorMessage = " Person Continent Name Required ")]
        [Display(Name = "Person Continent Name ")]
        public string PContinent
        {
            get { return pContinent; }
            set { pContinent = value; }
        }
        [Required(ErrorMessage = " Person Gps Coordinates Longitude Required ")]
        [Display(Name = "Person Gps Coordinates [Long] ")]
        public string PGpsCoOrdinatesLong
        {
            get { return pGpsCoOrdinatesLong; }
            set { pGpsCoOrdinatesLong = value; }
        }
        [Required(ErrorMessage = " Person Gps Coordinates Latitude Required ")]
        [Display(Name = "Person  Gps Coordinates [Lati] ")]
        public string PGpsCoOrdinatesLat
        {
            get { return pGpsCoOrdinatesLat; }
            set { pGpsCoOrdinatesLat = value; }
        }
        [Required(ErrorMessage = " Person  National Animal ID Required ")]
        [Display(Name = "Person  National Animal ID ")]
        public string PNationAnimalID
        {
            get { return pNationAnimalID; }
            set { pNationAnimalID = value; }
        }


 #endregion
        #region methods
        public Person()
        {
                
        }
        public Person
      (int pId, string pName, string pSurname, DateTime dob)
        {
            this.pId = pId;
            this.pName = pName;
            this.pSurname = pSurname;
            this.dob = dob;
        }
        public Person
            (
            
            int pId,string pName,string pSurname, DateTime dob,
             GenderEnum pGender, string pAreaCode,string pAddress,
             string pPhoneNum,bool pUserAccess,string pProvinceName,
             string pCityName,string pCountryname,string pContinent,
             string pGpsCoOrdinatesLat, string pGpsCoOrdinatesLong, string pNationAnimalID
            
            
            )
        {
            this.pId = pId;
            this.pName = pName;
            this.pSurname = pSurname;
            this.dob = dob;
            this.pGender = pGender;
            this.pAreaCode = pAreaCode;
            this.pAddress = pAddress;
            this.pPhoneNum = pPhoneNum;
            this.pUserAccess = pUserAccess;
            this.pProvinceName = pProvinceName;
            this.pCityName = pCityName;
            this.pCountryname = pCountryname;
            this.pContinent = pContinent;
            this.pGpsCoOrdinatesLat = pGpsCoOrdinatesLat;
            this.pGpsCoOrdinatesLong = pGpsCoOrdinatesLong;
            this.pNationAnimalID = pNationAnimalID;

        }
  
        #endregion



    }
}
