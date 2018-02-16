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
   public class BALMainAccount
    {
       private DALMainAccount _objDALMainAcc = new DALMainAccount();
       DOUtility _objDOUtility = new DOUtility();
        public int MainAccountInsert(EmMainAccount objMainaccount)
        {
            return _objDALMainAcc.MainAccountInsert(objMainaccount);
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
        public object IsExitMainAccCode(string AccCode)
        {
            return _objDALMainAcc.IsExitMainAccCode(AccCode);
            
        }
        public DataSet getMainAccounts()
        {
            return _objDALMainAcc.getMainAccounts();
        }

    }
}
