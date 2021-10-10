using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TouristInsured.Models
{
    public class TrawickVisitorstousResponse
    {
         public int products_id { get; set; }
        public string Plan { get; set; }
        public int plan_id { get; set; }
        public int Min_Age { get; set; }
        public int Max_Age { get; set; }
        public double Rate { get; set; }
        public bool RenewalOnly { get; set; }
        public double SetRate { get; set; }
    }


    public class TrawickSchengen
    {
        //public double Total { get; set; }
        public int QuoteResponse_id { get; set; }
        public int ProductId { get; set; }
        public int OrderRequestId { get; set; }
        public int PrimaryMemberId { get; set; }
        public string OrderStatusMessage { get; set; }
        public int OrderStatusCode { get; set; }
        public double TotalPrice { get; set; }
        public long QuoteNumber { get; set; }
        public string BuyNowLink { get; set; }
        public bool UsedMinTime { get; set; }
        public string QuotedEffDate { get; set; }
        public string QuotedTermDate { get; set; }
        public string DayCount { get; set; }
        public string MinTimeReport { get; set; }
        public IList<QuoteResponseDetails> QuoteResponseDetail { get; set; }

    }

    [JsonObject("QuoteResponseDetails")] 
    public class QuoteResponseDetails
    {
            public int Id { get; set; }
        public int QuoteResponse_id { get; set; }
        public double PriceQuoted { get; set; }
        public string Description { get; set; }
        public long TravlerAgeId { get; set; }
        public string QuoteResponse { get; set; }
    }
    public class checkresult
    {
        public IList<check> QuoteResponseDetail { get; set; }
    }
    public class check
    {
        public int Id { get; set; }
        public int QuoteResponse_id { get; set; }
        public double PriceQuoted { get; set; }
        public string Description { get; set; }
        public long TravlerAgeId { get; set; }
        public string QuoteResponse { get; set; }
    }
}