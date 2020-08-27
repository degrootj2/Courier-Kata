﻿using System;
using System.Collections.Generic;
using CourierCore;
using static CourierCore.OrderPriceCalculator;

namespace ConsoleTestingApp
{
    class Program
    {
        static void Main(string[] args)
        {
            List<PackageSize> inputPackages = new List<PackageSize>() {
                new PackageSize {Length = 5, Width = 2, Thickness = 1},
                new PackageSize {Length = 10, Width = 2, Thickness = 1},
                new PackageSize {Length = 10, Width = 5, Thickness = 12},
                new PackageSize {Length = 15, Width = 5, Thickness = 3},
                new PackageSize {Length = 55, Width = 5, Thickness = 3},
                new PackageSize {Length = 105, Width = 5, Thickness = 3}
            };

            OrderPricing orderPricing = CalculateOrderPrice(inputPackages);

            foreach (var package in orderPricing.PackagesInfo)
            {
                if (package.Type == PackageType.Undefined)
                {
                    Console.WriteLine("Unknown package type and price, an error occured");
                }
                else
                {
                    Console.WriteLine(package.Type.ToString() + " package cost: $" + package.Cost.ToString());
                }
            }
            Console.WriteLine("Total cost: $" + orderPricing.TotalCost);
        }
    }
}
