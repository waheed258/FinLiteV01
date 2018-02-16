using EntityManager;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataManager
{
   public class DALAgencyDebitMemo :DataUtilities
    {
         public int InsertAgencydebitmemo(EMAgencyDebitMemo objEMAgencydebitmemo)
        {
            Hashtable htparams = new Hashtable
                                            {
                                                {"@IsCreditNote",objEMAgencydebitmemo.IsCreditNote},
                                                {"@DebitMemoId",objEMAgencydebitmemo.DebitMemoId},
                                              {"@AdmNo",objEMAgencydebitmemo.AdmNo},
                                              {"@TicketNo",objEMAgencydebitmemo.TicketNo},
                                              {"@Supplier",objEMAgencydebitmemo.Supplier},
                                              {"@Type",objEMAgencydebitmemo.Type},
                                              {"@PassengerName",objEMAgencydebitmemo.PassengerName},
                                              {"@ExcluFare",objEMAgencydebitmemo.ExcluFare},
                                              {"@FareVat",objEMAgencydebitmemo.FareVat},
                                              {"@Commission",objEMAgencydebitmemo.Commission},
                                              {"@DepatureTaxes",objEMAgencydebitmemo.DepatureTaxes},
                                              {"@AgentVat",objEMAgencydebitmemo.AgentVat},
                                              {"@ClientTotal",objEMAgencydebitmemo.ClientTotal},
                                              {"@BSP",objEMAgencydebitmemo.BSP},
                                              {"@CreditCard",objEMAgencydebitmemo.CreditCard},
                                              {"@ProInvId",objEMAgencydebitmemo.ProInvId},
                                              {"@InvId",objEMAgencydebitmemo.InvId},
                                              {"@CompanyId",objEMAgencydebitmemo.CompanyId},
                                              {"@BranchId",objEMAgencydebitmemo.BranchId},
                                              {"@CreatedBy",objEMAgencydebitmemo.CreatedBy},
                                              {"@CreatedOn",objEMAgencydebitmemo.CreatedOn},
                                              {"@UpdatedBy",objEMAgencydebitmemo.UpdatedBy},
                                              {"@UpdatedON",objEMAgencydebitmemo.UpdatedON},
                                              {"@AdmTempUniqId",objEMAgencydebitmemo.AdmTempUniqId},
                                              {"@InvoiceType",objEMAgencydebitmemo.InvoiceType}
                                          };

            int IsSuccess = ExecuteNonQuery("AgencyDebitMemo_Insert", htparams);
            return IsSuccess;
        
        }
        //Type Select International or Domestic
        public DataSet Get_Type()
        {
            return ExecuteDataSet("[Type_Get]");
        }
        // If you select International Vat DD 14%
        public DataSet Get_Vat(int TypeId)
        {
            Hashtable htparams = new Hashtable
                                   {
                                       {"@TypeId",TypeId}
                                   };
            return ExecuteDataSet("Vat_Get", htparams);

        }

        //Payment Card Type VISA, AmericanExpress.
        public DataSet Get_CreditCard()
        {

            return ExecuteDataSet("[CreditCard_Get]");
        
        }
        public DataSet Get_AdmSuppliers()
        {
            return ExecuteDataSet("[LandSuppliers_Get]");
        }
    }
    }

