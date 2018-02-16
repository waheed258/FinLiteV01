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
   public class DALGeneralReceipt:DataUtilities
    {
       public int InsertGeneralReceipt(EMGeneralReceipt objEmGeneralReceipt)
        {
            Hashtable htparams = new Hashtable{



                                         {"@GRDate",objEmGeneralReceipt.GRDate},
                                         {"@GRSourceRef",objEmGeneralReceipt.GRSourceRef},
                                         {"@GRPrepairedby",objEmGeneralReceipt.GRPrepairedby},
                                         {"@GRIncAmount",objEmGeneralReceipt.GRIncAmount},
                                         {"@GRDivision",objEmGeneralReceipt.GRDivision},
                                         {"@GRVat",objEmGeneralReceipt.GRVat},
                                         {"@GRReceiptType",objEmGeneralReceipt.GRReceiptType},
                                         {"@GRVatAmount",objEmGeneralReceipt.GRVatAmount},
                                         {"@GRAutoDepositto",objEmGeneralReceipt.GRAutoDepositto},
                                         {"@GRExclAmount",objEmGeneralReceipt.GRExclAmount},
                                         {"@GRAccountNo",objEmGeneralReceipt.GRAccountNo},
                                         {"@GRPayDetails",objEmGeneralReceipt.GRPayDetails},
                                         {"@GRDetails",objEmGeneralReceipt.GRDetails},
                                         {"@GRDivision1",objEmGeneralReceipt.GRDivision1},
                                         {"@GRConsultant",objEmGeneralReceipt.GRConsultant},
                                         {"@GRClient",objEmGeneralReceipt.GRClient},
                                         {"@GRSupplier",objEmGeneralReceipt.GRSupplier},
                                         {"@GRSeerviceType",objEmGeneralReceipt.GRSeerviceType},
                                         {"@GRMessageType",objEmGeneralReceipt.GRMessageType},
                                         {"@GRMessage",objEmGeneralReceipt.GRMessage},
                                         {"@CreatedBy",objEmGeneralReceipt.CreatedBy},
                                         {"@UpdatedBy",objEmGeneralReceipt.UpdatedBy}
          };
            return ExecuteNonQuery("GeneralReceipt_Insert", htparams);
        }
       public DataSet GenReceiptsGet()
       {

           return ExecuteDataSet("GeneralReceiptList");
       }
    }
}


