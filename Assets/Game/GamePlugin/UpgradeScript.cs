using System;
using AssetsPlugin;
using UnityEngine;
using Random = UnityEngine.Random;

public class UpgradeScript : UnityScript
{
   [SerializeField] UpgradePresentation presentation;

   protected override void OnAwake()
   {
      base.OnAwake();
      usePause = true;
      presentation.PlayIdle();
   }

   void OnTriggerEnter2D(Collider2D col)
   {
      if (col.transform.TryGetComponent<HeroScript>(out var hero))
      {
         G.game.WeaponUpgrade(this);
      }
   }
}