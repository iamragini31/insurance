using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TouristInsured.Models
{
    [JsonObject("Property")]
    public class PropertyName
    {
        public string ID{ get; set; }
        public string Property_name { get; set; }
        public string Value { get; set; }
    }
}