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
    public class BABankAccount
    {
        DABankAccount objDABankAc = new DABankAccount();
        public int InsUpdBankAccount(EMBankAccount objBankAc)
        {
            return objDABankAc.InsUpdBankAccount(objBankAc);
        }
        public DataSet GetBankAccount(int BankAcId)
        {
            return objDABankAc.GetBankAccount(BankAcId);
        }
        public int DeleteBankAccount(int BankAcId)
        {
            return objDABankAc.DeleteBankAccount(BankAcId);
        }
    }
}
