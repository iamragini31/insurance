using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TouristInsured.Models
{
    [JsonObject("brokenrules")]
    public class Imgpaymentbrokenrules
    {
      public string ruleSet { get; set; }
      public int ruleGroupId { get; set; }
      public string id { get; set; }
        public string name { get; set; }
        public string status { get; set; }
        public string correlationId { get; set; }
    }
}