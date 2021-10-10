using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TouristInsured.Models
{
    public class CompareResponse
    {
        public string exactamount { get; set; }
        public string getplanmaximum { get; set; }
        public string getPlanDeductible { get; set; }
        public string LeavingHome { get; set; }
        public string TillDate { get; set; }
        public string ISOCODE { get; set; }
        public string TouristISOCODE { get; set; }
        public string TravelersDOB { get; set; }
        public string policyname { get; set; }
        public int planID { get; set; }
        public string Appname { get; set; }

    }
}