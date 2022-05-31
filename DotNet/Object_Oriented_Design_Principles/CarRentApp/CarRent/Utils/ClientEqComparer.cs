using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRent
{
    public class ClientEqComparer : EqualityComparer<Client>
    {
        public override bool Equals(Client x, Client y)
        {
            return x.LastName == y.LastName && x.Name == y.Name && x.DrivingLicense == y.DrivingLicense;
        }
        public override int GetHashCode(Client obj)
        {
            return HashCode.Combine(obj.Name, obj.LastName, obj.DrivingLicense);
        }
    }

}
