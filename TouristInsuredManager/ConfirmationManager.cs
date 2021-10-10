using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using TouristInsuredEntity;
using System.Data;
using System.Data.SqlClient;

namespace TouristInsuredManager
{
  public  class ConfirmationManager
    {

        public int SaveTransactionDetails(ConfirmationModel model)
        {
            SqlParameter p1 = new SqlParameter("@AuthCode", model.AuthCode);
            SqlParameter p2 = new SqlParameter("@certificatelink", model.certificatelink);
            SqlParameter p3 = new SqlParameter("@TranactionNO", model.TransactionID);
            SqlParameter p4 = new SqlParameter("@ID", model.TripID);
            SqlParameter p5 = new SqlParameter("@Totalpremium", model.Totalpremium);
            SqlParameter p6 = new SqlParameter("@Premium", model.Premium);
            SqlParameter p7 = new SqlParameter("@PolicyNumber", model.PolicyNumber);
            SqlParameter p8 = new SqlParameter("@policyname", model.policyname);
            SqlParameter p9 = new SqlParameter("@OrderStatusMessage", model.OrderStatusMessage);
            SqlParameter p10 = new SqlParameter("@OrderRequestId", model.OrderRequestId);
            SqlParameter p11 = new SqlParameter("@OrderNo", model.OrderNo);
            SqlParameter p12 = new SqlParameter("@MiddleName", model.MiddleName);
            SqlParameter p13 = new SqlParameter("@LastName", model.LastName);
            SqlParameter p14 = new SqlParameter("@Gender", model.Gender);
            SqlParameter p15 = new SqlParameter("@FirstName", model.FirstName);
            SqlParameter p16 = new SqlParameter("@DOB", model.DOB);
            SqlParameter p17 = new SqlParameter("@coverageLength", model.coverageLength);
            SqlParameter p18 = new SqlParameter("@Applicationfee", model.Applicationfee);
            SqlParameter p19 = new SqlParameter("@code", model.code);
            SqlParameter p20 = new SqlParameter("@message", model.message);
            SqlParameter p21 = new SqlParameter("@ErrorMessage", model.ErrorMessage);
            SqlParameter flag = new SqlParameter("@Flag", "3");
            int res = clsDataAccess.ExecuteNonQuery(CommandType.StoredProcedure, "Sp_Trip_Details", p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18,p19,p20,p21, flag);
            return res;
        }
    }
}
