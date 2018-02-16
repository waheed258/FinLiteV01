using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using EntityManager;

namespace DataManager
{
   public class DALServicefee:DataUtilities
    {
       public int InsertUpdateSerfee(EMServicefee objService, decimal MergeAmt)
       {
           Hashtable htParams = new Hashtable
                                     {
                                       {"@IsCreditNote",objService.IsCreditNote},
                                         {"@ServiceFeeId",objService.ServiceFeeId},
                                         {"@Type",objService.Type},
                                         {"@SourceRef",objService.SourceRef}, 
                                         {"@MergeC",objService.MergeC},
                                         {"@TravelDate",objService.TravelDate},
                                         {"@PassengerName",objService.PassengerName},
                                         {"@Details",objService.Details},
                                         {"@ExcluAmount",objService.ExcluAmount},                                        
                                         {"@VatPer",objService.VatPer},
                                         {"@VatAmount",objService.VatAmount},
                                         {"@ClientTot",objService.ClientTot},
                                         {"@PaymentMethod",objService.PaymentMethod},
                                         {"@CreditCardType",objService.CreditCardType},
                                         {"@CollectVia",objService.CollectVia},
                                         {"@TasfMpd",objService.TasfMpd},
                                         {"@SFProInvId",objService.ProInvId},                                        
                                         {"@InvId",objService.SerFeeInvId},
                                         {"@CompanyId",objService.CompanyId},
                                         {"@BranchId",objService.BranchId},
                                         {"@T5ID",objService.T5ID},                                         
                                         {"@CreatedBy",objService.CreatedBy},
                                          {"@SerTempUniqCode",objService.SerTempUniqCode},
                                          {"@InvoiceType",objService.InvoiceType},
                                           {"@InvDocumentNo",objService.invDocumentNo},
                                           {"@MergeAmt" , MergeAmt},
                                            {"@RefundAmount" , objService.RefundAmount}
                                         
                                     };
           return ExecuteNonQuery("ServiceFee_InsertUpdate", htParams);
       }

     
       public DataSet BindTicketNumber(string Tempuniqcode)
       {
           Hashtable htParams = new Hashtable
                                     {

                                         {"@Tempuniqcode",Tempuniqcode},
                                     };
           return ExecuteDataSet("Airticket_TicketNumbers",htParams);
       }
       public DataSet BindPassengerNames(string Tempuniqcode,int Airtickno)
       {
           Hashtable htParams = new Hashtable
                                     {
                                         {"@AirTickNo",Airtickno},
                                         {"@Tempuniqcode",Tempuniqcode},
                                     };

           return ExecuteDataSet("Airticket_getPassengerName", htParams);
       }

       public Object GetSFMergeAmount(int TicketId, int Merge)
       {
           Hashtable htParams = new Hashtable
                                     {
                                         {"@TicketId",TicketId},
                                         {"@Mergec",Merge},
                                     };

           return ExecuteScalar("Servicefee_getMergeAmount", htParams);
       }
       public DataSet BindCollectVia()
       {

           return ExecuteDataSet("CollectVia_GetData");
       }
       public DataSet BindSerServiceTypes()
       {

           return ExecuteDataSet("ServiceType_GetData");
       }
       public object getVatPercentage(int ComId, string ComDesc)
       {
           Hashtable htParams = new Hashtable
                                     {
                                       
                                         {"@ComId",ComId},
                                         {"@ComDesc",ComDesc}
                                     };
           return ExecuteScalar("GetServGen_VatRate", htParams);
       }
    }
}
