using System;
using AssetsPlugin;
using UnityEngine;


public class HeroScript : UnityScript, IStunnable
{
   HeroPresentation presentation;
   AssetContainer   asset;

   public HeroState state { get; private set; }
   GameTimestamp    timestampStun;

   protected override void OnAwake()
   {
      base.OnAwake();
      // presentation = GetComponent<HeroPresentation>();
      // asset        = G.db.GetAsset(AssetId.Hero);
      // state        = HeroState.Default;
      G.game.hero = this;
      usePause    = true;
   }

   protected override void OnFixedUpdate()
   {
      base.OnFixedUpdate();

      switch (state)
      {
         case HeroState.Stunned:
            if (timestampStun.TimePassed(1f))
               state = HeroState.Default;
            break;
      }
   }
   //
   // void DoMove()
   // {
   //    var movement = Vector2.zero;
   //    if (Input.GetKey(KeyCode.W))
   //    {
   //       movement.y += 1;
   //    }
   //
   //    if (Input.GetKey(KeyCode.S))
   //    {
   //       movement.y -= 1;
   //    }
   //
   //    if (Input.GetKey(KeyCode.A))
   //    {
   //       movement.x -= 1;
   //    }
   //
   //    if (Input.GetKey(KeyCode.D))
   //    {
   //       movement.x += 1;
   //    }
   //
   //    // Нормализация для одинаковой скорости по диагонали
   //    movement = movement.normalized;
   //
   //    var speed = asset.As<AssetMovement>().speed;
   //    presentation.AddPosition(movement * speed * Time.deltaTime);
   // }
   public Vector2 GetStunCenterPosition()
   {
      return transform.position;
   }

   public void    Stun()
   {
      state = HeroState.Stunned;
      timestampStun.Stamp();
   }
}

public enum HeroState
{
   None,
   Default,
   Stunned
}