using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using EntityManager;
using DataManager;
using System.Data.SqlClient;

namespace BusinessManager
{
    public class BALProformaInvoice
    {
        //PF :- Proforma
        private DALProformaInvoice _objDALPFInvoice = new DALProformaInvoice();

        public int InsertPFInvoice(EMProformaInvoice objEmPFInvoice)
        {
            return _objDALPFInvoice.InsertProformaInvoice(objEmPFInvoice);
        }
        public int UpdatePFInvId(int PFInvId, string UniqueCode)
        {
            return _objDALPFInvoice.UpdateProformaInvId(PFInvId, UniqueCode);
        }
        public DataSet GetPFInvoiceList()
        {
            return _objDALPFInvoice.Get_PFInvoiceList();

        }
        public DataSet GetPFPdfDetails(int PFInvId, int companyid)
        {
            return _objDALPFInvoice.GetPFPdfDetails(PFInvId, companyid);
        }
        public DataSet GetPFServiceFeeMergeValue(int PFInvId, string TempuniqCode)
        {
            return _objDALPFInvoice.GetPFServiceFeeMergeValue(PFInvId, TempuniqCode);
        }
        public DataSet PFDraftPdfDetails(string TempuniqCode, int companyid)
        {
            return _objDALPFInvoice.PFDraftPdfDetails(TempuniqCode, companyid);
        }
    
    }
}
