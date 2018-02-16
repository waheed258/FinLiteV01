using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityManager;
using System.Collections;

namespace DataManager
{
    public class DAIncomeReceipt : DataUtilities
    {

        public int InsertIncomeReceipt(EMIncomeReceipt objIncomereceipt)
        {
            Hashtable htparams = new Hashtable
            {
                {"@IncomeId",objIncomereceipt.IncomeId},
                {"@FromIncomeAccount",objIncomereceipt.FromIncomeAccount},
                {"@FromMainAccount",objIncomereceipt.FromMainAccount},
                {"@ToIncomeAccount",objIncomereceipt.ToIncomeAccount},
                {"@ToMainAccount",objIncomereceipt.ToMainAccount},
                {"@Category",objIncomereceipt.Category},
                {"@IncomeAmount",objIncomereceipt.IncomeAmount},
                {"@createdBy",objIncomereceipt.createdBy},
                {"@Incomedate",objIncomereceipt.Incomedate},
                {"@SourceRefNo",objIncomereceipt.SourceRefNo},
            };
            int IsSuccess = ExecuteNonQuery("IncomeReceipt_insert", htparams);
            return IsSuccess;
        }

        public DataSet GetChartedAccount()
        {
            return ExecuteDataSet("IncomeAccount_Details");

        }



    }
}
