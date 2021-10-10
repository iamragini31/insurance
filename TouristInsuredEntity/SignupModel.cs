using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TouristInsuredEntity
{
   public class SignupModel
    {

        public string EmailID { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Gender { get; set; }
        public string DOB { get; set; }
        public long Contact_No { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }
    }
}
