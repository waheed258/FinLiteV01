using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityManager
{
    public class TransactionMaster
    {
        public int InvoiceId { get; set; }
        public string Divission { get; set; }
        public string ReceiptType { get; set; }
        public int AutoDepositeId { get; set; }
        public string AutoDepositeAccountNo { get; set; }
        public string ClientAccountNo { get; set; }
        public int ClientTypeId { get; set; }
        public int ClientAccountNoID { get; set; }
        public string PayeeDetails { get; set; }
        public decimal ReceiptAmount { get; set; }
        public decimal AllocatedAmount { get; set; }
        public decimal ReceiptBalanceAmount { get; set; }
        public decimal InvoiceBalanceAmount { get; set; }
        public decimal PrvClientOpenAmount { get; set; }
        public string Details { get; set; }
        public string Messages { get; set; }
        public int CreatedBy { get; set; }
        public string PaymentSourceRef { get; set; }
        public decimal ReceiptAmountAfterPaid { get; set; }

    }
    public class OpenAmountDetails
    {
        public int OpenAmountDtlId { get; set; }
        public int ClientTypeId { get; set; }
        public int ClientNameId { get; set; }
        public decimal ReceiptAmount { get; set; }
        public decimal PrvOpenAmount { get; set; }
        public decimal AlocatedAmount { get; set; }
        public decimal ReceiptOpenAmount { get; set; }
        public string SourceRef { get; set; }
        public string ReceiptType { get; set; }
        public string FromAccount { get; set; }
        public string ToAccount { get; set; }
        public int CreatedBy { get; set; }
    }

    public class Transaction
    {
        public int TransId { get; set; }
        public string FmAccountNO { get; set; }
        public string ReferenceAccountNO { get; set; }
        public decimal CreditAmount { get; set; }
        public decimal DebitAmount { get; set; }
        public string ReferenceNo { get; set; }
        public DateTime TransactionDate { get; set; }
        public int InvoiceId { get; set; }
        public string InvoiceNo { get; set; }
        public string FmMainAccount { get; set; }
        public string ReferenceType { get; set; }
        public int FmAccountNoId { get; set; }
        public int ReferenceAccountNoId { get; set; }
        public decimal BalanceAmount { get; set; }
        public int CreatedBy { get; set; }
        public string ToMainAccount { get; set; }
    }

}
