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
    public class DATemplateCategory:DataUtilities
    {
         public int InsUpdTemplateCategory(EMTemplateCategory objEMTempCategory)
        {
            Hashtable htparams = new Hashtable
            {
                {"@Id",objEMTempCategory.Id},
                {"@TC_Key",objEMTempCategory.Key},
                {"@Description",objEMTempCategory.Description},
                {"@CreatedBy",objEMTempCategory.CreatedBy}
            };
            return ExecuteNonQuery("TemplateCategory_InsertUpdate", htparams);
        }


         public DataSet GetTemplateCategory(int Id)
         {
             Hashtable htparams = new Hashtable{
                                           {"@Id",Id},
           };
             return ExecuteDataSet("TemplateCategory_Get", htparams);
         }

         public int DeleteTemplateCategory(int Id)
         {
             Hashtable htparams = new Hashtable{
                                            {"@Id",Id},
           };
             return ExecuteNonQuery("TemplateCategory_Delete", htparams);
         }
    }
}
