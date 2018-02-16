using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityManager
{
    public  class EMConsultant
    {
        public int ConsultantId { get; set; }
        public string KeyId { get; set;}
        public string Name { get; set; }
        public int GroupId { get; set; }
        public int Division { get; set;}
        public string Email { get; set; }
        public string TelephoneNo { get; set; }
        public string CellNo { get; set;}
        public string FaxNo { get; set; }
        public int ClientType { get; set; }
        public int ClientStutus { get; set;}
        public int CreatedBy { get; set; }


        #region CharteredAccounts
        public int ChartedAccId { get; set; }
        public string ChartedAccName { get; set; }
        public int ChartedMasterAccName { get; set; }
        public string Type { get; set; }
        public string AccCode { get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        public int DepartmentId { get; set; }
        public int BaseCurrency { get; set; }
        public int TranCurrency { get; set; }
        public int CategoryId { get; set; }
        public string RefType { get; set; }
        public string RefId { get; set; }

        #endregion CharteredAccounts
    }
}
