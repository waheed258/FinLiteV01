using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityManager
{
   public class EMCreditCard
    {
       public int CreditCardId { get; set; }
       public string CreditCardKey { get; set; }
       public string CreditDescription { get; set; }
       public int NumberPrefix { get; set; }
       public int CreatedBy { get; set; }

    }
}
