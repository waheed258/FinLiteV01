using DataManager;
using EntityManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManager
{
   public class BADepositTransaction
    {
       DADepositTransaction objDADepositTransaction = new DADepositTransaction();
       public DataSet getUnbankReceipts(int RType, int CType)
       {
           return objDADepositTransaction.getUnbankReceipts(RType,CType);
       }
       public int insertDepositMaster(EmDepositMaster objEmDepositMaster)
       {
           return objDADepositTransaction.insertDepositMaster(objEmDepositMaster);
       }
       public int insertDepositChild(EMDepositChild objEmDepositChild)
       {
           return objDADepositTransaction.insertDepositChild(objEmDepositChild);
       }

        public DataSet DepositList()
       {
           return objDADepositTransaction.getDepositList();
       }

       
    }
}
