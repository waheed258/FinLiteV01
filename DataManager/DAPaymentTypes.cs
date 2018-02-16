using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityManager;
using System.Collections;
using System.Data;

namespace DataManager
{
    public class DAPaymentTypes:DataUtilities
    {
        public int InsUpdPaymentTypes(EMPaymentTypes objPaymentTypes)
        {
            Hashtable htparams = new Hashtable
                                            {
                                                {"@PaymentId",objPaymentTypes.PaymentId},
                                                {"@PaymentName",objPaymentTypes.PaymentName},
                                                {"@PaymentCode",objPaymentTypes.PaymentCode},
                                                {"@IsDefaultPayment",objPaymentTypes.IsDefaultPayment},
                                                {"@CreatedBy",objPaymentTypes.CreatedBy},
                                                
                                          };

            int IsSuccess = ExecuteNonQuery("PaymentTypes_InsertUpdate", htparams);
            return IsSuccess;
        }

        public DataSet GetPaymentTypes(int PaymentId)
        {
            Hashtable htparams = new Hashtable
                                              {
                                                 {"@PaymentId",PaymentId},
                                              };
            return ExecuteDataSet("PaymentTypes_Get", htparams);
        }

        public int DeletePaymentTypes(int PaymentId)
        {
            Hashtable htparams = new Hashtable
                                              {
                                                 {"@PaymentId",PaymentId},
                                              };
            return ExecuteNonQuery("PaymentTypes_Delete", htparams);
        }
    }
}
