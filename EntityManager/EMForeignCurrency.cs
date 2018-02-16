using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityManager
{
    public class EMForeignCurrency
    {
        public int Id { get; set; }
        public string Description { get; set;}
        public string Key{get;set;}
        public int Action{get;set;}
        public int Template{get;set;}
        public int Deactivate { get; set; }
        public int CreatedBy { get; set; }
        
    }
}
