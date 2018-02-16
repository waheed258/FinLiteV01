using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataManager;
using System.Data;

namespace BusinessManager
{
 public  class BALServicefee
    {

     DALServicefee _Dalobject = new DALServicefee();
     public DataSet BindPassengerNames(string Tempuniqcode,int AirTickNo)
     {
         return _Dalobject.BindPassengerNames(Tempuniqcode, AirTickNo);
     }

     public Object GetSFMergeAmount(int ticktId, int merge)
     {
         return _Dalobject.GetSFMergeAmount(ticktId, merge);

     }

    }
}
