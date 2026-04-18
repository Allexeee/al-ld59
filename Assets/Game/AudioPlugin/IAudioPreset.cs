using UnityEngine;

namespace AudioPlugin
{
   public interface IAudioPreset
   {
      float SoundPlaybackChance    { get; }
      int   SoundPlaybackRateLimit { get; }
      int   GroupAntiSpam          { get; }

      GameObject Prefab { get; }
      
      void WriteTo(AudioSource audioSource);
   }
}