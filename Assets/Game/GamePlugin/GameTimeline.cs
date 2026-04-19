// Отвечает за спаун врагов, событий, выдачу улучшений...
// Всё, что привязано к игровому времени.

using System;
using System.Collections.Generic;

public class GameTimeline
{
   float gameTime;
   float gameTimePrev;

   List<TimelineTrigger> events = new();
   public float          timePrev => gameTimePrev;
   public float          time     => gameTime;

   public void RegisterTriggers(List<(Func<bool> condition, Action action)> triggers)
   {
      foreach (var (condition, action) in triggers)
      {
         events.Add(new TimelineTrigger() {condition = condition, action = action});
      }
   }

   public void Update(float dt)
   {
      gameTimePrev =  gameTime;
      gameTime     += dt;

      foreach (var evt in events)
      {
         if (!evt.triggered && evt.condition()) // объект evt условие получил true
         {
            evt.action?.Invoke();
            evt.triggered = true;
         }
      }
   }
}

public class TimelineTrigger
{
   public Func<bool> condition;
   public Action     action;
   public bool       triggered = false;
}