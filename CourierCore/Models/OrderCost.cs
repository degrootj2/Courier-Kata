using System.Collections.Generic;

namespace CourierCore
{
    public class OrderPricing
    {
        public List<PackageCostInfo> PackagesInfo { get; set; }
        public float TotalCost { get; set; }
        public float SpeedyShippingCost { get; set; }
    }
}