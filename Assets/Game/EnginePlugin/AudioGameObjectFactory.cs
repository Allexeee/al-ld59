using AudioPlugin;
using PoolPlugin;
using UnityEngine;

public class AudioGameObjectFactory : IAudioGameObjectFactory
{
   PoolGameObjectService pool;

   public AudioGameObjectFactory(PoolGameObjectService pool)
   {
      this.pool = pool;
   }

   public GameObject SpawnInactive(GameObject prefab)
   {
      var go = pool.GetObjectInactive(prefab);
      prefab.SetActive(true);
      return go;
   }

   public void Despawn(GameObject gameObject)
   {
      var comp = gameObject.GetComponent<ComponentPooled>();
      pool.ReturnObject(gameObject, comp.pool.prefab);
   }
}