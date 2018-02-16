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
    public class DALProformaInvoice : DataUtilities
    {
        public int InsertProformaInvoice(EMProformaInvoice objPFInvoice)
        {
            Hashtable htParams = new Hashtable
                                     {
                                       
                                         {"@PFInvDate",objPFInvoice.PFInvDate},
                                         {"@ClientTypeId",objPFInvoice.ClientTypeId},
                                         {"@ClientNameId",objPFInvoice.ClientNameId}, 
                                         {"@ConsultantId",objPFInvoice.ConsultantId},
                                         {"@PFInvOrder",objPFInvoice.PFInvOrder},                                                                             
                                         {"@BookSourceId",objPFInvoice.BookSourceId},                                        
                                         {"@BookDestinationId",objPFInvoice.BookDestinationId},
                                         {"@BookingNo",objPFInvoice.BookingNo},
                                         {"@Message",objPFInvoice.Message},
                                         {"@MessageType",objPFInvoice.MessageType},
                                         {"@PrintStyleId",objPFInvoice.PrintStyleId},
                                         {"@AirTotal",objPFInvoice.AirTotal},
                                         {"@PFInvoiceTotal",objPFInvoice.PFInvoiceTotal},
                                         {"@LandTotal",objPFInvoice.LandTotal},
                                         {"@ServiceTot",objPFInvoice.ServiceTot},
                                         {"@GenchargeTotal",objPFInvoice.GenChargeTot},
                                         {"@ADMTot",objPFInvoice.ADMTot},                                        
                                         {"@ACMTot",objPFInvoice.ACMTot},
                                         {"@CreatedBy",objPFInvoice.CreatedBy},
                                         {"@TempUniqueId",objPFInvoice.TempUniqCode},
                                          {"@InvDocumentNo",objPFInvoice.invDocumentNo},
                                     };
            return ExecuteNonQuery("PFInvoice_Insert", htParams, "@return");
        }
        public int UpdateProformaInvId(int PFInvId, string UniqueCode)
        {
            Hashtable htParams = new Hashtable
                                     {
                                       
                                         {"@CurrentPFInvId",PFInvId},
                                         {"@TempUniqueId",UniqueCode},
                                        
                                     };
            return ExecuteNonQuery("Update_PFInvoiceId", htParams);
        }
        public DataSet Get_PFInvoiceList()
        {

            return ExecuteDataSet("[PFInvoiceList]");
        }

        public DataSet GetPFPdfDetails(int PFInvId, int companyid)
        {
            Hashtable htParams = new Hashtable
                                     {
                                       
                                         {"@PFInvId",PFInvId},
                                           {"@UsercomapnyId",companyid},
                                          
                                        
                                     };
            return ExecuteDataSet("PFInvoices_getPdfData", htParams);
        }
        public DataSet GetPFServiceFeeMergeValue(int PFInvId, string TempuniqCode)
        {
            Hashtable htParams = new Hashtable
                                     {
                                       
                                         {"@PFInvid",PFInvId},
                                         {"@TempuniqCode",TempuniqCode},
                                           
                                     };
            return ExecuteDataSet("PFServiceFee_getMerge", htParams);
        }
        public DataSet PFDraftPdfDetails(string TempuniqCode, int companyid)
        {
            Hashtable htParams = new Hashtable
                                     {
                                       
                                         {"@TempUniqCode",TempuniqCode},
                                           {"@UsercomapnyId",companyid},
                                        
                                     };
            return ExecuteDataSet("DraftPdf_Details", htParams);
        }
      
    }
}
