using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TouristInsured.Models
{
    [JsonObject("families")]
   
    public class Imgfamilies
    {
        
     public List<IMGInsureds> insureds { get; set; }
    }


    [JsonObject("families")]

    public class Imgfamilies_withRiders
    {

        public List<IMGInsureds> insureds { get; set; }
    }

}