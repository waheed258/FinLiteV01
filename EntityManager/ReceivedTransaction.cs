using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityManager
{
   public class ReceivedTransaction
    {
       public int OpenAmountSupplDtlId { get; set; }
       public int SupplTypeId { get; set; }
       public int SupplNameId { get; set; }
       public decimal ReceiptAmount { get; set; }
       public decimal OpenAmount { get; set; }
       public decimal PrvOpenAmount { get; set; }
       public decimal AlocatedAmount { get; set; }
       public decimal PaymentOpenAmount { get; set; }
       public string SourceRef { get; set; }
       public string PaymentType { get; set; }
           public string FromAccount { get; set; }
           public string ToAccount { get; set; }
           public int CreatedBy { get; set; }
     
    }


   public class PaymentTransaction
   {
       public int InvoiceId { get; set; }
       public int TicketId { get; set; }
       public string Divission { get; set; }
       public string PaymentType { get; set; }
       public int AutoDepositeId { get; set; }
       public string AutoDepositeAccountNo { get; set; }
       public string ClientSupplAccountNo { get; set; }
       public int ClientSupplTypeId { get; set; }
       public int ClientSupplAccountNoID { get; set; }
       public string PayeeDetails { get; set; }
       public decimal PaymentAmount { get; set; }
       public decimal AllocatedAmount { get; set; }
       public decimal PaymentBalanceAmount { get; set; }
       public decimal InvoiceBalanceAmount { get; set; }
       public decimal PrvClientOpenAmount { get; set; }
       public string Details { get; set; }
       public string Messages { get; set; }
       public int CreatedBy { get; set; }
       public string PaymentSourceRef { get; set; }
       public string categoryName { get; set; }
       public string MainAccount { get; set; }
       public int CategoryId { get; set; }

   }
}
