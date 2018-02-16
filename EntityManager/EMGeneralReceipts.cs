using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityManager
{
   public class EMGeneralReceipts
    {
        public int GeneralReceiptId { get; set; }
        public DateTime? GRPaymentDate { get; set; }
        public string GRPreparedBy { get; set; }
        public int GRCategoryID { get; set; }
        public int GRFromAccount { get; set; }
        public int ToAccountID { get; set; }
        public string GRSupplierMainAccCode { get; set; }
        public string GRSupplierFromAccCode { get; set; }
        public decimal GRPaymentAmount { get; set; }
        public int CreatedBy { get; set; }
    }
}
