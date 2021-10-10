using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TouristInsured.Models
{
    public class Trawick_Payment_Response
    {
        //public string ProductId { get; set; }
        //public string OrderRequestId { get; set; }
        //public string OrderNo { get; set; }
        //public string PrimaryMemberId { get; set; }
        //public string OrderStatusMessage { get; set; }
        //public string OrderStatusCode { get; set; }
        //public string TotalPrice { get; set; }
        //public string CreditCardNumber { get; set; }
        //public string PolicyNumber { get; set; }
        //public string TransactionId { get; set; }

        //private List<Member> Members = new List<Member>();
        //public List<Member> MembersInfo
        //{
        //    get { return Members; }
        //    set { Members = value; }
        //}

        //private List<CreditCardResponses> CreditCardResponse = new List<CreditCardResponses>();
        //public List<CreditCardResponses> CreditCardResponseInfo
        //{
        //    get { return CreditCardResponse; }
        //    set { CreditCardResponse = value; }
        //}

        public int ProductId { get; set; }
        public int OrderRequestId { get; set; }
        public int OrderNo { get; set; }
        public int PrimaryMemberId { get; set; }
        public string OrderStatusMessage { get; set; }
        public int OrderStatusCode { get; set; }
        public double TotalPrice { get; set; }
        public string CreditCardNumber { get; set; }
        public string PolicyNumber { get; set; }
        public string TransactionId { get; set; }
        public List<Member> Members { get; set; }
        public CreditCardResponses CreditCardResponse { get; set; }
    }
}