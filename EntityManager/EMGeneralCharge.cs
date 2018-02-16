using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityManager
{
  public  class EMGeneralCharge
    {
      public int GenChgId { get; set; }
      public int Type { get; set; }
      public string PassengerName { get; set; }
      public string Details { get; set; }
      public int Units { get; set; }
      public decimal RateNet { get; set; }
      public decimal ExcluAmt { get; set; }
      public decimal VatPer { get; set; }
      public decimal VatAmount { get; set; }
      public decimal ClientTot { get; set; }
      public int CreditCard { get; set; }
      public int GCProInvId { get; set; }
      public int GenInvId { get; set; }
      public int CompanyId { get; set; }
      public int BranchId { get; set; }
      public int CreatedBy { get; set; }
      public string GenTempUniqCode { get; set; }
      public string InvoiceType { get; set; }
      public string invDocumentNo { get; set; }
    }
}
