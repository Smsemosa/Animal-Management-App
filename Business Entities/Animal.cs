using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
namespace BusinessEntities
{
   public abstract class Animal:Owner
    {
        private  int aId;
        private string aBreed;
        private GenderEnum aGender;
        private double aAge;
        private int aYear;
        private int aMonth;
        private int aDay;
        private AnimalTypeEnum aType;
        private StatusEnum status;
        private string aIdentifactionChar;
        private string aCustomeIdentifaction;



  
       [Required(ErrorMessage = "Animal Id Required")]
       [Display(Name="Animal ID")]
  
       public int AId
        {
            get { return aId; }
            set { aId = value; }
        }
       [Required (ErrorMessage="Animal Breed-Required")]
       [Display(Name = "Animal Breed")]
       public string ABreed
       {
           get { return aBreed; }
           set { aBreed = value; }
       }

       [Required(ErrorMessage = "Animal Gender Required")]
       [Display(Name = "Animal Gender")]
       public GenderEnum AGender
       {
           get { return aGender; }
           set { aGender = value; }
       }

       [Required(ErrorMessage = "Animal Age Required (Months)")]
       [Display(Name = "Animal Age")]
       public double AAge
       {
           get { return aAge; }
           set { aAge = value; }
       }

       [Required(ErrorMessage = "Animal Year Required ")]
       [Display(Name = "Animal Year")]
       public int AYear
       {
           get { return aYear; }
           set { aYear = value; }
       }
       [Required(ErrorMessage = "Animal Month Required ")]
       [Display(Name = "Animal Month")]
       public int AMonth
       {
           get { return aMonth; }
           set { aMonth = value; }
       }

       [Required(ErrorMessage = "Animal Day Required ")]
       [Display(Name = "Animal Day (Birth Day)")]
       public int ADay
       {
           get { return aDay; }
           set { aDay = value; }
       }
       [Required(ErrorMessage = "Animal Type Required ")]
       [Display(Name = "Animal Type")]
       public AnimalTypeEnum AType
       {
           get { return aType; }
           set { aType = value; }
       }

       [Required(ErrorMessage = "Animal Status Required ")]
       [Display(Name = "Animal Status")]
       public StatusEnum Status
       {
           get { return status; }
           set { status = value; }
       }

       [Display(Name = "Animal Owner National Id Number")]
       public string AIdentifactionChar
       {
           get { return aIdentifactionChar; }
           set { aIdentifactionChar = value; }
       }

       [Display(Name = "Animal Id Custom Number")]
       public string ACustomeIdentifaction
       {
           get { return aCustomeIdentifaction; }
           set { aCustomeIdentifaction = value; }
       }

       public Animal()
       {

       }
       public Animal(  int aId,string aBreed, GenderEnum aGender,double aAge,int aYear,int aMonth,int aDay,AnimalTypeEnum aType,
           int oId, int pId, string pName, string pSurname, DateTime dob, StatusEnum status,string aIdentifactionChar, string  aCustomeIdentifaction
           )
           :base( oId, pId, pName, pSurname,  dob)
       {
           this.aId = aId;
           this.aBreed = aBreed;
           this.aGender = aGender;
           this.aAge = aAge;
           this.aYear = aYear;
           this.aMonth = aMonth;
           this.aDay = aDay;
           this.aType = aType;
           this.status = status;
           this.AIdentifactionChar = aIdentifactionChar;
           this.ACustomeIdentifaction = aCustomeIdentifaction;

       }


    }
}
