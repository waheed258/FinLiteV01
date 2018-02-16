using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityManager
{
   public class EMContactLog
    {
       public int LogId { get; set; }
       public string LogKey { get; set; }
       public string LogDescription { get; set; }
       public int CreatedBy { get; set; }
    }
}
