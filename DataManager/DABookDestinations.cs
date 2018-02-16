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
    public class DABookDestinations:DataUtilities
    {
        public int InsUpdBookDestinations(EMBookDestinations objBookDestinations)
        {
            Hashtable htparams = new Hashtable
                                      {
                                          {"@BookDestId",objBookDestinations.BookDestId},
                                          {"@BookDestKey",objBookDestinations.BookDestKey},
                                          {"@BookDestName",objBookDestinations.BookDestName},
                                          {"@BookDestStatus",objBookDestinations.BookDestStatus},
                                          {"@CreatedBy",objBookDestinations.CreatedBy}
                                      };
            int IsSuccess = ExecuteNonQuery("BookingDestination_InsertUpdate", htparams);
            return IsSuccess;
        }

         public int DeleteBookDestinations(int BookDestId)
        {
            Hashtable htparams = new Hashtable
                                       {
                                           {"@BookDestId",BookDestId},
                                       };
            return ExecuteNonQuery("BookingDestination_Delete", htparams);
         }
        public DataSet GetBookDestinations(int BookDestId)
        {
            Hashtable htparams = new Hashtable
                                      {
                                          {"@BookDestId",BookDestId},
                                      };
            return ExecuteDataSet("BookingDestination_Get", htparams);
        }
    }
}
