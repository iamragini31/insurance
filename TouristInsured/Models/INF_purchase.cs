using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TouristInsured.Models
{
    public class INF_purchase
    {
        public int planId { get; set; }
        public string coverageStartDate { get; set; }
        public string coverageEndDate { get; set; }
        public string coverageArea { get; set; }
        public string agreement { get; set; }
        private List<primary> userDetails = new List<primary>();
        public List<primary> primary
        {
            get { return userDetails; }
            set { userDetails = value; }
        }

        private List<payment> carddetails = new List<payment>();
        public List<payment> paymentInfo
        {
            get { return carddetails; }
            set { carddetails = value; }
        }

        private List<travel> traveldetails = new List<travel>();
        public List<travel> travelInfo
        {
            get { return traveldetails; }
            set { traveldetails = value; }
        }

        
        public string adbbeneficiary { get; set; }
        public string adbrelation { get; set; }
        
    }

    public class INF_Spouse_purchase
    {
        public int planId { get; set; }
        public string coverageStartDate { get; set; }
        public string coverageEndDate { get; set; }
        public string coverageArea { get; set; }
        public string agreement { get; set; }
        private List<primary> userDetails = new List<primary>();
        public List<primary> primary
        {
            get { return userDetails; }
            set { userDetails = value; }
        }

        private List<INFspouse> spousedetails = new List<INFspouse>();
        public List<INFspouse> spouse
        {
            get { return spousedetails; }
            set { spousedetails = value; }
        }
        private List<payment> carddetails = new List<payment>();
        public List<payment> paymentInfo
        {
            get { return carddetails; }
            set { carddetails = value; }
        }

        private List<travel> traveldetails = new List<travel>();
        public List<travel> travelInfo
        {
            get { return traveldetails; }
            set { traveldetails = value; }
        }


        public string adbbeneficiary { get; set; }
        public string adbrelation { get; set; }

    }

    public class INF_Child1_purchase
    {
        public int planId { get; set; }
        public string coverageStartDate { get; set; }
        public string coverageEndDate { get; set; }
        public string coverageArea { get; set; }
        public string agreement { get; set; }
        private List<primary> userDetails = new List<primary>();
        public List<primary> primary
        {
            get { return userDetails; }
            set { userDetails = value; }
        }

        private List<INFspouse> spousedetails = new List<INFspouse>();
        public List<INFspouse> spouse
        {
            get { return spousedetails; }
            set { spousedetails = value; }
        }
        private List<INFChild1> Child1details = new List<INFChild1>();
        public List<INFChild1> child1
        {
            get { return Child1details; }
            set { Child1details = value; }
        }
        private List<payment> carddetails = new List<payment>();
        public List<payment> paymentInfo
        {
            get { return carddetails; }
            set { carddetails = value; }
        }

        private List<travel> traveldetails = new List<travel>();
        public List<travel> travelInfo
        {
            get { return traveldetails; }
            set { traveldetails = value; }
        }


        public string adbbeneficiary { get; set; }
        public string adbrelation { get; set; }

    }


    public class INF_Child2_purchase
    {
        public int planId { get; set; }
        public string coverageStartDate { get; set; }
        public string coverageEndDate { get; set; }
        public string coverageArea { get; set; }
        public string agreement { get; set; }
        private List<primary> userDetails = new List<primary>();
        public List<primary> primary
        {
            get { return userDetails; }
            set { userDetails = value; }
        }

        private List<INFspouse> spousedetails = new List<INFspouse>();
        public List<INFspouse> spouse
        {
            get { return spousedetails; }
            set { spousedetails = value; }
        }
        private List<INFChild1> Child1details = new List<INFChild1>();
        public List<INFChild1> child1
        {
            get { return Child1details; }
            set { Child1details = value; }
        }


        private List<INFChild2> Child2details = new List<INFChild2>();
        public List<INFChild2> child2
        {
            get { return Child2details; }
            set { Child2details = value; }
        }
        private List<payment> carddetails = new List<payment>();



        public List<payment> paymentInfo
        {
            get { return carddetails; }
            set { carddetails = value; }
        }

        private List<travel> traveldetails = new List<travel>();
        public List<travel> travelInfo
        {
            get { return traveldetails; }
            set { traveldetails = value; }
        }


        public string adbbeneficiary { get; set; }
        public string adbrelation { get; set; }

    }
    public class INF_Child3_purchase
    {
        public int planId { get; set; }
        public string coverageStartDate { get; set; }
        public string coverageEndDate { get; set; }
        public string coverageArea { get; set; }
        public string agreement { get; set; }
        private List<primary> userDetails = new List<primary>();
        public List<primary> primary
        {
            get { return userDetails; }
            set { userDetails = value; }
        }

        private List<INFspouse> spousedetails = new List<INFspouse>();
        public List<INFspouse> spouse
        {
            get { return spousedetails; }
            set { spousedetails = value; }
        }
        private List<INFChild1> Child1details = new List<INFChild1>();
        public List<INFChild1> child1
        {
            get { return Child1details; }
            set { Child1details = value; }
        }


        private List<INFChild2> Child2details = new List<INFChild2>();
        public List<INFChild2> child2
        {
            get { return Child2details; }
            set { Child2details = value; }
        }
        private List<INFChild3> Child3details = new List<INFChild3>();
        public List<INFChild3> child3
        {
            get { return Child3details; }
            set { Child3details = value; }
        }
        private List<payment> carddetails = new List<payment>();



        public List<payment> paymentInfo
        {
            get { return carddetails; }
            set { carddetails = value; }
        }

        private List<travel> traveldetails = new List<travel>();
        public List<travel> travelInfo
        {
            get { return traveldetails; }
            set { traveldetails = value; }
        }


        public string adbbeneficiary { get; set; }
        public string adbrelation { get; set; }

    }

    public class INF_Child4_purchase
    {
        public int planId { get; set; }
        public string coverageStartDate { get; set; }
        public string coverageEndDate { get; set; }
        public string coverageArea { get; set; }
        public string agreement { get; set; }
        private List<primary> userDetails = new List<primary>();
        public List<primary> primary
        {
            get { return userDetails; }
            set { userDetails = value; }
        }

        private List<INFspouse> spousedetails = new List<INFspouse>();
        public List<INFspouse> spouse
        {
            get { return spousedetails; }
            set { spousedetails = value; }
        }
        private List<INFChild1> Child1details = new List<INFChild1>();
        public List<INFChild1> child1
        {
            get { return Child1details; }
            set { Child1details = value; }
        }


        private List<INFChild2> Child2details = new List<INFChild2>();
        public List<INFChild2> child2
        {
            get { return Child2details; }
            set { Child2details = value; }
        }
        private List<INFChild3> Child3details = new List<INFChild3>();
        public List<INFChild3> child3
        {
            get { return Child3details; }
            set { Child3details = value; }
        }

        private List<INFChild4> Child4details = new List<INFChild4>();
        public List<INFChild4> child4
        {
            get { return Child4details; }
            set { Child4details = value; }
        }
        private List<payment> carddetails = new List<payment>();



        public List<payment> paymentInfo
        {
            get { return carddetails; }
            set { carddetails = value; }
        }

        private List<travel> traveldetails = new List<travel>();
        public List<travel> travelInfo
        {
            get { return traveldetails; }
            set { traveldetails = value; }
        }


        public string adbbeneficiary { get; set; }
        public string adbrelation { get; set; }

    }

    public class INF_Child1_purchase_Primary
    {
        public int planId { get; set; }
        public string coverageStartDate { get; set; }
        public string coverageEndDate { get; set; }
        public string coverageArea { get; set; }
        public string agreement { get; set; }
        private List<primary> userDetails = new List<primary>();
        public List<primary> primary
        {
            get { return userDetails; }
            set { userDetails = value; }
        }

        
        private List<INFChild1> Child1details = new List<INFChild1>();
        public List<INFChild1> child1
        {
            get { return Child1details; }
            set { Child1details = value; }
        }
        private List<payment> carddetails = new List<payment>();
        public List<payment> paymentInfo
        {
            get { return carddetails; }
            set { carddetails = value; }
        }

        private List<travel> traveldetails = new List<travel>();
        public List<travel> travelInfo
        {
            get { return traveldetails; }
            set { traveldetails = value; }
        }


        public string adbbeneficiary { get; set; }
        public string adbrelation { get; set; }

    }
    public class INF_Child2_purchase_Primary
    {
        public int planId { get; set; }
        public string coverageStartDate { get; set; }
        public string coverageEndDate { get; set; }
        public string coverageArea { get; set; }
        public string agreement { get; set; }
        private List<primary> userDetails = new List<primary>();
        public List<primary> primary
        {
            get { return userDetails; }
            set { userDetails = value; }
        }

        
        private List<INFChild1> Child1details = new List<INFChild1>();
        public List<INFChild1> child1
        {
            get { return Child1details; }
            set { Child1details = value; }
        }


        private List<INFChild2> Child2details = new List<INFChild2>();
        public List<INFChild2> child2
        {
            get { return Child2details; }
            set { Child2details = value; }
        }
        private List<payment> carddetails = new List<payment>();



        public List<payment> paymentInfo
        {
            get { return carddetails; }
            set { carddetails = value; }
        }

        private List<travel> traveldetails = new List<travel>();
        public List<travel> travelInfo
        {
            get { return traveldetails; }
            set { traveldetails = value; }
        }


        public string adbbeneficiary { get; set; }
        public string adbrelation { get; set; }

    }
    public class INF_Child3_purchase_Primary
    {
        public int planId { get; set; }
        public string coverageStartDate { get; set; }
        public string coverageEndDate { get; set; }
        public string coverageArea { get; set; }
        public string agreement { get; set; }
        private List<primary> userDetails = new List<primary>();
        public List<primary> primary
        {
            get { return userDetails; }
            set { userDetails = value; }
        }

    
        private List<INFChild1> Child1details = new List<INFChild1>();
        public List<INFChild1> child1
        {
            get { return Child1details; }
            set { Child1details = value; }
        }


        private List<INFChild2> Child2details = new List<INFChild2>();
        public List<INFChild2> child2
        {
            get { return Child2details; }
            set { Child2details = value; }
        }
        private List<INFChild3> Child3details = new List<INFChild3>();
        public List<INFChild3> child3
        {
            get { return Child3details; }
            set { Child3details = value; }
        }
        private List<payment> carddetails = new List<payment>();



        public List<payment> paymentInfo
        {
            get { return carddetails; }
            set { carddetails = value; }
        }

        private List<travel> traveldetails = new List<travel>();
        public List<travel> travelInfo
        {
            get { return traveldetails; }
            set { traveldetails = value; }
        }


        public string adbbeneficiary { get; set; }
        public string adbrelation { get; set; }

    }
    public class INF_Child4_purchase_Primary
    {
        public int planId { get; set; }
        public string coverageStartDate { get; set; }
        public string coverageEndDate { get; set; }
        public string coverageArea { get; set; }
        public string agreement { get; set; }
        private List<primary> userDetails = new List<primary>();
        public List<primary> primary
        {
            get { return userDetails; }
            set { userDetails = value; }
        }

        
        private List<INFChild1> Child1details = new List<INFChild1>();
        public List<INFChild1> child1
        {
            get { return Child1details; }
            set { Child1details = value; }
        }


        private List<INFChild2> Child2details = new List<INFChild2>();
        public List<INFChild2> child2
        {
            get { return Child2details; }
            set { Child2details = value; }
        }
        private List<INFChild3> Child3details = new List<INFChild3>();
        public List<INFChild3> child3
        {
            get { return Child3details; }
            set { Child3details = value; }
        }

        private List<INFChild4> Child4details = new List<INFChild4>();
        public List<INFChild4> child4
        {
            get { return Child4details; }
            set { Child4details = value; }
        }
        private List<payment> carddetails = new List<payment>();



        public List<payment> paymentInfo
        {
            get { return carddetails; }
            set { carddetails = value; }
        }

        private List<travel> traveldetails = new List<travel>();
        public List<travel> travelInfo
        {
            get { return traveldetails; }
            set { traveldetails = value; }
        }


        public string adbbeneficiary { get; set; }
        public string adbrelation { get; set; }

    }
    public class INFspouse
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string dob { get; set; }
        public string passport { get; set; }
    }
    public class INFChild1
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string dob { get; set; }
        public string passport { get; set; }
    }
    public class INFChild2
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string dob { get; set; }
        public string passport { get; set; }
    }
    public class INFChild3
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string dob { get; set; }
        public string passport { get; set; }
    }
    public class INFChild4
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string dob { get; set; }
        public string passport { get; set; }
    }
}