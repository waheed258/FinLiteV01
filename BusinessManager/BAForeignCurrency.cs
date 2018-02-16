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
    public class BAForeignCurrency
    {
        DAForeignCurrency objDAForeignCurrency = new DAForeignCurrency();
        public int InsertUpdateForeignCurrency(EMForeignCurrency objEMForeignCurrency)
        {
            return objDAForeignCurrency.InsertUpdateForeignCurrency(objEMForeignCurrency);
        }
        public DataSet GetForeignCurrency(int Id)
        {
            return objDAForeignCurrency.GetForeignCurrency(Id);
        }
        public int DeleteForeignCurrency(int Id)
        {
            return objDAForeignCurrency.DeleteForeignCurrency(Id);
        }
    }
}
