using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityManager;
using System.Data;
using DataManager;

namespace BusinessManager
{
    public class BAState
    {
        DAState objDAState = new DAState();
        public int InsUpdState(EMState objState)
        {
            return objDAState.InsUpdState(objState);
        }
        public DataSet GetState(int StateId)
        {
            return objDAState.GetState(StateId);
        }
        public int DeleteState(int StateId)
        {
            return objDAState.DeleteState(StateId);
        }

    }
}
