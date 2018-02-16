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
   
    public class DABankAccount : DataUtilities
    {
       public int InsUpdBankAccount(EMBankAccount objBankAc)
        {
            Hashtable htparams = new Hashtable
            {
                {"@BankAcId",objBankAc.BankAcId},
                {"@BankAcKey",objBankAc.BankAcKey},
                {"@IsDeActivate",objBankAc.IsDeActivate},
                {"@BankName",objBankAc.BankName},
                {"@BankAcType",objBankAc.BankAcType},
                {"@BankAcNo",objBankAc.BankAcNo},
                {"@BankBranchCode",objBankAc.BankBranchCode},
                {"@BranchName",objBankAc.BranchName},
                {"@AccountHolder",objBankAc.AccountHolder},
                {"@Graphic",objBankAc.Graphic},
                {"@OwnerBranch",objBankAc.OwnerBranch},
                {"@GiCode",objBankAc.GiCode},
                {"@GiDepositBatch",objBankAc.GiDepositBatch},
                {"@GiPaymentBatch",objBankAc.GiPaymentBatch},
                {"@InternetBankingLink",objBankAc.InternetBankingLink},
                {"@StatementFormat",objBankAc.StatementFormat},
                {"@CreatedBy",objBankAc.CreatedBy}
            };
            int IsSuccess = ExecuteNonQuery("BankAccounts_InsertUpdate", htparams);
            return IsSuccess;
        }
       public DataSet GetBankAccount(int BankAcId)
       {
           Hashtable htparams = new Hashtable
           {
             {"@BankAcId",BankAcId}
           };

           return ExecuteDataSet("BankAccounts_Get", htparams);

       }
       public int DeleteBankAccount(int BankAcId)
       {
           Hashtable htparams = new Hashtable
           {
             {"@BankAcId",BankAcId}
           };

           return ExecuteNonQuery("BankAccounts_Delete", htparams);

       }

    }
    
}
    

