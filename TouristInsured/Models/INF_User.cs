using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TouristInsured.Models
{
    public class INF_User
    {
        public string quoteStartDate { get; set; }
        public string quoteEndDate { get; set; }
        public string coverageArea { get; set; }

        private List<INF_User_Primary> userDetails = new List<INF_User_Primary>();
        public List<INF_User_Primary> primary
        {
            get { return userDetails; }
            set { userDetails = value; }
        }
//        public string spouseDob { get; set; }
//public string ageChild1 { get; set; }
//        public string ageChild2 { get; set; }

    }
    public class INF_User_Primary
    {
        //public string email { get; set; }
        public string dob { get; set; }
        //spublic string phone { get; set; }

    }
    public class INF_Spouse
    {
        public string quoteStartDate { get; set; }
        public string quoteEndDate { get; set; }
        public string coverageArea { get; set; }

        private List<INF_User_Primary> userDetails = new List<INF_User_Primary>();
        public List<INF_User_Primary> primary
        {
            get { return userDetails; }
            set { userDetails = value; }
        }
        public string spouseDob { get; set; }
        //public string ageChild1 { get; set; }
        //public string ageChild2 { get; set; }
    }

    
    public class INF_Child1
    {
        public string quoteStartDate { get; set; }
        public string quoteEndDate { get; set; }
        public string coverageArea { get; set; }

        private List<INF_User_Primary> userDetails = new List<INF_User_Primary>();
        public List<INF_User_Primary> primary
        {
            get { return userDetails; }
            set { userDetails = value; }
        }
        public string spouseDob { get; set; }
        public string ageChild1 { get; set; }
        //public string ageChild2 { get; set; }
    }
    public class INF_Child2
    {
        public string quoteStartDate { get; set; }
        public string quoteEndDate { get; set; }
        public string coverageArea { get; set; }

        private List<INF_User_Primary> userDetails = new List<INF_User_Primary>();
        public List<INF_User_Primary> primary
        {
            get { return userDetails; }
            set { userDetails = value; }
        }
        public string spouseDob { get; set; }
        public string ageChild1 { get; set; }
        public string ageChild2 { get; set; }
    }
    public class INF_Child3
    {
        public string quoteStartDate { get; set; }
        public string quoteEndDate { get; set; }
        public string coverageArea { get; set; }

        private List<INF_User_Primary> userDetails = new List<INF_User_Primary>();
        public List<INF_User_Primary> primary
        {
            get { return userDetails; }
            set { userDetails = value; }
        }
        public string spouseDob { get; set; }
        public string ageChild1 { get; set; }
        public string ageChild2 { get; set; }
        public string ageChild3 { get; set; }
    }
    public class INF_Child4
    {
        public string quoteStartDate { get; set; }
        public string quoteEndDate { get; set; }
        public string coverageArea { get; set; }

        private List<INF_User_Primary> userDetails = new List<INF_User_Primary>();
        public List<INF_User_Primary> primary
        {
            get { return userDetails; }
            set { userDetails = value; }
        }
        public string spouseDob { get; set; }
        public string ageChild1 { get; set; }
        public string ageChild2 { get; set; }
        public string ageChild3 { get; set; }
        public string ageChild4 { get; set; }
    }

    //pc1
    public class INF_Child1_Primary
    {
        public string quoteStartDate { get; set; }
        public string quoteEndDate { get; set; }
        public string coverageArea { get; set; }

        private List<INF_User_Primary> userDetails = new List<INF_User_Primary>();
        public List<INF_User_Primary> primary
        {
            get { return userDetails; }
            set { userDetails = value; }
        }
       
        public string ageChild1 { get; set; }
        //public string ageChild2 { get; set; }
    }
    public class INF_Child2_Primary
    {
        public string quoteStartDate { get; set; }
        public string quoteEndDate { get; set; }
        public string coverageArea { get; set; }

        private List<INF_User_Primary> userDetails = new List<INF_User_Primary>();
        public List<INF_User_Primary> primary
        {
            get { return userDetails; }
            set { userDetails = value; }
        }
    
        public string ageChild2 { get; set; }
    }
    public class INF_Child3_Primary
    {
        public string quoteStartDate { get; set; }
        public string quoteEndDate { get; set; }
        public string coverageArea { get; set; }

        private List<INF_User_Primary> userDetails = new List<INF_User_Primary>();
        public List<INF_User_Primary> primary
        {
            get { return userDetails; }
            set { userDetails = value; }
        }
        
        public string ageChild3 { get; set; }
    }
    public class INF_Child4_Primary
    {
        public string quoteStartDate { get; set; }
        public string quoteEndDate { get; set; }
        public string coverageArea { get; set; }

        private List<INF_User_Primary> userDetails = new List<INF_User_Primary>();
        public List<INF_User_Primary> primary
        {
            get { return userDetails; }
            set { userDetails = value; }
        }
        
        public string ageChild4 { get; set; }
    }
    //pc1c2
    public class INF_Child2_Child1
    {
        public string quoteStartDate { get; set; }
        public string quoteEndDate { get; set; }
        public string coverageArea { get; set; }

        private List<INF_User_Primary> userDetails = new List<INF_User_Primary>();
        public List<INF_User_Primary> primary
        {
            get { return userDetails; }
            set { userDetails = value; }
        }
        
        public string ageChild1 { get; set; }
        public string ageChild2 { get; set; }
    }
    public class INF_Child3_Child1
    {
        public string quoteStartDate { get; set; }
        public string quoteEndDate { get; set; }
        public string coverageArea { get; set; }

        private List<INF_User_Primary> userDetails = new List<INF_User_Primary>();
        public List<INF_User_Primary> primary
        {
            get { return userDetails; }
            set { userDetails = value; }
        }
        
        public string ageChild1 { get; set; }
        
        public string ageChild3 { get; set; }
    }
    public class INF_Child4_Child1
    {
        public string quoteStartDate { get; set; }
        public string quoteEndDate { get; set; }
        public string coverageArea { get; set; }

        private List<INF_User_Primary> userDetails = new List<INF_User_Primary>();
        public List<INF_User_Primary> primary
        {
            get { return userDetails; }
            set { userDetails = value; }
        }
        
        public string ageChild1 { get; set; }
       
        public string ageChild4 { get; set; }
    }
    public class INF_Child3_Child2
    {
        public string quoteStartDate { get; set; }
        public string quoteEndDate { get; set; }
        public string coverageArea { get; set; }

        private List<INF_User_Primary> userDetails = new List<INF_User_Primary>();
        public List<INF_User_Primary> primary
        {
            get { return userDetails; }
            set { userDetails = value; }
        }
       
        public string ageChild2 { get; set; }
        public string ageChild3 { get; set; }
    }
    public class INF_Child4_Child2
    {
        public string quoteStartDate { get; set; }
        public string quoteEndDate { get; set; }
        public string coverageArea { get; set; }

        private List<INF_User_Primary> userDetails = new List<INF_User_Primary>();
        public List<INF_User_Primary> primary
        {
            get { return userDetails; }
            set { userDetails = value; }
        }
        
        public string ageChild2 { get; set; }
        
        public string ageChild4 { get; set; }
    }
    public class INF_Child4_Child3
    {
        public string quoteStartDate { get; set; }
        public string quoteEndDate { get; set; }
        public string coverageArea { get; set; }

        private List<INF_User_Primary> userDetails = new List<INF_User_Primary>();
        public List<INF_User_Primary> primary
        {
            get { return userDetails; }
            set { userDetails = value; }
        }
        
        public string ageChild3 { get; set; }
        public string ageChild4 { get; set; }
    }
    public class INF_Child4_Child1_Child3
    {
        public string quoteStartDate { get; set; }
        public string quoteEndDate { get; set; }
        public string coverageArea { get; set; }

        private List<INF_User_Primary> userDetails = new List<INF_User_Primary>();
        public List<INF_User_Primary> primary
        {
            get { return userDetails; }
            set { userDetails = value; }
        }
      
        public string ageChild1 { get; set; }
        
        public string ageChild3 { get; set; }
        public string ageChild4 { get; set; }
    }
    public class INF_Child4_Child2_Child3
    {
        public string quoteStartDate { get; set; }
        public string quoteEndDate { get; set; }
        public string coverageArea { get; set; }

        private List<INF_User_Primary> userDetails = new List<INF_User_Primary>();
        public List<INF_User_Primary> primary
        {
            get { return userDetails; }
            set { userDetails = value; }
        }
    
        public string ageChild2 { get; set; }
        public string ageChild3 { get; set; }
        public string ageChild4 { get; set; }
    }
    public class INF_Child2_Spouse
    {
        public string quoteStartDate { get; set; }
        public string quoteEndDate { get; set; }
        public string coverageArea { get; set; }

        private List<INF_User_Primary> userDetails = new List<INF_User_Primary>();
        public List<INF_User_Primary> primary
        {
            get { return userDetails; }
            set { userDetails = value; }
        }
        public string spouseDob { get; set; }
        
        public string ageChild2 { get; set; }
    }
    public class INF_Child3_Spouse
    {
        public string quoteStartDate { get; set; }
        public string quoteEndDate { get; set; }
        public string coverageArea { get; set; }

        private List<INF_User_Primary> userDetails = new List<INF_User_Primary>();
        public List<INF_User_Primary> primary
        {
            get { return userDetails; }
            set { userDetails = value; }
        }
        public string spouseDob { get; set; }
       
        public string ageChild3 { get; set; }
    }
    public class INF_Child4_Spouse
    {
        public string quoteStartDate { get; set; }
        public string quoteEndDate { get; set; }
        public string coverageArea { get; set; }

        private List<INF_User_Primary> userDetails = new List<INF_User_Primary>();
        public List<INF_User_Primary> primary
        {
            get { return userDetails; }
            set { userDetails = value; }
        }
        public string spouseDob { get; set; }
        
        public string ageChild4 { get; set; }
    }
    public class INF_Child3_Spouse_Child2
    {
        public string quoteStartDate { get; set; }
        public string quoteEndDate { get; set; }
        public string coverageArea { get; set; }

        private List<INF_User_Primary> userDetails = new List<INF_User_Primary>();
        public List<INF_User_Primary> primary
        {
            get { return userDetails; }
            set { userDetails = value; }
        }
        public string spouseDob { get; set; }
        
        public string ageChild2 { get; set; }
        public string ageChild3 { get; set; }
    }
    public class INF_Child4_Spouse_Child2
    {
        public string quoteStartDate { get; set; }
        public string quoteEndDate { get; set; }
        public string coverageArea { get; set; }

        private List<INF_User_Primary> userDetails = new List<INF_User_Primary>();
        public List<INF_User_Primary> primary
        {
            get { return userDetails; }
            set { userDetails = value; }
        }
        public string spouseDob { get; set; }
       
        public string ageChild2 { get; set; }
       
        public string ageChild4 { get; set; }
    }
    public class INF_Child4_Spouse_Child3
    {
        public string quoteStartDate { get; set; }
        public string quoteEndDate { get; set; }
        public string coverageArea { get; set; }

        private List<INF_User_Primary> userDetails = new List<INF_User_Primary>();
        public List<INF_User_Primary> primary
        {
            get { return userDetails; }
            set { userDetails = value; }
        }
        public string spouseDob { get; set; }
        
        public string ageChild3 { get; set; }
        public string ageChild4 { get; set; }
    }
    public class INF_Child4_Spouse_Child3_Child2
    {
        public string quoteStartDate { get; set; }
        public string quoteEndDate { get; set; }
        public string coverageArea { get; set; }

        private List<INF_User_Primary> userDetails = new List<INF_User_Primary>();
        public List<INF_User_Primary> primary
        {
            get { return userDetails; }
            set { userDetails = value; }
        }
        public string spouseDob { get; set; }
        
        public string ageChild2 { get; set; }
        public string ageChild3 { get; set; }
        public string ageChild4 { get; set; }
    }

    //pc1c2c3
    public class INF_Child3_Child1_Child2
    {
        public string quoteStartDate { get; set; }
        public string quoteEndDate { get; set; }
        public string coverageArea { get; set; }

        private List<INF_User_Primary> userDetails = new List<INF_User_Primary>();
        public List<INF_User_Primary> primary
        {
            get { return userDetails; }
            set { userDetails = value; }
        }
       
        public string ageChild1 { get; set; }
        public string ageChild2 { get; set; }
        public string ageChild3 { get; set; }
    }
    //pc1c2c3c4
    public class INF_Child4_Child3_Child1_Child2
    {
        public string quoteStartDate { get; set; }
        public string quoteEndDate { get; set; }
        public string coverageArea { get; set; }

        private List<INF_User_Primary> userDetails = new List<INF_User_Primary>();
        public List<INF_User_Primary> primary
        {
            get { return userDetails; }
            set { userDetails = value; }
        }
        
        public string ageChild1 { get; set; }
        public string ageChild2 { get; set; }
        public string ageChild3 { get; set; }
        public string ageChild4 { get; set; }
    }
}