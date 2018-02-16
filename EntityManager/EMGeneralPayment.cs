using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityManager
{
   public class EMGeneralPayment
    {

       public int GeneralPymtId { get; set; }
       public int FromAccCodeId { get; set; }
       public string FmMainAcCode { get; set; }
       public int ToAccCodeId { get; set; }
       public string ToMainAcCode { get; set; }
       public decimal PaymentAmount { get; set; }
       public int CategoryId { get; set; }
       public DateTime PaymentDate { get; set; }
       public int CreatedBy { get; set; }

    }

}
