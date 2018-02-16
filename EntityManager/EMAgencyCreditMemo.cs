using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityManager
{
   public class EMAgencyCreditMemo
    {
        public int CreditMemoRevId { get; set; }
        public string AcmNo { get; set; }
        public string TicketNo { get; set; }

        public int Supplier { get; set; }
        public int Type { get; set; }
        public string PassengerName { get; set; }
        public decimal ExcluFare { get; set; }
        public decimal FareVat { get; set; }
        public decimal DepatureTaxes { get; set; }
        public decimal ClientTotal { get; set; }
        public decimal Commission { get; set; }
        public decimal AgentVat { get; set; }
        public decimal BSP { get; set; }
        public int CreditCard { get; set; }
        public int ProInvId { get; set; }
        public int InvId { get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public int UpdatedBy { get; set; }
        public string UpdatedON { get; set; }

        public string AcmTempUniqId { get; set; }
        public string InvoiceType { get; set; }
        public int IsCreditNote { get; set; }
    }
}
