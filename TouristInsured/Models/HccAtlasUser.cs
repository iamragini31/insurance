using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TouristInsured.Models
{
    public class HccAtlasUser
    {
        public string ReferId { get; set; }
        public string Culture { get; set; }
        public bool USDest { get; set; }
        public string CoverageBeginDt { get; set; }
        public string CoverageEndDt { get; set; }
        public string PolicyMax { get; set; }
        public string Deductible { get; set; }
        public string AppName { get; set; }
     
        public string MailOpt { get; set; }
        public bool SurplusLines { get; set; }
        public List<ApplicantList> ApplicantList { get; set; }
    }

    public class HccAtlasUserRider
    {
        public string ReferId { get; set; }
        public string Culture { get; set; }
        public bool USDest { get; set; }
        public string CoverageBeginDt { get; set; }
        public string CoverageEndDt { get; set; }
        public string PolicyMax { get; set; }
        public string Deductible { get; set; }
        public string AppName { get; set; }

        public string MailOpt { get; set; }
        public bool SurplusLines { get; set; }
        public List<ApplicantList> ApplicantList { get; set; }

        public List<SecondaryCoverageList> SecondaryCoverageList { get; set; }
    }
}