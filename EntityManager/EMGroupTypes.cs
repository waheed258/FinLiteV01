using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityManager
{
     public class EMGroupTypes
    {
         public int Id { get; set; }
         public string Name { get; set; }
         public string Code { get; set; }
         public string GroupType { get; set; }
         public int CompanyId { get; set; }
         public int BranchId { get; set; }
         public int CreatedBy { get; set; }
       
    }
}
