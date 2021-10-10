using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TouristInsured.Models
{
    public class DiplomatPurchase
    {



        public DateTime PurchaseDate { get; set; }
        public bool ProcessCreditCard { get; set; }
        private List<SubAgent> agent = new List<SubAgent>();
        public List<SubAgent> SubAgent
        {
            get { return agent; }
            set { agent = value; }
        }

        private List<BillingInformation> billinginfo = new List<BillingInformation>();
        public List<BillingInformation> BillingInformation
        {
            get { return billinginfo; }
            set { billinginfo = value; }
        }
        private List<PrimaryAdultTraveler> primarytraveler = new List<PrimaryAdultTraveler>();
        public List<PrimaryAdultTraveler> PrimaryAdultTraveler
        {
            get { return primarytraveler; }
            set { primarytraveler = value; }
        }

        
        public List<AdditionalTravelers> AdditionalTravelers { get; set; }

         public double PolicyPremium { get; set; }
 public DateTime PolicyStartDate { get; set; }
        public DateTime PolicyEndDate { get; set; }
        public int LTNumberOfMonths { get; set; }
        public string Plan { get; set; }
        public string TravelerOneAgeRange { get; set; }
        public string TravelerTwoAgeRange { get; set; }
        public string Deductible { get; set; }
        public string ADDBenefit { get; set; }
        public int NumberOfMinorDependents { get; set; }
 public List<string> Riders { get; set; }
 public List<string> CountryIso2Codes { get; set; }
 public string WarRiskCoverage { get; set; }

    }

  public class SubAgent {
    public string Name { get; set; }
    public string Email { get; set; }
  }
  public class BillingInformation {
        public string FirstName { get; set; }
        public string MiddleInitial { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string CompanyName { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string CountryISO2Code { get; set; }
        public string StateISOCode { get; set; }
        public string PostalCode { get; set; }
        public string PhoneNumber { get; set; }
        public string CreditCardNumber { get; set; }
        public string CreditCardType { get; set; }
        public string CreditCardExpirationMonth { get; set; }
        public string CreditCardExpirationYear { get; set; }
        public string CVV2 { get; set; }
    }
    public class PrimaryAdultTraveler {
        public string FirstName { get; set; }
        public string MiddleInitial { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string AddressLineOne { get; set; }
        public string AddressLineTwo { get; set; }
        public string City { get; set; }
        public string CountryISO2Code { get; set; }
        public string StateISOCode { get; set; }
        public string PostalCode { get; set; }
        public string PrimaryEmailAddress { get; set; }
        public string SecondaryEmailAddress { get; set; }
        public string HomePhone { get; set; }
        public string WorkPhone { get; set; }
        public string CellPhone { get; set; }
        public string PassportNumber { get; set; }
        public string PassportIssuingCountryISO2Code { get; set; }
        public List<Beneficiaries> Beneficiaries { get; set; }
    }


    [JsonObject("Beneficiaries")]
    public class Beneficiaries 
      {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DateOfBirth { get; set; }
        public int BenefitPercentage { get; set; }
        public string Relationship { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Phone { get; set; }
        public string CountryIso2Code { get; set; }

    }

    [JsonObject("AdditionalTravelers")]
    public class AdditionalTravelers
    {
        public string FirstName { get; set; }
      public string LastName { get; set; }
        public string Relationship { get; set; }
        public DateTime DateOfBirth { get; set; }
        public List<Beneficiaries> Beneficiaries { get;set;}
    }

 

}