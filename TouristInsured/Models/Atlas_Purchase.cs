using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TouristInsured.Models
{
    public class Atlas_Purchase
    {
        public int ReferId { get; set; }
        public string Culture { get; set; }
        public int ShowAuthCodes { get; set; }
        public int USDest { get; set; }
        public string CoverageBeginDt { get; set; }
        public string CoverageEndDt { get; set; }
        public string PrimaryHomeCountry { get; set; }
        public List<string> DestinationList { get; set; }
        public string Beneficiary { get; set; }
        public int PhysicallyLocatedState { get; set; }
        public int PhysicallyLocatedCountry { get; set; }
        public long PolicyMax { get; set; }
        public long Deductible { get; set; }
        public string AppName { get; set; }
        public int PrimaryFlWork { get; set; }
        public string MailOpt { get; set; }
        public int OnlineFulFill { get; set; }
        public string ShippingCost { get; set; }
        public string SendToName { get; set; }
        public string MailAddress1 { get; set; }
        public string MailAddress { get; set; }
        public string MailCity { get; set; }
        public string MailCountry { get; set; }
        public string MailState { get; set; }
        public long MailZip { get; set; }
        public string Phone { get; set; }
        public string EmailAddress { get; set; }
        public List<AtlasApplicantList> ApplicantList { get; set; }

        public List<AtlasCreditCardDetails> CreditCard { get; set; }


    }
}