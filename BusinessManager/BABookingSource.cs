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
    public class BABookingSource
    {
        DABookingSource objDABookSource = new DABookingSource();
        public int InsUpdBookingSource(EMBookingSource objEMBookSource)
        {
            return objDABookSource.InsUpdBookingSource(objEMBookSource);
        }
        public DataSet GetBookingSource(int BookingId)
        {

            return objDABookSource.GetBookingSource(BookingId);

        }
        public int DeleteBookingSource(int BookingId)
        {

            return objDABookSource.DeleteBookingSource(BookingId);

        }
    }
}
