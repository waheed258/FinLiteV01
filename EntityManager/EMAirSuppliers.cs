using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityManager
{
    public class EMAirSuppliers
    {
        public int SupplierId { get; set; }
        public string SupAccountCode { get; set; }
        public string SupplierName { get; set; }
        public int IsActive { get; set; }
        public int ServiceType { get; set; }
        public int Country { get; set; }
        public int GroupId { get; set; }
        public int StateId { get; set; }
        public int Division { get; set; }
        public int City { get; set; }
        public int Consultant { get; set; }
        public string Telephone { get; set; }
        public string Fax { get; set; }
        public string CellNo { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }
        public string Web { get; set; }
        public string PhysicalAddress { get; set; }
        public string PostalAddress { get; set; }
        public string VatRegNo { get; set; }
        public int NoVatNo { get; set; }
        public string ExtAcc { get; set; }
        public string QuickTravelCode { get; set; }
        public int Bank { get; set; }
        public string BranchCode { get; set; }
        public string BranchName { get; set; }
        public string AccountNo { get; set; }
        public int AccountType { get; set; }
        public string AccHolder { get; set; }
        public string QuickGIAccount { get; set; }
        public string LedgerAccount { get; set; }      
        public decimal CommPercentage { get; set; }
        public int ZeroCommission { get; set; }
        public int PaymentMethod { get; set; }
        public int ClientTaxInvoice { get; set; }
        public int ClientInvoiceType { get; set; }
        public int PrinciTaxInvoice { get; set; }
        public int IgnDupInvoiceNo { get; set; }
        public int AllocItemType { get; set; }
        public int EconomyClass { get; set; }
        public int BusinessClass { get; set; }
        public int FirstClass { get; set; }
        public string Notes { get; set; }
        public int NoteType { get; set; }
        public int CreatedBy { get; set; }
        public int CommissMethod { get; set; }
        public string SupplMainGIAccCode { get; set; }


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
