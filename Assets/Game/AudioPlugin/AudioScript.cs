using System;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace AudioPlugin
{
   public class AudioScript : MonoBehaviour
   {
      [SerializeField] AudioSource audioSource;

      public AudioSource  AudioSource => audioSource;
      public IAudioPreset preset      { get; private set; }

      public void SetPosition(Vector2 position) => transform.position = position;

      public void Play(IAudioPreset audioPreset)
      {
         preset = audioPreset;
         audioPreset.WriteTo(audioSource);
         audioSource.Play();
      }

      public bool CheckFinished()
      {
         return !audioSource.isPlaying;
      }

      public void Stop()
      {
         audioSource.Stop();
      }
   }
}