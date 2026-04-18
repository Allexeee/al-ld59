using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AudioPlugin
{
   public class AudioManager : MonoBehaviour
   {
      Dictionary<AudioPointer, AudioScript> active = new();

      List<AudioPointer> toRemove = new();
      AudioFactory       audioFactory;

      public void Inject(AudioFactory factoryAudio)
      {
         audioFactory = factoryAudio;
      }

      void Update()
      {
         foreach (var (pointer, script) in active)
            if (script.CheckFinished())
            {
               script.Stop();
               audioFactory.Despawn(script);
               toRemove.Add(pointer);
            }

         foreach (var pointer in toRemove)
            active.Remove(pointer);

         toRemove.Clear();
      }

      public AudioPointer Play(Vector2 position, IAudioPreset preset)
      {
         var success = CheckChance(preset) && CheckRateLimit(preset);
         if (success)
         {
            var audioScript = audioFactory.Spawn(position, preset);
            audioScript.Play(preset);

            var pointer = AudioPointer.GetNext();

            active.Add(pointer, audioScript);
            return pointer;
         }

         return default;
      }


      bool CheckChance(IAudioPreset audioPreset)
      {
         if (audioPreset.SoundPlaybackChance == 0f)
            return true;

         return Random.value <= audioPreset.SoundPlaybackChance;
      }

      bool CheckRateLimit(IAudioPreset audioPreset)
      {
         if (audioPreset.GroupAntiSpam == default) return true;

         var counter = 0;
         foreach (var (pointer, script) in active)
         {
            if (script.preset.GroupAntiSpam == audioPreset.GroupAntiSpam)
            {
               counter++;
               if (counter >= audioPreset.SoundPlaybackRateLimit)
                  return false;
            }
         }

         return true;
      }
   }
}