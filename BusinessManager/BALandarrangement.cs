using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityManager;
using DataManager;

using System.Data;
using System.Data.SqlClient;

namespace BusinessManager
{
    public class BALandarrangement
    {

        DALandarrangement objDALandarrangement = new DALandarrangement();
        DOUtility _objDOUtility = new DOUtility();

        public int InsertLanadarrangement(EMLandarrangement objLandarrangement)
        {
            return objDALandarrangement.InsertLandArrangement(objLandarrangement);
        }
       
       
    }
}
