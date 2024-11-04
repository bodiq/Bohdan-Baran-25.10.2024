using System;
using System.Collections.Generic;
using UnityEngine;

namespace Configs
{
    public static class ConfigHelper
    {
        public static List<T> GetEnumValues<T>() where T : Enum
        {
            return new List<T>((T[])Enum.GetValues(typeof(T)));
        }

        public static int RoundToNearestTen(int number)
        {
            return Mathf.RoundToInt(number / 10) * 10;
        }
    }
}