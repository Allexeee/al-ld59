using System.Collections.Generic;

namespace EventsPlugin
{
   public class EventsService
   {
      public void Register(object obj)
      {
         InterfaceRegistry.RegisterAllInterfaces(obj);
      }

      public void Unregister(object obj)
      {
         InterfaceRegistry.UnregisterAllInterfaces(obj);
      }

      public InterfaceRegistry<T> Get<T>()
      {
         return InterfaceRegistry.Get<T>();
      }
   }
}