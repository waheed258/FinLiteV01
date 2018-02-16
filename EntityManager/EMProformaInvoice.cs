using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityManager
{
    public class EMProformaInvoice
    {
        public DateTime? PFInvDate { get; set; }
        public int ClientTypeId { get; set; }
        public int ClientNameId { get; set; }
        public int ConsultantId { get; set; }
        public string PFInvOrder { get; set; }
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
        public decimal PFInvoiceTotal { get; set; }
        public int CreatedBy { get; set; }
        public int UpdateBy { get; set; }
        public DateTime CreatedON { get; set; }
        public DateTime UpdateON { get; set; }
        public string TempUniqCode { get; set; }

        public string invDocumentNo { get; set; }

       

        public decimal TotalCommInclu { get; set; }
        public decimal TotalCommExclu { get; set; }
        public decimal TotalCommVatAmount { get; set; }
    }
}
