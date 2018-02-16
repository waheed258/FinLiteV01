using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityManager
{
    public class EmInvoice
    {
        public int InvId { get; set; }
        public DateTime? InvDate { get; set; }
        public int ClientTypeId { get; set; }
        public int ClientNameId { get; set; }
        public int ConsultantId { get; set; }
        public string InvOrder { get; set; }
        public int BookSourceId { get; set; }
        public int BookDestinationId { get; set; }
        public int BookingNo { get; set; }
        public string Message { get; set; }
        public int MessageType { get; set; }
        public int PrintStyleId { get; set; }
        public decimal AirTotal { get; set; }
        public decimal LandTotal { get; set; }
        public decimal ServiceTot { get; set; }
        public decimal GenChargeTot { get; set; }
        public decimal ADMTot { get; set; }
        public decimal ACMTot { get; set; }
        public decimal InvoiceTotal { get; set; }
        public int CreatedBy { get; set; }
        public int UpdateBy { get; set; }
        //public DateTime CreatedON { get; set; }
        //public DateTime UpdateON { get; set; }
        public string TempUniqCode { get; set; }
        public decimal InvoiceOpenAmount { get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        public string invDocumentNo { get; set; }
      
        public decimal TotalCommInclu { get; set; }
        public decimal TotalCommExclu { get; set; }
        public decimal TotalCommVatAmount { get; set; }

        public decimal AirCommi { get; set; }
        public decimal LandCommi { get; set; }
        public decimal ServicefeeCommi { get; set; }
        public int IsCreditNote { get; set; }
        public decimal RefundAmount { get; set; }
    }
}
