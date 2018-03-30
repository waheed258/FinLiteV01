using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using EntityManager;
namespace DataManager
{
    public class DALTransactions : DataUtilities
    {
        public int ReceivedTransactionInsert(TransactionMaster objTransactionMaster)
        {
            Hashtable htparams = new Hashtable
            {
                {"@InvoiceId",objTransactionMaster.InvoiceId},
                {"@Divission",objTransactionMaster.Divission},
                {"@ReceiptType",objTransactionMaster.ReceiptType},
                {"@AutoDepositeId",objTransactionMaster.AutoDepositeId},
                {"@AutoDepositeAccountNo",objTransactionMaster.AutoDepositeAccountNo},
                {"@ClientTypeId",objTransactionMaster.ClientTypeId},
                {"@ClientAccountNo",objTransactionMaster.ClientAccountNo},
                {"@ClientAccountNoID",objTransactionMaster.ClientAccountNoID},
                {"@PayeeDetails",objTransactionMaster.PayeeDetails},
                {"@ReceiptAmount",objTransactionMaster.ReceiptAmount},
                {"@PrvClientOpenAmount",objTransactionMaster.PrvClientOpenAmount},
                {"@AllocatedAmount",objTransactionMaster.AllocatedAmount},
                {"@ReceiptBalanceAmount",objTransactionMaster.ReceiptBalanceAmount},
                {"@InvoiceBalanceAmount",objTransactionMaster.InvoiceBalanceAmount},
                {"@Details",objTransactionMaster.Details},
                {"@Messages",objTransactionMaster.Messages},
                {"@CreatedBy",objTransactionMaster.CreatedBy},
                {"@PaymentSourceRef",objTransactionMaster.PaymentSourceRef},
                 {"@ReceiptAfterPaid",objTransactionMaster.ReceiptAmountAfterPaid},
                 {"@SuspenseAccId",objTransactionMaster.SuspenseAccId}
              
            };
            return ExecuteNonQuery("ReceivedTransaction_insert_update", htparams,"@return");

        }
        public int OpenAmountDetailsInsertUpdateMaster(OpenAmountDetails objOpenAmountDetails)
        {
            Hashtable htparams = new Hashtable
            {
                {"@ClientTypeId",objOpenAmountDetails.ClientTypeId},
                {"@ClientNameId",objOpenAmountDetails.ClientNameId},
                {"@ReceiptAmount",objOpenAmountDetails.ReceiptAmount},
                {"@PrvOpenAmount",objOpenAmountDetails.PrvOpenAmount},
                {"@AlocatedAmount",objOpenAmountDetails.AlocatedAmount},
                {"@ReceiptOpenAmount",objOpenAmountDetails.ReceiptOpenAmount},
                {"@SourceRef",objOpenAmountDetails.SourceRef},
                {"@ReceiptType",objOpenAmountDetails.ReceiptType},
                {"@FromAccount",objOpenAmountDetails.FromAccount},
                {"@ToAccount",objOpenAmountDetails.ToAccount},
                {"@CreatedBy",objOpenAmountDetails.CreatedBy}
              
            };
            return ExecuteNonQuery("OpenAmountDetails_insert", htparams);

        }

        public int OpenAmountsupplDetailsInsertUpdateMaster(ReceivedTransaction objOpenAmountDetails)
        {
            Hashtable htparams = new Hashtable
            {
                {"@SupplTypeId",objOpenAmountDetails.SupplTypeId},
                {"@SupplNameId",objOpenAmountDetails.SupplNameId},
                {"@ReceiptAmount",objOpenAmountDetails.ReceiptAmount},
                {"@PrvOpenAmount",objOpenAmountDetails.PrvOpenAmount},
                {"@AlocatedAmount",objOpenAmountDetails.AlocatedAmount},
                {"@PaymentOpenAmount",objOpenAmountDetails.PaymentOpenAmount},
                {"@SourceRef",objOpenAmountDetails.SourceRef},
                {"@PaymentType",objOpenAmountDetails.PaymentType},
                {"@FromAccount",objOpenAmountDetails.FromAccount},
                {"@ToAccount",objOpenAmountDetails.ToAccount},
                {"@CreatedBy",objOpenAmountDetails.CreatedBy}
              
            };
            return ExecuteNonQuery("OpenAmountSupplDetails_insert", htparams);

        }

        public int PaymentTransactionInsert(PaymentTransaction objTransactionMaster)
        {
            Hashtable htparams = new Hashtable
            {
                {"@InvoiceId",objTransactionMaster.InvoiceId},
                {"@TicketId",objTransactionMaster.TicketId},
                {"@Divission",objTransactionMaster.Divission},
                {"@PaymentType",objTransactionMaster.PaymentType},
                {"@AutoDepositeId",objTransactionMaster.AutoDepositeId},
                {"@AutoDepositeAccountNo",objTransactionMaster.AutoDepositeAccountNo},
                {"@ClientSupplTypeId",objTransactionMaster.ClientSupplTypeId},
                {"@ClientSupplAccountNo",objTransactionMaster.ClientSupplAccountNo},
                {"@ClientSupplAccountNoID",objTransactionMaster.ClientSupplAccountNoID},
                {"@PayeeDetails",objTransactionMaster.PayeeDetails},
                {"@PaymentAmount",objTransactionMaster.PaymentAmount},
                {"@PrvClientOpenAmount",objTransactionMaster.PrvClientOpenAmount},
                {"@AllocatedAmount",objTransactionMaster.AllocatedAmount},
                {"@PaymentBalanceAmount",objTransactionMaster.PaymentBalanceAmount},
                {"@InvoiceBalanceAmount",objTransactionMaster.InvoiceBalanceAmount},
                {"@Details",objTransactionMaster.Details},
                {"@Messages",objTransactionMaster.Messages},
                {"@CreatedBy",objTransactionMaster.CreatedBy},
                {"@PaymentSourceRef",objTransactionMaster.PaymentSourceRef},
               {"@categoryName",objTransactionMaster.categoryName},
                {"@MainAcc",objTransactionMaster.MainAccount},
               {"@CategoryId",objTransactionMaster.CategoryId},
               
              
            };
            return ExecuteNonQuery("PaymentTransaction_insert_update", htparams);

        }

        public DataSet PaymentList()
        {
            return ExecuteDataSet("PaymentTransaction_GetList");
        }
        public DataSet ReceiptsGet()
        {

            return ExecuteDataSet("ReceivedTransaction_Get");
        }

        public DataSet PaymentTransaction_GetDataBycategory(int Category, string CategoryName)
        {
            Hashtable htparams = new Hashtable
            {
                {"@CategoryId",Category},
                {"@Categoryname",CategoryName},
            };


            return ExecuteDataSet("PaymentTransaction_getCategoriesByData", htparams); 
          }


        //Transaction Table Insert


        public int TransactionInsert(Transaction objtransaction)
        {
            Hashtable htparams = new Hashtable
            {
                {"@TransId",objtransaction.TransId},
                {"@FmAccountNO",objtransaction.FmAccountNO},
                {"@ReferenceAccountNO",objtransaction.ReferenceAccountNO},
                {"@CreditAmount",objtransaction.CreditAmount},
                {"@DebitAmount",objtransaction.DebitAmount},
                {"@ReferenceNo",objtransaction.ReferenceNo},               
                {"@InvoiceId",objtransaction.InvoiceId},
                {"@InvoiceNo",objtransaction.InvoiceNo},
                {"@FmMainAccount",objtransaction.FmMainAccount},
                {"@ReferenceType",objtransaction.ReferenceType},
                 {"@FmAccountNoId",objtransaction.FmAccountNoId},
                {"@ReferenceAccountNoId",objtransaction.ReferenceAccountNoId},
                {"@BalanceAmount",objtransaction.BalanceAmount},
                {"@CreatedBy",objtransaction.CreatedBy},
                 {"@ToMainAccount",objtransaction.ToMainAccount},
              
            };
            return ExecuteNonQuery("Transactions_Insert", htparams);

        }
        public DataSet Transaction_GetAccountsData(int ChartedAccId, int BankAccount, string RefType, string CreatedBy)
        {
            Hashtable htparams = new Hashtable
            {
                {"@FromAccountNoID",ChartedAccId},
                {"@ToAccountId",BankAccount},
                  {"@RefType",RefType},
                  {"@CreatedBy",CreatedBy},
            };


            return ExecuteDataSet("Transaction_GetAccount", htparams);
        }

      

    }
}
