using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using TouristInsuredEntity;
using DataAccessLayer;
using System.Data.SqlClient;

namespace TouristInsuredManager
{
  public class PurchasePolicyManager
    {
        public List<PurchasePolicyModel> BindTouristCountry()
        {
            List<PurchasePolicyModel> listPurchasePolicymodel = new List<PurchasePolicyModel>();
            try
            {

                SqlParameter flag = new SqlParameter("@flag", "1");
                DataTable dt = clsDataAccess.ExecuteDataTable(CommandType.StoredProcedure, "Sp_tblCountryCode",  flag);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        PurchasePolicyModel PurchasePolicymodel = new PurchasePolicyModel();
                        PurchasePolicymodel.TouristISOCODE = dt.Rows[i]["ISOCode"].ToString();
                        PurchasePolicymodel.TouristCountryName = dt.Rows[i]["CountryName"].ToString();

                        listPurchasePolicymodel.Add(PurchasePolicymodel);
                    }

                }
                //SqlDataReader ds = clsDataAccess.ExecuteReader(CommandType.StoredProcedure, "Sp_tblCountryCode", flag);
                //if (ds.HasRows)
                //{
                //    while (ds.Read())
                //    {
                //        PurchasePolicyModel PurchasePolicymodel = new PurchasePolicyModel();
                //        PurchasePolicymodel.TouristISOCODE = ds["ISOCode"].ToString();
                //        PurchasePolicymodel.TouristCountryName = ds["CountryName"].ToString();
                //        listPurchasePolicymodel.Add(PurchasePolicymodel);
                //    }
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listPurchasePolicymodel;
        }

        public List<LoginModel> BindHomeCountry()
        {
            List<LoginModel> listLoginModel = new List<LoginModel>();
            try
            {

                SqlParameter flag = new SqlParameter("@flag", "1");
                DataTable dt = clsDataAccess.ExecuteDataTable(CommandType.StoredProcedure, "Sp_tblCountryCode",  flag);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        LoginModel Loginmodel = new LoginModel();
                        Loginmodel.ISOCODE = dt.Rows[i]["ISOCode"].ToString();
                        Loginmodel.CountryName = dt.Rows[i]["CountryName"].ToString();
                        listLoginModel.Add(Loginmodel);

                        
                    }

                }
                //SqlDataReader ds = clsDataAccess.ExecuteReader(CommandType.StoredProcedure, "Sp_tblCountryCode", flag);
                //if (ds.HasRows)
                //{
                //    while (ds.Read())
                //    {
                //        LoginModel Loginmodel = new LoginModel();
                //        Loginmodel.ISOCODE = ds["ISOCode"].ToString();
                //        Loginmodel.CountryName = ds["CountryName"].ToString();
                //        listLoginModel.Add(Loginmodel);
                //    }
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listLoginModel;
        }

        public List<PurchasePolicyModel> getCountryName(string code)
        {
            List<PurchasePolicyModel> purchasepolicymodel = new List<PurchasePolicyModel>();
            try
            {


                SqlParameter flag = new SqlParameter("@flag", "2");
                SqlParameter p1 = new SqlParameter("@ISOCode", code);
                DataTable dt = clsDataAccess.ExecuteDataTable(CommandType.StoredProcedure, "Sp_tblCountryCode", flag, p1);
                if (dt.Rows.Count > 0 && dt.Rows != null)
                {
                    
                        PurchasePolicyModel countryofcitizenship = new PurchasePolicyModel();
                        countryofcitizenship.Countryofcitizenship = dt.Rows[0]["CountryName"].ToString();
                        purchasepolicymodel.Add(countryofcitizenship);
                    
                }
                
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return purchasepolicymodel;
        }

        public List<PurchasePolicyModel> getDestinationCountryName(string code)
        {
            List<PurchasePolicyModel> purchasepolicymodel = new List<PurchasePolicyModel>();
            try
            {


                SqlParameter flag = new SqlParameter("@flag", "2");
                SqlParameter p1 = new SqlParameter("@ISOCode", code);
                DataTable dt = clsDataAccess.ExecuteDataTable(CommandType.StoredProcedure, "Sp_tblCountryCode", flag, p1);
                if (dt.Rows.Count > 0 && dt.Rows != null)
                {

                    PurchasePolicyModel countryofcitizenship = new PurchasePolicyModel();
                    countryofcitizenship.Countryofcitizenship = dt.Rows[0]["CountryName"].ToString();
                    purchasepolicymodel.Add(countryofcitizenship);

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return purchasepolicymodel;
        }

        public string saveDetailsIntripMaster(PurchasePolicyModel PurchasePolicymodel)
        {
            int res = 0;
           
                SqlParameter p1 = new SqlParameter("@Citizenship",PurchasePolicymodel.Countryofcitizenship);
                SqlParameter p2 = new SqlParameter("@Home_Country",PurchasePolicymodel.ISOCODE);
                SqlParameter p3 = new SqlParameter("@Destination_Country",PurchasePolicymodel.WhereareYouTravlingTo);
                SqlParameter p4 = new SqlParameter("@Coverage_Start_date", PurchasePolicymodel.LeavingHome);
                SqlParameter p5 = new SqlParameter("@Coverage_End_Date", PurchasePolicymodel.TillDate);
                SqlParameter p6 = new SqlParameter("@Spouse_Name", PurchasePolicymodel.SpouseName);
                SqlParameter p7 = new SqlParameter("@Spose_DOB", PurchasePolicymodel.SpouseDOB);
                SqlParameter p8 = new SqlParameter("@Child_Name", PurchasePolicymodel.ChildName);
                SqlParameter p9 = new SqlParameter("@Child_DOB", PurchasePolicymodel.ChildDOB);
                //SqlParameter p10 = new SqlParameter("@date_of_Departure_from_Home", Convert.ToDateTime(PurchasePolicymodel.DeparturefromHomeCountry).ToString("MM/dd/yyyy"));
                //SqlParameter p11 = new SqlParameter("@date_Return_Home", Convert.ToDateTime(PurchasePolicymodel.DateofReturntoHome).ToString("MM/dd/yyyy"));
                SqlParameter p12 = new SqlParameter("@Beneficiary_name", PurchasePolicymodel.Beneficiayname);
                SqlParameter p13 = new SqlParameter("@Relationship", PurchasePolicymodel.Relationship);
                SqlParameter p14 = new SqlParameter("@BillingAddress", PurchasePolicymodel.BillingAddress);
                SqlParameter p15 = new SqlParameter("@BillingCountry", PurchasePolicymodel.BilingCountry);
                SqlParameter p16 = new SqlParameter("@Billing_State", PurchasePolicymodel.BillingState);
                SqlParameter p17 = new SqlParameter("@Billing_City", PurchasePolicymodel.BillingCity);
                SqlParameter p18 = new SqlParameter("@BillingZipCode", PurchasePolicymodel.BillingZipcode);
                SqlParameter p19 = new SqlParameter("@Contact_No", PurchasePolicymodel.TravelerMobile);
                SqlParameter p20 = new SqlParameter("@UserID", PurchasePolicymodel.TravelerEmail);
                SqlParameter p21 = new SqlParameter("@PlanID", PurchasePolicymodel.PlanId);
            SqlParameter p22 = new SqlParameter("@Policymax", PurchasePolicymodel.planMaximum);
            SqlParameter p23 = new SqlParameter("@Deductible", PurchasePolicymodel.planDeductible);
            SqlParameter p24 = new SqlParameter("@Total_premium", PurchasePolicymodel.Totalamount);
            SqlParameter p25 = new SqlParameter("@policyname", PurchasePolicymodel.policyName);
                SqlParameter Flag = new SqlParameter("@Flag", "1");
            string result = string.Empty;
            try
            {

                DataTable dt = clsDataAccess.ExecuteDataTable(CommandType.StoredProcedure, "Sp_Trip_Details", p1, p2, p3, p4, p5, p6, p7, p8, p9, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, Flag, p22, p23, p24,p25);

                if (dt.Rows.Count > 0 && dt!=null)
                {
                     result = dt.Rows[0]["ID"].ToString();
                   
                }
               
            }
            catch(Exception ex)
            {
                throw ex;
            }



            return result;
        }

        public int SaveUserdetails(PurchasePolicyModel purchasepolicymodel)
        {
            try
            {

           
            SqlParameter p1 = new SqlParameter("@First_Name", purchasepolicymodel.TravelerFirstName);
            SqlParameter p2 = new SqlParameter("@Last_Name", purchasepolicymodel.TravelerLastName);
            SqlParameter p3 = new SqlParameter("@Gender", purchasepolicymodel.Gender);
            SqlParameter p4 = new SqlParameter("@DOB", purchasepolicymodel.TravelersDOB);
            SqlParameter p5 = new SqlParameter("@Contact_No", purchasepolicymodel.TravelerMobile);
            SqlParameter p6 = new SqlParameter("@Address", purchasepolicymodel.PrimaryApplicantAddress1);
                SqlParameter p7 = new SqlParameter("@EmailID", purchasepolicymodel.TravelerEmail);
            SqlParameter flag = new SqlParameter("Flag", "4");
            
           
                int res = clsDataAccess.ExecuteNonQuery(CommandType.StoredProcedure, "Sp_UserMaster", p1, p2, p3, p4, p5, p6, flag,p7);
                return res;
            }
            catch(Exception ex)
            {
                throw ex;
            }



        }

        public List<PurchasePolicyModel> BindState()
        {
            List<PurchasePolicyModel> purchaemodel = new List<PurchasePolicyModel>();
            SqlParameter flag = new SqlParameter("@Flag", "1");
            DataTable dt = clsDataAccess.ExecuteDataTable(CommandType.StoredProcedure, "Sp_StateCode", flag);
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    PurchasePolicyModel Purchasepolicymodel = new PurchasePolicyModel();
                    Purchasepolicymodel.statename = dt.Rows[i]["State"].ToString();
                    Purchasepolicymodel.Statecode = dt.Rows[i]["StateCode"].ToString();
                    purchaemodel.Add(Purchasepolicymodel);
                   
                   
                }

            }
            //SqlDataReader sd = clsDataAccess.ExecuteReader(CommandType.StoredProcedure, "Sp_StateCode", flag);
            //if (sd.HasRows)
            //{
            //    while (sd.Read())
            //    {
            //        PurchasePolicyModel Purchasepolicymodel = new PurchasePolicyModel();
            //        Purchasepolicymodel.statename = sd["State"].ToString();
            //        Purchasepolicymodel.Statecode = sd["StateCode"].ToString();
            //        purchaemodel.Add(Purchasepolicymodel);
            //    }
            //}
            return purchaemodel;
        }


        public string BindCountryname(string model)
        {
            string Countryname = "";
            List<PurchasePolicyModel> listhomecountry = new  List<PurchasePolicyModel>();
            SqlParameter p1 = new SqlParameter("@ISOCode",model);
            SqlParameter p2 = new SqlParameter("@flag", "2");
            DataTable dt = clsDataAccess.ExecuteDataTable(CommandType.StoredProcedure, "Sp_tblCountryCode", p2, p1);

            if (dt.Rows.Count > 0 && dt.Rows != null)
            {

                PurchasePolicyModel country = new PurchasePolicyModel();
                Countryname = dt.Rows[0]["CountryName"].ToString();
               

            }

            return Countryname;
        }

        public List<PurchasePolicyModel> BindTouristCountryname(PurchasePolicyModel model)
        {
            List<PurchasePolicyModel> listhomecountry = new List<PurchasePolicyModel>();
            SqlParameter p1 = new SqlParameter("@ISOCode", model.TouristISOCODE);
            SqlParameter p2 = new SqlParameter("@flag", "2");
            DataTable dt = clsDataAccess.ExecuteDataTable(CommandType.StoredProcedure, "Sp_tblCountryCode", p2, p1);

            if (dt.Rows.Count > 0 && dt.Rows != null)
            {

                PurchasePolicyModel country = new PurchasePolicyModel();
                country.TouristCountryName = dt.Rows[0]["CountryName"].ToString();
                listhomecountry.Add(country);

            }

            return listhomecountry;
        }

        public string GetIMGCountryCode(string Code)
        {
            string IMGCode = "";
            SqlParameter p1 = new SqlParameter("@IMGCountrycode", Code);
            SqlParameter flag = new SqlParameter("@flag", "5");
            DataTable dt = new DataTable();
            dt = clsDataAccess.ExecuteDataTable(CommandType.StoredProcedure, "Sp_tblCountryCode", p1, flag);
            if (dt != null && dt.Rows.Count > 0)
            {
                IMGCode = dt.Rows[0]["IMGCountryCode"].ToString();
            }
            return IMGCode;
        }


        public void SaveErrorLog(string serviceprovider, string error)
        {
            int res = 0;
            try
            {
                SqlParameter p1 = new SqlParameter("@ServiceProvider", serviceprovider);
                SqlParameter p2 = new SqlParameter("@Error_Message", error);
                SqlParameter flag = new SqlParameter("@flag", "1");
                res = clsDataAccess.ExecuteNonQuery(CommandType.StoredProcedure, "sp_log_master", p1, p2, flag);

            }
            catch (Exception ex)
            {

            }
        }

    }
}
