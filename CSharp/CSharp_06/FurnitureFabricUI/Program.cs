using System;
using AbstractFabricLib;

namespace AbstractFabricUI
{
    class Program
    {
        static void Main(string[] args)
        {
            FurnitureFactory furnitureFactory = new OfficeFurnitureFactory();
            Client client1 = new Client(furnitureFactory);
            Console.WriteLine();

            furnitureFactory = new KitchenFurnitureFactory();
            Client client2 = new Client(furnitureFactory);
        }
    }
}
