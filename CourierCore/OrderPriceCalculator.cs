using System;
using System.Collections.Generic;
using System.Linq;
using static CourierCore.PackagePriceCalculator;

namespace CourierCore
{
    public static class OrderPriceCalculator
    {
        public static OrderPricing CalculateOrderPrice(List<PackageSize> packageSizes)
        {
            List<PackageCostInfo> packagePrices = new List<PackageCostInfo>();

            foreach (PackageSize package in packageSizes)
            {
                packagePrices.Add(GeneratePackageInfo(package));
            }

            return new OrderPricing
            {
                TotalCost = packagePrices.Sum(package => package.Cost),
                PackagesInfo = packagePrices
            };
        }
    }
}