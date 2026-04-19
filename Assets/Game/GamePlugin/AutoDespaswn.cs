using System;
using AssetsPlugin;
using UnityEngine;

public class AutoDespaswn : MonoBehaviour
{
   [SerializeField] float time;

   GameTimestamp timestamp;
   
   void OnEnable()
   {
      timestamp.Stamp();
   }

   void Update()
   {
      if (timestamp.WithSince(time))
      {
         G.spawner.Despawn(gameObject);
      }
   }
}