using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using EntityManager;

namespace DataManager
{
   public class DAContactLog:DataUtilities
    {
       public int InsUpdContactLog(EMContactLog objEMConLog)
       {
           Hashtable htparams = new Hashtable{
                                            {"@LogId",objEMConLog.LogId},
                                            {"@LogKey",objEMConLog.LogKey},
                                            {"@LogDescription",objEMConLog.LogDescription},
                                            {"@CreatedBy",objEMConLog.CreatedBy},
           };
           return ExecuteNonQuery("ContactLog_InsertUpdate", htparams);
       }

       public DataSet GetContactLog(int LogId)
       {
           Hashtable htparams = new Hashtable{
                                           {"@LogId",LogId},
           };
           return ExecuteDataSet("ContactLog_Get", htparams);
       }

       public int DeleteContactLog(int LogId)
       {
           Hashtable htparams = new Hashtable{
                                            {"@LogId",LogId},
           };
           return ExecuteNonQuery("ContactLog_Delete", htparams);
       }
    }
}
