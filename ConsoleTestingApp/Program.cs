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
                new PackageSizeWeight {Length = 5, Width = 2, Thickness = 1, Weight = 3},
                new PackageSizeWeight {Length = 5, Width = 2, Thickness = 1, Weight = 1},
                new PackageSizeWeight {Length = 5, Width = 2, Thickness = 1, Weight = 1},
                new PackageSizeWeight {Length = 10, Width = 2, Thickness = 1, Weight = 2},
                new PackageSizeWeight {Length = 10, Width = 5, Thickness = 12, Weight = 6},
                new PackageSizeWeight {Length = 15, Width = 5, Thickness = 3, Weight = 2},
                new PackageSizeWeight {Length = 15, Width = 5, Thickness = 3, Weight = 8},
                new PackageSizeWeight {Length = 55, Width = 5, Thickness = 3, Weight = 5},
                new PackageSizeWeight {Length = 105, Width = 5, Thickness = 3, Weight = 7}
            };

            OrderCost orderCost = CalculateOrderCost(inputPackages, true);

            foreach (var package in orderCost.PackagesInfo)
            {
                if (package.Type == PackageType.Undefined)
                {
                    Console.WriteLine("Unknown package type and cost, an error occured");
                }
                else
                {
                    Console.WriteLine(package.Type.ToString() + " package cost: $" + package.Cost.ToString() + (package.Discounted ? " (discounted)" : ""));
                }
            }
            Console.WriteLine("Total discounts: $" + orderCost.TotalDiscounts);
            if (orderCost.SpeedyShippingCost != 0)
            {
                Console.WriteLine("Speedy shipping cost: $" + orderCost.SpeedyShippingCost);
            }
            Console.WriteLine("Total cost (minus any discounts): $" + orderCost.TotalCost);
        }
    }
}
