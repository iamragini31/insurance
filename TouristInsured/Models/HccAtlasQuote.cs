using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace TouristInsured.Models
{

    public class HccAtlasQuote
    {
        //public List<HccAtlasApplicantResponse> ApplicantList { get; set; }
        //public string Premium { get; set; }
        //public string OptionalCoverage { get; set; }
        //public string ShippingCost { get; set; }
        //public string Tax { get; set; }
        //public string TotalPremium { get; set; }
        //public double Total { get; set; }
        public List<HccAtlasApplicantResponse> ApplicantList { get; set; }
        public double Premium { get; set; }
        public double OptionalCoverage { get; set; }
        public double ShippingCost { get; set; }
        public double Tax { get; set; }
        public double TotalPremium { get; set; }
        public double Total { get; set; }


    }
}