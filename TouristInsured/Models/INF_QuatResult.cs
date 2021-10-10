using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Reflection;

namespace TouristInsured.Models
{
    public class INF_QuatResult
    {
        private List<INF_Quote> insuredsDetails = new List<INF_Quote>();
    }
    public class INF_Quote
    {

        public long planId { get; set; }
        public string policyName { get; set; }
        public string policyDescription { get; set; }
        public string policyUnderWriter { get; set; }
        public string policyType { get; set; }
        public string policyAdditionalFeatures { get; set; }
        public long planDeductible { get; set; }
        public long planMaximum { get; set; }
        public long coverageLength { get; set; }
        public long coverageClass { get; set; }
        public long preExDeductible { get; set; }
        public long preExCoverageAmount { get; set; }
        public string policyNumber { get; set; }
        public string prexded { get; set; }
        public string prextype { get; set; }
        public double totalPremium { get; set; }

        private List<insureds> insureds = new List<insureds>();
        //private List<insureds> userDetails = new List<insureds>();
        //public List<insureds> insureds
        //{
        //    get { return userDetails; }
        //    set { userDetails = value; }
        //}
        //public Primary insureds = new Primary();
    }


    public class insureds
    {
        public Primary primary = new Primary();
        //private List<Primary> userDetails = new List<Primary>();
        //public List<Primary> primary
        //{
        //    get { return userDetails; }
        //    set { userDetails = value; }
        //}

        public spouse spouse = new spouse();
        //private List<spouse> spouseDetails = new List<spouse>();
        //public List<spouse> spouse
        //{
        //    get { return spouseDetails; }
        //    set { spouseDetails = value; }
        //}
        public child1 child1 = new child1();
        //private List<child1> child1Details = new List<child1>();
        //public List<child1> child1
        //{
        //    get { return child1Details; }
        //    set { child1Details = value; }
        //}
        public child2 child2 = new child2();
        //private List<child2> child2Details = new List<child2>();
        //public List<child2> child2
        //{
        //    get { return child2Details; }
        //    set { child2Details = value; }
        //}
    }


    public class Primary
    {
        public double individualFee { get; set; }
        public long age { get; set; }
    }

    public class spouse
    {
        public double individualFee { get; set; }
        public long age { get; set; }
    }

    public class child1
    {
        public double individualFee { get; set; }
        public string age { get; set; }


    }

    public class child2
    {
        public double individualFee { get; set; }
        public string age { get; set; }

    }
    //public class primaryResult1
    //{
    //    public List<primaryResult> primary = new List<primaryResult>();
    //}

    //    public class spouse
    //{

    //}
    //    public class Primary
    //{
    //        public double individualFee { get; set; }
    //        public long age { get; set; }

    //        public static DataTable CreateDataTable<T>(IEnumerable<T> list)
    //        {
    //            Type type = typeof(T);
    //            var properties = type.GetProperties();

    //            DataTable dataTable = new DataTable();
    //            foreach (PropertyInfo info in properties)
    //            {
    //                dataTable.Columns.Add(new DataColumn(info.Name, Nullable.GetUnderlyingType(info.PropertyType) ?? info.PropertyType));
    //            }

    //            foreach (T entity in list)
    //            {
    //                object[] values = new object[properties.Length];
    //                for (int i = 0; i < properties.Length; i++)
    //                {
    //                    values[i] = properties[i].GetValue(entity);
    //                }

    //                dataTable.Rows.Add(values);
    //            }

    //            return dataTable;
    //        }
    //    }

}