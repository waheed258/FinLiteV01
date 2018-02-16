using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using EntityManager;
namespace DataManager
{
    public class DAForeignCurrency:DataUtilities
    {
        public int InsertUpdateForeignCurrency(EMForeignCurrency objEMForeignCurrency)
        {
            Hashtable htparams = new Hashtable
            {
               {"@Id",objEMForeignCurrency.Id},
               {"@FC_Key",objEMForeignCurrency.Key},
               {"@Description",objEMForeignCurrency.Description},
               {"@Action",objEMForeignCurrency.Action},
               {"@Template",objEMForeignCurrency.Template},
               {"@DeActivate",objEMForeignCurrency.Deactivate},
               {"@CreatedBy",objEMForeignCurrency.CreatedBy}
           };
            int IsSuccess = ExecuteNonQuery("ForeignCurrencies_InsertUpdate", htparams);
            return IsSuccess;
        }
        public DataSet GetForeignCurrency(int Id)
        {
            Hashtable htparams = new Hashtable
           {
             {"@Id",Id}
           };

            return ExecuteDataSet("ForeignCurrencies_Get", htparams);

        }
        public int DeleteForeignCurrency(int Id)
        {
            Hashtable htparams = new Hashtable
           {
             {"@Id",Id}
           };

            return ExecuteNonQuery("ForeignCurrencies_Delete", htparams);

        }

    }
}
