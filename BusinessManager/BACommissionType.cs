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
    public class BACommissionType
    {
       DACommissionType objDACommType=new DACommissionType();
       public int InsUpdCommissionType(EMCommissionType objCommType)
       {
           return objDACommType.InsUpdCommissionType(objCommType);
       }
       public DataSet GetCommissionType(int ComId)
       {
           return objDACommType.GetCommissionType(ComId);
       }
       public int DeleteCommissionType(int ComId)
       {
           return objDACommType.DeleteCommissionType(ComId);
       }
       public DataSet GetCategory()
       {
           return objDACommType.GetCategory();
       }
       public DataSet GetVAT(int vatId)
       {
           return objDACommType.GetVAT(vatId);
       }
       public DataSet GetLandSubCategory()
       {
           return objDACommType.GetLandSubCategory();
       }
   
    }
}
