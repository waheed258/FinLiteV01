using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using EntityManager;

namespace DataManager
{
    public class DAState:DataUtilities
    {
        public int InsUpdState(EMState objState)
        {
            Hashtable htparams = new Hashtable
            {
              {"@Id",objState.Id},
              {"@StateKey",objState.Key},
              {"@Name",objState.Name},
              {"@Country_id",objState.Country},
              {"@CreatedBy",objState.CreatedBy}
               
           };
            int IsSuccess = ExecuteNonQuery("StatesMaster_InsertUpdate", htparams);
            return IsSuccess;
        }
        public DataSet GetState(int StateId)
        {
            Hashtable htparams = new Hashtable
           {
             {"@Id",StateId}
           };

            return ExecuteDataSet("StatesMaster_Get", htparams);

        }
        public int DeleteState(int StateId)
        {
            Hashtable htparams = new Hashtable
           {
             {"@Id",StateId}
           };

            return ExecuteNonQuery("StatesMaster_Delete", htparams);

        }
    }
}
