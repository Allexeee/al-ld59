using System;
using System.Collections.Generic;
using AudioPlugin;
using UnityEngine;
using Random = UnityEngine.Random;

public static class Utils
{
   public static AudioPointer Play(this AudioService service, Vector2 position, AssetId soundId)
   {
      var preset = G.db.GetAudio(soundId);

      return service.Play(position, preset);
   }

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

   public static int FindIndex<T>(this T[] array, Predicate<T> predicate)
   {
      return Array.FindIndex(array, predicate);
   }

   public static bool IsNullOrEmpty<T>(this IList<T> list) => list == null || list.Count == 0;

   public static T[] Slice<T>(this T[] source, Predicate<T> predicate, int end, int times)
   {
      var start = -1;

      for (var i = 0; i < source.Length; i++)
      {
         ref var val = ref source[i];
         if (predicate(val))
         {
            start = i;
            break;
         }
      }

#if UNITY_EDITOR
      if (start == -1)
         throw new Exception($"Couldn't find object typeof {typeof(T)}");
#endif

      var len = (start + end - start) * times;

      // Return new array.
      var res = new T[len];
      for (var i = 0; i < len; i++)
      {
         res[i] = source[i / times + start];
      }

      return res;
   }

   public static TY GetOrAdd<T, TY>(this Dictionary<T, TY> dictionary, T key)
   {
      if (dictionary.TryGetValue(key, out var result))
         return result;

      dictionary[key] = result = typeof(TY).IsClass ? Activator.CreateInstance<TY>() : default;
      return result;
   }

   public static TY Set<T, TY>(this Dictionary<T, TY> dictionary, T key, TY value)
   {
      dictionary[key] = value;
      return value;
   }

   /// <summary>
   /// Перемешивает элементы списка случайным образом (in-place).
   /// </summary>
   public static void Shuffle<T>(this IList<T> list)
   {
      var n = list.Count;
      while (n > 1)
      {
         n--;
         var k = Random.Range(0, n + 1);
         // Обмен значениями list[k] и list[n]
         (list[k], list[n]) = (list[n], list[k]);
      }
   }
}