using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityManager;
using DataManager;
using System.Data;

namespace BusinessManager
{
    public class BAContinents
    {

        DAContinents objDAContinents = new DAContinents();
        public int IsuUpdContinents(EMContinents objContinents)
        {
            return objDAContinents.IsuUpdContinents(objContinents);
        }
        public DataSet GetContinents(int ContinentId)
        {
            return objDAContinents.GetContinents(ContinentId);
        }

        public int DeleteContinents(int ContinentId)
        {
            return objDAContinents.DeleteContinents(ContinentId);
        }
    }
}
