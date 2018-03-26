using EntityManager;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataManager
{
    public class DALInvoice : DataUtilities
    {
        public int InsertInvoice(EmInvoice objInvoice)
        {
            Hashtable htParams = new Hashtable
                                     {
                                         {"@IsCreditNote",objInvoice.IsCreditNote},
                                         {"@InvId",objInvoice.InvId},
                                         {"@InvDate",objInvoice.InvDate},
                                         {"@ClientTypeId",objInvoice.ClientTypeId},
                                         {"@ClientNameId",objInvoice.ClientNameId}, 
                                         {"@ConsultantId",objInvoice.ConsultantId},
                                         {"@InvOrder",objInvoice.InvOrder},                                                                             
                                         {"@BookSourceId",objInvoice.BookSourceId},                                        
                                         {"@BookDestinationId",objInvoice.BookDestinationId},
                                         {"@BookingNo",objInvoice.BookingNo},
                                         {"@Message",objInvoice.Message},
                                         {"@MessageType",objInvoice.MessageType},
                                         {"@PrintStyleId",objInvoice.PrintStyleId},
                                         {"@AirTotal",objInvoice.AirTotal},
                                         {"@InvoiceTotal",objInvoice.InvoiceTotal},
                                         {"@LandTotal",objInvoice.LandTotal},
                                         {"@ServiceTot",objInvoice.ServiceTot},
                                         {"@GenchargeTotal",objInvoice.GenChargeTot},
                                         {"@ADMTot",objInvoice.ADMTot},                                        
                                         {"@ACMTot",objInvoice.ACMTot},
                                         {"@CompanyId",objInvoice.CompanyId},
                                         {"@BranchId",objInvoice.BranchId},
                                         {"@CreatedBy",objInvoice.CreatedBy},
                                         {"@UpdatedBy",objInvoice.UpdateBy},
                                         {"@InvoiceOpenAmount",objInvoice.InvoiceOpenAmount},
                                         {"@TotalCommInclu",objInvoice.TotalCommInclu},
                                         {"@TotalCommExclu",objInvoice.TotalCommExclu},
                                         {"@TotalCommVatAmount",objInvoice.TotalCommVatAmount},
                                         {"@TempUniqCode",objInvoice.TempUniqCode},
                                         {"@InvDocumentNo",objInvoice.invDocumentNo},

                                          {"@AirCommi",objInvoice.AirCommi},
                                         {"@LandCommi",objInvoice.LandCommi},
                                         {"@ServicefeeCommi",objInvoice.ServicefeeCommi},
                                          {"@RefundAmount",objInvoice.RefundAmount},
                                     };
            int IsSuccess = ExecuteNonQuery("Invoice_Insert", htParams, "@return");
            return IsSuccess;
        }

        public int UpdateInvId(int InvId, string UniqueCode, string InvDocumentNo, int T5id)
        {
            Hashtable htParams = new Hashtable
                                     {
                                       
                                         {"@CurrentinvId",InvId},
                                         {"@TempUniqueId",UniqueCode},
                                         {"@InvDocumentNo",InvDocumentNo},
                                          {"@T5Id",T5id}
                                     };
            return ExecuteNonQuery("Update_InvoiceId", htParams);
        }

        public DataSet GetInvoiceLines(string TempUniqId)
        {
            Hashtable htparams = new Hashtable
           {
             {"@TempUniqId",TempUniqId}
           };

            return ExecuteDataSet("InvoiceItems_Get", htparams);

        }
        public DataSet GetInvoiceLinesCount(string TempUniqId)
        {
            Hashtable htparams = new Hashtable
           {
             {"@TempUniqId",TempUniqId}
           };

            return ExecuteDataSet("InvoiceItemsCount_Get", htparams);

        }
        public DataSet Get_InvoiceList()
        {

            return ExecuteDataSet("InvoiceList");
        }

        public DataSet Get_printstyle()
        {
            return ExecuteDataSet("PrintStyle_GetData");
        }

        public DataSet Get_ClientmessageandNote(int clientId)
        {

            Hashtable htParams = new Hashtable
                                     {
                                       
                                         {"@clientId",clientId}
                                        
                                        
                                     };
            return ExecuteDataSet("Select_ClientMessageAndNote", htParams);
        }


        // Get Data PDF Details With sending to Email 

        public DataSet GetServiceFeeMergeValue(int InvId, string TempuniqCode)
        {
            Hashtable htParams = new Hashtable
                                     {
                                       
                                         {"@Invid",InvId},
                                         {"@TempuniqCode",TempuniqCode},
                                           
                                     };
            return ExecuteDataSet("ServiceFee_getMerge", htParams);
        }

        public DataSet GetPdfDetails(int InvId, int companyid)
        {
            Hashtable htParams = new Hashtable
                                     {
                                       
                                         {"@InvId",InvId},
                                           {"@UsercomapnyId",companyid},
                                          
                                        
                                     };
            return ExecuteDataSet("Invoices_getPdfData", htParams);
        }
        public DataSet Get_InvoiceACAnalysis(int TempuniqID)
        {


            Hashtable htParams = new Hashtable
                                     {
                                       
                                         {"@TempuniqID",TempuniqID},
                                          
                                        
                                     };
            return ExecuteDataSet("InvoiceACAnalysis", htParams);
        }

        public DataSet DraftPdfDetails(string TempuniqCode, int companyid)
        {
            Hashtable htParams = new Hashtable
                                     {
                                       
                                         {"@TempUniqCode",TempuniqCode},
                                           {"@UsercomapnyId",companyid},
                                        
                                     };
            return ExecuteDataSet("DraftPdf_Details", htParams);
        }


        // Get Invoice's by client and status 

        public DataSet GetInvoiceDetailsByClientAndStatus(int ClientType, int ClientId, int Status)
        {
            Hashtable htParams = new Hashtable
                                     {
                                       
                                         {"@ClientTypeId",ClientType},
                                           {"@ClientNameId",ClientId},
                                           {"@PaymentStatus",Status}
                                     };
            return ExecuteDataSet("Invoices_getby_client", htParams);
        }


        public DataSet GetInvoiceDetailsBySUpplAndStatus(int SupplTypeId, string SupplTypeName, int categoryId, int Status)
        {
            Hashtable htParams = new Hashtable
                                     {
                                       
                                         {"@suppltypeid",SupplTypeId},
                                         {"@suppltypename",SupplTypeName},
                                          {"@categoryId",categoryId},
                                        {"@PaymentStatus",Status}
                                     };
            return ExecuteDataSet("Invoices_getby_supplier", htParams);
        }

        public DataSet GetInvocieDetailsBySupplierLevel(int SupplTypeId, string SupplTypeName, int Status)
        {
            Hashtable htParams = new Hashtable
                                     {
                                       
                                         {"@suppltypeid",SupplTypeId},
                                         {"@suppltypename",SupplTypeName},
                                      
                                        {"@PaymentStatus",Status},

                                     };
            return ExecuteDataSet("PaymentTransaction_GroupBySupplierLevel", htParams);
        }


        public DataSet GetSupplierOpeningAmount(int TicketId, int AirInvid, int Status, string category)
        {
            Hashtable htParams = new Hashtable
                                     {
                                       
                                         {"@TicketId",TicketId},
                                         {"@AirInvid",AirInvid},
                                      
                                        {"@PaymentStatus",Status},
                                        {"@category",category}
                                     };
            return ExecuteDataSet("PaymentTransaction_GetSupplierOpeningAmount", htParams);
        }

        //Edit for Invoice
        public DataSet GetInvoice(int InvId)
        {
            Hashtable htparams = new Hashtable
           {
             {"@InvId",InvId}
           

             
           };

            return ExecuteDataSet("Invoice_Get", htparams);

        }
        // Invoice Editer Grid
        public DataSet GetInvoiceLinesEdit(int InvId)
        {
            Hashtable htparams = new Hashtable
           {
             {"@InvId",InvId}
           };

            return ExecuteDataSet("[InvoiceItemsEdit_Get]", htparams);

        }
        public DataSet GetInvoiceLinesCountEdit(int InvId)
        {
            Hashtable htparams = new Hashtable
           {
             {"@InvId",InvId}
           };

            return ExecuteDataSet("InvoiceItemsCountEdit_Get", htparams);

        }


        public DataSet GetAccAnalysisData(int InvId)
        {
            Hashtable htParams = new Hashtable
                                     {
                                       
                                         {"@Invid",InvId},
                                           
                                     };
            return ExecuteDataSet("Invoices_GetAcountanalysis", htParams);
        }

        public DataSet GetAccAnalysisDetails(int InvId)
        {
            Hashtable htParams = new Hashtable
                                     {
		
                                         {"@Invid",InvId},
                                           
                                     };
            return ExecuteDataSet("AccountAnalysis_GetAmountsData", htParams);
        }

        public DataSet GetTicket(int TicketNo, string TicketType)
        {
            Hashtable htparams = new Hashtable
           {
             {"@TicketNo",TicketNo},
             {"@TicketType",TicketType}


           };

            return ExecuteDataSet("Tickets_Get", htparams);

        }

        public DataSet GetTicketType(int TId)
        {
            Hashtable htparams = new Hashtable{
                                     {"@TId",TId},
          };
            return ExecuteDataSet("TicketType_Get", htparams);
        }


        //Insert Credit Note

        public int CreditNote_Insert(int Invid, int TicketId, string Type, decimal RefundAmt)
        {
            Hashtable htparams = new Hashtable
           {
             {"@Invid",Invid},
             {"@TicketId",TicketId},
             {"@Type",Type},
             {"@RefundAmt",RefundAmt}

           };

            return ExecuteNonQuery("CreditNote_Insert", htparams);

        }
        public DataSet Check_Payment_Deposit()
        {

            return ExecuteDataSet("Check_Payment_Deposit");
        }
        public int DeleteInvoice(int InvoiceId)
        {
            Hashtable htparams = new Hashtable
           {
             {"@InvoiceId",InvoiceId}
           };

            return ExecuteNonQuery("Delete_Invoice", htparams);

        }

    }
}
