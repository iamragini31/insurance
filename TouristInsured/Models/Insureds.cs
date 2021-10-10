using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TouristInsured.Models
{
    [JsonObject("Insureds")]
    public class Insureds
    {
        public DateTime dateOfBirth { get; set; }
        public string citizenship { get; set; }
        public string residence { get; set; }
        public string travelerType { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        //public IList<ProductOptions> productOptions { get; set; }
        //public IList<string> riders { get; set; }
    }

    [JsonObject("Insureds")]
    public class Insureds_with_Riders
    {
        public DateTime dateOfBirth { get; set; }
        public string citizenship { get; set; }
        public string residence { get; set; }
        public string travelerType { get; set; }

        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        //public IList<ProductOptions> productOptions { get; set; }
        public List<string> riders { get; set; }
    }
}