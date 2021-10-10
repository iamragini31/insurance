using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TouristInsured.Models
{
    public class TrawickErrorResponse
    {

        public string QuoteResponse_id { get; set; }
        public string ProductId { get; set; }
        public string OrderRequestId { get; set; }
        public string  PrimaryMemberId{get;set;}
    public string OrderStatusMessage { get; set; }
    public string OrderStatusCode { get; set; }
    public string TotalPrice { get; set; }
    public string QuoteNumber { get; set; }
    public string BuyNowLink { get; set; }
    public string UsedMinTime { get; set; }
    public string QuotedEffDate { get; set; }
    public string QuotedTermDate { get; set; }
    public string DayCount { get; set; }
    public string MinTimeReport { get; set; }
    public string QuoteResponseDetails { get; set; }

    }
}