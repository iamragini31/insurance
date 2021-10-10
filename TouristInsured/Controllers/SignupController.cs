using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TouristInsuredEntity;
using TouristInsuredManager;

namespace TouristInsured.Controllers
{
    
    public class SignupController : Controller
    {
        SignupManager signmanager = new SignupManager();
        SignupModel signupmodel = new SignupModel();
        // GET: Signup
        public ActionResult Signup()
        {
            return View();
        }

        public ActionResult SaveDetails(FormCollection form)
        {
            signupmodel.Address =form["txtAddress"];
            signupmodel.Contact_No = Convert.ToInt64(form["txtContactNo"]);
            signupmodel.DOB = form["txtDob"];
            signupmodel.EmailID = form["txtEmail"];
            signupmodel.First_Name = form["txtfirstname"];
            signupmodel.Gender = form["Gender"];
            signupmodel.Last_Name = form["txtLastname"];
            signupmodel.Password = form["txtPassword"];


            var result = signmanager.SaveUser(signupmodel);
            return Json(result,JsonRequestBehavior.AllowGet);
        }
    }
}