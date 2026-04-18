using UnityEngine;
using UnityEngine.Audio;

namespace UntilWeDie.Game.Audio
{
   public class AudioMixers
   {
      AudioMixerGroup[] mixers;

      public AudioMixerGroup Master      => mixers[0];
      public AudioMixerGroup Music       => mixers[1];
      public AudioMixerGroup SoundEffect => mixers[2];
      public AudioMixerGroup Ambient     => mixers[3];
      
      public AudioMixers(string pathToMixer)
      {
         mixers = Resources.Load<AudioMixer>($"{pathToMixer}/Master").FindMatchingGroups("Master");
      }

      public AudioMixerGroup Get(MixerGroup layer)
      {
         return mixers[(int) layer];
      }
   }

   public enum MixerGroup
   {
      Master,
      Music,
      SoundEffect,
      Ambient,
   }
}