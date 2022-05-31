using FilterLib;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Configuration;

namespace CarRent
{
    public class CarManagerXml : ICarManager
    {
        private List<Car> occupiedCars;
        private IClientManager clientManager;
        private readonly string path;
        public CarManagerXml(IClientManager clientManager)
        {
            path = ConfigurationManager.AppSettings["path"];
            if (string.IsNullOrEmpty(path))
                throw new ConfigurationErrorsException("Error reading app settings");

            this.clientManager = clientManager;
            occupiedCars = new();
        }
        public List<Car> GetAllAviableCars()
        {
            XDocument xdoc = XDocument.Load(path);
            var c = double.Parse(xdoc.Root.Element("Car").Element("EngineCapacity").Value, CultureInfo.InvariantCulture);
            var cars = xdoc.Root.Elements("Car").Select(c => new Car(
                int.Parse(c.Element("NumberOfPassengers").Value),
                double.Parse(c.Element("EngineCapacity").Value, CultureInfo.InvariantCulture),
                double.Parse(c.Element("Mileage").Value, CultureInfo.InvariantCulture),
                int.Parse(c.Element("YearOfManufacture").Value),
                c.Element("RegistrationNumber").Value,
                decimal.Parse(c.Element("InsuranceAmount").Value, CultureInfo.InvariantCulture),
                decimal.Parse(c.Element("CostOfRent").Value, CultureInfo.InvariantCulture))).ToList();

            return cars;
        }
        public Car GetAviableCar(Filter<Car> filter)
        {
            if (filter == null)
                throw new ArgumentNullException("Filter can't be null");

            return filter.Apply(GetAllAviableCars()).FirstOrDefault();
        }

        public bool GiveCarToClient(Client client, Car car)
        {
            if (client == null || car == null)
                throw new ArgumentNullException("Arguments can't be null");

            XDocument xdoc = XDocument.Load(path);
            var availableCar = xdoc.Root.Elements("Car").FirstOrDefault(c => c.Element("RegistrationNumber").Value == car.RegistrationNumber);
            if (availableCar == null)
                return false;

            availableCar.Remove();
            xdoc.Save(path);

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
            XDocument xdoc = XDocument.Load(path);
            var root = xdoc.Root;
            root.Add(new XElement("Car",
                new XElement(nameof(car.NumberOfPassengers), car.NumberOfPassengers),
                new XElement(nameof(Car.EngineCapacity), car.EngineCapacity),
                new XElement(nameof(Car.Mileage), car.Mileage),
                new XElement(nameof(Car.YearOfManufacture), car.YearOfManufacture),
                new XElement(nameof(Car.InsuranceAmount), car.InsuranceAmount),
                new XElement(nameof(Car.RegistrationNumber), car.RegistrationNumber),
                new XElement(nameof(Car.CostOfRent), car.CostOfRent)));
            xdoc.Save(path);

            occupiedCars.Remove(car);
            clientManager.AddCarRentRecord(client, new RentRecord(car, false, DateTime.Now));
            return true;
        }
    }
}
