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
    public class OrderDetailsManager


    {

        public List<OrderDetailsModel> GetOrderDetails(string userid)
        {
            List<OrderDetailsModel> listorderdetails = new List<OrderDetailsModel>();
            SqlParameter Flag = new SqlParameter("@Flag", "1");
            SqlParameter p1 = new SqlParameter("@Userid", userid);
            SqlDataReader sqlDataReader = clsDataAccess.ExecuteReader(CommandType.StoredProcedure, "Sp_Order_Details", Flag,p1);
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    OrderDetailsModel orderdetailsmodel = new OrderDetailsModel();
                    orderdetailsmodel.TranactionNO = (sqlDataReader["TranactionNO"]).ToString();
                    orderdetailsmodel.AuthCode = sqlDataReader["AuthCode"].ToString();
                    orderdetailsmodel.certificatelink = sqlDataReader["certificatelink"].ToString();
                    orderdetailsmodel.Coverage_start_Date = sqlDataReader["Coverage_start_Date"].ToString();
                    orderdetailsmodel.Coverage_End_Date = sqlDataReader["Coverage_End_Date"].ToString();
                    //orderdetailsmodel.Premium = Convert.ToDouble(sqlDataReader[""]);
                    orderdetailsmodel.Total_premium = Convert.ToDouble(sqlDataReader["Total_premium"]);
                    orderdetailsmodel.PolicyNumber = sqlDataReader["PolicyNumber"].ToString();
                    orderdetailsmodel.policyName = sqlDataReader["policyName"].ToString();
                    orderdetailsmodel.Policymax = (sqlDataReader["Policymax"]).ToString();
                    orderdetailsmodel.Deductible = sqlDataReader["Deductible"].ToString();
                    listorderdetails.Add(orderdetailsmodel);




                   
                }
            }
            return listorderdetails;
        }
    }
}
