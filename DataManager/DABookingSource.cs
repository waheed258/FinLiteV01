using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityManager;
using System.Data;
using System.Data.SqlClient;
namespace DataManager
{
    public class DABookingSource:DataUtilities
    {
        public int InsUpdBookingSource(EMBookingSource objEMBookSource)
        {
            Hashtable htparams = new Hashtable
            {
               {"@BookingId",objEMBookSource.Id},
               {"@BookingKey",objEMBookSource.Key},
               {"@BookingName",objEMBookSource.Desc},
               {"@IsActive",objEMBookSource.Deactivate},
               {"@CreatedBy",objEMBookSource.CreatedBy}
           };
            int IsSuccess = ExecuteNonQuery("BookingSource_InsertUpdate", htparams);
            return IsSuccess;
        }
        public DataSet GetBookingSource(int BookingId)
        {
            Hashtable htparams = new Hashtable
           {
             {"@BookingId",BookingId}
           };

            return ExecuteDataSet("BookingSource_Get", htparams);

        }
        public int DeleteBookingSource(int BookingId)
        {
            Hashtable htparams = new Hashtable
           {
             {"@BookingId",BookingId}
           };

            return ExecuteNonQuery("BookingSource_Delete", htparams);

        }

    }
}
