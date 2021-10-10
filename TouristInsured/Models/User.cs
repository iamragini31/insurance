using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TouristInsured.Models
{
    public class User
    {
        public string quoteStartDate { get; set; }
        public string quoteEndDate { get; set; }
        public string coverageArea { get; set; }

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

    }
}