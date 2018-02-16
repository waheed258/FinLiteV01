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
    public class DACarbonCorfficients:DataUtilities
    {
        public int InsUpdCarbonCoefficients(EMCarbonCoefficients objCarbon)

        {
            Hashtable htparams = new Hashtable
                                     {
                                         {"@CarbonId",objCarbon.CarbonId},
                                         {"@CarbonKey",objCarbon.CarbonKey},
                                         {"@CarbonDesc",objCarbon.CarbonDesc},
                                         {"@StartDate",objCarbon.StartDate},
                                         {"@EndDate",objCarbon.EndDate},
                                         {"@Cofficient",objCarbon.Cofficient},
                                         {"@CreatedBy",objCarbon.CreatedBy}
                                     };
            int IsSuccess = ExecuteNonQuery("CarbonCoefficients_InsUpdate", htparams);
            return IsSuccess;

        }


        public DataSet GetCarbonCoefficients(int CarbonId)
        {
            Hashtable htparams = new Hashtable
                                      {
                                          {"@CarbonId",CarbonId}
                                      };
            return ExecuteDataSet("CarbonCoefficients_Get", htparams);
        }

        public int DeleteCarbonCoefficients(int CarbonId)
        {
            Hashtable htparams = new Hashtable
                                     {
                                          {"@CarbonId",CarbonId}

                                     };
            return ExecuteNonQuery("CarbonCoefficients_Delete", htparams);
        }
    }
}
