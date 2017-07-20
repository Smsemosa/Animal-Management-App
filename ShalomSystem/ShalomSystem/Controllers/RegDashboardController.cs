using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShalomSystem.Models;
using BusinessEntities;
using System.IO;
using System.Text;

namespace ShalomSystem.Controllers
{
    public class RegDashboardController : Controller
    {

        //
        // GET: /RegDashboard/
        MyViewModel model = new MyViewModel();
        //[OutputCache(Duration = int.MaxValue, VaryByParam = "none")]
        public ActionResult RegUserDashboardIndex()
        {

            //model
           
            //Get User Id because this user was validated to exist on login

               //  'get person credetials to display on profile
            if (!TempData["LoginUname"].ToString().Equals(null) && !TempData["LoginUname"].ToString().Equals("DashLoad"))
            {
             

                //if refresh pass the id number of the person anmd thw data will passed into the database to request a refresh of the data 
                model.NewPerson = UserLogin.GetPersonInfo(UserLogin.LoginGetUserID(TempData["LoginUname"].ToString(), TempData["LoginUpass"].ToString(), (bool)TempData["LoginUaccess"]));


               
                }
            else
            {
                model.NewPerson = UserLogin.GetPersonInfo(int.Parse(TempData["UserID"].ToString()));
                model.cattleInfo = CattleModel.GetCattleInfo(model.NewPerson.PId, model.NewPerson);
              //  model.cattleInfo = CattleModel.GetCattleInfo(int.Parse(TempData["AidCurrent"].ToString()));
                model.CattleInfo = CattleModel.GetCattleIDs(model.NewPerson.PId);

                //sessions
                //login info
                TempData["LoginUname"] = "DashLoad";
                TempData.Keep("LoginUname");
                TempData["LoginUpass"] = model.NewPerson.PSurname;
                TempData.Keep("LoginUpass");
                TempData["LoginUaccess"] = model.NewPerson.PId;
                TempData.Keep("LoginUaccess");
              
                //animal info
                TempData["AidCurrent"] = model.cattleInfo.AId;
                TempData.Keep("AidCurrent");
                //owner info
                TempData["Uname"] = model.NewPerson.PName;
                TempData.Keep("Uname");
                TempData["USname"] = model.NewPerson.PSurname;
                TempData.Keep("USname");
                TempData["UserID"] = model.NewPerson.PId;
                TempData.Keep("UserID");


                return View(model);
               
            }


            //sessions 
            TempData["LoginUname"] = "DashLoad";
            TempData.Keep("LoginUname");
            TempData["LoginUpass"] = model.NewPerson.PSurname;
            TempData.Keep("LoginUpass");
            TempData["LoginUaccess"] = model.NewPerson.PId;
            TempData.Keep("LoginUaccess");
   


            //owner info
            TempData["Uname"] = model.NewPerson.PName;
            TempData.Keep("Uname");
            TempData["USname"] = model.NewPerson.PSurname;
            TempData.Keep("USname");
            TempData["UserID"] = model.NewPerson.PId;
            TempData.Keep("UserID");


            //get all available aniamls
            
            model.cattleInfo = CattleModel.GetCattleInfo(model.NewPerson.PId,model.NewPerson);
            model.CattleInfo = CattleModel.GetCattleIDs(model.NewPerson.PId);


            //animal id incase page is refreshed 

            TempData["AidCurrent"] = model.cattleInfo.AId;
            TempData.Keep("AidCurrent");
            return View(model);
        }
        //[OutputCache(Duration = int.MaxValue, VaryByParam = "none")]
        public ActionResult GetUserData()
        {
            //model
            MyViewModel model = new MyViewModel();

            //Get User Id because this user was validated to exist on login
            // get person credetials to display on profile
            model.PersonInfo = PersonModel.GetPersonDetails(TempData["LoginUname"].ToString(), TempData["LoginUpass"].ToString(), (bool)TempData["LoginUaccess"]);

            return View("AdminIndex", "_Layout", model);
        }
        [HttpPost]
        //[OutputCache(Duration = 60)]
        public ActionResult GetMainAreaUpdate(int AnimalId)
        {
            model.cattleInfo = CattleModel.GetCattleInfo(AnimalId);
            TempData["AidCurrent"] = model.cattleInfo.AId;
            return PartialView("~/Views/RegDashboard/PartialMainArea.cshtml", model);
        }
        [HttpGet]
        public ActionResult GetChartDonutData(int AID)
        {
            //fix this to get data from database
          
            Cattle currentCattle = CattleModel.GetCattleInfo(AID);
         
            List<ChartData> dataList = new List<ChartData>();

            Dictionary<String, int> OffSpringBreedTypes = new Dictionary<string, int>();
            ChartData details;
            MyViewModel model = new MyViewModel();


            //once data is retrived
            //check if there is data for this animal
            if (currentCattle.COffSpring.Count == 0 || currentCattle.COffSpring == null)
            {
                OffSpringBreedTypes.Add("No offspring \n on Record ", 0);
            }


            for (int i = 0; i < currentCattle.COffSpring.Count; i++)
                {
                    if (true == OffSpringBreedTypes.ContainsKey(currentCattle.COffSpring[i].ABreed))
                    {
                        OffSpringBreedTypes[currentCattle.COffSpring[i].ABreed]++;
                    }
                    else
                    {
                        OffSpringBreedTypes.Add(currentCattle.COffSpring[i].ABreed, 1);
                    }
                }
            





            foreach (KeyValuePair<string, int> kp in OffSpringBreedTypes)
            {
                details = new ChartData();
                details.label = kp.Key.ToString();
                details.value = kp.Value;
                dataList.Add(details);
            }

            model.barChart = dataList;
            return Json(model.barChart, JsonRequestBehavior.AllowGet);

            ////oper = null which means its first load.
            //var jsonSerializer = new JavaScriptSerializer();
            //string data = jsonSerializer.Serialize(dataList);
            //return data;
        }
        [HttpGet]
        public ActionResult GetLineXYChartData(int AID)
        {
            List<ChartData> dataList = new List<ChartData>();
            Dictionary<string, int> cpts = new Dictionary<string, int>();

            Cattle currentCattle = CattleModel.GetCattleInfo(AID);
            for (int i = 0; i < currentCattle.CAdultWeightDateHistory.Count; i++)
            {   string y = currentCattle.CAdultWeightDateHistory[i].Year.ToString();
                string m = currentCattle.CAdultWeightDateHistory[i].Month.ToString();
                string d = currentCattle.CAdultWeightDateHistory[i].Day.ToString();
            
                string full2 = y + "-" + m + "-" + d;
                dataList.Add(new ChartData { label = "Boran", valueWeight = currentCattle.CAdultWeightHistory[i], valuex = full2 });
            }

            MyViewModel model = new MyViewModel();
            model.barChart = dataList;




            return Json(model.barChart, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public ActionResult AddNewFunc(Cattle p)
        {
            CattleModel P = new CattleModel();
            P.AId = CattleModel.GetNewCattleID();
            model.NewPerson = UserLogin.GetPersonInfo(int.Parse(TempData["UserID"].ToString()));
            TempData.Keep("UserID");

            P.PName = model.NewPerson.PName;
            P.PSurname = model.NewPerson.PSurname;
            P.Dob = model.NewPerson.Dob;
            P.PGender = model.NewPerson.PGender;
            P.PAreaCode = model.NewPerson.PAreaCode;
            P.PAddress = model.NewPerson.PAddress;
            P.PPhoneNum = model.NewPerson.PPhoneNum;
            P.PUserAccess = model.NewPerson.PUserAccess;
            P.PProvinceName = model.NewPerson.PProvinceName;
            P.PCityName = model.NewPerson.PCityName;
            P.PCountryname = model.NewPerson.PCountryname;
            P.PContinent = model.NewPerson.PContinent;
            P.PGpsCoOrdinatesLat = model.NewPerson.PGpsCoOrdinatesLat;
            P.PGpsCoOrdinatesLong = model.NewPerson.PGpsCoOrdinatesLong;
            P.PNationAnimalID = model.NewPerson.PNationAnimalID;

            ModelState.Clear();
            return PartialView("~/Views/RegDashboard/PartialAddNewCow.cshtml",P);
        }
        [HttpPost]
        public ActionResult DeleteAnimalFunction()
        {
            return Json("Deleted", JsonRequestBehavior.DenyGet);
        }
        [HttpPost]
        public ActionResult UpdatedAnimalFunction()
        {
            return Json("Updated", JsonRequestBehavior.DenyGet);
        }
        [HttpPost]
        public ActionResult CompareAnimalFunction()
        {
            return Json("Compared", JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        [ValidateInput(true)]
        public ActionResult SubmitNew(CattleModel P)  ///  CattleModel P)
        {
       
            model.cattleInfo = new Cattle(P.AId, P.ABreed, P.AGender,(DateTime.Today.Year - P.ADOB.Year), P.ADOB.Year, P.ADOB.Month, P.ADOB.Day,
                                          AnimalTypeEnum.Cattle,"","" , 0000, P.PId, P.PName, P.PSurname, P.Dob, 0000, P.CParentFId, P.CParentMId,
                                          P.Imagebase64, null, P.Status, P.CScotralSize, P.CColor, P.CBreedingStatus, P.CFrameSize,
                                          P.CBirthWeight, P.CWeaningWeight, P.CPostWeaningWeight, P.CAdultSxWeight, P.CCurrentAdultWeight,
                                          P.CCurrentWeightDateTaken,null,null,null);
            
                                        model.cattleInfo.CDiagnoses = null;
                                            //person
                                        model.cattleInfo.PName = P.PName;
                                        model.cattleInfo.PSurname = P.PSurname;
                                        model.cattleInfo.Dob = P.Dob;
                                        model.cattleInfo.PGender = P.PGender;
                                        model.cattleInfo.PAreaCode = P.PAreaCode;
                                        model.cattleInfo.PAddress = P.PAddress;
                                        model.cattleInfo.PPhoneNum = P.PPhoneNum;
                                        model.cattleInfo.PUserAccess = P.PUserAccess;
                                        model.cattleInfo.PProvinceName = P.PProvinceName;
                                        model.cattleInfo.PCityName = P.PCityName;
                                        model.cattleInfo.PCountryname = P.PCountryname;
                                        model.cattleInfo.PContinent = P.PContinent;
                                        model.cattleInfo.PGpsCoOrdinatesLat = P.PGpsCoOrdinatesLat;
                                        model.cattleInfo.PGpsCoOrdinatesLong = P.PGpsCoOrdinatesLong;
                                        model.cattleInfo.AIdentifactionChar = P.PNationAnimalID;
                                        model.cattleInfo.ACustomeIdentifaction = P.PNationAnimalID + "" + P.AId.ToString() + "" + P.Dob.Year.ToString();

            if (ModelState.IsValid)
            {
                //string that makes base64 images visible 
                byte[] image = null;
                if (P.Imagebase64 != null)
                {
                    string r = ("data:image/jpeg;base64");
                    //r.Length
                    string bb = P.Imagebase64.Substring(0);
                   
                    image = Encoding.ASCII.GetBytes(bb);
                    model.cattleInfo.CImagePath = P.Imagebase64;

                }

                bool state = CattleModel.AddNewAnimal(model.cattleInfo);

                if (state)
                {
                    return Json(P.AId, JsonRequestBehavior.DenyGet); 
                }
                else 
                {
                    return Json(P.AId+":Error", JsonRequestBehavior.DenyGet);
                }

               
            }
            else
            {

                //check modelstate errors
                var query = from state in ModelState.Values
                            from error in state.Errors
                            select error.ErrorMessage;

                var errorList = query.ToList();
                model.cattleInfo.Errors = errorList;
                
                return PartialView("~/Views/RegDashboard/PartialAddNewCow.cshtml", P); /// P.PName + 
            }

 }


        public static byte[] ConvertToBytes(HttpPostedFileBase image)
        {
            byte[] imageBytes = null;
            BinaryReader reader = new BinaryReader(image.InputStream);
            imageBytes = reader.ReadBytes((int)image.ContentLength);
            return imageBytes;
        }

    }
}
