using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TouristInsured.Models
{
    [JsonObject("CreditCard")]
    public class AtlasCreditCardDetails
    {
        public int CardExpirationMonth { get; set; }
        public int CardExpirationYear { get; set; }
        public string CardHolderAddress1 { get; set; }
        public string CardHolderAddress2 { get; set; }
        public string CardHolderCity { get; set; }
        public string CardHolderCountry { get; set; }
        public string CardHolderDayTimePhone { get; set; }
        public string CardHolderName { get; set; }
        public string CardHolderState { get; set; }
        public long CardHolderZip { get; set; }
        public string CardNumber { get; set; }
        public int CardSecurityCode { get; set; }
        public int CreditCardID { get; set; }
        public string PaymentMethod { get; set; }

        public List<AtlasTransaction> Transaction { get; set; }


    }
}