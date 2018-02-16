using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityManager
{
    public class EMLandSuppliers
    {
        public int LSupplierId { get; set; }
        public string LSupAccountCode { get; set; }
        public string LSupplierName { get; set; }
        public int LIsActive { get; set; }
        public int LServiceType { get; set; }
        public int LCountry { get; set; }
        public int LGroupId { get; set; }
        public int LStateId { get; set; }
        public int LDivision { get; set; }
        public int LCity { get; set; }
        public int LConsultant { get; set; }
        public string LLatitute { get; set; }
        public string LLongitude { get; set; }
        public string LTelephone { get; set; }
        public string LFax { get; set; }
        public string LCellNo { get; set; }
        public string LContactNo { get; set; }
        public string LEmail { get; set; }
        public string LWeb { get; set; }
        public string LPhysicalAddress { get; set; }
        public string LPostalAddress { get; set; }
        public string LVatRegNo { get; set; }
        public int LNoVatNo { get; set; }
        public string LExtAcc { get; set; }
        public string LIATAReg { get; set; }
        public string LAlphaCode { get; set; }
        public string LAmadeus { get; set; }
        public string LGalileo { get; set; }
        public string LSabre { get; set; }
        public string LWorldSpan { get; set; }
        public string LCarHire { get; set; }
        public string LFrontDesk { get; set; }
        public string LOtherPropertyNo { get; set; }
        public int LBank { get; set; }
        public string LBranchCode { get; set; }
        public string LBranchName { get; set; }
        public string LAccountNo { get; set; }
        public int LAccountType { get; set; }
        public string LAccHolder { get; set; }
        public string LQuickGIAccount { get; set; }
        public string LLedgerAccount { get; set; }
        public int LCommissMethod { get; set; }
        public decimal LCommPercentage { get; set; }
        public int LZeroCommission { get; set; }
        public int LPaymentMethod { get; set; }
        public int LClientTaxInvoice { get; set; }
        public int LClientInvoiceType { get; set; }
        public int LPrinciTaxInvoice { get; set; }
        public int LIgnDupInvoiceNo { get; set; }
        public int LAllocItemType { get; set; }    
        public string LNotes { get; set; }
        public int LNoteType { get; set; }
        public int CreatedBy { get; set; }
        public string LContactKey { get; set; }
        public string LContactName { get; set; }
        public string LContactPosition { get; set; }
        public string LContTelephone { get; set; }
        public string LContEmailAddress { get; set; }
        public int LContAutoMail { get; set; }
        public string ContactCellNo { get; set; }
        public string ContactFax { get; set; }
        public int ContactDeactivate { get; set; }
        public string SupplMainGIAccCode { get; set; }

        #region contacts
        public int ContactId { get; set; }
        public string ContactKey { get; set; }
        public string ContactName { get; set; }
        public string Position { get; set; }
        public string Telephone { get; set; }
        public string EmailAddress { get; set; }
        public int AutoMail { get; set; }
        public int ClientId { get; set; }
        public string UserCategory { get; set; }
        public string ConCellNo { get; set; }
        public string ConFax { get; set; }
        public int ConDeactivate { get; set; }

        #endregion contacts


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
