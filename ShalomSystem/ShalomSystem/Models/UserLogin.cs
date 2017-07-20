using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessEntities;

namespace ShalomSystem.Models
{
    public  class UserLogin:User
    {
        //constructor
        public UserLogin():base()
        {

        }


        /// <summary>
        /// Login process
        /// </summary>
        /// <param name="uName"></param>
        /// <param name="uPassword"></param>
        /// <param name="uAccess"></param>
        /// <returns></returns>
        public static bool LoginValidateUser(string uName, string uPassword, bool uAccess)
        {
            bool validationStatus = DataAccessData.ValidateUserLogin(uName, uPassword, uAccess);
            return validationStatus;
        }
        public static int LoginGetUserID(string uName, string uPassword, bool uAccess)
        {
            //add access type
            int userID = DataAccessData.LoginUser(uName, uPassword,  uAccess);
            return userID;
        }
        public static Person GetPersonInfo(int userID)
        {
            //gets user data 
            Person personData = DataAccessData.GetUserDetails(userID);
            return personData;
        }


    }
}