using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessEntities;
using ShalomSystem.Models;

namespace ShalomSystem.Controllers
{
    public class LoginUserController : Controller
    {
        //
        // GET: /LoginUser/

        //public ActionResult Index()
        //{
        //    return View();
        //}

        [HttpPost]
       
        public ActionResult LoginAuth(User p, string selectedValue)
        {

            bool t = ModelState.IsValid;
            if (ModelState.IsValid)
            {
                if ((string.IsNullOrEmpty(p.UName) && string.IsNullOrEmpty(p.UPassword)) || string.IsNullOrEmpty(p.UName) || string.IsNullOrEmpty(p.UPassword))
                {
                    return Json("Error:Missing Credentials", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    //business object
                    //PersonDetails person = new PersonDetails();

                    //validate user
                    //check authentication details 
                    

                    if (UserLogin.LoginValidateUser(p.UName, p.UPassword, p.UAccess) == true)
                    {
                      
                        //session to hold user name 
                        // Session["SelectedValue"] = selectedValue;

                       TempData["LoginUname"] = p.UName;
                       
                 
                       TempData["LoginUpass"] = p.UPassword;
                      
                       TempData["LoginUaccess"] = p.UAccess;
                      
            
                        return Json("Success", JsonRequestBehavior.DenyGet);
                    }
                    else
                    {
                        return Json("Error:User not found", JsonRequestBehavior.AllowGet);
                    }
                }
            }
            else
            {
                return PartialView("~/Home/LoadLogin.cshtml", p); /// P.PName + 
            }
           
        }

     



    }

 
}
