using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityManager;

namespace DataManager
{
    public class DAImportedTickets : DataUtilities
    {

        public DataSet GetImportTickets(int invStatusId)
       {
           Hashtable htparams = new Hashtable{
                                            {"@invStatusId",invStatusId},

            };
           return ExecuteDataSet("ImportTickets_Get", htparams);
       }
    }
}
