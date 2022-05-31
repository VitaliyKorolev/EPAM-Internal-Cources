using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRent
{
    public class Car
    {
        public int NumberOfPassengers { get; init; }
        public double EngineCapacity { get; init; }
        public double Mileage { get; init; }
        public int YearOfManufacture { get; init; }
        public string RegistrationNumber { get; init; }
        public decimal InsuranceAmount { get; init; }
        public decimal CostOfRent { get; init; }

        public Car(int numberOfPassengers, double engineCapacity, double mileage, int yearOfManufacture, string registrationNumber, decimal insuranceAmount, decimal costOfRent)
        {
            if (string.IsNullOrEmpty(registrationNumber))
                throw new ArgumentException("Invalid registration number");

            if (yearOfManufacture > DateTime.Today.Year || yearOfManufacture <= 1900 )
                throw new ArgumentException("Invalid year of manufacture");

            NumberOfPassengers = numberOfPassengers;
            EngineCapacity = engineCapacity;
            Mileage = mileage;
            YearOfManufacture = yearOfManufacture;
            RegistrationNumber = registrationNumber;
            InsuranceAmount = insuranceAmount;
            CostOfRent = costOfRent;
        }
    }
}
