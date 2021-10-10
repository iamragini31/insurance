using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TouristInsured.Models
{
    public class HCC_User
    {
        public string ReferId { get; set; }
        public string Culture { get; set; } 
        public bool USDest { get; set; }
        public string CoverageBeginDt { get; set; }
        public string CoverageEndDt { get; set; }
        public string ArriveInUSDate { get; set; }
        public string Deductible { get; set; }
        public string Plan { get; set; }
        public string PlanName { get; set; }
        public string MailOpt { get; set; }
        public bool SurplusLines { get; set; }
        public List<ApplicantList> ApplicantList { get; set; }
        //public List<ApplicantList> ApplicationList { get; set; }
        //    private List<ApplicantList> userDOB = new List<ApplicantList>();
        //public List<ApplicantList> ApplicantList
        //{
        //    get { return userDOB; }
        //    set { userDOB = value; }
        //}
    }
}