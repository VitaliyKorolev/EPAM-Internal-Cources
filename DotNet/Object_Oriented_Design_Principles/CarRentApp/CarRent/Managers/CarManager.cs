using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FilterLib;

namespace CarRent
{
    public class CarManager : ICarManager
    {
        private List<Car> availableCars;
        private List<Car> occupiedCars;
        private IClientManager clientManager;
        public CarManager(IClientManager clientManager)
        {
            this.clientManager = clientManager;
            availableCars = new List<Car>()
            {
                new Car(4, 2.8, 12412, 2005, "h450tr45", 100000m, 1000m),
                new Car(6, 3.2, 43512, 2001, "j121et11", 100000m, 2000m),
                new Car(4, 1.8, 47612, 2015, "h684ol22", 100000m, 3000m),
                new Car(4, 1.6, 35412, 2006, "h754de44", 100000m, 4000m),
            };
            occupiedCars = new();
        }
        public List<Car> GetAllAviableCars()
        {
            return availableCars;
        }
        public Car GetAviableCar(Filter<Car> filter)
        {
            return filter.Apply(availableCars).First();
        }

        public bool GiveCarToClient(Client client, Car car)
        {
            if (!availableCars.Contains(car))
            {
                return false;
            }
            availableCars.Remove(car);
            occupiedCars.Add(car);
            clientManager.AddCarRentRecord(client, new RentRecord(car, true, DateTime.Now));
            return true;
        }

        public bool GetCarFromClient(Client client, Car car)
        {
            if (!occupiedCars.Contains(car))
            {
                return false;
            }
            availableCars.Add(car);
            occupiedCars.Remove(car);
            clientManager.AddCarRentRecord(client, new RentRecord(car, false, DateTime.Now));
            return true;
        }
    }
}
