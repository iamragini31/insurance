using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TouristInsured.Models
{
    public class quotes
    {
        public int planId { get; set; }
        public string policyName { get; set; }
        public string policyDescription { get; set; }
        public string policyUnderWriter { get; set; }
        public string policyType { get; set; }
        public string policyAdditionalFeatures { get; set; }
        public int planDeductible { get; set; }
        public int planMaximum { get; set; }
        public int coverageLength { get; set; }
        public int coverageClass { get; set; }
        public int preExDeductible { get; set; }
        public int preExCoverageAmount { get; set; }
        public string policyNumber { get; set; }
        public string prexded { get; set; }
        public string prextype { get; set; }
        public float totalPremium { get; set; }

        public string JsonResult { get; set; }

    }
}