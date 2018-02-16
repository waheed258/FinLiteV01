using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityManager
{
   public class EMContactNote
    {
       public int NotePadId { get; set; }
       public string NpName { get; set; }
       public string NpType { get; set; }
       public string NoteKey { get; set; }
       public int Deactivate { get; set; }
       public string HelpText { get; set; }
       public int AppToClients { get; set; }
       public int AppToPrincipals { get; set; }
       public int CreatedBy { get; set; }
    }
}
