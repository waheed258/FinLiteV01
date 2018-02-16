using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityManager
{
  public class EMAirport
    {
      public int AirportId { get; set; }
      public int Deactivate { get; set; }
      public string AirKey { get; set; }
      public string AirportName { get; set; }
      public int AirCity { get; set; }
      public int AirCountry { get; set; }
      public int CountryDetails { get; set; }
      public int CreatedBy { get; set; }
    }
}
