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
   public class BACities
    {
       DACities objDACities = new DACities();
       public int InsUpdateCities(EMCitiesMaster objEmCities)
       {
           return objDACities.InsUpdateCities(objEmCities);
       }

       public DataSet GetCities(int Id)
       {
           return objDACities.GetCities(Id);
       }
       public int DeleteCities(int Id)
       {
           return objDACities.DeleteCities(Id);
       }
    }
}
