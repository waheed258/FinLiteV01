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
   public class DAAirSuppliers:DataUtilities
    {
       public int InsUpdAirSupplier(EMAirSuppliers objSupplier)
       {
           Hashtable htParams = new Hashtable{
                                        {"@SupplierId",objSupplier.SupplierId},
                                        {"@SupAccountCode",objSupplier.SupAccountCode},
                                        {"@SupplierName",objSupplier.SupplierName},
                                        {"@IsActive",objSupplier.IsActive},
                                        {"@ServiceType",objSupplier.ServiceType},
                                        {"@Country",objSupplier.Country},
                                        {"@GroupId",objSupplier.GroupId},
                                        {"@StateId",objSupplier.StateId},
                                        {"@Division",objSupplier.Division},
                                        {"@City",objSupplier.City},
                                        {"@Consultant",objSupplier.Consultant},
                                        {"@Telephone",objSupplier.Telephone},
                                        {"@Fax",objSupplier.Fax},
                                        {"@CellNo",objSupplier.CellNo},
                                        {"@ContactNo",objSupplier.ContactNo},
                                        {"@Email",objSupplier.Email},
                                        {"@Web",objSupplier.Web},
                                        {"@PhysicalAddress",objSupplier.PhysicalAddress},
                                        {"@PostalAddress",objSupplier.PostalAddress},
                                        {"@VatRegNo",objSupplier.VatRegNo},
                                        {"@NoVatNo",objSupplier.NoVatNo},
                                        {"@ExtAcc",objSupplier.ExtAcc},
                                        {"@QuickTravelCode",objSupplier.QuickTravelCode},
                                        {"@Bank",objSupplier.Bank},
                                        {"@BranchCode",objSupplier.BranchCode},
                                        {"@BranchName",objSupplier.BranchName},
                                        {"@AccountNo",objSupplier.AccountNo},
                                        {"@AccountType",objSupplier.AccountType},
                                        {"@AccHolder",objSupplier.AccHolder},
                                        {"@QuickGIAccount",objSupplier.QuickGIAccount},
                                        {"@LedgerAccount",objSupplier.LedgerAccount},                                                                                
                                        {"@CommPercentage",objSupplier.CommPercentage},
                                        {"@ZeroCommission",objSupplier.ZeroCommission},
                                        {"@PaymentMethod",objSupplier.PaymentMethod},
                                        {"@ClientTaxInvoice",objSupplier.ClientTaxInvoice},
                                        {"@ClientInvoiceType",objSupplier.ClientInvoiceType},
                                        {"@PrinciTaxInvoice",objSupplier.PrinciTaxInvoice},
                                        {"@IgnDupInvoiceNo",objSupplier.IgnDupInvoiceNo},
                                        {"@AllocItemType",objSupplier.AllocItemType},
                                        {"@EconomyClass",objSupplier.EconomyClass},
                                        {"@BusinessClass",objSupplier.BusinessClass},
                                        {"@FirstClass",objSupplier.FirstClass},
                                        {"@Notes",objSupplier.Notes},
                                        {"@NoteType",objSupplier.NoteType},
                                        {"@CreatedBy",objSupplier.CreatedBy},
                                        {"@CommissMethod",objSupplier.CommissMethod},
                                        {"@SupplMainGIAccCode",objSupplier.SupplMainGIAccCode},
                                        

           };
           return ExecuteNonQuery("AirSuppliers_InsertUpdate", htParams, "@return");
           
       }

       public DataSet GetAirSuppliers(int SupplierId)
       {
           Hashtable htParams = new Hashtable{
                                        {"@SupplierId",SupplierId},
            };
           return ExecuteDataSet("AirSuppliers_Get", htParams);
       }

       public int DeleteAirSupplier(int SupplierId)
       {
           Hashtable htpParams = new Hashtable{
                                  {"@SupplierId",SupplierId},
           };
           return ExecuteNonQuery("AirSuppliers_Delete", htpParams);
       }

       //CharteredAccounts

       public int InsUpdChartAccounts(EMAirSuppliers objSupplier)
       {
           Hashtable htparams = new Hashtable
                                    {
                                         {"@ChartedAccId",objSupplier.ChartedAccId},
                                         {"@ChartedAccName",objSupplier.ChartedAccName},
                                         {"@ChartedMasterAccName",objSupplier.ChartedMasterAccName},
                                         {"@Type",objSupplier.Type},
                                         {"@AccCode",objSupplier.AccCode},
                                         {"@CompanyId",objSupplier.CompanyId},
                                         {"@BranchId",objSupplier.BranchId},
                                         {"@CreatedBy",objSupplier.CreatedBy},
                                         {"@DepartmentId",objSupplier.DepartmentId},
                                         {"@BaseCurrency",objSupplier.BaseCurrency},
                                         {"@TranCurrency",objSupplier.TranCurrency},
                                         {"@CategoryId",objSupplier.CategoryId},
                                         {"@RefType",objSupplier.RefType},
                                          {"@RefId",objSupplier.RefId},
                                    };
           int IsSuccess = ExecuteNonQuery("CharteredAccounts_Insert", htparams);
           return IsSuccess;
       }

       //CharteredAccounts Update
       public int UpdChartAccounts(EMAirSuppliers objSupplier)
       {
           Hashtable htparams = new Hashtable
                                    {
                                         {"@ChartedAccName",objSupplier.ChartedAccName},
                                          {"@RefType",objSupplier.RefType},
                                          {"@RefId",objSupplier.RefId},
                                    };
           int IsSuccess = ExecuteNonQuery("CharteredAccounts_Update", htparams);
           return IsSuccess;
       }
    }
}
