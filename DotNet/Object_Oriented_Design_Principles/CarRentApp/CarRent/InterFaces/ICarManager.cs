using FilterLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRent
{
    public interface ICarManager
    {
        public List<Car> GetAllAviableCars();
        public bool GiveCarToClient(Client client, Car car);
        public bool GetCarFromClient(Client client, Car car);
        public Car GetAviableCar(Filter<Car> filter);
    }
}
