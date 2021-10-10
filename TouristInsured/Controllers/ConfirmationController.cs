using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using TouristInsuredEntity;
using TouristInsuredManager;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using System.IO;

namespace TouristInsured.Controllers
{
    
    public class ConfirmationController : Controller
    {
        ConfirmationManager confirmationmanager = new ConfirmationManager();
        ConfirmationModel confirmationmodel = new ConfirmationModel();
        QuotationManager quotationalmanager = new QuotationManager();
        // GET: Confirmation
        public ActionResult Confirmation(string Data,string TripID)
        {
            try
            {
                if (Data.Contains("code") == true)
                {
                    int Place = Data.IndexOf("TRIPID:");

                    string testString = Data;
                    string sub = testString.Substring(testString.IndexOf("TRIPID:")).Replace("TRIPID:", "");

                    string tripid = testString.Substring(Place);


                    Data = ReplaceFirstOccurrence(Data, tripid, "");
                    var jsondata1 = Data;
                    var JSONObject = new JavaScriptSerializer().Deserialize<Dictionary<string, string>>(jsondata1);
                    var Response = new JavaScriptSerializer().Deserialize<Models.INFErrorresponse>(jsondata1);
                    confirmationmodel.code = Response.code;
                    confirmationmodel.message = Response.message;
                    confirmationmodel.display = "block";
                    confirmationmodel.displayforsuccess = "none";
                    confirmationmodel.TripID = sub;
                    var result = confirmationmanager.SaveTransactionDetails(confirmationmodel);
                    return View(confirmationmodel);
                }

                else
                {
                    int Place = Data.IndexOf("TRIPID:");

                    string testString = Data;
                    string sub = testString.Substring(testString.IndexOf("TRIPID:")).Replace("TRIPID:", "");

                    string tripid = testString.Substring(Place);


                    Data = ReplaceFirstOccurrence(Data, tripid, "");
                    //var jsondata = @"{""premium"":""104.00"",""totalPremium"":""119.00"",""transactionId"":""40046476666"",""applicationFee"":""15.00"",""policyName"":""INF TRAVELUSA"",
                    //""applicationFeeType"":""New Application"",""policyNumber"":""GLM N10783434S"",""coverageLength"":""32 days"",""certificateLink"":""https://sbox.infplans.com/API/getCertificate/purchases/pLVMnDJaVoOyWqJ""}";
                    var jsondata1 = Data;
                    var JSONObj = new JavaScriptSerializer().Deserialize<Dictionary<string, string>>(jsondata1);
                    var ResponseModel = new JavaScriptSerializer().Deserialize<Models.INF_Payment_Response>(jsondata1);
                    String premium = ResponseModel.premium;
                    double total_prem = ResponseModel.totalPremium;
                    String coverage_length = ResponseModel.coverageLength;
                    string CertificationLink = ResponseModel.certificateLink;
                    confirmationmodel.policyname = ResponseModel.policyName;
                    confirmationmodel.PolicyNumber = ResponseModel.policyNumber;
                    confirmationmodel.TransactionID = ResponseModel.transactionId;
                    confirmationmodel.Applicationfee = ResponseModel.applicationFee;
                    confirmationmodel.Premium = ResponseModel.premium;
                    confirmationmodel.Totalpremium = ResponseModel.totalPremium;
                    confirmationmodel.coverageLength = ResponseModel.coverageLength;
                    confirmationmodel.certificatelink = ResponseModel.certificateLink;
                    confirmationmodel.display = "none";
                    confirmationmodel.displayforsuccess = "block";
                    confirmationmodel.TripID = sub;
                    var result = confirmationmanager.SaveTransactionDetails(confirmationmodel);
                    return View(confirmationmodel);
                }
            }
            catch (Exception ex)
            {
                var error = ex;
                quotationalmanager.SaveErrorLog("INFConfirmation", Data);
                string res = "0";
                return Json(res, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult ConfirmTrawick(string Data,string Tripid)
        {
            try
            {
                if (Data.Contains("\"OrderStatusCode\":-200,\"") == true)
                {
                    int Place = Data.IndexOf("TRIPID:");

                    string testString = Data;
                    string sub = testString.Substring(testString.IndexOf("TRIPID:")).Replace("TRIPID:", "");

                    string tripid = testString.Substring(Place);


                    Data = ReplaceFirstOccurrence(Data, tripid, "");
                    var jsondata = Data;
                    var reserializedJSON = "";
                    using (var sr = new StringReader(jsondata))
                    using (var jr = new JsonTextReader(sr))
                    {
                        var serial = new JsonSerializer();
                        serial.Formatting = Formatting.Indented;
                        var obj = serial.Deserialize<Models.Trawick_Payment_Response>(jr);

                        reserializedJSON = JsonConvert.SerializeObject(obj, Formatting.Indented);

                    }
                    var res = new JavaScriptSerializer().Deserialize<Models.Trawick_Payment_Response>(reserializedJSON);
                    //string FirsName = res.Members[0].FirstName;

                    confirmationmodel.Totalpremium = res.TotalPrice;
                    confirmationmodel.OrderRequestId = res.OrderRequestId;
                    confirmationmodel.OrderStatusMessage = res.OrderStatusMessage;
                    confirmationmodel.PolicyNumber = res.PolicyNumber;
                    confirmationmodel.TransactionID = res.TransactionId;
                    confirmationmodel.OrderRequestId = res.OrderRequestId;
                    confirmationmodel.OrderNo = res.OrderNo;
                    confirmationmodel.FirstName = res.Members[0].FirstName + " " + res.Members[0].LastName;
                    confirmationmodel.DOB = (res.Members[0].DOB);
                    confirmationmodel.Gender = res.Members[0].Gender;
                    confirmationmodel.display = "none";
                    confirmationmodel.displayforsuccess = "block";
                    confirmationmodel.TripID = sub;
                    var result = confirmationmanager.SaveTransactionDetails(confirmationmodel);
                    return View(confirmationmodel);

                }
                else if (Data.Contains("\"OrderStatusCode\":-999,\"") == true)
                {
                    int Place = Data.IndexOf("TRIPID:");

                    string testString = Data;
                    string sub = testString.Substring(testString.IndexOf("TRIPID:")).Replace("TRIPID:", "");

                    string tripid = testString.Substring(Place);


                    Data = ReplaceFirstOccurrence(Data, tripid, "");
                    var jsondata = Data;
                    var ResponseModel = new JavaScriptSerializer().Deserialize<Models.TrawickErrorResponse>(jsondata);
                    confirmationmodel.OrderStatusMessage = ResponseModel.OrderStatusMessage;
                    confirmationmodel.TripID = sub;
                    confirmationmodel.display = "block";
                    confirmationmodel.displayforsuccess = "none";

                    var result = confirmationmanager.SaveTransactionDetails(confirmationmodel);
                    return View(confirmationmodel);
                }
                else
                {
                    int Place = Data.IndexOf("TRIPID:");

                    string testString = Data;
                    string sub = testString.Substring(testString.IndexOf("TRIPID:")).Replace("TRIPID:", "");

                    string tripid = testString.Substring(Place);


                    Data = ReplaceFirstOccurrence(Data, tripid, "");





                    //var jsondata1 = "{\"ProductId\":\"16\",\"OrderRequestId\":\"1006784\",\"OrderNo\":\"693591\",\"PrimaryMemberId\":\"525165\",\"OrderStatusMessage\":\"Order Processed Successfully\",\"OrderStatusCode\":\"1\",\"TotalPrice\":\"27.46\", \"CreditCardNumber\":\"4-1111\",\"PolicyNumber\":\"STUN-13468-18\", \"TransactionId\":\"769231\", \"Members\":[{\"LastName\":\"SWA\",\"FirstName\":\"AWS\",\"MiddleName\":\"THF\",\"Gender\":\"Male\", \"DOB\":\"1999 - 03 - 15T00: 00:00\",\"MemberID\":\"525165\",\"RelationshipType\":\"Primary\", \"UserID\":\"980251649\"}],\"CreditCardResponse\":{\"ResponseText\":\"Approval\",\"ResponseCode\":\"100\", \"TransactionId\":\"4113346c - 298f - 4102 - bc79 - e22edc874128\", \"CvvResponse\":\"\",\"AuthCode\":\"test12345\",\"Success\":\"true\"}";
                    var jsondata = Data;
                    //"{\"ProductId\":\"16\", " +
                    //"\"OrderRequestId\":\"1006784\", " +
                    //"\"OrderNo\":\"693591\", " +
                    //"\"PrimaryMemberId\":\"525165\", " +
                    //"\"OrderStatusMessage\":\"Order Processed Successfully\", " +
                    //"\"OrderStatusCode\":\"1\", " +
                    //"\"TotalPrice\":\"27.46\", " +
                    //"\"CreditCardNumber\":\"4-1111\", " +
                    //"\"PolicyNumber\":\"STUN-13468-18\", " +
                    //"\"TransactionId\":\"769231\", " +
                    //"\"Members\":" +
                    //"[" +
                    //"{\"LastName\":\"SWA\",\"FirstName\":\"AWS\",\"MiddleName\":\"THF\",\"Gender\":\"Male\", \"DOB\":\"1999 - 03 - 15T00: 00:00\",\"MemberID\":\"525165\",\"RelationshipType\":\"Primary\", \"UserID\":\"980251649\"}" +
                    //"]" +
                    //"\"CreditCardResponse\":" +
                    //"{\"ResponseText\":\"Approval\",\"ResponseCode\":\"100\", \"TransactionId\":\"4113346c - 298f - 4102 - bc79 - e22edc874128\", \"CvvResponse\":\"\",\"AuthCode\":\"test12345\",\"Success\":\"true\"}" +
                    //"}";
                    var reserializedJSON = "";
                    using (var sr = new StringReader(jsondata))
                    using (var jr = new JsonTextReader(sr))
                    {
                        var serial = new JsonSerializer();
                        serial.Formatting = Formatting.Indented;
                        var obj = serial.Deserialize<Models.Trawick_Payment_Response>(jr);

                        reserializedJSON = JsonConvert.SerializeObject(obj, Formatting.Indented);

                    }
                    var res = new JavaScriptSerializer().Deserialize<Models.Trawick_Payment_Response>(reserializedJSON);
                    //string FirsName = res.Members[0].FirstName;

                    confirmationmodel.Totalpremium = res.TotalPrice;
                    confirmationmodel.OrderRequestId = res.OrderRequestId;
                    confirmationmodel.OrderStatusMessage = res.OrderStatusMessage;
                    confirmationmodel.PolicyNumber = res.PolicyNumber;
                    confirmationmodel.TransactionID = res.TransactionId;
                    confirmationmodel.OrderRequestId = res.OrderRequestId;
                    confirmationmodel.OrderNo = res.OrderNo;
                    confirmationmodel.FirstName = res.Members[0].FirstName + " " + res.Members[0].LastName;
                    confirmationmodel.DOB = (res.Members[0].DOB);
                    confirmationmodel.Gender = res.Members[0].Gender;
                    confirmationmodel.display = "none";
                    confirmationmodel.displayforsuccess = "block";
                    confirmationmodel.TripID = sub;
                    var result = confirmationmanager.SaveTransactionDetails(confirmationmodel);

                    return View(confirmationmodel);
                }
            }
            catch (Exception ex)
            {
                var error = ex;
                quotationalmanager.SaveErrorLog("TrawickConfirmation", Data);
                string res = "0";
                return Json(res, JsonRequestBehavior.AllowGet);
            }

        }


        public ActionResult ConfirmAtlas(string data, string Tripid)
        {
            try
            {
                if (data.Contains("AuthCode") == true)
                {
                    int Place = data.IndexOf("TRIPID:");

                    string testString = data;
                    string sub = testString.Substring(testString.IndexOf("TRIPID:")).Replace("TRIPID:", "");

                    string tripid = testString.Substring(Place);


                    data = ReplaceFirstOccurrence(data, tripid, "");

                    var jsondata1 = data;
                    var JSONObj = new JavaScriptSerializer().Deserialize<Dictionary<string, string>>(jsondata1);
                    var ResponseModel = new JavaScriptSerializer().Deserialize<Models.AtlasPaymentResponse>(jsondata1);
                    confirmationmodel.TransactionID = ResponseModel.TransactionId;
                    confirmationmodel.certificatelink = ResponseModel.Cert;
                    confirmationmodel.AuthCode = ResponseModel.AuthCode;
                    confirmationmodel.display = "none";
                    confirmationmodel.displayforsuccess = "block";
                    confirmationmodel.TripID = sub;
                    var result = confirmationmanager.SaveTransactionDetails(confirmationmodel);

                    return View(confirmationmodel);
                }

                else
                {
                    int Place = data.IndexOf("TRIPID:");

                    string testString = data;
                    string sub = testString.Substring(testString.IndexOf("TRIPID:")).Replace("TRIPID:", "");

                    string tripid = testString.Substring(Place);


                    data = ReplaceFirstOccurrence(data, tripid, "");
                    var jsondata = data;
                    var JSONObject = new JavaScriptSerializer().Deserialize<Dictionary<string, string>>(jsondata);
                    var Response = new JavaScriptSerializer().Deserialize<Models.AtlasErrorResponse>(jsondata);
                    confirmationmodel.display = "block";
                    confirmationmodel.displayforsuccess = "none";
                    confirmationmodel.TripID = sub;
                    confirmationmodel.ErrorMessage = Response.ErrorMessage;
                    var result = confirmationmanager.SaveTransactionDetails(confirmationmodel);
                    return View(confirmationmodel);

                }
            }
            catch (Exception ex)
            {
                var error = ex;
                quotationalmanager.SaveErrorLog("AtlasConfirmation", data);
                string res = "0";
                return Json(res, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ConfirmImg(string data, string Tripid)
        {
            try
            {
                int Place = data.IndexOf("TRIPID:");

                string testString = data;
                string sub = testString.Substring(testString.IndexOf("TRIPID:")).Replace("TRIPID:", "");

                string tripid = testString.Substring(Place);


                data = ReplaceFirstOccurrence(data, tripid, "");

                var jsondata1 = data;
                //var JSONObj = new JavaScriptSerializer().Deserialize<Dictionary<string, string>>(jsondata1);


                JavaScriptSerializer oJS2 = new JavaScriptSerializer();
                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore
                };
                var ResponseModel = JsonConvert.DeserializeObject<Models.IMGPaymentResponse>(jsondata1, settings);
                //var ResponseModel = new JavaScriptSerializer().Deserialize<Models.IMGPaymentResponse>(jsondata1);

                string certi = ResponseModel.certificateNumber;
                if (ResponseModel.certificateNumber == "" || ResponseModel.certificateNumber == null && ResponseModel.errors != null)
                {

                    confirmationmodel.ErrorMessage = ResponseModel.errors[0];
                    confirmationmodel.display = "block";
                    confirmationmodel.displayforsuccess = "none";

                    confirmationmodel.TripID = sub;
                    var result = confirmationmanager.SaveTransactionDetails(confirmationmodel);
                }
                else if (ResponseModel.brokenRules != null && ResponseModel.certificateNumber == "")
                {
                    string Brokenruleerror = string.Empty;
                    for (int i = 0; i < ResponseModel.brokenRules.Count; i++)
                    {
                        Brokenruleerror += ResponseModel.brokenRules[i].name + "br";
                    }
                    confirmationmodel.ErrorMessage = Brokenruleerror;
                    confirmationmodel.display = "block";
                    confirmationmodel.displayforsuccess = "none";

                    confirmationmodel.TripID = sub;
                    var result = confirmationmanager.SaveTransactionDetails(confirmationmodel);
                }
                else
                {
                    confirmationmodel.certificatelink = ResponseModel.certificateNumber;
                    confirmationmodel.Totalpremium = ResponseModel.totalPremium;
                    confirmationmodel.display = "none";
                    confirmationmodel.displayforsuccess = "block";
                    confirmationmodel.TripID = sub;
                    confirmationmodel.ErrorMessage = "";
                    var result = confirmationmanager.SaveTransactionDetails(confirmationmodel);
                }

                return View(confirmationmodel);
            }
            catch (Exception ex)
            {
                var error = ex;
                quotationalmanager.SaveErrorLog("IMGConfirmation", data);
                string res = "0";
                return Json(res, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ConfirmVisitors(string data, string Tripid)
        {
            try
            {
                if (data.Contains("VS") == true)
                {
                    int Place = data.IndexOf("TRIPID:");

                    string testString = data;
                    string sub = testString.Substring(testString.IndexOf("TRIPID:")).Replace("TRIPID:", "");

                    string tripid = testString.Substring(Place);


                    data = ReplaceFirstOccurrence(data, tripid, "");
                    var jsondata = data;
                    confirmationmodel.display = "none";
                    confirmationmodel.displayforsuccess = "block";
                    confirmationmodel.TripID = sub;
                    confirmationmodel.certificatelink = jsondata;
                    var result = confirmationmanager.SaveTransactionDetails(confirmationmodel);
                    return View(confirmationmodel);
                }
                else if (data.Contains("Key") == true)
                {
                    int Place = data.IndexOf("TRIPID:");

                    string testString = data;
                    string sub = testString.Substring(testString.IndexOf("TRIPID:")).Replace("TRIPID:", "");

                    string tripid = testString.Substring(Place);


                    data = ReplaceFirstOccurrence(data, tripid, "");
                    var jsondata = data;
                    var ResponseModel = new JavaScriptSerializer().Deserialize<Models.DiplomatPurchaseResponse>(jsondata);
                    confirmationmodel.display = "none";
                    confirmationmodel.displayforsuccess = "block";
                    confirmationmodel.TripID = sub;
                    confirmationmodel.certificatelink = jsondata;
                    var result = confirmationmanager.SaveTransactionDetails(confirmationmodel);
                    return View(confirmationmodel);
                }
                else
                {
                    int Place = data.IndexOf("TRIPID:");

                    string testString = data;
                    string sub = testString.Substring(testString.IndexOf("TRIPID:")).Replace("TRIPID:", "");

                    string tripid = testString.Substring(Place);


                    data = ReplaceFirstOccurrence(data, tripid, "");
                    var jsondata = data;
                    var JSONObject = new JavaScriptSerializer().Deserialize<Dictionary<string, string>>(jsondata);
                    var Response = new JavaScriptSerializer().Deserialize<Models.AtlasErrorResponse>(jsondata);
                    confirmationmodel.display = "block";
                    confirmationmodel.displayforsuccess = "none";
                    confirmationmodel.TripID = sub;
                    confirmationmodel.ErrorMessage = Response.ErrorMessage;
                    var result = confirmationmanager.SaveTransactionDetails(confirmationmodel);
                    return View(confirmationmodel);

                }
            }
            catch (Exception ex)
            {
                var error = ex;
                quotationalmanager.SaveErrorLog("VisitorsConfirmation", data);
                string res = "0";
                return Json(res, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult ConfirmDiplomat(string data, string Tripid)
        {
            try
            {
                int Place = data.IndexOf("TRIPID:");

                string testString = data;
                string sub = testString.Substring(testString.IndexOf("TRIPID:")).Replace("TRIPID:", "");

                string tripid = testString.Substring(Place);


                data = ReplaceFirstOccurrence(data, tripid, "");

                var jsondata1 = data;
                //var JSONObj = new JavaScriptSerializer().Deserialize<Dictionary<string, string>>(jsondata1);

                var ResponseModel = new JavaScriptSerializer().Deserialize<Models.DiplomatPurchaseResponse>(jsondata1);

                //var ResponseModel = new JavaScriptSerializer().Deserialize<Models.IMGPaymentResponse>(jsondata1);
                if (data.Contains("PolicyNumber") == true)
                {
                    confirmationmodel.PolicyNumber = ResponseModel.PolicyNumber;
                    confirmationmodel.Totalpremium = ResponseModel.AmountCharged;

                    confirmationmodel.display = "none";
                    confirmationmodel.displayforsuccess = "block";
                    confirmationmodel.TripID = sub;
                    var result = confirmationmanager.SaveTransactionDetails(confirmationmodel);
                }
                else
                {
                    confirmationmodel.ErrorMessage = ResponseModel.Message;
                    confirmationmodel.display = "block";
                    confirmationmodel.displayforsuccess = "none";
                    var result = confirmationmanager.SaveTransactionDetails(confirmationmodel);
                }


                return View(confirmationmodel);
            }
            catch (Exception ex)
            {
                var error = ex;
                quotationalmanager.SaveErrorLog("DiplomatConfirmation", data);
                string res = "0";
                return Json(res, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ConfirmStudent(string data, string Tripid)
        {
            try
            {
                 if (data.Contains("Key") == true)
                {
                    int Place = data.IndexOf("TRIPID:");

                    string testString = data;
                    string sub = testString.Substring(testString.IndexOf("TRIPID:")).Replace("TRIPID:", "");

                    string tripid = testString.Substring(Place);


                    data = ReplaceFirstOccurrence(data, tripid, "");
                    var jsondata = data;
                    var ResponseModel = new JavaScriptSerializer().Deserialize<Models.HccStudentPurchaseResponse>(jsondata);
                    confirmationmodel.display = "none";
                    confirmationmodel.displayforsuccess = "block";
                    confirmationmodel.TripID = sub;
                    confirmationmodel.certificatelink = ResponseModel.Value;
                    var result = confirmationmanager.SaveTransactionDetails(confirmationmodel);
                    return View(confirmationmodel);
                }
                else
                {
                    int Place = data.IndexOf("TRIPID:");

                    string testString = data;
                    string sub = testString.Substring(testString.IndexOf("TRIPID:")).Replace("TRIPID:", "");

                    string tripid = testString.Substring(Place);


                    data = ReplaceFirstOccurrence(data, tripid, "");
                    var jsondata = data;
                    var JSONObject = new JavaScriptSerializer().Deserialize<Dictionary<string, string>>(jsondata);
                    var Response = new JavaScriptSerializer().Deserialize<Models.AtlasErrorResponse>(jsondata);
                    confirmationmodel.display = "block";
                    confirmationmodel.displayforsuccess = "none";
                    confirmationmodel.TripID = sub;
                    confirmationmodel.ErrorMessage = Response.ErrorMessage;
                    var result = confirmationmanager.SaveTransactionDetails(confirmationmodel);
                    return View(confirmationmodel);

                }
            }
            catch (Exception ex)
            {
                var error = ex;
                quotationalmanager.SaveErrorLog("VisitorsConfirmation", data);
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

    }
}