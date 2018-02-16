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
    public class BALFilereading
    {

        DALFilereading objDAFilereading = new DALFilereading();
        public int InsertFilereading(EMFilereading objEMFlereading)
        {
            return objDAFilereading.InsertFilereading(objEMFlereading);
        }

        public DataSet GetFilesContent(string Type)
        {
            return objDAFilereading.GetFilesContent(Type);
        }
    }
}
