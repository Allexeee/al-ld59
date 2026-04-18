using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Audio;
using UntilWeDie.Game.Audio;
using Random = UnityEngine.Random;

namespace AudioPlugin
{
   [CreateAssetMenu(menuName = "SO/Audio", fileName = "[Audio] ")]
   public class AudioSO : ScriptableObject, IAudioPreset
   {
      public GameObject      prefab;
      public List<AudioClip> clips;
      public float           minVolume = 1f;
      public float           maxVolume = 1f;
      public float           minPitch  = 1f;
      public float           maxPitch  = 1f;

      [Header("Anti Spam")]
      public AudioAntiSpam groupAntiSpam; // Ключ/группа эффекта. Используется для фильтрации антиспама
      public int soundPlaybackRateLimit;  // Ограничение воспроизведения звука по частоте (антиспам)
      [Header("Chance")]
      public float soundPlaybackChance = 1f; // Шанс на воспроизведение (от 0 до 1)

      public string NameDebug => name; // Имя для отладки/описания пресета

      public float      SoundPlaybackChance    => soundPlaybackChance;
      public int        SoundPlaybackRateLimit => soundPlaybackRateLimit;
      public int        GroupAntiSpam          => (int) groupAntiSpam;
      public GameObject Prefab                 => prefab;

      public void WriteTo(AudioSource audioSource)
      {
         var baseSource = prefab.GetComponent<AudioSource>();
         var baseVolume = baseSource.volume;
         var basePitch  = baseSource.pitch;

         audioSource.clip   = clips.RandomItem();
         audioSource.volume = baseVolume * Random.Range(minVolume, maxVolume);
         audioSource.pitch  = basePitch  * Random.Range(minPitch,  maxPitch);
      }
   }

   public enum AudioAntiSpam
   {
      None,
   }
}