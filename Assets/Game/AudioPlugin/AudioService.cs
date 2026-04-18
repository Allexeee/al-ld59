using UnityEngine;
using UnityEngine.Audio;
using UntilWeDie.Game.Audio;

namespace AudioPlugin
{
   public class AudioService
   {
      AudioManager manager;
      AudioMixers  mixers;

      public AudioService(AudioManager manager, AudioMixers mixers)
      {
         this.manager = manager;
         this.mixers  = mixers;
      }

      public AudioPointer Play(Vector2 position, IAudioPreset preset)
      {
         return manager.Play(position, preset);
      }
      
      public void SetVolume(AudioMixerGroup mixerGroup, float volume)
      {
         var dB = Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * 20f;
         mixerGroup.audioMixer.SetFloat("Volume", dB);
      }

      public float GetVolume(AudioMixerGroup mixerGroup)
      {
         mixerGroup.audioMixer.GetFloat("Volume", out var dB);
         var volume = Mathf.Pow(10f, dB / 20f);
         return volume;
      }

      public AudioSO Get(string path)
      {
         return Resources.Load<AudioSO>("Audio/Data/" + path);
      }
   }
}