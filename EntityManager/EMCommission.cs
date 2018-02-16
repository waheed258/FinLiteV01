using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityManager
{
   public class EMCommission
    {

       public int CommiId { get; set; }
       public decimal CommiAmount { get; set; }
       public string TicketType { get; set; }
       public int Invid { get; set; }
       public string InvDocumentNo { get; set; }
       public int ChartedAccCode { get; set; }
       public int Status { get; set; }
       public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public int TolCommiAccount { get; set; }
    }
}
