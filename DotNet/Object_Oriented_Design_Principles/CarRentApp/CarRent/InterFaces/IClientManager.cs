using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRent
{
    public interface IClientManager
    {
        public List<Client> GetClients();
        public void AddNewClient(Client client);
        public void DeleteClient(Client client);
        public bool AddCarRentRecord(Client client, RentRecord carBooking);
        public List<RentRecord> GetClientHistory(Client client);
    }
}
