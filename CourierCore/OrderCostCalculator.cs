using System;
using System.Collections.Generic;
using System.Linq;
using static CourierCore.PackageCostCalculator;

namespace CourierCore
{
    public static class OrderCostCalculator
    {
        public static OrderCost CalculateOrderCost(List<PackageSizeWeight> packageSizes, bool speedyShippingChosen)
        {
            List<PackageCostInfo> packageCostsUnsortedNoDiscount = new List<PackageCostInfo>();

            foreach (PackageSizeWeight package in packageSizes)
            {
                packageCostsUnsortedNoDiscount.Add(GeneratePackageInfo(package));
            }

            List<PackageCostInfo> smallParcelsDiscounted = ApplySmallPackageDiscounts(packageCostsUnsortedNoDiscount);
            List<PackageCostInfo> mediumParcelsDiscounted = ApplyMediumPackageDiscounts(packageCostsUnsortedNoDiscount);

            List<PackageCostInfo> discountedPackageCosts = new List<PackageCostInfo>();
            discountedPackageCosts.AddRange(smallParcelsDiscounted);
            discountedPackageCosts.AddRange(mediumParcelsDiscounted);
            discountedPackageCosts.AddRange(packageCostsUnsortedNoDiscount.Where(p => p.Type != PackageType.Small && p.Type != PackageType.Medium).ToList());

            float totalPackagesCost = 0f;
            float totalDiscount = 0f;
            foreach (var package in discountedPackageCosts)
            {
                if (package.Discounted)
                {
                    totalDiscount += package.Cost;
                }
                else
                {
                    totalPackagesCost += package.Cost;
                }
            }

            return new OrderCost
            {
                PackagesInfo = discountedPackageCosts,
                TotalDiscounts = totalDiscount,
                TotalCost = totalPackagesCost * (speedyShippingChosen ? 2 : 1),
                SpeedyShippingCost = (speedyShippingChosen) ? totalPackagesCost : 0f
            };
        }

        public static List<PackageCostInfo> ApplySmallPackageDiscounts(List<PackageCostInfo> packageCostsUnsortedNoDiscount)
        {
            var smallParcelsDiscounted = packageCostsUnsortedNoDiscount.Where(p => p.Type == PackageType.Small).OrderByDescending(p => p.Cost).ToList();
            for (var i = 0; i + 4 <= smallParcelsDiscounted.Count; i += 4)
            {
                smallParcelsDiscounted[i + 3].Discounted = true;
            }

            return smallParcelsDiscounted;
        }

        public static List<PackageCostInfo> ApplyMediumPackageDiscounts(List<PackageCostInfo> packageCostsUnsortedNoDiscount)
        {
            var mediumParcelsDiscounted = packageCostsUnsortedNoDiscount.Where(p => p.Type == PackageType.Medium).OrderByDescending(p => p.Cost).ToList();
            for (var i = 0; i + 3 <= mediumParcelsDiscounted.Count; i += 3)
            {
                mediumParcelsDiscounted[i + 2].Discounted = true;
            }

            return mediumParcelsDiscounted;
        }
    }
}