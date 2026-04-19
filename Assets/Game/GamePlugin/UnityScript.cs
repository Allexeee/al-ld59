using System;
using UnityEngine;

public abstract class UnityScript : MonoBehaviour, IGamePaused
{
   protected bool usePause;

   bool paused;

   void Awake()
   {
      OnAwake();
   }

   void Update()
   {
      if (paused) return;
      OnUpdate();
   }

   void FixedUpdate()
   {
      if (paused) return;
      OnFixedUpdate();
   }

   protected virtual void OnEnable() => G.events.Register(this);

   protected virtual void OnDisable() => G.events.Unregister(this);

   protected virtual void OnAwake()
   {
   }

   protected virtual void OnUpdate()
   {
   }

   protected virtual void OnFixedUpdate()
   {
   }

   public void OnPaused(bool paused)
   {
      if (usePause)
         this.paused = paused;
   }
}