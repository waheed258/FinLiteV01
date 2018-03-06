using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataManager;
using EntityManager;
using System.Data;
namespace BusinessManager
{
    public class BALAirTicket
    {
        private DALAirTicket _objDALAirTicket = new DALAirTicket();  
        public int InsertAirTicket(EmAirTicket objAirticket)
        {
            return _objDALAirTicket.InsertAirTicket(objAirticket);

        }

       
        public int InsertAirticketRouting(EmAirticketRouting objAirticketRouting)
        {
            return _objDALAirTicket.InsertAirticketRouting(objAirticketRouting);

        }

        public int updateAirticketId(int TicketId, string UniqueCode)
        {
            return _objDALAirTicket.UpdateAirTicketId(TicketId, UniqueCode);
        }


        public int InsertSupplier(EmAirTicket objAirticket)
        {
            return _objDALAirTicket.InsertSupplier(objAirticket);

        }


        public DataSet AllCommissionTypes_GetComPercentage()
        {
            return _objDALAirTicket.ExecuteDataSet("AllCommissionTypes_GetComPercentage");
        }

    }
}
