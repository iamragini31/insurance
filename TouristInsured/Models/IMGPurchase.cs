using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TouristInsured.Models
{
    public class IMGPurchase
    {


  public string producerNumber { get; set; }
 public string productCode { get; set; }
 public string appType { get; set; }
 public string signatureName { get; set; }
 public string signatureRelationship { get; set; }
 public string uservar { get; set; }
 public bool includeIdCards { get; set; }
 public int numberOfDays { get; set; }
        private List<IMGTravelinfo> Traveldetails = new List<IMGTravelinfo>();
        public List<IMGTravelinfo> travelInfo
        {
            get { return Traveldetails; }
            set { Traveldetails = value; }
        }
        private List<policyInfo> Policyinfo = new List<policyInfo>();
        public List<policyInfo> policyInfo
        {
            get { return Policyinfo; }
            set {Policyinfo=value; }
        }
  public List<Imgfamilies> families { get; set; }
        public List<IMGcontacts> contacts { get; set; }



        //"intlInsuranceInUS": {
        //  "carrier": "string",
        //  "expireDate": "2020-04-26T07:08:06.257Z"
        //},
        //"groupSponsorInfo": {
        //  "organization": "string",
        //  "firstName": "string",
        //  "lastName": "string"
        //},
        private List<paymentInfo> PaymentInfo = new List<paymentInfo>();
        public List<paymentInfo> paymentInfo
        {
            get { return PaymentInfo; }
            set { PaymentInfo=value; }
        }
        public List<IMGBeneficiary> beneficiaries { get; set; }
 public List<string> riders { get; set; }
public List<IMGProductOption> productOptions { get; set; }
        private List<workTravelInfo> Worktravelinfo = new List<workTravelInfo>();
        public List<workTravelInfo> workTravelInfo
        {
            get { return Worktravelinfo; }
            set { Worktravelinfo = value; }
        }

        private List<chargeInfo> Chargeinfo = new List<chargeInfo>();
        public List<chargeInfo> chargeInfo
        {
            get { return Chargeinfo; }
            set { Chargeinfo = value; }
        }

    }


     public class IMGTravelinfo
{
    public DateTime startDate { get; set; }
    public DateTime endDate { get; set; }
    public List<string> destinations { get; set; }
    public string dateOfUsArrival { get; set; }
    public string dateOfUsDeparture { get; set; }
    public bool isCurrentlyLocatedInFlorida { get; set; }
    }

public class policyInfo
    {
     public int deductible { get; set; }
    public int maximumLimit { get; set; }
    public string currencyCode { get; set; }
    public string paymentFrequency { get; set; }
    public string fulfillmentMethod { get; set; }
}

public class paymentInfo
{
    public string paymentType { get; set; }
        public string echeckType { get; set; }
        public string nameOnAccount { get; set; }
        public string routingNumber { get; set; }
        public string accountNumber { get; set; }
        public string creditCardNumber { get; set; }
        public string cardExpire { get; set; }
        public string cardCVV { get; set; }
        public double requestedTotal { get; set; }
    }

public class workTravelInfo{
        public bool programParticipation { get; set; }
        public string programName { get; set; }
    }

    public class chargeInfo{
        public int cardResult { get; set; }
        public string cardAuthCode { get; set; }
        public string pnref { get; set; }
        public string cardResponseMessage { get; set; }
        public int amount { get; set; }
    }


    public class IMGPurchase_withRiders
    {


        public string producerNumber { get; set; }
        public string productCode { get; set; }
        public string appType { get; set; }
        public string signatureName { get; set; }
        public string signatureRelationship { get; set; }
        public string uservar { get; set; }
        public bool includeIdCards { get; set; }
        public int numberOfDays { get; set; }
        private List<IMGTravelinfo> Traveldetails = new List<IMGTravelinfo>();
        public List<IMGTravelinfo> travelInfo
        {
            get { return Traveldetails; }
            set { Traveldetails = value; }
        }
        private List<policyInfo> Policyinfo = new List<policyInfo>();
        public List<policyInfo> policyInfo
        {
            get { return Policyinfo; }
            set { Policyinfo = value; }
        }
        public List<Imgfamilies> families { get; set; }
        public List<IMGcontacts> contacts { get; set; }

        
        private List<paymentInfo> PaymentInfo = new List<paymentInfo>();
        public List<paymentInfo> paymentInfo
        {
            get { return PaymentInfo; }
            set { PaymentInfo = value; }
        }
        public List<IMGBeneficiary> beneficiaries { get; set; }
        public List<string> riders { get; set; }
        public List<IMGProductOption> productOptions { get; set; }
        private List<workTravelInfo> Worktravelinfo = new List<workTravelInfo>();
        public List<workTravelInfo> workTravelInfo
        {
            get { return Worktravelinfo; }
            set { Worktravelinfo = value; }
        }

        private List<chargeInfo> Chargeinfo = new List<chargeInfo>();
        public List<chargeInfo> chargeInfo
        {
            get { return Chargeinfo; }
            set { Chargeinfo = value; }
        }

    }
}