using System;
using System.Collections.Generic;
using CarRent;
using FilterLib;
using System.Xml.Linq;


namespace CarRentUI
{
    class Program
    {
        static void Main(string[] args)
        {
            IClientManager clientManager = new ClientManager();
            ICarManager carManager = new CarManagerXml(clientManager);

            Filter<Car> filter = new Filter<Car>();
            filter.PropertyEqualsToValue("RegistrationNumber", "j121et11");

            Car car = carManager.GetAviableCar(filter);
            Client client1 = clientManager.GetClients()[0];
            Client client2 = clientManager.GetClients()[1];

            bool b = carManager.GiveCarToClient(client1, car);
            bool f = carManager.GiveCarToClient(client2, car);
            carManager.GetCarFromClient(client1, car);
            var hist = clientManager.GetClientHistory(client1);
        }
    }
}
