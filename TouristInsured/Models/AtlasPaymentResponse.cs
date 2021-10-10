using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TouristInsured.Models
{
    public class AtlasPaymentResponse
    {
     public string Cert { get; set; }
    public string AuthCode { get; set; }
    public string TransactionId { get; set; }
    }
}