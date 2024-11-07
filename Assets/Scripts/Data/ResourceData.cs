using Enums;

namespace Data
{
    public struct ResourceData
    {
        public readonly ResourceType ResourceType;
        public readonly int CountToEarn;

        public ResourceData(ResourceType resourceType, int countToEarn)
        {
            ResourceType = resourceType;
            CountToEarn = countToEarn;
        }
    }
}