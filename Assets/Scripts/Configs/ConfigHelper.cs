using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using UI;
using UnityEngine;
using Random = UnityEngine.Random;

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
        
        public static TUIScreen GetUIScreen<TUIScreen>(List<UIScreen> screens) where TUIScreen : UIScreen
        {
            return screens.Find(screen => screen is TUIScreen) as TUIScreen;
        }

        public static Vector3 GenerateRandomVector(Vector3 range)
        {
            return new Vector3(
                Random.Range(-range.x, range.x),
                Random.Range(-range.y, range.y),
                Random.Range(-range.z, range.z));
        }
    }
}