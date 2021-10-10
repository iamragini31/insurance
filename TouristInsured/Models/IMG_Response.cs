using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TouristInsured.Models
{
    public class IMG_Response
    {
        public double TotalPremium { get; set; }
        public DateTimeOffset EffectiveDate { get; set; }
        public DateTimeOffset ExpirationDate { get; set; }
        public List<Detail> Details { get; set; }
        public List<object> BrokenRules { get; set; }
        public object Errors { get; set; }
    }

    public partial class Detail
    {
        public string ClassType { get; set; }
        public string CoverageType { get; set; }
        public long Id { get; set; }
        public object RiderType { get; set; }
        public double Amount { get; set; }
        public long Tax { get; set; }
        public double Total { get; set; }
    }
}