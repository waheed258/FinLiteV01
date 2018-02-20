using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using EntityManager;


namespace DataManager
{
  public class DALandSuppliers:DataUtilities
    {
      public int InsUpdlandSupplier(EMLandSuppliers objEMLandSupp)
      {
          Hashtable htparams = new Hashtable{
                                        {"@LSupplierId",objEMLandSupp.LSupplierId},
                                        {"@LSupAccountCode",objEMLandSupp.LSupAccountCode},
                                        {"@LSupplierName",objEMLandSupp.LSupplierName},
                                        {"@LIsActive",objEMLandSupp.LIsActive},
                                        {"@LServiceType",objEMLandSupp.LServiceType},
                                        {"@LCountry",objEMLandSupp.LCountry},
                                        {"@LGroupId",objEMLandSupp.LGroupId},
                                        {"@LStateId",objEMLandSupp.LStateId},
                                        {"@LDivision",objEMLandSupp.LDivision},
                                        {"@LCity",objEMLandSupp.LCity},
                                        {"@LConsultant",objEMLandSupp.LConsultant},
                                        {"@LLatitute",objEMLandSupp.LLatitute},
                                        {"@LLongitude",objEMLandSupp.LLongitude},
                                        {"@LTelephone",objEMLandSupp.LTelephone},
                                        {"@LFax",objEMLandSupp.LFax},
                                        {"@LCellNo",objEMLandSupp.LCellNo},
                                        {"@LContactNo",objEMLandSupp.LContactNo},
                                        {"@LEmail",objEMLandSupp.LEmail},
                                        {"@LWeb",objEMLandSupp.LWeb},
                                        {"@LPhysicalAddress",objEMLandSupp.LPhysicalAddress},
                                        {"@LPostalAddress",objEMLandSupp.LPostalAddress},
                                        {"@LVatRegNo",objEMLandSupp.LVatRegNo},
                                        {"@LNoVatNo",objEMLandSupp.LNoVatNo},
                                        {"@LExtAcc",objEMLandSupp.LExtAcc},
                                        {"@LIATAReg",objEMLandSupp.LIATAReg},
                                        {"@LAlphaCode",objEMLandSupp.LAlphaCode},
                                        {"@LAmadeus",objEMLandSupp.LAmadeus},
                                        {"@LGalileo",objEMLandSupp.LGalileo},
                                        {"@LSabre",objEMLandSupp.LSabre},
                                        {"@LWorldSpan",objEMLandSupp.LWorldSpan},
                                        {"@LCarHire",objEMLandSupp.LCarHire},
                                        {"@LFrontDesk",objEMLandSupp.LFrontDesk},
                                        {"@LOtherPropertyNo",objEMLandSupp.LOtherPropertyNo},
                                        {"@LBank",objEMLandSupp.LBank},
                                        {"@LBranchCode",objEMLandSupp.LBranchCode},
                                        {"@LBranchName",objEMLandSupp.LBranchName},
                                        {"@LAccountNo",objEMLandSupp.LAccountNo},
                                        {"@LAccountType",objEMLandSupp.LAccountType},
                                        {"@LAccHolder",objEMLandSupp.LAccHolder},
                                        {"@LQuickGIAccount",objEMLandSupp.LQuickGIAccount},
                                        {"@LLedgerAccount",objEMLandSupp.LLedgerAccount},
                                        {"@LCommissMethod",objEMLandSupp.LCommissMethod},                                        
                                        {"@LCommPercentage",objEMLandSupp.LCommPercentage},
                                        {"@LZeroCommission",objEMLandSupp.LZeroCommission},
                                        {"@LPaymentMethod",objEMLandSupp.LPaymentMethod},
                                        {"@LClientTaxInvoice",objEMLandSupp.LClientTaxInvoice},
                                        {"@LClientInvoiceType",objEMLandSupp.LClientInvoiceType},
                                        {"@LPrinciTaxInvoice",objEMLandSupp.LPrinciTaxInvoice},
                                        {"@LIgnDupInvoiceNo",objEMLandSupp.LIgnDupInvoiceNo},
                                        {"@LAllocItemType",objEMLandSupp.LAllocItemType},                                        
                                        {"@LNotes",objEMLandSupp.LNotes},
                                        {"@LNoteType",objEMLandSupp.LNoteType},
                                        {"@CreatedBy",objEMLandSupp.CreatedBy},
                                        {"@LContactKey",objEMLandSupp.LContactKey},
                                        {"@LContactName",objEMLandSupp.LContactName},
                                        {"@LContactPosition",objEMLandSupp.LContactPosition},
                                        {"@LContTelephone",objEMLandSupp.LContTelephone},
                                        {"@LContEmailAddress",objEMLandSupp.LContEmailAddress},
                                        {"@LContAutoMail",objEMLandSupp.LContAutoMail},
                                        {"@ContactCellNo",objEMLandSupp.ContactCellNo},
                                        {"@ContactFax",objEMLandSupp.ContactFax},
                                        {"@ContactDeactivate",objEMLandSupp.ContactDeactivate},
                                        {"@SupplMainGIAccCode",objEMLandSupp.SupplMainGIAccCode},
                                        
                                        
          };
          return ExecuteNonQuery("LandSuppliers_InsertUpdate", htparams,"@return");
      }

      public DataSet GetLandSupplier(int LSupplierId)
      {
          Hashtable htparams = new Hashtable{
                                     {"@LSupplierId",LSupplierId},
          };
          return ExecuteDataSet("LandSuppliers_Get", htparams);
      }
      public int DeleteLandSupplier(int LSupplierId)
      {
          Hashtable htparams = new Hashtable{
                                          {"@LSupplierId",LSupplierId},
          };
          return ExecuteNonQuery("LandSupplier_DeleteData", htparams);
      }

      public int InsertUpdContact(EMLandSuppliers objEMLandSupp)
      {
          Hashtable htparams = new Hashtable{
                                       {"@ContactId",objEMLandSupp.ContactId},
                                       {"@ContactKey",objEMLandSupp.ContactKey},
                                       {"@ContactName",objEMLandSupp.ContactName},
                                       {"@Position",objEMLandSupp.Position},
                                       {"@Telephone",objEMLandSupp.Telephone},
                                       {"@EmailAddress",objEMLandSupp.EmailAddress},
                                       {"@AutoMail",objEMLandSupp.AutoMail},
                                       {"@LSupplierId",objEMLandSupp.LSupplierId},
                                       {"@ClientId",objEMLandSupp.ClientId},
                                       {"@UserCategory",objEMLandSupp.UserCategory},
                                       {"@ConCellNo",objEMLandSupp.ConCellNo},
                                       {"@ConFax",objEMLandSupp.ConFax},
                                       {"@ConDeactivate",objEMLandSupp.ConDeactivate},
          };
          return ExecuteNonQuery("ContactDetails_InsUpdate", htparams);
      }

      
      public int DeleteContact(int ContactId)
      {
          Hashtable htparams = new Hashtable{
                                          {"@ContactId",ContactId},
          };
          return ExecuteNonQuery("ContactDetails_Delete", htparams);
      }


      //CharteredAccounts

      public int InsUpdChartAccounts(EMLandSuppliers objEMLandSupp)
      {
          Hashtable htparams = new Hashtable
                                    {
                                         {"@ChartedAccId",objEMLandSupp.ChartedAccId},
                                         {"@ChartedAccName",objEMLandSupp.ChartedAccName},
                                         {"@ChartedMasterAccName",objEMLandSupp.ChartedMasterAccName},
                                         {"@Type",objEMLandSupp.Type},
                                         {"@AccCode",objEMLandSupp.AccCode},
                                         {"@CompanyId",objEMLandSupp.CompanyId},
                                         {"@BranchId",objEMLandSupp.BranchId},
                                         {"@CreatedBy",objEMLandSupp.CreatedBy},
                                         {"@DepartmentId",objEMLandSupp.DepartmentId},
                                         {"@BaseCurrency",objEMLandSupp.BaseCurrency},
                                         {"@TranCurrency",objEMLandSupp.TranCurrency},
                                          {"@CategoryId",objEMLandSupp.CategoryId},
                                          {"@RefType",objEMLandSupp.RefType},
                                          {"@RefId",objEMLandSupp.RefId},
                                          {"@IsClient",objEMLandSupp.IsClient}
                                    };
          int IsSuccess = ExecuteNonQuery("CharteredAccounts_Insert", htparams);
          return IsSuccess;
      }
      //CharteredAccounts Update
      public int UpdChartAccounts(EMLandSuppliers objEMLandSupp)
      {
          Hashtable htparams = new Hashtable
                                    {
                                         {"@ChartedAccName",objEMLandSupp.ChartedAccName},
                                          {"@RefType",objEMLandSupp.RefType},
                                          {"@RefId",objEMLandSupp.RefId},
                                    };
          int IsSuccess = ExecuteNonQuery("CharteredAccounts_Update", htparams);
          return IsSuccess;
      }
    }
}
