using System;
using System.Collections.Generic;

namespace EventsPlugin
{
   public static class EventInterfaceCache
   {
      private static readonly Dictionary<Type, List<Type>> cache = new();

      public static List<Type> GetEventInterfaces(Type t)
      {
         if (cache.TryGetValue(t, out var interfaces))
            return interfaces;

         var result = new List<Type>();
         foreach (var i in t.GetInterfaces())
         {
            if (typeof(IEventAbstract).IsAssignableFrom(i) && i != typeof(IEventAbstract))
               result.Add(i);
         }

         cache[t] = result;
         return result;
      }
   }
}