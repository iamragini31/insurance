using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TouristInsured.Models
{
    public class ApplicantList
    {
        public string Dob { get; set; }
        //public int Age { get; set; }
        //public double Premium { get; set; }
        //public double OptionalCoverage { get; set; }
        //public double Tax { get; set; }
    }

    public class SecondaryCoverageList
    {
        public string CoverageType { get; set; }
    }
}