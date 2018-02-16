using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityManager
{
    public class EMSupplierChoice
    {
        public int SupplierChoiceId { get; set; }
        public string ChoiceKey { get; set; }
        public int ChoiceDeactivate { get; set; }
        public string ChoiceDescription { get; set; }
        public int CreatedBy { get; set; }
    }
}
