using System;
using AssetsPlugin;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyAScript : UnityScript
{
   [SerializeField] EnemyPresentation presentation;
   AssetContainer                     asset;

   public AssetId assetId;
   
   int        countDamage;
   EnemyState state;
   Vector3    target;

   protected override void OnAwake()
   {
      base.OnAwake();
      state    = EnemyState.MoveToRadar;
      usePause = true;
   }

   public void OnSpawn(AssetId assetId)
   {
      this.assetId = assetId;
      asset        = G.db.GetAsset(assetId);
      countDamage  = default;

      presentation.rb.position = transform.position;
      presentation.SetSize(asset.As<AssetSize>().size);
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

            if (target == default)
            {
               target   =  radar.transform.position;
               target.x += Random.Range(-1f, 1f);
            }
            
            var direction = (Vector2) (target - transform.position).normalized;
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
         G.game.RadarDamage(this);
      }
   }

   public void GetDamage(int count)
   {
      countDamage += count;

      if (countDamage >= asset.As<AssetHealth>().maxHealth)
      {
         // todo: эффекты, звуки

         G.game.KillEnemy(this);
         // Kill();
         // G.spawner.SpawnUniversal(transform.position, AssetId.EnemyVfxDestroy);
         // G.audio.Play(transform.position, AssetId.EnemySfxDestroy);
         // G.spawner.Despawn(gameObject);
      }
   }

   public void Kill()
   {
      G.spawner.SpawnUniversal(transform.position, AssetId.EnemyVfxDestroy);
      G.audio.Play(transform.position, AssetId.EnemySfxDestroy);
      G.spawner.Despawn(gameObject);
   }

   public void PushBack(float force)
   {
      var direction = -presentation.lastDirection;
      presentation.AddPush(direction * force);
   }
}

public enum EnemyState
{
   None,
   MoveToRadar,
}