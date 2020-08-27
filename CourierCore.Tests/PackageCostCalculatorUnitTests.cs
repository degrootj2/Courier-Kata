using System;
using Xunit;
using static CourierCore.PackageCostCalculator;

namespace CourierCore.Tests
{
    public class PackageCostCalculatorUnitTests
    {

        [Fact]
        public void CalculateCostUsingTypeCorrectly()
        {
            Assert.Equal(CalculateCost(PackageType.Small), 3f);
            Assert.Equal(CalculateCost(PackageType.Medium), 8f);
            Assert.Equal(CalculateCost(PackageType.Large), 15f);
            Assert.Equal(CalculateCost(PackageType.ExtraLarge), 26f);
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
            PackageCostInfo smallPackage = GeneratePackageInfo(new PackageSize { Length = 5, Width = 2, Thickness = 1 });
            PackageCostInfo mediumPackage = GeneratePackageInfo(new PackageSize { Length = 15, Width = 2, Thickness = 1 });
            PackageCostInfo largePackage = GeneratePackageInfo(new PackageSize { Length = 55, Width = 2, Thickness = 1 });
            PackageCostInfo extraLargePackage = GeneratePackageInfo(new PackageSize { Length = 105, Width = 2, Thickness = 1 });

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
