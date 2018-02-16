using EntityManager;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataManager
{
    public class DALAirTicket : DataUtilities
    {
        public int InsertAirTicket(EmAirTicket objAirticket)
        {
            Hashtable htParams = new Hashtable
                                     {
                                       
                                         {"@IsCreditNote",objAirticket.IsCreditNote},
                                         {"@TicketId",objAirticket.TicketId},
                                         {"@AirTicketNo",objAirticket.AirTicketNo},
                                         {"@Type",objAirticket.Type},
                                         {"@AirPnr",objAirticket.AirPnr}, 
                                         {"@Conjunction",objAirticket.Conjunction},
                                         {"@AirPassenger",objAirticket.AirPassenger},                                                                             
                                         {"@Airline",objAirticket.Airline},                                        
                                         {"@AirService",objAirticket.AirService},
                                         {"@AirTicketType",objAirticket.AirTicketType},
                                         {"@AirRouting",objAirticket.AirRouting},
                                         {"@AirMiles",objAirticket.AirMiles},
                                         {"@AirTravelDate",objAirticket.AirTravelDate},
                                         {"@AirReturnDate",objAirticket.AirReturnDate},
                                         {"@AirExclusiveFare",objAirticket.AirExclusiveFare},
                                         {"@AirVatonFare",objAirticket.AirVatonFare},                                        
                                         {"@AirportTaxes",objAirticket.AirportTaxes},
                                         {"@AirVatInTaxes",objAirticket.AirVatInTaxes},
                                         {"@AirClientTotal",objAirticket.AirClientTotal},
                                         {"@AirPayment",objAirticket.AirPayment},
                                         {"@AirCreditCardType",objAirticket.AirCreditCardType},
                                         {"@AirCommPer",objAirticket.AirCommPer},
                                         {"@AirCommExcl",objAirticket.AirCommExcl},
                                         {"@AirVatPer",objAirticket.AirCommVatPer},
                                         {"@AirAgentVat",objAirticket.AirAgentVat},
                                         {"@AirCommInclu",objAirticket.AirCommInclu},
                                         {"@AirDueToBsp",objAirticket.AirDueToBsp},
                                         {"@TempUniqCode",objAirticket.TempUniqCode},
                                         {"@CreatedBy",objAirticket.CreatedBy},
                                         {"@InvoiceType",objAirticket.InvoiceType},
                                         {"@SupplOpeningAmount",objAirticket.SupplOpenAmount},
                                         {"@AirInvId",objAirticket.AirInvId},
                                         {"@CompanyId",objAirticket.CompanyId},
                                         {"@BranchId",objAirticket.BranchId},
                                         {"@T5ID",objAirticket.T5ID},
                                         {"@AirProInvId",objAirticket.AirProInvId},
                                         {"@currentinv",objAirticket.CurrentInvId},
                                         {"@CreatedOn",objAirticket.CreatedOn},
                                         {"@UpdatedOn",objAirticket.UpdatedOn},
                                         {"@InvDocumentNo",objAirticket.invDocumentNo},
                                         {"@AirRoutTempID",objAirticket.AirRoutTempID},
                                         {"@RefundAmount",objAirticket.RefundAmount}
                                         
                                     };
            int IsSuccess = ExecuteNonQuery("Airticket_Insert", htParams, "@return");
            return IsSuccess;
        }


        public int InsertAirticketRouting(EmAirticketRouting objAirRouting)
        {
            Hashtable htParams = new Hashtable
                                     {
                                       {"@ClassId",objAirRouting.ClassId},
                                        {"@Routing",objAirRouting.Routing},
                                        {"@Routs",objAirRouting.Routs},
                                         {"@Miles",objAirRouting.Miles},
                                         {"@FlightNo",objAirRouting.FlightNo},
                                         {"@Class",objAirRouting.Class},
                                         {"@Date",objAirRouting.Date}, 
                                         {"@AirticketId",objAirRouting.AirticketId},                                                                             
                                         {"@InvoiceId",objAirRouting.InvoiceId},  
                                         {"@CreatedBy",objAirRouting.CreatedBy},
                                         {"@TicketType",objAirRouting.TicketType},
                                        {"@TempUniqCode",objAirRouting.TempUniqCode},
                                     };

            return ExecuteNonQuery("AirticketRouting_InsertUpdate", htParams);

        }

        public int UpdateAirTicketId(int TicketId, string UniqueCode)
        {
            Hashtable htParams = new Hashtable
                                     {
                                       
                                         {"@CurrentTicketId",TicketId},
                                         {"@TempUniqueId",UniqueCode}
                                     };
            return ExecuteNonQuery("[Update_AirTicketId]", htParams);
        }


        public int InsertSupplier(EmAirTicket objAirticket)
        {
            Hashtable htParams = new Hashtable
                                     {
                                       
                                         {"@SupAccountCode",objAirticket.SupAccountCode},
                                         {"@AirlineID",objAirticket.AirlineID},
                                         {"@IsActive",objAirticket.IsActive},
                                         {"@SupMainGIAccCode",objAirticket.SupMainGIAccCode} ,
                                         {"@AirlineName",objAirticket.AirlineName}, 
                                         {"@AirService",objAirticket.AirService},
                                          
                                     };
            int IsSuccess = ExecuteNonQuery("[Supplier_Insert_Mir]",htParams);
            return IsSuccess;
        }


    }
}
