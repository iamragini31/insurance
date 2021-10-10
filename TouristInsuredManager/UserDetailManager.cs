using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristInsuredEntity;
using DataAccessLayer;
using System.Data.SqlClient;
using System.Data;

namespace TouristInsuredManager
{
    
    public class UserDetailManager
    {
        public List<UserDetailModel> GetdetailsofUser( string userid)
        {
            List<UserDetailModel> userdetailsmodel = new List<UserDetailModel>();
            try
            {
                SqlParameter p2 = new SqlParameter("@EmailID", userid);
                SqlParameter p1 = new SqlParameter("@Flag", "2");
                DataTable dt = clsDataAccess.ExecuteDataTable(CommandType.StoredProcedure, "Sp_UserMaster", p1,p2);
                if(dt !=null && dt.Rows.Count > 0)
                {
                    UserDetailModel usermodel = new UserDetailModel();
                    usermodel.Address = dt.Rows[0]["Address"].ToString();
                    usermodel.Last_Name= dt.Rows[0]["Last_Name"].ToString();
                    usermodel.Gender = dt.Rows[0]["Gender"].ToString();
                    usermodel.First_Name= dt.Rows[0]["First_Name"].ToString();
                    usermodel.DOB= dt.Rows[0]["DOB"].ToString();
                    usermodel.Contact_No= Convert.ToInt64(dt.Rows[0]["Contact_No"]);
                    
                    userdetailsmodel.Add(usermodel);
                
                }

            }
            catch(Exception ex)
            {
                throw ex;
            }
            return userdetailsmodel;
        }


        public int UpdateUserDetails( UserDetailModel usermodel)
        {
            SqlParameter p1 = new SqlParameter("@First_Name", usermodel.First_Name);
            SqlParameter p2 = new SqlParameter("@Last_Name", usermodel.Last_Name);
            SqlParameter p3 = new SqlParameter("@Gender", usermodel.Gender);
            
            SqlParameter p5 = new SqlParameter("@DOB", usermodel.DOB);
            SqlParameter p6 = new SqlParameter("@Contact_No", usermodel.Contact_No);
            SqlParameter p7 = new SqlParameter("@Address", usermodel.Address);
            SqlParameter p8 = new SqlParameter("@EmailID", usermodel.EmailID);
            SqlParameter Flag = new SqlParameter("@Flag", "3");
            int res = 0;
            res = clsDataAccess.ExecuteNonQuery(CommandType.StoredProcedure, "Sp_UserMaster", p1, p2, p3, p5, p6, p7,p8, Flag);
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
