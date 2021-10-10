using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TouristInsured.Models
{
    public class Hcc_Quote
    {
        public List<HCCQuoteApplicantresponse> ApplicantList { get; set; }
        public string Premium { get; set; }
    public string ShippingCost { get; set; }
    public string Tax { get; set; }
    public string TotalPremium { get; set; }
   public double Total { get; set; }
    
    }
}