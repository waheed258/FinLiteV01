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
    public class BAPaymentTypes
    {
        DAPaymentTypes objDAPaymentTypes = new DAPaymentTypes();

        public int InsUpdPaymentTypes(EMPaymentTypes objPaymentTypes)
        {
            return objDAPaymentTypes.InsUpdPaymentTypes(objPaymentTypes);
        }

        public DataSet GetPaymentTypes(int PaymentId)
        {
            return objDAPaymentTypes.GetPaymentTypes(PaymentId);
        }

        public int DeletePaymentTypes(int PaymentId)
        {
            return objDAPaymentTypes.DeletePaymentTypes(PaymentId);
        }
    }
}
