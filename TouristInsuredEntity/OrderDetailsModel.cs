using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TouristInsuredEntity
{
    public class OrderDetailsModel
    {
       public string TranactionNO { get; set; }
       public string AuthCode { get; set; }
       public string certificatelink { get; set; }
        public double Total_premium { get; set; }
        public double  Premium { get; set; }
        public string  PolicyNumber { get; set; }
         public string Coverage_start_Date { get; set; }
public string Coverage_End_Date { get; set; }
public string Policymax { get; set; }
public string Deductible { get; set; }
public string policyName { get; set; }
    }
}
