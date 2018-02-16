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
    public class DACountries:DataUtilities
    {
        public int InsUpdCountries(EMCountries objCountries)
        {
            Hashtable htparams = new Hashtable
                                      {
                                          {"@Id",objCountries.Id},
                                          {"@Name",objCountries.Name},
                                          {"@CountryKey",objCountries.CountryKey},
                                          {"@Continent",objCountries.Continent},
                                          {"@TimeZoneUTC",objCountries.TimeZoneUTC},
                                          {"@DialCode",objCountries.DialCode},
                                          {"@VATOrGSTRate",objCountries.VATOrGSTRate},
                                          {"@TravelCategory",objCountries.TravelCategory},
                                          {"@CreatedBy",objCountries.CreatedBy}
                                      };
            int IsSuccess = ExecuteNonQuery("CountriesMaster_InsUpdate", htparams);
            return IsSuccess;
        }

        public DataSet GetCountries(int Id)
        {
            Hashtable htparams = new Hashtable
                                    {
                                        {"@Id",Id}
                                    };
            return ExecuteDataSet("CountriesMaster_Get",htparams);
        }

        public int DeleteCountries(int Id)
        {
            Hashtable htparams = new Hashtable
                                      {
                                            {"@Id",Id}
                                    };
            return ExecuteNonQuery("CountriesMaster_Delete", htparams);
        }
    }
}
