using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityManager
{
    public class EMState
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }
        public int Country { get; set; }
        public int CreatedBy { get; set; }
    }
}
