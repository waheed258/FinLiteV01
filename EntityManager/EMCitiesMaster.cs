using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityManager
{
   public class EMCitiesMaster
    {
       public int Id { get; set; }

       public string Name { get; set; }
       public int CountryId { get; set; }
       public int state_id { get; set; }
       public string CityKey { get; set; }
       public string CityTimezoneUtc { get; set; }
       public int CreatedBy { get; set; }
    }
}
