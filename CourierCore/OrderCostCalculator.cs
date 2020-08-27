using System;
using System.Collections.Generic;
using System.Linq;
using static CourierCore.PackageCostCalculator;

namespace CourierCore
{
    public static class OrderCostCalculator
    {
        public static OrderPricing CalculateOrderCost(List<PackageSize> packageSizes, bool speedyShippingChosen)
        {
            List<PackageCostInfo> packageCosts = new List<PackageCostInfo>();

            foreach (PackageSize package in packageSizes)
            {
                packageCosts.Add(GeneratePackageInfo(package));
            }

            float totalPackagedCost = packageCosts.Sum(package => package.Cost);
            return new OrderPricing
            {
                TotalCost = totalPackagedCost * (speedyShippingChosen ? 2 : 1),
                PackagesInfo = packageCosts,
                SpeedyShippingCost = (speedyShippingChosen) ? totalPackagedCost : 0f
            };
        }
    }
}