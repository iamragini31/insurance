using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristInsuredEntity;
using DataAccessLayer;
using System.Data;
using System.Data.SqlClient;

namespace TouristInsuredManager
{
  public  class SignupManager
    {
        public int SaveUser(SignupModel signup)
        {
            SqlParameter p1 = new SqlParameter("@First_Name", signup.First_Name);
            SqlParameter p2 = new SqlParameter("@Last_Name", signup.Last_Name);
            SqlParameter p3 = new SqlParameter("@Gender", signup.Gender);
            SqlParameter p4 = new SqlParameter("@EmailID", signup.EmailID);
            SqlParameter p5 = new SqlParameter("@DOB", signup.DOB);
            SqlParameter p6 = new SqlParameter("@Contact_No", signup.Contact_No);
            SqlParameter p7 = new SqlParameter("@Address", signup.Address);
            SqlParameter p8 = new SqlParameter("@Password", signup.Password);
            SqlParameter Flag = new SqlParameter("@Flag", "1");
            int res = 0;
            res = clsDataAccess.ExecuteNonQuery(CommandType.StoredProcedure, "Sp_UserMaster", p1, p2, p3, p4, p5, p6, p7, p8, Flag);
            if(res > 0)
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
