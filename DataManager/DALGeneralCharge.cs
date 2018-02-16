using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityManager;
using System.Collections;
using EntityManager;
using System.Data;

namespace DataManager
{
    public class DALGeneralCharge : DataUtilities
    {
       public int InsertGencharge(EMGeneralCharge objGeneralCharge)
       {
           Hashtable htParams = new Hashtable
                                     {
                                       
                                         {"@GenChgId",objGeneralCharge.GenChgId},
                                         {"@Type",objGeneralCharge.Type},
                                         {"@PassengerName",objGeneralCharge.PassengerName}, 
                                         {"@Details",objGeneralCharge.Details},
                                         {"@Units",objGeneralCharge.Units},                                                                             
                                         {"@RateNet",objGeneralCharge.RateNet},                                        
                                         {"@ExcluAmt",objGeneralCharge.ExcluAmt},
                                         {"@VatPer",objGeneralCharge.VatPer},
                                         {"@VatAmount",objGeneralCharge.VatAmount},
                                         {"@ClientTot",objGeneralCharge.ClientTot},
                                         {"@CreditCard",objGeneralCharge.CreditCard},
                                         {"@GCProInvId",objGeneralCharge.GCProInvId},
                                         {"@InvId",objGeneralCharge.GenChgId},
                                         {"@CompanyId",objGeneralCharge.CompanyId},                                        
                                         {"@BranchId",objGeneralCharge.BranchId},
                                         {"@CreatedBy",objGeneralCharge.CreatedBy},
                                         {"@GenTempUniqCode",objGeneralCharge.GenTempUniqCode},
                                         {"@InvoiceType",objGeneralCharge.InvoiceType},
                                           {"@InvDocumentNo",objGeneralCharge.invDocumentNo}
                                         
                                     };
           return ExecuteNonQuery("GeneralCharge_InsertUpdate", htParams);
       }

       public DataSet BindGenchargeType()
       {

           return ExecuteDataSet("Generalcharge_getType");
       }

     public object getVatPercentage(int GenChargeId)
    {
        Hashtable htParams = new Hashtable
                                     {
                                       
                                         {"@GenChgId",GenChargeId}
                                     };

        return ExecuteScalar("Vat_getChnChrgeVatper", htParams);
    }
        

    }
}
