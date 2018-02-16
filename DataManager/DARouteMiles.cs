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

    public class DARouteMiles:DataUtilities
    {
        public int InsUpdRouteMiles(EMRouteMiles objEMRouteMiles)
        {
            Hashtable htparams = new Hashtable
             {
                {"@RMId",objEMRouteMiles.Id},
                {"@RMkey",objEMRouteMiles.Key},
                {"@RMDeactivate",objEMRouteMiles.Deactivate},
                {"@RMRouting",objEMRouteMiles.Routing},
                {"@RMMiles",objEMRouteMiles.Miles},
                {"@RMDuration",objEMRouteMiles.Duration},
                {"@CreatedBy",objEMRouteMiles.CreatedBy}
             };
            int IsSuccess = ExecuteNonQuery("RouteMiles_InsertUpdate", htparams);
           return IsSuccess;
       }
       public DataSet GetRouteMiles(int RMId)
       {
           Hashtable htparams = new Hashtable
           {
             {"@RMId",RMId}
           };

           return ExecuteDataSet("RouteMiles_Get", htparams);

       }
       public int DeleteRouteMiles(int RMId)
       {
           Hashtable htparams = new Hashtable
           {
             {"@RMId",RMId}
           };

           return ExecuteNonQuery("RouteMiles_Delete", htparams);

       }
    }
}
