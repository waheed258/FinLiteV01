using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityManager
{
    public class EMBranch
    {
        public int BranchId { get;set; }
        public string BranchCode { get; set; }
        public string BranchName { get; set; }
        public int DeActivate { get; set; }
        public int city { get; set; }
        public int Province { get; set; }
        public int CreatedBy { get; set; }
        public string ContactName { get; set; }
        public string TelephoneNumber { get; set; }
        public string FaxNumber { get; set; }
        public string CellNumber { get; set; }
        public string EmailAddress { get; set; }
        public string WebAddress { get; set; }
        public string DOCEX { get; set; }
        public string PostalAddress { get; set; }
        public string PhysicalAddress { get; set;}
        public string Co_No { get; set; }
        public string IATA_No { get; set; }
        public string Vat_No { get; set; }
        public int ASATA_Member { get; set; }
        public int Print_Doc { get; set; }
        
    }
}
