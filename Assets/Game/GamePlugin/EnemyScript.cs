using System;
using AssetsPlugin;
using UnityEngine;

public class EnemyScript : UnityScript
{
   [SerializeField] EnemyPresentation presentation;
   AssetContainer                     asset;

   public int countDamage;
   
   EnemyState state;

   protected override void OnAwake()
   {
      base.OnAwake();
      state = EnemyState.MoveToRadar;
      asset = G.db.GetAsset(AssetId.Enemy);
      // presentation = GetComponent<HeroPresentation>();
   }

   protected override void OnFixedUpdate()
   {
      base.OnFixedUpdate();

      switch (state)
      {
         case EnemyState.MoveToRadar:
            var radar = G.game.radar;
            if (radar == default)
               return;

            var direction = (Vector2) (radar.transform.position - transform.position).normalized;
            var speed     = asset.As<AssetMovement>().speed;
            presentation.AddPosition(direction * speed * Time.deltaTime);
            presentation.RotateByDirection(direction);
            break;
      }
   }

   void OnCollisionEnter2D(Collision2D col)
   {
      if (col.transform.TryGetComponent<RadarScript>(out var radar))
      {
         G.game.RadarDamage(gameObject);
      }
   }

   public void GetDamage(int count)
   {
      countDamage += count;

      if (countDamage >= asset.As<AssetHealth>().maxHealth)
      {
         // todo: эффекты, звуки
         
         G.spawner.Despawn(gameObject);
      }
   }
}

public enum EnemyState
{
   None,
   MoveToRadar,
}