using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TouristInsured.Models
{
    public class IMGPaymentResponse
    {

        public string certificateNumber { get; set; }
        public DateTime effectiveDate { get; set; }
        public DateTime expirationDate { get; set; }
        public double totalPremium { get; set; }
        public List<ImgPaymentInsured> insureds { get; set; }
        public List<Imgpaymentbrokenrules> brokenRules { get; set; }
        public List<string> errors { get; set; }


    }
}