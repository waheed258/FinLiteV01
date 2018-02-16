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
   public class BAContactNote
    {
       DAContactNote objDAContactNote = new DAContactNote();
       public int InsUpdContactNote(EMContactNote objEMContactNote)
       {
           return objDAContactNote.InsUpdContactNote(objEMContactNote);
       }
       public DataSet GetContactNote(int NotePadId)
       {
           return objDAContactNote.GetContactNote(NotePadId);
       }
       public int DeleteContactNote(int NotePadId)
       {
           return objDAContactNote.DeleteContactNote(NotePadId);
       }
    }
}
