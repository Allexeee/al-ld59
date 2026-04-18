using System;
using System.Collections.Generic;


namespace EventsPlugin
{
   public static class InterfaceRegistry
   {
      // Один реестр на каждый тип интерфейса
      private static readonly Dictionary<Type, object> registries = new();

      public static InterfaceRegistry<T> Get<T>()
      {
         var type = typeof(T);
         if (!registries.TryGetValue(type, out var registry))
         {
            registry         = new InterfaceRegistry<T>();
            registries[type] = registry;
         }

         return (InterfaceRegistry<T>) registry;
      }

      // Универсальная регистрация: this, any object
      public static void RegisterAllInterfaces(object obj)
      {
         var interfaces = EventInterfaceCache.GetEventInterfaces(obj.GetType());

         foreach (var iface in interfaces)
         {
            var registryType = typeof(InterfaceRegistry<>).MakeGenericType(iface);
            var registry     = registries.GetValueOrDefault(iface);
            if (registry == null)
            {
               registry          = Activator.CreateInstance(registryType);
               registries[iface] = registry;
            }

            // Вызваем метод через reflection (можно ускорить кэшированием delegate)
            var addMethod = registryType.GetMethod("RegisterInstance");
            addMethod.Invoke(registry, new[] {obj});
         }
      }

      public static void UnregisterAllInterfaces(object obj)
      {
         var interfaces = EventInterfaceCache.GetEventInterfaces(obj.GetType());
         foreach (var iface in interfaces)
         {
            if (registries.TryGetValue(iface, out var registry))
            {
               var registryType = typeof(InterfaceRegistry<>).MakeGenericType(iface);
               var removeMethod = registryType.GetMethod("UnregisterInstance");
               removeMethod.Invoke(registry, new[] {obj});
            }
         }
      }
   }

   public class InterfaceRegistry<T>
   {
      private readonly List<T> listeners = new();

      public void RegisterInstance(object obj)
      {
         // Безопасный cast, иначе exception
         if (obj is T t && !listeners.Contains(t))
            listeners.Add(t);
      }

      public void UnregisterInstance(object obj)
      {
         if (obj is T t)
            listeners.Remove(t);
      }

      public IReadOnlyList<T> All => listeners;
   }
}