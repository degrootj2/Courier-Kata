using System;
using Xunit;
using static CourierCore.PackageCostCalculator;

namespace CourierCore.Tests
{
    public class PackageCostCalculatorUnitTests
    {

        [Fact]
        public void CalculateCostUsingTypeCorrectly_WithinWeightLimit()
        {
            Assert.Equal(CalculateCost(PackageType.Small, 1f), 3f);
            Assert.Equal(CalculateCost(PackageType.Medium, 3f), 8f);
            Assert.Equal(CalculateCost(PackageType.Large, 6f), 15f);
            Assert.Equal(CalculateCost(PackageType.ExtraLarge, 10f), 26f);
        }

        [Fact]
        public void CalculateCostUsingTypeCorrectly_OverWeightLimit()
        {
            Assert.Equal(CalculateCost(PackageType.Small, 3f), 5f);
            Assert.Equal(CalculateCost(PackageType.Medium, 5f), 10f);
            Assert.Equal(CalculateCost(PackageType.Large, 8f), 17f);
            Assert.Equal(CalculateCost(PackageType.ExtraLarge, 12f), 28f);
            Assert.Equal(CalculateCost(PackageType.ExtraLarge, 20f), 46f);
        }

        [Fact]
        public void DetermineSizeCorrectly()
        {
            Assert.Equal(DetermineSize(0.2f, 5, 2), PackageType.Small);
            Assert.Equal(DetermineSize(2, 5, 2), PackageType.Small);
            Assert.Equal(DetermineSize(10, 5, 2), PackageType.Medium);
            Assert.Equal(DetermineSize(15, 55, 2), PackageType.Large);
            Assert.Equal(DetermineSize(105, 105, 105), PackageType.ExtraLarge);
            Assert.Equal(DetermineSize(5, 105, 105), PackageType.ExtraLarge);
        }

        [Fact]
        public void GeneratePackageInfoCorrectly()
        {
            PackageCostInfo smallPackage = GeneratePackageInfo(new PackageSizeWeight { Length = 5, Width = 2, Thickness = 1, Weight = 2f });
            PackageCostInfo mediumPackage = GeneratePackageInfo(new PackageSizeWeight { Length = 15, Width = 2, Thickness = 1, Weight = 2f });
            PackageCostInfo largePackage = GeneratePackageInfo(new PackageSizeWeight { Length = 55, Width = 2, Thickness = 1, Weight = 2f });
            PackageCostInfo extraLargePackage = GeneratePackageInfo(new PackageSizeWeight { Length = 105, Width = 2, Thickness = 1, Weight = 2f });

            Assert.Equal(smallPackage.Type, PackageType.Small);
            Assert.Equal(smallPackage.Cost, 3f);

            Assert.Equal(mediumPackage.Type, PackageType.Medium);
            Assert.Equal(mediumPackage.Cost, 8f);

            Assert.Equal(largePackage.Type, PackageType.Large);
            Assert.Equal(largePackage.Cost, 15f);

            Assert.Equal(extraLargePackage.Type, PackageType.ExtraLarge);
            Assert.Equal(extraLargePackage.Cost, 26f);
        }
    }
}
