using System.Collections.Generic;
using UnityEngine;

public class HandlerPauseGame : IGamePaused
{
   List<Rigidbody2D>                       pausedRigidbodies = new();
   List<(Vector2 velocity, float angular)> prevVelocities    = new();

   public HandlerPauseGame()
   {
      G.events.Register(this);
   }
   
   public void PauseAllRigidbodies()
   {
      pausedRigidbodies.Clear();
      prevVelocities.Clear();
      
      foreach (var rb in GameObject.FindObjectsOfType<Rigidbody2D>())
      {
         pausedRigidbodies.Add(rb);
         prevVelocities.Add((rb.velocity, rb.angularVelocity));
         rb.velocity        = Vector2.zero;
         rb.angularVelocity = 0;
         rb.simulated       = false; // Это прям полностью "замораживает" Rigidbody2D
      }
   }
   public void ResumeAllRigidbodies()
   {
      for (int i = 0; i < pausedRigidbodies.Count; i++)
      {
         var rb   = pausedRigidbodies[i];
         var prev = prevVelocities[i];
         rb.simulated       = true;
         rb.velocity        = prev.velocity;
         rb.angularVelocity = prev.angular;
      }
      pausedRigidbodies.Clear();
      prevVelocities.Clear();
   }

   public void OnPaused(bool paused)
   {
      if(paused)
         PauseAllRigidbodies();
      else
         ResumeAllRigidbodies();
   }
}