using System.Collections.Generic;

namespace CourierCore
{
    public class OrderCost
    {
        public List<PackageCostInfo> PackagesInfo { get; set; }
        public float TotalCost { get; set; }
        public float SpeedyShippingCost { get; set; }
        public float TotalDiscounts { get; set; }
    }
}