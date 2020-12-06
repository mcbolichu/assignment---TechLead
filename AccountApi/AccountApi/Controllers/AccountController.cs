using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AccountApi.Models;
using System.Collections;

namespace AccountApi.Controllers
{
    public class AccountController : ApiController
    {
        public static Hashtable htAccount;
        // GET: api/Account
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Account/5
        public GET_ACCOUNT_INFO_RESP Get(string id)
        {
            return getAccountInfo(id);
        }

        // POST: api/Account
        //public void Post([FromBody]string value)
        //{
        //}
        public GET_ACCOUNT_INFO_RESP Post(ACCOUNT_INFO accountInfo)
        {
            return createAccount(accountInfo);
        }

        // PUT: api/Account/5
        public GET_ACCOUNT_INFO_RESP Put(string tranType, string accountNumber, float amount)
        {
            return performTransaction(tranType, accountNumber, amount);
        }

        // DELETE: api/Account/5
        public GET_ACCOUNT_INFO_RESP Delete(string id)
        {
            return deactivateAccount(id);
        }
        public static string generateAccountNumber()
        {
            String Prefix = "000";
            Random rdm = new Random();
            return string.Concat(Prefix, rdm.Next(0, 999999999).ToString("D9"));
        }
        public static GET_ACCOUNT_INFO_RESP createAccount(ACCOUNT_INFO accountInfo)
        {
            //Initialize return code to success value
            GET_ACCOUNT_INFO_RESP resp = new GET_ACCOUNT_INFO_RESP();
            resp.Return_Code = "0";

            try
            {
                ACCOUNT_INFO acctInfo = new ACCOUNT_INFO();
                acctInfo.Account_No = generateAccountNumber();
                acctInfo.Short_Title = accountInfo.Short_Title;
                acctInfo.Opening_Date = DateTime.Now.ToString("yyyyMMdd");
                acctInfo.Company_Code = accountInfo.Company_Code;
                acctInfo.Company_Name = accountInfo.Company_Name;
                acctInfo.Account_Status = "ACTIVE";

                acctInfo.Main_Holder = new MAIN_HOLDER();
                acctInfo.Main_Holder.Customer = accountInfo.Main_Holder.Customer;
                acctInfo.Main_Holder.Short_Name = accountInfo.Main_Holder.Short_Name;

                acctInfo.Category_Item = new CATEGORY_ITEM();
                acctInfo.Category_Item.Category = accountInfo.Category_Item.Category;
                acctInfo.Category_Item.Category_Desc = accountInfo.Category_Item.Category_Desc;

                acctInfo.Currency_Item = new CURRENCY_ITEM();
                acctInfo.Currency_Item.Currency = accountInfo.Currency_Item.Currency;
                acctInfo.Currency_Item.Currency_Desc = accountInfo.Currency_Item.Currency_Desc;

                acctInfo.Balance = new BALANCE();
                acctInfo.Balance.Available_Balance = accountInfo.Balance.Available_Balance;
                acctInfo.Balance.Previous_Balance = 0;

                acctInfo.Mailing_Address = new MAILING_ADDRESS_ITEM();
                acctInfo.Mailing_Address.Short_Name = accountInfo.Mailing_Address.Short_Name;
                acctInfo.Mailing_Address.Name_1 = accountInfo.Mailing_Address.Name_1;
                acctInfo.Mailing_Address.Name_2 = accountInfo.Mailing_Address.Name_2;
                acctInfo.Mailing_Address.Street_Addr = accountInfo.Mailing_Address.Street_Addr;
                acctInfo.Mailing_Address.Address_Line2 = accountInfo.Mailing_Address.Address_Line2;
                acctInfo.Mailing_Address.Address_Line3 = accountInfo.Mailing_Address.Address_Line3;
                acctInfo.Mailing_Address.Town_Country = accountInfo.Mailing_Address.Town_Country;
                acctInfo.Mailing_Address.Post_Code = accountInfo.Mailing_Address.Post_Code;
                acctInfo.Mailing_Address.Country = accountInfo.Mailing_Address.Country;
                acctInfo.Mailing_Address.Country_Code = accountInfo.Mailing_Address.Country_Code;

                //Add record to table
                htAccount.Add(acctInfo.Account_No, acctInfo);
                //Set Account number
                resp.Account_Info = acctInfo;
            }
            catch (Exception ex)
            {
                //Set return code value to -ve value to indicate error
                resp.Return_Code = "-1";
                resp.Error_Msg = ex.Message.ToString();

            }
            return resp;
        }

        public static GET_ACCOUNT_INFO_RESP getAccountInfo(string strAccountNumber)
        {
            GET_ACCOUNT_INFO_RESP resp = new GET_ACCOUNT_INFO_RESP();
            //Set response to error
            resp.Return_Code = "-1";

            try
            {
                if (htAccount.Contains(strAccountNumber))
                {
                    //Retrieve account info
                    ACCOUNT_INFO account = (ACCOUNT_INFO)htAccount[strAccountNumber];

                    //Check if account is active
                    if (account.Account_Status == STATUS.Inactive)
                    {
                        resp.Return_Code = "1";
                        throw new Exception("Account is inactive");
                    }
                    else
                    {
                        resp.Account_Info = account;
                    }
                }
                else
                {
                    resp.Return_Code = "1";
                    throw new Exception("Invalid account number");
                }
            }
            catch (Exception ex)
            {
                resp.Error_Msg = ex.Message.ToString();
            }
            return resp;
        }

        public static GET_ACCOUNT_INFO_RESP deactivateAccount(string strAccountNumber)
        {
            GET_ACCOUNT_INFO_RESP resp = new GET_ACCOUNT_INFO_RESP();
            //Set response to error
            resp.Return_Code = "-1";

            try
            {
                if (htAccount.Contains(strAccountNumber))
                {
                    ACCOUNT_INFO account = (ACCOUNT_INFO)htAccount[strAccountNumber];

                    //Check if account is active
                    if (account.Account_Status == STATUS.Inactive)
                    {
                        resp.Return_Code = "1";
                        throw new Exception("Account is already inactive");
                    }
                    else
                    {
                        //Update account status
                        account.Account_Status = STATUS.Inactive;

                        //Update account table
                        htAccount[strAccountNumber] = account;

                        //Update response
                        resp.Return_Code = "0";
                        resp.Account_Info = account;
                    }
                }
                else
                {
                    resp.Return_Code = "1";
                    throw new Exception("Invalid account number");
                }
            }
            catch (Exception ex)
            {
                resp.Error_Msg = ex.Message.ToString();
            }
            return resp;
        }

        public static GET_ACCOUNT_INFO_RESP performTransaction(string tranType, string strAccountNumber, float amount)
        {
            GET_ACCOUNT_INFO_RESP resp = new GET_ACCOUNT_INFO_RESP();
            //Set response to error
            resp.Return_Code = "-1";

            try
            {
                if (htAccount.Contains(strAccountNumber))
                {
                    ACCOUNT_INFO account = (ACCOUNT_INFO)htAccount[strAccountNumber];

                    //Check if account is active
                    if (account.Account_Status == STATUS.Inactive)
                    {
                        resp.Return_Code = "1";
                        throw new Exception("Account is inactive");
                    }
                    else
                    {
                        switch (tranType.ToUpper())
                        {
                            case "DEBIT":
                                //Check if requested value can be debited
                                if (account.Balance.Available_Balance > amount)
                                {
                                    //Set previous balance to current balance
                                    account.Balance.Previous_Balance = account.Balance.Available_Balance;

                                    //credit available balance
                                    account.Balance.Available_Balance -= amount;

                                    //Update account table
                                    htAccount[strAccountNumber] = account;

                                    //Update response
                                    resp.Return_Code = "0";
                                    resp.Account_Info = account;
                                }
                                else
                                {
                                    resp.Return_Code = "1";
                                    throw new Exception("Debit amount is greater than available balance");
                                }
                                break;

                            case "CREDIT":
                                account.Balance.Previous_Balance = account.Balance.Available_Balance;

                                //credit available balance
                                account.Balance.Available_Balance += amount;

                                //Update account table
                                htAccount[strAccountNumber] = account;

                                //Update response
                                resp.Return_Code = "0";
                                resp.Account_Info = account;
                                break;
                        }
                    }
                }
                else
                {
                    resp.Return_Code = "1";
                    throw new Exception("Invalid account number");
                }
            }
            catch (Exception ex)
            {
                resp.Error_Msg = ex.Message.ToString();
            }
            return resp;
        }

        public void generateAccountHashTable(Hashtable ht)
        {
            string[] strAccountNumbers = new string[] { "000441123111", "000441123112", "000441123113", "000455698128" };
            string[] strShortTitles = new string[] { "MR JOHN DOE", "MR OLIVER CHUNG", "MRS KAREEN LI", "MR BEN SKY" };
            string[] strOpeningDate = new string[] { "20120524", "20100210", "20170815", "20000115" };
            string[] strCustomer = new string[] { "1234741", "1236548", "1123564", "1023841" };

            string[] strShortName = new string[] { "MR JOHN DOE", "MR OLIVER CHUNG", "MRS KAREEN LI", "MR BEN SKY" };


            float[] strAvailableBalances = new float[] { 50000, 100000.20f, 200000, 50 };
            string[] strStreetAddr = new string[] { "1 ROYAL ROAD", "20 ABS ROAD", "11 OLLIER AVE", "250 COASTAL ROAD" };
            string[] strTownCountry = new string[] { "Beau Bassin", "Rose Hill", "Quatre Bornes", "ALBION" };

            //Loop through sample data to generate account numbers
            for (int i = 0; i < strAccountNumbers.Length; i++)
            {
                ACCOUNT_INFO acctInfo = new ACCOUNT_INFO();
                acctInfo.Account_No = strAccountNumbers[i];
                acctInfo.Short_Title = strShortTitles[i];
                acctInfo.Opening_Date = strOpeningDate[i];
                acctInfo.Company_Code = "MU0010044";
                acctInfo.Company_Name = "MCB PORT LOUIS";

                //Use of static classes to keep status values
                if (acctInfo.Account_No == "000455698128")
                {
                    acctInfo.Account_Status = STATUS.Inactive;
                }
                else
                {
                    acctInfo.Account_Status = STATUS.Active;
                }

                acctInfo.Main_Holder = new MAIN_HOLDER();
                acctInfo.Main_Holder.Customer = strCustomer[i];
                acctInfo.Main_Holder.Short_Name = strShortName[i];

                acctInfo.Category_Item = new CATEGORY_ITEM();
                acctInfo.Category_Item.Category = "6001";
                acctInfo.Category_Item.Category_Desc = "Savings Regular Account";

                acctInfo.Currency_Item = new CURRENCY_ITEM();
                acctInfo.Currency_Item.Currency = "MUR";
                acctInfo.Currency_Item.Currency_Desc = "Mauritian Rupee";

                acctInfo.Balance = new BALANCE();
                acctInfo.Balance.Available_Balance = strAvailableBalances[i];
                acctInfo.Balance.Previous_Balance = strAvailableBalances[i];

                acctInfo.Mailing_Address = new MAILING_ADDRESS_ITEM();
                acctInfo.Mailing_Address.Short_Name = strShortName[i];
                acctInfo.Mailing_Address.Name_1 = strShortTitles[i];
                acctInfo.Mailing_Address.Street_Addr = strStreetAddr[i];
                acctInfo.Mailing_Address.Town_Country = strTownCountry[i];
                acctInfo.Mailing_Address.Country = "MAURITIUS";
                acctInfo.Mailing_Address.Country_Code = "MU";

                ht.Add(strAccountNumbers[i], acctInfo);
            }
            htAccount = ht;
        }


    }
    


}
