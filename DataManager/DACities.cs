using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using EntityManager;

namespace DataManager
{
    public class DACities:DataUtilities
    {
        public int InsUpdateCities(EMCitiesMaster objEmCities)
        {
            Hashtable htparams = new Hashtable{
                                            {"@Id",objEmCities.Id},
                                            {"@Name",objEmCities.Name},
                                            {"@CountryId",objEmCities.CountryId},
                                            {"@state_id",objEmCities.state_id},
                                            {"@CityKey",objEmCities.CityKey},
                                            {"@CityTimezoneUtc",objEmCities.CityTimezoneUtc},
                                            {"@CreatedBy",objEmCities.CreatedBy},
            };
            return ExecuteNonQuery("CitiesMaster_InsertUpdate", htparams);
        }

        public DataSet GetCities(int Id)
        {
            Hashtable htparams = new Hashtable{
                                            {"@Id",Id},
            };
            return ExecuteDataSet("CitiesMaster_Get", htparams);
        }
        public int DeleteCities(int Id)
        {
            Hashtable htparams = new Hashtable{
                                            {"@Id",Id},
            };
            return ExecuteNonQuery("CitiesMaster_Delete", htparams);
        }
    }
}
