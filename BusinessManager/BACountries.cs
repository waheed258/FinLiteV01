using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityManager;
using DataManager;
using System.Data;

namespace BusinessManager
{
    public class BACountries
    {
        DACountries objDACountries = new DACountries();
        public int InsUpdCountries(EMCountries objCountries)
        {
            return objDACountries.InsUpdCountries(objCountries);
        }
        public DataSet GetCountries(int Id)
        {
            return objDACountries.GetCountries(Id);
        }

        public int DeleteCountries(int Id)
        {
            return objDACountries.DeleteCountries(Id);
        }
    }
}
