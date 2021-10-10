using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TouristInsured.Models
{
    public class hccStudentRequest
    {

        public string ReferId { get; set; }
        public string Culture { get; set; }
        public bool USDest { get; set; }
        public string CoverageBeginDt { get; set; }
        public string CoverageEndDt { get; set; }
        public string PlanType { get; set; }
        public string PaymentType { get; set; }
        public string MailOpt { get; set; }
        public bool SurlpusLines { get; set; }
        public List<ApplicantList> ApplicantList { get; set; }


    }

    public class StudentApplicantList
    {
      
        public string Dob { get; set; }
       
    }


    public class hccStudentRequest_Riders
    {


        public string ReferId { get; set; }
        public string Culture { get; set; }
        public bool USDest { get; set; }
        public string CoverageBeginDt { get; set; }
        public string CoverageEndDt { get; set; }
        public string PlanType { get; set; }
        public string PaymentType { get; set; }
        public string MailOpt { get; set; }
        public bool SurlpusLines { get; set; }
        public List<ApplicantList> ApplicantList { get; set; }

        public List<SecondaryCoverageList> SecondaryCoverageList { get; set; }
    }
}





