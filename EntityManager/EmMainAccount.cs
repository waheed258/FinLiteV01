using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityManager
{
   public class EmMainAccount
    {
       public int MainAccId { get; set; }
       public string MainAccCode { get; set; }
       public string MainAcName { get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        public int CreatedBy { get; set; }
        public int UpdateBy { get; set; }
        public DateTime CreatedON { get; set; }
        public DateTime UpdateON { get; set; }
        public int DepartmentId { get; set; }
        public int BaseCurrency { get; set; }
        public int TranCurrency { get; set; }
        public int AcType { get; set; }
        public int CategoryId { get; set; }
    }
}
