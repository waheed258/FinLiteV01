using DataManager;
using EntityManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManager
{
    public class BALAgencyCreditMemo
    {
        DALAgencyCreditMemo objDALACM = new DALAgencyCreditMemo();
        DOUtility _objDOUtility = new DOUtility();

        public int InsertAgencycreditmemo(EMAgencyCreditMemo objAgencycreditmemo)
        {
            return objDALACM.InsertAgencycreditmemo(objAgencycreditmemo);
        }
    }
}
