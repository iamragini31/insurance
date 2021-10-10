using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TouristInsured.Models
{
    [JsonObject("insureds")]
    public class ImgPaymentInsured
    {
       public string insuredId { get; set; }
      public DateTime effectiveDate { get; set; }
        public DateTime expirationDate { get; set; }
        public double amount { get; set; }
    }
}