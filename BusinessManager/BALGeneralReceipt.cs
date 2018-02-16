using DataManager;
using EntityManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManager
{
  public class BALGeneralReceipt
    {
      DALGeneralReceipt ObjDALGeneralReceipt = new DALGeneralReceipt();
      public int InsertGeneralReceipt(EMGeneralReceipt objEMGeneralReceipt)
        {
            return ObjDALGeneralReceipt.InsertGeneralReceipt(objEMGeneralReceipt);
        }
      public DataSet GetRecipts()
      {
          return ObjDALGeneralReceipt.GenReceiptsGet();
      }
    }
}
