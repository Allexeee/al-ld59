using System;
using UnityEngine;

public class AL_Sandbox : MonoBehaviour, IEventSceneReady
{
   void OnEnable() => G.events.Register(this);

   void OnDisable() => G.events.Unregister(this);

   void Awake()
   {
      Debug.Log("Awake");
   }

   public void OnSceneReady()
   {
      Debug.Log("Scene Ready");
   }

   void Update()
   {
      if (Input.GetKeyDown(KeyCode.Keypad0))
      {
         // G.spawner.SpawnEnemy(new Vector2(7f, 0f));
         // G.audio.Play(Vector2.zero, AssetId.AudioTest);
      }     
      //
      // if (Input.GetKeyDown(KeyCode.Keypad1))
      // {
      //    Debug.Log("Shhh 1");
      //
      //    var cmd = PlayAudioCommand.Get(Vector2.zero, AssetId.AudioTest);
      //    G.scheduler.Schedule(cmd, 1f, 2, 0.5f);
      // }      
      //
      // if (Input.GetKeyDown(KeyCode.Keypad2))
      // {
      //    Debug.Log("Shhh 2");
      //
      //    var cmd = PlayAudioCommand.Get(Vector2.zero, AssetId.AudioTest);
      //    G.scheduler.Schedule(cmd, 1f, 2, 0.5f);
      //    G.scheduler.Schedule(cmd, 2f, 2, 0.1f);
      // }
   }
}