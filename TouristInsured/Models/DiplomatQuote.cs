using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TouristInsured.Models
{
    public class DiplomatQuote
    {
        public string PolicyStartDate { get; set; }
        public string PolicyEndDate { get; set; }
        public int LTNumberOfMonths { get; set; }
        public string Plan { get; set; }
        public string TravelerOneAgeRange { get; set; }
        public string TravelerTwoAgeRange { get; set; }
        public string Deductible { get; set; }
        public string ADDBenefit { get; set; }
        public int NumberOfMinorDependents { get; set; }
        public List<string> Riders { get; set; }
        public List<string> CountryIso2Codes { get; set; }
        public string WarRiskCoverage { get; set; }
    }
}