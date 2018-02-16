using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityManager
{
    public class EMReceiptTypes
    {
        public int ReceiptId { get; set; }
        public string ReceiptKey { get; set; }
        public int Deactivate { get; set; }
        public string RecDescription { get; set; }
        public int DepListMethod { get; set; }
        public int BankAccount { get; set; }
        public int CreditCardType { get; set; }
        public int NewReceipt { get; set; }
        public int CreatedBy { get; set; }

    }
}
