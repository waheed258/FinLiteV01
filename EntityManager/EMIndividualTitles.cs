using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityManager
{
    public class EMIndividualTitles
    {
        public int TitleId { get; set; }
        public string TitleKey { get; set; }
        public string TitleDescription { get; set; }
        public string Gender { get; set; }
        public int CreatedBy { get; set; }
    }
}
