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
            List<PackageSizeWeight> orderPackages1 = new List<PackageSizeWeight>() {
                new PackageSizeWeight {Length = 5, Width = 2, Thickness = 1, Weight = 2f},
                new PackageSizeWeight {Length = 10, Width = 2, Thickness = 1, Weight = 2f},
                new PackageSizeWeight {Length = 10, Width = 5, Thickness = 12, Weight = 2f},
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
            List<PackageSizeWeight> orderPackages1 = new List<PackageSizeWeight>() {
                new PackageSizeWeight {Length = 5, Width = 2, Thickness = 1, Weight = 2f},
                new PackageSizeWeight {Length = 10, Width = 2, Thickness = 1, Weight = 2f},
                new PackageSizeWeight {Length = 10, Width = 5, Thickness = 12, Weight = 2f},
                new PackageSizeWeight {Length = 15, Width = 5, Thickness = 3, Weight = 2f},
                new PackageSizeWeight {Length = 55, Width = 5, Thickness = 3, Weight = 2f},
                new PackageSizeWeight {Length = 105, Width = 5, Thickness = 3, Weight = 2f}
            };

            OrderPricing orderPricingNoSpeedy = CalculateOrderCost(orderPackages1, false);
            OrderPricing orderPricingWithSpeedy = CalculateOrderCost(orderPackages1, true);


            Assert.True(orderPricingNoSpeedy.TotalCost == 68);
            Assert.True(orderPricingWithSpeedy.TotalCost == 136);
            Assert.True(orderPricingWithSpeedy.SpeedyShippingCost == 68);
        }

    }
}
