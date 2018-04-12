using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using EntityManager;
using DataManager;
namespace BusinessManager
{
    public class BALTransactions
    {
        DALTransactions _objDALTransactions = new DALTransactions();
        public int ReceivedTransactionInsert(TransactionMaster objTransactionMaster)
        {
            return _objDALTransactions.ReceivedTransactionInsert(objTransactionMaster);
        }
        public int OpenAmountDetailsInsertUpdateMaster(OpenAmountDetails objOpenAmountDetails)
        {
            return _objDALTransactions.OpenAmountDetailsInsertUpdateMaster(objOpenAmountDetails);
        }

         public int OpenAmountSupplDetailsInsertUpdateMaster(ReceivedTransaction objOpenAmountDetails)
        {
            return _objDALTransactions.OpenAmountsupplDetailsInsertUpdateMaster(objOpenAmountDetails);
        }

         public int PaymentTransactionInsert(PaymentTransaction objpaymentTransactionMaster)
        {
            return _objDALTransactions.PaymentTransactionInsert(objpaymentTransactionMaster);
        }

         public DataSet PaymentTransaction_GetDataBycategory(int Category, string CategoryName)
         {
             return _objDALTransactions.PaymentTransaction_GetDataBycategory(Category,CategoryName);
         }

        
        public DataSet GetRecipts()
        {
            return _objDALTransactions.ReceiptsGet();
        }

        public DataSet PaymentList()
        {
            return _objDALTransactions.PaymentList();
        }


        //Transaction insert

        public int TransactionInsert(Transaction objtransaction)
        {
            return _objDALTransactions.TransactionInsert(objtransaction);
        }

        public DataSet Transaction_GetAccountsData(int ChartedAccId, int BankAccount,string RefType,string Category)
        {
            return _objDALTransactions.Transaction_GetAccountsData(ChartedAccId, BankAccount, RefType, Category);
        }

        public DataSet Get_PrintReceipt(int invid)
        {
            return _objDALTransactions.Get_PrintReceipt(invid);
        }
    }
}
