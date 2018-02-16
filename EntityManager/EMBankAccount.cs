using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityManager
{
    public  class EMBankAccount
    {
        public int BankAcId { get; set; }
       public string BankAcKey { get; set; }
        public int IsDeActivate { get; set; }
        public string BankName { get; set; }
        public string BankAcType { get; set; }
        public string BankAcNo { get; set; }
        public string BankBranchCode { get; set; }
        public string BranchName { get; set; }
        public string AccountHolder { get; set; }
        public int Graphic { get; set; }
        public int OwnerBranch { get; set; }
        public string GiCode { get; set; }
        public string GiDepositBatch { get; set; }
        public string GiPaymentBatch { get; set; }
        public string InternetBankingLink { get; set; }
        public int StatementFormat { get; set; }
        public int CreatedBy { get; set; }
    }
}
