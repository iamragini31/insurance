using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TouristInsured.Models
{
    [JsonObject("insureds")]
    public class IMGInsureds
    {

       
      public string firstName { get; set; }
        public string lastName { get; set; }
        public DateTime    dateOfBirth { get; set; }
         public string   citizenship { get; set; }
        public string gender { get; set; }
        public string residence { get; set; }
        public string travelerType { get; set; }
        public string governmentIssuedIdNumber { get; set; }
        public string visaType { get; set; }

       public List<IMGProductOption> productOptions { get; set; }
         public List<string> riders { get; set; }
    private List<domesticInsurance> Domesticinsure = new List<domesticInsurance>();
    public List<domesticInsurance> domesticInsurance
    {
        get { return Domesticinsure; }
        set { Domesticinsure = value; }
    }
         
     public DateTime startDate { get; set; }
public DateTime endDate { get; set; }
public string email{ get; set; }
        }

    public class domesticInsurance
{
    public string carrier { get; set; }
    public string policyNumber { get; set; }
    public bool acknowledgment { get; set; }
}
    
}