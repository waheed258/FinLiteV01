using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityManager;
using System.Data;
using DataManager;
namespace BusinessManager
{
    public class BARouteMiles
    {
        DARouteMiles objDARouteMiles = new DARouteMiles();
        public int InsUpdRouteMiles(EMRouteMiles objEMRouteMiles)
        {
            return objDARouteMiles.InsUpdRouteMiles(objEMRouteMiles);
        }
        public DataSet GetRouteMiles(int RMId)
        {
            return objDARouteMiles.GetRouteMiles(RMId);
        }
        public int DeleteRouteMiles(int RMId)
        {
            return objDARouteMiles.DeleteRouteMiles(RMId);
        }
    }
}
