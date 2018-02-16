using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Collections;
using System.Text;
using System.Data.SqlClient;
using EntityManager;

namespace DataManager
{
    public class DACashBook:DataUtilities
    {
        public int InsUpdCashBook(EMCashBook objEMCash)
        {
            Hashtable htparams = new Hashtable{
                                            {"@CashBookId",objEMCash.CashBookId},
                                            {"@CashBookKey",objEMCash.CashBookKey},
                                            {"@CashDescription",objEMCash.CashDescription},
                                            {"@Deactivate",objEMCash.Deactivate},
                                            {"@DefaultAction",objEMCash.DefaultAction},
                                            {"@GICode",objEMCash.GICode},
                                            {"@ReferenceFormat",objEMCash.ReferenceFormat},
                                            {"@VatCodes",objEMCash.VatCodes},
                                            {"@CreatedBy",objEMCash.CreatedBy},
            };
            return ExecuteNonQuery("CashBookTypes_InsertUpdate", htparams);
        }

        public DataSet GetCashBook(int CashBookId)
        {
            Hashtable htparams = new Hashtable{
                                            {"@CashBookId",CashBookId},
            };
            return ExecuteDataSet("CashBookTypes_Get", htparams);
        }

        public int DeleteCashBook(int CashBookId)
        {
            Hashtable htparams = new Hashtable{
                                            {"@CashBookId",CashBookId},
            };
            return ExecuteNonQuery("CashBookTypes_Delete", htparams);
        }
    }
}
