using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using TouristInsuredEntity;
using TouristInsuredManager;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;
using System.Threading;
using System.IO;
using System.Configuration;
using System.Net;
using System.Net.Http;

namespace TouristInsured.Controllers
{
    public class JVISAController : Controller
    {
        LoginModel Loginmodel = new LoginModel();
        LoginManager Loginmanager = new LoginManager();
        // GET: JVISA
        public ActionResult JVISA()
        {

            var CountryName = Loginmanager.BindHomeCountry();
            var TouristCountryName = Loginmanager.BindTouristCountry();
            //ViewData["ddlHomeCountry"] = new SelectList(CountryName, "ISOCODE", "CountryName");
            //ViewData["ddlTouristCountry"] = new SelectList(TouristCountryName, "TouristISOCODE", "TouristCountryName");
            return View();
        }
        //[HttpPost]
        public ActionResult JVISAForm(FormCollection form)
        {
            //return View();
            Loginmodel.ISOCODE = form["ISOCODE"];
            Loginmodel.TouristISOCODE = form["TouristISOCODE"];
            Loginmodel.LeavingHome = form["txtCoverageStartDate"];
            Loginmodel.TillDate = form["txtCoverageEndDate"];
            Loginmodel.Travelersnumber = form["txttravelerdetails"];
            Loginmodel.travelersage = form["txttravellerageinyear"];
            Loginmodel.spouseage = form["txtspouseage"];
            Loginmodel.child1age = form["txtchild1age"];
            Loginmodel.child2age = form["txtchild2age"];
            Loginmodel.child3age = form["txtchild3age"];
            Loginmodel.child4age = form["txtchild4age"];
            int res = 0;
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ValidateUser(FormCollection form)
        {
            string result = "";

            Loginmodel.userid = form["singInName"];
            Loginmodel.password = form["password"];
            if (form["singInName"].ToString() != "" || form["password"].ToString() != "")
            {
                List<LoginModel> model = new List<LoginModel>();
                model = Loginmanager.Validateuser(Loginmodel);
                if (model.Count > 0)
                {
                    Session["userid"] = Loginmodel.userid;
                    result = "1";
                }
                else
                {
                    result = "0";
                }

            }





            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public string ValidateSession(FormCollection form)
        {
            string result = "";
            if (Session["userid"] == null)
            {
                result = "0";

            }

            else if (Session["userid"] != null)
            {
                result = "1";
            }

            return result;
        }


        public ActionResult LogoutSession()
        {
            Session["userid"] = null;
            var result = 0;
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveDetails(FormCollection form)
        {

            Loginmodel.userid = form["singUPName"];

            Loginmodel.password = form["Signuppassword"];


            var result = Loginmanager.SaveUser(Loginmodel);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Index(string Prefix)
        {
            string prefix = (Prefix).ToUpper();
            List<LoginModel> allsearch = Loginmanager.GetCountryName(Prefix);
            var CityList = (from N in allsearch
                            where N.CountryName.StartsWith(prefix)
                            select new
                            {
                                label = N.CountryName,
                                val = N.ISOCODE
                            }).ToList();
            return Json(CityList, JsonRequestBehavior.AllowGet);
        }
    }
}