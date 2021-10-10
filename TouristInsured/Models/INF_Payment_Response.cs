using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TouristInsured.Models
{
    public class INF_Payment_Response
    {
        public string premium { get; set; }
        public double totalPremium { get; set; }
        public string transactionId { get; set; }
        public string applicationFee { get; set; }
        public string policyName { get; set; }
        public string applicationFeeType { get; set; }
        public string policyNumber { get; set; }
        public string coverageLength { get; set; }
        public string certificateLink { get; set; }
    }
}