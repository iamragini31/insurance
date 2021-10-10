using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
using System.Data;

using Newtonsoft.Json.Linq;

namespace TouristInsured.Controllers
{
    public class ImmigrantQuotationController : Controller
    {
        List<AllQuatationResult> lstQuatationResult = new List<AllQuatationResult>();
        DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
        QuotationModel Quotationmodel = new QuotationModel();
        QuotationManager quotationalmanager = new QuotationManager();
        LoginModel Loginmodel = new LoginModel();
        private static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        // GET: Quotation
        public ActionResult ImmigrantQuotation(string Data)
        {
            DataTable Quotes = (DataTable)JsonConvert.DeserializeObject(Data, (typeof(DataTable)));
            Quotationmodel.CountryName = Quotes.Rows[0]["CountryName"].ToString();
            Quotationmodel.TouristCountryName = Quotes.Rows[0]["txttouristountry"].ToString();
            Quotationmodel.ISOCODE = Quotes.Rows[0]["ISOCODE"].ToString();
            Quotationmodel.TouristISOCODE = Quotes.Rows[0]["TouristISOCODE"].ToString();
            Quotationmodel.LeavingHome = Quotes.Rows[0]["txtCoverageStartDate"].ToString();
            Quotationmodel.TillDate = Quotes.Rows[0]["txtCoverageEndDate"].ToString();
            Quotationmodel.Travelersnumber = Quotes.Rows[0]["txttravelerdetails"].ToString();
            Quotationmodel.travelersage = Convert.ToInt32(Quotes.Rows[0]["txttravellerageinyear"]);
            Quotationmodel.spouseage = Quotes.Rows[0]["txtspouseage"].ToString();
            Quotationmodel.child1age = Quotes.Rows[0]["txtchild1age"].ToString();
            Quotationmodel.child2age = Quotes.Rows[0]["txtchild2age"].ToString();
            Quotationmodel.child3age = Quotes.Rows[0]["txtchild3age"].ToString();
            Quotationmodel.child4age = Quotes.Rows[0]["txtchild4age"].ToString();
            Quotationmodel.Citizenship = Quotes.Rows[0]["Citizenship"].ToString();
            return View(Quotationmodel);
        }



        [HttpPost]
        public ActionResult GetQuotationAllList(QuotationModel req)
        {

           
            string IMGstatus = ConfigurationManager.AppSettings["IMGStatus"];
            string INFStatus = ConfigurationManager.AppSettings["INFStatus"];
            string TrawickStatus = ConfigurationManager.AppSettings["TrawickStatus"];
            string AtlasStatus = ConfigurationManager.AppSettings["AtlasStatus"];
            string VisitorStatus = ConfigurationManager.AppSettings["VisitorStatus"];
            
            int imgrespose = 0;
            int age = 0;
            int count = 0;
            if (req.travelersage == 0)
            {
                DateTime dob = Convert.ToDateTime(req.TravelersDOB);
                age = CalculateAge(dob);
            }
            else
            {
                age = req.travelersage;
            }

            string Appname = "";

            if (req.TouristISOCODE == null && req.ISOCODE == null)
            {


            }
            else
            {
                string fromdate = Convert.ToDateTime(req.LeavingHome).ToString("yyyy-MM-dd");
                string todate = Convert.ToDateTime(req.TillDate).ToString("yyyy-MM-dd");
                DateTime LeavingHome = DateTime.Parse(fromdate);
                DateTime TillDate = DateTime.Parse(todate);
                var DATE = (TillDate - LeavingHome).TotalDays;

                DATE = DATE + 1;
                if (DATE >= 5 && DATE <= 364)
                {
                    if (req.ISOCODE == "BW" || req.ISOCODE == "GM" || req.ISOCODE == "GH" || req.ISOCODE == "NE" || req.ISOCODE == "NG" || req.ISOCODE == "SL")
                    {
                    }
                    else
                    {
                        if (IMGstatus == "1")
                        {
                            string tokenimg = Imgtoken();
                            if (req.TouristISOCODE == "US")
                            {

                                string[] productCode = { "PATAP", "PATAI", "VIC" };
                                for (int i = 0; i < productCode.Length; i++)
                                {
                                    if (productCode[i] == "PATAP")
                                    {
                                        string apptype = "0521";
                                        if (age >= 80)
                                        {
                                            int[] deductible = { 0, 100, 250, 500, 1000, 2500 };
                                            int maximumlimit = 10000;
                                            for (int j = 0; j < deductible.Length; j++)
                                            {
                                                if (deductible[j] == 0)
                                                {
                                                    imgrespose = SearchIMG(productCode[i], deductible[j], maximumlimit, req, apptype, tokenimg);

                                                }
                                                else
                                                {
                                                    if (imgrespose == 1)
                                                    {
                                                        GetDummyIMGValue(productCode[i], deductible[j], maximumlimit, req, apptype);
                                                        imgrespose = 0;
                                                    }

                                                }
                                            }

                                        }
                                        else if (age >= 70 && age <= 79)
                                        {
                                            int[] deductible = { 0, 100, 250, 500, 1000, 2500 };
                                            int[] maxlimit = { 50000 };
                                            for (int k = 0; k < maxlimit.Length; k++)
                                            {
                                                for (int j = 0; j < deductible.Length; j++)
                                                {
                                                    if (deductible[j] == 0)
                                                    {
                                                        imgrespose = SearchIMG(productCode[i], deductible[j], maxlimit[k], req, apptype, tokenimg);

                                                    }
                                                    else
                                                    {
                                                        if (imgrespose == 1)
                                                        {
                                                            GetDummyIMGValue(productCode[i], deductible[j], maxlimit[k], req, apptype);
                                                            imgrespose = 0;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            int[] deductible = { 0, 100, 250, 500, 1000, 2500 };
                                            int[] maxlimit = { 50000, 100000, 500000 };

                                            for (int k = 0; k < maxlimit.Length; k++)
                                            {
                                                if (maxlimit[k] == 50000)
                                                {
                                                    for (int j = 0; j < deductible.Length; j++)
                                                    {
                                                        if (deductible[j] == 0)
                                                        {
                                                            imgrespose = SearchIMG(productCode[i], deductible[j], maxlimit[k], req, apptype, tokenimg);

                                                        }
                                                        else
                                                        {
                                                            if (imgrespose == 1)
                                                            {
                                                                GetDummyIMGValue(productCode[i], deductible[j], maxlimit[k], req, apptype);

                                                            }
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    for (int j = 0; j < deductible.Length; j++)
                                                    {
                                                        if (imgrespose == 1)
                                                        {
                                                            GetDummyIMGValue(productCode[i], deductible[j], maxlimit[k], req, apptype);
                                                            imgrespose = 0;
                                                        }

                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else if (productCode[i] == "PATAI")
                                    {
                                        string apptype = "0521";
                                        if (age >= 80)
                                        {
                                            int[] deductible = { 0, 100, 250, 500, 1000, 2500 };
                                            int maximumlimit = 10000;
                                            for (int j = 0; j < deductible.Length; j++)
                                            {
                                                if (deductible[j] == 0)
                                                {
                                                    imgrespose = SearchIMG(productCode[i], deductible[j], maximumlimit, req, apptype, tokenimg);

                                                }
                                                else
                                                {

                                                    if (imgrespose == 1)
                                                    {
                                                        GetDummyIMGValue(productCode[i], deductible[j], maximumlimit, req, apptype);
                                                        imgrespose = 0;
                                                    }

                                                }
                                            }

                                        }
                                        else if (age >= 70 && age <= 79)
                                        {
                                            int[] deductible = { 0, 100, 250, 500, 1000, 2500 };
                                            int[] maxlimit = { 50000 };
                                            for (int k = 0; k < maxlimit.Length; k++)
                                            {
                                                for (int j = 0; j < deductible.Length; j++)
                                                {
                                                    if (deductible[j] == 0)
                                                    {
                                                        imgrespose = SearchIMG(productCode[i], deductible[j], maxlimit[k], req, apptype, tokenimg);

                                                    }
                                                    else
                                                    {
                                                        if (imgrespose == 1)
                                                        {
                                                            GetDummyIMGValue(productCode[i], deductible[j], maxlimit[k], req, apptype);
                                                            imgrespose = 0;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            int[] deductible = { 0, 100, 250, 500, 1000, 2500 };
                                            int[] maxlimit = { 50000, 100000, 500000, 1000000 };
                                            for (int k = 0; k < maxlimit.Length; k++)
                                            {
                                                if (maxlimit[k] == 50000)
                                                {
                                                    for (int j = 0; j < deductible.Length; j++)
                                                    {
                                                        if (deductible[j] == 0)
                                                        {
                                                            imgrespose = SearchIMG(productCode[i], deductible[j], maxlimit[k], req, apptype, tokenimg);

                                                        }
                                                        else
                                                        {
                                                            if (imgrespose == 1)
                                                            {
                                                                GetDummyIMGValue(productCode[i], deductible[j], maxlimit[k], req, apptype);

                                                            }
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    for (int j = 0; j < deductible.Length; j++)
                                                    {
                                                        if (imgrespose == 1)
                                                        {
                                                            GetDummyIMGValue(productCode[i], deductible[j], maxlimit[k], req, apptype);
                                                            imgrespose = 0;
                                                        }

                                                    }

                                                }
                                            }
                                        }
                                    }
                                    else if (productCode[i] == "VIC")
                                    {
                                        if (req.TouristISOCODE == "US" || req.TouristISOCODE == "ASM" || req.TouristISOCODE == "GUM" || req.TouristISOCODE == "MNP" || req.TouristISOCODE == "PRI" || req.TouristISOCODE == "UMI" || req.TouristISOCODE == "VIR")
                                        {

                                            if (age >= 80)
                                            {
                                                string apptype = "0521A";
                                                int[] deductible = { 50, 100 };
                                                int maximumlimit = 10000;
                                                for (int j = 0; j < deductible.Length; j++)
                                                {
                                                    if (deductible[j] == 0)
                                                    {
                                                        imgrespose = SearchIMG(productCode[i], deductible[j], maximumlimit, req, apptype, tokenimg);

                                                    }
                                                    else
                                                    {
                                                        if (imgrespose == 1)
                                                        {
                                                            GetDummyIMGValue(productCode[i], deductible[j], maximumlimit, req, apptype);
                                                            imgrespose = 0;
                                                        }
                                                    }
                                                }

                                            }

                                            else
                                            {
                                                string[] apptype = { "0521A", "0521B", "0521C" };
                                                int[] deductible = { 0, 50, 100 };


                                                for (int k = 0; k < apptype.Length; k++)
                                                {
                                                    if (apptype[k] == "0521A")
                                                    {
                                                        int maxlimit = 25000;
                                                        for (int j = 0; j < deductible.Length; j++)
                                                        {
                                                            if (deductible[j] == 0)
                                                            {
                                                                imgrespose = SearchIMG(productCode[i], deductible[j], maxlimit, req, apptype[k], tokenimg);

                                                            }
                                                            else
                                                            {
                                                                if (imgrespose == 1)
                                                                {
                                                                    GetDummyIMGValue(productCode[i], deductible[j], maxlimit, req, apptype[k]);

                                                                }
                                                            }
                                                        }
                                                    }
                                                    else if (apptype[k] == "0521B")
                                                    {
                                                        int maxlimit = 50000;
                                                        for (int j = 0; j < deductible.Length; j++)
                                                        {
                                                            if (deductible[j] == 0)
                                                            {
                                                                SearchIMG(productCode[i], deductible[j], maxlimit, req, apptype[k], tokenimg);

                                                            }
                                                            else
                                                            {
                                                                if (imgrespose == 1)
                                                                {
                                                                    GetDummyIMGValue(productCode[i], deductible[j], maxlimit, req, apptype[k]);
                                                                }
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        int maxlimit = 100000;
                                                        for (int j = 0; j < deductible.Length; j++)
                                                        {
                                                            if (deductible[j] == 0)
                                                            {
                                                                SearchIMG(productCode[i], deductible[j], maxlimit, req, apptype[k], tokenimg);

                                                            }
                                                            else
                                                            {
                                                                if (imgrespose == 1)
                                                                {
                                                                    GetDummyIMGValue(productCode[i], deductible[j], maxlimit, req, apptype[k]);
                                                                    imgrespose = 0;
                                                                }

                                                            }
                                                        }
                                                    }

                                                }
                                            }
                                        }
                                    }
                                }

                            }

                        }
                    }
                }
                if (INFStatus == "1")
                {
                    var Result4 = Search(req);
                }
                if (DATE <= 364)
                {
                    if (req.ISOCODE == "AU" || req.ISOCODE == "CU" || req.ISOCODE == "IR" || req.ISOCODE == "KP" || req.ISOCODE == "CA")
                    {

                    }
                    else
                    {
                        if (VisitorStatus == "1")
                        {
                            if (age >= 80)
                            {
                                string PlanName = "A";
                                string[] Deductible = { "100", "200" };


                                for (int j = 0; j < Deductible.Length; j++)
                                {
                                    if (Deductible[j] == "100")
                                    {
                                        SearchVisitorSecure(Deductible[j], PlanName, req, age);
                                    }
                                    else
                                    {
                                        GetdummyVisitorsvalue(Deductible[j], PlanName, req, age);
                                    }
                                }





                            }
                            else if (age >= 70 && age <= 79)
                            {
                                string[] PlanName = { "A", "B" };
                                string[] Deductible = { "100", "200" };
                                for (int i = 0; i < PlanName.Length; i++)
                                {
                                    if (PlanName[i] == "A")
                                    {
                                        for (int j = 0; j < Deductible.Length; j++)
                                        {
                                            if (Deductible[j] == "100")
                                            {
                                                SearchVisitorSecure(Deductible[j], PlanName[i], req, age);
                                            }
                                            else
                                            {
                                                GetdummyVisitorsvalue(Deductible[j], PlanName[i], req, age);
                                            }
                                        }
                                    }
                                    else
                                    {


                                        for (int j = 0; j < Deductible.Length; j++)
                                        {

                                            GetdummyVisitorsvalue(Deductible[j], PlanName[i], req, age);


                                        }
                                    }

                                }
                            }
                            else
                            {
                                string[] PlanName = { "A", "B", "C", "D" };
                                string[] Deductible = { "0", "50", "100" };
                                for (int i = 0; i < PlanName.Length; i++)
                                {
                                    if (PlanName[i] == "A")
                                    {
                                        for (int j = 0; j < Deductible.Length; j++)
                                        {
                                            if (Deductible[j] == "0")
                                            {
                                                SearchVisitorSecure(Deductible[j], PlanName[i], req, age);
                                            }
                                            else
                                            {
                                                GetdummyVisitorsvalue(Deductible[j], PlanName[i], req, age);
                                            }
                                        }
                                    }
                                    else
                                    {


                                        for (int j = 0; j < Deductible.Length; j++)
                                        {

                                            GetdummyVisitorsvalue(Deductible[j], PlanName[i], req, age);


                                        }
                                    }

                                }
                            }
                        }
                    }
                }

            }


            var Result2 = lstQuatationResult.OrderBy(x => x.planMaximum).ThenBy(x => x.planDeductible);
            //var Result2 = lstQuatationResult.OrderBy(x => x.totalPremium);
            var Result3 = lstQuatationResult.OrderByDescending(x => x.planMaximum).ThenBy(x => x.planDeductible);
            var Result5 = Result3.OrderByDescending(x => x.planDeductible);

            var DistinctQuot = Result2.GroupBy(x => x.policyName).Select(x => x.FirstOrDefault()).ToList();
            var DistinctQuot2 = Result3.GroupBy(x => x.policyName).Select(x => x.FirstOrDefault()).ToList();
            //var OrderByDeductible = Result2.OrderBy(x => x.planDeductible).Where(x => x.planDeductible == 50).ToList();
            //var OrderBynotnull = OrderByDeductible.GroupBy(x => x.policyName).Select(x => x.FirstOrDefault()).Where(x => x.totalPremium != 0.0).ToList();
            var taborderbyasc = DistinctQuot.OrderBy(x => x.totalPremium).ToList();
            var taborderbydes = DistinctQuot.OrderByDescending(x => x.totalPremium).ToList();
            var DistinctQuotNo = Result2.GroupBy(x => x.policyName).Select(x => x.FirstOrDefault()).Where(x => x.prexded == "N").ToList();
            var DistinctQuotNo2 = Result3.GroupBy(x => x.policyName).Select(x => x.FirstOrDefault()).Where(x => x.prexded == "N").ToList();

            var DistinctQuotYes = Result2.GroupBy(x => x.policyName).Select(x => x.FirstOrDefault()).Where(x => x.prexded == "Y").ToList();
            var DistinctQuotYes2 = Result3.GroupBy(x => x.policyName).Select(x => x.FirstOrDefault()).Where(x => x.prexded == "Y").ToList();
            var INsuranceComp = Result2.GroupBy(x => x.INsuranceComp).Select(x => x.FirstOrDefault()).ToList();
            //var INFGroupNo = DistinctQuotNo.GroupBy(x => x.policyName).Select(x => x.FirstOrDefault()).Where(x => x.INsuranceComp == "INF").ToList();
            //var INFGroupYes = DistinctQuotYes.GroupBy(x => x.policyName).Select(x => x.FirstOrDefault()).Where(x => x.INsuranceComp == "INF").ToList();
            var INFGroupNo = Result2.GroupBy(x => x.policyName).Select(x => x.FirstOrDefault()).Where(x => x.INsuranceComp == "INF").ToList();
            var INFGroupYes = Result2.GroupBy(x => x.policyName).Select(x => x.FirstOrDefault()).Where(x => x.INsuranceComp == "INF").ToList();
            var AtlasGroup = Result2.GroupBy(x => x.policyName).Select(x => x.FirstOrDefault()).Where(x => x.INsuranceComp == "Trawick").ToList();
            var PLANTYPE = Result2.GroupBy(x => x.Plan_Type).Select(x => x.FirstOrDefault()).Where(x => x.Plan_Type != null).ToList();
            var ComprehensivePlan = Result2.GroupBy(x => x.Appname).Select(x => x.FirstOrDefault()).Where(x => x.Plan_Type == "Comprehensive").ToList();
            var LimitedPlan = Result2.GroupBy(x => x.Appname).Select(x => x.FirstOrDefault()).Where(x => x.Plan_Type == "LIMITED").ToList();
            var PreExisting = Result2.GroupBy(x => x.policyName).Select(x => x.FirstOrDefault()).Where(x => x.Pre_Existing == "YES").ToList();
            var PreExistingNo = Result2.GroupBy(x => x.policyName).Select(x => x.FirstOrDefault()).Where(x => x.Pre_Existing == "NO").ToList();


            var Comprehensivebyasc = ComprehensivePlan.OrderBy(x => x.totalPremium).ToList();
            var Comprehensivebydes = ComprehensivePlan.OrderByDescending(x => x.totalPremium).ToList();
            //check with 983
            var ComprehensivebyascPreexistingYES = ComprehensivePlan.OrderBy(x => x.totalPremium).Where(x => x.Pre_Existing == "YES").ToList();
            // check with 978
            var ComprehensivebydesPreexistingYES = ComprehensivePlan.OrderByDescending(x => x.totalPremium).Where(x => x.Pre_Existing == "YES").ToList();
            //check with 983
            var ComprehensivebyascPreexistingNO = ComprehensivePlan.OrderBy(x => x.totalPremium).Where(x => x.Pre_Existing == "NO").ToList();
            //check with 986
            var ComprehensivebydesPreexistingNO = ComprehensivePlan.OrderByDescending(x => x.totalPremium).Where(x => x.Pre_Existing == "NO").ToList();


            var LimitedPreExistingYEs = LimitedPlan.GroupBy(x => x.policyName).Select(x => x.FirstOrDefault()).Where(x => x.Pre_Existing == "YES").ToList();
            var LimitedPreExistingNo = LimitedPlan.GroupBy(x => x.policyName).Select(x => x.FirstOrDefault()).Where(x => x.Pre_Existing == "NO").ToList();
            var LimitedByDes = LimitedPlan.OrderByDescending(x => x.totalPremium).ToList();
            var LimitedByAsc = LimitedPlan.OrderBy(x => x.totalPremium).ToList();

            var ComprehensivePreExistingYes = ComprehensivePlan.GroupBy(x => x.policyName).Select(x => x.FirstOrDefault()).Where(x => x.Pre_Existing == "YES").ToList();
            var ComprehensivePreExistingNo = ComprehensivePlan.GroupBy(x => x.policyName).Select(x => x.FirstOrDefault()).Where(x => x.Pre_Existing == "NO").ToList();

            var PreexistingByLimitedByAsc = PreExisting.OrderBy(x => x.totalPremium).Where(x => x.Plan_Type == "LIMITED").ToList();
            //same
            //var PreexistingByComprehensiveByAcs = PreExisting.OrderBy(x => x.totalPremium).Where(x => x.Plan_Type == "Comprehensive").ToList();
            var PreexistingByLimitedbydes = PreExisting.OrderByDescending(x => x.totalPremium).Where(x => x.Plan_Type == "LIMITED").ToList();

            //same
            //var PreexistingByComprehensivebydes = PreExisting.OrderByDescending(x => x.totalPremium).Where(x => x.Plan_Type == "Comprehensive").ToList();

            var PreExistingNoByDes = PreExistingNo.OrderByDescending(x => x.totalPremium).ToList();
            var PreExistingNoByAcs = PreExistingNo.OrderBy(x => x.totalPremium).ToList();

            var PreExistingYesByDes = PreExisting.OrderByDescending(x => x.totalPremium).ToList();
            var PreExistingYesByAcs = PreExisting.OrderBy(x => x.totalPremium).ToList();

            var PreexistingNoByLimitedByAsc = PreExistingNo.OrderBy(x => x.totalPremium).Where(x => x.Plan_Type == "LIMITED").ToList();
            //same
            //var PreexistingNoByComprehensiveByAsc = PreExistingNo.OrderBy(x => x.totalPremium).Where(x => x.Plan_Type == "Comprehensive").ToList();
            var PreexistingNoByLimitedbydes = PreExistingNo.OrderByDescending(x => x.totalPremium).Where(x => x.Plan_Type == "LIMITED").ToList();

            //same
            //var PreexistingNoByComprehensivebydes = PreExistingNo.OrderByDescending(x => x.totalPremium).Where(x => x.Plan_Type == "Comprehensive").ToList();

            var Res = new
            {
                INsuranceComp,
                Result2,
                DistinctQuot,
                DistinctQuotNo,
                DistinctQuotYes,
                INFGroupNo,
                INFGroupYes,
                AtlasGroup,
                PLANTYPE,
                ComprehensivePlan,
                LimitedPlan,
                Result3,
                DistinctQuot2,
                DistinctQuotNo2,
                Result5,
                taborderbyasc,
                taborderbydes,
                PreExisting,
                PreExistingNo,
                Comprehensivebyasc,
                Comprehensivebydes,
                ComprehensivebyascPreexistingYES,
                ComprehensivebydesPreexistingYES,
                ComprehensivebyascPreexistingNO,
                ComprehensivebydesPreexistingNO,
                LimitedPreExistingYEs,
                LimitedPreExistingNo,
                LimitedByDes,
                LimitedByAsc,
                ComprehensivePreExistingYes,
                ComprehensivePreExistingNo,
                PreexistingByLimitedByAsc,
                PreexistingByLimitedbydes,
                PreExistingNoByDes,
                PreExistingNoByAcs,
                PreExistingYesByDes,
                PreExistingYesByAcs,
                PreexistingNoByLimitedByAsc,
                PreexistingNoByLimitedbydes
            };
            return Json(Res, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetAllQuationList(FormCollection form)
        {
            QuotationModel req = new QuotationModel();
            req.LeavingHome = form["txtCoverageStartDate"];
            req.TillDate = form["txtCoverageEndDate"];
            req.ISOCODE = form["hdnisocode"];
            req.TouristISOCODE = form["hdnTouristIsocode"];
            req.travelersage = Convert.ToInt32(form["txttravellerageinyear"]);
            req.spouseage = form["txtspouseage"];
            req.child1age = form["txtchild1age"];
            req.child2age = form["txtchild2age"];
            req.child3age = form["txtchild3age"];
            req.child4age = form["txtchild4age"];
            req.Citizenship = form["citizenship"];

            string IMGstatus = ConfigurationManager.AppSettings["IMGStatus"];
            string INFStatus = ConfigurationManager.AppSettings["INFStatus"];
            string TrawickStatus = ConfigurationManager.AppSettings["TrawickStatus"];
            string AtlasStatus = ConfigurationManager.AppSettings["AtlasStatus"];
            string VisitorStatus = ConfigurationManager.AppSettings["VisitorStatus"];
            int imgrespose = 0;
            int age = 0;
            int count = 0;
            if (req.travelersage == 0)
            {
                DateTime dob = Convert.ToDateTime(req.TravelersDOB);
                age = CalculateAge(dob);
            }
            else
            {
                age = req.travelersage;
            }

            string Appname = "";

            if (req.TouristISOCODE == null && req.ISOCODE == null)
            {


            }
            else
            {
                string fromdate = Convert.ToDateTime(req.LeavingHome).ToString("yyyy-MM-dd");
                string todate = Convert.ToDateTime(req.TillDate).ToString("yyyy-MM-dd");
                DateTime LeavingHome = DateTime.Parse(fromdate);
                DateTime TillDate = DateTime.Parse(todate);
                var DATE = (TillDate - LeavingHome).TotalDays;

                DATE = DATE + 1;
                if (DATE >= 5 && DATE <= 364)
                {
                    if (req.ISOCODE == "BW" || req.ISOCODE == "GM" || req.ISOCODE == "GH" || req.ISOCODE == "NE" || req.ISOCODE == "NG" || req.ISOCODE == "SL")
                    {
                    }
                    else
                    {
                        if (IMGstatus == "1")
                        {
                            string tokenimg = Imgtoken();
                            if (req.TouristISOCODE == "US")
                            {

                                string[] productCode = { "PATAP", "PATAI", "VIC" };
                                for (int i = 0; i < productCode.Length; i++)
                                {
                                    if (productCode[i] == "PATAP")
                                    {
                                        string apptype = "0521";
                                        if (age >= 80)
                                        {
                                            int[] deductible = { 0, 100, 250, 500, 1000, 2500 };
                                            int maximumlimit = 10000;
                                            for (int j = 0; j < deductible.Length; j++)
                                            {
                                                if (deductible[j] == 0)
                                                {
                                                    imgrespose = SearchIMG(productCode[i], deductible[j], maximumlimit, req, apptype, tokenimg);

                                                }
                                                else
                                                {
                                                    if (imgrespose == 1)
                                                    {
                                                        GetDummyIMGValue(productCode[i], deductible[j], maximumlimit, req, apptype);
                                                        imgrespose = 0;
                                                    }

                                                }
                                            }

                                        }
                                        else if (age >= 70 && age <= 79)
                                        {
                                            int[] deductible = { 0, 100, 250, 500, 1000, 2500 };
                                            int[] maxlimit = { 50000 };
                                            for (int k = 0; k < maxlimit.Length; k++)
                                            {
                                                for (int j = 0; j < deductible.Length; j++)
                                                {
                                                    if (deductible[j] == 0)
                                                    {
                                                        imgrespose = SearchIMG(productCode[i], deductible[j], maxlimit[k], req, apptype, tokenimg);

                                                    }
                                                    else
                                                    {
                                                        if (imgrespose == 1)
                                                        {
                                                            GetDummyIMGValue(productCode[i], deductible[j], maxlimit[k], req, apptype);
                                                            imgrespose = 0;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            int[] deductible = { 0, 100, 250, 500, 1000, 2500 };
                                            int[] maxlimit = { 50000, 100000, 500000 };

                                            for (int k = 0; k < maxlimit.Length; k++)
                                            {
                                                if (maxlimit[k] == 50000)
                                                {
                                                    for (int j = 0; j < deductible.Length; j++)
                                                    {
                                                        if (deductible[j] == 0)
                                                        {
                                                            imgrespose = SearchIMG(productCode[i], deductible[j], maxlimit[k], req, apptype, tokenimg);

                                                        }
                                                        else
                                                        {
                                                            if (imgrespose == 1)
                                                            {
                                                                GetDummyIMGValue(productCode[i], deductible[j], maxlimit[k], req, apptype);

                                                            }
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    for (int j = 0; j < deductible.Length; j++)
                                                    {
                                                        if (imgrespose == 1)
                                                        {
                                                            GetDummyIMGValue(productCode[i], deductible[j], maxlimit[k], req, apptype);
                                                            imgrespose = 0;
                                                        }

                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else if (productCode[i] == "PATAI")
                                    {
                                        string apptype = "0521";
                                        if (age >= 80)
                                        {
                                            int[] deductible = { 0, 100, 250, 500, 1000, 2500 };
                                            int maximumlimit = 10000;
                                            for (int j = 0; j < deductible.Length; j++)
                                            {
                                                if (deductible[j] == 0)
                                                {
                                                    imgrespose = SearchIMG(productCode[i], deductible[j], maximumlimit, req, apptype, tokenimg);

                                                }
                                                else
                                                {

                                                    if (imgrespose == 1)
                                                    {
                                                        GetDummyIMGValue(productCode[i], deductible[j], maximumlimit, req, apptype);
                                                        imgrespose = 0;
                                                    }

                                                }
                                            }

                                        }
                                        else if (age >= 70 && age <= 79)
                                        {
                                            int[] deductible = { 0, 100, 250, 500, 1000, 2500 };
                                            int[] maxlimit = { 50000 };
                                            for (int k = 0; k < maxlimit.Length; k++)
                                            {
                                                for (int j = 0; j < deductible.Length; j++)
                                                {
                                                    if (deductible[j] == 0)
                                                    {
                                                        imgrespose = SearchIMG(productCode[i], deductible[j], maxlimit[k], req, apptype, tokenimg);

                                                    }
                                                    else
                                                    {
                                                        if (imgrespose == 1)
                                                        {
                                                            GetDummyIMGValue(productCode[i], deductible[j], maxlimit[k], req, apptype);
                                                            imgrespose = 0;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            int[] deductible = { 0, 100, 250, 500, 1000, 2500 };
                                            int[] maxlimit = { 50000, 100000, 500000, 1000000 };
                                            for (int k = 0; k < maxlimit.Length; k++)
                                            {
                                                if (maxlimit[k] == 50000)
                                                {
                                                    for (int j = 0; j < deductible.Length; j++)
                                                    {
                                                        if (deductible[j] == 0)
                                                        {
                                                            imgrespose = SearchIMG(productCode[i], deductible[j], maxlimit[k], req, apptype, tokenimg);

                                                        }
                                                        else
                                                        {
                                                            if (imgrespose == 1)
                                                            {
                                                                GetDummyIMGValue(productCode[i], deductible[j], maxlimit[k], req, apptype);

                                                            }
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    for (int j = 0; j < deductible.Length; j++)
                                                    {
                                                        if (imgrespose == 1)
                                                        {
                                                            GetDummyIMGValue(productCode[i], deductible[j], maxlimit[k], req, apptype);
                                                            imgrespose = 0;
                                                        }

                                                    }

                                                }
                                            }
                                        }
                                    }
                                    else if (productCode[i] == "VIC")
                                    {
                                        if (req.TouristISOCODE == "US" || req.TouristISOCODE == "ASM" || req.TouristISOCODE == "GUM" || req.TouristISOCODE == "MNP" || req.TouristISOCODE == "PRI" || req.TouristISOCODE == "UMI" || req.TouristISOCODE == "VIR")
                                        {

                                            if (age >= 80)
                                            {
                                                string apptype = "0521A";
                                                int[] deductible = { 50, 100 };
                                                int maximumlimit = 10000;
                                                for (int j = 0; j < deductible.Length; j++)
                                                {
                                                    if (deductible[j] == 0)
                                                    {
                                                        imgrespose = SearchIMG(productCode[i], deductible[j], maximumlimit, req, apptype, tokenimg);

                                                    }
                                                    else
                                                    {
                                                        if (imgrespose == 1)
                                                        {
                                                            GetDummyIMGValue(productCode[i], deductible[j], maximumlimit, req, apptype);
                                                            imgrespose = 0;
                                                        }
                                                    }
                                                }

                                            }

                                            else
                                            {
                                                string[] apptype = { "0521A", "0521B", "0521C" };
                                                int[] deductible = { 0, 50, 100 };


                                                for (int k = 0; k < apptype.Length; k++)
                                                {
                                                    if (apptype[k] == "0521A")
                                                    {
                                                        int maxlimit = 25000;
                                                        for (int j = 0; j < deductible.Length; j++)
                                                        {
                                                            if (deductible[j] == 0)
                                                            {
                                                                imgrespose = SearchIMG(productCode[i], deductible[j], maxlimit, req, apptype[k], tokenimg);

                                                            }
                                                            else
                                                            {
                                                                if (imgrespose == 1)
                                                                {
                                                                    GetDummyIMGValue(productCode[i], deductible[j], maxlimit, req, apptype[k]);

                                                                }
                                                            }
                                                        }
                                                    }
                                                    else if (apptype[k] == "0521B")
                                                    {
                                                        int maxlimit = 50000;
                                                        for (int j = 0; j < deductible.Length; j++)
                                                        {
                                                            if (deductible[j] == 0)
                                                            {
                                                                SearchIMG(productCode[i], deductible[j], maxlimit, req, apptype[k], tokenimg);

                                                            }
                                                            else
                                                            {
                                                                if (imgrespose == 1)
                                                                {
                                                                    GetDummyIMGValue(productCode[i], deductible[j], maxlimit, req, apptype[k]);
                                                                }
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        int maxlimit = 100000;
                                                        for (int j = 0; j < deductible.Length; j++)
                                                        {
                                                            if (deductible[j] == 0)
                                                            {
                                                                SearchIMG(productCode[i], deductible[j], maxlimit, req, apptype[k], tokenimg);

                                                            }
                                                            else
                                                            {
                                                                if (imgrespose == 1)
                                                                {
                                                                    GetDummyIMGValue(productCode[i], deductible[j], maxlimit, req, apptype[k]);
                                                                    imgrespose = 0;
                                                                }

                                                            }
                                                        }
                                                    }

                                                }
                                            }
                                        }
                                    }
                                }

                            }

                        }
                    }
                }
                if (INFStatus == "1")
                {
                    var Result4 = Search(req);
                }
                if (DATE <= 364)
                {
                    if (req.ISOCODE == "AU" || req.ISOCODE == "CU" || req.ISOCODE == "IR" || req.ISOCODE == "KP" || req.ISOCODE == "CA")
                    {

                    }
                    else
                    {
                        if (VisitorStatus == "1")
                        {
                            if (age >= 80)
                            {
                                string PlanName = "A";
                                string[] Deductible = { "100", "200" };


                                for (int j = 0; j < Deductible.Length; j++)
                                {
                                    if (Deductible[j] == "100")
                                    {
                                        SearchVisitorSecure(Deductible[j], PlanName, req, age);
                                    }
                                    else
                                    {
                                        GetdummyVisitorsvalue(Deductible[j], PlanName, req, age);
                                    }
                                }





                            }
                            else if (age >= 70 && age <= 79)
                            {
                                string[] PlanName = { "A", "B" };
                                string[] Deductible = { "100", "200" };
                                for (int i = 0; i < PlanName.Length; i++)
                                {
                                    if (PlanName[i] == "A")
                                    {
                                        for (int j = 0; j < Deductible.Length; j++)
                                        {
                                            if (Deductible[j] == "100")
                                            {
                                                SearchVisitorSecure(Deductible[j], PlanName[i], req, age);
                                            }
                                            else
                                            {
                                                GetdummyVisitorsvalue(Deductible[j], PlanName[i], req, age);
                                            }
                                        }
                                    }
                                    else
                                    {


                                        for (int j = 0; j < Deductible.Length; j++)
                                        {

                                            GetdummyVisitorsvalue(Deductible[j], PlanName[i], req, age);


                                        }
                                    }

                                }
                            }
                            else
                            {
                                string[] PlanName = { "A", "B", "C", "D" };
                                string[] Deductible = { "0", "50", "100" };
                                for (int i = 0; i < PlanName.Length; i++)
                                {
                                    if (PlanName[i] == "A")
                                    {
                                        for (int j = 0; j < Deductible.Length; j++)
                                        {
                                            if (Deductible[j] == "0")
                                            {
                                                SearchVisitorSecure(Deductible[j], PlanName[i], req, age);
                                            }
                                            else
                                            {
                                                GetdummyVisitorsvalue(Deductible[j], PlanName[i], req, age);
                                            }
                                        }
                                    }
                                    else
                                    {


                                        for (int j = 0; j < Deductible.Length; j++)
                                        {

                                            GetdummyVisitorsvalue(Deductible[j], PlanName[i], req, age);


                                        }
                                    }

                                }
                            }
                        }
                    }
                }





            }

            var Result2 = lstQuatationResult.OrderBy(x => x.planMaximum).ThenBy(x => x.planDeductible);
            //var Result2 = lstQuatationResult.OrderBy(x => x.totalPremium);
            var Result3 = lstQuatationResult.OrderByDescending(x => x.planMaximum).ThenBy(x => x.planDeductible);
            var Result5 = Result3.OrderByDescending(x => x.planDeductible);

            var DistinctQuot = Result2.GroupBy(x => x.policyName).Select(x => x.FirstOrDefault()).ToList();
            var DistinctQuot2 = Result3.GroupBy(x => x.policyName).Select(x => x.FirstOrDefault()).ToList();
            //var OrderByDeductible = Result2.OrderBy(x => x.planDeductible).Where(x => x.planDeductible == 50).ToList();
            //var OrderBynotnull = OrderByDeductible.GroupBy(x => x.policyName).Select(x => x.FirstOrDefault()).Where(x => x.totalPremium != 0.0).ToList();
            var taborderbyasc = DistinctQuot.OrderBy(x => x.totalPremium).ToList();
            var taborderbydes = DistinctQuot.OrderByDescending(x => x.totalPremium).ToList();
            var DistinctQuotNo = Result2.GroupBy(x => x.policyName).Select(x => x.FirstOrDefault()).Where(x => x.prexded == "N").ToList();
            var DistinctQuotNo2 = Result3.GroupBy(x => x.policyName).Select(x => x.FirstOrDefault()).Where(x => x.prexded == "N").ToList();

            var DistinctQuotYes = Result2.GroupBy(x => x.policyName).Select(x => x.FirstOrDefault()).Where(x => x.prexded == "Y").ToList();
            var DistinctQuotYes2 = Result3.GroupBy(x => x.policyName).Select(x => x.FirstOrDefault()).Where(x => x.prexded == "Y").ToList();
            var INsuranceComp = Result2.GroupBy(x => x.INsuranceComp).Select(x => x.FirstOrDefault()).ToList();
            var INFGroupNo = DistinctQuotNo.GroupBy(x => x.policyName).Select(x => x.FirstOrDefault()).Where(x => x.INsuranceComp == "INF").ToList();
            var INFGroupYes = DistinctQuotYes.GroupBy(x => x.policyName).Select(x => x.FirstOrDefault()).Where(x => x.INsuranceComp == "INF").ToList();
            var AtlasGroup = Result2.GroupBy(x => x.policyName).Select(x => x.FirstOrDefault()).Where(x => x.INsuranceComp == "Trawick").ToList();
            var PLANTYPE = Result2.GroupBy(x => x.Plan_Type).Select(x => x.FirstOrDefault()).Where(x => x.Plan_Type != null).ToList();
            var ComprehensivePlan = Result2.GroupBy(x => x.Appname).Select(x => x.FirstOrDefault()).Where(x => x.Plan_Type == "Comprehensive").ToList();
            var LimitedPlan = Result2.GroupBy(x => x.Appname).Select(x => x.FirstOrDefault()).Where(x => x.Plan_Type == "LIMITED").ToList();
            var PreExisting = Result2.GroupBy(x => x.policyName).Select(x => x.FirstOrDefault()).Where(x => x.Pre_Existing == "YES").ToList();
            var PreExistingNo = Result2.GroupBy(x => x.policyName).Select(x => x.FirstOrDefault()).Where(x => x.Pre_Existing == "NO").ToList();


            var Comprehensivebyasc = ComprehensivePlan.OrderBy(x => x.totalPremium).ToList();
            var Comprehensivebydes = ComprehensivePlan.OrderByDescending(x => x.totalPremium).ToList();
            //check with 983
            var ComprehensivebyascPreexistingYES = ComprehensivePlan.OrderBy(x => x.totalPremium).Where(x => x.Pre_Existing == "YES").ToList();
            // check with 978
            var ComprehensivebydesPreexistingYES = ComprehensivePlan.OrderByDescending(x => x.totalPremium).Where(x => x.Pre_Existing == "YES").ToList();
            //check with 983
            var ComprehensivebyascPreexistingNO = ComprehensivePlan.OrderBy(x => x.totalPremium).Where(x => x.Pre_Existing == "NO").ToList();
            //check with 986
            var ComprehensivebydesPreexistingNO = ComprehensivePlan.OrderByDescending(x => x.totalPremium).Where(x => x.Pre_Existing == "NO").ToList();


            var LimitedPreExistingYEs = LimitedPlan.GroupBy(x => x.policyName).Select(x => x.FirstOrDefault()).Where(x => x.Pre_Existing == "YES").ToList();
            var LimitedPreExistingNo = LimitedPlan.GroupBy(x => x.policyName).Select(x => x.FirstOrDefault()).Where(x => x.Pre_Existing == "NO").ToList();
            var LimitedByDes = LimitedPlan.OrderByDescending(x => x.totalPremium).ToList();
            var LimitedByAsc = LimitedPlan.OrderBy(x => x.totalPremium).ToList();

            var ComprehensivePreExistingYes = ComprehensivePlan.GroupBy(x => x.policyName).Select(x => x.FirstOrDefault()).Where(x => x.Pre_Existing == "YES").ToList();
            var ComprehensivePreExistingNo = ComprehensivePlan.GroupBy(x => x.policyName).Select(x => x.FirstOrDefault()).Where(x => x.Pre_Existing == "NO").ToList();

            var PreexistingByLimitedByAsc = PreExisting.OrderBy(x => x.totalPremium).Where(x => x.Plan_Type == "LIMITED").ToList();
            //same
            //var PreexistingByComprehensiveByAcs = PreExisting.OrderBy(x => x.totalPremium).Where(x => x.Plan_Type == "Comprehensive").ToList();
            var PreexistingByLimitedbydes = PreExisting.OrderByDescending(x => x.totalPremium).Where(x => x.Plan_Type == "LIMITED").ToList();

            //same
            //var PreexistingByComprehensivebydes = PreExisting.OrderByDescending(x => x.totalPremium).Where(x => x.Plan_Type == "Comprehensive").ToList();

            var PreExistingNoByDes = PreExistingNo.OrderByDescending(x => x.totalPremium).ToList();
            var PreExistingNoByAcs = PreExistingNo.OrderBy(x => x.totalPremium).ToList();

            var PreExistingYesByDes = PreExisting.OrderByDescending(x => x.totalPremium).ToList();
            var PreExistingYesByAcs = PreExisting.OrderBy(x => x.totalPremium).ToList();

            var PreexistingNoByLimitedByAsc = PreExistingNo.OrderBy(x => x.totalPremium).Where(x => x.Plan_Type == "LIMITED").ToList();
            //same
            //var PreexistingNoByComprehensiveByAsc = PreExistingNo.OrderBy(x => x.totalPremium).Where(x => x.Plan_Type == "Comprehensive").ToList();
            var PreexistingNoByLimitedbydes = PreExistingNo.OrderByDescending(x => x.totalPremium).Where(x => x.Plan_Type == "LIMITED").ToList();

            //same
            //var PreexistingNoByComprehensivebydes = PreExistingNo.OrderByDescending(x => x.totalPremium).Where(x => x.Plan_Type == "Comprehensive").ToList();

            var Res = new
            {
                INsuranceComp,
                Result2,
                DistinctQuot,
                DistinctQuotNo,
                DistinctQuotYes,
                INFGroupNo,
                INFGroupYes,
                AtlasGroup,
                PLANTYPE,
                ComprehensivePlan,
                LimitedPlan,
                Result3,
                DistinctQuot2,
                DistinctQuotNo2,
                Result5,
                taborderbyasc,
                taborderbydes,
                PreExisting,
                PreExistingNo,
                Comprehensivebyasc,
                Comprehensivebydes,
                ComprehensivebyascPreexistingYES,
                ComprehensivebydesPreexistingYES,
                ComprehensivebyascPreexistingNO,
                ComprehensivebydesPreexistingNO,
                LimitedPreExistingYEs,
                LimitedPreExistingNo,
                LimitedByDes,
                LimitedByAsc,
                ComprehensivePreExistingYes,
                ComprehensivePreExistingNo,
                PreexistingByLimitedByAsc,
                PreexistingByLimitedbydes,
                PreExistingNoByDes,
                PreExistingNoByAcs,
                PreExistingYesByDes,
                PreExistingYesByAcs,
                PreexistingNoByLimitedByAsc,
                PreexistingNoByLimitedbydes
            };
            return Json(Res, JsonRequestBehavior.AllowGet);
        }

        public List<Models.INF_Quote> Search(QuotationModel quote)
        {
            List<Models.INF_Quote> finalResult = new List<Models.INF_Quote>();
            try
            {


                DateTime LeavingHome = DateTime.Parse(quote.LeavingHome);
                DateTime TillDate = DateTime.Parse(quote.TillDate);
                var DATE = (TillDate - LeavingHome).TotalDays;
                DATE = DATE + 1;
                string Domainname = ConfigurationManager.AppSettings["Domainname"];
                string AuthenticateINF = ConfigurationManager.AppSettings["AuthenticateINF"];
                HttpClient auth = new HttpClient();
                auth.BaseAddress = new Uri(Domainname);
                auth.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");

                var login = new Models.auth();
                string key = ConfigurationManager.AppSettings["username"];
                string value = ConfigurationManager.AppSettings["password"];
                login.username = key;
                login.password = value;

                var res = auth.PostAsJsonAsync(AuthenticateINF, login).Result;

                if (res.IsSuccessStatusCode)
                {
                    string AuthKey = "";
                    using (HttpContent content = res.Content)
                    {
                        Task<string> result = content.ReadAsStringAsync();
                        Dictionary<string, object> values = JsonConvert.DeserializeObject<Dictionary<string, object>>(result.Result);

                        AuthKey = values["token"].ToString();
                        HttpClient client = new HttpClient();
                        client.BaseAddress = new Uri(Domainname);
                        client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
                        client.DefaultRequestHeaders.Add("Authorization", AuthKey);

                        string dateofbirth = "";
                        if (quote.TravelersDOB == null || quote.TravelersDOB == "")
                        {
                            int age = quote.travelersage;
                            int getDOByear = Convert.ToInt32(DateTime.Now.Year) - age;
                            string getdate = DateTime.Now.Day.ToString();
                            string getmonth = DateTime.Now.Month.ToString();
                            if (getdate.Length > 1)
                            {

                            }
                            else
                            {
                                getdate = "0" + getdate;
                            }
                            if (getmonth.Length > 1)
                            {

                            }
                            else
                            {
                                getmonth = "0" + getmonth;
                            }
                            dateofbirth = getDOByear.ToString() + "-" + getmonth + "-" + getdate;

                        }
                        else
                        {
                            dateofbirth = Convert.ToDateTime(quote.TravelersDOB).ToString("yyyy-MM-dd");
                        }

                        string spousedateofbirth = "";
                        if (quote.SpouseDOB == null || quote.SpouseDOB == "")
                        {


                        }
                        else
                        {
                            spousedateofbirth = Convert.ToDateTime(quote.SpouseDOB).ToString("yyyy-MM-dd");
                        }


                        if (quote.spouseage == null)
                        {
                            quote.spouseage = "";
                        }
                        else if (quote.spouseage == "")
                        {

                        }
                        else
                        {
                            int age = Convert.ToInt32(quote.spouseage);
                            int getDOByear = Convert.ToInt32(DateTime.Now.Year) - age;
                            string getdate = DateTime.Now.Day.ToString();
                            string getmonth = DateTime.Now.Month.ToString();
                            if (getdate.Length > 1)
                            {

                            }
                            else
                            {
                                getdate = "0" + getdate;
                            }
                            if (getmonth.Length > 1)
                            {

                            }
                            else
                            {
                                getmonth = "0" + getmonth;
                            }
                            spousedateofbirth = getDOByear.ToString() + "-" + getmonth + "-" + getdate;
                        }
                        if (quote.child1age == null)
                        {
                            quote.child1age = "";
                        }
                        else if (quote.child1age == "")
                        {
                            quote.child1age = "";
                        }
                        else
                        {

                        }
                        if (quote.child3age == null)
                        {
                            quote.child3age = "";
                        }
                        if (quote.child2age == null)
                        {
                            quote.child2age = "";
                        }
                        if (quote.child4age == null)
                        {
                            quote.child4age = "";
                        }
                        if ((quote.spouseage == "") && (quote.child1age == "" && quote.child2age == "") && (quote.child3age == "" && quote.child4age == ""))
                        {
                            var user = new Models.INF_User();
                            user.quoteStartDate = Convert.ToDateTime(quote.LeavingHome).ToString("yyyy-MM-dd");
                            user.quoteEndDate = Convert.ToDateTime(quote.TillDate).ToString("yyyy-MM-dd");

                            user.coverageArea = quote.TouristISOCODE == "US" ? "USA/CANADA" : "Other/International";//"USA/CANADA";

                            var option = new Models.INF_User_Primary();
                            option.dob = dateofbirth;//"1965-12-02";                   
                            user.primary.Add(option);




                            var json = JsonConverterINF.Serialize(user);
                            string INFGETQuote = ConfigurationManager.AppSettings["INFGETQuote"];
                            var t = Task.Run(() => PostRequest(json, INFGETQuote, AuthKey, Domainname));
                            t.Wait();
                            string JsonResult = t.Result;
                            ViewBag.Message = JsonResult;

                            var ObjINF_uatResult = new Models.INF_QuatResult();

                            System.Web.Script.Serialization.JavaScriptSerializer jsSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();

                            finalResult = jsSerializer.Deserialize<List<Models.INF_Quote>>(JsonResult);

                            var Appname = "";

                            foreach (Models.INF_Quote item in finalResult)
                            {
                                if (item.policyName == "INF STANDARD")
                                {
                                    Appname = "INFS";
                                }
                                else if (item.policyName == "INF TRAVELUSA")
                                {
                                    Appname = "INFT";
                                }
                                else if (item.policyName == "INF ELITE")
                                {
                                    Appname = "INFE";
                                }
                                else if (item.policyName == "INF PREMIER")
                                {
                                    Appname = "INFP";
                                }
                                if (item.policyName == "INF STANDARD" || item.policyName == "INF TRAVELUSA" || item.policyName == "INF ELITE" || item.policyName == "INF PREMIER")
                                {


                                    List<QuotationModel> listitem = quotationalmanager.GetPlanDetails(Appname);
                                    //string premium= String.Format("{0:0.00}", item.totalPremium + 15.00);
                                    double premium = item.totalPremium + 15.00;
                                    AllQuatationResult AllQuatationresult = new AllQuatationResult();
                                    AllQuatationresult.PlanId = item.planId;

                                    AllQuatationresult.policyName = item.policyName;
                                    AllQuatationresult.planDeductible = item.planDeductible;
                                    AllQuatationresult.preExDeductible = item.preExDeductible;
                                    AllQuatationresult.preExCoverageAmount = item.preExCoverageAmount;
                                    AllQuatationresult.totalPremium = premium;
                                    AllQuatationresult.planMaximum = item.planMaximum;
                                    AllQuatationresult.INsuranceComp = "INF";
                                    AllQuatationresult.BuyNowLink = "";
                                    AllQuatationresult.policyType = item.policyType;
                                    AllQuatationresult.prexded = item.prexded;
                                    AllQuatationresult.Appname = Appname;
                                    if (listitem.Count > 0)
                                    {


                                        AllQuatationresult.Plan_Type = listitem[0].Plan_Type;
                                        AllQuatationresult.CANCELABLE = listitem[0].CANCELABLE;
                                        AllQuatationresult.Company_Name = listitem[0].Company_Name;
                                        AllQuatationresult.COUNTRY_RESTRICTIONS = listitem[0].COUNTRY_RESTRICTIONS;
                                        AllQuatationresult.COVID_19 = listitem[0].COVID_19;
                                        AllQuatationresult.IN_NETWORK = listitem[0].IN_NETWORK;
                                        AllQuatationresult.OUT_OF_NETWORK = listitem[0].OUT_OF_NETWORK;
                                        AllQuatationresult.Plan_Type = listitem[0].Plan_Type;
                                        AllQuatationresult.PRE_EXISITING_CONDITION = listitem[0].PRE_EXISITING_CONDITION;
                                        AllQuatationresult.RENEWABLE = listitem[0].RENEWABLE;
                                        AllQuatationresult.Pre_Existing = listitem[0].Pre_Existing;
                                    }
                                    AllQuatationresult.NOOFDAYS = DATE.ToString();
                                    lstQuatationResult.Add(AllQuatationresult);
                                }
                                else
                                {

                                }
                            }

                            return finalResult;
                        }
                        else if ((quote.child2age == "") && (quote.child1age != "") && (quote.spouseage != "") && (quote.child3age == "" && quote.child4age == ""))
                        {
                            var user = new Models.INF_Child1();
                            user.quoteStartDate = Convert.ToDateTime(quote.LeavingHome).ToString("yyyy-MM-dd");
                            user.quoteEndDate = Convert.ToDateTime(quote.TillDate).ToString("yyyy-MM-dd");

                            user.coverageArea = quote.TouristISOCODE == "US" ? "USA/CANADA" : "Other/International";//"USA/CANADA";

                            var option = new Models.INF_User_Primary();
                            option.dob = dateofbirth;//"1965-12-02";                   
                            user.primary.Add(option);
                            user.spouseDob = Convert.ToDateTime(spousedateofbirth).ToString("yyyy-MM-dd");
                            user.ageChild1 = quote.child1age;



                            var json = JsonConverterINF.Serialize(user);
                            string INFGETQuote = ConfigurationManager.AppSettings["INFGETQuote"];
                            var t = Task.Run(() => PostRequest(json, INFGETQuote, AuthKey, Domainname));
                            t.Wait();
                            string JsonResult = t.Result;
                            ViewBag.Message = JsonResult;

                            var ObjINF_uatResult = new Models.INF_QuatResult();

                            System.Web.Script.Serialization.JavaScriptSerializer jsSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();

                            finalResult = jsSerializer.Deserialize<List<Models.INF_Quote>>(JsonResult);

                            var Appname = "";

                            foreach (Models.INF_Quote item in finalResult)
                            {
                                if (item.policyName == "INF STANDARD")
                                {
                                    Appname = "INFS";
                                }
                                else if (item.policyName == "INF TRAVELUSA")
                                {
                                    Appname = "INFT";
                                }
                                else if (item.policyName == "INF ELITE")
                                {
                                    Appname = "INFE";
                                }
                                else if (item.policyName == "INF PREMIER")
                                {
                                    Appname = "INFP";
                                }
                                if (item.policyName == "INF STANDARD" || item.policyName == "INF TRAVELUSA" || item.policyName == "INF ELITE" || item.policyName == "INF PREMIER")
                                {


                                    List<QuotationModel> listitem = quotationalmanager.GetPlanDetails(Appname);
                                    //string premium = String.Format("{0:0.00}", item.totalPremium + 15.00);
                                    double premium = item.totalPremium + 15.00;
                                    AllQuatationResult AllQuatationresult = new AllQuatationResult();
                                    AllQuatationresult.PlanId = item.planId;
                                    AllQuatationresult.policyName = item.policyName;
                                    AllQuatationresult.planDeductible = item.planDeductible;
                                    AllQuatationresult.preExDeductible = item.preExDeductible;
                                    AllQuatationresult.preExCoverageAmount = item.preExCoverageAmount;
                                    AllQuatationresult.totalPremium = premium;
                                    AllQuatationresult.planMaximum = item.planMaximum;
                                    AllQuatationresult.INsuranceComp = "INF";
                                    AllQuatationresult.BuyNowLink = "";
                                    AllQuatationresult.policyType = item.policyType;
                                    AllQuatationresult.prexded = item.prexded;
                                    AllQuatationresult.Appname = Appname;
                                    if (listitem.Count > 0)
                                    {


                                        AllQuatationresult.Plan_Type = listitem[0].Plan_Type;
                                        AllQuatationresult.CANCELABLE = listitem[0].CANCELABLE;
                                        AllQuatationresult.Company_Name = listitem[0].Company_Name;
                                        AllQuatationresult.COUNTRY_RESTRICTIONS = listitem[0].COUNTRY_RESTRICTIONS;
                                        AllQuatationresult.COVID_19 = listitem[0].COVID_19;
                                        AllQuatationresult.IN_NETWORK = listitem[0].IN_NETWORK;
                                        AllQuatationresult.OUT_OF_NETWORK = listitem[0].OUT_OF_NETWORK;
                                        AllQuatationresult.Plan_Type = listitem[0].Plan_Type;
                                        AllQuatationresult.PRE_EXISITING_CONDITION = listitem[0].PRE_EXISITING_CONDITION;
                                        AllQuatationresult.RENEWABLE = listitem[0].RENEWABLE;
                                        AllQuatationresult.Pre_Existing = listitem[0].Pre_Existing;
                                    }
                                    AllQuatationresult.NOOFDAYS = DATE.ToString();
                                    lstQuatationResult.Add(AllQuatationresult);
                                }
                                else
                                {

                                }
                            }

                            return finalResult;
                        }
                        else if (quote.child2age != "" && (quote.child3age == "" && quote.child4age == ""))
                        {
                            var user = new Models.INF_Child2();
                            user.quoteStartDate = Convert.ToDateTime(quote.LeavingHome).ToString("yyyy-MM-dd");
                            user.quoteEndDate = Convert.ToDateTime(quote.TillDate).ToString("yyyy-MM-dd");

                            user.coverageArea = quote.TouristISOCODE == "US" ? "USA/CANADA" : "Other/International";//"USA/CANADA";

                            var option = new Models.INF_User_Primary();
                            option.dob = dateofbirth;//"1965-12-02";                   
                            user.primary.Add(option);
                            user.spouseDob = Convert.ToDateTime(spousedateofbirth).ToString("yyyy-MM-dd");
                            user.ageChild1 = quote.child1age;
                            user.ageChild2 = quote.child2age;



                            var json = JsonConverterINF.Serialize(user);
                            string INFGETQuote = ConfigurationManager.AppSettings["INFGETQuote"];
                            var t = Task.Run(() => PostRequest(json, INFGETQuote, AuthKey, Domainname));
                            t.Wait();
                            string JsonResult = t.Result;
                            ViewBag.Message = JsonResult;

                            var ObjINF_uatResult = new Models.INF_QuatResult();

                            System.Web.Script.Serialization.JavaScriptSerializer jsSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();

                            finalResult = jsSerializer.Deserialize<List<Models.INF_Quote>>(JsonResult);

                            var Appname = "";

                            foreach (Models.INF_Quote item in finalResult)
                            {
                                if (item.policyName == "INF STANDARD")
                                {
                                    Appname = "INFS";
                                }
                                else if (item.policyName == "INF TRAVELUSA")
                                {
                                    Appname = "INFT";
                                }
                                else if (item.policyName == "INF ELITE")
                                {
                                    Appname = "INFE";
                                }
                                else if (item.policyName == "INF PREMIER")
                                {
                                    Appname = "INFP";
                                }
                                if (item.policyName == "INF STANDARD" || item.policyName == "INF TRAVELUSA" || item.policyName == "INF ELITE" || item.policyName == "INF PREMIER")
                                {


                                    List<QuotationModel> listitem = quotationalmanager.GetPlanDetails(Appname);
                                    //string premium = String.Format("{0:0.00}", item.totalPremium + 15.00);
                                    double premium = item.totalPremium + 15.00;
                                    AllQuatationResult AllQuatationresult = new AllQuatationResult();
                                    AllQuatationresult.PlanId = item.planId;
                                    AllQuatationresult.policyName = item.policyName;
                                    AllQuatationresult.planDeductible = item.planDeductible;
                                    AllQuatationresult.preExDeductible = item.preExDeductible;
                                    AllQuatationresult.preExCoverageAmount = item.preExCoverageAmount;
                                    AllQuatationresult.totalPremium = premium;
                                    AllQuatationresult.planMaximum = item.planMaximum;
                                    AllQuatationresult.INsuranceComp = "INF";
                                    AllQuatationresult.BuyNowLink = "";
                                    AllQuatationresult.policyType = item.policyType;
                                    AllQuatationresult.prexded = item.prexded;
                                    AllQuatationresult.Appname = Appname;
                                    if (listitem.Count > 0)
                                    {


                                        AllQuatationresult.Plan_Type = listitem[0].Plan_Type;
                                        AllQuatationresult.CANCELABLE = listitem[0].CANCELABLE;
                                        AllQuatationresult.Company_Name = listitem[0].Company_Name;
                                        AllQuatationresult.COUNTRY_RESTRICTIONS = listitem[0].COUNTRY_RESTRICTIONS;
                                        AllQuatationresult.COVID_19 = listitem[0].COVID_19;
                                        AllQuatationresult.IN_NETWORK = listitem[0].IN_NETWORK;
                                        AllQuatationresult.OUT_OF_NETWORK = listitem[0].OUT_OF_NETWORK;
                                        AllQuatationresult.Plan_Type = listitem[0].Plan_Type;
                                        AllQuatationresult.PRE_EXISITING_CONDITION = listitem[0].PRE_EXISITING_CONDITION;
                                        AllQuatationresult.RENEWABLE = listitem[0].RENEWABLE;
                                        AllQuatationresult.Pre_Existing = listitem[0].Pre_Existing;
                                    }
                                    AllQuatationresult.NOOFDAYS = DATE.ToString();
                                    lstQuatationResult.Add(AllQuatationresult);
                                }
                                else
                                {

                                }
                            }

                            return finalResult;
                        }
                        else if (quote.child2age != "" && (quote.child3age != "" && quote.child4age == ""))
                        {
                            var user = new Models.INF_Child3();
                            user.quoteStartDate = Convert.ToDateTime(quote.LeavingHome).ToString("yyyy-MM-dd");
                            user.quoteEndDate = Convert.ToDateTime(quote.TillDate).ToString("yyyy-MM-dd");

                            user.coverageArea = quote.TouristISOCODE == "US" ? "USA/CANADA" : "Other/International";//"USA/CANADA";

                            var option = new Models.INF_User_Primary();
                            option.dob = dateofbirth;//"1965-12-02";                   
                            user.primary.Add(option);
                            user.spouseDob = Convert.ToDateTime(spousedateofbirth).ToString("yyyy-MM-dd");
                            user.ageChild1 = quote.child1age;
                            user.ageChild2 = quote.child2age;
                            user.ageChild3 = quote.child3age;



                            var json = JsonConverterINF.Serialize(user);
                            string INFGETQuote = ConfigurationManager.AppSettings["INFGETQuote"];
                            var t = Task.Run(() => PostRequest(json, INFGETQuote, AuthKey, Domainname));
                            t.Wait();
                            string JsonResult = t.Result;
                            ViewBag.Message = JsonResult;

                            var ObjINF_uatResult = new Models.INF_QuatResult();

                            System.Web.Script.Serialization.JavaScriptSerializer jsSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();

                            finalResult = jsSerializer.Deserialize<List<Models.INF_Quote>>(JsonResult);

                            var Appname = "";

                            foreach (Models.INF_Quote item in finalResult)
                            {
                                if (item.policyName == "INF STANDARD")
                                {
                                    Appname = "INFS";
                                }
                                else if (item.policyName == "INF TRAVELUSA")
                                {
                                    Appname = "INFT";
                                }
                                else if (item.policyName == "INF ELITE")
                                {
                                    Appname = "INFE";
                                }
                                else if (item.policyName == "INF PREMIER")
                                {
                                    Appname = "INFP";
                                }
                                if (item.policyName == "INF STANDARD" || item.policyName == "INF TRAVELUSA" || item.policyName == "INF ELITE" || item.policyName == "INF PREMIER")
                                {


                                    List<QuotationModel> listitem = quotationalmanager.GetPlanDetails(Appname);
                                    //string premium = String.Format("{0:0.00}", item.totalPremium + 15.00);
                                    double premium = item.totalPremium + 15.00;
                                    AllQuatationResult AllQuatationresult = new AllQuatationResult();
                                    AllQuatationresult.PlanId = item.planId;
                                    AllQuatationresult.policyName = item.policyName;
                                    AllQuatationresult.planDeductible = item.planDeductible;
                                    AllQuatationresult.preExDeductible = item.preExDeductible;
                                    AllQuatationresult.preExCoverageAmount = item.preExCoverageAmount;
                                    AllQuatationresult.totalPremium = premium;

                                    AllQuatationresult.planMaximum = item.planMaximum;
                                    AllQuatationresult.INsuranceComp = "INF";
                                    AllQuatationresult.BuyNowLink = "";
                                    AllQuatationresult.policyType = item.policyType;
                                    AllQuatationresult.prexded = item.prexded;
                                    AllQuatationresult.Appname = Appname;
                                    if (listitem.Count > 0)
                                    {


                                        AllQuatationresult.Plan_Type = listitem[0].Plan_Type;
                                        AllQuatationresult.CANCELABLE = listitem[0].CANCELABLE;
                                        AllQuatationresult.Company_Name = listitem[0].Company_Name;
                                        AllQuatationresult.COUNTRY_RESTRICTIONS = listitem[0].COUNTRY_RESTRICTIONS;
                                        AllQuatationresult.COVID_19 = listitem[0].COVID_19;
                                        AllQuatationresult.IN_NETWORK = listitem[0].IN_NETWORK;
                                        AllQuatationresult.OUT_OF_NETWORK = listitem[0].OUT_OF_NETWORK;
                                        AllQuatationresult.Plan_Type = listitem[0].Plan_Type;
                                        AllQuatationresult.PRE_EXISITING_CONDITION = listitem[0].PRE_EXISITING_CONDITION;
                                        AllQuatationresult.RENEWABLE = listitem[0].RENEWABLE;
                                        AllQuatationresult.Pre_Existing = listitem[0].Pre_Existing;
                                    }
                                    AllQuatationresult.NOOFDAYS = DATE.ToString();
                                    lstQuatationResult.Add(AllQuatationresult);
                                }
                                else
                                {

                                }
                            }

                            return finalResult;
                        }
                        else if (quote.child2age != "" && (quote.child3age != "" && quote.child4age != ""))
                        {
                            var user = new Models.INF_Child4();
                            user.quoteStartDate = Convert.ToDateTime(quote.LeavingHome).ToString("yyyy-MM-dd");
                            user.quoteEndDate = Convert.ToDateTime(quote.TillDate).ToString("yyyy-MM-dd");

                            user.coverageArea = quote.TouristISOCODE == "US" ? "USA/CANADA" : "Other/International";//"USA/CANADA";

                            var option = new Models.INF_User_Primary();
                            option.dob = dateofbirth;//"1965-12-02";                   
                            user.primary.Add(option);
                            user.spouseDob = Convert.ToDateTime(spousedateofbirth).ToString("yyyy-MM-dd");
                            user.ageChild1 = quote.child1age;
                            user.ageChild2 = quote.child2age;
                            user.ageChild3 = quote.child3age;
                            user.ageChild4 = quote.child4age;



                            var json = JsonConverterINF.Serialize(user);
                            string INFGETQuote = ConfigurationManager.AppSettings["INFGETQuote"];
                            var t = Task.Run(() => PostRequest(json, INFGETQuote, AuthKey, Domainname));
                            t.Wait();
                            string JsonResult = t.Result;
                            ViewBag.Message = JsonResult;

                            var ObjINF_uatResult = new Models.INF_QuatResult();

                            System.Web.Script.Serialization.JavaScriptSerializer jsSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();

                            finalResult = jsSerializer.Deserialize<List<Models.INF_Quote>>(JsonResult);

                            var Appname = "";

                            foreach (Models.INF_Quote item in finalResult)
                            {
                                if (item.policyName == "INF STANDARD")
                                {
                                    Appname = "INFS";
                                }
                                else if (item.policyName == "INF TRAVELUSA")
                                {
                                    Appname = "INFT";
                                }
                                else if (item.policyName == "INF ELITE")
                                {
                                    Appname = "INFE";
                                }
                                else if (item.policyName == "INF PREMIER")
                                {
                                    Appname = "INFP";
                                }
                                if (item.policyName == "INF STANDARD" || item.policyName == "INF TRAVELUSA" || item.policyName == "INF ELITE" || item.policyName == "INF PREMIER")
                                {


                                    List<QuotationModel> listitem = quotationalmanager.GetPlanDetails(Appname);
                                    //string premium = String.Format("{0:0.00}", item.totalPremium + 15.00);
                                    double premium = item.totalPremium + 15.00;
                                    AllQuatationResult AllQuatationresult = new AllQuatationResult();
                                    AllQuatationresult.PlanId = item.planId;
                                    AllQuatationresult.policyName = item.policyName;
                                    AllQuatationresult.planDeductible = item.planDeductible;
                                    AllQuatationresult.preExDeductible = item.preExDeductible;
                                    AllQuatationresult.preExCoverageAmount = item.preExCoverageAmount;
                                    AllQuatationresult.totalPremium = premium;
                                    AllQuatationresult.planMaximum = item.planMaximum;
                                    AllQuatationresult.INsuranceComp = "INF";
                                    AllQuatationresult.BuyNowLink = "";
                                    AllQuatationresult.policyType = item.policyType;
                                    AllQuatationresult.prexded = item.prexded;
                                    AllQuatationresult.Appname = Appname;
                                    if (listitem.Count > 0)
                                    {


                                        AllQuatationresult.Plan_Type = listitem[0].Plan_Type;
                                        AllQuatationresult.CANCELABLE = listitem[0].CANCELABLE;
                                        AllQuatationresult.Company_Name = listitem[0].Company_Name;
                                        AllQuatationresult.COUNTRY_RESTRICTIONS = listitem[0].COUNTRY_RESTRICTIONS;
                                        AllQuatationresult.COVID_19 = listitem[0].COVID_19;
                                        AllQuatationresult.IN_NETWORK = listitem[0].IN_NETWORK;
                                        AllQuatationresult.OUT_OF_NETWORK = listitem[0].OUT_OF_NETWORK;
                                        AllQuatationresult.Plan_Type = listitem[0].Plan_Type;
                                        AllQuatationresult.PRE_EXISITING_CONDITION = listitem[0].PRE_EXISITING_CONDITION;
                                        AllQuatationresult.RENEWABLE = listitem[0].RENEWABLE;
                                        AllQuatationresult.Pre_Existing = listitem[0].Pre_Existing;
                                    }
                                    AllQuatationresult.NOOFDAYS = DATE.ToString();
                                    lstQuatationResult.Add(AllQuatationresult);
                                }
                                else
                                {

                                }
                            }

                            return finalResult;
                        }

                        //pc1
                        else if ((quote.child2age == "") && (quote.child1age != "") && (quote.spouseage == "") && (quote.child3age == "" && quote.child4age == ""))
                        {
                            var user = new Models.INF_Child1_Primary();
                            user.quoteStartDate = Convert.ToDateTime(quote.LeavingHome).ToString("yyyy-MM-dd");
                            user.quoteEndDate = Convert.ToDateTime(quote.TillDate).ToString("yyyy-MM-dd");

                            user.coverageArea = quote.TouristISOCODE == "US" ? "USA/CANADA" : "Other/International";//"USA/CANADA";

                            var option = new Models.INF_User_Primary();
                            option.dob = dateofbirth;//"1965-12-02";                   
                            user.primary.Add(option);

                            user.ageChild1 = quote.child1age;



                            var json = JsonConverterINF.Serialize(user);
                            string INFGETQuote = ConfigurationManager.AppSettings["INFGETQuote"];
                            var t = Task.Run(() => PostRequest(json, INFGETQuote, AuthKey, Domainname));
                            t.Wait();
                            string JsonResult = t.Result;
                            ViewBag.Message = JsonResult;

                            var ObjINF_uatResult = new Models.INF_QuatResult();

                            System.Web.Script.Serialization.JavaScriptSerializer jsSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();

                            finalResult = jsSerializer.Deserialize<List<Models.INF_Quote>>(JsonResult);

                            var Appname = "";

                            foreach (Models.INF_Quote item in finalResult)
                            {
                                if (item.policyName == "INF STANDARD")
                                {
                                    Appname = "INFS";
                                }
                                else if (item.policyName == "INF TRAVELUSA")
                                {
                                    Appname = "INFT";
                                }
                                else if (item.policyName == "INF ELITE")
                                {
                                    Appname = "INFE";
                                }
                                else if (item.policyName == "INF PREMIER")
                                {
                                    Appname = "INFP";
                                }
                                if (item.policyName == "INF STANDARD" || item.policyName == "INF TRAVELUSA" || item.policyName == "INF ELITE" || item.policyName == "INF PREMIER")
                                {


                                    List<QuotationModel> listitem = quotationalmanager.GetPlanDetails(Appname);
                                    //string premium = String.Format("{0:0.00}", item.totalPremium + 15.00);
                                    double premium = item.totalPremium + 15.00;
                                    AllQuatationResult AllQuatationresult = new AllQuatationResult();
                                    AllQuatationresult.PlanId = item.planId;
                                    AllQuatationresult.policyName = item.policyName;
                                    AllQuatationresult.planDeductible = item.planDeductible;
                                    AllQuatationresult.preExDeductible = item.preExDeductible;
                                    AllQuatationresult.preExCoverageAmount = item.preExCoverageAmount;
                                    AllQuatationresult.totalPremium = premium;
                                    AllQuatationresult.planMaximum = item.planMaximum;
                                    AllQuatationresult.INsuranceComp = "INF";
                                    AllQuatationresult.BuyNowLink = "";
                                    AllQuatationresult.policyType = item.policyType;
                                    AllQuatationresult.prexded = item.prexded;
                                    AllQuatationresult.Appname = Appname;
                                    if (listitem.Count > 0)
                                    {


                                        AllQuatationresult.Plan_Type = listitem[0].Plan_Type;
                                        AllQuatationresult.CANCELABLE = listitem[0].CANCELABLE;
                                        AllQuatationresult.Company_Name = listitem[0].Company_Name;
                                        AllQuatationresult.COUNTRY_RESTRICTIONS = listitem[0].COUNTRY_RESTRICTIONS;
                                        AllQuatationresult.COVID_19 = listitem[0].COVID_19;
                                        AllQuatationresult.IN_NETWORK = listitem[0].IN_NETWORK;
                                        AllQuatationresult.OUT_OF_NETWORK = listitem[0].OUT_OF_NETWORK;
                                        AllQuatationresult.Plan_Type = listitem[0].Plan_Type;
                                        AllQuatationresult.PRE_EXISITING_CONDITION = listitem[0].PRE_EXISITING_CONDITION;
                                        AllQuatationresult.RENEWABLE = listitem[0].RENEWABLE;
                                        AllQuatationresult.Pre_Existing = listitem[0].Pre_Existing;
                                    }
                                    AllQuatationresult.NOOFDAYS = DATE.ToString();
                                    lstQuatationResult.Add(AllQuatationresult);
                                }
                                else
                                {

                                }
                            }

                            return finalResult;
                        }
                        else if (quote.child2age != "" && (quote.child3age == "" && quote.child4age == "") && (quote.spouseage == "") && (quote.child1age == ""))
                        {
                            var user = new Models.INF_Child2_Primary();
                            user.quoteStartDate = Convert.ToDateTime(quote.LeavingHome).ToString("yyyy-MM-dd");
                            user.quoteEndDate = Convert.ToDateTime(quote.TillDate).ToString("yyyy-MM-dd");

                            user.coverageArea = quote.TouristISOCODE == "US" ? "USA/CANADA" : "Other/International";//"USA/CANADA";

                            var option = new Models.INF_User_Primary();
                            option.dob = dateofbirth;//"1965-12-02";                   
                            user.primary.Add(option);

                            user.ageChild2 = quote.child2age;



                            var json = JsonConverterINF.Serialize(user);
                            string INFGETQuote = ConfigurationManager.AppSettings["INFGETQuote"];
                            var t = Task.Run(() => PostRequest(json, INFGETQuote, AuthKey, Domainname));
                            t.Wait();
                            string JsonResult = t.Result;
                            ViewBag.Message = JsonResult;

                            var ObjINF_uatResult = new Models.INF_QuatResult();

                            System.Web.Script.Serialization.JavaScriptSerializer jsSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();

                            finalResult = jsSerializer.Deserialize<List<Models.INF_Quote>>(JsonResult);

                            var Appname = "";

                            foreach (Models.INF_Quote item in finalResult)
                            {
                                if (item.policyName == "INF STANDARD")
                                {
                                    Appname = "INFS";
                                }
                                else if (item.policyName == "INF TRAVELUSA")
                                {
                                    Appname = "INFT";
                                }
                                else if (item.policyName == "INF ELITE")
                                {
                                    Appname = "INFE";
                                }
                                else if (item.policyName == "INF PREMIER")
                                {
                                    Appname = "INFP";
                                }
                                if (item.policyName == "INF STANDARD" || item.policyName == "INF TRAVELUSA" || item.policyName == "INF ELITE" || item.policyName == "INF PREMIER")
                                {


                                    List<QuotationModel> listitem = quotationalmanager.GetPlanDetails(Appname);
                                    //string premium = String.Format("{0:0.00}", item.totalPremium + 15.00);
                                    double premium = item.totalPremium + 15.00;
                                    AllQuatationResult AllQuatationresult = new AllQuatationResult();
                                    AllQuatationresult.PlanId = item.planId;
                                    AllQuatationresult.policyName = item.policyName;
                                    AllQuatationresult.planDeductible = item.planDeductible;
                                    AllQuatationresult.preExDeductible = item.preExDeductible;
                                    AllQuatationresult.preExCoverageAmount = item.preExCoverageAmount;
                                    AllQuatationresult.totalPremium = premium;
                                    AllQuatationresult.planMaximum = item.planMaximum;
                                    AllQuatationresult.INsuranceComp = "INF";
                                    AllQuatationresult.BuyNowLink = "";
                                    AllQuatationresult.policyType = item.policyType;
                                    AllQuatationresult.prexded = item.prexded;
                                    AllQuatationresult.Appname = Appname;
                                    if (listitem.Count > 0)
                                    {


                                        AllQuatationresult.Plan_Type = listitem[0].Plan_Type;
                                        AllQuatationresult.CANCELABLE = listitem[0].CANCELABLE;
                                        AllQuatationresult.Company_Name = listitem[0].Company_Name;
                                        AllQuatationresult.COUNTRY_RESTRICTIONS = listitem[0].COUNTRY_RESTRICTIONS;
                                        AllQuatationresult.COVID_19 = listitem[0].COVID_19;
                                        AllQuatationresult.IN_NETWORK = listitem[0].IN_NETWORK;
                                        AllQuatationresult.OUT_OF_NETWORK = listitem[0].OUT_OF_NETWORK;
                                        AllQuatationresult.Plan_Type = listitem[0].Plan_Type;
                                        AllQuatationresult.PRE_EXISITING_CONDITION = listitem[0].PRE_EXISITING_CONDITION;
                                        AllQuatationresult.RENEWABLE = listitem[0].RENEWABLE;
                                        AllQuatationresult.Pre_Existing = listitem[0].Pre_Existing;
                                    }
                                    AllQuatationresult.NOOFDAYS = DATE.ToString();
                                    lstQuatationResult.Add(AllQuatationresult);
                                }
                                else
                                {

                                }
                            }

                            return finalResult;
                        }
                        else if (quote.child2age == "" && (quote.child3age != "" && quote.child4age == "") && (quote.spouseage == "") && (quote.child1age == ""))
                        {
                            var user = new Models.INF_Child3_Primary();
                            user.quoteStartDate = Convert.ToDateTime(quote.LeavingHome).ToString("yyyy-MM-dd");
                            user.quoteEndDate = Convert.ToDateTime(quote.TillDate).ToString("yyyy-MM-dd");

                            user.coverageArea = quote.TouristISOCODE == "US" ? "USA/CANADA" : "Other/International";//"USA/CANADA";

                            var option = new Models.INF_User_Primary();
                            option.dob = dateofbirth;//"1965-12-02";                   
                            user.primary.Add(option);

                            user.ageChild3 = quote.child3age;



                            var json = JsonConverterINF.Serialize(user);
                            string INFGETQuote = ConfigurationManager.AppSettings["INFGETQuote"];
                            var t = Task.Run(() => PostRequest(json, INFGETQuote, AuthKey, Domainname));
                            t.Wait();
                            string JsonResult = t.Result;
                            ViewBag.Message = JsonResult;

                            var ObjINF_uatResult = new Models.INF_QuatResult();

                            System.Web.Script.Serialization.JavaScriptSerializer jsSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();

                            finalResult = jsSerializer.Deserialize<List<Models.INF_Quote>>(JsonResult);

                            var Appname = "";

                            foreach (Models.INF_Quote item in finalResult)
                            {
                                if (item.policyName == "INF STANDARD")
                                {
                                    Appname = "INFS";
                                }
                                else if (item.policyName == "INF TRAVELUSA")
                                {
                                    Appname = "INFT";
                                }
                                else if (item.policyName == "INF ELITE")
                                {
                                    Appname = "INFE";
                                }
                                else if (item.policyName == "INF PREMIER")
                                {
                                    Appname = "INFP";
                                }
                                if (item.policyName == "INF STANDARD" || item.policyName == "INF TRAVELUSA" || item.policyName == "INF ELITE" || item.policyName == "INF PREMIER")
                                {


                                    List<QuotationModel> listitem = quotationalmanager.GetPlanDetails(Appname);
                                    //string premium = String.Format("{0:0.00}", item.totalPremium + 15.00);
                                    double premium = item.totalPremium + 15.00;
                                    AllQuatationResult AllQuatationresult = new AllQuatationResult();
                                    AllQuatationresult.PlanId = item.planId;
                                    AllQuatationresult.policyName = item.policyName;
                                    AllQuatationresult.planDeductible = item.planDeductible;
                                    AllQuatationresult.preExDeductible = item.preExDeductible;
                                    AllQuatationresult.preExCoverageAmount = item.preExCoverageAmount;
                                    AllQuatationresult.totalPremium = premium;

                                    AllQuatationresult.planMaximum = item.planMaximum;
                                    AllQuatationresult.INsuranceComp = "INF";
                                    AllQuatationresult.BuyNowLink = "";
                                    AllQuatationresult.policyType = item.policyType;
                                    AllQuatationresult.prexded = item.prexded;
                                    AllQuatationresult.Appname = Appname;
                                    if (listitem.Count > 0)
                                    {


                                        AllQuatationresult.Plan_Type = listitem[0].Plan_Type;
                                        AllQuatationresult.CANCELABLE = listitem[0].CANCELABLE;
                                        AllQuatationresult.Company_Name = listitem[0].Company_Name;
                                        AllQuatationresult.COUNTRY_RESTRICTIONS = listitem[0].COUNTRY_RESTRICTIONS;
                                        AllQuatationresult.COVID_19 = listitem[0].COVID_19;
                                        AllQuatationresult.IN_NETWORK = listitem[0].IN_NETWORK;
                                        AllQuatationresult.OUT_OF_NETWORK = listitem[0].OUT_OF_NETWORK;
                                        AllQuatationresult.Plan_Type = listitem[0].Plan_Type;
                                        AllQuatationresult.PRE_EXISITING_CONDITION = listitem[0].PRE_EXISITING_CONDITION;
                                        AllQuatationresult.RENEWABLE = listitem[0].RENEWABLE;
                                        AllQuatationresult.Pre_Existing = listitem[0].Pre_Existing;
                                    }
                                    AllQuatationresult.NOOFDAYS = DATE.ToString();
                                    lstQuatationResult.Add(AllQuatationresult);
                                }
                                else
                                {

                                }
                            }

                            return finalResult;
                        }
                        else if (quote.child2age == "" && (quote.child3age == "" && quote.child4age != "") && (quote.spouseage == "") && (quote.child1age == ""))
                        {
                            var user = new Models.INF_Child4_Primary();
                            user.quoteStartDate = Convert.ToDateTime(quote.LeavingHome).ToString("yyyy-MM-dd");
                            user.quoteEndDate = Convert.ToDateTime(quote.TillDate).ToString("yyyy-MM-dd");

                            user.coverageArea = quote.TouristISOCODE == "US" ? "USA/CANADA" : "Other/International";//"USA/CANADA";

                            var option = new Models.INF_User_Primary();
                            option.dob = dateofbirth;//"1965-12-02";                   
                            user.primary.Add(option);

                            user.ageChild4 = quote.child4age;



                            var json = JsonConverterINF.Serialize(user);
                            string INFGETQuote = ConfigurationManager.AppSettings["INFGETQuote"];
                            var t = Task.Run(() => PostRequest(json, INFGETQuote, AuthKey, Domainname));
                            t.Wait();
                            string JsonResult = t.Result;
                            ViewBag.Message = JsonResult;

                            var ObjINF_uatResult = new Models.INF_QuatResult();

                            System.Web.Script.Serialization.JavaScriptSerializer jsSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();

                            finalResult = jsSerializer.Deserialize<List<Models.INF_Quote>>(JsonResult);

                            var Appname = "";

                            foreach (Models.INF_Quote item in finalResult)
                            {
                                if (item.policyName == "INF STANDARD")
                                {
                                    Appname = "INFS";
                                }
                                else if (item.policyName == "INF TRAVELUSA")
                                {
                                    Appname = "INFT";
                                }
                                else if (item.policyName == "INF ELITE")
                                {
                                    Appname = "INFE";
                                }
                                else if (item.policyName == "INF PREMIER")
                                {
                                    Appname = "INFP";
                                }
                                if (item.policyName == "INF STANDARD" || item.policyName == "INF TRAVELUSA" || item.policyName == "INF ELITE" || item.policyName == "INF PREMIER")
                                {


                                    List<QuotationModel> listitem = quotationalmanager.GetPlanDetails(Appname);
                                    //string premium = String.Format("{0:0.00}", item.totalPremium + 15.00);
                                    double premium = item.totalPremium + 15.00;
                                    AllQuatationResult AllQuatationresult = new AllQuatationResult();
                                    AllQuatationresult.PlanId = item.planId;
                                    AllQuatationresult.policyName = item.policyName;
                                    AllQuatationresult.planDeductible = item.planDeductible;
                                    AllQuatationresult.preExDeductible = item.preExDeductible;
                                    AllQuatationresult.preExCoverageAmount = item.preExCoverageAmount;
                                    AllQuatationresult.totalPremium = premium;
                                    AllQuatationresult.planMaximum = item.planMaximum;
                                    AllQuatationresult.INsuranceComp = "INF";
                                    AllQuatationresult.BuyNowLink = "";
                                    AllQuatationresult.policyType = item.policyType;
                                    AllQuatationresult.prexded = item.prexded;
                                    AllQuatationresult.Appname = Appname;
                                    if (listitem.Count > 0)
                                    {


                                        AllQuatationresult.Plan_Type = listitem[0].Plan_Type;
                                        AllQuatationresult.CANCELABLE = listitem[0].CANCELABLE;
                                        AllQuatationresult.Company_Name = listitem[0].Company_Name;
                                        AllQuatationresult.COUNTRY_RESTRICTIONS = listitem[0].COUNTRY_RESTRICTIONS;
                                        AllQuatationresult.COVID_19 = listitem[0].COVID_19;
                                        AllQuatationresult.IN_NETWORK = listitem[0].IN_NETWORK;
                                        AllQuatationresult.OUT_OF_NETWORK = listitem[0].OUT_OF_NETWORK;
                                        AllQuatationresult.Plan_Type = listitem[0].Plan_Type;
                                        AllQuatationresult.PRE_EXISITING_CONDITION = listitem[0].PRE_EXISITING_CONDITION;
                                        AllQuatationresult.RENEWABLE = listitem[0].RENEWABLE;
                                        AllQuatationresult.Pre_Existing = listitem[0].Pre_Existing;
                                    }
                                    AllQuatationresult.NOOFDAYS = DATE.ToString();
                                    lstQuatationResult.Add(AllQuatationresult);
                                }
                                else
                                {

                                }
                            }

                            return finalResult;
                        }

                        //pc1c2
                        else if (quote.child2age != "" && (quote.child3age == "" && quote.child4age == "") && (quote.spouseage == "") && (quote.child1age != ""))
                        {
                            var user = new Models.INF_Child2_Child1();
                            user.quoteStartDate = Convert.ToDateTime(quote.LeavingHome).ToString("yyyy-MM-dd");
                            user.quoteEndDate = Convert.ToDateTime(quote.TillDate).ToString("yyyy-MM-dd");

                            user.coverageArea = quote.TouristISOCODE == "US" ? "USA/CANADA" : "Other/International";//"USA/CANADA";

                            var option = new Models.INF_User_Primary();
                            option.dob = dateofbirth;//"1965-12-02";                   
                            user.primary.Add(option);

                            user.ageChild1 = quote.child1age;
                            user.ageChild2 = quote.child2age;



                            var json = JsonConverterINF.Serialize(user);
                            string INFGETQuote = ConfigurationManager.AppSettings["INFGETQuote"];
                            var t = Task.Run(() => PostRequest(json, INFGETQuote, AuthKey, Domainname));
                            t.Wait();
                            string JsonResult = t.Result;
                            ViewBag.Message = JsonResult;

                            var ObjINF_uatResult = new Models.INF_QuatResult();

                            System.Web.Script.Serialization.JavaScriptSerializer jsSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();

                            finalResult = jsSerializer.Deserialize<List<Models.INF_Quote>>(JsonResult);

                            var Appname = "";

                            foreach (Models.INF_Quote item in finalResult)
                            {
                                if (item.policyName == "INF STANDARD")
                                {
                                    Appname = "INFS";
                                }
                                else if (item.policyName == "INF TRAVELUSA")
                                {
                                    Appname = "INFT";
                                }
                                else if (item.policyName == "INF ELITE")
                                {
                                    Appname = "INFE";
                                }
                                else if (item.policyName == "INF PREMIER")
                                {
                                    Appname = "INFP";
                                }
                                if (item.policyName == "INF STANDARD" || item.policyName == "INF TRAVELUSA" || item.policyName == "INF ELITE" || item.policyName == "INF PREMIER")
                                {


                                    List<QuotationModel> listitem = quotationalmanager.GetPlanDetails(Appname);
                                    //string premium = String.Format("{0:0.00}", item.totalPremium + 15.00);
                                    double premium = item.totalPremium + 15.00;
                                    AllQuatationResult AllQuatationresult = new AllQuatationResult();
                                    AllQuatationresult.PlanId = item.planId;
                                    AllQuatationresult.policyName = item.policyName;
                                    AllQuatationresult.planDeductible = item.planDeductible;
                                    AllQuatationresult.preExDeductible = item.preExDeductible;
                                    AllQuatationresult.preExCoverageAmount = item.preExCoverageAmount;
                                    AllQuatationresult.totalPremium = premium;
                                    AllQuatationresult.planMaximum = item.planMaximum;
                                    AllQuatationresult.INsuranceComp = "INF";
                                    AllQuatationresult.BuyNowLink = "";
                                    AllQuatationresult.policyType = item.policyType;
                                    AllQuatationresult.prexded = item.prexded;
                                    AllQuatationresult.Appname = Appname;
                                    if (listitem.Count > 0)
                                    {


                                        AllQuatationresult.Plan_Type = listitem[0].Plan_Type;
                                        AllQuatationresult.CANCELABLE = listitem[0].CANCELABLE;
                                        AllQuatationresult.Company_Name = listitem[0].Company_Name;
                                        AllQuatationresult.COUNTRY_RESTRICTIONS = listitem[0].COUNTRY_RESTRICTIONS;
                                        AllQuatationresult.COVID_19 = listitem[0].COVID_19;
                                        AllQuatationresult.IN_NETWORK = listitem[0].IN_NETWORK;
                                        AllQuatationresult.OUT_OF_NETWORK = listitem[0].OUT_OF_NETWORK;
                                        AllQuatationresult.Plan_Type = listitem[0].Plan_Type;
                                        AllQuatationresult.PRE_EXISITING_CONDITION = listitem[0].PRE_EXISITING_CONDITION;
                                        AllQuatationresult.RENEWABLE = listitem[0].RENEWABLE;
                                        AllQuatationresult.Pre_Existing = listitem[0].Pre_Existing;
                                    }
                                    AllQuatationresult.NOOFDAYS = DATE.ToString();
                                    lstQuatationResult.Add(AllQuatationresult);
                                }
                                else
                                {

                                }
                            }

                            return finalResult;
                        }
                        else if (quote.child2age != "" && (quote.child3age != "" && quote.child4age == "") && (quote.child1age == "") && (quote.spouseage == ""))
                        {
                            var user = new Models.INF_Child3_Child2();
                            user.quoteStartDate = Convert.ToDateTime(quote.LeavingHome).ToString("yyyy-MM-dd");
                            user.quoteEndDate = Convert.ToDateTime(quote.TillDate).ToString("yyyy-MM-dd");

                            user.coverageArea = quote.TouristISOCODE == "US" ? "USA/CANADA" : "Other/International";//"USA/CANADA";

                            var option = new Models.INF_User_Primary();
                            option.dob = dateofbirth;//"1965-12-02";                   
                            user.primary.Add(option);

                            user.ageChild2 = quote.child2age;
                            user.ageChild3 = quote.child3age;



                            var json = JsonConverterINF.Serialize(user);
                            string INFGETQuote = ConfigurationManager.AppSettings["INFGETQuote"];
                            var t = Task.Run(() => PostRequest(json, INFGETQuote, AuthKey, Domainname));
                            t.Wait();
                            string JsonResult = t.Result;
                            ViewBag.Message = JsonResult;

                            var ObjINF_uatResult = new Models.INF_QuatResult();

                            System.Web.Script.Serialization.JavaScriptSerializer jsSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();

                            finalResult = jsSerializer.Deserialize<List<Models.INF_Quote>>(JsonResult);

                            var Appname = "";

                            foreach (Models.INF_Quote item in finalResult)
                            {
                                if (item.policyName == "INF STANDARD")
                                {
                                    Appname = "INFS";
                                }
                                else if (item.policyName == "INF TRAVELUSA")
                                {
                                    Appname = "INFT";
                                }
                                else if (item.policyName == "INF ELITE")
                                {
                                    Appname = "INFE";
                                }
                                else if (item.policyName == "INF PREMIER")
                                {
                                    Appname = "INFP";
                                }
                                if (item.policyName == "INF STANDARD" || item.policyName == "INF TRAVELUSA" || item.policyName == "INF ELITE" || item.policyName == "INF PREMIER")
                                {


                                    List<QuotationModel> listitem = quotationalmanager.GetPlanDetails(Appname);
                                    //string premium = String.Format("{0:0.00}", item.totalPremium + 15.00);
                                    double premium = item.totalPremium + 15.00;
                                    AllQuatationResult AllQuatationresult = new AllQuatationResult();
                                    AllQuatationresult.PlanId = item.planId;
                                    AllQuatationresult.policyName = item.policyName;
                                    AllQuatationresult.planDeductible = item.planDeductible;
                                    AllQuatationresult.preExDeductible = item.preExDeductible;
                                    AllQuatationresult.preExCoverageAmount = item.preExCoverageAmount;
                                    AllQuatationresult.totalPremium = premium;

                                    AllQuatationresult.planMaximum = item.planMaximum;
                                    AllQuatationresult.INsuranceComp = "INF";
                                    AllQuatationresult.BuyNowLink = "";
                                    AllQuatationresult.policyType = item.policyType;
                                    AllQuatationresult.prexded = item.prexded;
                                    AllQuatationresult.Appname = Appname;
                                    if (listitem.Count > 0)
                                    {


                                        AllQuatationresult.Plan_Type = listitem[0].Plan_Type;
                                        AllQuatationresult.CANCELABLE = listitem[0].CANCELABLE;
                                        AllQuatationresult.Company_Name = listitem[0].Company_Name;
                                        AllQuatationresult.COUNTRY_RESTRICTIONS = listitem[0].COUNTRY_RESTRICTIONS;
                                        AllQuatationresult.COVID_19 = listitem[0].COVID_19;
                                        AllQuatationresult.IN_NETWORK = listitem[0].IN_NETWORK;
                                        AllQuatationresult.OUT_OF_NETWORK = listitem[0].OUT_OF_NETWORK;
                                        AllQuatationresult.Plan_Type = listitem[0].Plan_Type;
                                        AllQuatationresult.PRE_EXISITING_CONDITION = listitem[0].PRE_EXISITING_CONDITION;
                                        AllQuatationresult.RENEWABLE = listitem[0].RENEWABLE;
                                        AllQuatationresult.Pre_Existing = listitem[0].Pre_Existing;
                                    }
                                    AllQuatationresult.NOOFDAYS = DATE.ToString();
                                    lstQuatationResult.Add(AllQuatationresult);
                                }
                                else
                                {

                                }
                            }

                            return finalResult;
                        }
                        else if (quote.child2age != "" && (quote.child3age == "" && quote.child4age != "") && (quote.child1age == "") && (quote.spouseage == ""))
                        {
                            var user = new Models.INF_Child4_Child2();
                            user.quoteStartDate = Convert.ToDateTime(quote.LeavingHome).ToString("yyyy-MM-dd");
                            user.quoteEndDate = Convert.ToDateTime(quote.TillDate).ToString("yyyy-MM-dd");

                            user.coverageArea = quote.TouristISOCODE == "US" ? "USA/CANADA" : "Other/International";//"USA/CANADA";

                            var option = new Models.INF_User_Primary();
                            option.dob = dateofbirth;//"1965-12-02";                   
                            user.primary.Add(option);

                            user.ageChild2 = quote.child2age;

                            user.ageChild4 = quote.child4age;



                            var json = JsonConverterINF.Serialize(user);
                            string INFGETQuote = ConfigurationManager.AppSettings["INFGETQuote"];
                            var t = Task.Run(() => PostRequest(json, INFGETQuote, AuthKey, Domainname));
                            t.Wait();
                            string JsonResult = t.Result;
                            ViewBag.Message = JsonResult;

                            var ObjINF_uatResult = new Models.INF_QuatResult();

                            System.Web.Script.Serialization.JavaScriptSerializer jsSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();

                            finalResult = jsSerializer.Deserialize<List<Models.INF_Quote>>(JsonResult);

                            var Appname = "";

                            foreach (Models.INF_Quote item in finalResult)
                            {
                                if (item.policyName == "INF STANDARD")
                                {
                                    Appname = "INFS";
                                }
                                else if (item.policyName == "INF TRAVELUSA")
                                {
                                    Appname = "INFT";
                                }
                                else if (item.policyName == "INF ELITE")
                                {
                                    Appname = "INFE";
                                }
                                else if (item.policyName == "INF PREMIER")
                                {
                                    Appname = "INFP";
                                }
                                if (item.policyName == "INF STANDARD" || item.policyName == "INF TRAVELUSA" || item.policyName == "INF ELITE" || item.policyName == "INF PREMIER")
                                {


                                    List<QuotationModel> listitem = quotationalmanager.GetPlanDetails(Appname);
                                    //string premium = String.Format("{0:0.00}", item.totalPremium + 15.00);
                                    double premium = item.totalPremium + 15.00;
                                    AllQuatationResult AllQuatationresult = new AllQuatationResult();
                                    AllQuatationresult.PlanId = item.planId;
                                    AllQuatationresult.policyName = item.policyName;
                                    AllQuatationresult.planDeductible = item.planDeductible;
                                    AllQuatationresult.preExDeductible = item.preExDeductible;
                                    AllQuatationresult.preExCoverageAmount = item.preExCoverageAmount;
                                    AllQuatationresult.totalPremium = premium;
                                    AllQuatationresult.planMaximum = item.planMaximum;
                                    AllQuatationresult.INsuranceComp = "INF";
                                    AllQuatationresult.BuyNowLink = "";
                                    AllQuatationresult.policyType = item.policyType;
                                    AllQuatationresult.prexded = item.prexded;
                                    AllQuatationresult.Appname = Appname;
                                    if (listitem.Count > 0)
                                    {


                                        AllQuatationresult.Plan_Type = listitem[0].Plan_Type;
                                        AllQuatationresult.CANCELABLE = listitem[0].CANCELABLE;
                                        AllQuatationresult.Company_Name = listitem[0].Company_Name;
                                        AllQuatationresult.COUNTRY_RESTRICTIONS = listitem[0].COUNTRY_RESTRICTIONS;
                                        AllQuatationresult.COVID_19 = listitem[0].COVID_19;
                                        AllQuatationresult.IN_NETWORK = listitem[0].IN_NETWORK;
                                        AllQuatationresult.OUT_OF_NETWORK = listitem[0].OUT_OF_NETWORK;
                                        AllQuatationresult.Plan_Type = listitem[0].Plan_Type;
                                        AllQuatationresult.PRE_EXISITING_CONDITION = listitem[0].PRE_EXISITING_CONDITION;
                                        AllQuatationresult.RENEWABLE = listitem[0].RENEWABLE;
                                        AllQuatationresult.Pre_Existing = listitem[0].Pre_Existing;
                                    }
                                    AllQuatationresult.NOOFDAYS = DATE.ToString();
                                    lstQuatationResult.Add(AllQuatationresult);
                                }
                                else
                                {

                                }
                            }

                            return finalResult;
                        }
                        else if (quote.child2age == "" && (quote.child3age != "" && quote.child4age != "") && (quote.child1age == "") && (quote.spouseage == ""))
                        {
                            var user = new Models.INF_Child4_Child3();
                            user.quoteStartDate = Convert.ToDateTime(quote.LeavingHome).ToString("yyyy-MM-dd");
                            user.quoteEndDate = Convert.ToDateTime(quote.TillDate).ToString("yyyy-MM-dd");

                            user.coverageArea = quote.TouristISOCODE == "US" ? "USA/CANADA" : "Other/International";//"USA/CANADA";

                            var option = new Models.INF_User_Primary();
                            option.dob = dateofbirth;//"1965-12-02";                   
                            user.primary.Add(option);

                            user.ageChild3 = quote.child3age;
                            user.ageChild4 = quote.child4age;



                            var json = JsonConverterINF.Serialize(user);
                            string INFGETQuote = ConfigurationManager.AppSettings["INFGETQuote"];
                            var t = Task.Run(() => PostRequest(json, INFGETQuote, AuthKey, Domainname));
                            t.Wait();
                            string JsonResult = t.Result;
                            ViewBag.Message = JsonResult;

                            var ObjINF_uatResult = new Models.INF_QuatResult();

                            System.Web.Script.Serialization.JavaScriptSerializer jsSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();

                            finalResult = jsSerializer.Deserialize<List<Models.INF_Quote>>(JsonResult);

                            var Appname = "";

                            foreach (Models.INF_Quote item in finalResult)
                            {
                                if (item.policyName == "INF STANDARD")
                                {
                                    Appname = "INFS";
                                }
                                else if (item.policyName == "INF TRAVELUSA")
                                {
                                    Appname = "INFT";
                                }
                                else if (item.policyName == "INF ELITE")
                                {
                                    Appname = "INFE";
                                }
                                else if (item.policyName == "INF PREMIER")
                                {
                                    Appname = "INFP";
                                }
                                if (item.policyName == "INF STANDARD" || item.policyName == "INF TRAVELUSA" || item.policyName == "INF ELITE" || item.policyName == "INF PREMIER")
                                {


                                    List<QuotationModel> listitem = quotationalmanager.GetPlanDetails(Appname);
                                    //string premium = String.Format("{0:0.00}", item.totalPremium + 15.00);
                                    double premium = item.totalPremium + 15.00;
                                    AllQuatationResult AllQuatationresult = new AllQuatationResult();
                                    AllQuatationresult.PlanId = item.planId;
                                    AllQuatationresult.policyName = item.policyName;
                                    AllQuatationresult.planDeductible = item.planDeductible;
                                    AllQuatationresult.preExDeductible = item.preExDeductible;
                                    AllQuatationresult.preExCoverageAmount = item.preExCoverageAmount;
                                    AllQuatationresult.totalPremium = premium;
                                    AllQuatationresult.planMaximum = item.planMaximum;
                                    AllQuatationresult.INsuranceComp = "INF";
                                    AllQuatationresult.BuyNowLink = "";
                                    AllQuatationresult.policyType = item.policyType;
                                    AllQuatationresult.prexded = item.prexded;
                                    AllQuatationresult.Appname = Appname;
                                    if (listitem.Count > 0)
                                    {


                                        AllQuatationresult.Plan_Type = listitem[0].Plan_Type;
                                        AllQuatationresult.CANCELABLE = listitem[0].CANCELABLE;
                                        AllQuatationresult.Company_Name = listitem[0].Company_Name;
                                        AllQuatationresult.COUNTRY_RESTRICTIONS = listitem[0].COUNTRY_RESTRICTIONS;
                                        AllQuatationresult.COVID_19 = listitem[0].COVID_19;
                                        AllQuatationresult.IN_NETWORK = listitem[0].IN_NETWORK;
                                        AllQuatationresult.OUT_OF_NETWORK = listitem[0].OUT_OF_NETWORK;
                                        AllQuatationresult.Plan_Type = listitem[0].Plan_Type;
                                        AllQuatationresult.PRE_EXISITING_CONDITION = listitem[0].PRE_EXISITING_CONDITION;
                                        AllQuatationresult.RENEWABLE = listitem[0].RENEWABLE;
                                        AllQuatationresult.Pre_Existing = listitem[0].Pre_Existing;
                                    }
                                    AllQuatationresult.NOOFDAYS = DATE.ToString();
                                    lstQuatationResult.Add(AllQuatationresult);
                                }
                                else
                                {

                                }
                            }

                            return finalResult;
                        }
                        else if (quote.child2age == "" && (quote.child3age != "" && quote.child4age != "") && (quote.child1age != "") && (quote.spouseage == ""))
                        {
                            var user = new Models.INF_Child4_Child1_Child3();
                            user.quoteStartDate = Convert.ToDateTime(quote.LeavingHome).ToString("yyyy-MM-dd");
                            user.quoteEndDate = Convert.ToDateTime(quote.TillDate).ToString("yyyy-MM-dd");

                            user.coverageArea = quote.TouristISOCODE == "US" ? "USA/CANADA" : "Other/International";//"USA/CANADA";

                            var option = new Models.INF_User_Primary();
                            option.dob = dateofbirth;//"1965-12-02";                   
                            user.primary.Add(option);

                            user.ageChild1 = quote.child1age;

                            user.ageChild3 = quote.child3age;
                            user.ageChild4 = quote.child4age;



                            var json = JsonConverterINF.Serialize(user);
                            string INFGETQuote = ConfigurationManager.AppSettings["INFGETQuote"];
                            var t = Task.Run(() => PostRequest(json, INFGETQuote, AuthKey, Domainname));
                            t.Wait();
                            string JsonResult = t.Result;
                            ViewBag.Message = JsonResult;

                            var ObjINF_uatResult = new Models.INF_QuatResult();

                            System.Web.Script.Serialization.JavaScriptSerializer jsSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();

                            finalResult = jsSerializer.Deserialize<List<Models.INF_Quote>>(JsonResult);

                            var Appname = "";

                            foreach (Models.INF_Quote item in finalResult)
                            {
                                if (item.policyName == "INF STANDARD")
                                {
                                    Appname = "INFS";
                                }
                                else if (item.policyName == "INF TRAVELUSA")
                                {
                                    Appname = "INFT";
                                }
                                else if (item.policyName == "INF ELITE")
                                {
                                    Appname = "INFE";
                                }
                                else if (item.policyName == "INF PREMIER")
                                {
                                    Appname = "INFP";
                                }
                                if (item.policyName == "INF STANDARD" || item.policyName == "INF TRAVELUSA" || item.policyName == "INF ELITE" || item.policyName == "INF PREMIER")
                                {


                                    List<QuotationModel> listitem = quotationalmanager.GetPlanDetails(Appname);
                                    //string premium = String.Format("{0:0.00}", item.totalPremium + 15.00);
                                    double premium = item.totalPremium + 15.00;
                                    AllQuatationResult AllQuatationresult = new AllQuatationResult();
                                    AllQuatationresult.PlanId = item.planId;
                                    AllQuatationresult.policyName = item.policyName;
                                    AllQuatationresult.planDeductible = item.planDeductible;
                                    AllQuatationresult.preExDeductible = item.preExDeductible;
                                    AllQuatationresult.preExCoverageAmount = item.preExCoverageAmount;
                                    AllQuatationresult.totalPremium = premium;
                                    AllQuatationresult.planMaximum = item.planMaximum;
                                    AllQuatationresult.INsuranceComp = "INF";
                                    AllQuatationresult.BuyNowLink = "";
                                    AllQuatationresult.policyType = item.policyType;
                                    AllQuatationresult.prexded = item.prexded;
                                    AllQuatationresult.Appname = Appname;
                                    if (listitem.Count > 0)
                                    {


                                        AllQuatationresult.Plan_Type = listitem[0].Plan_Type;
                                        AllQuatationresult.CANCELABLE = listitem[0].CANCELABLE;
                                        AllQuatationresult.Company_Name = listitem[0].Company_Name;
                                        AllQuatationresult.COUNTRY_RESTRICTIONS = listitem[0].COUNTRY_RESTRICTIONS;
                                        AllQuatationresult.COVID_19 = listitem[0].COVID_19;
                                        AllQuatationresult.IN_NETWORK = listitem[0].IN_NETWORK;
                                        AllQuatationresult.OUT_OF_NETWORK = listitem[0].OUT_OF_NETWORK;
                                        AllQuatationresult.Plan_Type = listitem[0].Plan_Type;
                                        AllQuatationresult.PRE_EXISITING_CONDITION = listitem[0].PRE_EXISITING_CONDITION;
                                        AllQuatationresult.RENEWABLE = listitem[0].RENEWABLE;
                                        AllQuatationresult.Pre_Existing = listitem[0].Pre_Existing;
                                    }
                                    AllQuatationresult.NOOFDAYS = DATE.ToString();
                                    lstQuatationResult.Add(AllQuatationresult);
                                }
                                else
                                {

                                }
                            }

                            return finalResult;
                        }
                        else if (quote.child2age != "" && (quote.child3age != "" && quote.child4age != "") && (quote.child1age == "") && (quote.spouseage == ""))
                        {
                            var user = new Models.INF_Child4_Child2_Child3();
                            user.quoteStartDate = Convert.ToDateTime(quote.LeavingHome).ToString("yyyy-MM-dd");
                            user.quoteEndDate = Convert.ToDateTime(quote.TillDate).ToString("yyyy-MM-dd");

                            user.coverageArea = quote.TouristISOCODE == "US" ? "USA/CANADA" : "Other/International";//"USA/CANADA";

                            var option = new Models.INF_User_Primary();
                            option.dob = dateofbirth;//"1965-12-02";                   
                            user.primary.Add(option);

                            user.ageChild2 = quote.child2age;
                            user.ageChild3 = quote.child3age;
                            user.ageChild4 = quote.child4age;



                            var json = JsonConverterINF.Serialize(user);
                            string INFGETQuote = ConfigurationManager.AppSettings["INFGETQuote"];
                            var t = Task.Run(() => PostRequest(json, INFGETQuote, AuthKey, Domainname));
                            t.Wait();
                            string JsonResult = t.Result;
                            ViewBag.Message = JsonResult;

                            var ObjINF_uatResult = new Models.INF_QuatResult();

                            System.Web.Script.Serialization.JavaScriptSerializer jsSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();

                            finalResult = jsSerializer.Deserialize<List<Models.INF_Quote>>(JsonResult);

                            var Appname = "";

                            foreach (Models.INF_Quote item in finalResult)
                            {
                                if (item.policyName == "INF STANDARD")
                                {
                                    Appname = "INFS";
                                }
                                else if (item.policyName == "INF TRAVELUSA")
                                {
                                    Appname = "INFT";
                                }
                                else if (item.policyName == "INF ELITE")
                                {
                                    Appname = "INFE";
                                }
                                else if (item.policyName == "INF PREMIER")
                                {
                                    Appname = "INFP";
                                }
                                if (item.policyName == "INF STANDARD" || item.policyName == "INF TRAVELUSA" || item.policyName == "INF ELITE" || item.policyName == "INF PREMIER")
                                {


                                    List<QuotationModel> listitem = quotationalmanager.GetPlanDetails(Appname);
                                    //string premium = String.Format("{0:0.00}", item.totalPremium + 15.00);
                                    double premium = item.totalPremium + 15.00;
                                    AllQuatationResult AllQuatationresult = new AllQuatationResult();
                                    AllQuatationresult.PlanId = item.planId;
                                    AllQuatationresult.policyName = item.policyName;
                                    AllQuatationresult.planDeductible = item.planDeductible;
                                    AllQuatationresult.preExDeductible = item.preExDeductible;
                                    AllQuatationresult.preExCoverageAmount = item.preExCoverageAmount;
                                    AllQuatationresult.totalPremium = premium;
                                    AllQuatationresult.planMaximum = item.planMaximum;
                                    AllQuatationresult.INsuranceComp = "INF";
                                    AllQuatationresult.BuyNowLink = "";
                                    AllQuatationresult.policyType = item.policyType;
                                    AllQuatationresult.prexded = item.prexded;
                                    AllQuatationresult.Appname = Appname;
                                    if (listitem.Count > 0)
                                    {


                                        AllQuatationresult.Plan_Type = listitem[0].Plan_Type;
                                        AllQuatationresult.CANCELABLE = listitem[0].CANCELABLE;
                                        AllQuatationresult.Company_Name = listitem[0].Company_Name;
                                        AllQuatationresult.COUNTRY_RESTRICTIONS = listitem[0].COUNTRY_RESTRICTIONS;
                                        AllQuatationresult.COVID_19 = listitem[0].COVID_19;
                                        AllQuatationresult.IN_NETWORK = listitem[0].IN_NETWORK;
                                        AllQuatationresult.OUT_OF_NETWORK = listitem[0].OUT_OF_NETWORK;
                                        AllQuatationresult.Plan_Type = listitem[0].Plan_Type;
                                        AllQuatationresult.PRE_EXISITING_CONDITION = listitem[0].PRE_EXISITING_CONDITION;
                                        AllQuatationresult.RENEWABLE = listitem[0].RENEWABLE;
                                        AllQuatationresult.Pre_Existing = listitem[0].Pre_Existing;
                                    }
                                    AllQuatationresult.NOOFDAYS = DATE.ToString();
                                    lstQuatationResult.Add(AllQuatationresult);
                                }
                                else
                                {

                                }
                            }

                            return finalResult;
                        }
                        else if (quote.child2age != "" && (quote.child3age == "" && quote.child4age == "") && (quote.child1age == "") && (quote.spouseage != ""))
                        {
                            var user = new Models.INF_Child2_Spouse();
                            user.quoteStartDate = Convert.ToDateTime(quote.LeavingHome).ToString("yyyy-MM-dd");
                            user.quoteEndDate = Convert.ToDateTime(quote.TillDate).ToString("yyyy-MM-dd");

                            user.coverageArea = quote.TouristISOCODE == "US" ? "USA/CANADA" : "Other/International";//"USA/CANADA";

                            var option = new Models.INF_User_Primary();
                            option.dob = dateofbirth;//"1965-12-02";                   
                            user.primary.Add(option);
                            user.spouseDob = Convert.ToDateTime(spousedateofbirth).ToString("yyyy-MM-dd");

                            user.ageChild2 = quote.child2age;



                            var json = JsonConverterINF.Serialize(user);
                            string INFGETQuote = ConfigurationManager.AppSettings["INFGETQuote"];
                            var t = Task.Run(() => PostRequest(json, INFGETQuote, AuthKey, Domainname));
                            t.Wait();
                            string JsonResult = t.Result;
                            ViewBag.Message = JsonResult;

                            var ObjINF_uatResult = new Models.INF_QuatResult();

                            System.Web.Script.Serialization.JavaScriptSerializer jsSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();

                            finalResult = jsSerializer.Deserialize<List<Models.INF_Quote>>(JsonResult);

                            var Appname = "";

                            foreach (Models.INF_Quote item in finalResult)
                            {
                                if (item.policyName == "INF STANDARD")
                                {
                                    Appname = "INFS";
                                }
                                else if (item.policyName == "INF TRAVELUSA")
                                {
                                    Appname = "INFT";
                                }
                                else if (item.policyName == "INF ELITE")
                                {
                                    Appname = "INFE";
                                }
                                else if (item.policyName == "INF PREMIER")
                                {
                                    Appname = "INFP";
                                }
                                if (item.policyName == "INF STANDARD" || item.policyName == "INF TRAVELUSA" || item.policyName == "INF ELITE" || item.policyName == "INF PREMIER")
                                {


                                    List<QuotationModel> listitem = quotationalmanager.GetPlanDetails(Appname);
                                    //string premium = String.Format("{0:0.00}", item.totalPremium + 15.00);
                                    double premium = item.totalPremium + 15.00;
                                    AllQuatationResult AllQuatationresult = new AllQuatationResult();
                                    AllQuatationresult.PlanId = item.planId;
                                    AllQuatationresult.policyName = item.policyName;
                                    AllQuatationresult.planDeductible = item.planDeductible;
                                    AllQuatationresult.preExDeductible = item.preExDeductible;
                                    AllQuatationresult.preExCoverageAmount = item.preExCoverageAmount;
                                    AllQuatationresult.totalPremium = premium;
                                    AllQuatationresult.planMaximum = item.planMaximum;
                                    AllQuatationresult.INsuranceComp = "INF";
                                    AllQuatationresult.BuyNowLink = "";
                                    AllQuatationresult.policyType = item.policyType;
                                    AllQuatationresult.prexded = item.prexded;
                                    AllQuatationresult.Appname = Appname;
                                    if (listitem.Count > 0)
                                    {


                                        AllQuatationresult.Plan_Type = listitem[0].Plan_Type;
                                        AllQuatationresult.CANCELABLE = listitem[0].CANCELABLE;
                                        AllQuatationresult.Company_Name = listitem[0].Company_Name;
                                        AllQuatationresult.COUNTRY_RESTRICTIONS = listitem[0].COUNTRY_RESTRICTIONS;
                                        AllQuatationresult.COVID_19 = listitem[0].COVID_19;
                                        AllQuatationresult.IN_NETWORK = listitem[0].IN_NETWORK;
                                        AllQuatationresult.OUT_OF_NETWORK = listitem[0].OUT_OF_NETWORK;
                                        AllQuatationresult.Plan_Type = listitem[0].Plan_Type;
                                        AllQuatationresult.PRE_EXISITING_CONDITION = listitem[0].PRE_EXISITING_CONDITION;
                                        AllQuatationresult.RENEWABLE = listitem[0].RENEWABLE;
                                        AllQuatationresult.Pre_Existing = listitem[0].Pre_Existing;
                                    }
                                    AllQuatationresult.NOOFDAYS = DATE.ToString();
                                    lstQuatationResult.Add(AllQuatationresult);
                                }
                                else
                                {

                                }
                            }

                            return finalResult;
                        }
                        else if (quote.child2age == "" && (quote.child3age != "" && quote.child4age == "") && (quote.child1age == "") && (quote.spouseage != ""))
                        {
                            var user = new Models.INF_Child3_Spouse();
                            user.quoteStartDate = Convert.ToDateTime(quote.LeavingHome).ToString("yyyy-MM-dd");
                            user.quoteEndDate = Convert.ToDateTime(quote.TillDate).ToString("yyyy-MM-dd");

                            user.coverageArea = quote.TouristISOCODE == "US" ? "USA/CANADA" : "Other/International";//"USA/CANADA";

                            var option = new Models.INF_User_Primary();
                            option.dob = dateofbirth;//"1965-12-02";                   
                            user.primary.Add(option);
                            user.spouseDob = Convert.ToDateTime(spousedateofbirth).ToString("yyyy-MM-dd");

                            user.ageChild3 = quote.child3age;



                            var json = JsonConverterINF.Serialize(user);
                            string INFGETQuote = ConfigurationManager.AppSettings["INFGETQuote"];
                            var t = Task.Run(() => PostRequest(json, INFGETQuote, AuthKey, Domainname));
                            t.Wait();
                            string JsonResult = t.Result;
                            ViewBag.Message = JsonResult;

                            var ObjINF_uatResult = new Models.INF_QuatResult();

                            System.Web.Script.Serialization.JavaScriptSerializer jsSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();

                            finalResult = jsSerializer.Deserialize<List<Models.INF_Quote>>(JsonResult);

                            var Appname = "";

                            foreach (Models.INF_Quote item in finalResult)
                            {
                                if (item.policyName == "INF STANDARD")
                                {
                                    Appname = "INFS";
                                }
                                else if (item.policyName == "INF TRAVELUSA")
                                {
                                    Appname = "INFT";
                                }
                                else if (item.policyName == "INF ELITE")
                                {
                                    Appname = "INFE";
                                }
                                else if (item.policyName == "INF PREMIER")
                                {
                                    Appname = "INFP";
                                }
                                if (item.policyName == "INF STANDARD" || item.policyName == "INF TRAVELUSA" || item.policyName == "INF ELITE" || item.policyName == "INF PREMIER")
                                {


                                    List<QuotationModel> listitem = quotationalmanager.GetPlanDetails(Appname);
                                    //string premium = String.Format("{0:0.00}", item.totalPremium + 15.00);
                                    double premium = item.totalPremium + 15.00;
                                    AllQuatationResult AllQuatationresult = new AllQuatationResult();
                                    AllQuatationresult.PlanId = item.planId;
                                    AllQuatationresult.policyName = item.policyName;
                                    AllQuatationresult.planDeductible = item.planDeductible;
                                    AllQuatationresult.preExDeductible = item.preExDeductible;
                                    AllQuatationresult.preExCoverageAmount = item.preExCoverageAmount;
                                    AllQuatationresult.totalPremium = premium;

                                    AllQuatationresult.planMaximum = item.planMaximum;
                                    AllQuatationresult.INsuranceComp = "INF";
                                    AllQuatationresult.BuyNowLink = "";
                                    AllQuatationresult.policyType = item.policyType;
                                    AllQuatationresult.prexded = item.prexded;
                                    AllQuatationresult.Appname = Appname;
                                    if (listitem.Count > 0)
                                    {


                                        AllQuatationresult.Plan_Type = listitem[0].Plan_Type;
                                        AllQuatationresult.CANCELABLE = listitem[0].CANCELABLE;
                                        AllQuatationresult.Company_Name = listitem[0].Company_Name;
                                        AllQuatationresult.COUNTRY_RESTRICTIONS = listitem[0].COUNTRY_RESTRICTIONS;
                                        AllQuatationresult.COVID_19 = listitem[0].COVID_19;
                                        AllQuatationresult.IN_NETWORK = listitem[0].IN_NETWORK;
                                        AllQuatationresult.OUT_OF_NETWORK = listitem[0].OUT_OF_NETWORK;
                                        AllQuatationresult.Plan_Type = listitem[0].Plan_Type;
                                        AllQuatationresult.PRE_EXISITING_CONDITION = listitem[0].PRE_EXISITING_CONDITION;
                                        AllQuatationresult.RENEWABLE = listitem[0].RENEWABLE;
                                        AllQuatationresult.Pre_Existing = listitem[0].Pre_Existing;
                                    }
                                    AllQuatationresult.NOOFDAYS = DATE.ToString();
                                    lstQuatationResult.Add(AllQuatationresult);
                                }
                                else
                                {

                                }
                            }

                            return finalResult;
                        }
                        else if (quote.child2age == "" && (quote.child3age == "" && quote.child4age != "") && (quote.child1age == "") && (quote.spouseage != ""))
                        {
                            var user = new Models.INF_Child4_Spouse();
                            user.quoteStartDate = Convert.ToDateTime(quote.LeavingHome).ToString("yyyy-MM-dd");
                            user.quoteEndDate = Convert.ToDateTime(quote.TillDate).ToString("yyyy-MM-dd");

                            user.coverageArea = quote.TouristISOCODE == "US" ? "USA/CANADA" : "Other/International";//"USA/CANADA";

                            var option = new Models.INF_User_Primary();
                            option.dob = dateofbirth;//"1965-12-02";                   
                            user.primary.Add(option);
                            user.spouseDob = Convert.ToDateTime(spousedateofbirth).ToString("yyyy-MM-dd");

                            user.ageChild4 = quote.child4age;



                            var json = JsonConverterINF.Serialize(user);
                            string INFGETQuote = ConfigurationManager.AppSettings["INFGETQuote"];
                            var t = Task.Run(() => PostRequest(json, INFGETQuote, AuthKey, Domainname));
                            t.Wait();
                            string JsonResult = t.Result;
                            ViewBag.Message = JsonResult;

                            var ObjINF_uatResult = new Models.INF_QuatResult();

                            System.Web.Script.Serialization.JavaScriptSerializer jsSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();

                            finalResult = jsSerializer.Deserialize<List<Models.INF_Quote>>(JsonResult);

                            var Appname = "";

                            foreach (Models.INF_Quote item in finalResult)
                            {
                                if (item.policyName == "INF STANDARD")
                                {
                                    Appname = "INFS";
                                }
                                else if (item.policyName == "INF TRAVELUSA")
                                {
                                    Appname = "INFT";
                                }
                                else if (item.policyName == "INF ELITE")
                                {
                                    Appname = "INFE";
                                }
                                else if (item.policyName == "INF PREMIER")
                                {
                                    Appname = "INFP";
                                }
                                if (item.policyName == "INF STANDARD" || item.policyName == "INF TRAVELUSA" || item.policyName == "INF ELITE" || item.policyName == "INF PREMIER")
                                {


                                    List<QuotationModel> listitem = quotationalmanager.GetPlanDetails(Appname);
                                    //string premium = String.Format("{0:0.00}", item.totalPremium + 15.00);
                                    double premium = item.totalPremium + 15.00;
                                    AllQuatationResult AllQuatationresult = new AllQuatationResult();
                                    AllQuatationresult.PlanId = item.planId;
                                    AllQuatationresult.policyName = item.policyName;
                                    AllQuatationresult.planDeductible = item.planDeductible;
                                    AllQuatationresult.preExDeductible = item.preExDeductible;
                                    AllQuatationresult.preExCoverageAmount = item.preExCoverageAmount;
                                    AllQuatationresult.totalPremium = premium;
                                    AllQuatationresult.planMaximum = item.planMaximum;
                                    AllQuatationresult.INsuranceComp = "INF";
                                    AllQuatationresult.BuyNowLink = "";
                                    AllQuatationresult.policyType = item.policyType;
                                    AllQuatationresult.prexded = item.prexded;
                                    AllQuatationresult.Appname = Appname;
                                    if (listitem.Count > 0)
                                    {


                                        AllQuatationresult.Plan_Type = listitem[0].Plan_Type;
                                        AllQuatationresult.CANCELABLE = listitem[0].CANCELABLE;
                                        AllQuatationresult.Company_Name = listitem[0].Company_Name;
                                        AllQuatationresult.COUNTRY_RESTRICTIONS = listitem[0].COUNTRY_RESTRICTIONS;
                                        AllQuatationresult.COVID_19 = listitem[0].COVID_19;
                                        AllQuatationresult.IN_NETWORK = listitem[0].IN_NETWORK;
                                        AllQuatationresult.OUT_OF_NETWORK = listitem[0].OUT_OF_NETWORK;
                                        AllQuatationresult.Plan_Type = listitem[0].Plan_Type;
                                        AllQuatationresult.PRE_EXISITING_CONDITION = listitem[0].PRE_EXISITING_CONDITION;
                                        AllQuatationresult.RENEWABLE = listitem[0].RENEWABLE;
                                        AllQuatationresult.Pre_Existing = listitem[0].Pre_Existing;
                                    }
                                    AllQuatationresult.NOOFDAYS = DATE.ToString();
                                    lstQuatationResult.Add(AllQuatationresult);
                                }
                                else
                                {

                                }
                            }

                            return finalResult;
                        }
                        else if (quote.child2age != "" && (quote.child3age != "" && quote.child4age == "") && (quote.child1age == "") && (quote.spouseage != ""))
                        {
                            var user = new Models.INF_Child3_Spouse_Child2();
                            user.quoteStartDate = Convert.ToDateTime(quote.LeavingHome).ToString("yyyy-MM-dd");
                            user.quoteEndDate = Convert.ToDateTime(quote.TillDate).ToString("yyyy-MM-dd");

                            user.coverageArea = quote.TouristISOCODE == "US" ? "USA/CANADA" : "Other/International";//"USA/CANADA";

                            var option = new Models.INF_User_Primary();
                            option.dob = dateofbirth;//"1965-12-02";                   
                            user.primary.Add(option);
                            user.spouseDob = Convert.ToDateTime(spousedateofbirth).ToString("yyyy-MM-dd");

                            user.ageChild2 = quote.child2age;
                            user.ageChild3 = quote.child3age;



                            var json = JsonConverterINF.Serialize(user);
                            string INFGETQuote = ConfigurationManager.AppSettings["INFGETQuote"];
                            var t = Task.Run(() => PostRequest(json, INFGETQuote, AuthKey, Domainname));
                            t.Wait();
                            string JsonResult = t.Result;
                            ViewBag.Message = JsonResult;

                            var ObjINF_uatResult = new Models.INF_QuatResult();

                            System.Web.Script.Serialization.JavaScriptSerializer jsSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();

                            finalResult = jsSerializer.Deserialize<List<Models.INF_Quote>>(JsonResult);

                            var Appname = "";

                            foreach (Models.INF_Quote item in finalResult)
                            {
                                if (item.policyName == "INF STANDARD")
                                {
                                    Appname = "INFS";
                                }
                                else if (item.policyName == "INF TRAVELUSA")
                                {
                                    Appname = "INFT";
                                }
                                else if (item.policyName == "INF ELITE")
                                {
                                    Appname = "INFE";
                                }
                                else if (item.policyName == "INF PREMIER")
                                {
                                    Appname = "INFP";
                                }
                                if (item.policyName == "INF STANDARD" || item.policyName == "INF TRAVELUSA" || item.policyName == "INF ELITE" || item.policyName == "INF PREMIER")
                                {


                                    List<QuotationModel> listitem = quotationalmanager.GetPlanDetails(Appname);
                                    //string premium = String.Format("{0:0.00}", item.totalPremium + 15.00);
                                    double premium = item.totalPremium + 15.00;
                                    AllQuatationResult AllQuatationresult = new AllQuatationResult();
                                    AllQuatationresult.PlanId = item.planId;
                                    AllQuatationresult.policyName = item.policyName;
                                    AllQuatationresult.planDeductible = item.planDeductible;
                                    AllQuatationresult.preExDeductible = item.preExDeductible;
                                    AllQuatationresult.preExCoverageAmount = item.preExCoverageAmount;
                                    AllQuatationresult.totalPremium = premium;

                                    AllQuatationresult.planMaximum = item.planMaximum;
                                    AllQuatationresult.INsuranceComp = "INF";
                                    AllQuatationresult.BuyNowLink = "";
                                    AllQuatationresult.policyType = item.policyType;
                                    AllQuatationresult.prexded = item.prexded;
                                    AllQuatationresult.Appname = Appname;
                                    if (listitem.Count > 0)
                                    {


                                        AllQuatationresult.Plan_Type = listitem[0].Plan_Type;
                                        AllQuatationresult.CANCELABLE = listitem[0].CANCELABLE;
                                        AllQuatationresult.Company_Name = listitem[0].Company_Name;
                                        AllQuatationresult.COUNTRY_RESTRICTIONS = listitem[0].COUNTRY_RESTRICTIONS;
                                        AllQuatationresult.COVID_19 = listitem[0].COVID_19;
                                        AllQuatationresult.IN_NETWORK = listitem[0].IN_NETWORK;
                                        AllQuatationresult.OUT_OF_NETWORK = listitem[0].OUT_OF_NETWORK;
                                        AllQuatationresult.Plan_Type = listitem[0].Plan_Type;
                                        AllQuatationresult.PRE_EXISITING_CONDITION = listitem[0].PRE_EXISITING_CONDITION;
                                        AllQuatationresult.RENEWABLE = listitem[0].RENEWABLE;
                                        AllQuatationresult.Pre_Existing = listitem[0].Pre_Existing;
                                    }
                                    AllQuatationresult.NOOFDAYS = DATE.ToString();
                                    lstQuatationResult.Add(AllQuatationresult);
                                }
                                else
                                {

                                }
                            }

                            return finalResult;
                        }
                        else if (quote.child2age != "" && (quote.child3age == "" && quote.child4age != "") && (quote.child1age == "") && (quote.spouseage != ""))
                        {
                            var user = new Models.INF_Child4_Spouse_Child2();
                            user.quoteStartDate = Convert.ToDateTime(quote.LeavingHome).ToString("yyyy-MM-dd");
                            user.quoteEndDate = Convert.ToDateTime(quote.TillDate).ToString("yyyy-MM-dd");

                            user.coverageArea = quote.TouristISOCODE == "US" ? "USA/CANADA" : "Other/International";//"USA/CANADA";

                            var option = new Models.INF_User_Primary();
                            option.dob = dateofbirth;//"1965-12-02";                   
                            user.primary.Add(option);
                            user.spouseDob = Convert.ToDateTime(spousedateofbirth).ToString("yyyy-MM-dd");

                            user.ageChild2 = quote.child2age;

                            user.ageChild4 = quote.child4age;



                            var json = JsonConverterINF.Serialize(user);
                            string INFGETQuote = ConfigurationManager.AppSettings["INFGETQuote"];
                            var t = Task.Run(() => PostRequest(json, INFGETQuote, AuthKey, Domainname));
                            t.Wait();
                            string JsonResult = t.Result;
                            ViewBag.Message = JsonResult;

                            var ObjINF_uatResult = new Models.INF_QuatResult();

                            System.Web.Script.Serialization.JavaScriptSerializer jsSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();

                            finalResult = jsSerializer.Deserialize<List<Models.INF_Quote>>(JsonResult);

                            var Appname = "";

                            foreach (Models.INF_Quote item in finalResult)
                            {
                                if (item.policyName == "INF STANDARD")
                                {
                                    Appname = "INFS";
                                }
                                else if (item.policyName == "INF TRAVELUSA")
                                {
                                    Appname = "INFT";
                                }
                                else if (item.policyName == "INF ELITE")
                                {
                                    Appname = "INFE";
                                }
                                else if (item.policyName == "INF PREMIER")
                                {
                                    Appname = "INFP";
                                }
                                if (item.policyName == "INF STANDARD" || item.policyName == "INF TRAVELUSA" || item.policyName == "INF ELITE" || item.policyName == "INF PREMIER")
                                {


                                    List<QuotationModel> listitem = quotationalmanager.GetPlanDetails(Appname);
                                    //string premium = String.Format("{0:0.00}", item.totalPremium + 15.00);
                                    double premium = item.totalPremium + 15.00;
                                    AllQuatationResult AllQuatationresult = new AllQuatationResult();
                                    AllQuatationresult.PlanId = item.planId;
                                    AllQuatationresult.policyName = item.policyName;
                                    AllQuatationresult.planDeductible = item.planDeductible;
                                    AllQuatationresult.preExDeductible = item.preExDeductible;
                                    AllQuatationresult.preExCoverageAmount = item.preExCoverageAmount;
                                    AllQuatationresult.totalPremium = premium;
                                    AllQuatationresult.planMaximum = item.planMaximum;
                                    AllQuatationresult.INsuranceComp = "INF";
                                    AllQuatationresult.BuyNowLink = "";
                                    AllQuatationresult.policyType = item.policyType;
                                    AllQuatationresult.prexded = item.prexded;
                                    AllQuatationresult.Appname = Appname;
                                    if (listitem.Count > 0)
                                    {


                                        AllQuatationresult.Plan_Type = listitem[0].Plan_Type;
                                        AllQuatationresult.CANCELABLE = listitem[0].CANCELABLE;
                                        AllQuatationresult.Company_Name = listitem[0].Company_Name;
                                        AllQuatationresult.COUNTRY_RESTRICTIONS = listitem[0].COUNTRY_RESTRICTIONS;
                                        AllQuatationresult.COVID_19 = listitem[0].COVID_19;
                                        AllQuatationresult.IN_NETWORK = listitem[0].IN_NETWORK;
                                        AllQuatationresult.OUT_OF_NETWORK = listitem[0].OUT_OF_NETWORK;
                                        AllQuatationresult.Plan_Type = listitem[0].Plan_Type;
                                        AllQuatationresult.PRE_EXISITING_CONDITION = listitem[0].PRE_EXISITING_CONDITION;
                                        AllQuatationresult.RENEWABLE = listitem[0].RENEWABLE;
                                        AllQuatationresult.Pre_Existing = listitem[0].Pre_Existing;
                                    }
                                    AllQuatationresult.NOOFDAYS = DATE.ToString();
                                    lstQuatationResult.Add(AllQuatationresult);
                                }
                                else
                                {

                                }
                            }

                            return finalResult;
                        }
                        else if (quote.child2age == "" && (quote.child3age != "" && quote.child4age != "") && (quote.child1age == "") && (quote.spouseage != ""))
                        {
                            var user = new Models.INF_Child4_Spouse_Child3();
                            user.quoteStartDate = Convert.ToDateTime(quote.LeavingHome).ToString("yyyy-MM-dd");
                            user.quoteEndDate = Convert.ToDateTime(quote.TillDate).ToString("yyyy-MM-dd");

                            user.coverageArea = quote.TouristISOCODE == "US" ? "USA/CANADA" : "Other/International";//"USA/CANADA";

                            var option = new Models.INF_User_Primary();
                            option.dob = dateofbirth;//"1965-12-02";                   
                            user.primary.Add(option);
                            user.spouseDob = Convert.ToDateTime(spousedateofbirth).ToString("yyyy-MM-dd");

                            user.ageChild3 = quote.child3age;
                            user.ageChild4 = quote.child4age;



                            var json = JsonConverterINF.Serialize(user);
                            string INFGETQuote = ConfigurationManager.AppSettings["INFGETQuote"];
                            var t = Task.Run(() => PostRequest(json, INFGETQuote, AuthKey, Domainname));
                            t.Wait();
                            string JsonResult = t.Result;
                            ViewBag.Message = JsonResult;

                            var ObjINF_uatResult = new Models.INF_QuatResult();

                            System.Web.Script.Serialization.JavaScriptSerializer jsSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();

                            finalResult = jsSerializer.Deserialize<List<Models.INF_Quote>>(JsonResult);

                            var Appname = "";

                            foreach (Models.INF_Quote item in finalResult)
                            {
                                if (item.policyName == "INF STANDARD")
                                {
                                    Appname = "INFS";
                                }
                                else if (item.policyName == "INF TRAVELUSA")
                                {
                                    Appname = "INFT";
                                }
                                else if (item.policyName == "INF ELITE")
                                {
                                    Appname = "INFE";
                                }
                                else if (item.policyName == "INF PREMIER")
                                {
                                    Appname = "INFP";
                                }
                                if (item.policyName == "INF STANDARD" || item.policyName == "INF TRAVELUSA" || item.policyName == "INF ELITE" || item.policyName == "INF PREMIER")
                                {


                                    List<QuotationModel> listitem = quotationalmanager.GetPlanDetails(Appname);
                                    //string premium = String.Format("{0:0.00}", item.totalPremium + 15.00);
                                    double premium = item.totalPremium + 15.00;
                                    AllQuatationResult AllQuatationresult = new AllQuatationResult();
                                    AllQuatationresult.PlanId = item.planId;
                                    AllQuatationresult.policyName = item.policyName;
                                    AllQuatationresult.planDeductible = item.planDeductible;
                                    AllQuatationresult.preExDeductible = item.preExDeductible;
                                    AllQuatationresult.preExCoverageAmount = item.preExCoverageAmount;
                                    AllQuatationresult.totalPremium = premium;
                                    AllQuatationresult.planMaximum = item.planMaximum;
                                    AllQuatationresult.INsuranceComp = "INF";
                                    AllQuatationresult.BuyNowLink = "";
                                    AllQuatationresult.policyType = item.policyType;
                                    AllQuatationresult.prexded = item.prexded;
                                    AllQuatationresult.Appname = Appname;
                                    if (listitem.Count > 0)
                                    {


                                        AllQuatationresult.Plan_Type = listitem[0].Plan_Type;
                                        AllQuatationresult.CANCELABLE = listitem[0].CANCELABLE;
                                        AllQuatationresult.Company_Name = listitem[0].Company_Name;
                                        AllQuatationresult.COUNTRY_RESTRICTIONS = listitem[0].COUNTRY_RESTRICTIONS;
                                        AllQuatationresult.COVID_19 = listitem[0].COVID_19;
                                        AllQuatationresult.IN_NETWORK = listitem[0].IN_NETWORK;
                                        AllQuatationresult.OUT_OF_NETWORK = listitem[0].OUT_OF_NETWORK;
                                        AllQuatationresult.Plan_Type = listitem[0].Plan_Type;
                                        AllQuatationresult.PRE_EXISITING_CONDITION = listitem[0].PRE_EXISITING_CONDITION;
                                        AllQuatationresult.RENEWABLE = listitem[0].RENEWABLE;
                                        AllQuatationresult.Pre_Existing = listitem[0].Pre_Existing;
                                    }
                                    AllQuatationresult.NOOFDAYS = DATE.ToString();
                                    lstQuatationResult.Add(AllQuatationresult);
                                }
                                else
                                {

                                }
                            }

                            return finalResult;
                        }
                        else if (quote.child2age != "" && (quote.child3age != "" && quote.child4age != "") && (quote.child1age == "") && (quote.spouseage != ""))
                        {
                            var user = new Models.INF_Child4_Spouse_Child3_Child2();
                            user.quoteStartDate = Convert.ToDateTime(quote.LeavingHome).ToString("yyyy-MM-dd");
                            user.quoteEndDate = Convert.ToDateTime(quote.TillDate).ToString("yyyy-MM-dd");

                            user.coverageArea = quote.TouristISOCODE == "US" ? "USA/CANADA" : "Other/International";//"USA/CANADA";

                            var option = new Models.INF_User_Primary();
                            option.dob = dateofbirth;//"1965-12-02";                   
                            user.primary.Add(option);
                            user.spouseDob = Convert.ToDateTime(spousedateofbirth).ToString("yyyy-MM-dd");

                            user.ageChild2 = quote.child2age;
                            user.ageChild3 = quote.child3age;
                            user.ageChild4 = quote.child4age;



                            var json = JsonConverterINF.Serialize(user);
                            string INFGETQuote = ConfigurationManager.AppSettings["INFGETQuote"];
                            var t = Task.Run(() => PostRequest(json, INFGETQuote, AuthKey, Domainname));
                            t.Wait();
                            string JsonResult = t.Result;
                            ViewBag.Message = JsonResult;

                            var ObjINF_uatResult = new Models.INF_QuatResult();

                            System.Web.Script.Serialization.JavaScriptSerializer jsSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();

                            finalResult = jsSerializer.Deserialize<List<Models.INF_Quote>>(JsonResult);

                            var Appname = "";

                            foreach (Models.INF_Quote item in finalResult)
                            {
                                if (item.policyName == "INF STANDARD")
                                {
                                    Appname = "INFS";
                                }
                                else if (item.policyName == "INF TRAVELUSA")
                                {
                                    Appname = "INFT";
                                }
                                else if (item.policyName == "INF ELITE")
                                {
                                    Appname = "INFE";
                                }
                                else if (item.policyName == "INF PREMIER")
                                {
                                    Appname = "INFP";
                                }
                                if (item.policyName == "INF STANDARD" || item.policyName == "INF TRAVELUSA" || item.policyName == "INF ELITE" || item.policyName == "INF PREMIER")
                                {


                                    List<QuotationModel> listitem = quotationalmanager.GetPlanDetails(Appname);
                                    //string premium = String.Format("{0:0.00}", item.totalPremium + 15.00);
                                    double premium = item.totalPremium + 15.00;
                                    AllQuatationResult AllQuatationresult = new AllQuatationResult();
                                    AllQuatationresult.PlanId = item.planId;
                                    AllQuatationresult.policyName = item.policyName;
                                    AllQuatationresult.planDeductible = item.planDeductible;
                                    AllQuatationresult.preExDeductible = item.preExDeductible;
                                    AllQuatationresult.preExCoverageAmount = item.preExCoverageAmount;
                                    AllQuatationresult.totalPremium = premium;
                                    AllQuatationresult.planMaximum = item.planMaximum;
                                    AllQuatationresult.INsuranceComp = "INF";
                                    AllQuatationresult.BuyNowLink = "";
                                    AllQuatationresult.policyType = item.policyType;
                                    AllQuatationresult.prexded = item.prexded;
                                    AllQuatationresult.Appname = Appname;
                                    if (listitem.Count > 0)
                                    {


                                        AllQuatationresult.Plan_Type = listitem[0].Plan_Type;
                                        AllQuatationresult.CANCELABLE = listitem[0].CANCELABLE;
                                        AllQuatationresult.Company_Name = listitem[0].Company_Name;
                                        AllQuatationresult.COUNTRY_RESTRICTIONS = listitem[0].COUNTRY_RESTRICTIONS;
                                        AllQuatationresult.COVID_19 = listitem[0].COVID_19;
                                        AllQuatationresult.IN_NETWORK = listitem[0].IN_NETWORK;
                                        AllQuatationresult.OUT_OF_NETWORK = listitem[0].OUT_OF_NETWORK;
                                        AllQuatationresult.Plan_Type = listitem[0].Plan_Type;
                                        AllQuatationresult.PRE_EXISITING_CONDITION = listitem[0].PRE_EXISITING_CONDITION;
                                        AllQuatationresult.RENEWABLE = listitem[0].RENEWABLE;
                                        AllQuatationresult.Pre_Existing = listitem[0].Pre_Existing;
                                    }
                                    AllQuatationresult.NOOFDAYS = DATE.ToString();
                                    lstQuatationResult.Add(AllQuatationresult);
                                }
                                else
                                {

                                }
                            }

                            return finalResult;
                        }

                        //pc1c2c3
                        else if (quote.child2age != "" && (quote.child3age != "" && quote.child4age == "") && (quote.child1age != "") && (quote.spouseage == ""))
                        {
                            var user = new Models.INF_Child3_Child1_Child2();
                            user.quoteStartDate = Convert.ToDateTime(quote.LeavingHome).ToString("yyyy-MM-dd");
                            user.quoteEndDate = Convert.ToDateTime(quote.TillDate).ToString("yyyy-MM-dd");

                            user.coverageArea = quote.TouristISOCODE == "US" ? "USA/CANADA" : "Other/International";//"USA/CANADA";

                            var option = new Models.INF_User_Primary();
                            option.dob = dateofbirth;//"1965-12-02";                   
                            user.primary.Add(option);

                            user.ageChild1 = quote.child1age;
                            user.ageChild2 = quote.child2age;
                            user.ageChild3 = quote.child3age;



                            var json = JsonConverterINF.Serialize(user);
                            string INFGETQuote = ConfigurationManager.AppSettings["INFGETQuote"];
                            var t = Task.Run(() => PostRequest(json, INFGETQuote, AuthKey, Domainname));
                            t.Wait();
                            string JsonResult = t.Result;
                            ViewBag.Message = JsonResult;

                            var ObjINF_uatResult = new Models.INF_QuatResult();

                            System.Web.Script.Serialization.JavaScriptSerializer jsSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();

                            finalResult = jsSerializer.Deserialize<List<Models.INF_Quote>>(JsonResult);

                            var Appname = "";

                            foreach (Models.INF_Quote item in finalResult)
                            {
                                if (item.policyName == "INF STANDARD")
                                {
                                    Appname = "INFS";
                                }
                                else if (item.policyName == "INF TRAVELUSA")
                                {
                                    Appname = "INFT";
                                }
                                else if (item.policyName == "INF ELITE")
                                {
                                    Appname = "INFE";
                                }
                                else if (item.policyName == "INF PREMIER")
                                {
                                    Appname = "INFP";
                                }
                                if (item.policyName == "INF STANDARD" || item.policyName == "INF TRAVELUSA" || item.policyName == "INF ELITE" || item.policyName == "INF PREMIER")
                                {


                                    List<QuotationModel> listitem = quotationalmanager.GetPlanDetails(Appname);
                                    //string premium = String.Format("{0:0.00}", item.totalPremium + 15.00);
                                    double premium = item.totalPremium + 15.00;
                                    AllQuatationResult AllQuatationresult = new AllQuatationResult();
                                    AllQuatationresult.PlanId = item.planId;
                                    AllQuatationresult.policyName = item.policyName;
                                    AllQuatationresult.planDeductible = item.planDeductible;
                                    AllQuatationresult.preExDeductible = item.preExDeductible;
                                    AllQuatationresult.preExCoverageAmount = item.preExCoverageAmount;
                                    AllQuatationresult.totalPremium = premium;

                                    AllQuatationresult.planMaximum = item.planMaximum;
                                    AllQuatationresult.INsuranceComp = "INF";
                                    AllQuatationresult.BuyNowLink = "";
                                    AllQuatationresult.policyType = item.policyType;
                                    AllQuatationresult.prexded = item.prexded;
                                    AllQuatationresult.Appname = Appname;
                                    if (listitem.Count > 0)
                                    {


                                        AllQuatationresult.Plan_Type = listitem[0].Plan_Type;
                                        AllQuatationresult.CANCELABLE = listitem[0].CANCELABLE;
                                        AllQuatationresult.Company_Name = listitem[0].Company_Name;
                                        AllQuatationresult.COUNTRY_RESTRICTIONS = listitem[0].COUNTRY_RESTRICTIONS;
                                        AllQuatationresult.COVID_19 = listitem[0].COVID_19;
                                        AllQuatationresult.IN_NETWORK = listitem[0].IN_NETWORK;
                                        AllQuatationresult.OUT_OF_NETWORK = listitem[0].OUT_OF_NETWORK;
                                        AllQuatationresult.Plan_Type = listitem[0].Plan_Type;
                                        AllQuatationresult.PRE_EXISITING_CONDITION = listitem[0].PRE_EXISITING_CONDITION;
                                        AllQuatationresult.RENEWABLE = listitem[0].RENEWABLE;
                                        AllQuatationresult.Pre_Existing = listitem[0].Pre_Existing;
                                    }
                                    AllQuatationresult.NOOFDAYS = DATE.ToString();
                                    lstQuatationResult.Add(AllQuatationresult);
                                }
                                else
                                {

                                }
                            }

                            return finalResult;
                        }

                        //pc1c2c3c4
                        else if (quote.child2age != "" && (quote.child3age != "" && quote.child4age != "") && (quote.child1age != "") && (quote.spouseage == ""))
                        {
                            var user = new Models.INF_Child4_Child3_Child1_Child2();
                            user.quoteStartDate = Convert.ToDateTime(quote.LeavingHome).ToString("yyyy-MM-dd");
                            user.quoteEndDate = Convert.ToDateTime(quote.TillDate).ToString("yyyy-MM-dd");

                            user.coverageArea = quote.TouristISOCODE == "US" ? "USA/CANADA" : "Other/International";//"USA/CANADA";

                            var option = new Models.INF_User_Primary();
                            option.dob = dateofbirth;//"1965-12-02";                   
                            user.primary.Add(option);

                            user.ageChild1 = quote.child1age;
                            user.ageChild2 = quote.child2age;
                            user.ageChild3 = quote.child3age;
                            user.ageChild4 = quote.child4age;



                            var json = JsonConverterINF.Serialize(user);
                            string INFGETQuote = ConfigurationManager.AppSettings["INFGETQuote"];
                            var t = Task.Run(() => PostRequest(json, INFGETQuote, AuthKey, Domainname));
                            t.Wait();
                            string JsonResult = t.Result;
                            ViewBag.Message = JsonResult;

                            var ObjINF_uatResult = new Models.INF_QuatResult();

                            System.Web.Script.Serialization.JavaScriptSerializer jsSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();

                            finalResult = jsSerializer.Deserialize<List<Models.INF_Quote>>(JsonResult);

                            var Appname = "";

                            foreach (Models.INF_Quote item in finalResult)
                            {
                                if (item.policyName == "INF STANDARD")
                                {
                                    Appname = "INFS";
                                }
                                else if (item.policyName == "INF TRAVELUSA")
                                {
                                    Appname = "INFT";
                                }
                                else if (item.policyName == "INF ELITE")
                                {
                                    Appname = "INFE";
                                }
                                else if (item.policyName == "INF PREMIER")
                                {
                                    Appname = "INFP";
                                }
                                if (item.policyName == "INF STANDARD" || item.policyName == "INF TRAVELUSA" || item.policyName == "INF ELITE" || item.policyName == "INF PREMIER")
                                {


                                    List<QuotationModel> listitem = quotationalmanager.GetPlanDetails(Appname);
                                    //string premium = String.Format("{0:0.00}", item.totalPremium + 15.00);
                                    double premium = item.totalPremium + 15.00;
                                    AllQuatationResult AllQuatationresult = new AllQuatationResult();
                                    AllQuatationresult.PlanId = item.planId;
                                    AllQuatationresult.policyName = item.policyName;
                                    AllQuatationresult.planDeductible = item.planDeductible;
                                    AllQuatationresult.preExDeductible = item.preExDeductible;
                                    AllQuatationresult.preExCoverageAmount = item.preExCoverageAmount;
                                    AllQuatationresult.totalPremium = premium;
                                    AllQuatationresult.planMaximum = item.planMaximum;
                                    AllQuatationresult.INsuranceComp = "INF";
                                    AllQuatationresult.BuyNowLink = "";
                                    AllQuatationresult.policyType = item.policyType;
                                    AllQuatationresult.prexded = item.prexded;
                                    AllQuatationresult.Appname = Appname;
                                    if (listitem.Count > 0)
                                    {


                                        AllQuatationresult.Plan_Type = listitem[0].Plan_Type;
                                        AllQuatationresult.CANCELABLE = listitem[0].CANCELABLE;
                                        AllQuatationresult.Company_Name = listitem[0].Company_Name;
                                        AllQuatationresult.COUNTRY_RESTRICTIONS = listitem[0].COUNTRY_RESTRICTIONS;
                                        AllQuatationresult.COVID_19 = listitem[0].COVID_19;
                                        AllQuatationresult.IN_NETWORK = listitem[0].IN_NETWORK;
                                        AllQuatationresult.OUT_OF_NETWORK = listitem[0].OUT_OF_NETWORK;
                                        AllQuatationresult.Plan_Type = listitem[0].Plan_Type;
                                        AllQuatationresult.PRE_EXISITING_CONDITION = listitem[0].PRE_EXISITING_CONDITION;
                                        AllQuatationresult.RENEWABLE = listitem[0].RENEWABLE;
                                        AllQuatationresult.Pre_Existing = listitem[0].Pre_Existing;
                                    }
                                    AllQuatationresult.NOOFDAYS = DATE.ToString();
                                    lstQuatationResult.Add(AllQuatationresult);
                                }
                                else
                                {

                                }
                            }

                            return finalResult;
                        }
                        else
                        {

                            var user = new Models.INF_Spouse();
                            user.quoteStartDate = Convert.ToDateTime(quote.LeavingHome).ToString("yyyy-MM-dd");
                            user.quoteEndDate = Convert.ToDateTime(quote.TillDate).ToString("yyyy-MM-dd");

                            user.coverageArea = quote.TouristISOCODE == "US" ? "USA/CANADA" : "Other/International";//"USA/CANADA";

                            var option = new Models.INF_User_Primary();
                            option.dob = dateofbirth;//"1965-12-02";                   
                            user.primary.Add(option);
                            user.spouseDob = Convert.ToDateTime(spousedateofbirth).ToString("yyyy-MM-dd");





                            var json = JsonConverterINF.Serialize(user);
                            string INFGETQuote = ConfigurationManager.AppSettings["INFGETQuote"];
                            var t = Task.Run(() => PostRequest(json, INFGETQuote, AuthKey, Domainname));
                            t.Wait();
                            string JsonResult = t.Result;
                            ViewBag.Message = JsonResult;

                            var ObjINF_uatResult = new Models.INF_QuatResult();

                            System.Web.Script.Serialization.JavaScriptSerializer jsSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();

                            finalResult = jsSerializer.Deserialize<List<Models.INF_Quote>>(JsonResult);

                            var Appname = "";

                            foreach (Models.INF_Quote item in finalResult)
                            {
                                if (item.policyName == "INF STANDARD")
                                {
                                    Appname = "INFS";
                                }
                                else if (item.policyName == "INF TRAVELUSA")
                                {
                                    Appname = "INFT";
                                }
                                else if (item.policyName == "INF ELITE")
                                {
                                    Appname = "INFE";
                                }
                                else if (item.policyName == "INF PREMIER")
                                {
                                    Appname = "INFP";
                                }
                                if (item.policyName == "INF STANDARD" || item.policyName == "INF TRAVELUSA" || item.policyName == "INF ELITE" || item.policyName == "INF PREMIER")
                                {


                                    List<QuotationModel> listitem = quotationalmanager.GetPlanDetails(Appname);
                                    //string premium = String.Format("{0:0.00}", item.totalPremium + 15.00);
                                    double premium = item.totalPremium + 15.00;
                                    AllQuatationResult AllQuatationresult = new AllQuatationResult();
                                    AllQuatationresult.PlanId = item.planId;
                                    AllQuatationresult.policyName = item.policyName;
                                    AllQuatationresult.planDeductible = item.planDeductible;
                                    AllQuatationresult.preExDeductible = item.preExDeductible;
                                    AllQuatationresult.preExCoverageAmount = item.preExCoverageAmount;
                                    AllQuatationresult.totalPremium = premium;
                                    AllQuatationresult.planMaximum = item.planMaximum;
                                    AllQuatationresult.INsuranceComp = "INF";
                                    AllQuatationresult.BuyNowLink = "";
                                    AllQuatationresult.policyType = item.policyType;
                                    AllQuatationresult.prexded = item.prexded;
                                    AllQuatationresult.Appname = Appname;
                                    if (listitem.Count > 0)
                                    {


                                        AllQuatationresult.Plan_Type = listitem[0].Plan_Type;
                                        AllQuatationresult.CANCELABLE = listitem[0].CANCELABLE;
                                        AllQuatationresult.Company_Name = listitem[0].Company_Name;
                                        AllQuatationresult.COUNTRY_RESTRICTIONS = listitem[0].COUNTRY_RESTRICTIONS;
                                        AllQuatationresult.COVID_19 = listitem[0].COVID_19;
                                        AllQuatationresult.IN_NETWORK = listitem[0].IN_NETWORK;
                                        AllQuatationresult.OUT_OF_NETWORK = listitem[0].OUT_OF_NETWORK;
                                        AllQuatationresult.Plan_Type = listitem[0].Plan_Type;
                                        AllQuatationresult.PRE_EXISITING_CONDITION = listitem[0].PRE_EXISITING_CONDITION;
                                        AllQuatationresult.RENEWABLE = listitem[0].RENEWABLE;
                                        AllQuatationresult.Pre_Existing = listitem[0].Pre_Existing;
                                    }
                                    AllQuatationresult.NOOFDAYS = DATE.ToString();
                                    lstQuatationResult.Add(AllQuatationresult);
                                }
                                else
                                {

                                }
                            }

                            return finalResult;
                        }


                    }
                }
                else
                {

                    return new List<Models.INF_Quote>();
                }

            }
            catch (Exception ex)
            {
                quotationalmanager.SaveErrorLog("INF", ex.ToString());
                return new List<Models.INF_Quote>();
            }
        }
        public string Imgtoken()
        {
            string Domainname = ConfigurationManager.AppSettings["Domainname"];
            string AuthenticateINF = ConfigurationManager.AppSettings["AuthentificateIMG"];
            var token = Task.Run(() => GetData(AuthenticateINF, "grant_type=password&username=touristinsured@gmail.com&password=Password1"));
            token.Wait();
            string tokenValue1 = token.Result;



            var details = JObject.Parse(tokenValue1);
            string accesstoken = details["access_token"].ToString();
            string bearertype = details["token_type"].ToString();


            string AuthKey = tokenValue1;
            AuthKey = bearertype + " " + accesstoken;
            return AuthKey;
        }
        public int SearchIMG(string productCode, int deductible, int maximumlimit, QuotationModel req, string apptype, string AuthKey)
        {

            try
            {

                //Get IMGCode 
                string IMGTouristcode = quotationalmanager.GetIMGCountryCode(req.TouristISOCODE);
                string IMGISOCODE = quotationalmanager.GetIMGCountryCode(req.ISOCODE);
                string Domainname = ConfigurationManager.AppSettings["Domainname"];

                string DOB = "";
                if (req.TravelersDOB == null || req.TravelersDOB == "")
                {
                    int age = req.travelersage;
                    int getDOByear = Convert.ToInt32(DateTime.Now.Year) - age;
                    string getdate = DateTime.Now.Day.ToString();
                    string getmonth = DateTime.Now.Month.ToString();
                    DOB = getDOByear.ToString() + "-" + getmonth + "-" + getdate;
                }
                else
                {
                    DOB = Convert.ToDateTime(req.TravelersDOB).ToString("yyyy-MM-dd");
                }
                if (req.spouseage == null)
                {
                    req.spouseage = "";
                }
                if (req.child1age == null)
                {
                    req.child1age = "";
                }
                if (req.child3age == null)
                {
                    req.child3age = "";
                }
                if (req.child2age == null)
                {
                    req.child2age = "";
                }
                if (req.child4age == null)
                {
                    req.child4age = "";
                }
                string spouseDOB = "";
                if (req.spouseage != "")
                {
                    int age = Convert.ToInt32(req.spouseage);
                    int getDOByear = Convert.ToInt32(DateTime.Now.Year) - age;
                    string getdate = DateTime.Now.Day.ToString();
                    string getmonth = DateTime.Now.Month.ToString();
                    spouseDOB = Convert.ToDateTime(getDOByear.ToString() + "-" + getmonth + "-" + getdate).ToString("yyyy-MM-dd");
                }

                string childDOB = "";
                if (req.child1age != "")
                {
                    int age = Convert.ToInt32(req.child1age);
                    int getDOByear = Convert.ToInt32(DateTime.Now.Year) - age;
                    string getdate = DateTime.Now.Day.ToString();
                    string getmonth = DateTime.Now.Month.ToString();
                    childDOB = Convert.ToDateTime(getDOByear.ToString() + "-" + getmonth + "-" + getdate).ToString("yyyy-MM-dd");
                }
                string child2DOB = "";
                if (req.child2age != "")
                {
                    int age = Convert.ToInt32(req.child2age);
                    int getDOByear = Convert.ToInt32(DateTime.Now.Year) - age;
                    string getdate = DateTime.Now.Day.ToString();
                    string getmonth = DateTime.Now.Month.ToString();
                    child2DOB = Convert.ToDateTime(getDOByear.ToString() + "-" + getmonth + "-" + getdate).ToString("yyyy-MM-dd");
                }

                string child3DOB = "";
                if (req.child3age != "")
                {
                    int age = Convert.ToInt32(req.child3age);
                    int getDOByear = Convert.ToInt32(DateTime.Now.Year) - age;
                    string getdate = DateTime.Now.Day.ToString();
                    string getmonth = DateTime.Now.Month.ToString();
                    child3DOB = Convert.ToDateTime(getDOByear.ToString() + "-" + getmonth + "-" + getdate).ToString("yyyy-MM-dd");
                }
                string child4DOB = "";
                if (req.child4age != "")
                {
                    int age = Convert.ToInt32(req.child4age);
                    int getDOByear = Convert.ToInt32(DateTime.Now.Year) - age;
                    string getdate = DateTime.Now.Day.ToString();
                    string getmonth = DateTime.Now.Month.ToString();
                    child4DOB = Convert.ToDateTime(getDOByear.ToString() + "-" + getmonth + "-" + getdate).ToString("yyyy-MM-dd");
                }

                string fromdate = Convert.ToDateTime(req.LeavingHome).ToString("yyyy-MM-dd");
                string todate = Convert.ToDateTime(req.TillDate).ToString("yyyy-MM-dd");
                DateTime LeavingHome = DateTime.Parse(fromdate);
                DateTime TillDate = DateTime.Parse(todate);
                var DATE = (TillDate - LeavingHome).TotalDays;

                DATE = DATE + 1;
                //create TimeSpan object
                var user = new Models.IMG_Request();

                user.producerNumber = "538436";
                user.productCode = productCode;

                user.appType = apptype;
                user.numberOfDays = Convert.ToInt32(DATE);
                var travelinfo = new Models.TravelInfo();
                travelinfo.dateOfUsDeparture = Convert.ToDateTime(req.LeavingHome);
                travelinfo.destinations = new List<string>
                {
                    IMGTouristcode
                };
                travelinfo.startDate = Convert.ToDateTime(req.LeavingHome);
                travelinfo.endDate = Convert.ToDateTime(req.TillDate);
                user.travelInfo.Add(travelinfo);
                var policyinfo = new Models.PolicyInfo();
                policyinfo.deductible = Convert.ToInt32(deductible);
                policyinfo.maximumLimit = Convert.ToInt32(maximumlimit);
                policyinfo.currencyCode = "USD";
                policyinfo.fulfillmentMethod = "Online";

                user.policyInfo.Add(policyinfo);
                if ((req.spouseage == "") && (req.child1age == "") && (req.child2age == "") && (req.child3age == "") && (req.child4age == ""))
                {
                    user.families = new List<Models.Families>
                {
                    new Models.Families()
                    {
                        insureds=new List<Models.Insureds>
                        {
                            new Models.Insureds()
                            {
                               dateOfBirth=Convert.ToDateTime(DOB),


                                citizenship=IMGISOCODE,
                                residence =IMGISOCODE,
                                travelerType="Primary",
                                startDate=Convert.ToDateTime(req.LeavingHome),
                                endDate=Convert.ToDateTime(req.TillDate)
                            }

                        }
                    }


                };
                }
                else if ((req.spouseage != "") && (req.child1age != "") && (req.child2age == "") && (req.child3age == "") && (req.child4age == ""))
                {
                    user.families = new List<Models.Families>
                {
                    new Models.Families()
                    {
                        insureds=new List<Models.Insureds>
                        {
                            new Models.Insureds()
                            {
                               dateOfBirth=Convert.ToDateTime(DOB),


                                citizenship=IMGISOCODE,
                                residence =IMGISOCODE,
                                travelerType="Primary",
                                startDate=Convert.ToDateTime(req.LeavingHome),
                                endDate=Convert.ToDateTime(req.TillDate)
                            },
                            new Models.Insureds()
                            {
                               dateOfBirth=Convert.ToDateTime(spouseDOB),


                                citizenship=IMGISOCODE,
                                residence =IMGISOCODE,
                                travelerType="Spouse",
                                startDate=Convert.ToDateTime(req.LeavingHome),
                                endDate=Convert.ToDateTime(req.TillDate)
                            },
                            new Models.Insureds()
                            {
                               dateOfBirth=Convert.ToDateTime(childDOB),


                                citizenship=IMGISOCODE,
                                residence =IMGISOCODE,
                                travelerType="Child",
                                startDate=Convert.ToDateTime(req.LeavingHome),
                                endDate=Convert.ToDateTime(req.TillDate)
                            }

                        }

                    }


                };
                }
                else if ((req.spouseage != "") && (req.child1age != "") && (req.child2age != "") && (req.child3age == "") && (req.child4age == ""))
                {
                    user.families = new List<Models.Families>
                {
                    new Models.Families()
                    {
                        insureds=new List<Models.Insureds>
                        {
                            new Models.Insureds()
                            {
                               dateOfBirth=Convert.ToDateTime(DOB),


                                citizenship=IMGISOCODE,
                                residence =IMGISOCODE,
                                travelerType="Primary",
                                startDate=Convert.ToDateTime(req.LeavingHome),
                                endDate=Convert.ToDateTime(req.TillDate)
                            },
                            new Models.Insureds()
                            {
                               dateOfBirth=Convert.ToDateTime(spouseDOB),


                                citizenship=IMGISOCODE,
                                residence =IMGISOCODE,
                                travelerType="Spouse",
                                startDate=Convert.ToDateTime(req.LeavingHome),
                                endDate=Convert.ToDateTime(req.TillDate)
                            },
                            new Models.Insureds()
                            {
                               dateOfBirth=Convert.ToDateTime(childDOB),


                                citizenship=IMGISOCODE,
                                residence =IMGISOCODE,
                                travelerType="Child",
                                startDate=Convert.ToDateTime(req.LeavingHome),
                                endDate=Convert.ToDateTime(req.TillDate)
                            },
                            new Models.Insureds()
                            {
                               dateOfBirth=Convert.ToDateTime(child2DOB),


                                citizenship=IMGISOCODE,
                                residence =IMGISOCODE,
                                travelerType="Child",
                                startDate=Convert.ToDateTime(req.LeavingHome),
                                endDate=Convert.ToDateTime(req.TillDate)
                            }

                        }

                    }


                };
                }
                else if ((req.spouseage != "") && (req.child1age != "") && (req.child2age != "") && (req.child3age != "") && (req.child4age == ""))
                {
                    user.families = new List<Models.Families>
                {
                    new Models.Families()
                    {
                        insureds=new List<Models.Insureds>
                        {
                            new Models.Insureds()
                            {
                               dateOfBirth=Convert.ToDateTime(DOB),


                                citizenship=IMGISOCODE,
                                residence =IMGISOCODE,
                                travelerType="Primary",
                                startDate=Convert.ToDateTime(req.LeavingHome),
                                endDate=Convert.ToDateTime(req.TillDate)
                            },
                            new Models.Insureds()
                            {
                               dateOfBirth=Convert.ToDateTime(spouseDOB),


                                citizenship=IMGISOCODE,
                                residence =IMGISOCODE,
                                travelerType="Spouse",
                                startDate=Convert.ToDateTime(req.LeavingHome),
                                endDate=Convert.ToDateTime(req.TillDate)
                            },
                            new Models.Insureds()
                            {
                               dateOfBirth=Convert.ToDateTime(childDOB),


                                citizenship=IMGISOCODE,
                                residence =IMGISOCODE,
                                travelerType="Child",
                                startDate=Convert.ToDateTime(req.LeavingHome),
                                endDate=Convert.ToDateTime(req.TillDate)
                            },
                            new Models.Insureds()
                            {
                               dateOfBirth=Convert.ToDateTime(child2DOB),


                                citizenship=IMGISOCODE,
                                residence =IMGISOCODE,
                                travelerType="Child",
                                startDate=Convert.ToDateTime(req.LeavingHome),
                                endDate=Convert.ToDateTime(req.TillDate)
                            },
                            new Models.Insureds()
                            {
                               dateOfBirth=Convert.ToDateTime(child3DOB),


                                citizenship=IMGISOCODE,
                                residence =IMGISOCODE,
                                travelerType="Child",
                                startDate=Convert.ToDateTime(req.LeavingHome),
                                endDate=Convert.ToDateTime(req.TillDate)
                            }

                        }

                    }


                };
                }
                else if ((req.spouseage != "") && (req.child1age != "") && (req.child2age != "") && (req.child3age != "") && (req.child4age != ""))
                {
                    user.families = new List<Models.Families>
                {
                    new Models.Families()
                    {
                        insureds=new List<Models.Insureds>
                        {
                            new Models.Insureds()
                            {
                               dateOfBirth=Convert.ToDateTime(DOB),


                                citizenship=IMGISOCODE,
                                residence =IMGISOCODE,
                                travelerType="Primary",
                                startDate=Convert.ToDateTime(req.LeavingHome),
                                endDate=Convert.ToDateTime(req.TillDate)
                            },
                            new Models.Insureds()
                            {
                               dateOfBirth=Convert.ToDateTime(spouseDOB),


                                citizenship=IMGISOCODE,
                                residence =IMGISOCODE,
                                travelerType="Spouse",
                                startDate=Convert.ToDateTime(req.LeavingHome),
                                endDate=Convert.ToDateTime(req.TillDate)
                            },
                            new Models.Insureds()
                            {
                               dateOfBirth=Convert.ToDateTime(childDOB),


                                citizenship=IMGISOCODE,
                                residence =IMGISOCODE,
                                travelerType="Child",
                                startDate=Convert.ToDateTime(req.LeavingHome),
                                endDate=Convert.ToDateTime(req.TillDate)
                            },
                            new Models.Insureds()
                            {
                               dateOfBirth=Convert.ToDateTime(child2DOB),


                                citizenship=IMGISOCODE,
                                residence =IMGISOCODE,
                                travelerType="Child",
                                startDate=Convert.ToDateTime(req.LeavingHome),
                                endDate=Convert.ToDateTime(req.TillDate)
                            },
                            new Models.Insureds()
                            {
                               dateOfBirth=Convert.ToDateTime(child3DOB),


                                citizenship=IMGISOCODE,
                                residence =IMGISOCODE,
                                travelerType="Child",
                                startDate=Convert.ToDateTime(req.LeavingHome),
                                endDate=Convert.ToDateTime(req.TillDate)
                            },
                            new Models.Insureds()
                            {
                               dateOfBirth=Convert.ToDateTime(child4DOB),


                                citizenship=IMGISOCODE,
                                residence =IMGISOCODE,
                                travelerType="Child",
                                startDate=Convert.ToDateTime(req.LeavingHome),
                                endDate=Convert.ToDateTime(req.TillDate)
                            }

                        }

                    }


                };
                }

                else if ((req.spouseage == "") && (req.child1age != "") && (req.child2age == "") && (req.child3age == "") && (req.child4age == ""))
                {
                    user.families = new List<Models.Families>
                {
                    new Models.Families()
                    {
                        insureds=new List<Models.Insureds>
                        {
                            new Models.Insureds()
                            {
                               dateOfBirth=Convert.ToDateTime(DOB),


                                citizenship=IMGISOCODE,
                                residence =IMGISOCODE,
                                travelerType="Primary",
                                startDate=Convert.ToDateTime(req.LeavingHome),
                                endDate=Convert.ToDateTime(req.TillDate)
                            },

                            new Models.Insureds()
                            {
                               dateOfBirth=Convert.ToDateTime(childDOB),


                                citizenship=IMGISOCODE,
                                residence =IMGISOCODE,
                                travelerType="Child",
                                startDate=Convert.ToDateTime(req.LeavingHome),
                                endDate=Convert.ToDateTime(req.TillDate)
                            }

                        }

                    }


                };
                }
                else if ((req.spouseage == "") && (req.child1age != "") && (req.child2age != "") && (req.child3age == "") && (req.child4age == ""))
                {
                    user.families = new List<Models.Families>
                {
                    new Models.Families()
                    {
                        insureds=new List<Models.Insureds>
                        {
                            new Models.Insureds()
                            {
                               dateOfBirth=Convert.ToDateTime(DOB),


                                citizenship=IMGISOCODE,
                                residence =IMGISOCODE,
                                travelerType="Primary",
                                startDate=Convert.ToDateTime(req.LeavingHome),
                                endDate=Convert.ToDateTime(req.TillDate)
                            },

                            new Models.Insureds()
                            {
                               dateOfBirth=Convert.ToDateTime(childDOB),


                                citizenship=IMGISOCODE,
                                residence =IMGISOCODE,
                                travelerType="Child",
                                startDate=Convert.ToDateTime(req.LeavingHome),
                                endDate=Convert.ToDateTime(req.TillDate)
                            },
                            new Models.Insureds()
                            {
                               dateOfBirth=Convert.ToDateTime(child2DOB),


                                citizenship=IMGISOCODE,
                                residence =IMGISOCODE,
                                travelerType="Child",
                                startDate=Convert.ToDateTime(req.LeavingHome),
                                endDate=Convert.ToDateTime(req.TillDate)
                            }

                        }

                    }


                };
                }
                else if ((req.spouseage == "") && (req.child1age != "") && (req.child2age != "") && (req.child3age != "") && (req.child4age == ""))
                {
                    user.families = new List<Models.Families>
                {
                    new Models.Families()
                    {
                        insureds=new List<Models.Insureds>
                        {
                            new Models.Insureds()
                            {
                               dateOfBirth=Convert.ToDateTime(DOB),


                                citizenship=IMGISOCODE,
                                residence =IMGISOCODE,
                                travelerType="Primary",
                                startDate=Convert.ToDateTime(req.LeavingHome),
                                endDate=Convert.ToDateTime(req.TillDate)
                            },

                            new Models.Insureds()
                            {
                               dateOfBirth=Convert.ToDateTime(childDOB),


                                citizenship=IMGISOCODE,
                                residence =IMGISOCODE,
                                travelerType="Child",
                                startDate=Convert.ToDateTime(req.LeavingHome),
                                endDate=Convert.ToDateTime(req.TillDate)
                            },
                            new Models.Insureds()
                            {
                               dateOfBirth=Convert.ToDateTime(child2DOB),


                                citizenship=IMGISOCODE,
                                residence =IMGISOCODE,
                                travelerType="Child",
                                startDate=Convert.ToDateTime(req.LeavingHome),
                                endDate=Convert.ToDateTime(req.TillDate)
                            },
                            new Models.Insureds()
                            {
                               dateOfBirth=Convert.ToDateTime(child3DOB),


                                citizenship=IMGISOCODE,
                                residence =IMGISOCODE,
                                travelerType="Child",
                                startDate=Convert.ToDateTime(req.LeavingHome),
                                endDate=Convert.ToDateTime(req.TillDate)
                            }

                        }

                    }


                };
                }
                else if ((req.spouseage == "") && (req.child1age != "") && (req.child2age != "") && (req.child3age != "") && (req.child4age != ""))
                {
                    user.families = new List<Models.Families>
                {
                    new Models.Families()
                    {
                        insureds=new List<Models.Insureds>
                        {
                            new Models.Insureds()
                            {
                               dateOfBirth=Convert.ToDateTime(DOB),


                                citizenship=IMGISOCODE,
                                residence =IMGISOCODE,
                                travelerType="Primary",
                                startDate=Convert.ToDateTime(req.LeavingHome),
                                endDate=Convert.ToDateTime(req.TillDate)
                            },

                            new Models.Insureds()
                            {
                               dateOfBirth=Convert.ToDateTime(childDOB),


                                citizenship=IMGISOCODE,
                                residence =IMGISOCODE,
                                travelerType="Child",
                                startDate=Convert.ToDateTime(req.LeavingHome),
                                endDate=Convert.ToDateTime(req.TillDate)
                            },
                            new Models.Insureds()
                            {
                               dateOfBirth=Convert.ToDateTime(child2DOB),


                                citizenship=IMGISOCODE,
                                residence =IMGISOCODE,
                                travelerType="Child",
                                startDate=Convert.ToDateTime(req.LeavingHome),
                                endDate=Convert.ToDateTime(req.TillDate)
                            },
                            new Models.Insureds()
                            {
                               dateOfBirth=Convert.ToDateTime(child3DOB),


                                citizenship=IMGISOCODE,
                                residence =IMGISOCODE,
                                travelerType="Child",
                                startDate=Convert.ToDateTime(req.LeavingHome),
                                endDate=Convert.ToDateTime(req.TillDate)
                            },
                            new Models.Insureds()
                            {
                               dateOfBirth=Convert.ToDateTime(child4DOB),


                                citizenship=IMGISOCODE,
                                residence =IMGISOCODE,
                                travelerType="Child",
                                startDate=Convert.ToDateTime(req.LeavingHome),
                                endDate=Convert.ToDateTime(req.TillDate)
                            }

                        }

                    }


                };
                }

                else
                {
                    user.families = new List<Models.Families>
                {
                    new Models.Families()
                    {
                        insureds=new List<Models.Insureds>
                        {
                            new Models.Insureds()
                            {
                               dateOfBirth=Convert.ToDateTime(DOB),


                                citizenship=IMGISOCODE,
                                residence =IMGISOCODE,
                                travelerType="Primary",
                                startDate=Convert.ToDateTime(req.LeavingHome),
                                endDate=Convert.ToDateTime(req.TillDate)
                            },
                            new Models.Insureds()
                            {
                               dateOfBirth=Convert.ToDateTime(spouseDOB),


                                citizenship=IMGISOCODE,
                                residence =IMGISOCODE,
                                travelerType="Spouse",
                                startDate=Convert.ToDateTime(req.LeavingHome),
                                endDate=Convert.ToDateTime(req.TillDate)
                            }

                        }
                    }


                };
                }

                string jsonData = JsonConvert.SerializeObject(user, Formatting.Indented);
                string final = ReplaceFirstOccurrence(jsonData, "\"travelInfo\": [", "\"travelInfo\": ");
                final = ReplaceFirstOccurrence(final, "\"policyInfo\": [", "\"policyInfo\": ");
                final = ReplaceFirstOccurrence(final, "\r\n    }\r\n  ]", "\r\n    }\r\n ");
                final = ReplaceFirstOccurrence(final, "\r\n    }\r\n  ]", "\r\n    }\r\n ");
                var IMGGetquotes = ConfigurationManager.AppSettings["IMGQuote"];
                var t = Task.Run(() => PostRequest(final, IMGGetquotes, AuthKey, Domainname));
                t.Wait();
                string JsonResult = t.Result;



                System.Web.Script.Serialization.JavaScriptSerializer jsSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();

                var myDetails = JsonConvert.DeserializeObject<Models.IMG_Response>(JsonResult);

                var Appname = "";

                string Productname = "";
                if (productCode == "PATAP")
                {
                    Productname = "PATRIOT AMERICA PLUS";
                }
                else if (productCode == "PATAI" || productCode == "PATII")
                {

                    if (productCode == "PATII")
                    {
                        Productname = "PATRIOT INTERNATIONAL";
                    }
                    else
                    {
                        Productname = "PATRIOT AMERICA TRAVEL";
                    }

                }
                else if (productCode == "PPLAI" || productCode == "PPLII")
                {

                    if (productCode == "PPLII")
                    {
                        Productname = "PATRIOT PLATINUM INTERNATIONAL";
                    }
                    else
                    {
                        Productname = "PATRIOT PLATINUM AMERICA";
                    }
                }
                //else if (productCode == "EPSNI" || productCode == "EPSUI")
                //{
                //    Productname = "Patriot Exchange Program";
                //}
                else if (productCode == "VIC")
                {
                    if (apptype == "0521A")
                    {
                        Productname = "VISITORS CARE(PLAN A)";
                        productCode = "VICA";
                    }
                    else if (apptype == "0521B")
                    {
                        Productname = "VISITORS CARE(PLAN B)";
                        productCode = "VICB";
                    }
                    else if (apptype == "0521C")
                    {
                        Productname = "VISITORS CARE(PLAN C)";
                        productCode = "VICC";
                    }

                }
                List<QuotationModel> listitem = quotationalmanager.GetPlanDetails(productCode);
                //string premium = String.Format("{0:0.00}", myDetails.TotalPremium);
                double premium = myDetails.TotalPremium;
                AllQuatationResult AllQuatationresult = new AllQuatationResult();
                AllQuatationresult.PlanId = 1;
                AllQuatationresult.policyName = Productname;
                AllQuatationresult.planDeductible = deductible;
                AllQuatationresult.preExDeductible = 0.0;
                AllQuatationresult.preExCoverageAmount = 0.0;
                AllQuatationresult.totalPremium = premium;
                AllQuatationresult.planMaximum = maximumlimit;
                AllQuatationresult.INsuranceComp = "IMG";
                AllQuatationresult.BuyNowLink = "";
                AllQuatationresult.policyType = "";
                AllQuatationresult.prexded = "N";
                AllQuatationresult.Appname = productCode;
                if (listitem.Count > 0)
                {
                    AllQuatationresult.Plan_Type = listitem[0].Plan_Type;
                    AllQuatationresult.CANCELABLE = listitem[0].CANCELABLE;
                    AllQuatationresult.Company_Name = listitem[0].Company_Name;
                    AllQuatationresult.COUNTRY_RESTRICTIONS = listitem[0].COUNTRY_RESTRICTIONS;
                    AllQuatationresult.COVID_19 = listitem[0].COVID_19;
                    AllQuatationresult.Out_of_US = listitem[0].Out_of_US;
                    AllQuatationresult.IN_NETWORK = listitem[0].IN_NETWORK;
                    AllQuatationresult.OUT_OF_NETWORK = listitem[0].OUT_OF_NETWORK;
                    AllQuatationresult.Plan_Type = listitem[0].Plan_Type;
                    AllQuatationresult.PRE_EXISITING_CONDITION = listitem[0].PRE_EXISITING_CONDITION;
                    AllQuatationresult.RENEWABLE = listitem[0].RENEWABLE;
                    AllQuatationresult.Pre_Existing = listitem[0].Pre_Existing;
                }

                AllQuatationresult.NOOFDAYS = DATE.ToString();
                lstQuatationResult.Add(AllQuatationresult);

                return 1;
            }
            catch (Exception ex)
            {
                var error = ex;
                quotationalmanager.SaveErrorLog("IMG", error.ToString());
                return 0;
            }
        }

        public void GetDummyIMGValue(string productCode, int deductible, int maximumlimit, QuotationModel req, string apptype)
        {
            string Productname = "";
            if (productCode == "PATAP")
            {
                Productname = "PATRIOT AMERICA PLUS";
            }
            else if (productCode == "PATAI" || productCode == "PATII")
            {

                if (productCode == "PATII")
                {
                    Productname = "PATRIOT INTERNATIONAL";
                }
                else
                {
                    Productname = "PATRIOT AMERICA TRAVEL";
                }

            }
            else if (productCode == "PPLAI" || productCode == "PPLII")
            {

                if (productCode == "PPLII")
                {
                    Productname = "PATRIOT PLATINUM INTERNATIONAL";
                }
                else
                {
                    Productname = "PATRIOT PLATINUM AMERICA";
                }
            }
            //else if (productCode == "EPSNI" || productCode == "EPSUI")
            //{
            //    Productname = "Patriot Exchange Program";
            //}
            else if (productCode == "VIC")
            {
                if (apptype == "0521A")
                {
                    Productname = "VISITORS CARE(PLAN A)";
                    productCode = "VICA";
                }
                else if (apptype == "0521B")
                {
                    Productname = "VISITORS CARE(PLAN B)";
                    productCode = "VICB";
                }
                else if (apptype == "0521C")
                {
                    Productname = "VISITORS CARE(PLAN C)";
                    productCode = "VICC";
                }

            }

            AllQuatationResult AllQuatationresult = new AllQuatationResult();
            AllQuatationresult.PlanId = 1;
            AllQuatationresult.policyName = Productname;
            AllQuatationresult.planDeductible = deductible;
            AllQuatationresult.preExDeductible = 0.0;
            AllQuatationresult.preExCoverageAmount = 0.0;
            AllQuatationresult.totalPremium = 0.00;
            AllQuatationresult.planMaximum = maximumlimit;
            AllQuatationresult.INsuranceComp = "IMG";
            AllQuatationresult.BuyNowLink = "";
            AllQuatationresult.policyType = "";
            AllQuatationresult.prexded = "N";
            AllQuatationresult.Appname = productCode;
            lstQuatationResult.Add(AllQuatationresult);
        }
        private static async Task<string> PostRequest(string content, string url, string AuthKey, string baseurl)
        {
            try
            {
                //ServicePointManager.Expect100Continue = true;
                //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3 |
                //                                       SecurityProtocolType.Tls | SecurityProtocolType.Tls11;
                var data = new StringContent(content, Encoding.UTF8, "application/json");
                var client = new HttpClient();
                client.BaseAddress = new Uri(baseurl);
                client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
                client.DefaultRequestHeaders.Add("Authorization", AuthKey);
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                var response = await client.PostAsync(url, data);
                String ResponseJson = response.Content.ReadAsStringAsync().Result;
                return ResponseJson;
            }
            catch (Exception ex)
            {
                String ResponseJson = "";
                QuotationManager quotes = new QuotationManager();
                quotes.SaveErrorLog(url.Substring(8, 38), ex.ToString());
                return ResponseJson;
            }
        }

        public static async Task<string> GetData(string url, string data)
        {
            try
            {
                //ServicePointManager.Expect100Continue = true;
                //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3 |
                //                                       SecurityProtocolType.Tls | SecurityProtocolType.Tls11;


                HttpClient client = new HttpClient();


                //StringContent queryString = new StringContent(data);
                //                HttpResponseMessage response = await client.PostAsync(new Uri(url), queryString);
                var data1 = new StringContent(data, Encoding.UTF8, "application/json");
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                HttpResponseMessage response = await client.PostAsync(url, data1);

                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                return responseBody;
            }
            catch (Exception ex)
            {
                string responseBody = "";
                QuotationManager quotes = new QuotationManager();
                quotes.SaveErrorLog(url.Substring(8, 38), ex.ToString());
                return responseBody;
            }

        }
        private static async Task<string> PostRequestHcc(string content, string url, string baseurl)
        {
            try
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

            catch (Exception ex)
            {
                String ResponseJson = "";

                QuotationManager quotes = new QuotationManager();
                quotes.SaveErrorLog(url.Substring(0, 20), ex.ToString());
                return ResponseJson;
            }
        }
        public string DOBFun(int Age)
        {
            string DOB = Convert.ToDateTime(indianTime.AddYears(-(Age))).ToString("yyyy-MM-dd");
            return DOB;
        }
        public class AllQuatationResult
        {
            public long PlanId { get; set; }
            public double planDeductible { get; set; }
            public double planMaximum { get; set; }
            public double preExDeductible { get; set; }
            public double preExCoverageAmount { get; set; }
            public double totalPremium { get; set; }
            public string INsuranceComp { get; set; }
            public string policyName { get; set; }
            public string BuyNowLink { get; set; }
            public string policyType { get; set; }
            public string prexded { get; set; }
            public string Appname { get; set; }
            public string Plan_Type { get; set; }
            public string PRE_EXISITING_CONDITION { get; set; }
            public string RENEWABLE { get; set; }
            public string CANCELABLE { get; set; }
            public string IN_NETWORK { get; set; }
            public string OUT_OF_NETWORK { get; set; }
            public string COUNTRY_RESTRICTIONS { get; set; }
            public string COVID_19 { get; set; }
            public string Company_Name { get; set; }
            public string NOOFDAYS { get; set; }
            public string Pre_Existing { get; set; }
            public string Out_of_US { get; set; }
        }
        public class TrawickColumn
        {
            public string ColumnName { get; set; }
            public int deductable { get; set; }
        }

        static string CalculateYourAge(DateTime Dob)
        {
            DateTime Now = DateTime.Now;
            int Years = new DateTime(DateTime.Now.Subtract(Dob).Ticks).Year - 1;
            DateTime PastYearDate = Dob.AddYears(Years);
            int Months = 0;
            for (int i = 1; i <= 12; i++)
            {
                if (PastYearDate.AddMonths(i) == Now)
                {
                    Months = i;
                    break;
                }
                else if (PastYearDate.AddMonths(i) >= Now)
                {
                    Months = i - 1;
                    break;
                }
            }
            int Days = Now.Subtract(PastYearDate.AddMonths(Months)).Days;
            int Hours = Now.Subtract(PastYearDate).Hours;
            int Minutes = Now.Subtract(PastYearDate).Minutes;
            int Seconds = Now.Subtract(PastYearDate).Seconds;
            return String.Format("Age: {0} Year(s) {1} Month(s) {2} Day(s) {3} Hour(s) {4} Second(s)",
            Years, Months, Days, Hours, Seconds);
        }
        private static int CalculateAge(DateTime dateOfBirth)
        {
            int age = 0;
            age = DateTime.Now.Year - dateOfBirth.Year;
            if (DateTime.Now.DayOfYear < dateOfBirth.DayOfYear)
                age = age;

            return age;
        }

        public void GetdummyVisitorsvalue(string deductible, string Planname, QuotationModel req, int age)
        {
            string Plan = string.Empty;
            string policymax = string.Empty;
            string policyname = "VISITOR SECURE";
            if (Planname == "A")
            {
                Plan = "50";
                policymax = "50000";
            }
            else if (Planname == "B")
            {
                Plan = "75";
                policymax = "75000";
            }
            else if (Planname == "C")
            {
                Plan = "100";
                policymax = "100000";
            }
            else if (Planname == "D")
            {
                Plan = "130";
                policymax = "130000";
            }
            string Appname = "";


            if (req.TouristISOCODE == "US")
            {
                Appname = "VIS";
                policyname = "VISITOR SECURE";
            }
            else
            {
                Appname = "VISI";
                policyname = "VISITOR SECURE INTERNATIONAL";

            }
            AllQuatationResult AllQuatationresult = new AllQuatationResult();
            AllQuatationresult.PlanId = Convert.ToInt64(1);
            AllQuatationresult.policyName = policyname;
            AllQuatationresult.planDeductible = Convert.ToDouble(deductible);
            AllQuatationresult.preExDeductible = 0.0;
            AllQuatationresult.preExCoverageAmount = 0.0;
            AllQuatationresult.totalPremium = 0.00;
            AllQuatationresult.planMaximum = Convert.ToDouble(policymax);
            AllQuatationresult.INsuranceComp = "Atlas";
            AllQuatationresult.BuyNowLink = "";
            AllQuatationresult.policyType = "";
            AllQuatationresult.prexded = "N";
            AllQuatationresult.Appname = Appname;
            lstQuatationResult.Add(AllQuatationresult);
        }
        public string SearchVisitorSecure(string deductible, string Planname, QuotationModel req, int age)
        {
            try
            {
                DateTime LeavingHome = DateTime.Parse(req.LeavingHome);
                DateTime TillDate = DateTime.Parse(req.TillDate);
                var DATE = (TillDate - LeavingHome).TotalDays;
                DATE = DATE + 1;
                bool dest = true;
                string Plan = string.Empty;
                string policymax = string.Empty;

                if (Planname == "A")
                {
                    Plan = "50";
                    policymax = "50000";
                }
                else if (Planname == "B")
                {
                    Plan = "75";
                    policymax = "75000";
                }
                else if (Planname == "C")
                {
                    Plan = "100";
                    policymax = "100000";
                }
                else if (Planname == "D")
                {
                    Plan = "130";
                    policymax = "130000";
                }
                if (req.TouristISOCODE == "US")
                {
                    dest = true;
                }
                else
                {
                    dest = false;
                }
                string dateofbirth = "";
                if (req.TravelersDOB == null || req.TravelersDOB == "")
                {
                    int ageoftraveler = req.travelersage;
                    int getDOByear = Convert.ToInt32(DateTime.Now.Year) - ageoftraveler;
                    string getdate = DateTime.Now.Day.ToString();
                    string getmonth = DateTime.Now.Month.ToString();

                    dateofbirth = getmonth + "/" + getdate + "/" + getDOByear.ToString();
                }
                else
                {
                    dateofbirth = Convert.ToDateTime(req.TravelersDOB).ToString("MM/dd/yyyy");
                }
                string sposedateofbirth = "";
                if (req.spouseage == null)
                {
                    req.spouseage = "";
                }
                if (req.child1age == null)
                {
                    req.child1age = "";
                }
                if (req.child3age == null)
                {
                    req.child3age = "";
                }
                if (req.child2age == null)
                {
                    req.child2age = "";
                }
                if (req.child4age == null)
                {
                    req.child4age = "";
                }
                if (req.spouseage != "")
                {
                    int ageofspouse = Convert.ToInt32(req.spouseage);
                    int getDOByear = Convert.ToInt32(DateTime.Now.Year) - ageofspouse;
                    string getdate = DateTime.Now.Day.ToString();
                    string getmonth = DateTime.Now.Month.ToString();

                    sposedateofbirth = getmonth + "/" + getdate + "/" + getDOByear.ToString();
                }


                string childdateofbirth = "";
                if (req.child1age != "")
                {
                    int ageofchild1 = Convert.ToInt32(req.child1age);
                    int getDOByear = Convert.ToInt32(DateTime.Now.Year) - ageofchild1;
                    string getdate = DateTime.Now.Day.ToString();
                    string getmonth = DateTime.Now.Month.ToString();

                    childdateofbirth = /*Convert.ToDateTime(*/getmonth + "/" + getdate + "/" + getDOByear.ToString()/*).ToString("MM/dd/yyyy")*/;
                }
                string child2dateofbirth = "";
                if (req.child2age != "")
                {
                    int ageofchild2 = Convert.ToInt32(req.child2age);
                    int getDOByear = Convert.ToInt32(DateTime.Now.Year) - ageofchild2;
                    string getdate = DateTime.Now.Day.ToString();
                    string getmonth = DateTime.Now.Month.ToString();

                    child2dateofbirth =/* Convert.ToDateTime(*/getmonth + "/" + getdate + "/" + getDOByear.ToString()/*).ToString("MM/dd/yyyy")*/;
                }

                string child3dateofbirth = "";
                if (req.child3age != "")
                {
                    int ageofchild1 = Convert.ToInt32(req.child3age);
                    int getDOByear = Convert.ToInt32(DateTime.Now.Year) - ageofchild1;
                    string getdate = DateTime.Now.Day.ToString();
                    string getmonth = DateTime.Now.Month.ToString();

                    child3dateofbirth = /*Convert.ToDateTime(*/getmonth + "/" + getdate + "/" + getDOByear.ToString()/*).ToString("MM/dd/yyyy")*/;
                }
                string child4dateofbirth = "";
                if (req.child4age != "")
                {
                    int ageofchild2 = Convert.ToInt32(req.child4age);
                    int getDOByear = Convert.ToInt32(DateTime.Now.Year) - ageofchild2;
                    string getdate = DateTime.Now.Day.ToString();
                    string getmonth = DateTime.Now.Month.ToString();

                    child4dateofbirth = /*Convert.ToDateTime(*/getmonth + "/" + getdate + "/" + getDOByear.ToString()/*).ToString("MM/dd/yyyy")*/;
                }

                var Quote = new Models.HCC_User();
                Quote.ReferId = "9800";
                Quote.Culture = "en-US";
                Quote.USDest = dest;
                Quote.CoverageBeginDt = Convert.ToDateTime(req.LeavingHome).ToString("MM/dd/yyyy");// "04/15/2020";//
                Quote.CoverageEndDt = Convert.ToDateTime(req.TillDate).ToString("MM/dd/yyyy");//"05/16/2020"; // 
                Quote.ArriveInUSDate = Convert.ToDateTime(req.LeavingHome).ToString("MM/dd/yyyy");

                Quote.Deductible = deductible; //"250";//
                Quote.Plan = Plan; //"AP";//
                Quote.PlanName = Planname;

                Quote.MailOpt = "regular";
                Quote.SurplusLines = false;

                //Quote.ApplicationList[0].Dob = req.TravelersDOB;
                //var Dob = new Models.ApplicantList();
                //Dob.Dob = age;
                string child1age = req.child1age;
                if ((req.spouseage == "") && ((req.child1age == "") && (req.child2age == "")) && req.child3age == "" && req.child4age == "")
                {
                    Quote.ApplicantList = new List<Models.ApplicantList>
                          {
                             new Models.ApplicantList {Dob=dateofbirth
                          }

                          };
                }
                else if (((child1age != "") && (req.child2age == "")) && (req.spouseage != "") && req.child3age == "" && req.child4age == "")
                {
                    Quote.ApplicantList = new List<Models.ApplicantList>
                          {
                             new Models.ApplicantList {Dob=dateofbirth
                          },
                             new Models.ApplicantList {Dob=sposedateofbirth
                          },
                             new Models.ApplicantList {Dob=childdateofbirth
                          }

                          };
                }
                else if (((req.child1age != "") && (req.child2age != "")) && (req.spouseage != "") && req.child3age == "" && req.child4age == "")
                {
                    Quote.ApplicantList = new List<Models.ApplicantList>
                          {
                             new Models.ApplicantList {Dob=dateofbirth
                          },
                             new Models.ApplicantList {Dob=sposedateofbirth
                          },
                             new Models.ApplicantList {Dob=childdateofbirth
                          },
                              new Models.ApplicantList {Dob=child2dateofbirth
                          }

                          };
                }
                else if (((req.child1age != "") && (req.child2age != "")) && (req.spouseage != "") && req.child3age != "" && req.child4age == "")
                {
                    Quote.ApplicantList = new List<Models.ApplicantList>
                          {
                             new Models.ApplicantList {Dob=dateofbirth
                          },
                             new Models.ApplicantList {Dob=sposedateofbirth
                          },
                             new Models.ApplicantList {Dob=childdateofbirth
                          },
                              new Models.ApplicantList {Dob=child2dateofbirth
                          },
                              new Models.ApplicantList {Dob=child3dateofbirth
                          }

                          };
                }
                else if (((req.child1age != "") && (req.child2age != "")) && (req.spouseage != "") && req.child3age != "" && req.child4age != "")
                {
                    Quote.ApplicantList = new List<Models.ApplicantList>
                          {
                             new Models.ApplicantList {Dob=dateofbirth
                          },
                             new Models.ApplicantList {Dob=sposedateofbirth
                          },
                             new Models.ApplicantList {Dob=childdateofbirth
                          },
                              new Models.ApplicantList {Dob=child2dateofbirth
                          },
                              new Models.ApplicantList {Dob=child3dateofbirth
                          },
                              new Models.ApplicantList {Dob=child4dateofbirth
                          }

                          };
                }
                else if ((req.spouseage != "") && ((req.child1age == "") && (req.child2age == "")) && req.child3age == "" && req.child4age == "")
                {
                    Quote.ApplicantList = new List<Models.ApplicantList>
                          {
                             new Models.ApplicantList {Dob=dateofbirth
                          },
                             new Models.ApplicantList {Dob=sposedateofbirth
                          }

                          };
                }

                else if (((child1age != "") && (req.child2age == "")) && (req.spouseage == "") && req.child3age == "" && req.child4age == "")
                {
                    Quote.ApplicantList = new List<Models.ApplicantList>
                          {
                             new Models.ApplicantList {Dob=dateofbirth
                          },

                             new Models.ApplicantList {Dob=childdateofbirth
                          }

                          };
                }
                else if (((req.child1age != "") && (req.child2age != "")) && (req.spouseage == "") && req.child3age == "" && req.child4age == "")
                {
                    Quote.ApplicantList = new List<Models.ApplicantList>
                          {
                             new Models.ApplicantList {Dob=dateofbirth
                          },

                             new Models.ApplicantList {Dob=childdateofbirth
                          },
                              new Models.ApplicantList {Dob=child2dateofbirth
                          }

                          };
                }
                else if (((req.child1age != "") && (req.child2age != "")) && (req.spouseage == "") && req.child3age != "" && req.child4age == "")
                {
                    Quote.ApplicantList = new List<Models.ApplicantList>
                          {
                             new Models.ApplicantList {Dob=dateofbirth
                          },

                             new Models.ApplicantList {Dob=childdateofbirth
                          },
                              new Models.ApplicantList {Dob=child2dateofbirth
                          },
                              new Models.ApplicantList {Dob=child3dateofbirth
                          }

                          };
                }
                else if (((req.child1age != "") && (req.child2age != "")) && (req.spouseage == "") && req.child3age != "" && req.child4age != "")
                {
                    Quote.ApplicantList = new List<Models.ApplicantList>
                          {
                             new Models.ApplicantList {Dob=dateofbirth
                          },

                             new Models.ApplicantList {Dob=childdateofbirth
                          },
                              new Models.ApplicantList {Dob=child2dateofbirth
                          },
                              new Models.ApplicantList {Dob=child3dateofbirth
                          },
                              new Models.ApplicantList {Dob=child4dateofbirth
                          }

                          };
                }

                var json = JsonConverter.Serialize(Quote);
                string VisitorsQuote = ConfigurationManager.AppSettings["VisitorsQuote"];
                string Domainname = ConfigurationManager.AppSettings["Domainname"];
                var t = Task.Run(() => PostRequestHcc(json, VisitorsQuote, Domainname));
                t.Wait();
                string JsonResult = t.Result;
                ViewBag.Message = JsonResult;
                System.Web.Script.Serialization.JavaScriptSerializer jsSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();


                var reserializedJSON = "";
                using (var sr = new StringReader(JsonResult))
                using (var jr = new JsonTextReader(sr))
                {
                    var serial = new JsonSerializer();
                    serial.Formatting = Formatting.Indented;
                    var obj = serial.Deserialize<Models.Hcc_Quote>(jr);

                    reserializedJSON = JsonConvert.SerializeObject(obj, Formatting.Indented);

                }
                //var finalResult = new JavaScriptSerializer().Deserialize<Models.HccAtlasQuote>(reserializedJSON);
                var finalResult = jsSerializer.Deserialize<Models.Hcc_Quote>(reserializedJSON);
                string Appname = "";
                string policyname = "";
                if (req.TouristISOCODE == "US")
                {
                    Appname = "VIS";
                    policyname = "VISITOR SECURE";
                }
                else
                {
                    Appname = "VISI";
                    policyname = "VISITOR SECURE INTERNATIONAL";

                }
                List<QuotationModel> listitem = quotationalmanager.GetPlanDetails(Appname);
                //string premium = String.Format("{0:0.00}", finalResult.Total);
                double premium = finalResult.Total;
                AllQuatationResult AllQuatationresult = new AllQuatationResult();
                AllQuatationresult.PlanId = Convert.ToInt64(1);
                AllQuatationresult.policyName = policyname;
                AllQuatationresult.planDeductible = Convert.ToDouble(deductible);
                AllQuatationresult.preExDeductible = 0.0;
                AllQuatationresult.preExCoverageAmount = 0.0;
                AllQuatationresult.totalPremium = premium;
                AllQuatationresult.planMaximum = Convert.ToDouble(policymax);
                AllQuatationresult.INsuranceComp = "Vistorsecure";
                AllQuatationresult.BuyNowLink = "";
                AllQuatationresult.policyType = "";
                AllQuatationresult.prexded = "N";
                AllQuatationresult.Appname = Appname;
                if (listitem.Count > 0)
                {


                    AllQuatationresult.Plan_Type = listitem[0].Plan_Type;
                    AllQuatationresult.CANCELABLE = listitem[0].CANCELABLE;
                    AllQuatationresult.Company_Name = listitem[0].Company_Name;
                    AllQuatationresult.COUNTRY_RESTRICTIONS = listitem[0].COUNTRY_RESTRICTIONS;
                    AllQuatationresult.COVID_19 = listitem[0].COVID_19;
                    AllQuatationresult.Out_of_US = listitem[0].Out_of_US;
                    AllQuatationresult.IN_NETWORK = listitem[0].IN_NETWORK;
                    AllQuatationresult.OUT_OF_NETWORK = listitem[0].OUT_OF_NETWORK;
                    AllQuatationresult.Plan_Type = listitem[0].Plan_Type;
                    AllQuatationresult.PRE_EXISITING_CONDITION = listitem[0].PRE_EXISITING_CONDITION;
                    AllQuatationresult.RENEWABLE = listitem[0].RENEWABLE;
                    AllQuatationresult.NOOFDAYS = DATE.ToString();
                    AllQuatationresult.Pre_Existing = listitem[0].Pre_Existing;
                }
                lstQuatationResult.Add(AllQuatationresult);

                return "";
            }
            catch (Exception ex)
            {
                quotationalmanager.SaveErrorLog("Atlas", ex.ToString());
                return "";
            }
        }

        private static bool IsValidJson(string strInput)
        {
            strInput = strInput.Trim();
            if ((strInput.StartsWith("{") && strInput.EndsWith("}")) || //For object
                (strInput.StartsWith("[") && strInput.EndsWith("]"))) //For array
            {
                try
                {
                    var obj = JToken.Parse(strInput);
                    return true;
                }
                catch (JsonReaderException jex)
                {
                    //Exception in parsing json
                    Console.WriteLine(jex.Message);
                    return false;
                }
                catch (Exception ex) //some other exception
                {
                    Console.WriteLine(ex.ToString());
                    return false;
                }
            }
            else
            {
                return false;
            }
        }




        public ActionResult PremiumamountforIMG(FormCollection form)
        {
            string apptype = "";
            string productCode = "";
            string PolicyName = form["PolicyName"];
            if (PolicyName == "PATRIOT AMERICA PLUS")
            {
                if (form["hdnTouristcode"] == "US")
                {
                    productCode = "PATAP";
                    apptype = "0521";
                }

            }
            else if (PolicyName == "PATRIOT AMERICA TRAVEL" || PolicyName == "PATRIOT INTERNATIONAL")
            {
                if (form["hdnTouristcode"] == "US")
                {
                    productCode = "PATAI";
                    apptype = "0521";
                }
                else
                {
                    productCode = "PATII";
                    apptype = "0521";
                }

            }
            else if (PolicyName == "PATRIOT PLATINUM AMERICA" || PolicyName == "PATRIOT PLATINUM INTERNATIONAL")
            {
                if (form["hdnTouristcode"] == "US")
                {
                    productCode = "PPLAI";
                    apptype = "0521";
                }
                else
                {
                    productCode = "PPLII";
                    apptype = "0521";
                }
            }
            else if (PolicyName == "PATRIOT EXCHANGE PROGRAM")
            {
                if (form["hdnTouristcode"] == "US")
                {
                    productCode = "EPSNI";
                    apptype = "0619";
                }
                else
                {
                    productCode = "EPSUI";
                    apptype = "0619";
                }

            }
            else if (PolicyName == "VISITORS CARE(PLAN A)")
            {
                productCode = "VIC";
                if (form["selectedCoveragevalue"] == "25000" || form["selectedCoveragevalue"] == "10000")
                {
                    apptype = "0521A";
                }
            }
            else if (PolicyName == "VISITORS CARE(PLAN B)")
            {
                productCode = "VIC";
                if (form["selectedCoveragevalue"] == "50000")
                {
                    apptype = "0521B";
                }
            }
            else if (PolicyName == "VISITORS CARE(PLAN C)")
            {
                productCode = "VIC";
                apptype = "0521C";
            }


            string IMGTouristcode = quotationalmanager.GetIMGCountryCode(form["hdnTouristcode"]);
            string IMGISOCODE = quotationalmanager.GetIMGCountryCode(form["hdnISOCode"]);
            string Domainname = ConfigurationManager.AppSettings["Domainname"];

            string DOB = "";
            if (form["hdnTravelersDOB"] == null || form["hdnTravelersDOB"] == "")
            {
                int age = Convert.ToInt32(form["age"]);
                int getDOByear = Convert.ToInt32(DateTime.Now.Year) - age;
                string getdate = DateTime.Now.Day.ToString();
                string getmonth = DateTime.Now.Month.ToString();
                DOB = getDOByear.ToString() + "-" + getmonth + "-" + getdate;
            }
            else
            {
                DOB = form["hdnTravelersDOB"];
            }


            if (form["txtspouseage"] == null)
            {
                form["txtspouseage"] = "";
            }
            if (form["txtchild1age"] == null)
            {
                form["txtchild1age"] = "";
            }
            if (form["txtchild2age"] == null)
            {
                form["txtchild2age"] = "";
            }
            if (form["txtchild3age"] == null)
            {
                form["txtchild3age"] = "";
            }
            if (form["txtchild4age"] == null)
            {
                form["txtchild4age"] = "";
            }

            string spouseDOB = "";
            if (form["txtspouseage"] != "")
            {
                int age = Convert.ToInt32(form["txtspouseage"]);
                int getDOByear = Convert.ToInt32(DateTime.Now.Year) - age;
                string getdate = DateTime.Now.Day.ToString();
                string getmonth = DateTime.Now.Month.ToString();
                spouseDOB = Convert.ToDateTime(getDOByear.ToString() + "-" + getmonth + "-" + getdate).ToString("yyyy-MM-dd");
            }

            string childDOB = "";
            if (form["txtchild1age"] != "")
            {
                int age = Convert.ToInt32(form["txtchild1age"]);
                int getDOByear = Convert.ToInt32(DateTime.Now.Year) - age;
                string getdate = DateTime.Now.Day.ToString();
                string getmonth = DateTime.Now.Month.ToString();
                childDOB = Convert.ToDateTime(getDOByear.ToString() + "-" + getmonth + "-" + getdate).ToString("yyyy-MM-dd");
            }
            string child2DOB = "";
            if (form["txtchild2age"] != "")
            {
                int age = Convert.ToInt32(form["txtchild2age"]);
                int getDOByear = Convert.ToInt32(DateTime.Now.Year) - age;
                string getdate = DateTime.Now.Day.ToString();
                string getmonth = DateTime.Now.Month.ToString();
                child2DOB = Convert.ToDateTime(getDOByear.ToString() + "-" + getmonth + "-" + getdate).ToString("yyyy-MM-dd");
            }
            string child3DOB = "";
            if (form["txtchild3age"] != "")
            {
                int age = Convert.ToInt32(form["txtchild3age"]);
                int getDOByear = Convert.ToInt32(DateTime.Now.Year) - age;
                string getdate = DateTime.Now.Day.ToString();
                string getmonth = DateTime.Now.Month.ToString();
                child3DOB = Convert.ToDateTime(getDOByear.ToString() + "-" + getmonth + "-" + getdate).ToString("yyyy-MM-dd");
            }
            string child4DOB = "";
            if (form["txtchild4age"] != "")
            {
                int age = Convert.ToInt32(form["txtchild4age"]);
                int getDOByear = Convert.ToInt32(DateTime.Now.Year) - age;
                string getdate = DateTime.Now.Day.ToString();
                string getmonth = DateTime.Now.Month.ToString();
                child4DOB = Convert.ToDateTime(getDOByear.ToString() + "-" + getmonth + "-" + getdate).ToString("yyyy-MM-dd");
            }


            string AuthenticateINF = ConfigurationManager.AppSettings["AuthentificateIMG"];
            var token = Task.Run(() => GetData(AuthenticateINF, "grant_type=password&username=touristinsured@gmail.com&password=Password1"));
            token.Wait();
            string tokenValue1 = token.Result;




            var details = JObject.Parse(tokenValue1);
            string accesstoken = details["access_token"].ToString();
            string bearertype = details["token_type"].ToString();


            string AuthKey = tokenValue1;
            AuthKey = bearertype + " " + accesstoken;

            string fromdate = Convert.ToDateTime(form["hdnLeavingHome"]).ToString("yyyy-MM-dd");
            string todate = Convert.ToDateTime(form["hdnTillDate"]).ToString("yyyy-MM-dd");
            DateTime LeavingHome = DateTime.Parse(fromdate);
            DateTime TillDate = DateTime.Parse(todate);
            var DATE = (TillDate - LeavingHome).TotalDays;
            DATE = DATE + 1;

            //create TimeSpan object
            var user = new Models.IMG_Request();

            user.producerNumber = "538436";
            user.productCode = productCode;

            user.appType = apptype;
            user.numberOfDays = Convert.ToInt32(DATE);
            var travelinfo = new Models.TravelInfo();
            travelinfo.dateOfUsDeparture = Convert.ToDateTime(form["hdnLeavingHome"]);
            travelinfo.destinations = new List<string>
                {
                    IMGTouristcode
                };
            travelinfo.startDate = Convert.ToDateTime(form["hdnLeavingHome"]);
            travelinfo.endDate = Convert.ToDateTime(form["hdnTillDate"]);
            user.travelInfo.Add(travelinfo);
            var policyinfo = new Models.PolicyInfo();
            policyinfo.deductible = Convert.ToInt32(form["selectedDeductiblevalue"]);
            policyinfo.maximumLimit = Convert.ToInt32(form["selectedCoveragevalue"]);
            policyinfo.currencyCode = "USD";
            policyinfo.fulfillmentMethod = "Online";

            user.policyInfo.Add(policyinfo);

            if ((form["txtspouseage"] == "") && (form["txtchild1age"] == "") && (form["txtchild2age"] == "") && (form["txtchild3age"] == "") && (form["txtchild4age"] == ""))
            {
                user.families = new List<Models.Families>
                {
                    new Models.Families()
                    {
                        insureds=new List<Models.Insureds>
                        {
                            new Models.Insureds()
                            {
                               dateOfBirth=Convert.ToDateTime(DOB),


                                citizenship=IMGISOCODE,
                                residence =IMGISOCODE,
                                travelerType="Primary",
                               startDate=Convert.ToDateTime(form["hdnLeavingHome"]),
                                endDate=Convert.ToDateTime(form["hdnTillDate"])
                            }

                        }
                    }


                };
            }
            else if ((form["txtspouseage"] != "") && (form["txtchild1age"] != "") && (form["txtchild2age"] == "") && (form["txtchild3age"] == "") && (form["txtchild4age"] == ""))
            {
                user.families = new List<Models.Families>
                {
                    new Models.Families()
                    {
                        insureds=new List<Models.Insureds>
                        {
                            new Models.Insureds()
                            {
                               dateOfBirth=Convert.ToDateTime(DOB),


                                citizenship=IMGISOCODE,
                                residence =IMGISOCODE,
                                travelerType="Primary",
                              startDate=Convert.ToDateTime(form["hdnLeavingHome"]),
                                endDate=Convert.ToDateTime(form["hdnTillDate"])
                            },
                            new Models.Insureds()
                            {
                               dateOfBirth=Convert.ToDateTime(spouseDOB),


                                citizenship=IMGISOCODE,
                                residence =IMGISOCODE,
                                travelerType="Spouse",
                               startDate=Convert.ToDateTime(form["hdnLeavingHome"]),
                                endDate=Convert.ToDateTime(form["hdnTillDate"])
                            },
                            new Models.Insureds()
                            {
                               dateOfBirth=Convert.ToDateTime(childDOB),


                                citizenship=IMGISOCODE,
                                residence =IMGISOCODE,
                                travelerType="Child",
                                startDate=Convert.ToDateTime(form["hdnLeavingHome"]),
                                endDate=Convert.ToDateTime(form["hdnTillDate"])
                            }

                        }

                    }


                };
            }
            else if ((form["txtspouseage"] != "") && (form["txtchild1age"] != "") && (form["txtchild2age"] != "") && (form["txtchild3age"] == "") && (form["txtchild4age"] == ""))
            {
                user.families = new List<Models.Families>
                {
                    new Models.Families()
                    {
                        insureds=new List<Models.Insureds>
                        {
                            new Models.Insureds()
                            {
                               dateOfBirth=Convert.ToDateTime(DOB),


                                citizenship=IMGISOCODE,
                                residence =IMGISOCODE,
                                travelerType="Primary",
                                startDate=Convert.ToDateTime(form["hdnLeavingHome"]),
                                endDate=Convert.ToDateTime(form["hdnTillDate"])
                            },
                            new Models.Insureds()
                            {
                               dateOfBirth=Convert.ToDateTime(spouseDOB),


                                citizenship=IMGISOCODE,
                                residence =IMGISOCODE,
                                travelerType="Spouse",
                                startDate=Convert.ToDateTime(form["hdnLeavingHome"]),
                                endDate=Convert.ToDateTime(form["hdnTillDate"])
                            },
                            new Models.Insureds()
                            {
                               dateOfBirth=Convert.ToDateTime(childDOB),


                                citizenship=IMGISOCODE,
                                residence =IMGISOCODE,
                                travelerType="Child",
                               startDate=Convert.ToDateTime(form["hdnLeavingHome"]),
                                endDate=Convert.ToDateTime(form["hdnTillDate"])
                            },
                            new Models.Insureds()
                            {
                               dateOfBirth=Convert.ToDateTime(child2DOB),


                                citizenship=IMGISOCODE,
                                residence =IMGISOCODE,
                                travelerType="Child",
                                startDate=Convert.ToDateTime(form["hdnLeavingHome"]),
                                endDate=Convert.ToDateTime(form["hdnTillDate"])
                            }

                        }

                    }


                };
            }
            else if ((form["txtspouseage"] != "") && (form["txtchild1age"] != "") && (form["txtchild2age"] != "") && (form["txtchild3age"] != "") && (form["txtchild4age"] == ""))
            {
                user.families = new List<Models.Families>
                {
                    new Models.Families()
                    {
                        insureds=new List<Models.Insureds>
                        {
                            new Models.Insureds()
                            {
                               dateOfBirth=Convert.ToDateTime(DOB),


                                citizenship=IMGISOCODE,
                                residence =IMGISOCODE,
                                travelerType="Primary",
                                startDate=Convert.ToDateTime(form["hdnLeavingHome"]),
                                endDate=Convert.ToDateTime(form["hdnTillDate"])
                            },
                            new Models.Insureds()
                            {
                               dateOfBirth=Convert.ToDateTime(spouseDOB),


                                citizenship=IMGISOCODE,
                                residence =IMGISOCODE,
                                travelerType="Spouse",
                                startDate=Convert.ToDateTime(form["hdnLeavingHome"]),
                                endDate=Convert.ToDateTime(form["hdnTillDate"])
                            },
                            new Models.Insureds()
                            {
                               dateOfBirth=Convert.ToDateTime(childDOB),


                                citizenship=IMGISOCODE,
                                residence =IMGISOCODE,
                                travelerType="Child",
                               startDate=Convert.ToDateTime(form["hdnLeavingHome"]),
                                endDate=Convert.ToDateTime(form["hdnTillDate"])
                            },
                            new Models.Insureds()
                            {
                               dateOfBirth=Convert.ToDateTime(child2DOB),


                                citizenship=IMGISOCODE,
                                residence =IMGISOCODE,
                                travelerType="Child",
                                startDate=Convert.ToDateTime(form["hdnLeavingHome"]),
                                endDate=Convert.ToDateTime(form["hdnTillDate"])
                            },
                            new Models.Insureds()
                            {
                               dateOfBirth=Convert.ToDateTime(child3DOB),


                                citizenship=IMGISOCODE,
                                residence =IMGISOCODE,
                                travelerType="Child",
                                startDate=Convert.ToDateTime(form["hdnLeavingHome"]),
                                endDate=Convert.ToDateTime(form["hdnTillDate"])
                            }

                        }

                    }


                };
            }
            else if ((form["txtspouseage"] != "") && (form["txtchild1age"] != "") && (form["txtchild2age"] != "") && (form["txtchild3age"] != "") && (form["txtchild4age"] != ""))
            {
                user.families = new List<Models.Families>
                {
                    new Models.Families()
                    {
                        insureds=new List<Models.Insureds>
                        {
                            new Models.Insureds()
                            {
                               dateOfBirth=Convert.ToDateTime(DOB),


                                citizenship=IMGISOCODE,
                                residence =IMGISOCODE,
                                travelerType="Primary",
                                startDate=Convert.ToDateTime(form["hdnLeavingHome"]),
                                endDate=Convert.ToDateTime(form["hdnTillDate"])
                            },
                            new Models.Insureds()
                            {
                               dateOfBirth=Convert.ToDateTime(spouseDOB),


                                citizenship=IMGISOCODE,
                                residence =IMGISOCODE,
                                travelerType="Spouse",
                                startDate=Convert.ToDateTime(form["hdnLeavingHome"]),
                                endDate=Convert.ToDateTime(form["hdnTillDate"])
                            },
                            new Models.Insureds()
                            {
                               dateOfBirth=Convert.ToDateTime(childDOB),


                                citizenship=IMGISOCODE,
                                residence =IMGISOCODE,
                                travelerType="Child",
                               startDate=Convert.ToDateTime(form["hdnLeavingHome"]),
                                endDate=Convert.ToDateTime(form["hdnTillDate"])
                            },
                            new Models.Insureds()
                            {
                               dateOfBirth=Convert.ToDateTime(child2DOB),


                                citizenship=IMGISOCODE,
                                residence =IMGISOCODE,
                                travelerType="Child",
                                startDate=Convert.ToDateTime(form["hdnLeavingHome"]),
                                endDate=Convert.ToDateTime(form["hdnTillDate"])
                            },
                            new Models.Insureds()
                            {
                               dateOfBirth=Convert.ToDateTime(child3DOB),


                                citizenship=IMGISOCODE,
                                residence =IMGISOCODE,
                                travelerType="Child",
                                startDate=Convert.ToDateTime(form["hdnLeavingHome"]),
                                endDate=Convert.ToDateTime(form["hdnTillDate"])
                            },
                            new Models.Insureds()
                            {
                               dateOfBirth=Convert.ToDateTime(child4DOB),


                                citizenship=IMGISOCODE,
                                residence =IMGISOCODE,
                                travelerType="Child",
                                startDate=Convert.ToDateTime(form["hdnLeavingHome"]),
                                endDate=Convert.ToDateTime(form["hdnTillDate"])
                            }

                        }

                    }


                };
            }

            else if ((form["txtspouseage"] == "") && (form["txtchild1age"] != "") && (form["txtchild2age"] == "") && (form["txtchild3age"] == "") && (form["txtchild4age"] == ""))
            {
                user.families = new List<Models.Families>
                {
                    new Models.Families()
                    {
                        insureds=new List<Models.Insureds>
                        {
                            new Models.Insureds()
                            {
                               dateOfBirth=Convert.ToDateTime(DOB),


                                citizenship=IMGISOCODE,
                                residence =IMGISOCODE,
                                travelerType="Primary",
                              startDate=Convert.ToDateTime(form["hdnLeavingHome"]),
                                endDate=Convert.ToDateTime(form["hdnTillDate"])
                            },

                            new Models.Insureds()
                            {
                               dateOfBirth=Convert.ToDateTime(childDOB),


                                citizenship=IMGISOCODE,
                                residence =IMGISOCODE,
                                travelerType="Child",
                                startDate=Convert.ToDateTime(form["hdnLeavingHome"]),
                                endDate=Convert.ToDateTime(form["hdnTillDate"])
                            }

                        }

                    }


                };
            }
            else if ((form["txtspouseage"] == "") && (form["txtchild1age"] != "") && (form["txtchild2age"] != "") && (form["txtchild3age"] == "") && (form["txtchild4age"] == ""))
            {
                user.families = new List<Models.Families>
                {
                    new Models.Families()
                    {
                        insureds=new List<Models.Insureds>
                        {
                            new Models.Insureds()
                            {
                               dateOfBirth=Convert.ToDateTime(DOB),


                                citizenship=IMGISOCODE,
                                residence =IMGISOCODE,
                                travelerType="Primary",
                                startDate=Convert.ToDateTime(form["hdnLeavingHome"]),
                                endDate=Convert.ToDateTime(form["hdnTillDate"])
                            },

                            new Models.Insureds()
                            {
                               dateOfBirth=Convert.ToDateTime(childDOB),


                                citizenship=IMGISOCODE,
                                residence =IMGISOCODE,
                                travelerType="Child",
                               startDate=Convert.ToDateTime(form["hdnLeavingHome"]),
                                endDate=Convert.ToDateTime(form["hdnTillDate"])
                            },
                            new Models.Insureds()
                            {
                               dateOfBirth=Convert.ToDateTime(child2DOB),


                                citizenship=IMGISOCODE,
                                residence =IMGISOCODE,
                                travelerType="Child",
                                startDate=Convert.ToDateTime(form["hdnLeavingHome"]),
                                endDate=Convert.ToDateTime(form["hdnTillDate"])
                            }

                        }

                    }


                };
            }
            else if ((form["txtspouseage"] == "") && (form["txtchild1age"] != "") && (form["txtchild2age"] != "") && (form["txtchild3age"] != "") && (form["txtchild4age"] == ""))
            {
                user.families = new List<Models.Families>
                {
                    new Models.Families()
                    {
                        insureds=new List<Models.Insureds>
                        {
                            new Models.Insureds()
                            {
                               dateOfBirth=Convert.ToDateTime(DOB),


                                citizenship=IMGISOCODE,
                                residence =IMGISOCODE,
                                travelerType="Primary",
                                startDate=Convert.ToDateTime(form["hdnLeavingHome"]),
                                endDate=Convert.ToDateTime(form["hdnTillDate"])
                            },

                            new Models.Insureds()
                            {
                               dateOfBirth=Convert.ToDateTime(childDOB),


                                citizenship=IMGISOCODE,
                                residence =IMGISOCODE,
                                travelerType="Child",
                               startDate=Convert.ToDateTime(form["hdnLeavingHome"]),
                                endDate=Convert.ToDateTime(form["hdnTillDate"])
                            },
                            new Models.Insureds()
                            {
                               dateOfBirth=Convert.ToDateTime(child2DOB),


                                citizenship=IMGISOCODE,
                                residence =IMGISOCODE,
                                travelerType="Child",
                                startDate=Convert.ToDateTime(form["hdnLeavingHome"]),
                                endDate=Convert.ToDateTime(form["hdnTillDate"])
                            },
                            new Models.Insureds()
                            {
                               dateOfBirth=Convert.ToDateTime(child3DOB),


                                citizenship=IMGISOCODE,
                                residence =IMGISOCODE,
                                travelerType="Child",
                                startDate=Convert.ToDateTime(form["hdnLeavingHome"]),
                                endDate=Convert.ToDateTime(form["hdnTillDate"])
                            }

                        }

                    }


                };
            }
            else if ((form["txtspouseage"] == "") && (form["txtchild1age"] != "") && (form["txtchild2age"] != "") && (form["txtchild3age"] != "") && (form["txtchild4age"] != ""))
            {
                user.families = new List<Models.Families>
                {
                    new Models.Families()
                    {
                        insureds=new List<Models.Insureds>
                        {
                            new Models.Insureds()
                            {
                               dateOfBirth=Convert.ToDateTime(DOB),


                                citizenship=IMGISOCODE,
                                residence =IMGISOCODE,
                                travelerType="Primary",
                                startDate=Convert.ToDateTime(form["hdnLeavingHome"]),
                                endDate=Convert.ToDateTime(form["hdnTillDate"])
                            },


                            new Models.Insureds()
                            {
                               dateOfBirth=Convert.ToDateTime(childDOB),


                                citizenship=IMGISOCODE,
                                residence =IMGISOCODE,
                                travelerType="Child",
                               startDate=Convert.ToDateTime(form["hdnLeavingHome"]),
                                endDate=Convert.ToDateTime(form["hdnTillDate"])
                            },
                            new Models.Insureds()
                            {
                               dateOfBirth=Convert.ToDateTime(child2DOB),


                                citizenship=IMGISOCODE,
                                residence =IMGISOCODE,
                                travelerType="Child",
                                startDate=Convert.ToDateTime(form["hdnLeavingHome"]),
                                endDate=Convert.ToDateTime(form["hdnTillDate"])
                            },
                            new Models.Insureds()
                            {
                               dateOfBirth=Convert.ToDateTime(child3DOB),


                                citizenship=IMGISOCODE,
                                residence =IMGISOCODE,
                                travelerType="Child",
                                startDate=Convert.ToDateTime(form["hdnLeavingHome"]),
                                endDate=Convert.ToDateTime(form["hdnTillDate"])
                            },
                            new Models.Insureds()
                            {
                               dateOfBirth=Convert.ToDateTime(child4DOB),


                                citizenship=IMGISOCODE,
                                residence =IMGISOCODE,
                                travelerType="Child",
                                startDate=Convert.ToDateTime(form["hdnLeavingHome"]),
                                endDate=Convert.ToDateTime(form["hdnTillDate"])
                            }

                        }

                    }


                };
            }

            else
            {
                user.families = new List<Models.Families>
                {
                    new Models.Families()
                    {
                        insureds=new List<Models.Insureds>
                        {
                            new Models.Insureds()
                            {
                               dateOfBirth=Convert.ToDateTime(DOB),


                                citizenship=IMGISOCODE,
                                residence =IMGISOCODE,
                                travelerType="Primary",
                               startDate=Convert.ToDateTime(form["hdnLeavingHome"]),
                                endDate=Convert.ToDateTime(form["hdnTillDate"])
                            },
                            new Models.Insureds()
                            {
                               dateOfBirth=Convert.ToDateTime(spouseDOB),


                                citizenship=IMGISOCODE,
                                residence =IMGISOCODE,
                                travelerType="Spouse",
                                startDate=Convert.ToDateTime(form["hdnLeavingHome"]),
                                endDate=Convert.ToDateTime(form["hdnTillDate"])
                            }

                        }
                    }


                };
            }
            //user.families = new List<Models.Families>
            //    {
            //        new Models.Families()
            //        {
            //            insureds=new List<Models.Insureds>
            //            {
            //                new Models.Insureds()
            //                {
            //                    dateOfBirth=Convert.ToDateTime(DOB),
            //                    citizenship=IMGISOCODE,
            //                    residence =IMGISOCODE,
            //                    travelerType="Primary",
            //                    startDate=Convert.ToDateTime(form["hdnLeavingHome"]),
            //                    endDate=Convert.ToDateTime(form["hdnTillDate"])
            //                }
            //            }
            //        }
            //    };


            string jsonData = JsonConvert.SerializeObject(user, Formatting.Indented);
            string final = ReplaceFirstOccurrence(jsonData, "\"travelInfo\": [", "\"travelInfo\": ");
            final = ReplaceFirstOccurrence(final, "\"policyInfo\": [", "\"policyInfo\": ");
            final = ReplaceFirstOccurrence(final, "\r\n    }\r\n  ]", "\r\n    }\r\n ");
            final = ReplaceFirstOccurrence(final, "\r\n    }\r\n  ]", "\r\n    }\r\n ");
            var IMGGetquotes = ConfigurationManager.AppSettings["IMGQuote"];
            var t = Task.Run(() => PostRequest(final, IMGGetquotes, AuthKey, Domainname));
            t.Wait();
            string JsonResult = t.Result;




            var myDetails = JsonConvert.DeserializeObject<Models.IMG_Response>(JsonResult);

            var totalamount = myDetails.TotalPremium;

            return Json(totalamount, JsonRequestBehavior.AllowGet);


        }


        public ActionResult PremiumamountforVisitorsSecure(FormCollection form)
        {
            bool dest = true;
            if (form["hdnTouristcode"] == "US")
            {
                dest = true;
            }
            else
            {
                dest = false;
            }

            string Plan = string.Empty;
            string policymax = string.Empty;
            string policyname = "Visitor Secure";
            string Planname = string.Empty;
            if (form["selectedCoveragevalue"] == "50000")
            {
                Plan = "50";
                Planname = "A";
            }
            else if (form["selectedCoveragevalue"] == "75000")
            {
                Plan = "75";
                Planname = "B";
            }
            else if (form["selectedCoveragevalue"] == "100000")
            {
                Plan = "100";
                Planname = "C";
            }
            else if (form["selectedCoveragevalue"] == "130000")
            {
                Plan = "130";
                Planname = "D";
            }
            string check = form["selectedCoveragevalue"];

            string dateofbirth = "";
            if (form["hdnTravelersDOB"] == null || form["hdnTravelersDOB"] == "")
            {
                int ageoftraveler = Convert.ToInt32(form["age"]);
                int getDOByear = Convert.ToInt32(DateTime.Now.Year) - ageoftraveler;
                string getdate = DateTime.Now.Day.ToString();
                string getmonth = DateTime.Now.Month.ToString();

                dateofbirth = getmonth + "/" + getdate + "/" + getDOByear.ToString();
            }
            else
            {
                dateofbirth = Convert.ToDateTime(form["hdnTravelersDOB"]).ToString("MM/dd/yyyy");
            }


            if (form["txtspouseage"] == null)
            {
                form["txtspouseage"] = "";
            }
            if (form["txtchild1age"] == null)
            {
                form["txtchild1age"] = "";
            }
            if (form["txtchild2age"] == null)
            {
                form["txtchild2age"] = "";
            }
            if (form["txtchild3age"] == null)
            {
                form["txtchild3age"] = "";
            }
            if (form["txtchild4age"] == null)
            {
                form["txtchild4age"] = "";
            }
            string sposedateofbirth = "";
            if (form["txtspouseage"] != "")
            {
                int ageofspouse = Convert.ToInt32(form["txtspouseage"]);
                int getDOByear = Convert.ToInt32(DateTime.Now.Year) - ageofspouse;
                string getdate = DateTime.Now.Day.ToString();
                string getmonth = DateTime.Now.Month.ToString();

                sposedateofbirth = getmonth + "/" + getdate + "/" + getDOByear.ToString();
            }
            string childdateofbirth = "";
            if (form["txtchild1age"] != "")
            {
                int ageofchild1 = Convert.ToInt32(form["txtchild1age"]);
                int getDOByear = Convert.ToInt32(DateTime.Now.Year) - ageofchild1;
                string getdate = DateTime.Now.Day.ToString();
                string getmonth = DateTime.Now.Month.ToString();

                childdateofbirth =/* Convert.ToDateTime(*/getmonth + "/" + getdate + "/" + getDOByear.ToString()/*).ToString("MM/dd/yyyy")*/;
            }
            string child2dateofbirth = "";
            if (form["txtchild2age"] != "")
            {
                int ageofchild2 = Convert.ToInt32(form["txtchild2age"]);
                int getDOByear = Convert.ToInt32(DateTime.Now.Year) - ageofchild2;
                string getdate = DateTime.Now.Day.ToString();
                string getmonth = DateTime.Now.Month.ToString();

                child2dateofbirth =/* Convert.ToDateTime(*/getmonth + "/" + getdate + "/" + getDOByear.ToString()/*).ToString("MM/dd/yyyy")*/;
            }

            string child3dateofbirth = "";
            if (form["txtchild3age"] != "")
            {
                int ageofchild1 = Convert.ToInt32(form["txtchild3age"]);
                int getDOByear = Convert.ToInt32(DateTime.Now.Year) - ageofchild1;
                string getdate = DateTime.Now.Day.ToString();
                string getmonth = DateTime.Now.Month.ToString();

                child3dateofbirth = /*Convert.ToDateTime(*/getmonth + "/" + getdate + "/" + getDOByear.ToString()/*).ToString("MM/dd/yyyy")*/;
            }
            string child4dateofbirth = "";
            if (form["txtchild4age"] != "")
            {
                int ageofchild2 = Convert.ToInt32(form["txtchild4age"]);
                int getDOByear = Convert.ToInt32(DateTime.Now.Year) - ageofchild2;
                string getdate = DateTime.Now.Day.ToString();
                string getmonth = DateTime.Now.Month.ToString();

                child4dateofbirth = /*Convert.ToDateTime(*/getmonth + "/" + getdate + "/" + getDOByear.ToString()/*).ToString("MM/dd/yyyy")*/;
            }
            var Quote = new Models.HCC_User();
            Quote.ReferId = "9800";
            Quote.Culture = "en-US";
            Quote.USDest = dest;
            Quote.CoverageBeginDt = Convert.ToDateTime(form["hdnLeavingHome"]).ToString("MM/dd/yyyy");// "04/15/2020";//
            Quote.CoverageEndDt = Convert.ToDateTime(form["hdnTillDate"]).ToString("MM/dd/yyyy");//"05/16/2020"; //
            Quote.ArriveInUSDate = Convert.ToDateTime(form["hdnLeavingHome"]).ToString("MM/dd/yyyy");// "04/15/2020";//
            Quote.Plan = Plan;//"250000";//
            Quote.Deductible = (form["selectedDeductiblevalue"]); //"250";//
            Quote.PlanName = Planname; //"AP";//


            Quote.MailOpt = "regular";
            Quote.SurplusLines = false;

            //Quote.ApplicationList[0].Dob = req.TravelersDOB;
            //var Dob = new Models.ApplicantList();
            //Dob.Dob = age;



            if ((form["txtspouseage"] == "") && ((form["txtchild1age"] == "") && (form["txtchild2age"] == "")) && ((form["txtchild4age"] == "") && (form["txtchild3age"] == "")))
            {
                Quote.ApplicantList = new List<Models.ApplicantList>
                          {
                             new Models.ApplicantList {Dob=dateofbirth
                          }

                          };
            }
            else if (((form["txtchild1age"] != "") && (form["txtchild2age"] == "")) && (form["txtspouseage"] != "") && ((form["txtchild3age"] == "") && (form["txtchild4age"] != "")))
            {
                Quote.ApplicantList = new List<Models.ApplicantList>
                          {
                             new Models.ApplicantList {Dob=dateofbirth
                          },
                             new Models.ApplicantList {Dob=sposedateofbirth
                          },
                             new Models.ApplicantList {Dob=childdateofbirth
                          }

                          };
            }
            else if (((form["txtchild1age"] != "") && (form["txtchild2age"] != "")) && (form["txtspouseage"] != "") && ((form["txtchild4age"] == "") && (form["txtchild3age"] == "")))
            {
                Quote.ApplicantList = new List<Models.ApplicantList>
                          {
                             new Models.ApplicantList {Dob=dateofbirth
                          },
                             new Models.ApplicantList {Dob=sposedateofbirth
                          },
                             new Models.ApplicantList {Dob=childdateofbirth
                          },
                              new Models.ApplicantList {Dob=child2dateofbirth
                          }

                          };
            }
            else if (((form["txtchild1age"] != "") && (form["txtchild2age"] != "")) && (form["txtspouseage"] != "") && ((form["txtchild4age"] == "") && (form["txtchild3age"] != "")))
            {
                Quote.ApplicantList = new List<Models.ApplicantList>
                          {
                             new Models.ApplicantList {Dob=dateofbirth
                          },
                             new Models.ApplicantList {Dob=sposedateofbirth
                          },
                             new Models.ApplicantList {Dob=childdateofbirth
                          },
                              new Models.ApplicantList {Dob=child2dateofbirth
                          },
                              new Models.ApplicantList {Dob=child3dateofbirth
                          }

                          };
            }
            else if (((form["txtchild1age"] != "") && (form["txtchild2age"] != "")) && (form["txtspouseage"] != "") && ((form["txtchild3age"] != "") && (form["txtchild4age"] != "")))
            {
                Quote.ApplicantList = new List<Models.ApplicantList>
                          {
                             new Models.ApplicantList {Dob=dateofbirth
                          },
                             new Models.ApplicantList {Dob=sposedateofbirth
                          },
                             new Models.ApplicantList {Dob=childdateofbirth
                          },
                              new Models.ApplicantList {Dob=child2dateofbirth
                          },
                              new Models.ApplicantList {Dob=child3dateofbirth
                          },
                              new Models.ApplicantList {Dob=child4dateofbirth
                          }

                          };
            }
            else if ((form["txtspouseage"] != "") && ((form["txtchild1age"] == "") && (form["txtchild2age"] == "")) && ((form["txtchild3age"] == "") && (form["txtchild4age"] == "")))
            {
                Quote.ApplicantList = new List<Models.ApplicantList>
                          {
                             new Models.ApplicantList {Dob=dateofbirth
                          },
                             new Models.ApplicantList {Dob=sposedateofbirth
                          }

                          };
            }

            else if (((form["txtchild1age"] != "") && (form["txtchild2age"] == "")) && (form["txtspouseage"] == "") && ((form["txtchild3age"] == "") && (form["txtchild4age"] != "")))
            {
                Quote.ApplicantList = new List<Models.ApplicantList>
                          {
                             new Models.ApplicantList {Dob=dateofbirth
                          },

                             new Models.ApplicantList {Dob=childdateofbirth
                          }

                          };
            }
            else if (((form["txtchild1age"] != "") && (form["txtchild2age"] != "")) && (form["txtspouseage"] == "") && ((form["txtchild4age"] == "") && (form["txtchild3age"] == "")))
            {
                Quote.ApplicantList = new List<Models.ApplicantList>
                          {
                             new Models.ApplicantList {Dob=dateofbirth
                          },

                             new Models.ApplicantList {Dob=childdateofbirth
                          },
                              new Models.ApplicantList {Dob=child2dateofbirth
                          }

                          };
            }
            else if (((form["txtchild1age"] != "") && (form["txtchild2age"] != "")) && (form["txtspouseage"] == "") && ((form["txtchild4age"] == "") && (form["txtchild3age"] != "")))
            {
                Quote.ApplicantList = new List<Models.ApplicantList>
                          {
                             new Models.ApplicantList {Dob=dateofbirth
                          },

                             new Models.ApplicantList {Dob=childdateofbirth
                          },
                              new Models.ApplicantList {Dob=child2dateofbirth
                          },
                              new Models.ApplicantList {Dob=child3dateofbirth
                          }

                          };
            }
            else if (((form["txtchild1age"] != "") && (form["txtchild2age"] != "")) && (form["txtspouseage"] == "") && ((form["txtchild3age"] != "") && (form["txtchild4age"] != "")))
            {
                Quote.ApplicantList = new List<Models.ApplicantList>
                          {
                             new Models.ApplicantList {Dob=dateofbirth
                          },

                             new Models.ApplicantList {Dob=childdateofbirth
                          },
                              new Models.ApplicantList {Dob=child2dateofbirth
                          },
                              new Models.ApplicantList {Dob=child3dateofbirth
                          },
                              new Models.ApplicantList {Dob=child4dateofbirth
                          }

                          };
            }

            var jsonresult = JsonConverter.Serialize(Quote);
            string VisitorsQuote = ConfigurationManager.AppSettings["VisitorsQuote"];
            string Domainname = ConfigurationManager.AppSettings["Domainname"];
            var t = Task.Run(() => PostRequestHcc(jsonresult, VisitorsQuote, Domainname));

            t.Wait();
            string JsonResult = t.Result;
            ViewBag.Message = JsonResult;
            System.Web.Script.Serialization.JavaScriptSerializer jsSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();


            var reserializedJSON = "";
            using (var sr = new StringReader(JsonResult))
            using (var jr = new JsonTextReader(sr))
            {
                var serial = new JsonSerializer();
                serial.Formatting = Formatting.Indented;
                var obj = serial.Deserialize<Models.Hcc_Quote>(jr);

                reserializedJSON = JsonConvert.SerializeObject(obj, Formatting.Indented);

            }
            //var finalResult = new JavaScriptSerializer().Deserialize<Models.HccAtlasQuote>(reserializedJSON);
            var finalResult = jsSerializer.Deserialize<Models.Hcc_Quote>(reserializedJSON);

            var totalamount = finalResult.TotalPremium;

            return Json(totalamount, JsonRequestBehavior.AllowGet);
        }


        private string ReplaceFirstOccurrence(string Source, string Find, string Replace)
        {
            int Place = Source.IndexOf(Find);
            string result = Source.Remove(Place, Find.Length).Insert(Place, Replace);
            return result;
        }
        public ActionResult Redirecttocompare(string _item)
        {

            return Json(_item, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Index(string Prefix)
        {
            string prefix = (Prefix).ToUpper();
            List<LoginModel> allsearch = quotationalmanager.GetCountryName(Prefix);
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