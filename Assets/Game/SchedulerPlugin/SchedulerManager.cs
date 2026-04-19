using System;
using System.Collections.Generic;
using UnityEngine;

public class SchedulerManager : MonoBehaviour
{
   private class Entry
   {
      public float    delay;
      public int      repeats;
      public float    repeatDelay;
      public ICommand command;
   }

   private readonly List<Entry> jobs = new();

   Action<ICommand> onRelease;

   public void Inject(Action<ICommand> onRelease) => this.onRelease = onRelease;

   public void Schedule(ICommand cmd, float delay, int repeats = 0, float repeatDelay = 0)
   {
      jobs.Add(new Entry {
         delay       = delay,
         repeats     = repeats,
         repeatDelay = repeatDelay,
         command     = cmd
      });
   }

   public void OnUpdate(float dt)
   {
      for (var i = jobs.Count - 1; i >= 0; --i)
      {
         var entry = jobs[i];
         entry.delay -= dt;
         if (entry.delay <= 0)
         {
            entry.command.Execute();

            if (entry.repeats > 0)
            {
               entry.repeats--;
               entry.delay = entry.repeatDelay;
            }
            else
            {
               onRelease?.Invoke(entry.command); // Освобождаем команду в пул
               jobs.RemoveAt(i);
            }
         }
      }
   }
}