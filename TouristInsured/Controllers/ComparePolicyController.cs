using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using TouristInsuredEntity;
using TouristInsuredManager;
using System.Data;
using System.Reflection;
using TouristInsured.Models;

namespace TouristInsured.Controllers
{
    public class ComparePolicyController : Controller
    {
        List<Property> Comparelist = new List<Property>();
        ComparePolicyManager comparepolicymanager = new ComparePolicyManager();
        List<ComparePolicyModel> lstcomparemodel = new List<ComparePolicyModel>();
        ComparePolicyModel comparepolicymodel = new ComparePolicyModel();

        public ActionResult ComparePolicy(string Data)
        {


            var js = new JavaScriptSerializer();
            ComparePolicyModel[] policy = js.Deserialize<ComparePolicyModel[]>(Data);
            var orderedCustomerList = policy.OrderBy(item => item.Appname);
            //comparepolicymodel = policy;
            //foreach (Models.CompareResponse item in policy)
            //{
            //    comparepolicymodel.Appname = item.Appname;
            //    comparepolicymodel.exactamount = item.exactamount;
            //    comparepolicymodel.getPlanDeductible = item.getPlanDeductible;
            //    comparepolicymodel.getplanmaximum = item.getplanmaximum;
            //    comparepolicymodel.ISOCODE = item.ISOCODE;
            //    comparepolicymodel.LeavingHome = item.LeavingHome;
            //    comparepolicymodel.planID = item.planID;
            //    comparepolicymodel.policyname = item.policyname;
            //    comparepolicymodel.TillDate = item.TillDate;
            //    comparepolicymodel.TouristISOCODE = item.TouristISOCODE;
            //    comparepolicymodel.TravelersDOB = item.TravelersDOB;
            //}
            //ViewBag.comparepolicymodel = comparepolicymodel;
            return View(orderedCustomerList);
        }

        

        public class Property
        {
            public string Plan_ID { get; set; }
            public string Plan_Name { get; set; }
            public string Company_Name { get; set; }
            public List<PropertyName> Property_name { get; set; }
        }
        public ActionResult Test(FormCollection form)
        {
           
           
            DataTable result = new DataTable();
            var Appname1 = form["ap1"];
            var Appname2 = form["ap2"];
            var Appname3 = form["ap3"];
            if ((Appname1 == "" || Appname1 == "undefined") && Appname2 != "" && Appname3 != "")
            {
                result = comparepolicymanager.Fetchdata(Appname2, Appname3);
               if(result !=null && result.Rows.Count > 0)
                {
                    var ID = ""; var value = ""; var Property_name = "";
                    DataTable dt = result;
                    for (int i=0; i < dt.Rows.Count; i++)
                    {
                        Property listobject = new Property();

                        listobject.Company_Name = dt.Rows[i]["Company_Name"].ToString();
                        listobject.Plan_Name = dt.Rows[i]["Plan_Name"].ToString();
                        listobject.Plan_ID = dt.Rows[i]["Plan_ID"].ToString();
                        ID = dt.Rows[i]["ID"].ToString();
                        value = dt.Rows[i]["Value"].ToString();
                        Property_name = dt.Rows[i]["Property_name"].ToString();
                        listobject.Property_name = new List<PropertyName>
                      {
                         new PropertyName {
                           ID=ID,
                           Property_name=Property_name,
                           Value=value

                        }
                      };
                        Comparelist.Add(listobject);
                    }

                }
            }
            else if (Appname1 != "" && Appname2 != "" && (Appname3 == "" || Appname3 == "undefined"))
            {
                result = comparepolicymanager.Fetchdata(Appname2, Appname1);


                if (result != null && result.Rows.Count > 0)
                {
                    var ID = ""; var value = ""; var Property_name = "";
                    DataTable dt = result;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Property listobject = new Property();

                        listobject.Company_Name = dt.Rows[i]["Company_Name"].ToString();
                        listobject.Plan_Name = dt.Rows[i]["Plan_Name"].ToString();
                        listobject.Plan_ID = dt.Rows[i]["Plan_ID"].ToString();
                        ID = dt.Rows[i]["ID"].ToString();
                        value = dt.Rows[i]["Value"].ToString();
                        Property_name = dt.Rows[i]["Property_name"].ToString();
                        listobject.Property_name = new List<PropertyName>
                      {
                         new PropertyName {
                           ID=ID,
                           Property_name=Property_name,
                           Value=value

                        }
                      };
                        Comparelist.Add(listobject);
                    }

                }


            }
            else if (Appname1 != "" && (Appname2 == "" || Appname2 == "undefined") && Appname3 != "")
            {
                result = comparepolicymanager.Fetchdata(Appname1, Appname3);
                if (result != null && result.Rows.Count > 0)
                {
                    var ID = ""; var value = ""; var Property_name = "";
                    DataTable dt = result;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Property listobject = new Property();

                        listobject.Company_Name = dt.Rows[i]["Company_Name"].ToString();
                        listobject.Plan_Name = dt.Rows[i]["Plan_Name"].ToString();
                        listobject.Plan_ID = dt.Rows[i]["Plan_ID"].ToString();
                        ID = dt.Rows[i]["ID"].ToString();
                        value = dt.Rows[i]["Value"].ToString();
                        Property_name = dt.Rows[i]["Property_name"].ToString();
                        listobject.Property_name = new List<PropertyName>
                      {
                         new PropertyName {
                           ID=ID,
                           Property_name=Property_name,
                           Value=value

                        }
                      };
                        Comparelist.Add(listobject);
                    }

                }
                //Comparelist = ConversionMethod.ConvertDataTable<Models.ComparePolicyProperty>(result);


            }
            else
            {
                result = comparepolicymanager.FetchAlldata(Appname1, Appname3,Appname2);
                if (result != null && result.Rows.Count > 0)
                {
                    var ID = ""; var value = ""; var Property_name = "";
                    DataTable dt = result;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Property listobject = new Property();

                        listobject.Company_Name = dt.Rows[i]["Company_Name"].ToString();
                        listobject.Plan_Name = dt.Rows[i]["Plan_Name"].ToString();
                        listobject.Plan_ID = dt.Rows[i]["Plan_ID"].ToString();
                        ID = dt.Rows[i]["ID"].ToString();
                        value = dt.Rows[i]["Value"].ToString();
                        Property_name = dt.Rows[i]["Property_name"].ToString();
                        listobject.Property_name = new List<PropertyName>
                      {
                         new PropertyName {
                           ID=ID,
                           Property_name=Property_name,
                           Value=value

                        }
                      };
                        Comparelist.Add(listobject);
                    }

                }
            }
            
          
            string jsonData = JsonConvert.SerializeObject(Comparelist, Formatting.Indented);
            return Json(Comparelist, JsonRequestBehavior.AllowGet);
            //return Json(result, JsonRequestBehavior.AllowGet);
        }


    }


    //public List<Models.ComparePolicyProperty> GetStudentList()
    //{
    //    DataTable ResultDT = new DataTable();
    //   // Call BusinessLogic to fill DataTable, Here your ResultDT will get the result in which you will be having single or multiple rows with columns "StudentId,RoleNumber and Name"  
    //    List<Models.ComparePolicyProperty> Studentlist = new List<Models.ComparePolicyProperty>();
    //    Studentlist = CommonMethod.ConvertToList<Models.ComparePolicyProperty>(ResultDT);
    //    return Studentlist;
    //}
    public static class CommonMethod
    {
        public static List<T> ConvertToList<T>(DataTable dt)
        {
            var columnNames = dt.Columns.Cast<DataColumn>().Select(c => c.ColumnName.ToLower()).ToList();
            var properties = typeof(T).GetProperties();
            return dt.AsEnumerable().Select(row => {
                var objT = Activator.CreateInstance<T>();
                foreach (var pro in properties)
                {
                    if (columnNames.Contains(pro.Name.ToLower()))
                    {
                        try
                        {
                            pro.SetValue(objT, row[pro.Name]);
                        }
                        catch (Exception ex) { }
                    }
                }
                return objT;
            }).ToList();
        }
    }

    public static class ConversionMethod{
          public static List<T> ConvertDataTable<T>(DataTable dt)
    {
        List<T> data = new List<T>();
        foreach (DataRow row in dt.Rows)
        {
            T item = GetItem<T>(row);
            data.Add(item);
        }
        return data;
    }

        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                        pro.SetValue(obj, dr[column.ColumnName], null);
                    else
                        continue;
                }
            }
            return obj;
        }

    }


}