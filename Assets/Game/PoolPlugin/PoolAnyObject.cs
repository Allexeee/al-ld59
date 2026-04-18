using System;
using System.Collections.Generic;

namespace PoolPlugin
{
   public class PoolAnyObject
   {
      const int              initialBufferSize = 3;
      Dictionary<Type, Impl> pools             = new();

      Impl GetPool<T>() where T : new()
      {
         var key = typeof(T);
         if (!pools.TryGetValue(key, out var pool))
         {
            pool = new Impl(() => new T(), initialBufferSize);
            pools.Add(key, pool);
         }

         return pool;
      }

      public T Get<T>() where T : class, new()
      {
         var pool = GetPool<T>();
         return pool.Get() as T;
      }


      public bool Contains<T>(T obj) => pools.ContainsKey(obj.GetType());
      public bool Has<T>(T      obj) => Contains(obj) && pools[obj.GetType()].Contains(obj);

      public void Add<T>(T obj)
      {
         pools[obj.GetType()].Add(obj);
      }

      class Impl
      {
         private readonly Queue<object> pool = new();
         private readonly Func<object>  makeObject;

         public Impl(Func<object> makeObject, int initialBufferSize)
         {
            this.makeObject = makeObject;
            for (var i = 0; i < initialBufferSize; ++i)
            {
               var obj = makeObject();
               pool.Enqueue(obj);
            }
         }

         public bool Contains(object obj) => pool.Contains(obj);

         public object Get()
         {
            if (pool.Count > 0)
            {
               var obj = pool.Dequeue();
               return obj;
            }

            return makeObject();
         }

         public void Add(object obj)
         {
            // Это самая медленная часть пула в дебаг режиме
#if UNITY_EDITOR && DEVELOPMENT_BUILD
                if (pool.Contains(obj)) Debug.LogAssertion($"Pool уже содержит объект {obj}");
#endif
            pool.Enqueue(obj);
         }
      }
   }
}