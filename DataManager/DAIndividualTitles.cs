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
   public class DAIndividualTitles:DataUtilities
    {
       public int InsUpdtIndivTitles(EMIndividualTitles objEMTitles)
       {
           Hashtable htparams = new Hashtable{ 
                                            {"@TitleId",objEMTitles.TitleId},
                                            {"@TitleKey",objEMTitles.TitleKey},
                                            {"@TitleDescription",objEMTitles.TitleDescription},
                                            {"@Gender",objEMTitles.Gender},
                                            {"@CreatedBy",objEMTitles.CreatedBy},
           };
           return ExecuteNonQuery("IndividualTitles_InsertUpdate", htparams);
       }

       public DataSet GetIndivTitle(int TitleId)
       {
           Hashtable htparams = new Hashtable{
                                            {"@TitleId",TitleId},
           };
           return ExecuteDataSet("IndividualTitles_Get", htparams);
       }

       public int DeleteIndivTitle(int TitleId)
       {
           Hashtable htparams = new Hashtable{
                                            {"@TitleId",TitleId},
           };
           return ExecuteNonQuery("IndividualTitles_Delete", htparams);
       }
    }
}
