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
     public class BANonUploadReasons
    {
         DANonUploadReasons objDAUploadReasons = new DANonUploadReasons();
         public int InsUpdNonUploadReasons(EMNonUploadReasons objUploadReasons)
         {
             return objDAUploadReasons.InsUpdNonUploadReasons(objUploadReasons);
         }
         public DataSet GetNonUploadReasons(int UploadId)
         {
             return objDAUploadReasons.GetNonUploadReasons(UploadId);
         }

         public int DeleteUpdNonUploadReasons(int UploadId)
         {
             return objDAUploadReasons.DeleteUpdNonUploadReasons(UploadId);
         }

    }
}
