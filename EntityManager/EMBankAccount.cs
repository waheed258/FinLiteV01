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
        public int IsClient { get; set; }

        #region CharteredAccounts
        public int ChartedAccId { get; set; }
        public string ChartedAccName { get; set; }
        public int ChartedMasterAccName { get; set; }
        public string Type { get; set; }
        public string AccCode { get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        public int DepartmentId { get; set; }
        public int BaseCurrency { get; set; }
        public int TranCurrency { get; set; }
        public int CategoryId { get; set; }
        public string RefType { get; set; }
        public string RefId { get; set; }

        #endregion CharteredAccounts
    }
}
