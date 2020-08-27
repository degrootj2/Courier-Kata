using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace CourierCore
{
    public static class PackageCostCalculator
    {
        public static PackageCostInfo GeneratePackageInfo(PackageSize size)
        {
            try
            {
                PackageType packageType = DetermineSize(size.Length, size.Width, size.Thickness);
                float packageCost = CalculateCost(packageType);

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

        public static float CalculateCost(PackageType packageType)
        {
            switch (packageType)
            {
                case PackageType.Small: return 3f;
                case PackageType.Medium: return 8f;
                case PackageType.Large: return 15f;
                case PackageType.ExtraLarge: return 26f;
                default: return 26f;
            }
        }
    }
}