using System;

namespace CourierCore
{
    public class PackageCostInfo
    {
        public PackageType Type;
        public float Cost;
        public bool Discounted = false;

        public PackageCostInfo(PackageType type, float cost)
        {
            this.Type = type;
            this.Cost = cost;
        }
    }
}
