using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityManager
{
  public  class EMServicefee
    {

       public int ServiceFeeId { get; set; }
       public int Type { get; set; }
       public string SourceRef { get; set; }
       public int MergeC { get; set; }
       public DateTime TravelDate { get; set; }
       public int PassengerName { get; set; }
       public string Details { get; set; }
       public decimal ExcluAmount { get; set; }
       public decimal VatPer { get; set; }
       public decimal VatAmount { get; set; }
       public decimal ClientTot { get; set; }
       public int PaymentMethod { get; set; }
       public int CreditCardType { get; set; }
       public int CollectVia { get; set; }
       public string TasfMpd { get; set; }
       public int ProInvId { get; set; }
       public int SerFeeInvId { get; set; }
       public int CompanyId { get; set; }
       public int BranchId { get; set; }
       public int T5ID { get; set; }
       public int CreatedBy { get; set; }
       public string SerTempUniqCode { get; set; }
       public string InvoiceType { get; set; }

       public string invDocumentNo { get; set; }
       public int IsCreditNote { get; set; }
       public decimal RefundAmount { get; set; }
    }
}
