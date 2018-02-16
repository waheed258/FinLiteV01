using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityManager
{
    public  class EMNonUploadReasons
    {
        public int NonUploadId { get; set; }
        public string NonUploadKey { get; set; }
        public string NonUploadDesc { get; set; }
        public int CreatedBy { get; set; }
    }
}
