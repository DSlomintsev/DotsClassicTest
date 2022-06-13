using UnityEngine;

namespace DotsClassicTest.Utils
{
    public static class ArrayExtensions
    {
        public static T GetRandom<T>(this T[] array)
        {
            return array[Random.Range(0, array.Length)];
        }
    }
}