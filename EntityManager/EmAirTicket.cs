using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityManager
{
   public class EmAirTicket
    {

        public int TicketId { get; set; }
        public string AirTicketNo { get; set; }
        public int Type { get; set; }
        public string AirPnr { get; set; }
        public string Conjunction { get; set; }
        public string AirPassenger { get; set; }
        public int Airline { get; set; }
        public int AirService { get; set; }
        public int AirTicketType { get; set; }
        public string AirRouting { get; set; }
        public string AirMiles { get; set; }
        public DateTime? AirTravelDate { get; set; }
        public DateTime? AirReturnDate { get; set; }
        public decimal AirExclusiveFare { get; set; }
        public decimal AirVatonFare { get; set; }
        public decimal AirportTaxes { get; set; }
        public decimal AirVatInTaxes { get; set; }
        public decimal AirClientTotal { get; set; }
        public int AirPayment { get; set; }
        public int AirCreditCardType { get; set; }
        public decimal AirCommPer { get; set; }
        public decimal AirCommExcl { get; set; }
        public decimal AirVatPer { get; set; }
        public decimal AirCommVatPer { get; set; }
        public string TempUniqCode { get; set; }
        public decimal AirAgentVat { get; set; }
        public decimal AirCommInclu { get; set; }
        public decimal AirDueToBsp { get; set; }
        public int CreatedBy { get; set; }
        public string InvoiceType { get; set; }

        public decimal SupplOpenAmount { get; set; }
        public int AirInvId { get; set; }
       //
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        public int T5ID { get; set; }
        public int AirProInvId { get; set; }
        public int CurrentInvId { get; set; }

        public string AirRoutTempID { get; set; }
        public string invDocumentNo { get; set; }
        public string CreatedOn { get; set; }
        public string UpdatedOn { get; set; }
        public int IsCreditNote { get; set; }
        public decimal RefundAmount { get; set; }


       //Airsuppliers 

        public string SupAccountCode { get; set; }
        public string  SupplierName { get; set; }
        public int IsActive { get; set; }
        public string SupMainGIAccCode { get; set; }

        public string AirlineID { get; set; }
        public string AirlineName { get; set; }


       
    }
}
