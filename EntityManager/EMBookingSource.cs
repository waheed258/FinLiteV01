using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityManager
{
    public class EMBookingSource
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public int Deactivate { get; set; }
        public string Desc { get; set; }
        public int CreatedBy { get; set; }
    }
}
