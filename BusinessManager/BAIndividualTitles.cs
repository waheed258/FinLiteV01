using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using EntityManager;
using DataManager;

namespace BusinessManager
{
   public class BAIndividualTitles
    {
       DAIndividualTitles objDATitles = new DAIndividualTitles();
       
       public int InsUpdtIndivTitles(EMIndividualTitles objEMTitles)
       {
           return objDATitles.InsUpdtIndivTitles(objEMTitles);
       }


       public DataSet GetIndivTitle(int TitleId)
       {
           return objDATitles.GetIndivTitle(TitleId);
       }

       public int DeleteIndivTitle(int TitleId)
       {
           return objDATitles.DeleteIndivTitle(TitleId);
       }

    }
}
