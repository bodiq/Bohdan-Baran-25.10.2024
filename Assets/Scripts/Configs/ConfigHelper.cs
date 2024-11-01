using System;
using System.Collections.Generic;

namespace Configs
{
    public static class ConfigHelper
    {
        public static List<T> GetEnumValues<T>() where T : Enum
        {
            return new List<T>((T[])Enum.GetValues(typeof(T)));
        }
    }
}