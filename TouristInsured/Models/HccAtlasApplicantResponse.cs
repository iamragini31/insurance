using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TouristInsured.Models
{
    public class HccAtlasApplicantResponse
    {
        public string Dob { get; set; }
        public string Age { get; set; }
        public string Premium { get; set; }
        public string OptionalCoverage { get; set; }
        public string Tax { get; set; }
    }
}