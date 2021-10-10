using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TouristInsured.Models
{
    public class HccStudentPurchaseRequest
    {
        public string ReferId { get; set; }
        public string Culture { get; set; }
        public string CoverageArea { get; set; }
        public string CoverageBeginDt { get; set; }
        public string CoverageEndDt { get; set; }
        public string OnlineFulFill { get; set; }
        public string MailOpt { get; set; }
        public string ShippingCost { get; set; }
        public string SelectedPaymentType { get; set; }
        public string SelectedPlanType { get; set; }
        public string PrimaryBeneficiary { get; set; }
        public string PrimaryCitizenship { get; set; }
        public string PrimaryDob { get; set; }
        public string PrimaryEligibleRequirements { get; set; }
        public string PrimaryEmailAddr { get; set; }
        public string PrimaryGender { get; set; }
        public string PrimaryHomeCountry { get; set; }
        public string PrimaryHostCountry { get; set; }
        public string PrimaryMailAddr1 { get; set; }
        public string PrimaryMailAddr2 { get; set; }
        public string PrimaryMailCity { get; set; }
        public string PrimaryMailCountry { get; set; }
        public string PrimaryMailState { get; set; }
        public string PhysicallyLocated { get; set; }
        public string PrimaryMailZip { get; set; }
        public string PrimaryNameFirst { get; set; }
        public string PrimaryNameLast { get; set; }
        public string PrimaryNameMiddle { get; set; }
        public string PrimaryPhone { get; set; }
        public string PrimaryStudentScholarStatus { get; set; }
        public string PrimaryUniversity { get; set; }
        public string PrimaryUsCitizenOrResident { get; set; }
        public string PrimaryUsState { get; set; }
        public string PrimaryVisaType { get; set; }
        private List<HccStudentCreditcardDetails> CreditCarddetails = new List<HccStudentCreditcardDetails>();
        public List<HccStudentCreditcardDetails> CreditCard
        {
            get { return CreditCarddetails; }
            set { CreditCarddetails = value; }
        }
    }


    public class HccStudentCreditcardDetails
    {

        public string CardExpirationMonth { get; set; }
        public string CardExpirationYear { get; set; }
        public string CardHolderAddress1 { get; set; }
        public string CardHolderAddress2 { get; set; }
        public string CardHolderCity { get; set; }

        public string CardHolderCountry { get; set; }
        public string CardHolderDayTimePhone { get; set; }
        public string CardHolderName { get; set; }
        public string CardHolderState { get; set; }
        public string CardHolderZip { get; set; }
        public string CardNumber { get; set; }
        public string CardSecurityCode { get; set; }
        public string PaymentMethod { get; set; }


    }

}