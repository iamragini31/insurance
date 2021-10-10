using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using DataAccessLayer;
using TouristInsuredEntity;



namespace TouristInsuredManager
{
   public class ComparePolicyManager
    {
        public DataTable Fetchdata(string Appname1, string Appname2)
        {
            var Planid = Appname1 + "," + Appname2;
            SqlParameter p1 = new SqlParameter("@PlanID", Planid);
            DataTable dt = clsDataAccess.ExecuteDataTable(CommandType.StoredProcedure, "sp_GetPlanProperty", p1);



            return dt;
            
        }

        public DataTable FetchAlldata(string Appname1, string Appname2, string Appname3)
        {
            var Planid = Appname1 + "," + Appname2 +"," + Appname3;
            SqlParameter p1 = new SqlParameter("@PlanID", Planid);
            DataTable dt = clsDataAccess.ExecuteDataTable(CommandType.StoredProcedure, "sp_GetPlanProperty", p1);



            return dt;

        }
    }
}
