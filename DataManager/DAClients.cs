using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityManager;
using System.Collections;
using System.Data;

namespace DataManager
{
    public class DAClients : DataUtilities
    {
        public int InsUpdClients(EMClients objClients)
        {
            Hashtable htparams = new Hashtable
                                        {
                                            {"@ClientId",objClients.ClientId},
                                            {"@ClientType",objClients.ClientType},
                                            {"@ClientAccountNo",objClients.ClientAccountNo},
                                            {"@ClientName",objClients.ClientName},
                                            {"@IsActive",objClients.IsActive},
                                            {"@ClientGroup",objClients.ClientGroup},
                                            {"@ClientConsultant",objClients.ClientConsultant},
                                            {"@ClientDepartment",objClients.ClientDepartment},
                                            {"@ClientDivision",objClients.ClientDivision},
                                            {"@ClientVatRegNo",objClients.ClientVatRegNo},
                                            {"@ClientNoVatNo",objClients.ClientNoVatNo},
                                            {"@ClientTelephone",objClients.ClientTelephone},
                                            {"@ClientFax",objClients.ClientFax},
                                            {"@ClientContactPerson",objClients.ClientContactPerson},
                                            {"@ClientEmail",objClients.ClientEmail},
                                            {"@ClientPostalAddress",objClients.ClientPostalAddress},
                                            {"@ClientPhysicalAddress",objClients.ClientPhysicalAddress},
                                            {"@YearEnd",objClients.YearEnd},
                                            {"@Logo",objClients.Logo},
                                            {"@HtmlWidth",objClients.HtmlWidth},
                                            {"@HtmlHeight",objClients.HtmlHeight},
                                            {"@Align",objClients.Align},
                                            {"@IsDisableAutoPrintManual",objClients.IsDisableAutoPrintManual},
                                            {"@IsDisableAutoPrintTicket",objClients.IsDisableAutoPrintTicket},
                                            {"@DefaultOrderNo",objClients.DefaultOrderNo},
                                            {"@IsForceOrderNo",objClients.IsForceOrderNo},
                                            {"@IsForceExternalVoucher",objClients.IsForceExternalVoucher},
                                            {"@ClientDuplicateOrderNo",objClients.ClientDuplicateOrderNo},
                                            {"@ActionIsNoServiceFee",objClients.ActionIsNoServiceFee},
                                            {"@ClientOpenItemLoad",objClients.ClientOpenItemLoad},
                                            {"@ClientBroughtFwdBreakDwn",objClients.ClientBroughtFwdBreakDwn},
                                            {"@ClientLineItemBreakDwn",objClients.ClientLineItemBreakDwn},
                                            {"@ClientDbOrCd",objClients.ClientDbOrCd},
                                            {"@ClientSuppressNilVal",objClients.ClientSuppressNilVal},
                                            {"@ClientTotalCharge",objClients.ClientTotalCharge},
                                            {"@ClientCreditCard",objClients.ClientCreditCard},
                                            {"@ClientTransactionRef",objClients.ClientTransactionRef},
                                            {"@ClientAllocPaidItems",objClients.ClientAllocPaidItems},
                                            {"@ClientCustmDetail",objClients.ClientCustmDetail},
                                            {"@ClientExcludeFmPrint",objClients.ClientExcludeFmPrint},
                                            {"@ClientCreditLimit",objClients.ClientCreditLimit},
                                            {"@ClientLimitAction",objClients.ClientLimitAction},
                                            {"@ClientMaxInvoiceVal",objClients.ClientMaxInvoiceVal},
                                            {"@ClientCreditTerms",objClients.ClientCreditTerms},
                                            {"@ClientTermsAction",objClients.ClientTermsAction},
                                            {"@IsForce",objClients.IsForce},
                                            {"@ClientCalAir",objClients.ClientCalAir},
                                            {"@ClientCalLand",objClients.ClientCalLand},
                                            {"@ClientDirectSettleTrans",objClients.ClientDirectSettleTrans},
                                            {"@ClientCCForCashTrans",objClients.ClientCCForCashTrans},
                                            {"@IsTicketOrLandCreditCard",objClients.IsTicketOrLandCreditCard},
                                            {"@CreditCardImportedTickets",objClients.CreditCardImportedTickets},
                                            {"@ClientNotes",objClients.ClientNotes},
                                            {"@ClientMessage",objClients.ClientMessage},
                                            {"@CreatedBy",objClients.CreatedBy},
                                            {"@LogoPath",objClients.LogoPath},
                                            {"@CContactKey",objClients.CContactKey},
                                            {"@CContactName",objClients.CContactName},
                                            {"@CPosition",objClients.CPosition},
                                            {"@CTelephone",objClients.CTelephone},
                                            {"@CEmailAddress",objClients.CEmailAddress},
                                            {"@CAutoMail",objClients.CAutoMail},
                                            {"@CCellNo",objClients.CCellNo},
                                            {"@CFax",objClients.CFax},
                                            {"@CDeactivate",objClients.CDeactivate},
                                            {"@ClientAccCode",objClients.ClientAccCode},
                                            {"@ClientTypeAccCode",objClients.ClientTypeAccCode}
                                            
                                        };
            int IsSuccess = ExecuteNonQuery("Clients_InsertUpdate", htparams, "@return");
            return IsSuccess;
        }

        public int DeleteClients(int ClientId)
        {
            Hashtable htparams = new Hashtable
                                         {
                                             {"@ClientId",ClientId}
                                         };
            return ExecuteNonQuery("Clients_Delete", htparams);
        }
        public DataSet GetClients(int ClientId)
        {
            Hashtable htparams = new Hashtable
                                          {
                                              {"@ClientId",ClientId},
                                          };
            return ExecuteDataSet("Clients_Get", htparams);
        }

        //Contact
        public int InsertUpdContact(EMClients objClients)
        {
            Hashtable htparams = new Hashtable{
                                       {"@ContactId",objClients.ContactId},
                                       {"@ContactKey",objClients.ContactKey},
                                       {"@ContactName",objClients.ContactName},
                                       {"@Position",objClients.Position},
                                       {"@Telephone",objClients.Telephone},
                                       {"@EmailAddress",objClients.EmailAddress},
                                       {"@AutoMail",objClients.AutoMail},
                                       {"@LSupplierId",objClients.LSupplierId},
                                       {"@ClientId",objClients.ClientId},
                                       {"@UserCategory",objClients.UserCategory},
                                       {"@ConCellNo",objClients.ConCellNo},
                                       {"@ConFax",objClients.ConFax},
                                       {"@ConDeactivate",objClients.ConDeactivate},
                                       
          };
            int IsSuccess = ExecuteNonQuery("ContactDetails_InsUpdate", htparams);
            return IsSuccess;
        }
        public int DeleteContact(int ContactId)
        {
            Hashtable htparams = new Hashtable{
                                          {"@ContactId",ContactId},
          };
            return ExecuteNonQuery("ContactDetails_Delete", htparams);
        }

        //CreditCard
        public int InsUpdCreditCardDetails(EMClients objClients)
        {
            Hashtable htparams = new Hashtable
                                          {
                                               {"@CreditCardId",objClients.CreditCardId},
                                               {"@CreditCardCode",objClients.CreditCardCode},
                                               {"@CreditCardType",objClients.CreditCardType},
                                               {"@CreditCardAccNo",objClients.CreditCardAccNo},
                                               {"@CreditCardAccHolder",objClients.CreditCardAccHolder},
                                               {"@CreditCardExpires",objClients.CreditCardExpires},
                                               {"@ClientId",objClients.ClientId}
                                         };
            int IsSuccess = ExecuteNonQuery("CreditCardDetails_InsUpdate", htparams);
            return IsSuccess;
        }

        public int DeleteCreditCardDetails(int CreditCardId)
        {
            Hashtable htparams = new Hashtable
                                          {
                                              {"@CreditCardId",CreditCardId}
                                          };
            return ExecuteNonQuery("CreditCardDetails_Delete", htparams);
        }
        public DataSet GetClientByClientType(string Clienttype)
        {
            Hashtable htparams = new Hashtable
                                          {
                                              {"@ClientType",Clienttype}
                                          };
            return ExecuteDataSet("Clients_get_byclienttype", htparams);
        }
       
        //CharteredAccounts

        public int InsUpdChartAccounts(EMClients objClients)
        {
            Hashtable htparams = new Hashtable
                                    {
                                         {"@ChartedAccId",objClients.ChartedAccId},
                                         {"@ChartedAccName",objClients.ChartedAccName},
                                         {"@ChartedMasterAccName",objClients.ChartedMasterAccName},
                                         {"@Type",objClients.Type},
                                         {"@AccCode",objClients.AccCode},
                                         {"@CompanyId",objClients.CompanyId},
                                         {"@BranchId",objClients.BranchId},
                                         {"@CreatedBy",objClients.CreatedBy},
                                         {"@DepartmentId",objClients.DepartmentId},
                                         {"@BaseCurrency",objClients.BaseCurrency},
                                         {"@TranCurrency",objClients.TranCurrency},
                                         {"@CategoryId",objClients.CategoryId},
                                         {"@RefType",objClients.RefType},
                                         {"@RefId",objClients.RefId},
                                         {"@IsClient",objClients.IsClient}
                                    };
            int IsSuccess = ExecuteNonQuery("CharteredAccounts_Insert", htparams);
            return IsSuccess;
        }
        //CharteredAccounts
        public int UpdChartAccounts(EMClients objClients)
        {
            Hashtable htparams = new Hashtable
                                    {
                                         {"@ChartedAccName",objClients.ChartedAccName},
                                          {"@RefType",objClients.RefType},
                                          {"@RefId",objClients.RefId},
                                    };
            int IsSuccess = ExecuteNonQuery("CharteredAccounts_Update", htparams);
            return IsSuccess;
        }
    }
}
