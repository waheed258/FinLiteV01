using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataManager
{
  public  class DAAutoInvoice : DataUtilities
    {

      public DataSet GetTicketLevelData(int T5ID)
        {
            Hashtable htparams = new Hashtable
           {
             {"@T5ID",T5ID}
           };

            return ExecuteDataSet("TextFileTicketLevel_Data", htparams);

        }

      public DataSet GetClientsNames(int T5ID)
      {
          Hashtable htparams = new Hashtable
           {
             {"@T5ID",T5ID}
           };

          return ExecuteDataSet("t5_booking_header_getClientsDetails", htparams);

      }

      public DataSet GetAirSupllierId(string AirSupName)
      {
          Hashtable htparams = new Hashtable
           {
             {"@AirSupName",AirSupName}
           };

          return ExecuteDataSet("AirsupplierId_GetTextFiles", htparams);

      }
      public DataSet GetChartedAccount(string chartedaccount)
      {
          Hashtable htparams = new Hashtable
           {
             {"@chartedaccount",chartedaccount}
           };

          return ExecuteDataSet("ChartedAccount_GetTextFiles", htparams);

      }
    }
}
