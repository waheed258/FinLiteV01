using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataManager;
using System.Data;

namespace BusinessManager
{
    public class BAImportedTickets
    {
        DAImportedTickets objDaImportedTickets = new DAImportedTickets();

        public DataSet GetImportTickets(int invStatusId)
        {
            return objDaImportedTickets.GetImportTickets(invStatusId);
        }
    }
}
