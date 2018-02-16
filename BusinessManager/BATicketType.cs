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
    public class BATicketType
    {
        DATicketType objDATicketType = new DATicketType();
        public int InsUpdTicketType(EMTicketType objEMTicketType)
        {
            return objDATicketType.InsUpdTicketType(objEMTicketType);
        }
        public DataSet GetTicketType(int TId)
        {
            return objDATicketType.GetTicketType(TId);
        }
        public int DeleteTicketType(int TId)
        {
            return objDATicketType.DeleteTicketType(TId);
        }
    }
}
