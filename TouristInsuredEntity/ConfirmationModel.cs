using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TouristInsuredEntity
{
public class ConfirmationModel
    {
        public string Premium { get; set; }
        public string TransactionID { get; set; }
        public string policyname { get; set; }
        public double Totalpremium { get; set; }
        public string Applicationfee { get; set; }
        public string PolicyNumber { get; set; }
        public string coverageLength { get; set; }
        public string certificatelink { get; set; }
        public string OrderStatusMessage { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Gender { get; set; }
        public string DOB { get; set; }
        public int OrderRequestId { get; set; }
        public int OrderNo { get; set; }
        public string AuthCode { get; set; }
        public string TripID { get; set; }
        public string ErrorMessage { get; set; }
        public string code { get; set; }
        public string message { get; set; }
        public string display { get; set; }
        public string displayforsuccess { get; set; }
    }
}
