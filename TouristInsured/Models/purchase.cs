using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TouristInsured.Models
{
    public class purchase
    {
        public int planId { get; set; }
        public string coverageStartDate { get; set; }
        public string coverageEndDate { get; set; }
        public string coverageArea { get; set; }
        public string agreement { get; set; }

        private List<primary> userDetails = new List<primary>();
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string gender { get; set; }
        public string country { get; set; }
        public string passport { get; set; }
        public string email { get; set; }
        public string dob { get; set; }
        public string phone { get; set; }
        public List<primary> primary
        {
            get { return userDetails; }
            set { userDetails = value; }
        }

        private List<payment> carddetails = new List<payment>();
        public string cardNumber { get; set; }
        public string cardExpiry { get; set; }
        public string cardSecurityCode { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zipCode { get; set; }
        public List<payment> paymentInfo
        {
            get { return carddetails; }
            set { carddetails = value; }
        }

        private List<travel> traveldetails = new List<travel>();
        public string arrivalDate { get; set; }
        public string countryToVisit { get; set; }
        public List<travel> travelInfo
        {
            get { return traveldetails; }
            set { traveldetails = value; }
        }

        //private List<adb> adbdetails = new List<adb>();
        public string adbbeneficiary { get; set; }
        public string adbrelation { get; set; }
        //public List<adb> adbInfo
        //{
        //    get { return adbdetails; }
        //    set { adbdetails = value; }
        //}

    }
}