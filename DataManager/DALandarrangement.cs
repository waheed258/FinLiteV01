using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityManager;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace DataManager
{
    public class DALandarrangement : DataUtilities
    {

        public int InsertLandArrangement(EMLandarrangement objLandarrangements)
        {
            Hashtable htparams = new Hashtable
                                            {
                                                {"@IsCreditNote",objLandarrangements.IsCreditNote},
                                                {"@LandArrId",objLandarrangements.LandArrId},
                                               {"@LandSupplier",objLandarrangements.LandSupplier},
	                                           {"@Services",objLandarrangements.Services},
	                                           {"@Type",objLandarrangements.Type},
	                                           {"@PassengerName",objLandarrangements.PassengerName},
	                                           {"@TravelFrDate",objLandarrangements.TravelFrDate},
	                                           {"@TravelToDate",objLandarrangements.TravelToDate},
	                                           {"@BookRefNo",objLandarrangements.BookRefNo},
	                                           {"@Voucher",objLandarrangements.Voucher},
	                                           {"@SuppRef",objLandarrangements.SuppRef},
	                                           {"@SuppInvNo",objLandarrangements.SuppInvNo},
	                                           {"@TotIncl",objLandarrangements.TotIncl},
	                                           {"@ExclVatPer",objLandarrangements.ExclVatPer},
                                               {"@ExclusiveVatAmount",objLandarrangements.ExclusiveVatAmount},
	                                           {"@VatPer",objLandarrangements.VatPer},
	                                           {"@TotExclu",objLandarrangements.TotExclu},
	                                           {"@Payment",objLandarrangements.Payment},
	                                           {"@CreditCard",objLandarrangements.CreditCard},
	                                           {"@CO2Emis",objLandarrangements.CO2Emis},
	                                           {"@Units",objLandarrangements.Units},
	                                           {"@RateInclu",objLandarrangements.RateInclu},
	                                           {"@CommissionableInclu",objLandarrangements.CommissionableInclu},
	                                           {"@CommabExclu",objLandarrangements.CommabExclu},
	                                           {"@OthCommabInclu",objLandarrangements.OthCommabInclu},
	                                           {"@TotCommabInclu",objLandarrangements.TotCommabInclu},
	                                           {"@NonCommabInclu",objLandarrangements.NonCommabInclu},
	                                           {"@CommPer",objLandarrangements.CommPer},
	                                           {"@CommissionInclu",objLandarrangements.CommissionInclu},
	                                           {"@CommissionExclu",objLandarrangements.CommissionExclu},
	                                           {"@CommissionVatPer",objLandarrangements.CommissionVatPer},
	                                           {"@CommVatAmount",objLandarrangements.CommVatAmount},
	                                           {"@DueTOSupplier",objLandarrangements.DueTOSupplier},
                                               {"@DueToClient",objLandarrangements.DueToClient},
                                               {"@LessCommission",objLandarrangements.LessCommission},
                                               {"@LandProInvId",objLandarrangements.ProInvId},
	                                           {"@LandInvId",objLandarrangements.LandInvId},
	                                           {"@CompanyId",objLandarrangements.CompanyId},
	                                           {"@T5ID",objLandarrangements.T5ID},
	                                           {"@StatusId",objLandarrangements.StatusId},
	                                           {"@BranchId",objLandarrangements.BranchId},
	                                           {"@CreatedBy",objLandarrangements.CreatedBy},
	                                           {"@UpdateBy",objLandarrangements.UpdatedBy},
	                                           {"@CreatedON",objLandarrangements.CreatedON},
	                                           {"@UpdateON",objLandarrangements.UpdatedON},
                                                {"@LandTempUniqCode",objLandarrangements.LandTempUniqCode},
                                                 {"@InvoiceType",objLandarrangements.InvoiceType},
                                                  {"@SupplOpeningAmount",objLandarrangements.SupplOpenAmount},
                                                   {"@InvDocumentNo",objLandarrangements.invDocumentNo},
                                                     {"@RefundAmount",objLandarrangements.RefundAmount}
                                          };

            int IsSuccess = ExecuteNonQuery("Landarrangements_Insert", htparams);
            return IsSuccess;
        }

        // Land Supplier Name Data Set
        public DataSet Get_LandSuppliers( int landSupId)
        {
            return ExecuteDataSet("[LandSuppliers_Get]");
        }
        //Land Service set
        public DataSet Get_LandService()
        {
            return ExecuteDataSet("[LandService_Get]");
        }
        // Type Dataset Ex- Domestic , International 
        public DataSet Get_Type()
        {
            return ExecuteDataSet("[Type_Get]");
        }

        // Payment Method Get Ex:- CreditCard, Cash
        public DataSet Get_PaymentType()
        {


            return ExecuteDataSet("[PaymentType_Get]");
        }


        //Payment Card Type VISA, AmericanExpress.
        public DataSet Get_CreditCard()
        {

            return ExecuteDataSet("CreditCardTypeMaster_GetData");
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
