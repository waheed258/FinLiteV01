using EntityManager;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataManager
{
    public class DALMainAccount : DataUtilities
    {
        public int MainAccountInsert(EmMainAccount objMainAccount)
        {
            Hashtable htParams = new Hashtable
                                     {
                                       {"@MainAccId",objMainAccount.MainAccId},
                                         {"@MainAccCode",objMainAccount.MainAccCode},
                                         {"@MainAcName",objMainAccount.MainAcName},
                                         {"@CompanyId",objMainAccount.CompanyId},                                                                             
                                         {"@BranchId",objMainAccount.BranchId},                                        
                                         {"@CreatedBy",objMainAccount.CreatedBy},
                                     {"@DepartmentId",objMainAccount.DepartmentId},
                                       {"@BaseCurrency",objMainAccount.BaseCurrency},
                                         {"@TranCurrency",objMainAccount.TranCurrency},
                                           {"@AcType",objMainAccount.AcType},
                                           {"@CategoryId",objMainAccount.CategoryId},
                                         
                                     };
            return ExecuteNonQuery("MainAccounts_Insert", htParams);
        }
        public DataSet BindDepartments()
        {

            return ExecuteDataSet("Department_Get");
        }

        public DataSet BindCurrency()
        {

            return ExecuteDataSet("Currency_Get");
        }
        public DataSet BindDefaultCurrency()
        {
            return ExecuteDataSet("Currency_getDefaultCurrency");
        }
        public object IsExitMainAccCode(string AccCode)
        {
            Hashtable htParams = new Hashtable
                                     {
                                       {"@MainAccCode",AccCode}
                                     };

            return ExecuteDataSet("MainAccount_IsExitAccCode",htParams);
        }
        public DataSet getMainAccounts()
        {

            return ExecuteDataSet("MainAcount_ListGet");
        }
    }
}
