using EntityManager;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataManager
{
    public class DALFilereading : DataUtilities
    {
        public int InsertFilereading(EMFilereading objfilereading)
        {
            Hashtable htparams = new Hashtable{
                                            {"@FileId",objfilereading.FileId},
                                            {"@FileName",objfilereading.FileName},
                                            {"@FileContent",objfilereading.FileContent},
                                        
                                            {"@CreatedBy",objfilereading.CreatedBy},

            };
            return ExecuteNonQuery("FileReading_Insert", htparams);
        }

        public DataSet GetFilesContent(string Type)
        {
            Hashtable htparams = new Hashtable{
                                            {"@Type",Type},
             };
            return ExecuteDataSet("FileReading_GetContent",htparams);
        }
    }
}
