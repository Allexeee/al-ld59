using System.Collections.Generic;
using UnityEngine;

namespace PoolPlugin
{
   public interface IPoolable
   {
      PoolGameObjectService.Pool pool { get; set; }
   }

   public class PoolGameObjectService
   {
      readonly Dictionary<GameObject, Pool> pools = new Dictionary<GameObject, Pool>();

      public void NewPool(GameObject prefab)
      {
         var pool = new Pool {
            prefab = prefab,
         };

         pools[prefab] = pool;
      }

      public void TryNewPool(GameObject prefab)
      {
         if (pools.ContainsKey(prefab)) return;
         NewPool(prefab);
      }

      public Pool GetPool(GameObject prefab)
      {
         TryNewPool(prefab);
         return pools[prefab];
      }

      public void ClearPools()
      {
         foreach (var next in pools)
         {
            next.Value.objects.Clear();
         }
      }

      public GameObject GetObjectInactiveSilently(GameObject prefab)
      {
         return pools[prefab].GetObjectInactiveSilently();
      }

      public GameObject GetObjectInactive(GameObject prefab)
      {
         if (!pools.ContainsKey(prefab)) NewPool(prefab);
         return pools[prefab].GetObjectInactive();
      }

      public T GetObjectInactive<T>(GameObject prefab)
      {
         if (!pools.ContainsKey(prefab)) NewPool(prefab);
         return pools[prefab].GetObjectInactive().GetComponent<T>();
      }

      public T GetObject<T>(GameObject prefab)
      {
         if (!pools.ContainsKey(prefab)) NewPool(prefab);
         return pools[prefab].GetObject().GetComponent<T>();
      }

      public GameObject GetObject(GameObject prefab)
      {
         if (!pools.ContainsKey(prefab)) NewPool(prefab);
         return pools[prefab].GetObject();
      }

      public void ReturnObject(GameObject instance, GameObject prefab)
      {
         if (!pools.ContainsKey(prefab)) NewPool(prefab);
         pools[prefab].ReturnObject(instance);
      }

      public class Pool
      {
         public          GameObject        prefab;
         public readonly Queue<GameObject> objects = new Queue<GameObject>();
         
         public GameObject Create()
         {
            prefab.SetActive(false);
            var result = UnityEngine.Object.Instantiate(prefab);
            prefab.SetActive(true);
            var s = result.GetComponent<IPoolable>();
            s.pool = this;
            result.SetActive(true);
            return result;
         }

         public GameObject CreateInactive()
         {
            prefab.SetActive(false);
            var result = UnityEngine.Object.Instantiate(prefab);
            prefab.SetActive(true);
            var s = result.GetComponent<IPoolable>();
            s.pool = this;
            return result;
         }

         public GameObject CreateInactiveSilently()
         {
            var result = UnityEngine.Object.Instantiate(prefab);
            var s      = result.GetComponent<IPoolable>();
            s.pool = this;
            return result;
         }

         public GameObject GetObjectInactive()
         {
            if (objects.Count == 0)
            {
               return CreateInactive();
            }

            var result = objects.Dequeue();
            return result;
         }

         public GameObject GetObjectInactiveSilently()
         {
            if (objects.Count == 0)
            {
               return CreateInactiveSilently();
            }

            var result = objects.Dequeue();
            return result;
         }

         public GameObject GetObject()
         {
            if (objects.Count == 0)
            {
               return Create();
            }

            var result = objects.Dequeue();
            result.SetActive(true);
            return result;
         }

         public void ReturnObject(GameObject instance)
         {
            instance.SetActive(false);
            objects.Enqueue(instance);
         }
      }
   }
}