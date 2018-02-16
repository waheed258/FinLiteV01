using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityManager
{
   public class EMDepositChild
    {
       public DateTime RecieptDate { get; set; }
       public string ReceiptType { get; set; }
       public string ReciptClient { get; set; }
       public decimal ReceiptAmount { get; set; }
       public int DepositTransMasterId { get; set; }
       public string CreatedBy { get; set; }
       
        public int CreatedOn { get; set; }
        public int ReceiptId { get; set; }
        public int InvoiceId { get; set; }

        public int DepositAcId  { get; set; }
    }
}
