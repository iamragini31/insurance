using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TouristInsured.Models
{
    public class auth
    {
        public string username { get; set; }
        public string password { get; set; }
        public string code { get; set; }
        public string message { get; set; }
        public string token { get; set; }
    }
}