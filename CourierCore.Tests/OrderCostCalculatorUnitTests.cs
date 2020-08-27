using System.Collections.Generic;
using Xunit;
using static CourierCore.OrderCostCalculator;

namespace CourierCore.Tests
{
    public class OrderCostCalculatorUnitTests
    {

        [Fact]
        public void CalculateOrderCost_Correctly()
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

            OrderPricing orderPricing1 = CalculateOrderCost(orderPackages1, false);
            OrderPricing orderPricing2 = CalculateOrderCost(orderPackages2, false);
            OrderPricing orderPricing3 = CalculateOrderCost(orderPackages3, false);
            OrderPricing orderPricing4 = CalculateOrderCost(orderPackages4, false);

            Assert.True(orderPricing1.TotalCost == 68);
            Assert.True(orderPricing2.TotalCost == 3);
            Assert.True(orderPricing3.TotalCost == 57);
            Assert.True(orderPricing4.TotalCost == 0);
        }

        [Fact]
        public void CalculateOrderCost_SpeedyShippingCorrectly()
        {
            List<PackageSize> orderPackages1 = new List<PackageSize>() {
                new PackageSize {Length = 5, Width = 2, Thickness = 1},
                new PackageSize {Length = 10, Width = 2, Thickness = 1},
                new PackageSize {Length = 10, Width = 5, Thickness = 12},
                new PackageSize {Length = 15, Width = 5, Thickness = 3},
                new PackageSize {Length = 55, Width = 5, Thickness = 3},
                new PackageSize {Length = 105, Width = 5, Thickness = 3}
            };

            OrderPricing orderPricingNoSpeedy = CalculateOrderCost(orderPackages1, false);
            OrderPricing orderPricingWithSpeedy = CalculateOrderCost(orderPackages1, true);


            Assert.True(orderPricingNoSpeedy.TotalCost == 68);
            Assert.True(orderPricingWithSpeedy.TotalCost == 136);
            Assert.True(orderPricingWithSpeedy.SpeedyShippingCost == 68);
        }

    }
}
