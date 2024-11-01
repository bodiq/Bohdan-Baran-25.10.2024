using System.Collections.Generic;
using Enums;

namespace Constants
{
    public static class ResourceConstants
    {
        public const int MinResourcesArrayLenght = 1;
        public const int MaxResourcesArrayLenght = 3;

        public const int MinResourcesCount = 10;
        public const int MaxResourcesCount = 400;

        public static readonly List<ResourceType> ListResourceTypes;

        static ResourceConstants()
        {
            ListResourceTypes = Configs.ConfigHelper.GetEnumValues<ResourceType>();
        }
    }
}