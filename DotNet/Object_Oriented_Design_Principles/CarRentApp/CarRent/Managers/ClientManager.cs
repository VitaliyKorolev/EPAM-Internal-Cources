using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRent
{
    public class ClientManager : IClientManager
    {
        private Dictionary<Client, List<RentRecord>> clientsHistory;
        public ClientManager()
        {
            clientsHistory = new Dictionary<Client, List<RentRecord>>(new ClientEqComparer())
            {
                [new Client("Andrey", "Baranov", "B1")] = new List<RentRecord>(),
                [new Client("Valeriy", "Petrov", "B1")] = new List<RentRecord>(),
                [new Client("Nikita", "Vinogradov", "B1")] = new List<RentRecord>(),
                [new Client("Andrey", "Vlasov", "B1")] =  new List<RentRecord>()
            };
        }

        public List<Client> GetClients()
        {
            return new List<Client>(clientsHistory.Keys);
        }
        public void AddNewClient(Client client)
        {
            clientsHistory.Add(client, new List<RentRecord>());
        }
        public void DeleteClient(Client client)
        {
            clientsHistory.Remove(client);
        }
        public bool AddCarRentRecord(Client client, RentRecord carBooking)
        {
            if (!clientsHistory.ContainsKey(client))
            {
                return false;
            }
            clientsHistory[client].Add(carBooking);
            return true;
        }

        public List<RentRecord> GetClientHistory(Client client)
        {
            return clientsHistory[client];
        }
    }
}
