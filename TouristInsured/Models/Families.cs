using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TouristInsured.Models
{
    [JsonObject("Families")]
    public class Families
    {
        public IList<Insureds> insureds { get; set; }
    }
    [JsonObject("Families")]
    public class Families_withriders
    {
        public IList<Insureds_with_Riders> insureds { get; set; }
    }
}