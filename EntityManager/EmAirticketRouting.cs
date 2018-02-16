using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityManager
{
  public  class EmAirticketRouting
    {
      public int ClassId { get; set; }
      public string Routing { get; set; }
      public string Miles { get; set; }
      public string Routs { get; set; }
      public string FlightNo { get; set; }
      public string Class { get; set; }
      public Nullable<DateTime> Date { get; set; }
     
      public int AirticketId { get; set; }

      public int InvoiceId { get; set; }

      public int CreatedBy { get; set; }

      public string TicketType { get; set; }
      public string TempUniqCode { get; set; }
    }
}
