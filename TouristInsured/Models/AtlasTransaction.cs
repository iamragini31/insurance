using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TouristInsured.Models
{
    [JsonObject("Transaction")]
    public class AtlasTransaction
    {
        public double Amount { get; set; }
    }
}