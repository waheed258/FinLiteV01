using DataManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManager
{
   public class BAAutoInvoice
    {
       DAAutoInvoice objDAAutoInvoice = new DAAutoInvoice();
       public DataSet GetTicketLevlData(int T5ID)
       {
           return objDAAutoInvoice.GetTicketLevelData(T5ID);
       }


       public DataSet GetClientsNames(int T5ID)
       {
           return objDAAutoInvoice.GetClientsNames(T5ID);
       }

       public DataSet GetAirSupllierId(string SupplierName)
       {
           return objDAAutoInvoice.GetAirSupllierId(SupplierName);
       }

       public DataSet GetChartedAccount(string chartedaccount)
       {
           return objDAAutoInvoice.GetChartedAccount(chartedaccount);
       }
    }
}
