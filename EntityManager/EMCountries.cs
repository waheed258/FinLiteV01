using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityManager
{
    public class EMCountries
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CountryKey { get; set; }
        public int Continent { get; set; }
        public string TimeZoneUTC { get; set; }
        public int DialCode { get; set; }
        public decimal VATOrGSTRate { get; set; }
        public int TravelCategory { get; set; }
        public int CreatedBy { get; set; }
    }
}
