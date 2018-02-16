using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityManager;
using System.Data;
using DataManager;

namespace BusinessManager
{
   public class BAReceiptType
    {
       DAReceiptType objDAReceipt = new DAReceiptType();
       public int InsUpdReceiptType(EMReceiptTypes objEmReceipt)
       {
           return objDAReceipt.InsUpdReceiptType(objEmReceipt);
       }
       public DataSet GetReceiptType(int ReceiptId)
       {
           return objDAReceipt.GetReceiptType(ReceiptId);
       }
       public int DeleteReceiptType(int ReceiptId)
       {
           return objDAReceipt.DeleteReceiptType(ReceiptId);
       }
    }
}
