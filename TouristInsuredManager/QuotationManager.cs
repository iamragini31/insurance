using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using System.Data;
using System.Data.SqlClient;
using TouristInsuredEntity;

namespace TouristInsuredManager
{
   public class QuotationManager
    {
        public string GetIMGCountryCode(string Code)
        {
            string IMGCode = "";
            SqlParameter p1 = new SqlParameter("@IMGCountrycode", Code);
            SqlParameter flag = new SqlParameter("@flag", "5");
            DataTable dt = new DataTable();
            dt = clsDataAccess.ExecuteDataTable(CommandType.StoredProcedure, "Sp_tblCountryCode", p1, flag);
            if(dt !=null && dt.Rows.Count > 0)
            {
                IMGCode = dt.Rows[0]["IMGCountryCode"].ToString();
            }
            return IMGCode;
        }


        public List<LoginModel> Validateuser(LoginModel Loginmodel)
        {
            List<LoginModel> model = new List<LoginModel>();


            SqlParameter p1 = new SqlParameter("@UserID", Loginmodel.userid);
            SqlParameter p2 = new SqlParameter("@Password", Loginmodel.password);
            SqlParameter flag = new SqlParameter("@Flag", "1");
            DataTable dt = clsDataAccess.ExecuteDataTable(CommandType.StoredProcedure, "sp_login_Master", p1, p2, flag);
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    LoginModel login = new LoginModel();

                    login.userid = dt.Rows[i]["UserID"].ToString();
                    model.Add(login);
                }

            }
            //SqlDataReader dr = clsDataAccess.ExecuteReader(CommandType.StoredProcedure, "sp_login_Master", p1, p2, flag);
            //if (dr.HasRows)
            //{
            //    while (dr.Read())
            //    {
            //        LoginModel login = new LoginModel();
            //        login.userid = dr["UserID"].ToString();
            //        model.Add(login);
            //    }
            //}
            return model;


        }

        public int SaveUser(LoginModel signup)
        {

            SqlParameter p4 = new SqlParameter("@EmailID", signup.userid);

            SqlParameter p8 = new SqlParameter("@Password", signup.password);
            SqlParameter Flag = new SqlParameter("@Flag", "1");
            int res = 0;
            res = clsDataAccess.ExecuteNonQuery(CommandType.StoredProcedure, "Sp_UserMaster", p4, p8, Flag);
            if (res > 0)
            {
                return res;
            }
            else
            {
                return 0;
            }

        }


        public void SaveErrorLog(string serviceprovider,string error)
        {
            int res = 0;
            try
            {
                SqlParameter p1 = new SqlParameter("@ServiceProvider", serviceprovider);
                SqlParameter p2 = new SqlParameter("@Error_Message", error);
                SqlParameter flag = new SqlParameter("@flag","1");
                res = clsDataAccess.ExecuteNonQuery(CommandType.StoredProcedure, "sp_log_master", p1, p2, flag);
               
            }
            catch(Exception ex)
            {
               
            }
        }


        public List<QuotationModel> GetPlanDetails(string PlanID)
        {
            List<QuotationModel> PlanDetails = new List<QuotationModel>();
            try
            {
                SqlParameter p1 = new SqlParameter("@Plan_ID", PlanID);
                SqlParameter p2 = new SqlParameter("@Flag", "1");
                DataTable dt = clsDataAccess.ExecuteDataTable(CommandType.StoredProcedure, "Sp_GetPlanDetails", p1, p2);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        QuotationModel list = new QuotationModel();
                        list.Plan_Type = dt.Rows[i]["Plan_Type"].ToString();
                        list.PRE_EXISITING_CONDITION = dt.Rows[i]["PRE_EXISITING_CONDITION"].ToString();
                        list.CANCELABLE = dt.Rows[i]["CANCELABLE"].ToString();
                        list.COUNTRY_RESTRICTIONS = dt.Rows[i]["COUNTRY_RESTRICTIONS"].ToString();
                        list.Out_of_US = dt.Rows[i]["Out_of_US"].ToString();
                        list.COVID_19 = dt.Rows[0]["COVID_19"].ToString();
                        list.Company_Name = dt.Rows[i]["Company_Name"].ToString();
                        list.UW_Name = dt.Rows[i]["UW_Name"].ToString();
                        list.IN_NETWORK = dt.Rows[i]["IN_NETWORK"].ToString();
                        list.OUT_OF_NETWORK = dt.Rows[i]["OUT_OF_NETWORK"].ToString();
                        list.RENEWABLE = dt.Rows[i]["RENEWABLE"].ToString();
                        list.Pre_Existing = dt.Rows[i]["Pre_Existing"].ToString();
                       
                        PlanDetails.Add(list);
                    }

                }
                //SqlDataReader sda = clsDataAccess.ExecuteReader(CommandType.StoredProcedure, "Sp_GetPlanDetails", p1, p2);
                //if (sda.HasRows)
                //{
                //    while (sda.Read())
                //    {
                //        QuotationModel list = new QuotationModel();
                //        list.Plan_Type = sda["Plan_Type"].ToString();
                //        list.PRE_EXISITING_CONDITION= sda["PRE_EXISITING_CONDITION"].ToString();
                //        list.CANCELABLE= sda["CANCELABLE"].ToString();
                //        list.COUNTRY_RESTRICTIONS= sda["COUNTRY_RESTRICTIONS"].ToString();
                //        list.COVID_19= sda["COVID_19"].ToString();
                //        list.Company_Name= sda["Company_Name"].ToString();
                //        list.IN_NETWORK= sda["IN_NETWORK"].ToString();
                //        list.OUT_OF_NETWORK= sda["OUT_OF_NETWORK"].ToString();
                //        list.RENEWABLE= sda["RENEWABLE"].ToString();
                //        PlanDetails.Add(list);
                //    }
                //}

            }
            catch(Exception ex)
            {

            }
            return PlanDetails;
        }



        public List<LoginModel> GetCountryName(string prefix)
        {

            List<LoginModel> login = new List<LoginModel>();
            try
            {
                //SqlParameter sp1 = new SqlParameter("@Country_name", prefix);
                SqlParameter flag = new SqlParameter("@Flag", "1");
                DataTable dt = clsDataAccess.ExecuteDataTable(CommandType.StoredProcedure, "Sp_tblCountryCode", flag);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        LoginModel Loginmodel = new LoginModel();
                        Loginmodel.ISOCODE = dt.Rows[i]["ISOCode"].ToString();
                        Loginmodel.CountryName = dt.Rows[i]["CountryName"].ToString();
                        login.Add(Loginmodel);
                    }

                }
                //SqlDataReader dr = clsDataAccess.ExecuteReader(CommandType.StoredProcedure, "Sp_tblCountryCode", flag);
                //if (dr.HasRows)
                //{
                //    while (dr.Read())
                //    {
                //        LoginModel Loginmodel = new LoginModel();
                //        Loginmodel.ISOCODE = dr["ISOCode"].ToString();
                //        Loginmodel.CountryName = dr["CountryName"].ToString();
                //        login.Add(Loginmodel);
                //    }
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return login;
        }
    }
}
