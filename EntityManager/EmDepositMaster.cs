using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityManager
{
   public  class EmDepositMaster
    {
        public DateTime DepositDate { get; set; }
        public string DepositSourceRef { get; set; }
        public int DepositRecieptType { get; set; }
        public string DepositConsultant { get; set; }
        public int DepositClientPrefix { get; set; }
        public string DepositComments { get; set; }
        public decimal TotalRecieptsDeposited { get; set; }
        public int CreatedBy { get; set; }

        public decimal TotalDepositAmount { get; set; }
        public int DepositAcId { get; set; }
    }
}
