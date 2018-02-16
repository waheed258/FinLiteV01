using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityManager
{
     public class EMPaymentTypes
    {
         public int PaymentId { get; set; }
         public string PaymentName { get; set; }
         public string PaymentCode { get; set; }
         public int IsDefaultPayment { get; set; }
         public int CreatedBy { get; set; }
        
    }
}
