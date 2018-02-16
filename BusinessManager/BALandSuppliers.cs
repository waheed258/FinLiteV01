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
    public class BALandSuppliers
    {
        DALandSuppliers objLandSupp = new DALandSuppliers();
        public int InsUpdlandSupplier(EMLandSuppliers objEMLandSupp)
        {
            return objLandSupp.InsUpdlandSupplier(objEMLandSupp);
        }

        public DataSet GetLandSupplier(int LSupplierId)
        {
            return objLandSupp.GetLandSupplier(LSupplierId);
        }

        public int DeleteLandSupplier(int LSupplierId)
        {
            return objLandSupp.DeleteLandSupplier(LSupplierId);
        }



        public int InsertUpdContact(EMLandSuppliers objEMLandSupp)
        {
            return objLandSupp.InsertUpdContact(objEMLandSupp);
        }

      
        public int DeleteContact(int ContactId)
        {
            return objLandSupp.DeleteContact(ContactId);
        }

        //CharteredAccounts
        public int InsUpdChartAccounts(EMLandSuppliers objEMLandSupp)
        {
            return objLandSupp.InsUpdChartAccounts(objEMLandSupp);
        }
        //Charted Accounts Update 
        public int UpdateChartAccounts(EMLandSuppliers objEMLandSupp)
        {
            return objLandSupp.UpdChartAccounts(objEMLandSupp);
        }
    }
}
