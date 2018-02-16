using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataManager;
using System.Data;
using EntityManager;

namespace BusinessManager
{
   public class BAIncomeReceipt
    {

       DAIncomeReceipt objDAIncomeReceipt = new DAIncomeReceipt();
       public DataSet GetChartedAccount()
        {
            return objDAIncomeReceipt.GetChartedAccount();
        }

     
       public int InsertIncomeReceipt(EMIncomeReceipt objIncomereceipt)
       {
           return objDAIncomeReceipt.InsertIncomeReceipt(objIncomereceipt);

       }
    }
}
