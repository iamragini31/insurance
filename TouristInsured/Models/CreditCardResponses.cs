using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TouristInsured.Models
{
    public class CreditCardResponses
    {
        //public string ResponseText { get; set; }
        //public string ResponseCode { get; set; }
        //public string TransactionId { get; set; }
        //public string CvvResponse { get; set; }
        //public string AuthCode { get; set; }
        //public string Success { get; set; }

        public string ResponseText { get; set; }
        public string ResponseCode { get; set; }
        public string TransactionId { get; set; }
        public string CvvResponse { get; set; }
        public string AuthCode { get; set; }
        public bool Success { get; set; }
    }
}