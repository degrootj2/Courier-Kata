using System.Collections.Generic;
using Xunit;
using static CourierCore.OrderCostCalculator;

namespace CourierCore.Tests
{
    public class OrderCostCalculatorUnitTests
    {

        private bool ContainsAnyDiscounts(List<PackageCostInfo> packagePrices)
        {
            foreach (var package in packagePrices)
            {
                if (package.Discounted) return true;
            }
            return false;
        }

        [Fact]
        public void CalculateOrderCost_Correctly()
        {
            List<PackageSizeWeight> orderPackages1 = new List<PackageSizeWeight>() {
                new PackageSizeWeight {Length = 5, Width = 2, Thickness = 1, Weight = 2f},
                new PackageSizeWeight {Length = 10, Width = 2, Thickness = 1, Weight = 2f},
                new PackageSizeWeight {Length = 15, Width = 5, Thickness = 3, Weight = 2f},
                new PackageSizeWeight {Length = 55, Width = 5, Thickness = 3, Weight = 2f},
                new PackageSizeWeight {Length = 105, Width = 5, Thickness = 3, Weight = 2f}
            };
            List<PackageSizeWeight> orderPackages2 = new List<PackageSizeWeight>() {
                new PackageSizeWeight {Length = 5, Width = 2, Thickness = 1, Weight = 2f},
            };
            List<PackageSizeWeight> orderPackages3 = new List<PackageSizeWeight>() {
                new PackageSizeWeight {Length = 10, Width = 2, Thickness = 1, Weight = 2f},
                new PackageSizeWeight {Length = 15, Width = 5, Thickness = 3, Weight = 2f},
                new PackageSizeWeight {Length = 55, Width = 5, Thickness = 3, Weight = 2f},
                new PackageSizeWeight {Length = 105, Width = 5, Thickness = 3, Weight = 2f}
            };
            List<PackageSizeWeight> orderPackages4 = new List<PackageSizeWeight>();

            OrderCost orderCost1 = CalculateOrderCost(orderPackages1, false);
            OrderCost orderCost2 = CalculateOrderCost(orderPackages2, false);
            OrderCost orderCost3 = CalculateOrderCost(orderPackages3, false);
            OrderCost orderCost4 = CalculateOrderCost(orderPackages4, false);

            Assert.True(orderCost1.TotalCost == 62);
            Assert.True(orderCost2.TotalCost == 5);
            Assert.True(orderCost3.TotalCost == 57);
            Assert.True(orderCost4.TotalCost == 0);
        }

        [Fact]
        public void CalculateOrderCost_SpeedyShippingCorrectly()
        {
            List<PackageSizeWeight> orderPackages1 = new List<PackageSizeWeight>() {
                new PackageSizeWeight {Length = 5, Width = 2, Thickness = 1, Weight = 2f},
                new PackageSizeWeight {Length = 10, Width = 2, Thickness = 1, Weight = 2f},
                new PackageSizeWeight {Length = 15, Width = 5, Thickness = 3, Weight = 2f},
                new PackageSizeWeight {Length = 55, Width = 5, Thickness = 3, Weight = 2f},
                new PackageSizeWeight {Length = 105, Width = 5, Thickness = 3, Weight = 2f}
            };

            OrderCost orderCostNoSpeedy = CalculateOrderCost(orderPackages1, false);
            OrderCost orderCostWithSpeedy = CalculateOrderCost(orderPackages1, true);


            Assert.Equal(orderCostNoSpeedy.TotalCost, 62);
            Assert.Equal(orderCostWithSpeedy.TotalCost, 124);
            Assert.Equal(orderCostWithSpeedy.SpeedyShippingCost, 62);
        }

        [Fact]
        public void CalculateOrderCost_NoValidDiscounts()
        {

            List<PackageSizeWeight> orderPackages1 = new List<PackageSizeWeight>() {
                new PackageSizeWeight {Length = 2, Width = 2, Thickness = 1, Weight = 1f},
                new PackageSizeWeight {Length = 50, Width = 2, Thickness = 1, Weight = 5f},
                new PackageSizeWeight {Length = 50, Width = 2, Thickness = 1, Weight = 5f},
                new PackageSizeWeight {Length = 100, Width = 2, Thickness = 1, Weight = 1f},
            };

            OrderCost orderCost1 = CalculateOrderCost(orderPackages1, false);

            Assert.Equal(ContainsAnyDiscounts(orderCost1.PackagesInfo), false);
        }

        [Fact]
        public void CalculateOrderCost_ApplySmallParcelDiscount()
        {

            List<PackageSizeWeight> orderPackages1 = new List<PackageSizeWeight>() {
                new PackageSizeWeight {Length = 4, Width = 2, Thickness = 1, Weight = 1f},
                new PackageSizeWeight {Length = 4, Width = 2, Thickness = 1, Weight = 5f},
                new PackageSizeWeight {Length = 4, Width = 2, Thickness = 1, Weight = 1f},
                new PackageSizeWeight {Length = 4, Width = 2, Thickness = 1, Weight = 3f}
            };

            OrderCost orderCost1 = CalculateOrderCost(orderPackages1, false);

            Assert.Equal(21, orderCost1.TotalCost);
            Assert.Equal(orderCost1.PackagesInfo[3].Discounted, true);
            Assert.Equal(orderCost1.PackagesInfo[3].Cost, 3);
        }

        [Fact]
        public void ApplySmallPackageDiscount_CalculatesCorrectly()
        {

            List<PackageCostInfo> smallParcels = new List<PackageCostInfo> {
                new PackageCostInfo (PackageType.Small, 10f),
                new PackageCostInfo (PackageType.Small, 10f),
                new PackageCostInfo (PackageType.Small, 8f),
                new PackageCostInfo (PackageType.Small, 10f)
            };

            List<PackageCostInfo> discountedPackaged = ApplySmallPackageDiscounts(smallParcels);

            Assert.Equal(discountedPackaged[3].Discounted, true);
            Assert.Equal(discountedPackaged[3].Cost, 8);
        }

        [Fact]
        public void CalculateOrderCost_ApplyMediumParcelDiscount()
        {

            List<PackageSizeWeight> orderPackages1 = new List<PackageSizeWeight>() {
                new PackageSizeWeight {Length = 10, Width = 2, Thickness = 1, Weight = 1f},
                new PackageSizeWeight {Length = 10, Width = 2, Thickness = 1, Weight = 5f},
                new PackageSizeWeight {Length = 10, Width = 2, Thickness = 1, Weight = 1f},
            };

            OrderCost orderCost1 = CalculateOrderCost(orderPackages1, false);

            Assert.Equal(20, orderCost1.TotalCost);
            Assert.Equal(orderCost1.PackagesInfo[2].Discounted, true);
            Assert.Equal(orderCost1.PackagesInfo[2].Cost, 8);
        }

        [Fact]
        public void ApplyMediumPackageDiscount_CalculatesCorrectly()
        {

            List<PackageCostInfo> smallParcels = new List<PackageCostInfo> {
                new PackageCostInfo (PackageType.Medium, 10f),
                new PackageCostInfo (PackageType.Medium, 10f),
                new PackageCostInfo (PackageType.Medium, 12f),
            };

            List<PackageCostInfo> discountedPackaged = ApplyMediumPackageDiscounts(smallParcels);

            Assert.Equal(discountedPackaged[2].Discounted, true);
            Assert.Equal(discountedPackaged[2].Cost, 10);
        }

    }
}
