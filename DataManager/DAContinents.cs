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
    public class DAContinents : DataUtilities
    {
        public int IsuUpdContinents(EMContinents objContinents)
        {
            Hashtable htparams=new Hashtable
                                        {
                                            {"@ContinentId",objContinents.ContinentId},
                                            {"@ContinentKey",objContinents.ContinentKey},
                                            {"@ContinentDesc",objContinents.ContinentDesc},
                                            {"@CreatedBy",objContinents.CreatedBy}
                                        };
            int IsSuccess = ExecuteNonQuery("Continents_InsUpdate", htparams);
            return IsSuccess;
        }

        public int DeleteContinents(int ContinentId)
        {
            Hashtable htparams = new Hashtable
                                        {
                                             {"@ContinentId",ContinentId}
                                        };
            return ExecuteNonQuery("Continents_Delete", htparams);
        }
        public DataSet GetContinents(int ContinentId)
        {
            Hashtable htparams = new Hashtable
                                       {
                                           {"@ContinentId",ContinentId}
                                       };
            return ExecuteDataSet("Continents_Get", htparams);
        }
    }
}
