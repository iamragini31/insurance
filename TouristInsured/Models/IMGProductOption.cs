using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TouristInsured.Models
{
    [JsonObject("productoptions")]
    public class IMGProductOption
    {
        public string productOptionType { get; set; }
        public string selectedValue { get; set; }
    }
}