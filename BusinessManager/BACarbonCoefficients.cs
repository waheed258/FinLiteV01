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
    public class BACarbonCoefficients
    {
        DACarbonCorfficients objDACarbon = new DACarbonCorfficients();
        public int InsUpdCarbonCoefficients(EMCarbonCoefficients objCarbon)
        {
            return objDACarbon.InsUpdCarbonCoefficients(objCarbon);
        }
        public DataSet GetCarbonCoefficients(int CarbonId)
        {
            return objDACarbon.GetCarbonCoefficients(CarbonId);
        }

        public int DeleteCarbonCoefficients(int CarbonId)
        {
            return objDACarbon.DeleteCarbonCoefficients(CarbonId);
        }

    }
}
