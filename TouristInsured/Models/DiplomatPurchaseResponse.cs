using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TouristInsured.Models
{
    public class DiplomatPurchaseResponse
    {
        
    public string PolicyNumber { get; set; }
    public double AmountCharged { get; set; }
        public string Message { get; set; }
    }
}