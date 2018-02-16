using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataManager;
using System.Data;
using EntityManager;

namespace BusinessManager
{
   
   public class BAGeneralPayment
    {
       DAGeneralPayment _objDAgenpaymt = new DAGeneralPayment();
       public DataSet Get_Category()
       {
           return _objDAgenpaymt.Get_Category();

       }
       public DataSet GeneralPayment_list()
       {
           return _objDAgenpaymt.GeneralPayment_List();

       }
       public DataSet Get_CategoryAccCode(int categoryId)
       {
           return _objDAgenpaymt.Get_CategoryAccCode(categoryId);

       }

       public DataSet Get_MainAccCode(int chartedAccId)
       {
           return _objDAgenpaymt.Get_MainAccCode(chartedAccId);

       }

       public int InsertGeneralPaymrnt(EMGeneralPayment objEmGeneralPayment)
       {
           return _objDAgenpaymt.InsertGeneralPaymrnt(objEmGeneralPayment);

       }

    }
}
