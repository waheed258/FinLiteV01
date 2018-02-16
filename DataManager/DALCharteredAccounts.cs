using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataManager;
using System.Collections;
using EntityManager;

namespace DataManager
{
    public class DALCharteredAccounts : DataUtilities
    {
      public DataSet BindMasterAcNames()
      {

          return ExecuteDataSet("MasterAccounts_GetMasterAcNames");
      }

      public DataSet BindMasterTypeandAccountCode(int MasteAccId)
      {
          Hashtable htParams = new Hashtable
                                     {
                                       
                                         {"@MasterAccountId",MasteAccId}
                                     };
          return ExecuteDataSet("MasterAccounts_getMasterAccountCodeandType",htParams);
      }

     
      public int CharteredAccountInsert(EmCharteredAccounts objChartedAccount)
      {
          Hashtable htParams = new Hashtable
                                     {
                                       {"@ChartedAccId",objChartedAccount.ChartedAccId},
                                         {"@ChartedAccName",objChartedAccount.ChartedAccName},
                                         {"@ChartedMasterAccName",objChartedAccount.ChartedMasterAccName},
                                         {"@Type",objChartedAccount.Type}, 
                                         {"@AccCode",objChartedAccount.AccCode},
                                         {"@CompanyId",objChartedAccount.CompanyId},                                                                             
                                         {"@BranchId",objChartedAccount.BranchId},  
                                         {"@DepartmentId",objChartedAccount.DepartmentId},
                                         {"@CreatedBy",objChartedAccount.CreatedBy},
                                     {"@BaseCurrency",objChartedAccount.BaseCurrency},
                                         {"@TranCurrency",objChartedAccount.TranCurrency},
                                         {"@CategoryId",objChartedAccount.CategoryId},
                                         
                                     };
          return ExecuteNonQuery("CharteredAccounts_Insert", htParams);
      }

      public DataSet BindCharAccList()
      {

          return ExecuteDataSet("CharteredAccounts_ChartedAccList");
      }
    }
}
