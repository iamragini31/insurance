using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristInsuredEntity;
using DataAccessLayer;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;

namespace TouristInsuredManager
{
    public class LoginManager
    {
        public List<LoginModel> BindHomeCountry()
        {
            List<LoginModel> listLoginModel = new List<LoginModel>();
            try
            {

                SqlParameter flag = new SqlParameter("@flag", "1");
                DataTable dt = clsDataAccess.ExecuteDataTable(CommandType.StoredProcedure, "Sp_tblCountryCode", flag);
                if(dt !=null && dt.Rows.Count > 0)
                {
                    for (int i=0;i<dt.Rows.Count;i++)
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
        public List<LoginModel> BindTouristCountry()
        {
            List<LoginModel> listLoginModel = new List<LoginModel>();
            try
            {

                SqlParameter flag = new SqlParameter("@flag", "6");
                DataTable dt = clsDataAccess.ExecuteDataTable(CommandType.StoredProcedure, "Sp_tblCountryCode", flag);
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
                //        Loginmodel.TouristISOCODE = ds["ISOCode"].ToString();
                //        Loginmodel.TouristCountryName = ds["CountryName"].ToString();
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


        public List<LoginModel> GetSchengenCountryName(string prefix)
        {

            List<LoginModel> login = new List<LoginModel>();
            try
            {
                //SqlParameter sp1 = new SqlParameter("@Country_name", prefix);
                SqlParameter flag = new SqlParameter("@Flag", "6");
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
            res = clsDataAccess.ExecuteNonQuery(CommandType.StoredProcedure, "Sp_UserMaster",  p4,  p8, Flag);
            if (res > 0)
            {
                return res;
            }
            else
            {
                return 0;
            }

        }
    }
}
