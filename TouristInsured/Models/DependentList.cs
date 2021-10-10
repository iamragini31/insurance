using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace TouristInsured.Models
{
    [JsonObject("DependentList")]
    public class DependentList
    {
        public string NameFirst { get; set; }
        public string NameMiddle { get; set; }
        public string NameLast { get; set; }
        public string Dob { get; set; }
        public string Gender { get; set; }
        public string Citzenship { get; set; }
        public string Passport { get; set; }
    }
}