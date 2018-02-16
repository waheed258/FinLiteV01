using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityManager
{
    public class EMCashBook
    {
        public int CashBookId { get; set; }
        public string CashBookKey { get; set; }
        public string CashDescription { get; set; }
        public int Deactivate { get; set; }
        public int DefaultAction { get; set; }
        public string GICode { get; set; }
        public string ReferenceFormat { get; set; }
        public string VatCodes { get; set; }
        public int CreatedBy { get; set; }
    }
}
