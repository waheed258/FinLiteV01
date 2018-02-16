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
    
    public class BATemplateCategory
    {
        DATemplateCategory objDATemplateCategory = new DATemplateCategory();
        public int InsUpdTemplateCategory(EMTemplateCategory objEMTempCategory)
        {
            return objDATemplateCategory.InsUpdTemplateCategory(objEMTempCategory);
        }

        public DataSet GetTemplateCategory(int Id)
        {
            return objDATemplateCategory.GetTemplateCategory(Id);
        }

        public int DeleteTemplateCategory(int Id)
        {
            return objDATemplateCategory.DeleteTemplateCategory(Id);
        }
    }
}
