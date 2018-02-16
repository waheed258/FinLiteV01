using DataManager;
using EntityManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using EntityManager;
namespace BusinessManager
{
 public   class BALCharteredAccounts
    {
     private DALCharteredAccounts _objDALChartedAcc = new DALCharteredAccounts();
     DOUtility _objDOUtility = new DOUtility();
     public DataSet BindMasterAcNames()
        {
            return _objDALChartedAcc.BindMasterAcNames();
        }

     public DataSet BindMasterTypeandAccountCode(int MasteAccId)
     {
         return _objDALChartedAcc.BindMasterTypeandAccountCode(MasteAccId);
     }

     public DataSet BindCharAccList()
     {
         return _objDALChartedAcc.BindCharAccList();
     }
     public int CharteredAccountInsert(EmCharteredAccounts objcharteredaccount)
     {
         return _objDALChartedAcc.CharteredAccountInsert(objcharteredaccount);
     }

     public DataSet BindDepartments()
     {
         return _objDOUtility.BindDepartments();
     }


     public DataSet BindCurrency()
     {
         return _objDOUtility.BindCurrency();
     }

     public DataSet BindDefaultCurrency()
     {
         return _objDOUtility.BindDefaultCurrency();
     }
    }
}
