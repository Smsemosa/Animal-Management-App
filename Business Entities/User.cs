using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
namespace BusinessEntities
{
    public class User
    {
     

        
        string uName;
        string uPassword;
        bool   uAccess;





        [Required(ErrorMessage = "Username Required")]
        [Display(Name = "Username")]
        public string UName
        {
          get { return uName; }
          set { uName = value; }
        }
        [Required(ErrorMessage = "User password Required")]
        [Display(Name = "Password")]
        public string UPassword
        {
          get { return uPassword; }
          set { uPassword = value; }
        }
        [Required(ErrorMessage = "Access Level Required")]
        [Display(Name = "User Access")]
        public bool UAccess
        {
          get { return uAccess; }
          set { uAccess = value; }
        }

        public User()
        {

        }
    

        public User (string uName,string uPassword,bool   uAccess)
	    {
            this.uName= uName;
            this.uPassword = uPassword;
            this.uAccess=uAccess;
	    }

    
       

    }
}
