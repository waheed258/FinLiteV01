using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityManager;
using DataManager;
using System.Data;

namespace BusinessManager
{
    public  class BAConsultant
    {
        DAConsultant objDAConsultant = new DAConsultant();
        public int InsUpdConsultant(EMConsultant objConsultant)
        {
            return objDAConsultant.InsUpdConsultant(objConsultant);
        }

        public DataSet Get_consultantAccCode()
        {
            return objDAConsultant.Get_ConsultantAccCode();
        }
        public DataSet GetConsultant(int ConsultantId)
        {
            return objDAConsultant.GetConsultant(ConsultantId);
        }

        public int DeleteConsultant(int ConsultantId)
        {
            return objDAConsultant.DeleteConsultant(ConsultantId);
        }

        public int InsUpdChartAccounts(EMConsultant objConsultant)
        {
            return objDAConsultant.InsUpdChartAccounts(objConsultant);
        }
        public int UpdateChartAccounts(EMConsultant objConsultant)
        {
            return objDAConsultant.UpdChartAccounts(objConsultant);
        }
    }
}
