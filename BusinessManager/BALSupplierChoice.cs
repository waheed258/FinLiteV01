using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using EntityManager;
using DataManager;

namespace BusinessManager
{
    public class BALSupplierChoice
    {
        DALSupplierChoice objDALChoice = new DALSupplierChoice();
        public int InsUpdSuppChoice(EMSupplierChoice objEMSupChoice)
        {
            return objDALChoice.InsUpdSuppChoice(objEMSupChoice);
        }
        public DataSet GetSuppChoice(int SupplierChoiceId)
        {
            return objDALChoice.GetSuppChoice(SupplierChoiceId);
        }
        public int DeleteSuppChoice(int SupplierChoiceId)
        {
            return objDALChoice.DeleteSuppChoice(SupplierChoiceId);
        }

    }
}
