using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using EntityManager;

namespace DataManager
{
   public class DACreditCard:DataUtilities
    {
       public int InsUpdCreditType(EMCreditCard objEMCredit)
       {
           Hashtable htparams = new Hashtable{
                                          {"@CreditCardId",objEMCredit.CreditCardId},
                                          {"@CreditCardKey",objEMCredit.CreditCardKey},
                                          {"@CreditDescription",objEMCredit.CreditDescription},
                                          {"@NumberPrefix",objEMCredit.NumberPrefix},
                                          {"@CreatedBy",objEMCredit.CreatedBy},
           };
           return ExecuteNonQuery("CreditCardType_InsertUpdate", htparams);
       }

       public DataSet GetCreditCard(int CreditCardId)
       {
           Hashtable htparams = new Hashtable{
                                          {"@CreditCardId",CreditCardId},
           };
           return ExecuteDataSet("CreditCardType_Get", htparams);
       }

       public int DeleteCreditCard(int CreditCardId)
       {
           Hashtable htparams = new Hashtable{
                                          {"@CreditCardId",CreditCardId},
           };
           return ExecuteNonQuery("CreditCardTypes_Delete", htparams);
       }
    }
}
