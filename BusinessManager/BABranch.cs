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
    public class BABranch
    {
        DABranch objDABranch = new DABranch();
        public int InsUpdBranch(EMBranch objbranch)
        {
            return objDABranch.InsUpdBranch(objbranch);
        }
        public DataSet GetBranch(int BranchId)
        {
            return objDABranch.GetBranch(BranchId);
        }
        public int DeleteBranch(int BranchId)
        {
            return objDABranch.DeleteBranch(BranchId);
        }
    }
}
