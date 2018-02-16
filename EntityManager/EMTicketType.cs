using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityManager
{
    public class EMTicketType
    {
        public int TId { get; set; }
        public string TKey { get; set; }
        public string TDesc { get; set; }
        public int CreatedBy { get; set; }
    }
}
