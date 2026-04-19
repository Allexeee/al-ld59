using UnityEngine;

public struct GameTimestamp
{
   public float timestamp;
   public float now  => G.timeline.time;
   public float prev => G.timeline.timePrev;

   // Иногда работает некорректно
   public bool TimePassed(float time)
   {
      if (timestamp == default)
         return false;

      return prev < timestamp + time && now >= timestamp + time;
   }   
   
   public bool WithSince(float time)
   {
      if (timestamp == default)
         return false;

      return now >= timestamp + time;
   }
}

public static class Extentions
{
   public static void Stamp(this ref GameTimestamp gameTimestamp)
   {
      gameTimestamp.timestamp = gameTimestamp.now;
   }
}