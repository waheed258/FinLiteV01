using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using EntityManager;

namespace DataManager
{
    public class DAContactNote:DataUtilities
    {
        public int InsUpdContactNote(EMContactNote objEMContactNote)
        {
            Hashtable htparams = new Hashtable{
                                         {"@NotePadId",objEMContactNote.NotePadId},
                                         {"@NpName",objEMContactNote.NpName},
                                         {"@NpType",objEMContactNote.NpType},
                                         {"@NoteKey",objEMContactNote.NoteKey},
                                         {"@Deactivate",objEMContactNote.Deactivate},
                                         {"@HelpText",objEMContactNote.HelpText},
                                         {"@AppToClients",objEMContactNote.AppToClients},
                                         {"@AppToPrincipals",objEMContactNote.AppToPrincipals},
                                         {"@CreatedBy",objEMContactNote.CreatedBy},
            };
            return ExecuteNonQuery("NotePads_InsertUpdate", htparams);
        }

        public DataSet GetContactNote(int NotePadId)
        {
            Hashtable htparams = new Hashtable{
                                        {"@NotePadId",NotePadId},
            };
            return ExecuteDataSet("Notepads_GetData", htparams);
        }

        public int DeleteContactNote(int NotePadId)
        {
            Hashtable htparams = new Hashtable{
                                        {"@NotePadId",NotePadId},
            };
            return ExecuteNonQuery("NotePads_Delete", htparams);
        }
    }
}
