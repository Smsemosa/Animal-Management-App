using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShalomSystem.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        [HandleError]
        public ActionResult Index()
        {
           // throw new InvalidOperationException();
            ViewBag.Title = "Welcome to Shalom Systems";
            return View();
        }
        [HttpGet]
       //the html.action works/html.renderaction
        public PartialViewResult LoadLogin()
        {

            return PartialView();
        }

    }
}
