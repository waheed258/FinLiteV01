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
    public class BABookDestinations
    {
        DABookDestinations objDABookDestinations = new DABookDestinations();
        public int InsUpdBookDestinations(EMBookDestinations objBookDestinations)
        {
            return objDABookDestinations.InsUpdBookDestinations(objBookDestinations);
        }

        public int DeleteBookDestinations(int BookDestId)
        {
            return objDABookDestinations.DeleteBookDestinations(BookDestId);
        }
        public DataSet GetBookDestinations(int BookDestId)
        {
            return objDABookDestinations.GetBookDestinations(BookDestId);
        }
    }
}
