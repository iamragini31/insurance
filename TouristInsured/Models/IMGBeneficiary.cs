using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TouristInsured.Models
{
    [JsonObject("beneficiary")]
    public class IMGBeneficiary
    {
      public string name { get; set; }
        public string relationship { get; set; }
        public int percentage { get; set; }
        public string beneficiaryType { get; set; }
    }
}