using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Data;
using EntityManager;

namespace DataManager
{
    public class DAAirport:DataUtilities
    {
        public int InsUpdtAirport(EMAirport objEMAirport)
        {
            Hashtable htparams = new Hashtable{
                                            {"@AirportId",objEMAirport.AirportId},
                                            {"@Deactivate",objEMAirport.Deactivate},
                                            {"@AirKey",objEMAirport.AirKey},
                                            {"@AirportName",objEMAirport.AirportName},
                                            {"@AirCity",objEMAirport.AirCity},
                                            {"@AirState",objEMAirport.AirState},
                                            {"@AirCountry",objEMAirport.AirCountry},
                                            {"@CountryDetails",objEMAirport.CountryDetails},
                                            {"@CreatedBy",objEMAirport.CreatedBy},

            };
            return ExecuteNonQuery("Airports_InsertUpdate", htparams);
        }

        public DataSet GetAirport(int AirportId)
        {
            Hashtable htparams = new Hashtable{
                                            {"@AirportId",AirportId},

            };
            return ExecuteDataSet("Airports_Get", htparams);
        }

        public int DeleteAirport(int AirportId)
        {
            Hashtable htparams = new Hashtable{
                                            {"@AirportId",AirportId},

            };
            return ExecuteNonQuery("Airports_Delete", htparams);
        }
    }
}
