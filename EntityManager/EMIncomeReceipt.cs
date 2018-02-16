using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityManager
{
  public  class EMIncomeReceipt
    {
      public int IncomeId { get; set; }
      public int FromIncomeAccount { get; set; }
      public string FromMainAccount { get; set; }
      public int ToIncomeAccount { get; set; }
      public string ToMainAccount { get; set; }
      public int Category { get; set; }
      public decimal IncomeAmount { get; set; }
      public int createdBy { get; set; }
      public DateTime Incomedate { get; set; }
      public string SourceRefNo { get; set; }
       

    }
}
