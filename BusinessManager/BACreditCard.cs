using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityManager;
using System.Data;
using DataManager;

namespace BusinessManager
{
   public class BACreditCard
    {
       DACreditCard objDACredit = new DACreditCard();
       public int InsUpdCreditType(EMCreditCard objEMCredit)
       {
           return objDACredit.InsUpdCreditType(objEMCredit);
       }

       public DataSet GetCreditCard(int CreditCardId)
       {
           return objDACredit.GetCreditCard(CreditCardId);
       }

       public int DeleteCreditCard(int CreditCardId)
       {
           return objDACredit.DeleteCreditCard(CreditCardId);
       }
    }
}
