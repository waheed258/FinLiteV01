using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using EntityManager;
using DataManager;

namespace BusinessManager
{
   public class BAContactLog
    {
       DAContactLog objDAConLog = new DAContactLog();
       public int InsUpdContactLog(EMContactLog objEMConLog)
       {
           return objDAConLog.InsUpdContactLog(objEMConLog);
       }

       public DataSet GetContactLog(int LogId)
       {
           return objDAConLog.GetContactLog(LogId);
       }

       public int DeleteContactLog(int LogId)
       {
           return objDAConLog.DeleteContactLog(LogId);
       }
    }
}
