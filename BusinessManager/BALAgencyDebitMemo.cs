using DataManager;
using EntityManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManager
{
    public class BALAgencyDebitMemo
    {
        DALAgencyDebitMemo objDALADM = new DALAgencyDebitMemo();
        DOUtility _objDOUtility = new DOUtility();
        public int InserAgencydebitmemo(EMAgencyDebitMemo objAgencydebitmemo)
        {
            return objDALADM.InsertAgencydebitmemo(objAgencydebitmemo);
        }
    }
}
