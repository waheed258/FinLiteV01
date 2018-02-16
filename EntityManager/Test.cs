using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityManager
{
   public class Test
    {
        public static string FormatDecimal(decimal value, int decimalSeparator)
        {
            return value.ToString(string.Format("0.{0}", new string('0', decimalSeparator)));
        }
        public static string DoFormat(double myNumber)
        {
            var s = string.Format("{0:0.00}", myNumber);

            if (s.EndsWith("00"))
            {
                return ((int)myNumber).ToString();
            }
            else
            {
                return s;
            }
        }
    }
}
