using System;
using System.Collections.Generic;
using CourierCore;
using static CourierCore.OrderCostCalculator;

namespace ConsoleTestingApp
{
    class Program
    {
        static void Main(string[] args)
        {
            List<PackageSizeWeight> inputPackages = new List<PackageSizeWeight>() {
                new PackageSizeWeight {Length = 5, Width = 2, Thickness = 1, Weight = 1},
                new PackageSizeWeight {Length = 10, Width = 2, Thickness = 1, Weight = 2},
                new PackageSizeWeight {Length = 10, Width = 5, Thickness = 12, Weight = 2},
                new PackageSizeWeight {Length = 15, Width = 5, Thickness = 3, Weight = 2},
                new PackageSizeWeight {Length = 55, Width = 5, Thickness = 3, Weight = 5},
                new PackageSizeWeight {Length = 105, Width = 5, Thickness = 3, Weight = 7}
            };

            OrderPricing orderPricing = CalculateOrderCost(inputPackages, true);

            foreach (var package in orderPricing.PackagesInfo)
            {
                if (package.Type == PackageType.Undefined)
                {
                    Console.WriteLine("Unknown package type and cost, an error occured");
                }
                else
                {
                    Console.WriteLine(package.Type.ToString() + " package cost: $" + package.Cost.ToString());
                }
            }
            if (orderPricing.SpeedyShippingCost != 0)
            {
                Console.WriteLine("Speedy shipping cost: $" + orderPricing.SpeedyShippingCost);
            }
            Console.WriteLine("Total cost: $" + orderPricing.TotalCost);
        }
    }
}
