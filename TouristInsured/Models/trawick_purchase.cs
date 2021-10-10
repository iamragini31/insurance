using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Reflection;
using Newtonsoft.Json;

namespace TouristInsured.Models
{
    public class trawick_purchase
    {
        public int ProductID { get; set; }
    }

    public class RootObjectTrawick
    {
        public int travel_rates_id { get; set; }
        public int Products_id { get; set; }
        public string description { get; set; }
        public int policy_max { get; set; }
        public int min_age { get; set; }
        public int max_age { get; set; }
        public double Deductible_0 { get; set; }
        public double Deductible_50 { get; set; }
        public double Deductible_100 { get; set; }
        public double Deductible_250 { get; set; }
        public double Deductible_500 { get; set; }
        public double Deductible_1000 { get; set; }
        public double Deductible_2500 { get; set; }
        public double Deductible_5000 { get; set; }
    }

    public class Primary1
    {
        public double individualFee { get; set; }
        public long age { get; set; }

        public static DataTable CreateDataTable<T>(IEnumerable<T> list)
        {
            Type type = typeof(T);
            var properties = type.GetProperties();

            DataTable dataTable = new DataTable();
            foreach (PropertyInfo info in properties)
            {
                dataTable.Columns.Add(new DataColumn(info.Name, Nullable.GetUnderlyingType(info.PropertyType) ?? info.PropertyType));
            }

            foreach (T entity in list)
            {
                object[] values = new object[properties.Length];
                for (int i = 0; i < properties.Length; i++)
                {
                    values[i] = properties[i].GetValue(entity);
                }

                dataTable.Rows.Add(values);
            }

            return dataTable;
        }
    }

    public class TrawickStudentResponnse
    {

        public int Products_id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int Market_Policy_id { get; set; }
        public int minTime { get; set; }
        public int minTimeUnits { get; set; }
        public bool requestSchoolName { get; set; }
        public bool active { get; set; }
        public int maxAge { get; set; }
        public string AgentLinkCategory { get; set; }
        public bool OutBound { get; set; }
        public string ProductQuoteType { get; set; }
        public bool StateCerts { get; set; }
        public double MinPrice { get; set; }
        public string ProductContainerType { get; set; }
        public string ProductOptionsType { get; set; }
        public int MaxStartTime { get; set; }
        public List<v> v { get; set; }
    }

    [JsonObject("v")]
    public class  v { 
               
                public double student_rate { get; set; }
        public double rate_type_id { get; set; }
        public double spouse_rate { get; set; }
        public double child_rate { get; set; }
        public int min_age { get; set; }
        public int max_age { get; set; }
        public int Products_id { get; set; }
        public string description { get; set; }
        public double policy_max { get; set; }
        public int age_band_id { get; set; }
    }
}