using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityManager;
using System.Data;
using System.Data.SqlClient;
namespace DataManager
{
    public class DATicketType : DataUtilities
    {
        public int InsUpdTicketType(EMTicketType objEMTicketType)
        {
            Hashtable htparams = new Hashtable
              {
                  {"@TId",objEMTicketType.TId},
                  {"@TKey",objEMTicketType.TKey},
                  {"@TDesc",objEMTicketType.TDesc},
                  {"@CreatedBy",objEMTicketType.CreatedBy}
              };
            int IsSuccess = ExecuteNonQuery("TicketType_InsertUpdate", htparams);
            return IsSuccess;
        }
        public DataSet GetTicketType(int TId)
        {
            Hashtable htparams = new Hashtable
           {
             {"@TId",TId}
           };

            return ExecuteDataSet("TicketType_Get", htparams);

        }
        public int DeleteTicketType(int TId)
        {
            Hashtable htparams = new Hashtable
           {
             {"@TId",TId}
           };

            return ExecuteNonQuery("TicketType_Delete", htparams);

        }
    }
}
