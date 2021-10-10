using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace TouristInsured.Models
{
    public class Request
    {
        public int product { get; set; }
        public string eff_date { get; set; }
        public string term_date { get; set; }
        public string country { get; set; }
        public string destination { get; set; }
        public int policy_max { get; set; }
        public int deductible { get; set; }
        public string dob1 { get; set; }
    }
    public class QuoteResponseDetail
    {
        public int Id { get; set; }
        public int QuoteResponse_id { get; set; }
        public double PriceQuoted { get; set; }
        public string Description { get; set; }
        public object TravlerAgeId { get; set; }
        public object QuoteResponse { get; set; }
    }

    public class RootObject
    {
        public int QuoteResponse_id { get; set; }
        public int ProductId { get; set; }
        public int OrderRequestId { get; set; }
        public int PrimaryMemberId { get; set; }
        public object OrderStatusMessage { get; set; }
        public int OrderStatusCode { get; set; }
        public double TotalPrice { get; set; }
        public int QuoteNumber { get; set; }
        public string BuyNowLink { get; set; }
        public bool UsedMinTime { get; set; }
        public string QuotedEffDate { get; set; }
        public string QuotedTermDate { get; set; }
        public string DayCount { get; set; }
        public object MinTimeReport { get; set; }
        public List<QuoteResponseDetail> QuoteResponseDetails { get; set; }
    }
}