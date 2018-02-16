using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityManager;
using DataManager;
using System.Data;

namespace BusinessManager
{
    
    public class BAGroupTypes
    {
        DAGroupTypes objDAGroupTypes = new DAGroupTypes();
        public int InsUpdateGroupTypes(EMGroupTypes objGroupTypes)
        {
            return objDAGroupTypes.InsUpdateGroupTypes(objGroupTypes);
        }

        public int DeleteGroupTypes(int Id)
        {
            return objDAGroupTypes.DeleteGroupTypes(Id);
        }
        public DataSet GetGroupTypes(int Id)
        {
            return objDAGroupTypes.GetGroupTypes(Id);
        }
    }
}
