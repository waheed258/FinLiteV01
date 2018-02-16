using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using EntityManager;

namespace DataManager
{
  public class DAReceiptType:DataUtilities
    {
      public int InsUpdReceiptType(EMReceiptTypes objEmReceipt)
      {
          Hashtable htparams = new Hashtable{
                                         {"@ReceiptId",objEmReceipt.ReceiptId},
                                         {"@ReceiptKey",objEmReceipt.ReceiptKey},
                                         {"@Deactivate",objEmReceipt.Deactivate},
                                         {"@RecDescription",objEmReceipt.RecDescription},
                                         {"@DepListMethod",objEmReceipt.DepListMethod},
                                         {"@BankAccount",objEmReceipt.BankAccount},
                                         {"@CreditCardType",objEmReceipt.CreditCardType},
                                         {"@NewReceipt",objEmReceipt.NewReceipt},
                                         {"@CreatedBy",objEmReceipt.CreatedBy},
          };
          return ExecuteNonQuery("ReceiptType_InsertUpdate", htparams);
      }

      public DataSet GetReceiptType(int ReceiptId)
      {
          Hashtable htparams = new Hashtable{
                                           {"@ReceiptId",ReceiptId},
          };
          return ExecuteDataSet("ReceiptType_Get", htparams);
      }

      public int DeleteReceiptType(int ReceiptId)
      {
          Hashtable htparams = new Hashtable{
                                           {"@ReceiptId",ReceiptId},
          };
          return ExecuteNonQuery("ReceiptType_Delete", htparams);
      }
    }
}
