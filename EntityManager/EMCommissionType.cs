using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityManager
{
    public class EMCommissionType
    {
        public int Id { get; set; }
        public string key { get; set; }
        public int Deactivate { get; set; }
        public string Desc { get; set; }
        public int Category { get; set; }
        public int LSCategory { get; set; }
        public int DType { get; set; }
        public decimal DComm { get; set; }
        public decimal DRate { get; set; }
        public string UDesc { get; set; }
        public int DVat { get; set; }
        public int NTFee { get; set; }
        public int ZUType { get; set; }
        public int Income { get; set; }
        public int OVat { get; set; }
        public int CreatedBy { get; set; }
    }
}
