using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRent
{
    public struct RentRecord
    {
        public Car Car;
        bool IsReception; //either receiving or returning
        DateTime Date;

        public RentRecord(Car car, bool isReception, DateTime date)
        {
            Car = car;
            IsReception = isReception;
            Date = date;
        }
    }
}
