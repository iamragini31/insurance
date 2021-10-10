using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TouristInsuredEntity;
using TouristInsuredManager;

namespace TouristInsured.Controllers
{
    public class OrderDetailsController : Controller
    {
        OrderDetailsManager orderdetailsmanager = new OrderDetailsManager();
        // GET: OrderDetails
        public ActionResult OrderDetails()
        {
            return View();
        }

        public ActionResult GetOrderDetails()
        {
            var userid = "";
            if (Session["userid"] == null)
            {
                string res = "No Data Found";
                return Json(res, JsonRequestBehavior.AllowGet);
            }
            else
            {
                 userid = Session["userid"].ToString();
                var result = orderdetailsmanager.GetOrderDetails(userid);

                return Json(result, JsonRequestBehavior.AllowGet);
            }

            
          
            
            
        }
    }
}