using UnityEngine;

namespace AudioPlugin
{
   public interface IAudioGameObjectFactory
   {
      public GameObject SpawnInactive(GameObject prefab);
      public void       Despawn(GameObject       gameObject);
   }
}