using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace CourierCore
{
    public static class PackageCostCalculator
    {
        public static PackageCostInfo GeneratePackageInfo(PackageSizeWeight sizeWeight)
        {
            try
            {
                PackageType packageType = DetermineSize(sizeWeight.Length, sizeWeight.Width, sizeWeight.Thickness);
                float packageCost = CalculateCost(packageType, sizeWeight.Weight);

                return new PackageCostInfo(packageType, packageCost);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error creating Package info object: " + ex.Message);
                return new PackageCostInfo(PackageType.Undefined, 0);
            }
        }

        public static PackageType DetermineSize(float length, float width, float height)
        {
            float largestDimension = (new List<float> { length, width, height }).Max();
            if (largestDimension < 10f)
            {
                return PackageType.Small;
            }
            else if (largestDimension >= 10f && largestDimension < 50f)
            {
                return PackageType.Medium;
            }
            else if (largestDimension >= 50f && largestDimension < 100f)
            {
                return PackageType.Large;
            }
            else
            {
                return PackageType.ExtraLarge;
            }
        }

        public static float CalculateCost(PackageType packageType, float weight)
        {
            float cost = 0f;
            float amountOverWeight = 0f;

            const float overWeightCostPerKg = 2f;

            if (packageType == PackageType.Small)
            {
                cost = 3f;
                amountOverWeight = (weight > 1f) ? weight - 1f : 0f;
            }
            else if (packageType == PackageType.Medium)
            {
                cost = 8f;
                amountOverWeight = (weight > 3f) ? weight - 3f : 0f;
            }
            else if (packageType == PackageType.Large)
            {
                cost = 15f;
                amountOverWeight = (weight > 6f) ? weight - 6f : 0f;
            }
            else if (packageType == PackageType.ExtraLarge)
            {
                cost = 26f;
                amountOverWeight = (weight > 10) ? weight - 10f : 0f;
            }

            if (amountOverWeight != 0)
            {
                cost += (float)(Math.Floor(amountOverWeight / 2f)) * overWeightCostPerKg;
            }

            return cost;
        }
    }
}