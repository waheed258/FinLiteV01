using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityManager
{
    public class EMGeneralReceipt
    {
        public DateTime? GRDate { get; set; }
        public string GRSourceRef { get; set; }
        public string GRPrepairedby { get; set; }
        public decimal GRIncAmount { get; set; }

        public int GRDivision { get; set; }
        public int GRVat { get; set; }
        public int GRReceiptType { get; set; }
        public decimal GRVatAmount { get; set; }
        public int GRAutoDepositto { get; set; }
        public decimal GRExclAmount { get; set; }
        public int GRAccountNo { get; set; }
        public string GRPayDetails { get; set; }
        public string GRDetails { get; set; }
        public int GRDivision1 { get; set; }
        public int GRConsultant { get; set; }
        public int GRClient { get; set; }
        public int GRSupplier { get; set; }
        public int GRSeerviceType { get; set; }
        public int GRMessageType { get; set; }
        public string GRMessage { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }




    }
}
