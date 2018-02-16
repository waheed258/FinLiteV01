using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityManager
{
    public class EMBookDestinations
    {
        public int BookDestId { get; set; }
        public string BookDestKey { get; set; }
        public string BookDestName { get; set; }
        public int BookDestStatus { get; set; }
        public int CreatedBy { get; set; }
    }
}
