using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityManager
{
  public  class EMLandarrangement
    {
        public int LandArrId { get; set; }
        public int LandSupplier { get; set; }
        public int Services { get; set; }
        public int Type { get; set; }
        public string PassengerName { get; set; }
        public DateTime? TravelFrDate { get; set; }
        public DateTime? TravelToDate { get; set; }
        public string BookRefNo { get; set; }
        public string Voucher { get; set; }
        public string SuppRef { get; set; }
        public string SuppInvNo { get; set; }
        public decimal TotIncl { get; set; }
        public decimal ExclVatPer { get; set; }
        public decimal ExclusiveVatAmount { get; set; }
        public decimal VatPer { get; set; }
        public decimal TotExclu { get; set; }
        public int Payment { get; set; }
        public int CreditCard { get; set; }
        public decimal CO2Emis { get; set; }
        public int Units { get; set; }
        public decimal RateInclu { get; set; }
        public decimal CommissionableInclu { get; set; }
        public decimal CommabExclu { get; set; }
        public decimal OthCommabInclu { get; set; }
        public decimal TotCommabInclu { get; set; }
        public decimal NonCommabInclu { get; set; }
        public decimal CommPer { get; set; }
        public decimal CommissionInclu { get; set; }
        public decimal CommissionExclu { get; set; }
        public int CommissionVatPer { get; set; }
        public decimal CommVatAmount { get; set; }
        public decimal DueTOSupplier { get; set; }
        public decimal DueToClient { get; set; }
        public decimal LessCommission { get; set; }
        public int ProInvId { get; set; }
        public int LandInvId { get; set; }
        public int CompanyId { get; set; }
        public int T5ID { get; set; }
        public int StatusId { get; set; }
        public int BranchId { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime? CreatedON { get; set; }
        public DateTime? UpdatedON { get; set; }
        public string invDocumentNo { get; set; }
        public string LandTempUniqCode { get; set; }
        public string InvoiceType { get; set; }
        public decimal SupplOpenAmount { get; set; }
        public int IsCreditNote { get; set; }

        public decimal RefundAmount { get; set; }

    }
}
