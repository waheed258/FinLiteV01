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
    public class BAAirSuppliers
    {
        private DAAirSuppliers objDASupplier = new DAAirSuppliers();

        public int InsUpdAirSupplier(EMAirSuppliers objSupplier)
        {
            return objDASupplier.InsUpdAirSupplier(objSupplier);
        }

        public DataSet GetAirSuppliers(int SupplierId)
        {
            return objDASupplier.GetAirSuppliers(SupplierId);
        }

        public int DeleteAirSupplier(int SupplierId)
        {
            return objDASupplier.DeleteAirSupplier(SupplierId);
        }

        //CharteredAccounts

        public int InsUpdChartAccounts(EMAirSuppliers objSupplier)
        {
            return objDASupplier.InsUpdChartAccounts(objSupplier);
        }
        //Charted Accounts Update 
        public int UpdateChartAccounts(EMAirSuppliers objSupplier)
        {
            return objDASupplier.UpdChartAccounts(objSupplier);
        }
    }
}
