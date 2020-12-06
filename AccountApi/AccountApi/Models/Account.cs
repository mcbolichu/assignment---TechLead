using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace AccountApi.Models
{

    public class Account
    {
    }
    [DataContract]
    public class MAIN_HOLDER
    {
        [DataMember(Name = "Customer")]
        public string Customer { get; set; }
        [DataMember(Name = "Short_Name")]
        public string Short_Name { get; set; }
    }
    [DataContract]
    public class CATEGORY_ITEM
    {
        [DataMember(Name = "Category")]
        public string Category { get; set; }
        [DataMember(Name = "Category_Desc")]
        public string Category_Desc { get; set; }

    }
    [DataContract]
    public class CURRENCY_ITEM
    {
        [DataMember(Name = "Currency")]
        public string Currency { get; set; }
        [DataMember(Name = "Currency_Desc")]
        public string Currency_Desc { get; set; }

    }
    [DataContract]
    public class BALANCE
    {
        [DataMember(Name = "Available_Balance")]
        public float Available_Balance { get; set; }
        [DataMember(Name = "Previous_Balance")]
        public float Previous_Balance { get; set; }
    }
    [DataContract]
    public class MAILING_ADDRESS_ITEM
    {
        [DataMember(Name = "Short_Name")]
        public string Short_Name { get; set; }
        [DataMember(Name = "Name_1")]
        public string Name_1 { get; set; }
        [DataMember(Name = "Name_2")]
        public string Name_2 { get; set; }
        [DataMember(Name = "Street_Addr")]
        public string Street_Addr { get; set; }
        [DataMember(Name = "Address_Line2")]
        public string Address_Line2 { get; set; }
        [DataMember(Name = "Address_Line3")]
        public string Address_Line3 { get; set; }
        [DataMember(Name = "Town_Country")]
        public string Town_Country { get; set; }
        [DataMember(Name = "Post_Code")]
        public string Post_Code { get; set; }
        [DataMember(Name = "Country")]
        public string Country { get; set; }
        [DataMember(Name = "Country_Code")]
        public string Country_Code { get; set; }

    }
    [DataContract]
    public class ACCOUNT_INFO
    {
        [DataMember(Name = "Account_No")]
        public string Account_No { get; set; }
        [DataMember(Name = "Short_Title")]
        public string Short_Title { get; set; }
        [DataMember(Name = "Account_Title")]
        public string Account_Title { get; set; }
        [DataMember(Name = "Opening_Date")]
        public string Opening_Date { get; set; }
        [DataMember(Name = "Company_Code")]
        public string Company_Code { get; set; }
        [DataMember(Name = "Company_Name")]
        public string Company_Name { get; set; }
        [DataMember(Name = "Account_Status")]
        public string Account_Status { get; set; }
        [DataMember(Name = "Main_Holder")]
        public MAIN_HOLDER Main_Holder { get; set; }
        [DataMember(Name = "Category_Item")]
        public CATEGORY_ITEM Category_Item { get; set; }
        [DataMember(Name = "Currency_Item")]
        public CURRENCY_ITEM Currency_Item { get; set; }
        [DataMember(Name = "Balance")]
        public BALANCE Balance { get; set; }
        [DataMember(Name = "Mailing_Address")]
        public MAILING_ADDRESS_ITEM Mailing_Address { get; set; }
    }
    [DataContract]
    public class GET_ACCOUNT_INFO_RESP
    {
        [DataMember(Name = "Return_Code")]
        public string Return_Code { get; set; }
        [DataMember(Name = "Error_Msg")]
        public string Error_Msg { get; set; }
        [DataMember(Name = "Account_Info")]
        public ACCOUNT_INFO Account_Info { get; set; }
    }

    //public partial class CREATE_ACCOUNT_RESP
    //{
    //    public string Return_Code { get; set; }
    //    public string Error_Message { get; set; }
    //    public string Account_Number { get; set; }
    //}
    [DataContract]
    public class STATUS
    {
        [DataMember(Name = "Active")]
        public const string Active = "ACTIVE";
        [DataMember(Name = "Inactive")]
        public const string Inactive = "INACTIVE";
    }

    public class TRANSACTION_TYPE
    {
        [DataMember(Name = "Active")]
        public const string DEBIT = "DEBIT";
        [DataMember(Name = "Inactive")]
        public const string CREDIT = "CREDIT";
    }


}