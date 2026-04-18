using System;
using UnityEngine;

public abstract class UnityScript : MonoBehaviour
{
   void Awake()
   {
      OnAwake();
   }

   void Update()
   {
      OnUpdate();
   }
   
   void FixedUpdate()
   {
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
}