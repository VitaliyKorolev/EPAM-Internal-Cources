using System;

namespace CarRent
{
    public class Client
    {
        public Client(string name, string lastName, string drivingLicense)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(drivingLicense))
                throw new ArgumentException("Arguments are null or empty");
            Name = name;
            LastName = lastName;
            DrivingLicense = drivingLicense;
        }

        public string Name { get; init; }
        public string LastName { get; init; }
        public string DrivingLicense { get; init; }
    }
}
