using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityManager
{
    public class EMCarbonCoefficients
    {
        public int CarbonId { get; set; }
        public string CarbonKey { get; set; }
        public string CarbonDesc { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public decimal Cofficient { get; set; }
        public int CreatedBy { get; set; }
        
    }
}
