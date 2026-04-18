using System;
using UnityEngine;

namespace AudioPlugin
{
   public class AudioFactory
   {
      GameObject              prefabDefault;
      Action<GameObject>      actionSpawn;
      IAudioGameObjectFactory factoryGameObject;

      public AudioFactory(IAudioGameObjectFactory factoryGameObject, string pathDefault)
      {
         this.factoryGameObject = factoryGameObject;
         prefabDefault          = Resources.Load<GameObject>(pathDefault);
      }

      public AudioScript Spawn(Vector2 position, IAudioPreset preset)
      {
         var prefab = preset.Prefab;

         if (prefab == default)
            prefab = prefabDefault;

         var obj = factoryGameObject.SpawnInactive(prefab).GetComponent<AudioScript>();

         obj.SetPosition(position);
         obj.gameObject.SetActive(true);
         return obj;
      }

      public void Despawn(AudioScript obj)
      {
         factoryGameObject.Despawn(obj.gameObject);
      }
   }
}