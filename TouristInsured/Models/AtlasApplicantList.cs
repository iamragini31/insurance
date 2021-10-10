using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TouristInsured.Models
{
    [JsonObject("ApplicantList")]
    public class AtlasApplicantList
    {
        public string FirstName { get; set; }
        public string MiddleInitial { get; set; }
        public string LastName { get; set; }
        public string Dob { get; set; }
        public string Gender { get; set; }
        public string Citizenship { get; set; }
        public string Passport { get; set; }
    }
}