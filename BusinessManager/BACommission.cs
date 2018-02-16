using DataManager;
using EntityManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManager
{
  public  class BACommission
    {
      DACommission objDACommission = new DACommission();
        public int InsertUpdateCommission(EMCommission objcommission)
        {
            return objDACommission.InsertUpdateCommission(objcommission);
        }
        public DataSet GetCommiChartedAccId(string chartedaccname)
        {
            return objDACommission.GetCommiChartedAccId(chartedaccname);
        }
    }
}
