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
    public class BAAirport
    {
        DAAirport objDAAirport = new DAAirport();
        public int InsUpdtAirport(EMAirport objEMAirport)
        {
            return objDAAirport.InsUpdtAirport(objEMAirport);
        }
        public DataSet GetAirport(int AirportId)
        {
            return objDAAirport.GetAirport(AirportId);
        }

        public int DeleteAirport(int AirportId)
        {
            return objDAAirport.DeleteAirport(AirportId);
        }
    }
}
