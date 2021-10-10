using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TouristInsured.Models
{
    public class HccPurchase
    {
        public string ReferId { get; set; }
        public string USDest { get; set; }
        public string CoverageBeginDt { get; set; }
        public string CoverageEndDt { get; set; }
        public string ArriveInUSDt { get; set; }
        public string PrimaryHome { get; set; }
        public string PrimaryDestination { get; set; }
        public string PrimaryBeneficiary { get; set; }
        public string PhysicallyLocatedState { get; set; }
        public string PhysicallyLocatedCountry { get; set; }
        public string Plan { get; set; }
        public string PlanName { get; set; }
        public string Deductible { get; set; }
        public string PrimaryFlWork { get; set; }
        public string MailOption { get; set; }
        public string OnlineFulfill { get; set; }
        public string ShippingCost { get; set; }
        public string PrimaryAddr1 { get; set; }
        public string PrimaryAddr2 { get; set; }
        public string PrimaryCity { get; set; }
        public string PrimaryCountry { get; set; }
        public string PrimaryState { get; set; }
        public string PrimaryZip { get; set; }
        public string PrimaryHomePhone { get; set; }
        public string PrimaryEmailAddr { get; set; }
        public string PrimaryCitizenship { get; set; }
        public string PrimaryDob { get; set; }
        public string PrimaryGender { get; set; }
        public string PrimaryNameFirst { get; set; }
        public string PrimaryNameLast { get; set; }
        public string PrimaryNameMiddle { get; set; }
        public string SpouseCitizenship { get; set; }
        public string SpouseDob { get; set; }
        public string SpouseGender { get; set; }
        public string SpouseNameFirst { get; set; }
        public string SpouseNameLast { get; set; }
        public string SpouseNameMiddle { get; set; }
        public string QuoteTotal { get; set; }
        public string SendToName { get; set; }
        public List<DependentList> DependentList { get; set; }
        private List<CreditCard> CardDetails = new List<CreditCard>();
        public List<CreditCard> CreditCard
        {
            get { return CardDetails; }
            set { CardDetails = value; }
        }

}
    public class CreditCard
    {
        public int CardExpirationMonth { get; set; }
        public int  CardExpirationYear { get; set; }
        public string CardFirstName { get; set; }
        public string CardHolderAddress1 { get; set; }
        public string CardHolderAddress2 { get; set; }
        public string CardHolderCity { get; set; }
        public string CardHolderCountry { get; set; }
        public string CardHolderDayTimePhone { get; set; }
        public string CardHolderName { get; set; }
        public string CardHolderState { get; set; }
        public string CardHolderZip { get; set; }
        public string CardLastName { get; set; }
        public string CardMiddleName { get; set; }
        public string CardNumber{ get; set; }
public string CardSecurityCode { get; set; }
        public int CreditCardID { get; set; }
        public string ModifiedBy { get; set; }
        public string ModifiedOn { get; set; }
        public string PaymentMethod { get; set; }
    }
}