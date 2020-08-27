using System.Collections.Generic;
using Xunit;
using static CourierCore.OrderPriceCalculator;

namespace CourierCore.Tests
{
    public class OrderPriceCalculatorUnitTests
    {

        [Fact]
        public void CalculateOrderPriceCorrectly()
        {
            List<PackageSize> orderPackages1 = new List<PackageSize>() {
                new PackageSize {Length = 5, Width = 2, Thickness = 1},
                new PackageSize {Length = 10, Width = 2, Thickness = 1},
                new PackageSize {Length = 10, Width = 5, Thickness = 12},
                new PackageSize {Length = 15, Width = 5, Thickness = 3},
                new PackageSize {Length = 55, Width = 5, Thickness = 3},
                new PackageSize {Length = 105, Width = 5, Thickness = 3}
            };
            List<PackageSize> orderPackages2 = new List<PackageSize>() {
                new PackageSize {Length = 5, Width = 2, Thickness = 1},
            };
            List<PackageSize> orderPackages3 = new List<PackageSize>() {
                new PackageSize {Length = 10, Width = 2, Thickness = 1},
                new PackageSize {Length = 15, Width = 5, Thickness = 3},
                new PackageSize {Length = 55, Width = 5, Thickness = 3},
                new PackageSize {Length = 105, Width = 5, Thickness = 3}
            };
            List<PackageSize> orderPackages4 = new List<PackageSize>();

            OrderPricing orderPricing1 = CalculateOrderPrice(orderPackages1);
            OrderPricing orderPricing2 = CalculateOrderPrice(orderPackages2);
            OrderPricing orderPricing3 = CalculateOrderPrice(orderPackages3);
            OrderPricing orderPricing4 = CalculateOrderPrice(orderPackages4);

            Assert.True(orderPricing1.TotalCost == 68);
            Assert.True(orderPricing2.TotalCost == 3);
            Assert.True(orderPricing3.TotalCost == 57);
            Assert.True(orderPricing4.TotalCost == 0);
        }

    }
}
