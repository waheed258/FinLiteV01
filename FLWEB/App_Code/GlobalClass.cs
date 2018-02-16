using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for GlobalClass
/// </summary>
public class GlobalClass
{
	public GlobalClass()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    //public static string FormatTwoDecimal(string value)
    //{
    //    decimal decimalAmount = Convert.ToDecimal(value);
    //    return decimalAmount.ToString(string.Format("0.{0}", new string('0', 2)));
    //}

    public static string FormatFourDecimal(decimal value)
    {
        return value.ToString(string.Format("0.{0}", new string('0', 4)));
    }

    public static string exclVatSum(string exclusiveAmount,string vatPer)
    {
        decimal exclusiveDecimal = Convert.ToDecimal(exclusiveAmount);
        decimal exclusiveVatPer = Convert.ToDecimal(vatPer);
        decimal sumExclVat = (exclusiveDecimal * exclusiveVatPer) / 100;
        return sumExclVat.ToString(string.Format("0.{0}", new string('0', 2))); 
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