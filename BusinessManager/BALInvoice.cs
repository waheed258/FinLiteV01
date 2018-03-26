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
    
    public class BALInvoice
    {

        private DALInvoice _objDALInvoice = new DALInvoice();
        public int InsertInvoice(EmInvoice objEmInvoice)
        {
            return _objDALInvoice.InsertInvoice(objEmInvoice);
        }
        public int updateInvId(int InvId, string UniqueCode, string invDocumentNo,int T5Id)
        {
            return _objDALInvoice.UpdateInvId(InvId, UniqueCode, invDocumentNo,T5Id);
        }
        public DataSet getInvoiceLines(string TempUniqCode)
        {
            return _objDALInvoice.GetInvoiceLines(TempUniqCode);
        }
        public DataSet getInvoiceLinesCount(string TempUniqCode)
        {
            return _objDALInvoice.GetInvoiceLinesCount(TempUniqCode);
        }
        public DataSet GetInvoiceList()
        {
            return _objDALInvoice.Get_InvoiceList();

        }

        public DataSet Get_printstyle()
        {
            return _objDALInvoice.Get_printstyle();
        }

        public DataSet Get_ClientmessageandNote(int clientId)
        {
            return _objDALInvoice.Get_ClientmessageandNote(clientId);

        }

        // Set The Data PDF Details With sending to Email 




        public DataSet GetServiceFeeMergeValue(int InvId, string TempuniqCode)
        {
            return _objDALInvoice.GetServiceFeeMergeValue(InvId, TempuniqCode);
        }
        public DataSet GetPdfDetails(int InvId, int companyid)
        {
            return _objDALInvoice.GetPdfDetails(InvId, companyid);
        }

        public DataSet GetInvoiceACAnalasis(int TempuniqID)
        {
            return _objDALInvoice.Get_InvoiceACAnalysis(TempuniqID);

        }
        public DataSet DraftPdfDetails(string TempuniqCode, int companyid )
        {
            return _objDALInvoice.DraftPdfDetails(TempuniqCode, companyid);
        }
        public DataSet GetInvoiceDetailsByClientAndStatus(int ClientType, int ClientId, int Status)
        {
            return _objDALInvoice.GetInvoiceDetailsByClientAndStatus(ClientType, ClientId, Status);
        }
        //Edit for Invoice
        public DataSet GetInvoice(int InvId)
        {
            return _objDALInvoice.GetInvoice(InvId);
        }
        //Invoice Editer Grid
        public DataSet GetInvoiceLinesEdit(int InvId)
        {
            return _objDALInvoice.GetInvoiceLinesEdit(InvId);
        }
        // Invoice Editer Grid Count
        public DataSet GetInvoiceLinesCountEdit(int InvId)
        {
            return _objDALInvoice.GetInvoiceLinesCountEdit(InvId);
        }

        public DataSet GetInvoiceDetailsBySUpplAndStatus(int SupplTypeId, string suppltypename, int categoryId, int Status)
        {
            return _objDALInvoice.GetInvoiceDetailsBySUpplAndStatus(SupplTypeId,suppltypename, categoryId, Status);
        }


        public DataSet GetInvocieDetailsBySupplierLevel(int SupplTypeId, string suppltypename,int Status)
        {
            return _objDALInvoice.GetInvocieDetailsBySupplierLevel(SupplTypeId, suppltypename, Status);
        }


        public DataSet GetSupplierOpeningAmount(int TicketId, int airinvid, int Status, string category)
        {
            return _objDALInvoice.GetSupplierOpeningAmount(TicketId, airinvid, Status, category);
        }


        public DataSet GetAccAnalysisData(int invid)
        {
            return _objDALInvoice.GetAccAnalysisData(invid);
        }



        public DataSet GetAccAnalysisDetails(int invid)
        {
            return _objDALInvoice.GetAccAnalysisDetails(invid);
        }

        public DataSet GetTicket(int TicketNo, string TicketType)
        {
            return _objDALInvoice.GetTicket(TicketNo, TicketType);
        }

        public DataSet GetTicketType(int TId)
        {
            return _objDALInvoice.GetTicketType(TId);
        }

        //  Insert Credit Note


        public int CreditNote_Insert(int Invid, int TicketId, string Type, decimal RefundAmt)
        {
            return _objDALInvoice.CreditNote_Insert(Invid, TicketId, Type, RefundAmt);
        }


        public DataSet Check_Payment_Deposit()
        {
            return _objDALInvoice.Check_Payment_Deposit();
        }

        public int DeleteInvoice(int InvoiceId)
        {
            return _objDALInvoice.DeleteInvoice(InvoiceId);
        }
    }
}
