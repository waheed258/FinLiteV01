using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityManager
{
    public class EMVatType
    {
        public int VatId { get; set; }
        public string VatKey { get; set; }
        public string VatDesc { get; set; }
        public string VatRate { get; set; }
        public string VatEffDate { get; set; }
        public string VatAppTo { get; set; }
        public string VatGICode { get; set; }
        public int CreatedBy { get; set; }
        
    }
}
