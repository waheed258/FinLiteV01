using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityManager
{
    public class EMClients
    {
        public int ClientId { get; set; }
        public string ClientType { get; set; }
        public string ClientAccountNo { get; set; }
        public string ClientName { get; set; }
        public int IsActive { get; set; }
        public int ClientGroup { get; set; }
        public int ClientConsultant { get; set; }
        public int ClientDepartment { get; set; }
        public int ClientDivision { get; set; }
        public string ClientVatRegNo { get; set; }
        public int ClientNoVatNo { get; set; }
        public string ClientTelephone { get; set; }
        public string ClientFax { get; set; }
        public string ClientContactPerson { get; set; }
        public string ClientEmail { get; set; }
        public string ClientPostalAddress { get; set; }
        public string ClientPhysicalAddress { get; set; }
        public string YearEnd { get; set; }
        public string Logo { get; set; }
        public int HtmlWidth { get; set; }
        public int HtmlHeight { get; set; }
        public string Align { get; set; }
        public int IsDisableAutoPrintManual { get; set; }
        public int IsDisableAutoPrintTicket { get; set; }
        public int DefaultOrderNo { get; set; }
        public int IsForceOrderNo { get; set; }
        public int IsForceExternalVoucher { get; set; }
        public int ClientDuplicateOrderNo { get; set; }
        public int ActionIsNoServiceFee { get; set; }
        public int ClientOpenItemLoad { get; set; }
        public int ClientBroughtFwdBreakDwn { get; set; }
        public int ClientLineItemBreakDwn { get; set; }
        public int ClientDbOrCd { get; set; }
        public int ClientSuppressNilVal { get; set; }
        public int ClientTotalCharge { get; set; }
        public int ClientCreditCard { get; set; }
        public int ClientTransactionRef { get; set; }
        public int ClientAllocPaidItems { get; set; }
        public string ClientCustmDetail { get; set; }
        public int ClientExcludeFmPrint { get; set; }
        public decimal ClientCreditLimit { get; set; }
        public int ClientLimitAction { get; set; }
        public decimal ClientMaxInvoiceVal { get; set; }
        public int ClientCreditTerms { get; set; }
        public int ClientTermsAction { get; set; }
        public int IsForce { get; set; }
        public int ClientCalAir { get; set; }
        public int ClientCalLand { get; set; }
        public int ClientDirectSettleTrans { get; set; }
        public int ClientCCForCashTrans { get; set; }
        public int IsTicketOrLandCreditCard { get; set; }
        public int CreditCardImportedTickets { get; set; }
        public string ClientNotes { get; set; }
        public string ClientMessage { get; set; }
        public int CreatedBy { get; set; }
        public string LogoPath { get; set; }
        public string CContactKey { get; set; }
        public string CContactName { get; set; }
        public string CPosition { get; set; }
        public string CTelephone { get; set; }
        public string CEmailAddress { get; set; }
        public int CAutoMail { get; set; }
        public string CCellNo { get; set; }
        public string CFax { get; set; }
        public int CDeactivate { get; set; }

        #region contacts
        public int ContactId { get; set; }
        public string ContactKey { get; set; }
        public string ContactName { get; set; }
        public string Position { get; set; }
        public string Telephone { get; set; }
        public string EmailAddress { get; set; }
        public bool AutoMail { get; set; }
        public int LSupplierId { get; set; }
        public string UserCategory { get; set; }
        public string ConCellNo { get; set; }
        public string ConFax { get; set; }
        public bool ConDeactivate { get; set; }
        public string ClientAccCode { get; set; }
        public string ClientTypeAccCode { get; set; }
        #endregion contacts
        #region CreditCard
        public int CreditCardId { get; set; }
        public string CreditCardCode { get; set; }
        public string CreditCardType { get; set; }
        public string CreditCardAccNo { get; set; }
        public string CreditCardAccHolder { get; set; }
        public string CreditCardExpires { get; set; }
        //public int ClientId { get; set; }

        #endregion CreditCard

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
