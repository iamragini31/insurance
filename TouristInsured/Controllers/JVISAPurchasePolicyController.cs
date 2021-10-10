using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using TouristInsuredEntity;
using TouristInsuredManager;
using Newtonsoft.Json;
using System.Net;
using System.IO;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Configuration;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace TouristInsured.Controllers
{
    public class JVISAPurchasePolicyController : Controller
    {
        PurchasePolicyManager PurchasePolicymanager = new PurchasePolicyManager();
        PurchasePolicyModel PurchasePolicymodel = new PurchasePolicyModel();
        QuotationManager quotationalmanager = new QuotationManager();


        private static async Task<string> PostRequestAtlas(string content, string url, string baseurl)
        {
            var data = new StringContent(content, Encoding.UTF8, "application/json");
            var client = new HttpClient();
            client.BaseAddress = new Uri(baseurl);
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
            //client.DefaultRequestHeaders.Add("Authorization", AuthKey);
            var response = await client.PostAsync(url, data);
            String ResponseJson = response.Content.ReadAsStringAsync().Result;
            return ResponseJson;
        }

        public ActionResult JVISAPurchasePolicy(string Data)
        {
            DataTable PurchaseList = (DataTable)JsonConvert.DeserializeObject(Data, (typeof(DataTable)));
            PurchasePolicymodel.PlanId = Convert.ToInt32(PurchaseList.Rows[0]["PlanId"].ToString());
            PurchasePolicymodel.planDeductible = Convert.ToDouble(PurchaseList.Rows[0]["planDeductible"].ToString());
            PurchasePolicymodel.planMaximum = Convert.ToDouble(PurchaseList.Rows[0]["planMaximum"].ToString());
            PurchasePolicymodel.INsuranceComp = PurchaseList.Rows[0]["INsuranceComp"].ToString();
            PurchasePolicymodel.policyName = PurchaseList.Rows[0]["policyName"].ToString();
            PurchasePolicymodel.LeavingHome = PurchaseList.Rows[0]["LeavingHome"].ToString();
            PurchasePolicymodel.TillDate = PurchaseList.Rows[0]["TillDate"].ToString();
            PurchasePolicymodel.ISOCODE = PurchaseList.Rows[0]["ISOCODE"].ToString();
            PurchasePolicymodel.TouristISOCODE = PurchaseList.Rows[0]["TouristISOCODE"].ToString();
            string TravelersDOB = PurchaseList.Rows[0]["TravelersDOB"].ToString();
            PurchasePolicymodel.Appname = PurchaseList.Rows[0]["Appname"].ToString();
            PurchasePolicymodel.Totalamount = PurchaseList.Rows[0]["TotalAmount"].ToString();
            PurchasePolicymodel.Age = Convert.ToInt32(PurchaseList.Rows[0]["Age"]);
            PurchasePolicymodel.spouseage = PurchaseList.Rows[0]["SpouseAge"].ToString();
            string SpouseDOB = PurchaseList.Rows[0]["SpouseDOB"].ToString();
            string child1age = PurchaseList.Rows[0]["child1Age"].ToString();
            string child2age = PurchaseList.Rows[0]["Child2age"].ToString();
            string child3age = PurchaseList.Rows[0]["Child3age"].ToString();
            string child4age = PurchaseList.Rows[0]["Child4age"].ToString();
            string DOB = "";


            if ((PurchaseList.Rows[0]["SpouseAge"].ToString() != null) && (PurchaseList.Rows[0]["SpouseAge"].ToString() != ""))
            {
                string spouseage = PurchaseList.Rows[0]["SpouseAge"].ToString();

                PurchasePolicymodel.spouseage = spouseage;
            }

            if (child1age != null && child1age != "")
            {
                int age = Convert.ToInt32(PurchaseList.Rows[0]["child1Age"]);

                PurchasePolicymodel.child1age = age.ToString();
            }
            if (child2age != null && child2age != "")
            {
                int age = Convert.ToInt32(PurchaseList.Rows[0]["Child2age"]);

                PurchasePolicymodel.child2age = age.ToString();
            }

            if (child3age != null && child3age != "")
            {
                int age = Convert.ToInt32(PurchaseList.Rows[0]["child3Age"]);

                PurchasePolicymodel.child3age = age.ToString();
            }
            if (child4age != null && child4age != "")
            {
                int age = Convert.ToInt32(PurchaseList.Rows[0]["Child4age"]);

                PurchasePolicymodel.child4age = age.ToString();
            }
            string ISOCODE = PurchaseList.Rows[0]["ISOCODE"].ToString();
            string TouristISOCODE = PurchaseList.Rows[0]["TouristISOCODE"].ToString();
            var HomeCountryName = PurchasePolicymanager.BindCountryname(ISOCODE);
            var TouristCountry = PurchasePolicymanager.BindCountryname(TouristISOCODE);
            PurchasePolicymodel.HomeCountryName = HomeCountryName;
            PurchasePolicymodel.TouristCountryName = TouristCountry;
            //PurchasePolicymanager.BindHomeCountry = HomeCountryName.;
            //PurchasePolicymodel.Totalamount = PurchaseList.Rows[0][""].ToString();
            var TouristCountryName = PurchasePolicymanager.BindTouristCountry();
            ViewData["ddlcitizenship"] = new SelectList(TouristCountryName, "TouristISOCODE", "TouristCountryName");
            var CountryName = PurchasePolicymanager.BindHomeCountry();
            ViewData["ddlResidenceHomeCountry"] = new SelectList(CountryName, "ISOCODE", "CountryName");
            var DestinationCountry = PurchasePolicymanager.BindTouristCountry();
            ViewData["ddlWhereareyoutravelingto"] = new SelectList(DestinationCountry, "TouristISOCODE", "TouristCountryName");
            var BiilingCountry = PurchasePolicymanager.BindHomeCountry();
            ViewData["ddlBillingCountry"] = new SelectList(BiilingCountry, "TouristISOCODE", "TouristCountryName");
            var BillingCountry = PurchasePolicymanager.BindHomeCountry();
            ViewData["ddlBillingCountry"] = new SelectList(BillingCountry);


            return View(PurchasePolicymodel);

        }

        public ActionResult Get2ndtabDetails(FormCollection form)
        {


            //PurchasePolicymodel.LeavingHome = form["txtCoverageStartDate"];
            //PurchasePolicymodel.TillDate = form["txtCoverageEndDate"];
            //PurchasePolicymodel.TouristISOCODE = form["ddlWhereareyoutravelingto"];
            //PurchasePolicymodel.TravelersDOB = form["TravelersDOB"];
            //PurchasePolicymodel.SpouseName = form["txtspousename"];
            //PurchasePolicymodel.SpouseDOB = form["txtspousedateofbirth"];
            //PurchasePolicymodel.ChildName = form["txtChildname"];
            //PurchasePolicymodel.ChildDOB = form["txtchilddateofbirth"];
            //PurchasePolicymodel.TravelerEmail = form["txttraveleremail"];
            //PurchasePolicymodel.INsuranceComp = form["hdnINsuranceComp"];
            //PurchasePolicymodel.policyName = form["hdnpolicyName"];

            //PurchasePolicymodel.planDeductible = Convert.ToDouble(form["hdnplanDeductible"]);
            //PurchasePolicymodel.PlanId = Convert.ToInt64(form["hdnPlanId"]);
            //PurchasePolicymodel.planMaximum = Convert.ToDouble(form["hdnplanMaximum"]);
            //PurchasePolicymodel.ISOCODE = form["ISOCODE"];

            string ISocode = form["ISOCODE"];
            var Countryname = PurchasePolicymanager.getCountryName(ISocode);

            return Json(Countryname, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Get2ndtabDetailsforstatename(string ISOCODE)
        {

            var Countryname = PurchasePolicymanager.getCountryName(ISOCODE);

            return Json(Countryname, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Get3rdtabDetails(FormCollection form)
        {
            var leavinghome = PurchasePolicymodel.LeavingHome;
            PurchasePolicymodel.TravelerFirstName = form["txtTravelerFirstname1"];
            PurchasePolicymodel.TravelerMiddleName = form["txtTravelerMiddlename"];
            //PurchasePolicymodel.TravelerLastName = form["txtTravelerMiddlename"];
            PurchasePolicymodel.TravelerLastName = form["txtTravelerLastname"];
            PurchasePolicymodel.TravelerPasspost = form["TravelerPasspost"];
            PurchasePolicymodel.Primaryapplicantfirstname = form["txtPrimaryapplicantfirstname"];
            PurchasePolicymodel.PrimaryapplicantLastname = form["txtPrimaryapplicantLastname"];
            PurchasePolicymodel.TravelerCity = form["txtTravelerCity"];
            PurchasePolicymodel.PrimaryApplicantAddress1 = form["txtPrimaryApplicantAddress1"];
            PurchasePolicymodel.Beneficiayname = form["txtBeneficiayname"];
            PurchasePolicymodel.TravelerPostalCode = form["txtTravelerPostalCode"];
            PurchasePolicymodel.TravelerMobile = form["txtTravelerMobile"];
            PurchasePolicymodel.DeparturefromHomeCountry = form["txtDeparturefromHomeCountry"];
            PurchasePolicymodel.DateofReturntoHome = form["txtDateofReturntoHome"];
            PurchasePolicymodel.Gender = form["Gender"];


            PurchasePolicymodel.ISOCODE = form["TouristISOCODE"];

            string TouristISOCODE = PurchasePolicymodel.ISOCODE;
            var Countryname = PurchasePolicymanager.getDestinationCountryName(form["TouristISOCODE"]);
            //var Countryname = PurchasePolicymanager.getDestinationCountryName(TouristISOCODE);

            return Json(Countryname, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult PaymentDetails(FormCollection form)
        {
            if (form["displayplanname"] == "Safe")
            {
                TrawickPurchaseDetails(form);


            }
            else if (form["displayplanname"] == "Atlas")
            {
                AtlasPurchaseDetails(form);
            }
            else
            {
                INFPUrchaseDetails(form);
            }

            return Json(ViewBag.Message, JsonRequestBehavior.AllowGet);






        }

        private static async Task<string> PostRequest(string content, string url, string AuthKey, string baseurl)
        {
            var data = new StringContent(content, Encoding.UTF8, "application/json");
            var client = new HttpClient();
            client.BaseAddress = new Uri(baseurl);
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
            client.DefaultRequestHeaders.Add("Authorization", AuthKey);
            var response = await client.PostAsync(url, data);
            String ResponseJson = response.Content.ReadAsStringAsync().Result;
            return ResponseJson;
        }

        public static async Task<string> GetData(string url, string data)
        {
            HttpClient client = new HttpClient();
            StringContent queryString = new StringContent(data);

            HttpResponseMessage response = await client.PostAsync(new Uri(url), queryString);

            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            return responseBody;
        }
        public string INFPUrchaseDetails(FormCollection form)
        {
            try
            {


                string dateofbirth = "";
                if (form["TravelersDOB"] == null || form["TravelersDOB"] == "")
                {
                    int ageoftraveler = Convert.ToInt32(form["age"]);
                    int getDOByear = Convert.ToInt32(DateTime.Now.Year) - ageoftraveler;
                    string getdate = DateTime.Now.Day.ToString();
                    string getmonth = DateTime.Now.Month.ToString();

                    dateofbirth = getmonth + "/" + getdate + "/" + getDOByear.ToString();
                }
                else
                {
                    dateofbirth = Convert.ToDateTime(form["TravelersDOB"]).ToString("MM/dd/yyyy");
                }
                PurchasePolicymodel.PlanId = Convert.ToInt64(form["hdnPlanId"]);
                PurchasePolicymodel.planDeductible = Convert.ToDouble(form["hdnplanDeductible"]);
                PurchasePolicymodel.planMaximum = Convert.ToDouble(form["hdnplanMaximum"]);
                PurchasePolicymodel.INsuranceComp = form["hdnINsuranceComp"];
                PurchasePolicymodel.policyName = form["hdnpolicyName"];
                PurchasePolicymodel.LeavingHome = Convert.ToDateTime(form["txtCoverageStartDate"]).ToString("MM/dd/yyyy");
                PurchasePolicymodel.TillDate = Convert.ToDateTime(form["txtCoverageEndDate"]).ToString("MM/dd/yyyy");
                PurchasePolicymodel.ISOCODE = form["ISOCODE"];
                PurchasePolicymodel.WhereareYouTravlingTo = form["ddlWhereareyoutravelingto"];
                PurchasePolicymodel.TravelersDOB = Convert.ToDateTime(dateofbirth).ToString("MM/dd/yyyy");

                PurchasePolicymodel.SpouseName = form["txtspousename"];
                PurchasePolicymodel.SpouseDOB = form["txtspousedateofbirth"];
                PurchasePolicymodel.ChildName = form["txtChildname"];
                PurchasePolicymodel.ChildDOB = form["txtchilddateofbirth"];
                PurchasePolicymodel.TravelerEmail = form["txttraveleremail"];
                PurchasePolicymodel.TravelerFirstName = form["txtTravelerFirstname1"];
                PurchasePolicymodel.TravelerMiddleName = form["txtTravelerMiddlename"];
                PurchasePolicymodel.TravelerLastName = form["txtTravelerLastname"];
                PurchasePolicymodel.Gender = form["Gender1"];
                PurchasePolicymodel.TravelerPasspost = form["TravelerPasspost"];
                PurchasePolicymodel.DeparturefromHomeCountry = form["txtDeparturefromHomeCountry"];
                PurchasePolicymodel.DateofReturntoHome = form["txtDateofReturntoHome"];
                PurchasePolicymodel.Primaryapplicantfirstname = form["txtPrimaryapplicantfirstname"];
                PurchasePolicymodel.PrimaryapplicantLastname = form["txtPrimaryapplicantLastname"];
                PurchasePolicymodel.PrimaryApplicantAddress1 = form["txtPrimaryApplicantAddress1"];
                PurchasePolicymodel.PrimaryApplicantAddress2 = PurchasePolicymodel.PrimaryApplicantAddress2;
                PurchasePolicymodel.TravelerCity = form["txtTravelerCity"];
                PurchasePolicymodel.TravelerPostalCode = form["txtTravelerPostalCode"];
                PurchasePolicymodel.TravelerEmailConfirm = PurchasePolicymodel.TravelerEmailConfirm;
                PurchasePolicymodel.TravelerMobile = form["txtTravelerMobile"];
                PurchasePolicymodel.Beneficiayname = form["txtBeneficiayname"];
                PurchasePolicymodel.Relationship = PurchasePolicymodel.Relationship;
                PurchasePolicymodel.PolicyDocument = PurchasePolicymodel.PolicyDocument;
                PurchasePolicymodel.Totalamount = form["hdntotalPremium"];
                PurchasePolicymodel.Countryofcitizenship = form["ddlcitizenship"];
                PurchasePolicymodel.Cardname = form["ddlcardname"];
                PurchasePolicymodel.Month = form["ddlMonth"];
                PurchasePolicymodel.Expiryyear = form["ddlExpiryyear"];
                PurchasePolicymodel.Nameoncard = form["txtNameoncard"];
                PurchasePolicymodel.Cardonnumber = form["txtCardonnumber"];
                PurchasePolicymodel.Securitycode = form["txtSecuritycode"];
                PurchasePolicymodel.TravelerState = form["txtBeneficiayname"];
                PurchasePolicymodel.BillingAddress = form["txtBillingAddress"];
                PurchasePolicymodel.BillingAddress2 = form["txtBillingAddress2"];
                PurchasePolicymodel.BillingState = form["ddlBillingState"];
                PurchasePolicymodel.BillingCity = form["txtBillingCity"];
                PurchasePolicymodel.BillingZipcode = form["txtBillingZipcode"];
                var userid = PurchasePolicymanager.SaveUserdetails(PurchasePolicymodel);
                var getTripID = PurchasePolicymanager.saveDetailsIntripMaster(PurchasePolicymodel);

                string countmonth = (form["ddlMonth"]);
                int lenth = countmonth.Length;
                string cardexpiry = "";
                if (lenth == 1)
                {
                    cardexpiry = "0" + form["ddlMonth"] + form["ddlExpiryyear"];
                }
                else
                {
                    cardexpiry = form["ddlMonth"] + form["ddlExpiryyear"];

                }
                string Gender = "";
                if (form["Gender"] == "MALE")
                {
                    Gender = "M";
                }
                else
                {
                    Gender = "F";
                }
                string Domainname = ConfigurationManager.AppSettings["Domainname"];
                HttpClient auth = new HttpClient();
                auth.BaseAddress = new Uri(Domainname);
                auth.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");

                var login = new TouristInsured.Models.auth();
                string key = ConfigurationManager.AppSettings["username"];
                string value = ConfigurationManager.AppSettings["password"];
                login.username = key;
                login.password = value;
                string AuthenticateINF = ConfigurationManager.AppSettings["AuthenticateINF"];
                var res = auth.PostAsJsonAsync(AuthenticateINF, login).Result;

                if (res.IsSuccessStatusCode)
                {
                    string AuthKey = "";
                    using (HttpContent content = res.Content)
                    {
                        Task<string> result = content.ReadAsStringAsync();
                        Dictionary<string, object> values =
                                    JsonConvert.DeserializeObject<Dictionary<string, object>>(result.Result);

                        AuthKey = values["token"].ToString();
                        HttpClient client = new HttpClient();
                        client.BaseAddress = new Uri(Domainname);
                        client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
                        client.DefaultRequestHeaders.Add("Authorization", AuthKey);
                        if (PurchasePolicymodel.spouseage == null)
                        {
                            PurchasePolicymodel.spouseage = "";
                        }
                        if (PurchasePolicymodel.child1age == null)
                        {
                            PurchasePolicymodel.child1age = "";
                        }
                        if (PurchasePolicymodel.child2age == null)
                        {
                            PurchasePolicymodel.child2age = "";
                        }
                        if (PurchasePolicymodel.child3age == null)
                        {
                            PurchasePolicymodel.child3age = "";
                        }
                        if (PurchasePolicymodel.child4age == null)
                        {
                            PurchasePolicymodel.child4age = "";
                        }
                        if (PurchasePolicymodel.spouseage == "" && PurchasePolicymodel.child1age == "" && PurchasePolicymodel.child2age == "" && PurchasePolicymodel.child3age == "" && PurchasePolicymodel.child4age == "")
                        {
                            var purchase = new Models.INF_purchase();
                            purchase.planId = Convert.ToInt32(form["hdnPlanId"]);
                            purchase.coverageStartDate = Convert.ToDateTime(form["txtCoverageStartDate"]).ToString("yyyy-MM-dd");
                            purchase.coverageEndDate = Convert.ToDateTime(form["txtCoverageEndDate"]).ToString("yyyy-MM-dd");
                            purchase.coverageArea = "USA/CANADA";
                            purchase.agreement = "yes";
                            purchase.adbbeneficiary = form["txtBeneficiayname"];
                            purchase.adbrelation = "NP";

                            var primary = new Models.primary();
                            primary.firstName = form["txtTravelerFirstname1"];
                            primary.lastName = form["txtTravelerLastname"];
                            primary.email = form["txttraveleremail"];
                            primary.dob = Convert.ToDateTime(dateofbirth).ToString("yyyy-MM-dd");
                            primary.phone = Convert.ToInt64(form["txtTravelerMobile"]).ToString("###-###-####");
                            primary.gender = Gender;
                            primary.country = "IN";
                            primary.passport = form["TravelerPasspost"];
                            purchase.primary.Add(primary);

                            var payment = new Models.payment();



                            payment.cardNumber = form["txtCardonnumber"];
                            payment.cardExpiry = "0425";
                            payment.cardSecurityCode = form["txtSecuritycode"];
                            payment.address = form["txtPrimaryApplicantAddress1"];
                            payment.city = form["txtTravelerCity"];
                            payment.state = "Jharkhand";
                            payment.zipCode = form["txtTravelerPostalCode"];
                            purchase.paymentInfo.Add(payment);
                            var travel = new Models.travel();
                            travel.arrivalDate = Convert.ToDateTime(form["txtCoverageStartDate"]).ToString("yyyy-MM-dd");
                            travel.countryToVisit = form["ddlWhereareyoutravelingto"];
                            //var displayname= form["displayplanname"];
                            purchase.travelInfo.Add(travel);

                            var json = JsonConverterINF.Serialize(purchase);


                            string INFPurchase = ConfigurationManager.AppSettings["INFPurchase"];

                            var t = Task.Run(() => PostRequest(json, INFPurchase, AuthKey, Domainname));
                            t.Wait();
                            string JsonResult = t.Result + "TRIPID:" + getTripID;
                            ViewBag.Message = JsonResult;


                        }
                        else if (PurchasePolicymodel.spouseage != "" && PurchasePolicymodel.child1age != "" && PurchasePolicymodel.child2age == "" && PurchasePolicymodel.child3age == "" && PurchasePolicymodel.child4age == "")
                        {
                            var purchase = new Models.INF_Child1_purchase();
                            purchase.planId = Convert.ToInt32(form["hdnPlanId"]);
                            purchase.coverageStartDate = Convert.ToDateTime(form["txtCoverageStartDate"]).ToString("yyyy-MM-dd");
                            purchase.coverageEndDate = Convert.ToDateTime(form["txtCoverageEndDate"]).ToString("yyyy-MM-dd");
                            purchase.coverageArea = "USA/CANADA";
                            purchase.agreement = "yes";
                            purchase.adbbeneficiary = form["txtBeneficiayname"];
                            purchase.adbrelation = "NP";

                            var primary = new Models.primary();
                            primary.firstName = form["txtTravelerFirstname1"];
                            primary.lastName = form["txtTravelerLastname"];
                            primary.email = form["txttraveleremail"];
                            primary.dob = Convert.ToDateTime(dateofbirth).ToString("yyyy-MM-dd");
                            primary.phone = Convert.ToInt64(form["txtTravelerMobile"]).ToString("###-###-####");
                            primary.gender = Gender;
                            primary.country = "IN";
                            primary.passport = form["TravelerPasspost"];
                            purchase.primary.Add(primary);

                            var spouse = new Models.INFspouse();
                            spouse.firstName = form["txtSpousefirstname"];
                            spouse.lastName = form["txtspouselastname"];
                            spouse.dob = Convert.ToDateTime(form["txtspousedateofbirth"]).ToString("yyyy-MM-dd");
                            spouse.passport = form["txtTravelerPassNumSpouse"];
                            purchase.spouse.Add(spouse);

                            var child1 = new Models.INFChild1();
                            child1.firstName = form["txtchild1firstname"];
                            child1.lastName = form["txtchild1lastname"];
                            child1.dob = Convert.ToDateTime(form["txtchild1dob"]).ToString("yyyy-MM-dd");
                            child1.passport = form["txtTravelerPassNumchild1"];
                            purchase.child1.Add(child1);
                            var payment = new Models.payment();



                            payment.cardNumber = form["txtCardonnumber"];
                            payment.cardExpiry = "0425";
                            payment.cardSecurityCode = form["txtSecuritycode"];
                            payment.address = form["txtPrimaryApplicantAddress1"];
                            payment.city = form["txtTravelerCity"];
                            payment.state = "Jharkhand";
                            payment.zipCode = form["txtTravelerPostalCode"];
                            purchase.paymentInfo.Add(payment);
                            var travel = new Models.travel();
                            travel.arrivalDate = Convert.ToDateTime(form["txtCoverageStartDate"]).ToString("yyyy-MM-dd");
                            travel.countryToVisit = form["ddlWhereareyoutravelingto"];
                            //var displayname= form["displayplanname"];
                            purchase.travelInfo.Add(travel);

                            var json = JsonConverterINF.Serialize(purchase);


                            string INFPurchase = ConfigurationManager.AppSettings["INFPurchase"];

                            var t = Task.Run(() => PostRequest(json, INFPurchase, AuthKey, Domainname));
                            t.Wait();
                            string JsonResult = t.Result + "TRIPID:" + getTripID;
                            ViewBag.Message = JsonResult;


                        }
                        else if (PurchasePolicymodel.spouseage != "" && PurchasePolicymodel.child1age != "" && PurchasePolicymodel.child2age != "" && PurchasePolicymodel.child3age == "" && PurchasePolicymodel.child4age == "")
                        {
                            var purchase = new Models.INF_Child2_purchase();
                            purchase.planId = Convert.ToInt32(form["hdnPlanId"]);
                            purchase.coverageStartDate = Convert.ToDateTime(form["txtCoverageStartDate"]).ToString("yyyy-MM-dd");
                            purchase.coverageEndDate = Convert.ToDateTime(form["txtCoverageEndDate"]).ToString("yyyy-MM-dd");
                            purchase.coverageArea = "USA/CANADA";
                            purchase.agreement = "yes";
                            purchase.adbbeneficiary = form["txtBeneficiayname"];
                            purchase.adbrelation = "NP";

                            var primary = new Models.primary();
                            primary.firstName = form["txtTravelerFirstname1"];
                            primary.lastName = form["txtTravelerLastname"];
                            primary.email = form["txttraveleremail"];
                            primary.dob = Convert.ToDateTime(dateofbirth).ToString("yyyy-MM-dd");
                            primary.phone = Convert.ToInt64(form["txtTravelerMobile"]).ToString("###-###-####");
                            primary.gender = Gender;
                            primary.country = "IN";
                            primary.passport = form["TravelerPasspost"];
                            purchase.primary.Add(primary);

                            var spouse = new Models.INFspouse();
                            spouse.firstName = form["txtSpousefirstname"];
                            spouse.lastName = form["txtspouselastname"];
                            spouse.dob = Convert.ToDateTime(form["txtspousedateofbirth"]).ToString("yyyy-MM-dd");
                            spouse.passport = form["txtTravelerPassNumSpouse"];
                            purchase.spouse.Add(spouse);

                            var child1 = new Models.INFChild1();
                            child1.firstName = form["txtchild1firstname"];
                            child1.lastName = form["txtchild1lastname"];
                            child1.dob = Convert.ToDateTime(form["txtchild1dob"]).ToString("yyyy-MM-dd");
                            child1.passport = form["txtTravelerPassNumchild1"];
                            purchase.child1.Add(child1);

                            var child2 = new Models.INFChild2();
                            child2.firstName = form["txtchild2firstname"];
                            child2.lastName = form["txtchild2lastname"];
                            child2.dob = Convert.ToDateTime(form["txtchild2dob"]).ToString("yyyy-MM-dd");
                            child2.passport = form["txtTravelerPassNumChild2"];
                            purchase.child2.Add(child2);
                            var payment = new Models.payment();



                            payment.cardNumber = form["txtCardonnumber"];
                            payment.cardExpiry = "0425";
                            payment.cardSecurityCode = form["txtSecuritycode"];
                            payment.address = form["txtPrimaryApplicantAddress1"];
                            payment.city = form["txtTravelerCity"];
                            payment.state = "Jharkhand";
                            payment.zipCode = form["txtTravelerPostalCode"];
                            purchase.paymentInfo.Add(payment);
                            var travel = new Models.travel();
                            travel.arrivalDate = Convert.ToDateTime(form["txtCoverageStartDate"]).ToString("yyyy-MM-dd");
                            travel.countryToVisit = form["ddlWhereareyoutravelingto"];
                            //var displayname= form["displayplanname"];
                            purchase.travelInfo.Add(travel);

                            var json = JsonConverterINF.Serialize(purchase);


                            string INFPurchase = ConfigurationManager.AppSettings["INFPurchase"];

                            var t = Task.Run(() => PostRequest(json, INFPurchase, AuthKey, Domainname));
                            t.Wait();
                            string JsonResult = t.Result + "TRIPID:" + getTripID;
                            ViewBag.Message = JsonResult;


                        }
                        else if (PurchasePolicymodel.spouseage != "" && PurchasePolicymodel.child1age != "" && PurchasePolicymodel.child2age != "" && PurchasePolicymodel.child3age != "" && PurchasePolicymodel.child4age == "")
                        {
                            var purchase = new Models.INF_Child3_purchase();
                            purchase.planId = Convert.ToInt32(form["hdnPlanId"]);
                            purchase.coverageStartDate = Convert.ToDateTime(form["txtCoverageStartDate"]).ToString("yyyy-MM-dd");
                            purchase.coverageEndDate = Convert.ToDateTime(form["txtCoverageEndDate"]).ToString("yyyy-MM-dd");
                            purchase.coverageArea = "USA/CANADA";
                            purchase.agreement = "yes";
                            purchase.adbbeneficiary = form["txtBeneficiayname"];
                            purchase.adbrelation = "NP";

                            var primary = new Models.primary();
                            primary.firstName = form["txtTravelerFirstname1"];
                            primary.lastName = form["txtTravelerLastname"];
                            primary.email = form["txttraveleremail"];
                            primary.dob = Convert.ToDateTime(dateofbirth).ToString("yyyy-MM-dd");
                            primary.phone = Convert.ToInt64(form["txtTravelerMobile"]).ToString("###-###-####");
                            primary.gender = Gender;
                            primary.country = "IN";
                            primary.passport = form["TravelerPasspost"];
                            purchase.primary.Add(primary);

                            var spouse = new Models.INFspouse();
                            spouse.firstName = form["txtSpousefirstname"];
                            spouse.lastName = form["txtspouselastname"];
                            spouse.dob = Convert.ToDateTime(form["txtspousedateofbirth"]).ToString("yyyy-MM-dd");
                            spouse.passport = form["txtTravelerPassNumSpouse"];
                            purchase.spouse.Add(spouse);

                            var child1 = new Models.INFChild1();
                            child1.firstName = form["txtchild1firstname"];
                            child1.lastName = form["txtchild1lastname"];
                            child1.dob = Convert.ToDateTime(form["txtchild1dob"]).ToString("yyyy-MM-dd");
                            child1.passport = form["txtTravelerPassNumchild1"];
                            purchase.child1.Add(child1);

                            var child2 = new Models.INFChild2();
                            child2.firstName = form["txtchild2firstname"];
                            child2.lastName = form["txtchild2lastname"];
                            child2.dob = Convert.ToDateTime(form["txtchild2dob"]).ToString("yyyy-MM-dd");
                            child2.passport = form["txtTravelerPassNumChild2"];
                            purchase.child2.Add(child2);

                            var child3 = new Models.INFChild3();
                            child3.firstName = form["txtchild3firstname"];
                            child3.lastName = form["txtchild3lastname"];
                            child3.dob = Convert.ToDateTime(form["txtchild3dob"]).ToString("yyyy-MM-dd");
                            child3.passport = form["txtTravelerPassNumchild3"];
                            purchase.child3.Add(child3);
                            var payment = new Models.payment();



                            payment.cardNumber = form["txtCardonnumber"];
                            payment.cardExpiry = "0425";
                            payment.cardSecurityCode = form["txtSecuritycode"];
                            payment.address = form["txtPrimaryApplicantAddress1"];
                            payment.city = form["txtTravelerCity"];
                            payment.state = "Jharkhand";
                            payment.zipCode = form["txtTravelerPostalCode"];
                            purchase.paymentInfo.Add(payment);
                            var travel = new Models.travel();
                            travel.arrivalDate = Convert.ToDateTime(form["txtCoverageStartDate"]).ToString("yyyy-MM-dd");
                            travel.countryToVisit = form["ddlWhereareyoutravelingto"];
                            //var displayname= form["displayplanname"];
                            purchase.travelInfo.Add(travel);

                            var json = JsonConverterINF.Serialize(purchase);


                            string INFPurchase = ConfigurationManager.AppSettings["INFPurchase"];

                            var t = Task.Run(() => PostRequest(json, INFPurchase, AuthKey, Domainname));
                            t.Wait();
                            string JsonResult = t.Result + "TRIPID:" + getTripID;
                            ViewBag.Message = JsonResult;


                        }
                        else if (PurchasePolicymodel.spouseage != "" && PurchasePolicymodel.child1age != "" && PurchasePolicymodel.child2age != "" && PurchasePolicymodel.child3age != "" && PurchasePolicymodel.child4age != "")
                        {
                            var purchase = new Models.INF_Child4_purchase();
                            purchase.planId = Convert.ToInt32(form["hdnPlanId"]);
                            purchase.coverageStartDate = Convert.ToDateTime(form["txtCoverageStartDate"]).ToString("yyyy-MM-dd");
                            purchase.coverageEndDate = Convert.ToDateTime(form["txtCoverageEndDate"]).ToString("yyyy-MM-dd");
                            purchase.coverageArea = "USA/CANADA";
                            purchase.agreement = "yes";
                            purchase.adbbeneficiary = form["txtBeneficiayname"];
                            purchase.adbrelation = "NP";

                            var primary = new Models.primary();
                            primary.firstName = form["txtTravelerFirstname1"];
                            primary.lastName = form["txtTravelerLastname"];
                            primary.email = form["txttraveleremail"];
                            primary.dob = Convert.ToDateTime(dateofbirth).ToString("yyyy-MM-dd");
                            primary.phone = Convert.ToInt64(form["txtTravelerMobile"]).ToString("###-###-####");
                            primary.gender = Gender;
                            primary.country = "IN";
                            primary.passport = form["TravelerPasspost"];
                            purchase.primary.Add(primary);

                            var spouse = new Models.INFspouse();
                            spouse.firstName = form["txtSpousefirstname"];
                            spouse.lastName = form["txtspouselastname"];
                            spouse.dob = Convert.ToDateTime(form["txtspousedateofbirth"]).ToString("yyyy-MM-dd");
                            spouse.passport = form["txtTravelerPassNumSpouse"];
                            purchase.spouse.Add(spouse);

                            var child1 = new Models.INFChild1();
                            child1.firstName = form["txtchild1firstname"];
                            child1.lastName = form["txtchild1lastname"];
                            child1.dob = Convert.ToDateTime(form["txtchild1dob"]).ToString("yyyy-MM-dd");
                            child1.passport = form["txtTravelerPassNumchild1"];
                            purchase.child1.Add(child1);

                            var child2 = new Models.INFChild2();
                            child2.firstName = form["txtchild2firstname"];
                            child2.lastName = form["txtchild2lastname"];
                            child2.dob = Convert.ToDateTime(form["txtchild2dob"]).ToString("yyyy-MM-dd");
                            child2.passport = form["txtTravelerPassNumChild2"];
                            purchase.child2.Add(child2);

                            var child3 = new Models.INFChild3();
                            child3.firstName = form["txtchild3firstname"];
                            child3.lastName = form["txtchild3lastname"];
                            child3.dob = Convert.ToDateTime(form["txtchild3dob"]).ToString("yyyy-MM-dd");
                            child3.passport = form["txtTravelerPassNumchild3"];
                            purchase.child3.Add(child3);

                            var child4 = new Models.INFChild4();
                            child4.firstName = form["txtchild4firstname"];
                            child4.lastName = form["txtchild4lastname"];
                            child4.dob = Convert.ToDateTime(form["txtchild4dob"]).ToString("yyyy-MM-dd");
                            child4.passport = form["txtTravelerPassNumchild4"];
                            purchase.child4.Add(child4);

                            var payment = new Models.payment();



                            payment.cardNumber = form["txtCardonnumber"];
                            payment.cardExpiry = "0425";
                            payment.cardSecurityCode = form["txtSecuritycode"];
                            payment.address = form["txtPrimaryApplicantAddress1"];
                            payment.city = form["txtTravelerCity"];
                            payment.state = "Jharkhand";
                            payment.zipCode = form["txtTravelerPostalCode"];
                            purchase.paymentInfo.Add(payment);
                            var travel = new Models.travel();
                            travel.arrivalDate = Convert.ToDateTime(form["txtCoverageStartDate"]).ToString("yyyy-MM-dd");
                            travel.countryToVisit = form["ddlWhereareyoutravelingto"];
                            //var displayname= form["displayplanname"];
                            purchase.travelInfo.Add(travel);

                            var json = JsonConverterINF.Serialize(purchase);


                            string INFPurchase = ConfigurationManager.AppSettings["INFPurchase"];

                            var t = Task.Run(() => PostRequest(json, INFPurchase, AuthKey, Domainname));
                            t.Wait();
                            string JsonResult = t.Result + "TRIPID:" + getTripID;
                            ViewBag.Message = JsonResult;


                        }

                        else if (PurchasePolicymodel.spouseage == "" && PurchasePolicymodel.child1age != "" && PurchasePolicymodel.child2age == "" && PurchasePolicymodel.child3age == "" && PurchasePolicymodel.child4age == "")
                        {
                            var purchase = new Models.INF_Child1_purchase_Primary();
                            purchase.planId = Convert.ToInt32(form["hdnPlanId"]);
                            purchase.coverageStartDate = Convert.ToDateTime(form["txtCoverageStartDate"]).ToString("yyyy-MM-dd");
                            purchase.coverageEndDate = Convert.ToDateTime(form["txtCoverageEndDate"]).ToString("yyyy-MM-dd");
                            purchase.coverageArea = "USA/CANADA";
                            purchase.agreement = "yes";
                            purchase.adbbeneficiary = form["txtBeneficiayname"];
                            purchase.adbrelation = "NP";

                            var primary = new Models.primary();
                            primary.firstName = form["txtTravelerFirstname1"];
                            primary.lastName = form["txtTravelerLastname"];
                            primary.email = form["txttraveleremail"];
                            primary.dob = Convert.ToDateTime(dateofbirth).ToString("yyyy-MM-dd");
                            primary.phone = Convert.ToInt64(form["txtTravelerMobile"]).ToString("###-###-####");
                            primary.gender = Gender;
                            primary.country = "IN";
                            primary.passport = form["TravelerPasspost"];
                            purchase.primary.Add(primary);

                            //var spouse = new Models.INFspouse();
                            //spouse.firstName = form["txtSpousefirstname"];
                            //spouse.lastName = form["txtspouselastname"];
                            //spouse.dob = Convert.ToDateTime(form["txtspousedateofbirth"]).ToString("yyyy-MM-dd");
                            //spouse.passport = form["txtTravelerPassNumSpouse"];

                            var child1 = new Models.INFChild1();
                            child1.firstName = form["txtchild1firstname"];
                            child1.lastName = form["txtchild1lastname"];
                            child1.dob = Convert.ToDateTime(form["txtchild1dob"]).ToString("yyyy-MM-dd");
                            child1.passport = form["txtTravelerPassNumchild1"];
                            purchase.child1.Add(child1);
                            var payment = new Models.payment();



                            payment.cardNumber = form["txtCardonnumber"];
                            payment.cardExpiry = "0425";
                            payment.cardSecurityCode = form["txtSecuritycode"];
                            payment.address = form["txtPrimaryApplicantAddress1"];
                            payment.city = form["txtTravelerCity"];
                            payment.state = "Jharkhand";
                            payment.zipCode = form["txtTravelerPostalCode"];
                            purchase.paymentInfo.Add(payment);
                            var travel = new Models.travel();
                            travel.arrivalDate = Convert.ToDateTime(form["txtCoverageStartDate"]).ToString("yyyy-MM-dd");
                            travel.countryToVisit = form["ddlWhereareyoutravelingto"];
                            //var displayname= form["displayplanname"];
                            purchase.travelInfo.Add(travel);

                            var json = JsonConverterINF.Serialize(purchase);


                            string INFPurchase = ConfigurationManager.AppSettings["INFPurchase"];

                            var t = Task.Run(() => PostRequest(json, INFPurchase, AuthKey, Domainname));
                            t.Wait();
                            string JsonResult = t.Result + "TRIPID:" + getTripID;
                            ViewBag.Message = JsonResult;


                        }
                        else if (PurchasePolicymodel.spouseage == "" && PurchasePolicymodel.child1age != "" && PurchasePolicymodel.child2age != "" && PurchasePolicymodel.child3age == "" && PurchasePolicymodel.child4age == "")
                        {
                            var purchase = new Models.INF_Child2_purchase_Primary();
                            purchase.planId = Convert.ToInt32(form["hdnPlanId"]);
                            purchase.coverageStartDate = Convert.ToDateTime(form["txtCoverageStartDate"]).ToString("yyyy-MM-dd");
                            purchase.coverageEndDate = Convert.ToDateTime(form["txtCoverageEndDate"]).ToString("yyyy-MM-dd");
                            purchase.coverageArea = "USA/CANADA";
                            purchase.agreement = "yes";
                            purchase.adbbeneficiary = form["txtBeneficiayname"];
                            purchase.adbrelation = "NP";

                            var primary = new Models.primary();
                            primary.firstName = form["txtTravelerFirstname1"];
                            primary.lastName = form["txtTravelerLastname"];
                            primary.email = form["txttraveleremail"];
                            primary.dob = Convert.ToDateTime(dateofbirth).ToString("yyyy-MM-dd");
                            primary.phone = Convert.ToInt64(form["txtTravelerMobile"]).ToString("###-###-####");
                            primary.gender = Gender;
                            primary.country = "IN";
                            primary.passport = form["TravelerPasspost"];
                            purchase.primary.Add(primary);

                            //var spouse = new Models.INFspouse();
                            //spouse.firstName = form["txtSpousefirstname"];
                            //spouse.lastName = form["txtspouselastname"];
                            //spouse.dob = Convert.ToDateTime(form["txtspousedateofbirth"]).ToString("yyyy-MM-dd");
                            //spouse.passport = form["txtTravelerPassNumSpouse"];
                            //purchase.spouse.Add(spouse);

                            var child1 = new Models.INFChild1();
                            child1.firstName = form["txtchild1firstname"];
                            child1.lastName = form["txtchild1lastname"];
                            child1.dob = Convert.ToDateTime(form["txtchild1dob"]).ToString("yyyy-MM-dd");
                            child1.passport = form["txtTravelerPassNumchild1"];
                            purchase.child1.Add(child1);

                            var child2 = new Models.INFChild2();
                            child2.firstName = form["txtchild2firstname"];
                            child2.lastName = form["txtchild2lastname"];
                            child2.dob = Convert.ToDateTime(form["txtchild2dob"]).ToString("yyyy-MM-dd");
                            child2.passport = form["txtTravelerPassNumChild2"];
                            purchase.child2.Add(child2);
                            var payment = new Models.payment();



                            payment.cardNumber = form["txtCardonnumber"];
                            payment.cardExpiry = "0425";
                            payment.cardSecurityCode = form["txtSecuritycode"];
                            payment.address = form["txtPrimaryApplicantAddress1"];
                            payment.city = form["txtTravelerCity"];
                            payment.state = "Jharkhand";
                            payment.zipCode = form["txtTravelerPostalCode"];
                            purchase.paymentInfo.Add(payment);
                            var travel = new Models.travel();
                            travel.arrivalDate = Convert.ToDateTime(form["txtCoverageStartDate"]).ToString("yyyy-MM-dd");
                            travel.countryToVisit = form["ddlWhereareyoutravelingto"];
                            //var displayname= form["displayplanname"];
                            purchase.travelInfo.Add(travel);

                            var json = JsonConverterINF.Serialize(purchase);


                            string INFPurchase = ConfigurationManager.AppSettings["INFPurchase"];

                            var t = Task.Run(() => PostRequest(json, INFPurchase, AuthKey, Domainname));
                            t.Wait();
                            string JsonResult = t.Result + "TRIPID:" + getTripID;
                            ViewBag.Message = JsonResult;


                        }
                        else if (PurchasePolicymodel.spouseage == "" && PurchasePolicymodel.child1age != "" && PurchasePolicymodel.child2age != "" && PurchasePolicymodel.child3age != "" && PurchasePolicymodel.child4age == "")
                        {
                            var purchase = new Models.INF_Child3_purchase_Primary();
                            purchase.planId = Convert.ToInt32(form["hdnPlanId"]);
                            purchase.coverageStartDate = Convert.ToDateTime(form["txtCoverageStartDate"]).ToString("yyyy-MM-dd");
                            purchase.coverageEndDate = Convert.ToDateTime(form["txtCoverageEndDate"]).ToString("yyyy-MM-dd");
                            purchase.coverageArea = "USA/CANADA";
                            purchase.agreement = "yes";
                            purchase.adbbeneficiary = form["txtBeneficiayname"];
                            purchase.adbrelation = "NP";

                            var primary = new Models.primary();
                            primary.firstName = form["txtTravelerFirstname1"];
                            primary.lastName = form["txtTravelerLastname"];
                            primary.email = form["txttraveleremail"];
                            primary.dob = Convert.ToDateTime(dateofbirth).ToString("yyyy-MM-dd");
                            primary.phone = Convert.ToInt64(form["txtTravelerMobile"]).ToString("###-###-####");
                            primary.gender = Gender;
                            primary.country = "IN";
                            primary.passport = form["TravelerPasspost"];
                            purchase.primary.Add(primary);

                            //var spouse = new Models.INFspouse();
                            //spouse.firstName = form["txtSpousefirstname"];
                            //spouse.lastName = form["txtspouselastname"];
                            //spouse.dob = Convert.ToDateTime(form["txtspousedateofbirth"]).ToString("yyyy-MM-dd");
                            //spouse.passport = form["txtTravelerPassNumSpouse"];
                            //purchase.spouse.Add(spouse);

                            var child1 = new Models.INFChild1();
                            child1.firstName = form["txtchild1firstname"];
                            child1.lastName = form["txtchild1lastname"];
                            child1.dob = Convert.ToDateTime(form["txtchild1dob"]).ToString("yyyy-MM-dd");
                            child1.passport = form["txtTravelerPassNumchild1"];
                            purchase.child1.Add(child1);

                            var child2 = new Models.INFChild2();
                            child2.firstName = form["txtchild2firstname"];
                            child2.lastName = form["txtchild2lastname"];
                            child2.dob = Convert.ToDateTime(form["txtchild2dob"]).ToString("yyyy-MM-dd");
                            child2.passport = form["txtTravelerPassNumChild2"];
                            purchase.child2.Add(child2);

                            var child3 = new Models.INFChild3();
                            child3.firstName = form["txtchild3firstname"];
                            child3.lastName = form["txtchild3lastname"];
                            child3.dob = Convert.ToDateTime(form["txtchild3dob"]).ToString("yyyy-MM-dd");
                            child3.passport = form["txtTravelerPassNumchild3"];
                            purchase.child3.Add(child3);
                            var payment = new Models.payment();



                            payment.cardNumber = form["txtCardonnumber"];
                            payment.cardExpiry = "0425";
                            payment.cardSecurityCode = form["txtSecuritycode"];
                            payment.address = form["txtPrimaryApplicantAddress1"];
                            payment.city = form["txtTravelerCity"];
                            payment.state = "Jharkhand";
                            payment.zipCode = form["txtTravelerPostalCode"];
                            purchase.paymentInfo.Add(payment);
                            var travel = new Models.travel();
                            travel.arrivalDate = Convert.ToDateTime(form["txtCoverageStartDate"]).ToString("yyyy-MM-dd");
                            travel.countryToVisit = form["ddlWhereareyoutravelingto"];
                            //var displayname= form["displayplanname"];
                            purchase.travelInfo.Add(travel);

                            var json = JsonConverterINF.Serialize(purchase);


                            string INFPurchase = ConfigurationManager.AppSettings["INFPurchase"];

                            var t = Task.Run(() => PostRequest(json, INFPurchase, AuthKey, Domainname));
                            t.Wait();
                            string JsonResult = t.Result + "TRIPID:" + getTripID;
                            ViewBag.Message = JsonResult;


                        }
                        else if (PurchasePolicymodel.spouseage == "" && PurchasePolicymodel.child1age != "" && PurchasePolicymodel.child2age != "" && PurchasePolicymodel.child3age != "" && PurchasePolicymodel.child4age != "")
                        {
                            var purchase = new Models.INF_Child4_purchase_Primary();
                            purchase.planId = Convert.ToInt32(form["hdnPlanId"]);
                            purchase.coverageStartDate = Convert.ToDateTime(form["txtCoverageStartDate"]).ToString("yyyy-MM-dd");
                            purchase.coverageEndDate = Convert.ToDateTime(form["txtCoverageEndDate"]).ToString("yyyy-MM-dd");
                            purchase.coverageArea = "USA/CANADA";
                            purchase.agreement = "yes";
                            purchase.adbbeneficiary = form["txtBeneficiayname"];
                            purchase.adbrelation = "NP";

                            var primary = new Models.primary();
                            primary.firstName = form["txtTravelerFirstname1"];
                            primary.lastName = form["txtTravelerLastname"];
                            primary.email = form["txttraveleremail"];
                            primary.dob = Convert.ToDateTime(dateofbirth).ToString("yyyy-MM-dd");
                            primary.phone = Convert.ToInt64(form["txtTravelerMobile"]).ToString("###-###-####");
                            primary.gender = Gender;
                            primary.country = "IN";
                            primary.passport = form["TravelerPasspost"];
                            purchase.primary.Add(primary);

                            //var spouse = new Models.INFspouse();
                            //spouse.firstName = form["txtSpousefirstname"];
                            //spouse.lastName = form["txtspouselastname"];
                            //spouse.dob = Convert.ToDateTime(form["txtspousedateofbirth"]).ToString("yyyy-MM-dd");
                            //spouse.passport = form["txtTravelerPassNumSpouse"];
                            //purchase.spouse.Add(spouse);

                            var child1 = new Models.INFChild1();
                            child1.firstName = form["txtchild1firstname"];
                            child1.lastName = form["txtchild1lastname"];
                            child1.dob = Convert.ToDateTime(form["txtchild1dob"]).ToString("yyyy-MM-dd");
                            child1.passport = form["txtTravelerPassNumchild1"];
                            purchase.child1.Add(child1);

                            var child2 = new Models.INFChild2();
                            child2.firstName = form["txtchild2firstname"];
                            child2.lastName = form["txtchild2lastname"];
                            child2.dob = Convert.ToDateTime(form["txtchild2dob"]).ToString("yyyy-MM-dd");
                            child2.passport = form["txtTravelerPassNumChild2"];
                            purchase.child2.Add(child2);

                            var child3 = new Models.INFChild3();
                            child3.firstName = form["txtchild3firstname"];
                            child3.lastName = form["txtchild3lastname"];
                            child3.dob = Convert.ToDateTime(form["txtchild3dob"]).ToString("yyyy-MM-dd");
                            child3.passport = form["txtTravelerPassNumchild3"];
                            purchase.child3.Add(child3);

                            var child4 = new Models.INFChild4();
                            child4.firstName = form["txtchild4firstname"];
                            child4.lastName = form["txtchild4lastname"];
                            child4.dob = Convert.ToDateTime(form["txtchild4dob"]).ToString("yyyy-MM-dd");
                            child4.passport = form["txtTravelerPassNumchild4"];
                            purchase.child4.Add(child4);

                            var payment = new Models.payment();



                            payment.cardNumber = form["txtCardonnumber"];
                            payment.cardExpiry = "0425";
                            payment.cardSecurityCode = form["txtSecuritycode"];
                            payment.address = form["txtPrimaryApplicantAddress1"];
                            payment.city = form["txtTravelerCity"];
                            payment.state = "Jharkhand";
                            payment.zipCode = form["txtTravelerPostalCode"];
                            purchase.paymentInfo.Add(payment);
                            var travel = new Models.travel();
                            travel.arrivalDate = Convert.ToDateTime(form["txtCoverageStartDate"]).ToString("yyyy-MM-dd");
                            travel.countryToVisit = form["ddlWhereareyoutravelingto"];
                            //var displayname= form["displayplanname"];
                            purchase.travelInfo.Add(travel);

                            var json = JsonConverterINF.Serialize(purchase);


                            string INFPurchase = ConfigurationManager.AppSettings["INFPurchase"];

                            var t = Task.Run(() => PostRequest(json, INFPurchase, AuthKey, Domainname));
                            t.Wait();
                            string JsonResult = t.Result + "TRIPID:" + getTripID;
                            ViewBag.Message = JsonResult;


                        }
                        else if (PurchasePolicymodel.spouseage != "" && PurchasePolicymodel.child1age == "" && PurchasePolicymodel.child2age == "" && PurchasePolicymodel.child3age == "" && PurchasePolicymodel.child4age == "")
                        {
                            var purchase = new Models.INF_Spouse_purchase();
                            purchase.planId = Convert.ToInt32(form["hdnPlanId"]);
                            purchase.coverageStartDate = Convert.ToDateTime(form["txtCoverageStartDate"]).ToString("yyyy-MM-dd");
                            purchase.coverageEndDate = Convert.ToDateTime(form["txtCoverageEndDate"]).ToString("yyyy-MM-dd");
                            purchase.coverageArea = "USA/CANADA";
                            purchase.agreement = "yes";
                            purchase.adbbeneficiary = form["txtBeneficiayname"];
                            purchase.adbrelation = "NP";

                            var primary = new Models.primary();
                            primary.firstName = form["txtTravelerFirstname1"];
                            primary.lastName = form["txtTravelerLastname"];
                            primary.email = form["txttraveleremail"];
                            primary.dob = Convert.ToDateTime(dateofbirth).ToString("yyyy-MM-dd");
                            primary.phone = Convert.ToInt64(form["txtTravelerMobile"]).ToString("###-###-####");
                            primary.gender = Gender;
                            primary.country = "IN";
                            primary.passport = form["TravelerPasspost"];
                            purchase.primary.Add(primary);

                            var spouse = new Models.INFspouse();
                            spouse.firstName = form["txtSpousefirstname"];
                            spouse.lastName = form["txtspouselastname"];
                            spouse.dob = Convert.ToDateTime(form["txtspousedateofbirth"]).ToString("yyyy-MM-dd");
                            spouse.passport = form["txtTravelerPassNumSpouse"];
                            purchase.spouse.Add(spouse);



                            var payment = new Models.payment();

                            payment.cardNumber = form["txtCardonnumber"];
                            payment.cardExpiry = "0425";
                            payment.cardSecurityCode = form["txtSecuritycode"];
                            payment.address = form["txtPrimaryApplicantAddress1"];
                            payment.city = form["txtTravelerCity"];
                            payment.state = "Jharkhand";
                            payment.zipCode = form["txtTravelerPostalCode"];
                            purchase.paymentInfo.Add(payment);
                            var travel = new Models.travel();
                            travel.arrivalDate = Convert.ToDateTime(form["txtCoverageStartDate"]).ToString("yyyy-MM-dd");
                            travel.countryToVisit = form["ddlWhereareyoutravelingto"];
                            //var displayname= form["displayplanname"];
                            purchase.travelInfo.Add(travel);

                            var json = JsonConverterINF.Serialize(purchase);


                            string INFPurchase = ConfigurationManager.AppSettings["INFPurchase"];

                            var t = Task.Run(() => PostRequest(json, INFPurchase, AuthKey, Domainname));
                            t.Wait();
                            string JsonResult = t.Result + "TRIPID:" + getTripID;
                            ViewBag.Message = JsonResult;


                        }

                        return ViewBag.Message;
                    }
                }
                else
                {
                    ModelState.AddModelError("Authorization", "Unable to authorize");
                    return ("Error");
                }
            }
            catch (Exception ex)
            {
                var error = ex;
                PurchasePolicymanager.SaveErrorLog("INFPurchase", error.ToString());
                return "0";
            }
        }

        public string TrawickPurchaseDetails(FormCollection form)
        {
            try
            {
                string dateofbirth = "";
                if (form["TravelersDOB"] == null || form["TravelersDOB"] == "")
                {
                    int ageoftraveler = Convert.ToInt32(form["age"]);
                    int getDOByear = Convert.ToInt32(DateTime.Now.Year) - ageoftraveler;
                    string getdate = DateTime.Now.Day.ToString();
                    string getmonth = DateTime.Now.Month.ToString();

                    dateofbirth = getmonth + "/" + getdate + "/" + getDOByear.ToString();
                }
                else
                {
                    dateofbirth = Convert.ToDateTime(form["TravelersDOB"]).ToString("MM/dd/yyyy");
                }
                PurchasePolicymodel.PlanId = Convert.ToInt64(form["hdnPlanId"]);
                PurchasePolicymodel.planDeductible = Convert.ToDouble(form["hdnplanDeductible"]);
                PurchasePolicymodel.planMaximum = Convert.ToDouble(form["hdnplanMaximum"]);
                PurchasePolicymodel.INsuranceComp = form["hdnINsuranceComp"];
                PurchasePolicymodel.policyName = form["hdnpolicyName"];
                PurchasePolicymodel.LeavingHome = Convert.ToDateTime(form["txtCoverageStartDate"]).ToString("MM/dd/yyyy");
                PurchasePolicymodel.TillDate = Convert.ToDateTime(form["txtCoverageEndDate"]).ToString("MM/dd/yyyy");
                PurchasePolicymodel.ISOCODE = form["ISOCODE"];
                PurchasePolicymodel.WhereareYouTravlingTo = form["ddlWhereareyoutravelingto"];
                PurchasePolicymodel.TravelersDOB = Convert.ToDateTime(dateofbirth).ToString("MM/dd/yyyy");

                PurchasePolicymodel.SpouseName = form["txtspousename"];
                PurchasePolicymodel.SpouseDOB = form["txtspousedateofbirth"];
                PurchasePolicymodel.ChildName = form["txtChildname"];
                PurchasePolicymodel.ChildDOB = form["txtchilddateofbirth"];
                PurchasePolicymodel.TravelerEmail = form["txttraveleremail"];
                PurchasePolicymodel.TravelerFirstName = form["txtTravelerFirstname1"];
                PurchasePolicymodel.TravelerMiddleName = form["txtTravelerMiddlename"];
                PurchasePolicymodel.TravelerLastName = form["txtTravelerLastname"];
                PurchasePolicymodel.Gender = form["Gender1"];
                PurchasePolicymodel.TravelerPasspost = form["TravelerPasspost"];
                PurchasePolicymodel.DeparturefromHomeCountry = form["txtDeparturefromHomeCountry"];
                PurchasePolicymodel.DateofReturntoHome = form["txtDateofReturntoHome"];
                PurchasePolicymodel.Primaryapplicantfirstname = form["txtPrimaryapplicantfirstname"];
                PurchasePolicymodel.PrimaryapplicantLastname = form["txtPrimaryapplicantLastname"];
                PurchasePolicymodel.PrimaryApplicantAddress1 = form["txtPrimaryApplicantAddress1"];
                PurchasePolicymodel.PrimaryApplicantAddress2 = PurchasePolicymodel.PrimaryApplicantAddress2;
                PurchasePolicymodel.TravelerCity = form["txtTravelerCity"];
                PurchasePolicymodel.TravelerPostalCode = form["txtTravelerPostalCode"];
                PurchasePolicymodel.TravelerEmailConfirm = PurchasePolicymodel.TravelerEmailConfirm;
                PurchasePolicymodel.TravelerMobile = form["txtTravelerMobile"];
                PurchasePolicymodel.Beneficiayname = form["txtBeneficiayname"];
                PurchasePolicymodel.Relationship = PurchasePolicymodel.Relationship;
                PurchasePolicymodel.PolicyDocument = PurchasePolicymodel.PolicyDocument;
                PurchasePolicymodel.Totalamount = form["hdntotalPremium"];
                PurchasePolicymodel.Countryofcitizenship = form["ddlcitizenship"];
                PurchasePolicymodel.Cardname = form["ddlcardname"];
                PurchasePolicymodel.Month = form["ddlMonth"];
                PurchasePolicymodel.Expiryyear = form["ddlExpiryyear"];
                PurchasePolicymodel.Nameoncard = form["txtNameoncard"];
                PurchasePolicymodel.Cardonnumber = form["txtCardonnumber"];
                PurchasePolicymodel.Securitycode = form["txtSecuritycode"];
                PurchasePolicymodel.TravelerState = form["txtBeneficiayname"];
                PurchasePolicymodel.BillingAddress = form["txtBillingAddress"];
                PurchasePolicymodel.BillingAddress2 = form["txtBillingAddress2"];
                PurchasePolicymodel.BillingState = form["ddlBillingState"];
                PurchasePolicymodel.BillingCity = form["txtBillingCity"];
                PurchasePolicymodel.BillingZipcode = form["txtBillingZipcode"];
                var userid = PurchasePolicymanager.SaveUserdetails(PurchasePolicymodel);
                var getTripID = PurchasePolicymanager.saveDetailsIntripMaster(PurchasePolicymodel);
                long ID = Convert.ToInt64(form["hdnPlanId"]);
                double planDeductible = Convert.ToDouble(form["hdnplanDeductible"]);
                double planMaximum = Convert.ToDouble(form["hdnplanMaximum"]);
                string INsuranceComp = form["hdnINsuranceComp"];
                string policyName = form["hdnpolicyName"];
                string LeavingHome = Convert.ToDateTime(form["txtCoverageStartDate"]).ToString("MM/dd/yyyy");
                string TillDate = Convert.ToDateTime(form["txtCoverageEndDate"]).ToString("MM/dd/yyyy");
                string ISOCODE = form["ISOCODE"];
                string TouristISOCODE = form["ddlWhereareyoutravelingto"];
                string TravelersDOB = dateofbirth;
                string TouristCountryName = PurchasePolicymodel.TouristCountryName;
                string WhereareYouTravlingTo = PurchasePolicymodel.WhereareYouTravlingTo;
                string SpouseName = form["txtspousename"];
                //if (form["txtspousedateofbirth"] != "")
                //    { 
                //string SpouseDOB = Convert.ToDateTime(form["txtspousedateofbirth"]).ToString("MM/dd/yyyy");
                //}
                //string ChildName = form["txtChildname"];
                //if(form["txtchilddateofbirth"] != "") { 
                //string ChildDOB = form["txtchilddateofbirth"];
                //}
                string TravelerEmail = form["txttraveleremail"];
                string TravelerFirstName = form["txtTravelerFirstname1"];
                string TravelerMiddleName = form["txtTravelerMiddlename"];
                string TravelerLastName = form["txtTravelerLastname"];
                string Gender = form["Gender1"];
                string TravelerPasspost = form["TravelerPasspost"];
                string DeparturefromHomeCountry = form["txtDeparturefromHomeCountry"];
                string DateofReturntoHome = form["txtDateofReturntoHome"];
                string Primaryapplicantfirstname = form["txtPrimaryapplicantfirstname"];
                string PrimaryapplicantLastname = form["txtPrimaryapplicantLastname"];
                string PrimaryApplicantAddress1 = form["txtPrimaryApplicantAddress1"];
                string PrimaryApplicantAddress2 = PurchasePolicymodel.PrimaryApplicantAddress2;
                string TravelerCity = form["txtTravelerCity"];
                string TravelerPostalCode = form["txtTravelerPostalCode"];
                string TravelerEmailConfirm = PurchasePolicymodel.TravelerEmailConfirm;
                string TravelerMobile = form["txtTravelerMobile"];
                string Beneficiayname = form["txtBeneficiayname"];
                string Relationship = PurchasePolicymodel.Relationship;
                string PolicyDocument = PurchasePolicymodel.PolicyDocument;

                string Countryofcitizenship = PurchasePolicymodel.Countryofcitizenship;
                string Cardname = form["ddlcardname"];
                string Month = form["ddlMonth"];
                string Expiryyear = form["ddlExpiryyear"];
                string Nameoncard = form["txtNameoncard"];
                string Cardonnumber = form["txtCardonnumber"];
                string Securitycode = form["txtSecuritycode"];
                string TravelerState = form["txtBeneficiayname"];
                string BillingAddress = form["txtBillingAddress"];
                string BillingAddress2 = form["txtBillingAddress2"];
                string BillingState = form["ddlBillingState"];
                string BillingCity = form["txtBillingCity"];
                string BillingZipcode = form["txtBillingZipcode"];
                string citizenship = form["ddlcitizenship"];
                string txtchild1firstname = form["txtchild1firstname"];
                string txtchild1lastname = form["txtchild1lastname"];



                //string finaldt = string.Empty;
                //txtchild1dob = Convert.ToDateTime(txtchild1dob).ToString("dd/MM/yyyy");
                //string[] strdt = txtchild1dob.Split('-');
                //finaldt = strdt[1] + "/" + strdt[0] + "/" + strdt[2];

                //txtchild1dob = Convert.ToDateTime(finaldt).ToString("dd/MM/yyyy");
                string txtchild2firstname = form["txtchild2firstname"];
                string txtchild2middlename = form["txtchild2middlename"];
                string txtchild2lastname = form["txtchild2lastname"];
                string txtchild1dob = "";
                if (form["txtchild1dob"] != "")
                {
                    txtchild1dob = Convert.ToDateTime(form["txtchild1dob"]).ToString("MM/dd/yyyy");
                }

                string txtchild2dob = "";
                if (form["txtchild2dob"] != "")
                {
                    txtchild2dob = Convert.ToDateTime(form["txtchild2dob"]).ToString("MM/dd/yyyy");
                }
                string txtchild3dob = "";
                if (form["txtchild3dob"] != "")
                {
                    txtchild3dob = Convert.ToDateTime(form["txtchild3dob"]).ToString("MM/dd/yyyy");
                }
                string txtchild4dob = "";
                if (form["txtchild4dob"] != "")
                {
                    txtchild4dob = Convert.ToDateTime(form["txtchild4dob"]).ToString("MM/dd/yyyy");
                }
                string txtspousedateofbirth = "";
                if (form["txtspousedateofbirth"] != "")
                {
                    txtspousedateofbirth = Convert.ToDateTime(form["txtspousedateofbirth"]).ToString("MM/dd/yyyy");
                }
                string txtTravelerPassNumChild2 = form["txtTravelerPassNumChild2"];
                string txtSpousefirstname = form["txtSpousefirstname"];
                string txtspousemiddlename = form["txtspousemiddlename"];
                string txtspouselastname = form["txtspouselastname"];

                string SpouseGender = form["SpouseGender"];
                string txtchild1midname = form["txtchild1midname"];
                string child1radio = form["child1radio"];
                string child2radio = form["child2radio"];
                string child3radio = form["child3radio"];
                string child4radio = form["child4radio"];
                string txtchild3firstname = form["txtchild3firstname"];
                string txtchild3lastname = form["txtchild3lastname"];
                string txtchild3middlename = form["txtchild3middlename"];
                string txtchild4firstname = form["txtchild4firstname"];
                string txtchild4lastname = form["txtchild4lastname"];
                string txtchild4middlename = form["txtchild4middlename"];

                string TrawickPurchase = ConfigurationManager.AppSettings["TrawickPurchase"];
                string URL = TrawickPurchase;
                string postData = string.Empty;
                if (form["txtspousedateofbirth"] == "" && form["txtchild1dob"] == "" && form["txtchild2dob"] == "" && form["txtchild3dob"] == "" && form["txtchild4dob"] == "")
                {
                    postData = "product=" + ID + "&eff_date=" + LeavingHome + "&term_date=" + TillDate + "&country=" + ISOCODE + "&destination=" + TouristISOCODE + "&policy_max="
        + planMaximum + "&deductible=" + planDeductible + "&dob1=" + TravelersDOB + "&t1First=" + TravelerFirstName + "&t1Middle=" + TravelerMiddleName + "&t1Last=" + TravelerLastName +
        "&t1Gender=" + Gender + "&mainEmail=" + TravelerEmail + "&phone=" + TravelerMobile + "&street=" + PrimaryApplicantAddress1 + "&city=" + TravelerCity + "&state=AL" + "&zip=" + TravelerPostalCode +
        "&homecountry=" + ISOCODE + "&cc_name=" + Nameoncard + "&cc_street=" + BillingAddress + " " + BillingAddress2 + "&cc_city=" + BillingCity + "&cc_statecode=AL" + "&cc_postalcode=" + BillingZipcode +
        "&cc_country=" + ISOCODE + "&cc_number=" + Cardonnumber + "&cc_month=" + Month + "&cc_year=" + Expiryyear + "&cc_cvv=" + Securitycode + "&agent_id=" + 1 + "&completeOrder=" + true + "";

                }
                else if (form["txtspousedateofbirth"] != "" && form["txtchild1dob"] == "" && form["txtchild2dob"] == "" && form["txtchild3dob"] == "" && form["txtchild4dob"] == "")
                {
                    postData = "product=" + ID + "&eff_date=" + LeavingHome + "&term_date=" + TillDate + "&country=" + ISOCODE + "&destination=" + TouristISOCODE + "&policy_max="
        + planMaximum + "&deductible=" + planDeductible + "&dob1=" + TravelersDOB + "&t1First=" + TravelerFirstName + "&t1Middle=" + TravelerMiddleName + "&t1Last=" + TravelerLastName +
        "&t1Gender=" + Gender + "&dob2=" + txtspousedateofbirth + "&t2First=" + txtSpousefirstname + "&t2Middle=" + txtspousemiddlename + "&t2Last=" + txtspouselastname +
        "&t2Gender=" + SpouseGender +
        "&mainEmail=" + TravelerEmail + "&phone=" + TravelerMobile + "&street=" + PrimaryApplicantAddress1 + "&city=" + TravelerCity + "&state=AL" + "&zip=" + TravelerPostalCode +
        "&homecountry=" + ISOCODE + "&cc_name=" + Nameoncard + "&cc_street=" + BillingAddress + " " + BillingAddress2 + "&cc_city=" + BillingCity + "&cc_statecode=AL" + "&cc_postalcode=" + BillingZipcode +
        "&cc_country=" + ISOCODE + "&cc_number=" + Cardonnumber + "&cc_month=" + Month + "&cc_year=" + Expiryyear + "&cc_cvv=" + Securitycode + "&agent_id=" + 1 + "&completeOrder=" + true + "";

                }
                else if (form["txtspousedateofbirth"] != "" && form["txtchild1dob"] != "" && form["txtchild2dob"] == "" && form["txtchild3dob"] == "" && form["txtchild4dob"] == "")
                {
                    postData = "product=" + ID + "&eff_date=" + LeavingHome + "&term_date=" + TillDate + "&country=" + ISOCODE + "&destination=" + TouristISOCODE + "&policy_max="
        + planMaximum + "&deductible=" + planDeductible +
        "&dob1=" + TravelersDOB + "&t1First=" + TravelerFirstName + "&t1Middle=" + TravelerMiddleName + "&t1Last=" + TravelerLastName +
        "&t1Gender=" + Gender +
        "&dob2=" + txtspousedateofbirth + "&t2First=" + txtSpousefirstname + "&t2Middle=" + txtspousemiddlename + "&t2Last=" + txtspouselastname +
        "&t2Gender=" + SpouseGender +
        "&dob3=" + txtchild1dob + "&t3First=" + txtchild1firstname + "&t3Middle=" + txtchild1midname + "&t3Last=" + txtchild1lastname +
        "&t3Gender=" + child1radio +
        "&mainEmail=" + TravelerEmail + "&phone=" + TravelerMobile + "&street=" + PrimaryApplicantAddress1 + "&city=" + TravelerCity + "&state=AL" + "&zip=" + TravelerPostalCode +
        "&homecountry=" + ISOCODE + "&cc_name=" + Nameoncard + "&cc_street=" + BillingAddress + " " + BillingAddress2 + "&cc_city=" + BillingCity + "&cc_statecode=AL" + "&cc_postalcode=" + BillingZipcode +
        "&cc_country=" + ISOCODE + "&cc_number=" + Cardonnumber + "&cc_month=" + Month + "&cc_year=" + Expiryyear + "&cc_cvv=" + Securitycode + "&agent_id=" + 1 + "&completeOrder=" + true + "";

                }
                else if (form["txtspousedateofbirth"] != "" && form["txtchild1dob"] != "" && form["txtchild2dob"] != "" && form["txtchild3dob"] == "" && form["txtchild4dob"] == "")
                {
                    postData = "product=" + ID + "&eff_date=" + LeavingHome + "&term_date=" + TillDate + "&country=" + ISOCODE + "&destination=" + TouristISOCODE + "&policy_max="
        + planMaximum + "&deductible=" + planDeductible +
        "&dob1=" + TravelersDOB + "&t1First=" + TravelerFirstName + "&t1Middle=" + TravelerMiddleName + "&t1Last=" + TravelerLastName +
        "&t1Gender=" + Gender +
        "&dob2=" + txtspousedateofbirth + "&t2First=" + txtSpousefirstname + "&t2Middle=" + txtspousemiddlename + "&t2Last=" + txtspouselastname +
        "&t2Gender=" + SpouseGender +
        "&dob3=" + txtchild1dob + "&t3First=" + txtchild1firstname + "&t3Middle=" + txtchild1midname + "&t3Last=" + txtchild1lastname +
        "&t3Gender=" + child1radio +
        "&dob4=" + txtchild2dob + "&t4First=" + txtchild2firstname + "&t4Middle=" + txtchild2middlename + "&t4Last=" + txtchild2lastname +
        "&t4Gender=" + child2radio +
        "&mainEmail=" + TravelerEmail + "&phone=" + TravelerMobile + "&street=" + PrimaryApplicantAddress1 + "&city=" + TravelerCity + "&state=AL" + "&zip=" + TravelerPostalCode +
        "&homecountry=" + ISOCODE + "&cc_name=" + Nameoncard + "&cc_street=" + BillingAddress + " " + BillingAddress2 + "&cc_city=" + BillingCity + "&cc_statecode=AL" + "&cc_postalcode=" + BillingZipcode +
        "&cc_country=" + ISOCODE + "&cc_number=" + Cardonnumber + "&cc_month=" + Month + "&cc_year=" + Expiryyear + "&cc_cvv=" + Securitycode + "&agent_id=" + 1 + "&completeOrder=" + true + "";

                }
                else if (form["txtspousedateofbirth"] != "" && form["txtchild1dob"] != "" && form["txtchild2dob"] != "" && form["txtchild3dob"] != "" && form["txtchild4dob"] == "")
                {
                    postData = "product=" + ID + "&eff_date=" + LeavingHome + "&term_date=" + TillDate + "&country=" + ISOCODE + "&destination=" + TouristISOCODE + "&policy_max="
        + planMaximum + "&deductible=" + planDeductible +
        "&dob1=" + TravelersDOB + "&t1First=" + TravelerFirstName + "&t1Middle=" + TravelerMiddleName + "&t1Last=" + TravelerLastName +
        "&t1Gender=" + Gender +
        "&dob2=" + txtspousedateofbirth + "&t2First=" + txtSpousefirstname + "&t2Middle=" + txtspousemiddlename + "&t2Last=" + txtspouselastname +
        "&t2Gender=" + SpouseGender +
        "&dob3=" + txtchild1dob + "&t3First=" + txtchild1firstname + "&t3Middle=" + txtchild1midname + "&t3Last=" + txtchild1lastname +
        "&t3Gender=" + child1radio +
        "&dob4=" + txtchild2dob + "&t4First=" + txtchild2firstname + "&t4Middle=" + txtchild2middlename + "&t4Last=" + txtchild2lastname +
        "&t4Gender=" + child2radio +
        "&dob5=" + txtchild3dob + "&t5First=" + txtchild3firstname + "&t5Middle=" + txtchild3middlename + "&t5Last=" + txtchild3lastname +
        "&t5Gender=" + child3radio +

        "&mainEmail=" + TravelerEmail + "&phone=" + TravelerMobile + "&street=" + PrimaryApplicantAddress1 + "&city=" + TravelerCity + "&state=AL" + "&zip=" + TravelerPostalCode +
        "&homecountry=" + ISOCODE + "&cc_name=" + Nameoncard + "&cc_street=" + BillingAddress + " " + BillingAddress2 + "&cc_city=" + BillingCity + "&cc_statecode=AL" + "&cc_postalcode=" + BillingZipcode +
        "&cc_country=" + ISOCODE + "&cc_number=" + Cardonnumber + "&cc_month=" + Month + "&cc_year=" + Expiryyear + "&cc_cvv=" + Securitycode + "&agent_id=" + 1 + "&completeOrder=" + true + "";

                }
                else if (form["txtspousedateofbirth"] != "" && form["txtchild1dob"] != "" && form["txtchild2dob"] != "" && form["txtchild3dob"] != "" && form["txtchild4dob"] != "")
                {
                    postData = "product=" + ID + "&eff_date=" + LeavingHome + "&term_date=" + TillDate + "&country=" + ISOCODE + "&destination=" + TouristISOCODE + "&policy_max="
        + planMaximum + "&deductible=" + planDeductible +
        "&dob1=" + TravelersDOB + "&t1First=" + TravelerFirstName + "&t1Middle=" + TravelerMiddleName + "&t1Last=" + TravelerLastName +
        "&t1Gender=" + Gender +
        "&dob2=" + txtspousedateofbirth + "&t2First=" + txtSpousefirstname + "&t2Middle=" + txtspousemiddlename + "&t2Last=" + txtspouselastname +
        "&t2Gender=" + SpouseGender +
        "&dob3=" + txtchild1dob + "&t3First=" + txtchild1firstname + "&t3Middle=" + txtchild1midname + "&t3Last=" + txtchild1lastname +
        "&t3Gender=" + child1radio +
        "&dob4=" + txtchild2dob + "&t4First=" + txtchild2firstname + "&t4Middle=" + txtchild2middlename + "&t4Last=" + txtchild2lastname +
        "&t4Gender=" + child2radio +
        "&dob5=" + txtchild3dob + "&t5First=" + txtchild3firstname + "&t5Middle=" + txtchild3middlename + "&t5Last=" + txtchild3lastname +
        "&t5Gender=" + child3radio +
        "&dob6=" + txtchild4dob + "&t6First=" + txtchild4firstname + "&t6Middle=" + txtchild4middlename + "&t6Last=" + txtchild4lastname +
        "&t6Gender=" + child4radio +
        "&mainEmail=" + TravelerEmail + "&phone=" + TravelerMobile + "&street=" + PrimaryApplicantAddress1 + "&city=" + TravelerCity + "&state=AL" + "&zip=" + TravelerPostalCode +
        "&homecountry=" + ISOCODE + "&cc_name=" + Nameoncard + "&cc_street=" + BillingAddress + " " + BillingAddress2 + "&cc_city=" + BillingCity + "&cc_statecode=AL" + "&cc_postalcode=" + BillingZipcode +
        "&cc_country=" + ISOCODE + "&cc_number=" + Cardonnumber + "&cc_month=" + Month + "&cc_year=" + Expiryyear + "&cc_cvv=" + Securitycode + "&agent_id=" + 1 + "&completeOrder=" + true + "";

                }

                else if (form["txtspousedateofbirth"] == "" && form["txtchild1dob"] != "" && form["txtchild2dob"] == "" && form["txtchild3dob"] == "" && form["txtchild4dob"] == "")
                {
                    postData = "product=" + ID + "&eff_date=" + LeavingHome + "&term_date=" + TillDate + "&country=" + ISOCODE + "&destination=" + TouristISOCODE + "&policy_max="
        + planMaximum + "&deductible=" + planDeductible +
        "&dob1=" + TravelersDOB + "&t1First=" + TravelerFirstName + "&t1Middle=" + TravelerMiddleName + "&t1Last=" + TravelerLastName +
        "&t1Gender=" + Gender +
        //"&dob2=" + txtspousedateofbirth + "&t2First=" + txtSpousefirstname + "&t2Middle=" + txtspousemiddlename + "&t2Last=" + txtspouselastname +
        //"&t2Gender=" + SpouseGender +
        "&dob2=" + txtchild1dob + "&t2First=" + txtchild1firstname + "&t2Middle=" + txtchild1midname + "&t2Last=" + txtchild1lastname +
        "&t2Gender=" + child1radio +
        "&mainEmail=" + TravelerEmail + "&phone=" + TravelerMobile + "&street=" + PrimaryApplicantAddress1 + "&city=" + TravelerCity + "&state=AL" + "&zip=" + TravelerPostalCode +
        "&homecountry=" + ISOCODE + "&cc_name=" + Nameoncard + "&cc_street=" + BillingAddress + " " + BillingAddress2 + "&cc_city=" + BillingCity + "&cc_statecode=AL" + "&cc_postalcode=" + BillingZipcode +
        "&cc_country=" + ISOCODE + "&cc_number=" + Cardonnumber + "&cc_month=" + Month + "&cc_year=" + Expiryyear + "&cc_cvv=" + Securitycode + "&agent_id=" + 1 + "&completeOrder=" + true + "";

                }
                else if (form["txtspousedateofbirth"] == "" && form["txtchild1dob"] != "" && form["txtchild2dob"] != "" && form["txtchild3dob"] == "" && form["txtchild4dob"] == "")
                {
                    postData = "product=" + ID + "&eff_date=" + LeavingHome + "&term_date=" + TillDate + "&country=" + ISOCODE + "&destination=" + TouristISOCODE + "&policy_max="
        + planMaximum + "&deductible=" + planDeductible +
        "&dob1=" + TravelersDOB + "&t1First=" + TravelerFirstName + "&t1Middle=" + TravelerMiddleName + "&t1Last=" + TravelerLastName +
        "&t1Gender=" + Gender +
        //"&dob2=" + txtspousedateofbirth + "&t2First=" + txtSpousefirstname + "&t2Middle=" + txtspousemiddlename + "&t2Last=" + txtspouselastname +
        //"&t2Gender=" + SpouseGender +
        "&dob2=" + txtchild1dob + "&t2First=" + txtchild1firstname + "&t2Middle=" + txtchild1midname + "&t2Last=" + txtchild1lastname +
        "&t2Gender=" + child1radio +
        "&dob3=" + txtchild2dob + "&t3First=" + txtchild2firstname + "&t3Middle=" + txtchild2middlename + "&t3Last=" + txtchild2lastname +
        "&t3Gender=" + child2radio +
        "&mainEmail=" + TravelerEmail + "&phone=" + TravelerMobile + "&street=" + PrimaryApplicantAddress1 + "&city=" + TravelerCity + "&state=AL" + "&zip=" + TravelerPostalCode +
        "&homecountry=" + ISOCODE + "&cc_name=" + Nameoncard + "&cc_street=" + BillingAddress + " " + BillingAddress2 + "&cc_city=" + BillingCity + "&cc_statecode=AL" + "&cc_postalcode=" + BillingZipcode +
        "&cc_country=" + ISOCODE + "&cc_number=" + Cardonnumber + "&cc_month=" + Month + "&cc_year=" + Expiryyear + "&cc_cvv=" + Securitycode + "&agent_id=" + 1 + "&completeOrder=" + true + "";

                }
                else if (form["txtspousedateofbirth"] == "" && form["txtchild1dob"] != "" && form["txtchild2dob"] != "" && form["txtchild3dob"] != "" && form["txtchild4dob"] == "")
                {
                    postData = "product=" + ID + "&eff_date=" + LeavingHome + "&term_date=" + TillDate + "&country=" + ISOCODE + "&destination=" + TouristISOCODE + "&policy_max="
        + planMaximum + "&deductible=" + planDeductible +
        "&dob1=" + TravelersDOB + "&t1First=" + TravelerFirstName + "&t1Middle=" + TravelerMiddleName + "&t1Last=" + TravelerLastName +
        "&t1Gender=" + Gender +
        //"&dob2=" + txtspousedateofbirth + "&t2First=" + txtSpousefirstname + "&t2Middle=" + txtspousemiddlename + "&t2Last=" + txtspouselastname +
        //"&t2Gender=" + SpouseGender +
        "&dob2=" + txtchild1dob + "&t2First=" + txtchild1firstname + "&t2Middle=" + txtchild1midname + "&t2Last=" + txtchild1lastname +
        "&t2Gender=" + child1radio +
        "&dob3=" + txtchild2dob + "&t3First=" + txtchild2firstname + "&t3Middle=" + txtchild2middlename + "&t3Last=" + txtchild2lastname +
        "&t3Gender=" + child2radio +
        "&dob4=" + txtchild3dob + "&t4First=" + txtchild3firstname + "&t4Middle=" + txtchild3middlename + "&t4Last=" + txtchild3lastname +
        "&t4Gender=" + child3radio +

        "&mainEmail=" + TravelerEmail + "&phone=" + TravelerMobile + "&street=" + PrimaryApplicantAddress1 + "&city=" + TravelerCity + "&state=AL" + "&zip=" + TravelerPostalCode +
        "&homecountry=" + ISOCODE + "&cc_name=" + Nameoncard + "&cc_street=" + BillingAddress + " " + BillingAddress2 + "&cc_city=" + BillingCity + "&cc_statecode=AL" + "&cc_postalcode=" + BillingZipcode +
        "&cc_country=" + ISOCODE + "&cc_number=" + Cardonnumber + "&cc_month=" + Month + "&cc_year=" + Expiryyear + "&cc_cvv=" + Securitycode + "&agent_id=" + 1 + "&completeOrder=" + true + "";

                }
                else if (form["txtspousedateofbirth"] == "" && form["txtchild1dob"] != "" && form["txtchild2dob"] != "" && form["txtchild3dob"] != "" && form["txtchild4dob"] != "")
                {
                    postData = "product=" + ID + "&eff_date=" + LeavingHome + "&term_date=" + TillDate + "&country=" + ISOCODE + "&destination=" + TouristISOCODE + "&policy_max="
        + planMaximum + "&deductible=" + planDeductible +
        "&dob1=" + TravelersDOB + "&t1First=" + TravelerFirstName + "&t1Middle=" + TravelerMiddleName + "&t1Last=" + TravelerLastName +
        "&t1Gender=" + Gender +
        //"&dob2=" + txtspousedateofbirth + "&t2First=" + txtSpousefirstname + "&t2Middle=" + txtspousemiddlename + "&t2Last=" + txtspouselastname +
        //"&t2Gender=" + SpouseGender +
        "&dob2=" + txtchild1dob + "&t2First=" + txtchild1firstname + "&t2Middle=" + txtchild1midname + "&t2Last=" + txtchild1lastname +
        "&t2Gender=" + child1radio +
        "&dob3=" + txtchild2dob + "&t3First=" + txtchild2firstname + "&t3Middle=" + txtchild2middlename + "&t3Last=" + txtchild2lastname +
        "&t3Gender=" + child2radio +
        "&dob4=" + txtchild3dob + "&t4First=" + txtchild3firstname + "&t4Middle=" + txtchild3middlename + "&t4Last=" + txtchild3lastname +
        "&t4Gender=" + child3radio +
        "&dob5=" + txtchild4dob + "&t5First=" + txtchild4firstname + "&t5Middle=" + txtchild4middlename + "&t5Last=" + txtchild4lastname +
        "&t5Gender=" + child4radio +
        "&mainEmail=" + TravelerEmail + "&phone=" + TravelerMobile + "&street=" + PrimaryApplicantAddress1 + "&city=" + TravelerCity + "&state=AL" + "&zip=" + TravelerPostalCode +
        "&homecountry=" + ISOCODE + "&cc_name=" + Nameoncard + "&cc_street=" + BillingAddress + " " + BillingAddress2 + "&cc_city=" + BillingCity + "&cc_statecode=AL" + "&cc_postalcode=" + BillingZipcode +
        "&cc_country=" + ISOCODE + "&cc_number=" + Cardonnumber + "&cc_month=" + Month + "&cc_year=" + Expiryyear + "&cc_cvv=" + Securitycode + "&agent_id=" + 1 + "&completeOrder=" + true + "";

                }


                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
                request.Method = "POST";
                request.ContentLength = postData.Length;
                request.ContentType = "application/x-www-form-urlencoded";

                using (Stream writeStream = request.GetRequestStream())
                {
                    byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                    writeStream.Write(byteArray, 0, byteArray.Length);
                    writeStream.Close();
                }

                string result = string.Empty;

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        using (StreamReader readStream = new StreamReader(responseStream, Encoding.UTF8))
                        {
                            result = readStream.ReadToEnd() + "TRIPID:" + getTripID;
                        }

                    }
                }


                ViewBag.Message = result.ToString();


                return ViewBag.Message;
            }
            catch (Exception ex)
            {
                var error = ex;
                PurchasePolicymanager.SaveErrorLog("TrawickPurchase", error.ToString());
                return "0";
            }
        }


        public ActionResult AtlasPurchaseDetails(FormCollection form)
        {

            try
            {


                string dateofbirth = "";
                if (form["TravelersDOB"] == null || form["TravelersDOB"] == "")
                {
                    int ageoftraveler = Convert.ToInt32(form["age"]);
                    int getDOByear = Convert.ToInt32(DateTime.Now.Year) - ageoftraveler;
                    string getdate = DateTime.Now.Day.ToString();
                    string getmonth = DateTime.Now.Month.ToString();

                    dateofbirth = getmonth + "/" + getdate + "/" + getDOByear.ToString();
                }
                else
                {
                    dateofbirth = (form["TravelersDOB"]);
                    dateofbirth = Convert.ToDateTime(form["TravelersDOB"]).ToString("MM/dd/yyyy");
                    dateofbirth = Convert.ToDateTime(form["TravelersDOB"]).ToString("MM/dd/yyyy");
                }
                string SpouseDOB = form["txtspousedateofbirth"];
                if (SpouseDOB != "")
                {
                    SpouseDOB = Convert.ToDateTime(form["txtspousedateofbirth"]).ToString("MM/dd/yyyy");
                }
                string Child1DOB = form["txtchild1dob"];
                if (Child1DOB != "")
                {
                    Child1DOB = Convert.ToDateTime(form["txtchild1dob"]).ToString("MM/dd/yyyy");
                }
                string Child2DOB = form["txtchild2dob"];
                if (Child2DOB != "")
                {
                    Child2DOB = Convert.ToDateTime(form["txtchild2dob"]).ToString("MM/dd/yyyy");
                }
                string Child3DOB = form["txtchild3dob"];
                if (Child3DOB != "")
                {
                    Child3DOB = Convert.ToDateTime(form["txtchild3dob"]).ToString("MM/dd/yyyy");
                }
                string Child4DOB = form["txtchild4dob"];
                if (Child4DOB != "")
                {
                    Child4DOB = Convert.ToDateTime(form["txtchild4dob"]).ToString("MM/dd/yyyy");
                }

                string Gender = "";
                if (form["Gender"] == "MALE")
                {
                    Gender = "M";
                }
                else
                {
                    Gender = "F";
                }
                string SpouseGender = "";
                if (form["SpouseGender"] == "MALE")
                {
                    SpouseGender = "M";
                }
                else
                {
                    SpouseGender = "F";
                }
                string child1gender = "";
                if (form["child1radio"] == "MALE")
                {
                    child1gender = "M";
                }
                else
                {
                    child1gender = "F";
                }
                string child2gender = "";
                if (form["child2radio"] == "MALE")
                {
                    child2gender = "M";
                }
                else
                {
                    child2gender = "F";
                }
                string child3gender = "";
                if (form["child3radio"] == "MALE")
                {
                    child3gender = "M";
                }
                else
                {
                    child3gender = "F";
                }
                string child4gender = "";
                if (form["child4radio"] == "MALE")
                {
                    child4gender = "M";
                }
                else
                {
                    child4gender = "F";
                }

                if (form["txtspousedateofbirth"] == null)
                {
                    form["txtspousedateofbirth"] = "";
                }
                if (form["txtchild1dob"] == null)
                {
                    form["txtchild1dob"] = "";
                }
                if (form["txtchild2dob"] == null)
                {
                    form["txtchild2dob"] = "";
                }
                if (form["txtchild3dob"] == null)
                {
                    form["txtchild2dob"] = "";
                }
                if (form["txtchild4dob"] == null)
                {
                    form["txtchild4dob"] = "";
                }
                PurchasePolicymodel.PlanId = Convert.ToInt64(form["hdnPlanId"]);
                PurchasePolicymodel.planDeductible = Convert.ToDouble(form["hdnplanDeductible"]);
                PurchasePolicymodel.planMaximum = Convert.ToDouble(form["hdnplanMaximum"]);
                PurchasePolicymodel.INsuranceComp = form["hdnINsuranceComp"];
                PurchasePolicymodel.policyName = form["hdnpolicyName"];
                PurchasePolicymodel.LeavingHome = Convert.ToDateTime(form["txtCoverageStartDate"]).ToString("MM/dd/yyyy");
                PurchasePolicymodel.TillDate = Convert.ToDateTime(form["txtCoverageEndDate"]).ToString("MM/dd/yyyy");
                PurchasePolicymodel.ISOCODE = form["ISOCODE"];
                PurchasePolicymodel.WhereareYouTravlingTo = form["ddlWhereareyoutravelingto"];
                PurchasePolicymodel.TravelersDOB = Convert.ToDateTime(dateofbirth).ToString("MM/dd/yyyy");

                PurchasePolicymodel.SpouseName = form["txtspousename"];
                PurchasePolicymodel.SpouseDOB = form["txtspousedateofbirth"];
                PurchasePolicymodel.ChildName = form["txtChildname"];
                PurchasePolicymodel.ChildDOB = form["txtchilddateofbirth"];
                PurchasePolicymodel.TravelerEmail = form["txttraveleremail"];
                PurchasePolicymodel.TravelerFirstName = form["txtTravelerFirstname1"];
                PurchasePolicymodel.TravelerMiddleName = form["txtTravelerMiddlename"];
                PurchasePolicymodel.TravelerLastName = form["txtTravelerLastname"];
                PurchasePolicymodel.Gender = form["Gender1"];
                PurchasePolicymodel.TravelerPasspost = form["TravelerPasspost"];
                PurchasePolicymodel.DeparturefromHomeCountry = form["txtDeparturefromHomeCountry"];
                PurchasePolicymodel.DateofReturntoHome = form["txtDateofReturntoHome"];
                PurchasePolicymodel.Primaryapplicantfirstname = form["txtPrimaryapplicantfirstname"];
                PurchasePolicymodel.PrimaryapplicantLastname = form["txtPrimaryapplicantLastname"];
                PurchasePolicymodel.PrimaryApplicantAddress1 = form["txtPrimaryApplicantAddress1"];
                PurchasePolicymodel.PrimaryApplicantAddress2 = PurchasePolicymodel.PrimaryApplicantAddress2;
                PurchasePolicymodel.TravelerCity = form["txtTravelerCity"];
                PurchasePolicymodel.TravelerPostalCode = form["txtTravelerPostalCode"];
                PurchasePolicymodel.TravelerEmailConfirm = PurchasePolicymodel.TravelerEmailConfirm;
                PurchasePolicymodel.TravelerMobile = form["txtTravelerMobile"];
                PurchasePolicymodel.Beneficiayname = form["txtBeneficiayname"];
                PurchasePolicymodel.Relationship = PurchasePolicymodel.Relationship;
                PurchasePolicymodel.PolicyDocument = PurchasePolicymodel.PolicyDocument;
                PurchasePolicymodel.Totalamount = form["hdntotalPremium"];
                PurchasePolicymodel.Countryofcitizenship = form["ddlcitizenship"];
                PurchasePolicymodel.Cardname = form["ddlcardname"];
                PurchasePolicymodel.Month = form["ddlMonth"];
                PurchasePolicymodel.Expiryyear = form["ddlExpiryyear"];
                PurchasePolicymodel.Nameoncard = form["txtNameoncard"];
                PurchasePolicymodel.Cardonnumber = form["txtCardonnumber"];
                PurchasePolicymodel.Securitycode = form["txtSecuritycode"];
                PurchasePolicymodel.TravelerState = form["txtBeneficiayname"];
                PurchasePolicymodel.BillingAddress = form["txtBillingAddress"];
                PurchasePolicymodel.BillingAddress2 = form["txtBillingAddress2"];
                PurchasePolicymodel.BillingState = form["ddlBillingState"];
                PurchasePolicymodel.BillingCity = form["txtBillingCity"];
                PurchasePolicymodel.BillingZipcode = form["txtBillingZipcode"];
                var userid = PurchasePolicymanager.SaveUserdetails(PurchasePolicymodel);
                var getTripID = PurchasePolicymanager.saveDetailsIntripMaster(PurchasePolicymodel);
                int dest = 1;
                if (form["ddlWhereareyoutravelingto"] == "US")
                {
                    dest = 1;
                }
                else
                {
                    dest = 0;
                }
                var purchase = new Models.Atlas_Purchase();

                //var purchase = new Models.Welcome();
                purchase.ReferId = 9800;
                purchase.Culture = "en-US";
                purchase.ShowAuthCodes = 1;
                purchase.USDest = dest;
                purchase.CoverageBeginDt = Convert.ToDateTime(form["txtCoverageStartDate"]).ToString("MM/dd/yyyy");//"04/15/2020";
                purchase.CoverageEndDt = Convert.ToDateTime(form["txtCoverageEndDate"]).ToString("MM/dd/yyyy");//"05/16/2020";//
                                                                                                               //purchase.DestinationList = 250;
                purchase.DestinationList = new List<string>
            {
                form["ddlWhereareyoutravelingto"]
            };
                purchase.PrimaryHomeCountry = form["ISOCODE"];
                purchase.Beneficiary = "Wife";
                purchase.PhysicallyLocatedState = 0;
                purchase.PhysicallyLocatedCountry = 0;
                purchase.PolicyMax = Convert.ToInt64(form["hdnplanMaximum"]); //250000;
                purchase.Deductible = Convert.ToInt64(form["hdnplanDeductible"]); //250;
                purchase.AppName = form["hdnAppname"];//"AP";
                purchase.PrimaryFlWork = 0;
                purchase.MailOpt = "regular";
                purchase.OnlineFulFill = 1;
                purchase.ShippingCost = "0.00";
                purchase.SendToName = form["txtTravelerFirstname1"] + " " + form["txtTravelerLastname"]; //"Joe Test";
                purchase.MailAddress1 = form["txtPrimaryApplicantAddress1"];//"123 Main";
                purchase.MailAddress = "";
                purchase.MailCity = form["txtTravelerCity"];//"Indianapolis";
                purchase.MailCountry = form["ISOCODE"];
                purchase.MailState = "Jharkhand";
                purchase.MailZip = 46204;
                purchase.Phone = form["txtTravelerMobile"]; //"3172218048";
                purchase.EmailAddress = form["txttraveleremail"]; //"Test_Emails@hccmis.com";

                if (form["txtspousedateofbirth"] == "" && form["txtchild1dob"] == "" && form["txtchild2dob"] == "" && form["txtchild3dob"] == "" && form["txtchild4dob"] == "")
                {
                    purchase.ApplicantList = new List<Models.AtlasApplicantList>
                      {
                         new Models.AtlasApplicantList {
                            FirstName=form["txtTravelerFirstname1"], //"Joe",
                            MiddleInitial = form["txtTravelerMiddlename"],//"T", 
                            LastName =form["txtTravelerLastname"], //"Test",
                            Dob=Convert.ToDateTime(dateofbirth).ToString("MM/dd/yyyy"), //"01/01/1990" "01/01/1990", 
                            Gender=Gender,
                            Citizenship=form["ISOCODE"],
                            Passport=""

                            }
                      };
                }
                else if (form["txtspousedateofbirth"] != "" && form["txtchild1dob"] == "" && form["txtchild2dob"] == "" && form["txtchild3dob"] == "" && form["txtchild4dob"] == "")
                {
                    purchase.ApplicantList = new List<Models.AtlasApplicantList>
                      {
                         new Models.AtlasApplicantList {
                            FirstName=form["txtTravelerFirstname1"], //"Joe",
                            MiddleInitial = form["txtTravelerMiddlename"],//"T", 
                            LastName =form["txtTravelerLastname"], //"Test",
                            Dob=Convert.ToDateTime(dateofbirth).ToString("MM/dd/yyyy"), //"01/01/1990" "01/01/1990", 
                            Gender=Gender,
                            Citizenship=form["ISOCODE"],
                            Passport=""

                            },
                          new Models.AtlasApplicantList {
                            FirstName=form["txtSpousefirstname"], //"Joe",
                            MiddleInitial = form["txtspousemiddlename"],//"T", 
                            LastName =form["txtspouselastname"], //"Test",
                            Dob=Convert.ToDateTime(SpouseDOB).ToString("MM/dd/yyyy"), //"01/01/1990" "01/01/1990", 
                            Gender=SpouseGender,
                            Citizenship=form["ISOCODE"],
                            Passport=""

                            }
                      };
                }

                else if (form["txtspousedateofbirth"] != "" && form["txtchild1dob"] != "" && form["txtchild2dob"] == "" && form["txtchild3dob"] == "" && form["txtchild4dob"] == "")
                {
                    purchase.ApplicantList = new List<Models.AtlasApplicantList>
                      {
                         new Models.AtlasApplicantList {
                            FirstName=form["txtTravelerFirstname1"], //"Joe",
                            MiddleInitial = form["txtTravelerMiddlename"],//"T", 
                            LastName =form["txtTravelerLastname"], //"Test",
                            Dob=Convert.ToDateTime(dateofbirth).ToString("MM/dd/yyyy"), //"01/01/1990" "01/01/1990", 
                            Gender=Gender,
                            Citizenship=form["ISOCODE"],
                            Passport=""

                            },
                          new Models.AtlasApplicantList {
                            FirstName=form["txtSpousefirstname"], //"Joe",
                            MiddleInitial = form["txtspousemiddlename"],//"T", 
                            LastName =form["txtspouselastname"], //"Test",
                            Dob=Convert.ToDateTime(SpouseDOB).ToString("MM/dd/yyyy"), //"01/01/1990" "01/01/1990", 
                            Gender=SpouseGender,
                            Citizenship=form["ISOCODE"],
                            Passport=""

                            },  new Models.AtlasApplicantList {
                            FirstName=form["txtchild1firstname"], //"Joe",
                            MiddleInitial = form["txtchild1midname"],//"T", 
                            LastName =form["txtchild1lastname"], //"Test",
                            Dob=Convert.ToDateTime(Child1DOB).ToString("MM/dd/yyyy"), //"01/01/1990" "01/01/1990", 
                            Gender=child1gender,
                            Citizenship=form["ISOCODE"],
                            Passport=""

                            }
                      };
                }
                else if (form["txtspousedateofbirth"] != "" && form["txtchild1dob"] != "" && form["txtchild2dob"] != "" && form["txtchild3dob"] == "" && form["txtchild4dob"] == "")
                {
                    purchase.ApplicantList = new List<Models.AtlasApplicantList>
                      {
                         new Models.AtlasApplicantList {
                            FirstName=form["txtTravelerFirstname1"], //"Joe",
                            MiddleInitial = form["txtTravelerMiddlename"],//"T", 
                            LastName =form["txtTravelerLastname"], //"Test",
                            Dob=Convert.ToDateTime(dateofbirth).ToString("MM/dd/yyyy"), //"01/01/1990" "01/01/1990", 
                            Gender=Gender,
                            Citizenship=form["ISOCODE"],
                            Passport=""

                            },
                          new Models.AtlasApplicantList {
                            FirstName=form["txtSpousefirstname"], //"Joe",
                            MiddleInitial = form["txtspousemiddlename"],//"T", 
                            LastName =form["txtspouselastname"], //"Test",
                            Dob=Convert.ToDateTime(SpouseDOB).ToString("MM/dd/yyyy"), //"01/01/1990" "01/01/1990", 
                            Gender=SpouseGender,
                            Citizenship=form["ISOCODE"],
                            Passport=""

                            },  new Models.AtlasApplicantList {
                            FirstName=form["txtchild1firstname"], //"Joe",
                            MiddleInitial = form["txtchild1midname"],//"T", 
                            LastName =form["txtchild1lastname"], //"Test",
                            Dob=Convert.ToDateTime(Child1DOB).ToString("MM/dd/yyyy"), //"01/01/1990" "01/01/1990", 
                            Gender=child1gender,
                            Citizenship=form["ISOCODE"],
                            Passport=""

                            },
                          new Models.AtlasApplicantList {
                            FirstName=form["txtchild2firstname"], //"Joe",
                            MiddleInitial = form["txtchild2middlename"],//"T", 
                            LastName =form["txtchild2lastname"], //"Test",
                            Dob=Convert.ToDateTime(Child2DOB).ToString("MM/dd/yyyy"), //"01/01/1990" "01/01/1990", 
                            Gender=child2gender,
                            Citizenship=form["ISOCODE"],
                            Passport=""

                            }
                      };
                }
                else if (form["txtspousedateofbirth"] != "" && form["txtchild1dob"] != "" && form["txtchild2dob"] != "" && form["txtchild3dob"] != "" && form["txtchild4dob"] == "")
                {
                    purchase.ApplicantList = new List<Models.AtlasApplicantList>
                      {
                         new Models.AtlasApplicantList {
                            FirstName=form["txtTravelerFirstname1"], //"Joe",
                            MiddleInitial = form["txtTravelerMiddlename"],//"T", 
                            LastName =form["txtTravelerLastname"], //"Test",
                            Dob=Convert.ToDateTime(dateofbirth).ToString("MM/dd/yyyy"), //"01/01/1990" "01/01/1990", 
                            Gender=Gender,
                            Citizenship=form["ISOCODE"],
                            Passport=""

                            },
                          new Models.AtlasApplicantList {
                            FirstName=form["txtSpousefirstname"], //"Joe",
                            MiddleInitial = form["txtspousemiddlename"],//"T", 
                            LastName =form["txtspouselastname"], //"Test",
                            Dob=Convert.ToDateTime(SpouseDOB).ToString("MM/dd/yyyy"), //"01/01/1990" "01/01/1990", 
                            Gender=SpouseGender,
                            Citizenship=form["ISOCODE"],
                            Passport=""

                            },  new Models.AtlasApplicantList {
                            FirstName=form["txtchild1firstname"], //"Joe",
                            MiddleInitial = form["txtchild1midname"],//"T", 
                            LastName =form["txtchild1lastname"], //"Test",
                            Dob=Convert.ToDateTime(Child1DOB).ToString("MM/dd/yyyy"), //"01/01/1990" "01/01/1990", 
                            Gender=child1gender,
                            Citizenship=form["ISOCODE"],
                            Passport=""

                            },
                          new Models.AtlasApplicantList {
                            FirstName=form["txtchild2firstname"], //"Joe",
                            MiddleInitial = form["txtchild2middlename"],//"T", 
                            LastName =form["txtchild2lastname"], //"Test",
                            Dob=Convert.ToDateTime(Child2DOB).ToString("MM/dd/yyyy"), //"01/01/1990" "01/01/1990", 
                            Gender=child2gender,
                            Citizenship=form["ISOCODE"],
                            Passport=""

                            },
                          new Models.AtlasApplicantList {
                            FirstName=form["txtchild3firstname"], //"Joe",
                            MiddleInitial = form["txtchild3middlename"],//"T", 
                            LastName =form["txtchild3lastname"], //"Test",
                            Dob=Convert.ToDateTime(Child3DOB).ToString("MM/dd/yyyy"), //"01/01/1990" "01/01/1990", 
                            Gender=child2gender,
                            Citizenship=form["ISOCODE"],
                            Passport=""

                            }
                      };
                }
                else if (form["txtspousedateofbirth"] != "" && form["txtchild1dob"] != "" && form["txtchild2dob"] != "" && form["txtchild3dob"] != "" && form["txtchild4dob"] != "")
                {
                    purchase.ApplicantList = new List<Models.AtlasApplicantList>
                      {
                         new Models.AtlasApplicantList {
                            FirstName=form["txtTravelerFirstname1"], //"Joe",
                            MiddleInitial = form["txtTravelerMiddlename"],//"T", 
                            LastName =form["txtTravelerLastname"], //"Test",
                            Dob=Convert.ToDateTime(dateofbirth).ToString("MM/dd/yyyy"), //"01/01/1990" "01/01/1990", 
                            Gender=Gender,
                            Citizenship=form["ISOCODE"],
                            Passport=""

                            },
                          new Models.AtlasApplicantList {
                            FirstName=form["txtSpousefirstname"], //"Joe",
                            MiddleInitial = form["txtspousemiddlename"],//"T", 
                            LastName =form["txtspouselastname"], //"Test",
                            Dob=Convert.ToDateTime(SpouseDOB).ToString("MM/dd/yyyy"), //"01/01/1990" "01/01/1990", 
                            Gender=SpouseGender,
                            Citizenship=form["ISOCODE"],
                            Passport=""

                            },  new Models.AtlasApplicantList {
                            FirstName=form["txtchild1firstname"], //"Joe",
                            MiddleInitial = form["txtchild1midname"],//"T", 
                            LastName =form["txtchild1lastname"], //"Test",
                            Dob=Convert.ToDateTime(Child1DOB).ToString("MM/dd/yyyy"), //"01/01/1990" "01/01/1990", 
                            Gender=child1gender,
                            Citizenship=form["ISOCODE"],
                            Passport=""

                            },
                          new Models.AtlasApplicantList {
                            FirstName=form["txtchild2firstname"], //"Joe",
                            MiddleInitial = form["txtchild2middlename"],//"T", 
                            LastName =form["txtchild2lastname"], //"Test",
                            Dob=Convert.ToDateTime(Child2DOB).ToString("MM/dd/yyyy"), //"01/01/1990" "01/01/1990", 
                            Gender=child2gender,
                            Citizenship=form["ISOCODE"],
                            Passport=""

                            },
                          new Models.AtlasApplicantList {
                            FirstName=form["txtchild3firstname"], //"Joe",
                            MiddleInitial = form["txtchild3middlename"],//"T", 
                            LastName =form["txtchild3lastname"], //"Test",
                            Dob=Convert.ToDateTime(Child3DOB).ToString("MM/dd/yyyy"), //"01/01/1990" "01/01/1990", 
                            Gender=child2gender,
                            Citizenship=form["ISOCODE"],
                            Passport=""

                            }
                          ,
                          new Models.AtlasApplicantList {
                            FirstName=form["txtchild4firstname"], //"Joe",
                            MiddleInitial = form["txtchild4middlename"],//"T", 
                            LastName =form["txtchild4lastname"], //"Test",
                            Dob=Convert.ToDateTime(Child4DOB).ToString("MM/dd/yyyy"), //"01/01/1990" "01/01/1990", 
                            Gender=child2gender,
                            Citizenship=form["ISOCODE"],
                            Passport=""

                            }
                      };
                }

                else if (form["txtspousedateofbirth"] == "" && form["txtchild1dob"] != "" && form["txtchild2dob"] == "" && form["txtchild3dob"] == "" && form["txtchild4dob"] == "")
                {
                    purchase.ApplicantList = new List<Models.AtlasApplicantList>
                      {
                         new Models.AtlasApplicantList {
                            FirstName=form["txtTravelerFirstname1"], //"Joe",
                            MiddleInitial = form["txtTravelerMiddlename"],//"T", 
                            LastName =form["txtTravelerLastname"], //"Test",
                            Dob=Convert.ToDateTime(dateofbirth).ToString("MM/dd/yyyy"), //"01/01/1990" "01/01/1990", 
                            Gender=Gender,
                            Citizenship=form["ISOCODE"],
                            Passport=""

                            },
                          new Models.AtlasApplicantList {
                            FirstName=form["txtchild1firstname"], //"Joe",
                            MiddleInitial = form["txtchild1midname"],//"T", 
                            LastName =form["txtchild1lastname"], //"Test",
                            Dob=Convert.ToDateTime(Child1DOB).ToString("MM/dd/yyyy"), //"01/01/1990" "01/01/1990", 
                            Gender=child1gender,
                            Citizenship=form["ISOCODE"],
                            Passport=""

                            }
                      };
                }
                else if (form["txtspousedateofbirth"] == "" && form["txtchild1dob"] != "" && form["txtchild2dob"] != "" && form["txtchild3dob"] == "" && form["txtchild4dob"] == "")
                {
                    purchase.ApplicantList = new List<Models.AtlasApplicantList>
                      {
                         new Models.AtlasApplicantList {
                            FirstName=form["txtTravelerFirstname1"], //"Joe",
                            MiddleInitial = form["txtTravelerMiddlename"],//"T", 
                            LastName =form["txtTravelerLastname"], //"Test",
                            Dob=Convert.ToDateTime(dateofbirth).ToString("MM/dd/yyyy"), //"01/01/1990" "01/01/1990", 
                            Gender=Gender,
                            Citizenship=form["ISOCODE"],
                            Passport=""

                            },
                           new Models.AtlasApplicantList {
                            FirstName=form["txtchild1firstname"], //"Joe",
                            MiddleInitial = form["txtchild1midname"],//"T", 
                            LastName =form["txtchild1lastname"], //"Test",
                            Dob=Convert.ToDateTime(Child1DOB).ToString("MM/dd/yyyy"), //"01/01/1990" "01/01/1990", 
                            Gender=child1gender,
                            Citizenship=form["ISOCODE"],
                            Passport=""

                            },
                          new Models.AtlasApplicantList {
                            FirstName=form["txtchild2firstname"], //"Joe",
                            MiddleInitial = form["txtchild2middlename"],//"T", 
                            LastName =form["txtchild2lastname"], //"Test",
                            Dob=Convert.ToDateTime(Child2DOB).ToString("MM/dd/yyyy"), //"01/01/1990" "01/01/1990", 
                            Gender=child2gender,
                            Citizenship=form["ISOCODE"],
                            Passport=""

                            }
                      };
                }
                else if (form["txtspousedateofbirth"] == "" && form["txtchild1dob"] != "" && form["txtchild2dob"] != "" && form["txtchild3dob"] != "" && form["txtchild4dob"] == "")
                {
                    purchase.ApplicantList = new List<Models.AtlasApplicantList>
                      {
                         new Models.AtlasApplicantList {
                            FirstName=form["txtTravelerFirstname1"], //"Joe",
                            MiddleInitial = form["txtTravelerMiddlename"],//"T", 
                            LastName =form["txtTravelerLastname"], //"Test",
                            Dob=Convert.ToDateTime(dateofbirth).ToString("MM/dd/yyyy"), //"01/01/1990" "01/01/1990", 
                            Gender=Gender,
                            Citizenship=form["ISOCODE"],
                            Passport=""

                            },
                          new Models.AtlasApplicantList {
                            FirstName=form["txtchild1firstname"], //"Joe",
                            MiddleInitial = form["txtchild1midname"],//"T", 
                            LastName =form["txtchild1lastname"], //"Test",
                            Dob=Convert.ToDateTime(Child1DOB).ToString("MM/dd/yyyy"), //"01/01/1990" "01/01/1990", 
                            Gender=child1gender,
                            Citizenship=form["ISOCODE"],
                            Passport=""

                            },
                          new Models.AtlasApplicantList {
                            FirstName=form["txtchild2firstname"], //"Joe",
                            MiddleInitial = form["txtchild2middlename"],//"T", 
                            LastName =form["txtchild2lastname"], //"Test",
                            Dob=Convert.ToDateTime(Child2DOB).ToString("MM/dd/yyyy"), //"01/01/1990" "01/01/1990", 
                            Gender=child2gender,
                            Citizenship=form["ISOCODE"],
                            Passport=""

                            },
                          new Models.AtlasApplicantList {
                            FirstName=form["txtchild3firstname"], //"Joe",
                            MiddleInitial = form["txtchild3middlename"],//"T", 
                            LastName =form["txtchild3lastname"], //"Test",
                            Dob=Convert.ToDateTime(Child3DOB).ToString("MM/dd/yyyy"), //"01/01/1990" "01/01/1990", 
                            Gender=child2gender,
                            Citizenship=form["ISOCODE"],
                            Passport=""

                            }
                      };
                }
                else if (form["txtspousedateofbirth"] == "" && form["txtchild1dob"] != "" && form["txtchild2dob"] != "" && form["txtchild3dob"] != "" && form["txtchild4dob"] != "")
                {
                    purchase.ApplicantList = new List<Models.AtlasApplicantList>
                      {
                         new Models.AtlasApplicantList {
                            FirstName=form["txtTravelerFirstname1"], //"Joe",
                            MiddleInitial = form["txtTravelerMiddlename"],//"T", 
                            LastName =form["txtTravelerLastname"], //"Test",
                            Dob=Convert.ToDateTime(dateofbirth).ToString("MM/dd/yyyy"), //"01/01/1990" "01/01/1990", 
                            Gender=Gender,
                            Citizenship=form["ISOCODE"],
                            Passport=""

                            },  new Models.AtlasApplicantList {
                            FirstName=form["txtchild1firstname"], //"Joe",
                            MiddleInitial = form["txtchild1midname"],//"T", 
                            LastName =form["txtchild1lastname"], //"Test",
                            Dob=Convert.ToDateTime(Child1DOB).ToString("MM/dd/yyyy"), //"01/01/1990" "01/01/1990", 
                            Gender=child1gender,
                            Citizenship=form["ISOCODE"],
                            Passport=""

                            },
                          new Models.AtlasApplicantList {
                            FirstName=form["txtchild2firstname"], //"Joe",
                            MiddleInitial = form["txtchild2middlename"],//"T", 
                            LastName =form["txtchild2lastname"], //"Test",
                            Dob=Convert.ToDateTime(Child2DOB).ToString("MM/dd/yyyy"), //"01/01/1990" "01/01/1990", 
                            Gender=child2gender,
                            Citizenship=form["ISOCODE"],
                            Passport=""

                            },
                          new Models.AtlasApplicantList {
                            FirstName=form["txtchild3firstname"], //"Joe",
                            MiddleInitial = form["txtchild3middlename"],//"T", 
                            LastName =form["txtchild3lastname"], //"Test",
                            Dob=Convert.ToDateTime(Child3DOB).ToString("MM/dd/yyyy"), //"01/01/1990" "01/01/1990", 
                            Gender=child2gender,
                            Citizenship=form["ISOCODE"],
                            Passport=""

                            }
                          ,
                          new Models.AtlasApplicantList {
                            FirstName=form["txtchild4firstname"], //"Joe",
                            MiddleInitial = form["txtchild4middlename"],//"T", 
                            LastName =form["txtchild4lastname"], //"Test",
                            Dob=Convert.ToDateTime(Child4DOB).ToString("MM/dd/yyyy"), //"01/01/1990" "01/01/1990", 
                            Gender=child2gender,
                            Citizenship=form["ISOCODE"],
                            Passport=""

                            }
                      };
                }


                purchase.CreditCard = new List<Models.AtlasCreditCardDetails>
            {
                new Models.AtlasCreditCardDetails
                {
                    CardExpirationMonth = Convert.ToInt32(form["ddlMonth"]),
                    CardExpirationYear =Convert.ToInt32(form["ddlExpiryyear"]),
                    CardHolderAddress1 = form["txtBillingAddress"], //"123 Main",
            CardHolderAddress2 = "",
                    CardHolderCity =form["txtBillingCity"], //"Indianapolis",
                    CardHolderCountry = "",
                    CardHolderDayTimePhone = "3172218048",
                    CardHolderName = form["txtNameoncard"],
                    CardHolderState = "",
                    CardHolderZip = Convert.ToInt64(form["txtBillingZipcode"]),
                    CardNumber =  form["txtCardonnumber"], //"4111111111111111",
                    CardSecurityCode = Convert.ToInt32( form["txtSecuritycode"]),
                    CreditCardID = 0,
                    PaymentMethod =form["ddlcardname"],
                    Transaction = new List<Models.AtlasTransaction>
                    {
                        new Models.AtlasTransaction
                        {
                            Amount=Convert.ToDouble(form["hdntotalPremium"])
                        }
                    }
                }
            };


                string jsonData = JsonConvert.SerializeObject(purchase, Formatting.Indented);
                string final = ReplaceFirstOccurrence(jsonData, "\"CreditCard\": [", "\"CreditCard\": ");
                final = ReplaceFirstOccurrence(final, "\"Transaction\": [", "\"Transaction\": ");
                final = ReplaceFirstOccurrence(final, "]\r\n    }\r\n  ]", "\r\n    }\r\n ");
                string Domainname = ConfigurationManager.AppSettings["Domainname"];
                string AtlasPurchase = ConfigurationManager.AppSettings["AtlasPurchase"];
                var t = Task.Run(() => PostRequestAtlas(final, AtlasPurchase, Domainname));
                t.Wait();
                string JsonResult = t.Result + "TRIPID:" + getTripID;
                //string JsonResult = t.Result;


                ViewBag.Message = JsonResult;

                //var result = new { ViewBag.Message,getTripID };

                return Json(ViewBag.Message, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                var error = ex;
                PurchasePolicymanager.SaveErrorLog("AtlasPurchase", error.ToString());
                string res = "0";
                return Json(res, JsonRequestBehavior.AllowGet);
            }


        }


        public ActionResult IMGPurchaseDetails(FormCollection form)
        {
            try
            {
                string DOB = "";
                if (form["TravelersDOB"] == null || form["TravelersDOB"] == "")
                {
                    int age = Convert.ToInt32(form["age"]);
                    int getDOByear = Convert.ToInt32(DateTime.Now.Year) - age;
                    string getdate = DateTime.Now.Day.ToString();
                    string getmonth = DateTime.Now.Month.ToString();
                    DOB = getDOByear.ToString() + "-" + getmonth + "-" + getdate;
                }
                else
                {
                    DOB = form["TravelersDOB"];
                }
                PurchasePolicymodel.PlanId = Convert.ToInt64(form["hdnPlanId"]);
                PurchasePolicymodel.planDeductible = Convert.ToDouble(form["hdnplanDeductible"]);
                PurchasePolicymodel.planMaximum = Convert.ToDouble(form["hdnplanMaximum"]);
                PurchasePolicymodel.INsuranceComp = form["hdnINsuranceComp"];
                PurchasePolicymodel.policyName = form["hdnpolicyName"];
                PurchasePolicymodel.LeavingHome = Convert.ToDateTime(form["txtCoverageStartDate"]).ToString("MM/dd/yyyy");
                PurchasePolicymodel.TillDate = Convert.ToDateTime(form["txtCoverageEndDate"]).ToString("MM/dd/yyyy");
                PurchasePolicymodel.ISOCODE = form["ISOCODE"];
                PurchasePolicymodel.WhereareYouTravlingTo = form["ddlWhereareyoutravelingto"];
                PurchasePolicymodel.TravelersDOB = Convert.ToDateTime(DOB).ToString("MM/dd/yyyy");

                PurchasePolicymodel.SpouseName = form["txtspousename"];
                PurchasePolicymodel.SpouseDOB = form["txtspousedateofbirth"];
                PurchasePolicymodel.ChildName = form["txtChildname"];
                PurchasePolicymodel.ChildDOB = form["txtchilddateofbirth"];
                PurchasePolicymodel.TravelerEmail = form["txttraveleremail"];
                PurchasePolicymodel.TravelerFirstName = form["txtTravelerFirstname1"];
                PurchasePolicymodel.TravelerMiddleName = form["txtTravelerMiddlename"];
                PurchasePolicymodel.TravelerLastName = form["txtTravelerLastname"];
                PurchasePolicymodel.Gender = form["Gender1"];
                PurchasePolicymodel.TravelerPasspost = form["TravelerPasspost"];
                PurchasePolicymodel.DeparturefromHomeCountry = form["txtDeparturefromHomeCountry"];
                PurchasePolicymodel.DateofReturntoHome = form["txtDateofReturntoHome"];
                PurchasePolicymodel.Primaryapplicantfirstname = form["txtPrimaryapplicantfirstname"];
                PurchasePolicymodel.PrimaryapplicantLastname = form["txtPrimaryapplicantLastname"];
                PurchasePolicymodel.PrimaryApplicantAddress1 = form["txtPrimaryApplicantAddress1"];
                PurchasePolicymodel.PrimaryApplicantAddress2 = PurchasePolicymodel.PrimaryApplicantAddress2;
                PurchasePolicymodel.TravelerCity = form["txtTravelerCity"];
                PurchasePolicymodel.TravelerPostalCode = form["txtTravelerPostalCode"];
                PurchasePolicymodel.TravelerEmailConfirm = PurchasePolicymodel.TravelerEmailConfirm;
                PurchasePolicymodel.TravelerMobile = form["txtTravelerMobile"];
                PurchasePolicymodel.Beneficiayname = form["txtBeneficiayname"];
                PurchasePolicymodel.Relationship = PurchasePolicymodel.Relationship;
                PurchasePolicymodel.PolicyDocument = PurchasePolicymodel.PolicyDocument;
                PurchasePolicymodel.Totalamount = form["hdntotalPremium"];
                PurchasePolicymodel.Countryofcitizenship = form["ddlcitizenship"];
                PurchasePolicymodel.Cardname = form["ddlcardname"];
                PurchasePolicymodel.Month = form["ddlMonth"];
                PurchasePolicymodel.Expiryyear = form["ddlExpiryyear"];
                PurchasePolicymodel.Nameoncard = form["txtNameoncard"];
                PurchasePolicymodel.Cardonnumber = form["txtCardonnumber"];
                PurchasePolicymodel.Securitycode = form["txtSecuritycode"];
                PurchasePolicymodel.TravelerState = form["txtBeneficiayname"];
                PurchasePolicymodel.BillingAddress = form["txtBillingAddress"];
                PurchasePolicymodel.BillingAddress2 = form["txtBillingAddress2"];
                PurchasePolicymodel.BillingState = form["ddlBillingState"];
                PurchasePolicymodel.BillingCity = form["txtBillingCity"];
                PurchasePolicymodel.BillingZipcode = form["txtBillingZipcode"];
                var userid = PurchasePolicymanager.SaveUserdetails(PurchasePolicymodel);
                var getTripID = PurchasePolicymanager.saveDetailsIntripMaster(PurchasePolicymodel);

                string apptype = "";
                if (form["hdnAppname"] == "PATAP" || form["hdnAppname"] == "PATAI" || form["hdnAppname"] == "PATII" || form["hdnAppname"] == "PPLAI" || form["hdnAppname"] == "PPLII")
                {
                    apptype = "0521";
                }
                else if (form["hdnAppname"] == "EPSNI" || form["hdnAppname"] == "EPSUI")
                {
                    apptype = "0619";
                }
                else
                {
                    if (form["hdnplanMaximum"] == "25000" || form["hdnplanMaximum"] == "10000")
                    {
                        apptype = "0521A";

                    }
                    else if (form["hdnplanMaximum"] == "50000")
                    {
                        apptype = "0521B";

                    }
                    else
                    {
                        apptype = "0521C";
                    }
                }

                string fromdate = Convert.ToDateTime(form["txtCoverageStartDate"]).ToString("yyyy-MM-dd");
                string todate = Convert.ToDateTime(form["txtCoverageEndDate"]).ToString("yyyy-MM-dd");
                DateTime LeavingHome = DateTime.Parse(fromdate);
                DateTime TillDate = DateTime.Parse(todate);
                var DATE = (TillDate - LeavingHome).TotalDays;
                string countmonth = (form["ddlMonth"]);
                int lenth = countmonth.Length;
                string cardexpiry = "";
                if (lenth == 1)
                {
                    cardexpiry = form["ddlExpiryyear"] + "-0" + form["ddlMonth"];
                }
                else
                {
                    cardexpiry = form["ddlExpiryyear"] + "-" + form["ddlMonth"];

                }
                string IMGTouristcode = PurchasePolicymanager.GetIMGCountryCode(form["ddlWhereareyoutravelingto"]);
                string IMGISOCODE = PurchasePolicymanager.GetIMGCountryCode(form["ISOCODE"]);
                string IMGCitizenship = PurchasePolicymanager.GetIMGCountryCode(form["ddlcitizenship"]);

                if (IMGTouristcode == null || IMGTouristcode == "" || IMGISOCODE == null || IMGISOCODE == "")
                {

                }
                else
                {
                    string Domainname = ConfigurationManager.AppSettings["Domainname"];

                    HttpClient auth = new HttpClient();
                    auth.BaseAddress = new Uri(Domainname);




                    string AuthenticateIMG = ConfigurationManager.AppSettings["AuthentificateIMG"];
                    var token = Task.Run(() => GetData(AuthenticateIMG, "grant_type=password&username=touristinsured@gmail.com&password=Password1"));
                    token.Wait();
                    string tokenValue1 = token.Result;



                    var details = JObject.Parse(tokenValue1);
                    string accesstoken = details["access_token"].ToString();
                    string bearertype = details["token_type"].ToString();


                    string AuthKey = tokenValue1;
                    AuthKey = bearertype + " " + accesstoken;


                    var purchase = new Models.IMGPurchase();
                    purchase.producerNumber = "538436";
                    purchase.productCode = form["hdnAppname"];
                    purchase.appType = apptype;
                    purchase.signatureName = form["txtTravelerFirstname1"] + " " + form["txtTravelerLastname"];
                    purchase.numberOfDays = Convert.ToInt32(DATE);
                    var travelinfo = new Models.IMGTravelinfo();
                    travelinfo.startDate = Convert.ToDateTime(form["txtCoverageStartDate"]);
                    travelinfo.endDate = Convert.ToDateTime(form["txtCoverageEndDate"]);
                    travelinfo.destinations = new List<string>
            {
                IMGTouristcode
            };

                    travelinfo.isCurrentlyLocatedInFlorida = false;
                    purchase.travelInfo.Add(travelinfo);
                    var policyinfo = new Models.policyInfo();
                    policyinfo.deductible = Convert.ToInt32(form["hdnplanDeductible"]);
                    policyinfo.maximumLimit = Convert.ToInt32(form["hdnplanMaximum"]);
                    policyinfo.currencyCode = "USD";
                    policyinfo.fulfillmentMethod = "Online";
                    purchase.policyInfo.Add(policyinfo);
                    if (form["txtspousedateofbirth"] == "" && form["txtchild1dob"] == "" && form["txtchild2dob"] == "" && form["txtchild3dob"] == "" && form["txtchild4dob"] == "")
                    {
                        purchase.families = new List<Models.Imgfamilies>
                    {
                        new Models.Imgfamilies()
                {
                    insureds= new List<Models.IMGInsureds>
                    {
                        new Models.IMGInsureds()
                        {
                            firstName=form["txtTravelerFirstname1"],
                            lastName=form["txtTravelerLastname"],
                            dateOfBirth=Convert.ToDateTime(DOB),
                            citizenship=IMGCitizenship,
                            gender=form["Gender1"],
                            residence=IMGISOCODE,
                            travelerType="Primary",
                            startDate=Convert.ToDateTime(form["txtCoverageStartDate"]),
                            endDate=Convert.ToDateTime(form["txtCoverageEndDate"]),
                            email=form["txttraveleremail"]

                        }
                    }
                }
                     };
                    }
                    else if (form["txtspousedateofbirth"] != "" && form["txtchild1dob"] == "" && form["txtchild2dob"] == "" && form["txtchild3dob"] == "" && form["txtchild4dob"] == "")
                    {
                        purchase.families = new List<Models.Imgfamilies>
                    {
                        new Models.Imgfamilies()
                {
                    insureds= new List<Models.IMGInsureds>
                    {
                        new Models.IMGInsureds()
                        {
                            firstName=form["txtTravelerFirstname1"],
                            lastName=form["txtTravelerLastname"],
                            dateOfBirth=Convert.ToDateTime(DOB),
                            citizenship=IMGCitizenship,
                            gender=form["Gender1"],
                            residence=IMGISOCODE,
                            travelerType="Primary",
                            startDate=Convert.ToDateTime(form["txtCoverageStartDate"]),
                            endDate=Convert.ToDateTime(form["txtCoverageEndDate"]),
                            email=form["txttraveleremail"]

                        },
                         new Models.IMGInsureds()
                        {
                            firstName=form["txtSpousefirstname"],
                            lastName=form["txtspouselastname"],
                            dateOfBirth=Convert.ToDateTime(form["txtspousedateofbirth"]),
                            citizenship=IMGCitizenship,
                            gender=form["SpouseGender"],
                            residence=IMGISOCODE,
                            travelerType="Spouse",
                            startDate=Convert.ToDateTime(form["txtCoverageStartDate"]),
                            endDate=Convert.ToDateTime(form["txtCoverageEndDate"]),
                            email=form["txttraveleremail"]

                        }
                    }
                }
                     };
                    }

                    else if (form["txtspousedateofbirth"] != "" && form["txtchild1dob"] != "" && form["txtchild2dob"] == "" && form["txtchild3dob"] == "" && form["txtchild4dob"] == "")
                    {
                        purchase.families = new List<Models.Imgfamilies>
                    {
                        new Models.Imgfamilies()
                {
                    insureds= new List<Models.IMGInsureds>
                    {
                        new Models.IMGInsureds()
                        {
                            firstName=form["txtTravelerFirstname1"],
                            lastName=form["txtTravelerLastname"],
                            dateOfBirth=Convert.ToDateTime(DOB),
                            citizenship=IMGCitizenship,
                            gender=form["Gender1"],
                            residence=IMGISOCODE,
                            travelerType="Primary",
                            startDate=Convert.ToDateTime(form["txtCoverageStartDate"]),
                            endDate=Convert.ToDateTime(form["txtCoverageEndDate"]),
                            email=form["txttraveleremail"]

                        },
                         new Models.IMGInsureds()
                        {
                            firstName=form["txtSpousefirstname"],
                            lastName=form["txtspouselastname"],
                            dateOfBirth=Convert.ToDateTime(form["txtspousedateofbirth"]),
                            citizenship=IMGCitizenship,
                            gender=form["SpouseGender"],
                            residence=IMGISOCODE,
                            travelerType="Spouse",
                            startDate=Convert.ToDateTime(form["txtCoverageStartDate"]),
                            endDate=Convert.ToDateTime(form["txtCoverageEndDate"]),
                            email=form["txttraveleremail"]

                        },
                         new Models.IMGInsureds()
                        {
                            firstName=form["txtchild1firstname"],
                            lastName=form["txtchild1lastname"],
                            dateOfBirth=Convert.ToDateTime(form["txtchild1dob"]),
                            citizenship=IMGCitizenship,
                            gender=form["child1radio"],
                            residence=IMGISOCODE,
                            travelerType="Child",
                            startDate=Convert.ToDateTime(form["txtCoverageStartDate"]),
                            endDate=Convert.ToDateTime(form["txtCoverageEndDate"]),
                            email=form["txttraveleremail"]

                        }
                    }
                }
                     };
                    }
                    else if (form["txtspousedateofbirth"] != "" && form["txtchild1dob"] != "" && form["txtchild2dob"] != "" && form["txtchild3dob"] == "" && form["txtchild4dob"] == "")
                    {
                        purchase.families = new List<Models.Imgfamilies>
                    {
                        new Models.Imgfamilies()
                {
                    insureds= new List<Models.IMGInsureds>
                    {
                        new Models.IMGInsureds()
                        {
                            firstName=form["txtTravelerFirstname1"],
                            lastName=form["txtTravelerLastname"],
                            dateOfBirth=Convert.ToDateTime(DOB),
                            citizenship=IMGCitizenship,
                            gender=form["Gender1"],
                            residence=IMGISOCODE,
                            travelerType="Primary",
                            startDate=Convert.ToDateTime(form["txtCoverageStartDate"]),
                            endDate=Convert.ToDateTime(form["txtCoverageEndDate"]),
                            email=form["txttraveleremail"]

                        },
                         new Models.IMGInsureds()
                        {
                            firstName=form["txtSpousefirstname"],
                            lastName=form["txtspouselastname"],
                            dateOfBirth=Convert.ToDateTime(form["txtspousedateofbirth"]),
                            citizenship=IMGCitizenship,
                            gender=form["SpouseGender"],
                            residence=IMGISOCODE,
                            travelerType="Spouse",
                            startDate=Convert.ToDateTime(form["txtCoverageStartDate"]),
                            endDate=Convert.ToDateTime(form["txtCoverageEndDate"]),
                            email=form["txttraveleremail"]

                        },
                         new Models.IMGInsureds()
                        {
                            firstName=form["txtchild1firstname"],
                            lastName=form["txtchild1lastname"],
                            dateOfBirth=Convert.ToDateTime(form["txtchild1dob"]),
                            citizenship=IMGCitizenship,
                            gender=form["child1radio"],
                            residence=IMGISOCODE,
                            travelerType="Child",
                            startDate=Convert.ToDateTime(form["txtCoverageStartDate"]),
                            endDate=Convert.ToDateTime(form["txtCoverageEndDate"]),
                            email=form["txttraveleremail"]

                        }
                         ,
                         new Models.IMGInsureds()
                        {
                            firstName=form["txtchild2firstname"],
                            lastName=form["txtchild2lastname"],
                            dateOfBirth=Convert.ToDateTime(form["txtchild2dob"]),
                            citizenship=IMGCitizenship,
                            gender=form["child2radio"],
                            residence=IMGISOCODE,
                            travelerType="Child",
                            startDate=Convert.ToDateTime(form["txtCoverageStartDate"]),
                            endDate=Convert.ToDateTime(form["txtCoverageEndDate"]),
                            email=form["txttraveleremail"]

                        }
                    }
                }
                     };
                    }
                    else if (form["txtspousedateofbirth"] != "" && form["txtchild1dob"] != "" && form["txtchild2dob"] != "" && form["txtchild3dob"] != "" && form["txtchild4dob"] == "")
                    {
                        purchase.families = new List<Models.Imgfamilies>
                    {
                        new Models.Imgfamilies()
                {
                    insureds= new List<Models.IMGInsureds>
                    {
                        new Models.IMGInsureds()
                        {
                            firstName=form["txtTravelerFirstname1"],
                            lastName=form["txtTravelerLastname"],
                            dateOfBirth=Convert.ToDateTime(DOB),
                            citizenship=IMGCitizenship,
                            gender=form["Gender1"],
                            residence=IMGISOCODE,
                            travelerType="Primary",
                            startDate=Convert.ToDateTime(form["txtCoverageStartDate"]),
                            endDate=Convert.ToDateTime(form["txtCoverageEndDate"]),
                            email=form["txttraveleremail"]

                        },
                         new Models.IMGInsureds()
                        {
                            firstName=form["txtSpousefirstname"],
                            lastName=form["txtspouselastname"],
                            dateOfBirth=Convert.ToDateTime(form["txtspousedateofbirth"]),
                            citizenship=IMGCitizenship,
                            gender=form["SpouseGender"],
                            residence=IMGISOCODE,
                            travelerType="Spouse",
                            startDate=Convert.ToDateTime(form["txtCoverageStartDate"]),
                            endDate=Convert.ToDateTime(form["txtCoverageEndDate"]),
                            email=form["txttraveleremail"]

                        },
                         new Models.IMGInsureds()
                        {
                            firstName=form["txtchild1firstname"],
                            lastName=form["txtchild1lastname"],
                            dateOfBirth=Convert.ToDateTime(form["txtchild1dob"]),
                            citizenship=IMGCitizenship,
                            gender=form["child1radio"],
                            residence=IMGISOCODE,
                            travelerType="Child",
                            startDate=Convert.ToDateTime(form["txtCoverageStartDate"]),
                            endDate=Convert.ToDateTime(form["txtCoverageEndDate"]),
                            email=form["txttraveleremail"]

                        }
                         ,
                         new Models.IMGInsureds()
                        {
                            firstName=form["txtchild2firstname"],
                            lastName=form["txtchild2lastname"],
                            dateOfBirth=Convert.ToDateTime(form["txtchild2dob"]),
                            citizenship=IMGCitizenship,
                            gender=form["child2radio"],
                            residence=IMGISOCODE,
                            travelerType="Child",
                            startDate=Convert.ToDateTime(form["txtCoverageStartDate"]),
                            endDate=Convert.ToDateTime(form["txtCoverageEndDate"]),
                            email=form["txttraveleremail"]

                        } ,
                         new Models.IMGInsureds()
                        {
                            firstName=form["txtchild3firstname"],
                            lastName=form["txtchild3lastname"],
                            dateOfBirth=Convert.ToDateTime(form["txtchild3dob"]),
                            citizenship=IMGCitizenship,
                            gender=form["child3radio"],
                            residence=IMGISOCODE,
                            travelerType="Child",
                            startDate=Convert.ToDateTime(form["txtCoverageStartDate"]),
                            endDate=Convert.ToDateTime(form["txtCoverageEndDate"]),
                            email=form["txttraveleremail"]

                        }
                    }
                }
                     };
                    }
                    else if (form["txtspousedateofbirth"] != "" && form["txtchild1dob"] != "" && form["txtchild2dob"] != "" && form["txtchild3dob"] != "" && form["txtchild4dob"] != "")
                    {
                        purchase.families = new List<Models.Imgfamilies>
                    {
                        new Models.Imgfamilies()
                {
                    insureds= new List<Models.IMGInsureds>
                    {
                        new Models.IMGInsureds()
                        {
                            firstName=form["txtTravelerFirstname1"],
                            lastName=form["txtTravelerLastname"],
                            dateOfBirth=Convert.ToDateTime(DOB),
                            citizenship=IMGCitizenship,
                            gender=form["Gender1"],
                            residence=IMGISOCODE,
                            travelerType="Primary",
                            startDate=Convert.ToDateTime(form["txtCoverageStartDate"]),
                            endDate=Convert.ToDateTime(form["txtCoverageEndDate"]),
                            email=form["txttraveleremail"]

                        },
                         new Models.IMGInsureds()
                        {
                            firstName=form["txtSpousefirstname"],
                            lastName=form["txtspouselastname"],
                            dateOfBirth=Convert.ToDateTime(form["txtspousedateofbirth"]),
                            citizenship=IMGCitizenship,
                            gender=form["SpouseGender"],
                            residence=IMGISOCODE,
                            travelerType="Spouse",
                            startDate=Convert.ToDateTime(form["txtCoverageStartDate"]),
                            endDate=Convert.ToDateTime(form["txtCoverageEndDate"]),
                            email=form["txttraveleremail"]

                        },
                         new Models.IMGInsureds()
                        {
                            firstName=form["txtchild1firstname"],
                            lastName=form["txtchild1lastname"],
                            dateOfBirth=Convert.ToDateTime(form["txtchild1dob"]),
                            citizenship=IMGCitizenship,
                            gender=form["child1radio"],
                            residence=IMGISOCODE,
                            travelerType="Child",
                            startDate=Convert.ToDateTime(form["txtCoverageStartDate"]),
                            endDate=Convert.ToDateTime(form["txtCoverageEndDate"]),
                            email=form["txttraveleremail"]

                        }
                         ,
                         new Models.IMGInsureds()
                        {
                            firstName=form["txtchild2firstname"],
                            lastName=form["txtchild2lastname"],
                            dateOfBirth=Convert.ToDateTime(form["txtchild2dob"]),
                            citizenship=IMGCitizenship,
                            gender=form["child2radio"],
                            residence=IMGISOCODE,
                            travelerType="Child",
                            startDate=Convert.ToDateTime(form["txtCoverageStartDate"]),
                            endDate=Convert.ToDateTime(form["txtCoverageEndDate"]),
                            email=form["txttraveleremail"]

                        } ,
                         new Models.IMGInsureds()
                        {
                            firstName=form["txtchild3firstname"],
                            lastName=form["txtchild3lastname"],
                            dateOfBirth=Convert.ToDateTime(form["txtchild3dob"]),
                            citizenship=IMGCitizenship,
                            gender=form["child3radio"],
                            residence=IMGISOCODE,
                            travelerType="Child",
                            startDate=Convert.ToDateTime(form["txtCoverageStartDate"]),
                            endDate=Convert.ToDateTime(form["txtCoverageEndDate"]),
                            email=form["txttraveleremail"]

                        },
                         new Models.IMGInsureds()
                        {
                            firstName=form["txtchild4firstname"],
                            lastName=form["txtchild4lastname"],
                            dateOfBirth=Convert.ToDateTime(form["txtchild4dob"]),
                            citizenship=IMGCitizenship,
                            gender=form["child4radio"],
                            residence=IMGISOCODE,
                            travelerType="Child",
                            startDate=Convert.ToDateTime(form["txtCoverageStartDate"]),
                            endDate=Convert.ToDateTime(form["txtCoverageEndDate"]),
                            email=form["txttraveleremail"]

                        }
                    }
                }
                     };
                    }

                    else if (form["txtspousedateofbirth"] == "" && form["txtchild1dob"] != "" && form["txtchild2dob"] == "" && form["txtchild3dob"] == "" && form["txtchild4dob"] == "")
                    {
                        purchase.families = new List<Models.Imgfamilies>
                    {
                        new Models.Imgfamilies()
                {
                    insureds= new List<Models.IMGInsureds>
                    {
                        new Models.IMGInsureds()
                        {
                            firstName=form["txtTravelerFirstname1"],
                            lastName=form["txtTravelerLastname"],
                            dateOfBirth=Convert.ToDateTime(DOB),
                            citizenship=IMGCitizenship,
                            gender=form["Gender1"],
                            residence=IMGISOCODE,
                            travelerType="Primary",
                            startDate=Convert.ToDateTime(form["txtCoverageStartDate"]),
                            endDate=Convert.ToDateTime(form["txtCoverageEndDate"]),
                            email=form["txttraveleremail"]

                        },

                         new Models.IMGInsureds()
                        {
                            firstName=form["txtchild1firstname"],
                            lastName=form["txtchild1lastname"],
                            dateOfBirth=Convert.ToDateTime(form["txtchild1dob"]),
                            citizenship=IMGCitizenship,
                            gender=form["child1radio"],
                            residence=IMGISOCODE,
                            travelerType="Child",
                            startDate=Convert.ToDateTime(form["txtCoverageStartDate"]),
                            endDate=Convert.ToDateTime(form["txtCoverageEndDate"]),
                            email=form["txttraveleremail"]

                        }
                    }
                }
                     };
                    }
                    else if (form["txtspousedateofbirth"] == "" && form["txtchild1dob"] != "" && form["txtchild2dob"] != "" && form["txtchild3dob"] == "" && form["txtchild4dob"] == "")
                    {
                        purchase.families = new List<Models.Imgfamilies>
                    {
                        new Models.Imgfamilies()
                {
                    insureds= new List<Models.IMGInsureds>
                    {
                        new Models.IMGInsureds()
                        {
                            firstName=form["txtTravelerFirstname1"],
                            lastName=form["txtTravelerLastname"],
                            dateOfBirth=Convert.ToDateTime(DOB),
                            citizenship=IMGCitizenship,
                            gender=form["Gender1"],
                            residence=IMGISOCODE,
                            travelerType="Primary",
                            startDate=Convert.ToDateTime(form["txtCoverageStartDate"]),
                            endDate=Convert.ToDateTime(form["txtCoverageEndDate"]),
                            email=form["txttraveleremail"]

                        },

                         new Models.IMGInsureds()
                        {
                            firstName=form["txtchild1firstname"],
                            lastName=form["txtchild1lastname"],
                            dateOfBirth=Convert.ToDateTime(form["txtchild1dob"]),
                            citizenship=IMGCitizenship,
                            gender=form["child1radio"],
                            residence=IMGISOCODE,
                            travelerType="Child",
                            startDate=Convert.ToDateTime(form["txtCoverageStartDate"]),
                            endDate=Convert.ToDateTime(form["txtCoverageEndDate"]),
                            email=form["txttraveleremail"]

                        }
                         ,
                         new Models.IMGInsureds()
                        {
                            firstName=form["txtchild2firstname"],
                            lastName=form["txtchild2lastname"],
                            dateOfBirth=Convert.ToDateTime(form["txtchild2dob"]),
                            citizenship=IMGCitizenship,
                            gender=form["child2radio"],
                            residence=IMGISOCODE,
                            travelerType="Child",
                            startDate=Convert.ToDateTime(form["txtCoverageStartDate"]),
                            endDate=Convert.ToDateTime(form["txtCoverageEndDate"]),
                            email=form["txttraveleremail"]

                        }
                    }
                }
                     };
                    }
                    else if (form["txtspousedateofbirth"] == "" && form["txtchild1dob"] != "" && form["txtchild2dob"] != "" && form["txtchild3dob"] != "" && form["txtchild4dob"] == "")
                    {
                        purchase.families = new List<Models.Imgfamilies>
                    {
                        new Models.Imgfamilies()
                {
                    insureds= new List<Models.IMGInsureds>
                    {
                        new Models.IMGInsureds()
                        {
                            firstName=form["txtTravelerFirstname1"],
                            lastName=form["txtTravelerLastname"],
                            dateOfBirth=Convert.ToDateTime(DOB),
                            citizenship=IMGCitizenship,
                            gender=form["Gender1"],
                            residence=IMGISOCODE,
                            travelerType="Primary",
                            startDate=Convert.ToDateTime(form["txtCoverageStartDate"]),
                            endDate=Convert.ToDateTime(form["txtCoverageEndDate"]),
                            email=form["txttraveleremail"]

                        },

                         new Models.IMGInsureds()
                        {
                            firstName=form["txtchild1firstname"],
                            lastName=form["txtchild1lastname"],
                            dateOfBirth=Convert.ToDateTime(form["txtchild1dob"]),
                            citizenship=IMGCitizenship,
                            gender=form["child1radio"],
                            residence=IMGISOCODE,
                            travelerType="Child",
                            startDate=Convert.ToDateTime(form["txtCoverageStartDate"]),
                            endDate=Convert.ToDateTime(form["txtCoverageEndDate"]),
                            email=form["txttraveleremail"]

                        }
                         ,
                         new Models.IMGInsureds()
                        {
                            firstName=form["txtchild2firstname"],
                            lastName=form["txtchild2lastname"],
                            dateOfBirth=Convert.ToDateTime(form["txtchild2dob"]),
                            citizenship=IMGCitizenship,
                            gender=form["child2radio"],
                            residence=IMGISOCODE,
                            travelerType="Child",
                            startDate=Convert.ToDateTime(form["txtCoverageStartDate"]),
                            endDate=Convert.ToDateTime(form["txtCoverageEndDate"]),
                            email=form["txttraveleremail"]

                        } ,
                         new Models.IMGInsureds()
                        {
                            firstName=form["txtchild3firstname"],
                            lastName=form["txtchild3lastname"],
                            dateOfBirth=Convert.ToDateTime(form["txtchild3dob"]),
                            citizenship=IMGCitizenship,
                            gender=form["child3radio"],
                            residence=IMGISOCODE,
                            travelerType="Child",
                            startDate=Convert.ToDateTime(form["txtCoverageStartDate"]),
                            endDate=Convert.ToDateTime(form["txtCoverageEndDate"]),
                            email=form["txttraveleremail"]

                        }
                    }
                }
                     };
                    }
                    else if (form["txtspousedateofbirth"] == "" && form["txtchild1dob"] != "" && form["txtchild2dob"] != "" && form["txtchild3dob"] != "" && form["txtchild4dob"] != "")
                    {
                        purchase.families = new List<Models.Imgfamilies>
                    {
                        new Models.Imgfamilies()
                {
                    insureds= new List<Models.IMGInsureds>
                    {
                        new Models.IMGInsureds()
                        {
                            firstName=form["txtTravelerFirstname1"],
                            lastName=form["txtTravelerLastname"],
                            dateOfBirth=Convert.ToDateTime(DOB),
                            citizenship=IMGCitizenship,
                            gender=form["Gender1"],
                            residence=IMGISOCODE,
                            travelerType="Primary",
                            startDate=Convert.ToDateTime(form["txtCoverageStartDate"]),
                            endDate=Convert.ToDateTime(form["txtCoverageEndDate"]),
                            email=form["txttraveleremail"]

                        },

                         new Models.IMGInsureds()
                        {
                            firstName=form["txtchild1firstname"],
                            lastName=form["txtchild1lastname"],
                            dateOfBirth=Convert.ToDateTime(form["txtchild1dob"]),
                            citizenship=IMGCitizenship,
                            gender=form["child1radio"],
                            residence=IMGISOCODE,
                            travelerType="Child",
                            startDate=Convert.ToDateTime(form["txtCoverageStartDate"]),
                            endDate=Convert.ToDateTime(form["txtCoverageEndDate"]),
                            email=form["txttraveleremail"]

                        }
                         ,
                         new Models.IMGInsureds()
                        {
                            firstName=form["txtchild2firstname"],
                            lastName=form["txtchild2lastname"],
                            dateOfBirth=Convert.ToDateTime(form["txtchild2dob"]),
                            citizenship=IMGCitizenship,
                            gender=form["child2radio"],
                            residence=IMGISOCODE,
                            travelerType="Child",
                            startDate=Convert.ToDateTime(form["txtCoverageStartDate"]),
                            endDate=Convert.ToDateTime(form["txtCoverageEndDate"]),
                            email=form["txttraveleremail"]

                        } ,
                         new Models.IMGInsureds()
                        {
                            firstName=form["txtchild3firstname"],
                            lastName=form["txtchild3lastname"],
                            dateOfBirth=Convert.ToDateTime(form["txtchild3dob"]),
                            citizenship=IMGCitizenship,
                            gender=form["child3radio"],
                            residence=IMGISOCODE,
                            travelerType="Child",
                            startDate=Convert.ToDateTime(form["txtCoverageStartDate"]),
                            endDate=Convert.ToDateTime(form["txtCoverageEndDate"]),
                            email=form["txttraveleremail"]

                        },
                         new Models.IMGInsureds()
                        {
                            firstName=form["txtchild4firstname"],
                            lastName=form["txtchild4lastname"],
                            dateOfBirth=Convert.ToDateTime(form["txtchild4dob"]),
                            citizenship=IMGCitizenship,
                            gender=form["child4radio"],
                            residence=IMGISOCODE,
                            travelerType="Child",
                            startDate=Convert.ToDateTime(form["txtCoverageStartDate"]),
                            endDate=Convert.ToDateTime(form["txtCoverageEndDate"]),
                            email=form["txttraveleremail"]

                        }
                    }
                }
                     };
                    }


                    if (IMGTouristcode == "USA")
                    {
                        purchase.contacts = new List<Models.IMGcontacts>
            {
                new Models.IMGcontacts()
                {
                    contactInfoType="Residence",
                    careOfName="",
                    address=form["txtPrimaryApplicantAddress1"],
                    address2="",
                    city=form["txtTravelerCity"],

                    postalCode=form["txtTravelerPostalCode"],
                    country=IMGISOCODE,
                    phone=form["txtTravelerMobile"],
                    fax="",
                    email=form["txttraveleremail"]

                },
                  new Models.IMGcontacts()
                {
                    contactInfoType="Billing",

                    address=form["txtPrimaryApplicantAddress1"],
                    address2="",
                    city=form["txtTravelerCity"],

                    postalCode=form["txtTravelerPostalCode"],
                    country=IMGISOCODE,
                    phone=form["txtTravelerMobile"],

                    email=form["txttraveleremail"]

                }
            };
                    }
                    else
                    {
                        purchase.contacts = new List<Models.IMGcontacts>
            {
                new Models.IMGcontacts()
                {
                    contactInfoType="Residence",
                    careOfName="",
                    address=form["txtPrimaryApplicantAddress1"],
                    address2="",
                    city=form["txtTravelerCity"],
                    stateProvince="IN",
                    postalCode=form["txtTravelerPostalCode"],
                    country=IMGISOCODE,
                    phone=form["txtTravelerMobile"],
                    fax="",
                    email=form["txttraveleremail"]

                },
                  new Models.IMGcontacts()
                {
                    contactInfoType="Billing",

                    address=form["txtPrimaryApplicantAddress1"],
                    address2="",
                    city=form["txtTravelerCity"],
                     stateProvince="IN",
                    postalCode=form["txtTravelerPostalCode"],
                    country=IMGISOCODE,
                    phone=form["txtTravelerMobile"],

                    email=form["txttraveleremail"]

                }
            };
                    }

                    var paymentInfo = new Models.paymentInfo();
                    paymentInfo.paymentType = "CreditCard";

                    paymentInfo.nameOnAccount = form["txtNameoncard"];

                    paymentInfo.creditCardNumber = form["txtCardonnumber"];
                    paymentInfo.cardExpire = cardexpiry;
                    paymentInfo.cardCVV = form["txtSecuritycode"];
                    paymentInfo.requestedTotal = Convert.ToDouble(form["hdntotalPremium"]);
                    purchase.paymentInfo.Add(paymentInfo);


                    string jsonData = JsonConvert.SerializeObject(purchase, Formatting.Indented);
                    string final = ReplaceFirstOccurrence(jsonData, "\"travelInfo\": [", "\"travelInfo\": ");
                    final = ReplaceFirstOccurrence(final, "\"policyInfo\": [", "\"policyInfo\": ");
                    final = ReplaceFirstOccurrence(final, "\r\n    }\r\n  ]", "\r\n    }\r\n ");
                    final = ReplaceFirstOccurrence(final, "\r\n    }\r\n  ]", "\r\n    }\r\n ");
                    final = ReplaceFirstOccurrence(final, "\"paymentInfo\": [", "\"paymentInfo\": ");
                    final = ReplaceFirstOccurrence(final, "\r\n    }\r\n  ],\r\n  \"beneficiaries\"", "\r\n    }\r\n,\r\n  \"beneficiaries\" ");
                    //final = ReplaceFirstOccurrence(final, "\r\n    }\r\n  ]", "\r\n    }\r\n ");
                    var IMGGetquotes = ConfigurationManager.AppSettings["IMGPurchase"];
                    var t = Task.Run(() => PostRequest(final, IMGGetquotes, AuthKey, Domainname));
                    t.Wait();
                    string JsonResult = t.Result + "TRIPID:" + getTripID;
                    ViewBag.Message = JsonResult;
                }


                return Json(ViewBag.Message, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var error = ex;
                PurchasePolicymanager.SaveErrorLog("IMGPurchase", error.ToString());
                string res = "0";
                return Json(res, JsonRequestBehavior.AllowGet);
            }
        }




        public ActionResult VisitorPurchaseDetails(FormCollection form)
        {
            try
            {



                string Plan = string.Empty;
                string policymax = string.Empty;
                string policyname = "Visitor Secure";
                string Planname = string.Empty;
                if (form["hdnplanMaximum"] == "50000")
                {
                    Plan = "50";
                    Planname = "A";
                }
                else if (form["hdnplanMaximum"] == "75000")
                {
                    Plan = "75";
                    Planname = "B";
                }
                else if (form["hdnplanMaximum"] == "100000")
                {
                    Plan = "100";
                    Planname = "C";
                }
                else if (form["hdnplanMaximum"] == "130000")
                {
                    Plan = "130";
                    Planname = "D";
                }
                string dateofbirth = "";
                if (form["TravelersDOB"] == null || form["TravelersDOB"] == "")
                {
                    int ageoftraveler = Convert.ToInt32(form["age"]);
                    int getDOByear = Convert.ToInt32(DateTime.Now.Year) - ageoftraveler;
                    string getdate = DateTime.Now.Day.ToString();
                    string getmonth = DateTime.Now.Month.ToString();

                    dateofbirth = getmonth + "/" + getdate + "/" + getDOByear.ToString();
                }
                else
                {
                    dateofbirth = Convert.ToDateTime(form["TravelersDOB"]).ToString("MM/dd/yyyy");
                }

                string SpouseDOB = string.Empty;
                if (form["txtspousedateofbirth"] != "")
                {
                    SpouseDOB = Convert.ToDateTime(form["txtspousedateofbirth"]).ToString("MM/dd/yyyy");

                }
                string Child1DOB = form["txtchild1dob"];
                if (Child1DOB != "")
                {
                    Child1DOB = Convert.ToDateTime(form["txtchild1dob"]).ToString("MM/dd/yyyy");
                }
                string Child2DOB = form["txtchild2dob"];
                if (Child2DOB != "")
                {
                    Child2DOB = Convert.ToDateTime(form["txtchild2dob"]).ToString("MM/dd/yyyy");
                }
                string Child3DOB = form["txtchild3dob"];
                if (Child3DOB != "")
                {
                    Child3DOB = Convert.ToDateTime(form["txtchild3dob"]).ToString("MM/dd/yyyy");
                }
                string Child4DOB = form["txtchild4dob"];
                if (Child4DOB != "")
                {
                    Child4DOB = Convert.ToDateTime(form["txtchild4dob"]).ToString("MM/dd/yyyy");
                }
                string Gender = "";
                if (form["Gender"] == "MALE")
                {
                    Gender = "M";
                }
                else
                {
                    Gender = "F";
                }
                string SpouseGender = "";
                if (form["SpouseGender"] == "MALE")
                {
                    SpouseGender = "M";
                }
                else
                {
                    SpouseGender = "F";
                }
                string child1gender = "";
                if (form["child1radio"] == "MALE")
                {
                    child1gender = "M";
                }
                else
                {
                    child1gender = "F";
                }
                string child2gender = "";
                if (form["child2radio"] == "MALE")
                {
                    child2gender = "M";
                }
                else
                {
                    child2gender = "F";
                }
                string child3gender = "";
                if (form["child3radio"] == "MALE")
                {
                    child3gender = "M";
                }
                else
                {
                    child3gender = "F";
                }
                string child4gender = "";
                if (form["child4radio"] == "MALE")
                {
                    child4gender = "M";
                }
                else
                {
                    child4gender = "F";
                }


                PurchasePolicymodel.PlanId = Convert.ToInt64(form["hdnPlanId"]);
                PurchasePolicymodel.planDeductible = Convert.ToDouble(form["hdnplanDeductible"]);
                PurchasePolicymodel.planMaximum = Convert.ToDouble(form["hdnplanMaximum"]);
                PurchasePolicymodel.INsuranceComp = form["hdnINsuranceComp"];
                PurchasePolicymodel.policyName = form["hdnpolicyName"];
                PurchasePolicymodel.LeavingHome = Convert.ToDateTime(form["txtCoverageStartDate"]).ToString("MM/dd/yyyy");
                PurchasePolicymodel.TillDate = Convert.ToDateTime(form["txtCoverageEndDate"]).ToString("MM/dd/yyyy");
                PurchasePolicymodel.ISOCODE = form["ISOCODE"];
                PurchasePolicymodel.WhereareYouTravlingTo = form["ddlWhereareyoutravelingto"];
                PurchasePolicymodel.TravelersDOB = Convert.ToDateTime(dateofbirth).ToString("MM/dd/yyyy");

                PurchasePolicymodel.SpouseName = form["txtspousename"];
                PurchasePolicymodel.SpouseDOB = form["txtspousedateofbirth"];
                PurchasePolicymodel.ChildName = form["txtChildname"];
                PurchasePolicymodel.ChildDOB = form["txtchilddateofbirth"];
                PurchasePolicymodel.TravelerEmail = form["txttraveleremail"];
                PurchasePolicymodel.TravelerFirstName = form["txtTravelerFirstname1"];
                PurchasePolicymodel.TravelerMiddleName = form["txtTravelerMiddlename"];
                PurchasePolicymodel.TravelerLastName = form["txtTravelerLastname"];
                PurchasePolicymodel.Gender = form["Gender1"];
                PurchasePolicymodel.TravelerPasspost = form["TravelerPasspost"];
                PurchasePolicymodel.DeparturefromHomeCountry = form["txtDeparturefromHomeCountry"];
                PurchasePolicymodel.DateofReturntoHome = form["txtDateofReturntoHome"];
                PurchasePolicymodel.Primaryapplicantfirstname = form["txtPrimaryapplicantfirstname"];
                PurchasePolicymodel.PrimaryapplicantLastname = form["txtPrimaryapplicantLastname"];
                PurchasePolicymodel.PrimaryApplicantAddress1 = form["txtPrimaryApplicantAddress1"];
                PurchasePolicymodel.PrimaryApplicantAddress2 = PurchasePolicymodel.PrimaryApplicantAddress2;
                PurchasePolicymodel.TravelerCity = form["txtTravelerCity"];
                PurchasePolicymodel.TravelerPostalCode = form["txtTravelerPostalCode"];
                PurchasePolicymodel.TravelerEmailConfirm = PurchasePolicymodel.TravelerEmailConfirm;
                PurchasePolicymodel.TravelerMobile = form["txtTravelerMobile"];
                PurchasePolicymodel.Beneficiayname = form["txtBeneficiayname"];
                PurchasePolicymodel.Relationship = PurchasePolicymodel.Relationship;
                PurchasePolicymodel.PolicyDocument = PurchasePolicymodel.PolicyDocument;
                PurchasePolicymodel.Totalamount = form["hdntotalPremium"];
                PurchasePolicymodel.Countryofcitizenship = form["ddlcitizenship"];
                PurchasePolicymodel.Cardname = form["ddlcardname"];
                PurchasePolicymodel.Month = form["ddlMonth"];
                PurchasePolicymodel.Expiryyear = form["ddlExpiryyear"];
                PurchasePolicymodel.Nameoncard = form["txtNameoncard"];
                PurchasePolicymodel.Cardonnumber = form["txtCardonnumber"];
                PurchasePolicymodel.Securitycode = form["txtSecuritycode"];
                PurchasePolicymodel.TravelerState = form["txtBeneficiayname"];
                PurchasePolicymodel.BillingAddress = form["txtBillingAddress"];
                PurchasePolicymodel.BillingAddress2 = form["txtBillingAddress2"];
                PurchasePolicymodel.BillingState = form["ddlBillingState"];
                PurchasePolicymodel.BillingCity = form["txtBillingCity"];
                PurchasePolicymodel.BillingZipcode = form["txtBillingZipcode"];
                var userid = PurchasePolicymanager.SaveUserdetails(PurchasePolicymodel);
                var getTripID = PurchasePolicymanager.saveDetailsIntripMaster(PurchasePolicymodel);
                int dest = 1;
                if (form["ddlWhereareyoutravelingto"] == "US")
                {
                    dest = 1;
                }
                else
                {
                    dest = 0;
                }
                DateTime date = DateTime.Today;
                var purchase = new Models.HccPurchase();

                //var purchase = new Models.Welcome();
                purchase.ReferId = "9800";

                purchase.USDest = dest.ToString();
                purchase.CoverageBeginDt = Convert.ToDateTime(form["txtCoverageStartDate"]).ToString("MM/dd/yyyy");//"04/15/2020";//"04/15/2020";
                purchase.CoverageEndDt = Convert.ToDateTime(form["txtCoverageEndDate"]).ToString("MM/dd/yyyy");//"05/16/2020";//"05/16/2020";//
                purchase.ArriveInUSDt = Convert.ToDateTime(form["txtCoverageStartDate"]).ToString("MM/dd/yyyy");
                purchase.PrimaryHome = "India";
                purchase.PrimaryDestination = "US";


                purchase.PrimaryBeneficiary = "Wife";
                purchase.PhysicallyLocatedState = "0";
                purchase.PhysicallyLocatedCountry = "0";
                purchase.Plan = Plan; //250000;
                purchase.PlanName = Planname;
                purchase.Deductible = form["hdnplanDeductible"]; //250;

                purchase.PrimaryFlWork = "0";
                purchase.MailOption = "";
                purchase.OnlineFulfill = "1";
                purchase.ShippingCost = "0";
                //"Joe Test";
                purchase.PrimaryAddr1 = form["txtPrimaryApplicantAddress1"];//"123 Main";
                purchase.PrimaryAddr2 = "";
                purchase.PrimaryCity = form["txtTravelerCity"];//"Indianapolis";
                purchase.PrimaryCountry = form["ISOCODE"];
                purchase.PrimaryState = "Jharkhand";
                purchase.PrimaryZip = form["txtTravelerPostalCode"];
                purchase.PrimaryHomePhone = form["txtTravelerMobile"]; //"3172218048";
                purchase.PrimaryEmailAddr = form["txttraveleremail"]; //"Test_Emails@hccmis.com";
                purchase.PrimaryCitizenship = form["ISOCODE"];
                purchase.PrimaryDob = Convert.ToDateTime(dateofbirth).ToString("MM/dd/yyyy");
                purchase.PrimaryGender = Gender;
                purchase.PrimaryNameFirst = form["txtTravelerFirstname1"];
                purchase.PrimaryNameLast = form["txtTravelerLastname"];
                purchase.PrimaryNameMiddle = form["txtTravelerMiddlename"];

                if (SpouseDOB != "")
                {
                    purchase.SpouseCitizenship = form["ISOCODE"];
                    purchase.SpouseDob = Convert.ToDateTime(SpouseDOB).ToString("MM/dd/yyyy");
                    purchase.SpouseGender = SpouseGender;
                    purchase.SpouseNameFirst = form["txtSpousefirstname"];
                    purchase.SpouseNameLast = form["txtTravelerLastname"];
                    purchase.SpouseNameMiddle = form["txtTravelerMiddlename"];
                }
                else
                {
                    purchase.SpouseCitizenship = "";
                    purchase.SpouseDob = "";
                    purchase.SpouseGender = "";
                    purchase.SpouseNameFirst = "";
                    purchase.SpouseNameLast = "";
                    purchase.SpouseNameMiddle = "";
                }




                purchase.QuoteTotal = form["hdntotalPremium"];
                purchase.SendToName = "";






                if (form["txtspousedateofbirth"] != "" && form["txtchild1dob"] != "" && form["txtchild2dob"] == "" && form["txtchild3dob"] == "" && form["txtchild4dob"] == "")
                {
                    purchase.DependentList = new List<Models.DependentList>
            {
                new Models.DependentList
                {
                    NameFirst=form["txtchild1firstname"],
                    NameMiddle=form["txtchild1midname"],
                    NameLast=form["txtchild1lastname"],
                    Dob=Convert.ToDateTime(Child1DOB).ToString("MM/dd/yyyy"),
                    Gender=child1gender,
                    Citzenship=form["ISOCODE"]
                }
            };

                }

                else if (form["txtspousedateofbirth"] != "" && form["txtchild1dob"] != "" && form["txtchild2dob"] != "" && form["txtchild3dob"] == "" && form["txtchild4dob"] == "")
                {
                    purchase.DependentList = new List<Models.DependentList>
            {
                new Models.DependentList
                {
                    NameFirst=form["txtchild1firstname"],
                    NameMiddle=form["txtchild1midname"],
                    NameLast=form["txtchild1lastname"],
                    Dob=Convert.ToDateTime(Child1DOB).ToString("MM/dd/yyyy"),
                    Gender=child1gender,
                    Citzenship=form["ISOCODE"]
                },
                new Models.DependentList
                {
                    NameFirst=form["txtchild2firstname"],
                    NameMiddle=form["txtchild2middlename"],
                    NameLast=form["txtchild2lastname"],
                    Dob=Convert.ToDateTime(Child2DOB).ToString("MM/dd/yyyy"),
                    Gender=child2gender,
                    Citzenship=form["ISOCODE"]
                }
            };

                }
                else if (form["txtspousedateofbirth"] != "" && form["txtchild1dob"] != "" && form["txtchild2dob"] != "" && form["txtchild3dob"] != "" && form["txtchild4dob"] == "")
                {
                    purchase.DependentList = new List<Models.DependentList>
            {
                new Models.DependentList
                {
                    NameFirst=form["txtchild1firstname"],
                    NameMiddle=form["txtchild1midname"],
                    NameLast=form["txtchild1lastname"],
                    Dob=Convert.ToDateTime(Child1DOB).ToString("MM/dd/yyyy"),
                    Gender=child1gender,
                    Citzenship=form["ISOCODE"]
                },
                new Models.DependentList
                {
                    NameFirst=form["txtchild2firstname"],
                    NameMiddle=form["txtchild2middlename"],
                    NameLast=form["txtchild2lastname"],
                    Dob=Convert.ToDateTime(Child2DOB).ToString("MM/dd/yyyy"),
                    Gender=child2gender,
                    Citzenship=form["ISOCODE"]
                }
                ,
                new Models.DependentList
                {
                    NameFirst=form["txtchild3firstname"],
                    NameMiddle=form["txtchild3middlename"],
                    NameLast=form["txtchild3lastname"],
                    Dob=Convert.ToDateTime(Child3DOB).ToString("MM/dd/yyyy"),
                    Gender=child3gender,
                    Citzenship=form["ISOCODE"]
                }
            };

                }
                else if (form["txtspousedateofbirth"] != "" && form["txtchild1dob"] != "" && form["txtchild2dob"] != "" && form["txtchild3dob"] != "" && form["txtchild4dob"] != "")
                {
                    purchase.DependentList = new List<Models.DependentList>
            {
                new Models.DependentList
                {
                    NameFirst=form["txtchild1firstname"],
                    NameMiddle=form["txtchild1midname"],
                    NameLast=form["txtchild1lastname"],
                    Dob=Convert.ToDateTime(Child1DOB).ToString("MM/dd/yyyy"),
                    Gender=child1gender,
                    Citzenship=form["ISOCODE"]
                },
                new Models.DependentList
                {
                    NameFirst=form["txtchild2firstname"],
                    NameMiddle=form["txtchild2middlename"],
                    NameLast=form["txtchild2lastname"],
                    Dob=Convert.ToDateTime(Child2DOB).ToString("MM/dd/yyyy"),
                    Gender=child2gender,
                    Citzenship=form["ISOCODE"]
                }
                ,
                new Models.DependentList
                {
                    NameFirst=form["txtchild3firstname"],
                    NameMiddle=form["txtchild3middlename"],
                    NameLast=form["txtchild3lastname"],
                    Dob=Convert.ToDateTime(Child3DOB).ToString("MM/dd/yyyy"),
                    Gender=child3gender,
                    Citzenship=form["ISOCODE"]
                },
                new Models.DependentList
                {
                    NameFirst=form["txtchild4firstname"],
                    NameMiddle=form["txtchild4middlename"],
                    NameLast=form["txtchild4lastname"],
                    Dob=Convert.ToDateTime(Child4DOB).ToString("MM/dd/yyyy"),
                    Gender=child3gender,
                    Citzenship=form["ISOCODE"]
                }
            };

                }

                else if (form["txtspousedateofbirth"] == "" && form["txtchild1dob"] != "" && form["txtchild2dob"] == "" && form["txtchild3dob"] == "" && form["txtchild4dob"] == "")
                {
                    purchase.DependentList = new List<Models.DependentList>
            {
                new Models.DependentList
                {
                    NameFirst=form["txtchild1firstname"],
                    NameMiddle=form["txtchild1midname"],
                    NameLast=form["txtchild1lastname"],
                    Dob=Convert.ToDateTime(Child1DOB).ToString("MM/dd/yyyy"),
                    Gender=child1gender,
                    Citzenship=form["ISOCODE"]
                }
            };

                }

                else if (form["txtspousedateofbirth"] == "" && form["txtchild1dob"] != "" && form["txtchild2dob"] != "" && form["txtchild3dob"] == "" && form["txtchild4dob"] == "")
                {
                    purchase.DependentList = new List<Models.DependentList>
            {
                new Models.DependentList
                {
                    NameFirst=form["txtchild1firstname"],
                    NameMiddle=form["txtchild1midname"],
                    NameLast=form["txtchild1lastname"],
                    Dob=Convert.ToDateTime(Child1DOB).ToString("MM/dd/yyyy"),
                    Gender=child1gender,
                    Citzenship=form["ISOCODE"]
                },
                new Models.DependentList
                {
                    NameFirst=form["txtchild2firstname"],
                    NameMiddle=form["txtchild2middlename"],
                    NameLast=form["txtchild2lastname"],
                    Dob=Convert.ToDateTime(Child2DOB).ToString("MM/dd/yyyy"),
                    Gender=child2gender,
                    Citzenship=form["ISOCODE"]
                }
            };

                }
                else if (form["txtspousedateofbirth"] == "" && form["txtchild1dob"] != "" && form["txtchild2dob"] != "" && form["txtchild3dob"] != "" && form["txtchild4dob"] == "")
                {
                    purchase.DependentList = new List<Models.DependentList>
            {
                new Models.DependentList
                {
                    NameFirst=form["txtchild1firstname"],
                    NameMiddle=form["txtchild1midname"],
                    NameLast=form["txtchild1lastname"],
                    Dob=Convert.ToDateTime(Child1DOB).ToString("MM/dd/yyyy"),
                    Gender=child1gender,
                    Citzenship=form["ISOCODE"]
                },
                new Models.DependentList
                {
                    NameFirst=form["txtchild2firstname"],
                    NameMiddle=form["txtchild2middlename"],
                    NameLast=form["txtchild2lastname"],
                    Dob=Convert.ToDateTime(Child2DOB).ToString("MM/dd/yyyy"),
                    Gender=child2gender,
                    Citzenship=form["ISOCODE"]
                }
                ,
                new Models.DependentList
                {
                    NameFirst=form["txtchild3firstname"],
                    NameMiddle=form["txtchild3middlename"],
                    NameLast=form["txtchild3lastname"],
                    Dob=Convert.ToDateTime(Child3DOB).ToString("MM/dd/yyyy"),
                    Gender=child3gender,
                    Citzenship=form["ISOCODE"]
                }
            };

                }
                else if (form["txtspousedateofbirth"] == "" && form["txtchild1dob"] != "" && form["txtchild2dob"] != "" && form["txtchild3dob"] != "" && form["txtchild4dob"] != "")
                {
                    purchase.DependentList = new List<Models.DependentList>
            {
                new Models.DependentList
                {
                    NameFirst=form["txtchild1firstname"],
                    NameMiddle=form["txtchild1midname"],
                    NameLast=form["txtchild1lastname"],
                    Dob=Convert.ToDateTime(Child1DOB).ToString("MM/dd/yyyy"),
                    Gender=child1gender,
                    Citzenship=form["ISOCODE"]
                },
                new Models.DependentList
                {
                    NameFirst=form["txtchild2firstname"],
                    NameMiddle=form["txtchild2middlename"],
                    NameLast=form["txtchild2lastname"],
                    Dob=Convert.ToDateTime(Child2DOB).ToString("MM/dd/yyyy"),
                    Gender=child2gender,
                    Citzenship=form["ISOCODE"]
                }
                ,
                new Models.DependentList
                {
                    NameFirst=form["txtchild3firstname"],
                    NameMiddle=form["txtchild3middlename"],
                    NameLast=form["txtchild3lastname"],
                    Dob=Convert.ToDateTime(Child3DOB).ToString("MM/dd/yyyy"),
                    Gender=child3gender,
                    Citzenship=form["ISOCODE"]
                },
                new Models.DependentList
                {
                    NameFirst=form["txtchild4firstname"],
                    NameMiddle=form["txtchild4middlename"],
                    NameLast=form["txtchild4lastname"],
                    Dob=Convert.ToDateTime(Child4DOB).ToString("MM/dd/yyyy"),
                    Gender=child3gender,
                    Citzenship=form["ISOCODE"]
                }
            };

                }


                else
                {
                    purchase.DependentList = new List<Models.DependentList>
                    {

                    };
                }
                var credit = new Models.CreditCard();
                credit.CardExpirationMonth = Convert.ToInt32(form["ddlMonth"]);
                credit.CardExpirationYear = Convert.ToInt32(form["ddlExpiryyear"]);
                credit.CardFirstName = "";
                credit.CardHolderAddress1 = form["txtBillingAddress"];
                credit.CardHolderAddress2 = "";
                credit.CardHolderCity = form["txtBillingCity"];
                credit.CardHolderCountry = form["ISOCODE"];
                credit.CardHolderDayTimePhone = "3172218048";
                credit.CardHolderName = form["txtNameoncard"];
                credit.CardHolderState = "JH";
                credit.CardHolderZip = (form["txtBillingZipcode"]);
                credit.CardLastName = "";
                credit.CardMiddleName = "";
                credit.CardNumber = form["txtCardonnumber"];
                credit.CardSecurityCode = form["txtSecuritycode"];
                credit.CreditCardID = 0;
                credit.ModifiedBy = "";
                credit.ModifiedOn = date.ToString();
                credit.PaymentMethod = form["ddlcardname"];
                purchase.CreditCard.Add(credit);


                string jsonData = JsonConvert.SerializeObject(purchase, Formatting.Indented);
                string final = ReplaceFirstOccurrence(jsonData, "\"CreditCard\": [", "\"CreditCard\": ");
                //final = ReplaceFirstOccurrence(final, "\"Transaction\": [", "\"Transaction\": ");
                final = ReplaceFirstOccurrence(final, " ]\r\n}", "}\r\n ");
                string Domainname = ConfigurationManager.AppSettings["Domainname"];
                //string VisitorsQuote = ConfigurationManager.AppSettings["VisitorsQuote"];
                string VisitorsQuote = "http://beta.hccmis.com/vsds/";
                var t = Task.Run(() => PostRequestAtlas(final, VisitorsQuote, Domainname));
                t.Wait();
                string JsonResult = t.Result + "TRIPID:" + getTripID;
                //string JsonResult = t.Result;


                ViewBag.Message = JsonResult;

                //var result = new { ViewBag.Message,getTripID };

                return Json(ViewBag.Message, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var error = ex;
                PurchasePolicymanager.SaveErrorLog("VisitorsPurchase", error.ToString());
                string res = "0";
                return Json(res, JsonRequestBehavior.AllowGet);
            }
        }



        public ActionResult DiplomatPurchaseDetails(FormCollection form)
        {
            try
            {


                string DOB = "";
                int age = 0;
                if (form["TravelersDOB"] == null || form["TravelersDOB"] == "")
                {
                    age = Convert.ToInt32(form["age"]);
                    int getDOByear = Convert.ToInt32(DateTime.Now.Year) - age;
                    string getdate = DateTime.Now.Day.ToString();
                    string getmonth = DateTime.Now.Month.ToString();
                    DOB = getDOByear.ToString() + "-" + getmonth + "-" + getdate;
                }
                else
                {
                    DOB = form["TravelersDOB"];
                }
                PurchasePolicymodel.PlanId = Convert.ToInt64(form["hdnPlanId"]);
                PurchasePolicymodel.planDeductible = Convert.ToDouble(form["hdnplanDeductible"]);
                PurchasePolicymodel.planMaximum = Convert.ToDouble(form["hdnplanMaximum"]);
                PurchasePolicymodel.INsuranceComp = form["hdnINsuranceComp"];
                PurchasePolicymodel.policyName = form["hdnpolicyName"];
                PurchasePolicymodel.LeavingHome = Convert.ToDateTime(form["txtCoverageStartDate"]).ToString("MM/dd/yyyy");
                PurchasePolicymodel.TillDate = Convert.ToDateTime(form["txtCoverageEndDate"]).ToString("MM/dd/yyyy");
                PurchasePolicymodel.ISOCODE = form["ISOCODE"];
                PurchasePolicymodel.WhereareYouTravlingTo = form["ddlWhereareyoutravelingto"];
                PurchasePolicymodel.TravelersDOB = Convert.ToDateTime(DOB).ToString("MM/dd/yyyy");

                PurchasePolicymodel.SpouseName = form["txtspousename"];
                PurchasePolicymodel.SpouseDOB = form["txtspousedateofbirth"];
                PurchasePolicymodel.ChildName = form["txtChildname"];
                PurchasePolicymodel.ChildDOB = form["txtchilddateofbirth"];
                PurchasePolicymodel.TravelerEmail = form["txttraveleremail"];
                PurchasePolicymodel.TravelerFirstName = form["txtTravelerFirstname1"];
                PurchasePolicymodel.TravelerMiddleName = form["txtTravelerMiddlename"];
                PurchasePolicymodel.TravelerLastName = form["txtTravelerLastname"];
                PurchasePolicymodel.Gender = form["Gender1"];
                PurchasePolicymodel.TravelerPasspost = form["TravelerPasspost"];
                PurchasePolicymodel.DeparturefromHomeCountry = form["txtDeparturefromHomeCountry"];
                PurchasePolicymodel.DateofReturntoHome = form["txtDateofReturntoHome"];
                PurchasePolicymodel.Primaryapplicantfirstname = form["txtPrimaryapplicantfirstname"];
                PurchasePolicymodel.PrimaryapplicantLastname = form["txtPrimaryapplicantLastname"];
                PurchasePolicymodel.PrimaryApplicantAddress1 = form["txtPrimaryApplicantAddress1"];
                PurchasePolicymodel.PrimaryApplicantAddress2 = PurchasePolicymodel.PrimaryApplicantAddress2;
                PurchasePolicymodel.TravelerCity = form["txtTravelerCity"];
                PurchasePolicymodel.TravelerPostalCode = form["txtTravelerPostalCode"];
                PurchasePolicymodel.TravelerEmailConfirm = PurchasePolicymodel.TravelerEmailConfirm;
                PurchasePolicymodel.TravelerMobile = form["txtTravelerMobile"];
                PurchasePolicymodel.Beneficiayname = form["txtBeneficiayname"];
                PurchasePolicymodel.Relationship = PurchasePolicymodel.Relationship;
                PurchasePolicymodel.PolicyDocument = PurchasePolicymodel.PolicyDocument;
                PurchasePolicymodel.Totalamount = form["hdntotalPremium"];
                PurchasePolicymodel.Countryofcitizenship = form["ddlcitizenship"];
                PurchasePolicymodel.Cardname = form["ddlcardname"];
                PurchasePolicymodel.Month = form["ddlMonth"];
                PurchasePolicymodel.Expiryyear = form["ddlExpiryyear"];
                PurchasePolicymodel.Nameoncard = form["txtNameoncard"];
                PurchasePolicymodel.Cardonnumber = form["txtCardonnumber"];
                PurchasePolicymodel.Securitycode = form["txtSecuritycode"];
                PurchasePolicymodel.TravelerState = form["txtBeneficiayname"];
                PurchasePolicymodel.BillingAddress = form["txtBillingAddress"];
                PurchasePolicymodel.BillingAddress2 = form["txtBillingAddress2"];
                PurchasePolicymodel.BillingState = form["ddlBillingState"];
                PurchasePolicymodel.BillingCity = form["txtBillingCity"];
                PurchasePolicymodel.BillingZipcode = form["txtBillingZipcode"];
                var userid = PurchasePolicymanager.SaveUserdetails(PurchasePolicymodel);
                var getTripID = PurchasePolicymanager.saveDetailsIntripMaster(PurchasePolicymodel);


                string fromdate = Convert.ToDateTime(form["txtCoverageStartDate"]).ToString("yyyy-MM-dd");
                string todate = Convert.ToDateTime(form["txtCoverageEndDate"]).ToString("yyyy-MM-dd");
                DateTime LeavingHome = DateTime.Parse(fromdate);
                DateTime TillDate = DateTime.Parse(todate);
                var DATE = (TillDate - LeavingHome).TotalDays;
                string countmonth = (form["ddlMonth"]);
                int lenth = countmonth.Length;
                string year = form["ddlExpiryyearval"];
                string cardexpiry = "";
                string month = string.Empty;
                if (lenth == 1)
                {

                    month = "0" + form["ddlMonth"];
                }
                else
                {

                    month = form["ddlMonth"];

                }
                if (year.Length == 1)
                {
                    cardexpiry = "0" + form["ddlExpiryyearval"];
                }
                else
                {
                    cardexpiry = form["ddlExpiryyearval"];
                }
                DateTime dob = Convert.ToDateTime(form["TravelersDOB"]);
                age = CalculateAge(dob);

                string agerange = string.Empty;
                if (age >= 18 && age <= 29)
                {
                    agerange = "Range18To29";
                }
                else if (age >= 30 && age <= 39)
                {
                    agerange = "Range30To39";
                }
                else if (age >= 40 && age <= 49)
                {
                    agerange = "Range40To49";
                }
                else if (age >= 50 && age <= 59)
                {
                    agerange = "Range50To59";
                }
                else if (age >= 60 && age <= 64)
                {
                    agerange = "Range60To64";
                }
                else if (age >= 65 && age <= 69)
                {
                    agerange = "Range65To69";
                }
                else if (age >= 70 && age <= 79)
                {
                    agerange = "Range70To79";
                }
                else if (age >= 80)
                {
                    agerange = "EightyPlus";
                }

                if (form["txtspousedateofbirth"] == null)
                {
                    form["txtspousedateofbirth"] = "";
                }
                if (form["txtchild1dob"] == null)
                {
                    form["txtchild1dob"] = "";
                }
                if (form["txtchild2dob"] == null)
                {
                    form["txtchild2dob"] = "";
                }
                if (form["txtchild3dob"] == null)
                {
                    form["txtchild3dob"] = "";
                }
                if (form["txtchild4dob"] == null)
                {
                    form["txtchild4dob"] = "";
                }
                string spouseagerange = string.Empty;
                if (form["txtspousedateofbirth"] != "")
                {
                    int spouseage = Convert.ToInt32(form["txtspousedateofbirth"]);
                    DateTime spousedob = Convert.ToDateTime(form["txtspousedateofbirth"]);
                    spouseage = CalculateAge(spousedob);
                    if (spouseage >= 18 && spouseage <= 29)
                    {
                        spouseagerange = "Range18To29";
                    }
                    else if (spouseage >= 30 && spouseage <= 39)
                    {
                        spouseagerange = "Range30To39";
                    }
                    else if (spouseage >= 40 && spouseage <= 49)
                    {
                        spouseagerange = "Range40To49";
                    }
                    else if (spouseage >= 50 && spouseage <= 59)
                    {
                        spouseagerange = "Range50To59";
                    }
                    else if (spouseage >= 60 && spouseage <= 64)
                    {
                        spouseagerange = "Range60To64";
                    }
                    else if (spouseage >= 65 && spouseage <= 69)
                    {
                        spouseagerange = "Range65To69";
                    }
                    else if (spouseage >= 70 && spouseage <= 79)
                    {
                        spouseagerange = "Range70To79";
                    }
                    else if (spouseage >= 80)
                    {
                        spouseagerange = "EightyPlus";
                    }
                    else
                    {
                        spouseagerange = "None";
                    }
                }
                else
                {
                    spouseagerange = "None";
                }
                int childcount = 0;
                if (form["txtchild1dob"] != "")
                {
                    childcount = 1;
                }
                else if (form["txtchild2dob"] != "")
                {
                    childcount = 2;
                }
                else if (form["txtchild3dob"] != "")
                {
                    childcount = 3;
                }
                else if (form["txtchild4dob"] != "")
                {
                    childcount = 4;
                }
                string deductible = string.Empty;
                if (form["hdnplanDeductible"] == "0")
                {
                    deductible = "NoDeductible";
                }
                else if (form["hdnplanDeductible"] == "50")
                {
                    deductible = "Ded50";
                }
                else if (form["hdnplanDeductible"] == "100")
                {
                    deductible = "Ded100";
                }
                else if (form["hdnplanDeductible"] == "250")
                {
                    deductible = "Ded250";
                }
                else if (form["hdnplanDeductible"] == "500")
                {
                    deductible = "Ded500";
                }
                else if (form["hdnplanDeductible"] == "1000")
                {
                    deductible = "Ded1000";
                }
                else if (form["hdnplanDeductible"] == "2500")
                {
                    deductible = "Ded2500";
                }
                else if (form["hdnplanDeductible"] == "5000")
                {
                    deductible = "Ded5000";
                }
                string policymax = string.Empty;
                if (form["hdnplanMaximum"] == "50000")
                {
                    policymax = "AmericaPlanA";
                }
                else if (form["hdnplanMaximum"] == "100000")
                {
                    policymax = "AmericaPlanB";
                }
                else if (form["hdnplanMaximum"] == "250000")
                {
                    policymax = "AmericaPlanC";
                }
                else if (form["hdnplanMaximum"] == "500000")
                {
                    policymax = "AmericaPlanD";
                }
                else if (form["hdnplanMaximum"] == "1000000")
                {
                    policymax = "AmericaPlanE";
                }


                string Gender = "";
                if (form["Gender"] == "MALE")
                {
                    Gender = "M";
                }
                else
                {
                    Gender = "F";
                }
                string SpouseGender = "";
                if (form["SpouseGender"] == "MALE")
                {
                    SpouseGender = "M";
                }
                else
                {
                    SpouseGender = "F";
                }
                string child1gender = "";
                if (form["child1radio"] == "MALE")
                {
                    child1gender = "M";
                }
                else
                {
                    child1gender = "F";
                }
                string child2gender = "";
                if (form["child2radio"] == "MALE")
                {
                    child2gender = "M";
                }
                else
                {
                    child2gender = "F";
                }
                DateTime date = DateTime.Today;
                var purchase = new Models.DiplomatPurchase();
                purchase.PurchaseDate = Convert.ToDateTime(date);
                purchase.ProcessCreditCard = true;
                var subagent = new Models.SubAgent();
                subagent.Name = "Tourist Insured";
                subagent.Email = "touristinsured@gmail.com";
                purchase.SubAgent.Add(subagent);
                var billing = new Models.BillingInformation();
                billing.FirstName = form["txtNameoncard"];
                billing.MiddleInitial = "";
                billing.LastName = form["txtNameoncard"];
                billing.Email = form["txttraveleremail"];
                billing.CompanyName = "";
                billing.StreetAddress = form["txtBillingAddress"];
                billing.City = form["txtBillingCity"];
                billing.CountryISO2Code = form["ISOCODE"];
                billing.StateISOCode = "JH";
                billing.PostalCode = form["txtBillingZipcode"];
                billing.PhoneNumber = form["txtTravelerMobile"];
                billing.CreditCardNumber = form["txtCardonnumber"];
                billing.CreditCardType = form["ddlcardname"];
                billing.CreditCardExpirationMonth = month;
                billing.CreditCardExpirationYear = cardexpiry;
                billing.CVV2 = form["txtSecuritycode"];
                purchase.BillingInformation.Add(billing);
                var Primary = new Models.PrimaryAdultTraveler();
                Primary.FirstName = form["txtTravelerFirstname1"];
                Primary.MiddleInitial = form["txtTravelerMiddlename"];
                Primary.LastName = form["txtTravelerLastname"];
                Primary.Gender = Gender;
                Primary.DateOfBirth = Convert.ToDateTime(form["TravelersDOB"]);
                Primary.AddressLineOne = form["txtPrimaryApplicantAddress1"];
                Primary.AddressLineTwo = form["txtPrimaryApplicantAddress2"];
                Primary.City = form["txtTravelerCity"];
                Primary.CountryISO2Code = form["ISOCODE"];
                Primary.StateISOCode = "JH";
                Primary.PostalCode = form["txtTravelerPostalCode"];
                Primary.PrimaryEmailAddress = form["txttraveleremail"];
                Primary.SecondaryEmailAddress = form["txttraveleremail"];
                Primary.HomePhone = form["txtTravelerMobile"];
                Primary.WorkPhone = "";
                Primary.CellPhone = "";
                Primary.PassportNumber = "";
                Primary.PassportIssuingCountryISO2Code = form["ISOCODE"];
                Primary.Beneficiaries = new List<Models.Beneficiaries>
            {
                new Models.Beneficiaries
                {
                    FirstName=form["txtBeneficiayname"],
                    LastName=form["txtBeneficiayname"],
                    DateOfBirth="",
                    BenefitPercentage=100,
                    Relationship="Wife",
                    Address="",
                    City="",
                    Region="",
                    State="",
                    PostalCode="",
                    Phone="",
                    CountryIso2Code=form["ISOCODE"]
                }
            };
                purchase.PrimaryAdultTraveler.Add(Primary);

                var date1 = form["txtspousedateofbirth"];
                if (form["txtspousedateofbirth"] != "")
                {
                    purchase.AdditionalTravelers = new List<Models.AdditionalTravelers>
                {
                    new Models.AdditionalTravelers
                    {
                        FirstName=form["txtSpousefirstname"],
                        LastName=form["txtspouselastname"],
                        Relationship="Spouse",
                        DateOfBirth=Convert.ToDateTime(form["txtspousedateofbirth"]),
                        Beneficiaries=new List<Models.Beneficiaries>
            {
                new Models.Beneficiaries
                {
                    FirstName=form["txtBeneficiayname"],
                    LastName=form["txtBeneficiayname"],
                    DateOfBirth="",
                    BenefitPercentage=100,
                    Relationship="Wife",
                    Address="",
                    City="",
                    Region="",
                    State="",
                    PostalCode="",
                    Phone="",
                    CountryIso2Code=form["ISOCODE"]
                }
            }
            }
                };
                }
                else
                {
                    purchase.AdditionalTravelers = new List<Models.AdditionalTravelers>
                    {

                    };
                }
                var pre = form["hdntotalPremium"];
                purchase.PolicyPremium = Convert.ToDouble(form["hdntotalPremium"]);
                purchase.PolicyStartDate = Convert.ToDateTime(form["txtCoverageStartDate"]);
                purchase.PolicyEndDate = Convert.ToDateTime(form["txtCoverageEndDate"]);
                purchase.LTNumberOfMonths = 0;
                purchase.Plan = policymax;
                purchase.TravelerOneAgeRange = agerange;
                purchase.TravelerTwoAgeRange = spouseagerange;
                purchase.Deductible = deductible;
                purchase.ADDBenefit = "ADD25";
                purchase.NumberOfMinorDependents = childcount;
                purchase.Riders = new List<string>
            {
                ""
            };
                purchase.CountryIso2Codes = new List<string>
            {
                form["ddlWhereareyoutravelingto"]
            };
                purchase.WarRiskCoverage = "None";

                string jsonData = JsonConvert.SerializeObject(purchase, Formatting.Indented);
                string final = ReplaceFirstOccurrence(jsonData, "\"SubAgent\": [", "\"SubAgent\": ");
                final = ReplaceFirstOccurrence(final, "\"BillingInformation\": [", "\"BillingInformation\": ");
                final = ReplaceFirstOccurrence(final, "\"PrimaryAdultTraveler\": [", "\"PrimaryAdultTraveler\": ");
                //final = ReplaceFirstOccurrence(final, "\"Transaction\": [", "\"Transaction\": ");
                final = ReplaceFirstOccurrence(final, "\r\n    }\r\n  ],\r\n  \"BillingInformation\"", "\r\n    }\r\n,\r\n  \"BillingInformation\" ");
                final = ReplaceFirstOccurrence(final, "\r\n    }\r\n  ],\r\n  \"PrimaryAdultTraveler\"", "\r\n    }\r\n,\r\n  \"PrimaryAdultTraveler\" ");
                final = ReplaceFirstOccurrence(final, "\r\n    }\r\n  ],\r\n  \"AdditionalTravelers\"", "\r\n    }\r\n,\r\n  \"AdditionalTravelers\" ");


                string AtlasGEtQuote = "https://stagingglobalunderwriters.azurewebsites.net/v1/Policy/DiplomatPolicyPurchase";
                string Domainname = ConfigurationManager.AppSettings["Domainname"];
                var t = Task.Run(() => PostRequestDiplomat(final, AtlasGEtQuote, Domainname));
                t.Wait();
                string JsonResult = t.Result + "TRIPID:" + getTripID;
                ViewBag.Message = JsonResult;








                return Json(ViewBag.Message, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var error = ex;
                PurchasePolicymanager.SaveErrorLog("DiplomatPurchase", error.ToString());
                string res = "0";
                return Json(res, JsonRequestBehavior.AllowGet);
            }
        }

        private string ReplaceFirstOccurrence(string Source, string Find, string Replace)
        {
            int Place = Source.IndexOf(Find);
            string result = Source.Remove(Place, Find.Length).Insert(Place, Replace);
            return result;
        }


        public ActionResult GetStatedetails()
        {
            var result = PurchasePolicymanager.BindState();
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetCountryList3rdtab()
        {
            var Result = PurchasePolicymanager.BindHomeCountry();
            return Json(Result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetIMGCountryCode(FormCollection form)
        {
            string IMGISOCODE = PurchasePolicymanager.GetIMGCountryCode(form["ddlcitizenship"]);
            return Json(IMGISOCODE, JsonRequestBehavior.AllowGet);
        }

        private static async Task<string> PostRequestDiplomat(string content, string url, string baseurl)
        {
            try
            {
                var data = new StringContent(content, Encoding.UTF8, "application/json");
                HttpClientHandler handler = new HttpClientHandler();
                var client = new HttpClient(handler);
                client.BaseAddress = new Uri(baseurl);
                client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
                var byteArray = Encoding.ASCII.GetBytes("http://www.touristinsured.com/:719402A6-2760-4BC5-B9F9-C6AF768D9A75");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                //client.DefaultRequestHeaders.Add("Authorization", AuthKey);
                var response = await client.PostAsync(url, data);
                String ResponseJson = response.Content.ReadAsStringAsync().Result;
                return ResponseJson;
            }

            catch (Exception ex)
            {
                String ResponseJson = "";

                QuotationManager quotes = new QuotationManager();
                quotes.SaveErrorLog(url.Substring(0, 20), ex.ToString());
                return ResponseJson;
            }
        }

        private static int CalculateAge(DateTime dateOfBirth)
        {
            int age = 0;
            age = DateTime.Now.Year - dateOfBirth.Year;
            if (DateTime.Now.DayOfYear < dateOfBirth.DayOfYear)
                age = age;

            return age;
        }
    }
}