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
    public class BAVatType
    {
        DAVatType objDAVatType = new DAVatType();
        public int InsUpdVatType(EMVatType objEMVatType)
         {
            return objDAVatType.InsUpdVatType(objEMVatType);
         }
        public DataSet GetVatType(int VatId)
        {
            return objDAVatType.GetVatType(VatId);
        }
        public int DeleteVatType(int VatId)
        {
            return objDAVatType.DeleteVatType(VatId);
        }
    }
}
