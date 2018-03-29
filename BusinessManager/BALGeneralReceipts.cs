using EntityManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataManager;
using BusinessManager;
using System.Data.SqlClient;
using System.Data;

namespace BusinessManager
{
  
   public class BALGeneralReceipts
    {
       DALGeneralReceipts _objDAGenReceipts = new DALGeneralReceipts();
       public int InsertGeneralReceipts(EMGeneralReceipts objEmGeneralReceipts)
       {
           return _objDAGenReceipts.InsertGeneralReceipt(objEmGeneralReceipts);

       }
       public DataSet Get_MainAccCode(int supplierId, string category)
       {
           return _objDAGenReceipts.Get_MainAccCode(supplierId, category);
       }
       public DataSet GetRecipts()
       {
           return _objDAGenReceipts.GenReceiptsGet();
       }

      //   To Accounts Bind
       public DataSet Get_GRMainAccCode(int supplierId,string category)
       {
           return _objDAGenReceipts.Get_MainAccCode(supplierId, category);

       }
    }
}
