using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Collections;
using EntityManager;

namespace DataManager
{
   public class DALSupplierChoice:DataUtilities
    {
       public int InsUpdSuppChoice(EMSupplierChoice objEMSupChoice)
       {
           Hashtable htparams = new Hashtable{
                                            {"@SupplierChoiceId",objEMSupChoice.SupplierChoiceId},
                                            {"@ChoiceKey",objEMSupChoice.ChoiceKey},
                                            {"@ChoiceDeactivate",objEMSupChoice.ChoiceDeactivate},
                                            {"@ChoiceDescription",objEMSupChoice.ChoiceDescription},
                                            {"@CreatedBy",objEMSupChoice.CreatedBy},
           };
           return ExecuteNonQuery("SupplierChoice_InsertUpdate", htparams);
       }

       public DataSet GetSuppChoice(int SupplierChoiceId)
       {
           Hashtable htparams = new Hashtable{
                                            {"@SupplierChoiceId",SupplierChoiceId},
           };
           return ExecuteDataSet("SupplierChoice_Get", htparams);
       }

       public int DeleteSuppChoice(int SupplierChoiceId)
       {
           Hashtable htparams = new Hashtable
                                          {
                                              {"@SupplierChoiceId",SupplierChoiceId},
                                          };
           return ExecuteNonQuery("SupplierChoice_Delete", htparams);
       }
    }
}
