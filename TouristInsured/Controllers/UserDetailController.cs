using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TouristInsuredEntity;
using TouristInsuredManager;

namespace TouristInsured.Controllers
{
    
    public class UserDetailController : Controller
    {
        UserDetailModel userdetailmodel = new UserDetailModel();
        UserDetailManager userdetailmanager = new UserDetailManager();
        // GET: UserDetail
        public ActionResult UserDetail()
        {
            return View();
        }


        public ActionResult GetUserDetail()
        {
            string userid = Session["userid"].ToString();
            var result = userdetailmanager.GetdetailsofUser(userid);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateUser(FormCollection Form)
        {
            userdetailmodel.Address = Form["txtAddress"];
            userdetailmodel.Contact_No = Convert.ToInt64(Form["txtContactNo"]);
            userdetailmodel.DOB = Form["txtDob"];

            userdetailmodel.First_Name = Form["txtfirstname"];
            userdetailmodel.Gender = Form["Gender"];
            userdetailmodel.Last_Name = Form["txtLastname"];
            userdetailmodel.EmailID = Session["userid"].ToString();
            var result = userdetailmanager.UpdateUserDetails(userdetailmodel);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}