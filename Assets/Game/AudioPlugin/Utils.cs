using System.Collections.Generic;
using UnityEngine;

namespace AudioPlugin
{
   static class Utils
   {
      public static T RandomItem<T>(this T[] array)
      {
         var index = Random.Range(0, array.Length);
         return array[index];
      }

      public static T RandomItem<T>(this List<T> array)
      {
         if (array.Count == 0) return default;
         var index = Random.Range(0, array.Count);
         return array[index];
      }

      public static int RandomIndex<T>(this List<T> array)
      {
         var index = Random.Range(0, array.Count);
         return index;
      }

      public static int RandomIndex<T>(this T[] array)
      {
         var index = Random.Range(0, array.Length);
         return index;
      }

      public static int FindIndex<T>(this T[] array, T element)
      {
         for (int i = 0; i < array.Length; i++)
         {
            if (Equals(array[i], element))
               return i;
         }

         return -1;
      }
   }
}