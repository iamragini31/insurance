using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TouristInsured.Models
{
    public class IMG_Request
    {

        public string producerNumber { get; set; }
        public string productCode { get; set; }
        public string appType { get; set; }
        public int numberOfDays { get; set; }
        //public TravelInfo travelInfo { get; set; }
        private List<TravelInfo> Traveldetails = new List<TravelInfo>();
        public List<TravelInfo> travelInfo
        {
            get { return Traveldetails; }
            set { Traveldetails = value; }
        }
        //public PolicyInfo policyInfo { get; set; }
        private List<PolicyInfo> Policydetails = new List<PolicyInfo>();
        public List<PolicyInfo> policyInfo
        {
            get { return Policydetails; }
            set { Policydetails = value; }
        }
        public List<Families> families { get; set; }

        //public IList<string> riders { get; set; }
        //public IList<ProductOptions> productOptions { get; set; }

     
      
        //public class ProductOptions
        //{
        //    public string productOptionType { get; set; }
        //    public string selectedValue { get; set; }

        //}
        //public class Insureds
        //{
        //    public DateTime dateOfBirth { get; set; }
        //    public string citizenship { get; set; }
        //    public string residence { get; set; }
        //    public string travelerType { get; set; }
        //    public DateTime startDate { get; set; }
        //    public DateTime endDate { get; set; }
        //    public IList<ProductOptions> productOptions { get; set; }
        //    public IList<string> riders { get; set; }

        //}
        //public class Families
        //{
        //    public IList<Insureds> insureds { get; set; }

        //}
    }

    public class TravelInfo
    {
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public DateTime dateOfUsDeparture { get; set; }
        public List<string> destinations { get; set; }

    }

    public class PolicyInfo
    {
        public int deductible { get; set; }
        public int maximumLimit { get; set; }
        public string currencyCode { get; set; }
        public string fulfillmentMethod { get; set; }

    }
    public class IMG_Request_with_Riders
    {

        public string producerNumber { get; set; }
        public string productCode { get; set; }
        public string appType { get; set; }
        public int numberOfDays { get; set; }
        //public TravelInfo travelInfo { get; set; }
        private List<TravelInfo> Traveldetails = new List<TravelInfo>();
        public List<TravelInfo> travelInfo
        {
            get { return Traveldetails; }
            set { Traveldetails = value; }
        }
        //public PolicyInfo policyInfo { get; set; }
        private List<PolicyInfo> Policydetails = new List<PolicyInfo>();
        public List<PolicyInfo> policyInfo
        {
            get { return Policydetails; }
            set { Policydetails = value; }
        }
        public List<Families_withriders> families { get; set; }

        public List<string> riders { get; set; }

    }
}