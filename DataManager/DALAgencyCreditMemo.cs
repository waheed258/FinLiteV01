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
   public class DALAgencyCreditMemo :DataUtilities
    {
        public int InsertAgencycreditmemo(EMAgencyCreditMemo objEMAgencycreditmemo)
        {
            Hashtable htparams = new Hashtable
                                            {
                                                {"@IsCreditNote",objEMAgencycreditmemo.IsCreditNote},
                                                {"@CreditMemoRevId",objEMAgencycreditmemo.CreditMemoRevId},
                                               {"@AcmNo",objEMAgencycreditmemo.AcmNo},
                                              {"@TicketNo",objEMAgencycreditmemo.TicketNo},
                                              {"@Supplier",objEMAgencycreditmemo.Supplier},
                                              {"@Type",objEMAgencycreditmemo.Type},
                                              {"@PassengerName",objEMAgencycreditmemo.PassengerName},
                                              {"@ExcluFare",objEMAgencycreditmemo.ExcluFare},
                                              {"@FareVat",objEMAgencycreditmemo.FareVat},
                                              {"@Commission",objEMAgencycreditmemo.Commission},
                                              {"@DepatureTaxes",objEMAgencycreditmemo.DepatureTaxes},
                                              {"@AgentVat",objEMAgencycreditmemo.AgentVat},
                                              {"@ClientTotal",objEMAgencycreditmemo.ClientTotal},
                                              {"@BSP",objEMAgencycreditmemo.BSP},
                                           //   {"@CreditCard",objEMAgencycreditmemo.CreditCard},
                                              {"@ProInvId",objEMAgencycreditmemo.ProInvId},
                                              {"@InvId",objEMAgencycreditmemo.InvId},
                                              {"@CompanyId",objEMAgencycreditmemo.CompanyId},
                                              {"@BranchId",objEMAgencycreditmemo.BranchId},
                                              {"@CreatedBy",objEMAgencycreditmemo.CreatedBy},
                                              {"@CreatedOn",objEMAgencycreditmemo.CreatedOn},
                                              {"@UpdatedBy",objEMAgencycreditmemo.UpdatedBy},
                                              {"@UpdatedON",objEMAgencycreditmemo.UpdatedON},
                                              {"@AcmTempUniqId",objEMAgencycreditmemo.AcmTempUniqId},
                                              {"@InvoiceType",objEMAgencycreditmemo.InvoiceType}

                                            };

            int IsSuccess = ExecuteNonQuery("AgencyCreditMemo_Insert", htparams);
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
    }
}
