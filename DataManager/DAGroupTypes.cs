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
    public class DAGroupTypes:DataUtilities
    {
        public int InsUpdateGroupTypes(EMGroupTypes objGroupTypes)
        {
            Hashtable htparams = new Hashtable
                                     {
                                         {"@Id",objGroupTypes.Id},
                                         {"@Name",objGroupTypes.Name},
                                         {"@Code",objGroupTypes.Code},
                                         {"@GroupType",objGroupTypes.GroupType},
                                         {"@CompanyId",objGroupTypes.CompanyId},
                                         {"@BranchId",objGroupTypes.BranchId},
                                         {"@CreatedBy",objGroupTypes.CreatedBy},
                                        
                                     };
            int IsSuccess = ExecuteNonQuery("GroupMaster_InsUpdate", htparams);
            return IsSuccess;
        }

        public int DeleteGroupTypes(int Id)
        {
            Hashtable htparams = new Hashtable
                                     {
                                         {"@Id",Id},
                                     };
          return  ExecuteNonQuery("GroupMaster_Delete", htparams);
        }
        public DataSet GetGroupTypes(int Id)
        {
            Hashtable htparams = new Hashtable
                                      {
                                          {"@Id",Id},
                                      };
            return ExecuteDataSet("GroupMaster_Get", htparams);
        }
    }
}
