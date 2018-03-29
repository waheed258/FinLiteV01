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
   public class DALGeneralReceipts :DataUtilities
    {
       public int InsertGeneralReceipt(EMGeneralReceipts objGeneralReceipts)
       {
           Hashtable htparams = new Hashtable{
                                            {"@GeneralReceiptId",objGeneralReceipts.GeneralReceiptId},
                                            {"@GRPaymentDate",objGeneralReceipts.GRPaymentDate},
                                            {"@GRPreparedBy",objGeneralReceipts.GRPreparedBy},
                                            {"@GRCategoryID",objGeneralReceipts.GRCategoryID},
                                            {"@GRFromAccount",objGeneralReceipts.GRFromAccount},
                                            {"@ToAccountID",objGeneralReceipts.ToAccountID},
                                            {"@GRSupplierMainAccCode",objGeneralReceipts.GRSupplierMainAccCode},
                                            {"@GRSupplierFromAccCode",objGeneralReceipts.GRSupplierFromAccCode},
                                            {"@GRPaymentAmount",objGeneralReceipts.GRPaymentAmount},
                                            {"@CreatedBy",objGeneralReceipts.CreatedBy},
            };
           return ExecuteNonQuery("[GeneralReceipts_Insert]", htparams);
       }
       public DataSet Get_MainAccCode(int supplierId,string category)
       {
           Hashtable htparams = new Hashtable
                                            {
                                                {"@SupplierId",supplierId},
                                                {"@category",category},

                                            };
           return ExecuteDataSet("GeneralReceipts_GetMainAccCode", htparams);
       }

       public DataSet GenReceiptsGet()
       {

           return ExecuteDataSet("GeneralReceiptsList");
       }

    }
}
