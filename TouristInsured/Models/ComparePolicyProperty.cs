using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TouristInsured.Models
{
    public class ComparePolicyProperty
    {


        public string Plan_ID{ get; set; }
        public string Plan_Name { get; set; }
        public string Company_Name { get; set; }
        public List<PropertyName> Property_name { get; set; }
        //public class PropertyName
        //{
        //    public string Plan_ID { get; set; }
        //    public string Property_name { get; set; }
        //    public string Value { get; set; }
        //}

        //public class ComparePolicyProp
        //{
        //    public string ID { get; set; }
        //    public string Plan_Name { get; set; }
        //    public string Company_Name { get; set; }
        //    //public List<PropertyName> Property_name { get; set; }
        //}
    }
}