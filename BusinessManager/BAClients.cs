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
    public class BAClients
    {
        DAClients objDAClients = new DAClients();

        public int InsUpdClients(EMClients objClients)
        {
            return objDAClients.InsUpdClients(objClients);
        }

        public int DeleteClients(int ClientId)
        {
            return objDAClients.DeleteClients(ClientId);
        }
        public DataSet GetClients(int ClientId)
        {
            return objDAClients.GetClients(ClientId);
        }

        //Contact
        public int InsertUpdContact(EMClients objClients)
        {
            return objDAClients.InsertUpdContact(objClients);
        }

        public int DeleteContact(int ContactId)
        {
            return objDAClients.DeleteContact(ContactId);
        }

        //credidcard
        public int InsUpdCreditCardDetails(EMClients objClients)
        {
            return objDAClients.InsUpdCreditCardDetails(objClients);
        }

        public int DeleteCreditCardDetails(int CreditCardId)
        {
            return objDAClients.DeleteCreditCardDetails(CreditCardId);
        }
        public DataSet GetClientByClientType(string Clienttype)
        {
            return objDAClients.GetClientByClientType(Clienttype);
        }
      
        //CharteredAccounts

        public int InsUpdChartAccounts(EMClients objClients)
        {
            return objDAClients.InsUpdChartAccounts(objClients);
        }
        //Charted Account name Update 
        public int UpdateChartAccounts(EMClients objClients)
        {
            return objDAClients.UpdChartAccounts(objClients);
        }
    }
}
