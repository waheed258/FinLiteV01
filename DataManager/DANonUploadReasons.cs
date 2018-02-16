using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityManager;
using System.Collections;
using System.Data;

namespace DataManager
{
    public class DANonUploadReasons:DataUtilities
    {

        public int InsUpdNonUploadReasons(EMNonUploadReasons objUploadReasons)
        {
            Hashtable htparams = new Hashtable    
                                        {
                                              {"@NonUploadId",objUploadReasons.NonUploadId},
                                              {"@NonUploadKey",objUploadReasons.NonUploadKey},
                                              {"@NonUploadDesc",objUploadReasons.NonUploadDesc},
                                              {"@CreatedBy",objUploadReasons.CreatedBy}
                                        };
            int IsSuccess = ExecuteNonQuery("CCNonUploadreasons_InsUpdate", htparams);
            return IsSuccess;
        }

        public  DataSet GetNonUploadReasons(int UploadId)
        {
            Hashtable htparams = new Hashtable
                                      {
                                              {"@NonUploadId",UploadId}
                                      };
            return ExecuteDataSet("CCNonUploadreasons_Get", htparams);
        }

        public int DeleteUpdNonUploadReasons(int UploadId)
        {
            Hashtable htparams = new Hashtable
                                      {
                                              {"@NonUploadId",UploadId}
                                      };
            return ExecuteNonQuery("CCNonUploadReasons_Delete", htparams);
        }
    }
}
