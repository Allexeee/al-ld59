using System;
using AssetsPlugin;
using UnityEngine;

public class ProjectileScript : UnityScript
{
   [SerializeField] ProjectilePresentation presentation;
   AssetContainer                          asset;
   Vector2                                 direction;

   public int damage;

   protected override void OnAwake()
   {
      base.OnAwake();
      asset    = G.db.GetAsset(AssetId.Projectile);
      usePause = true;
   }

   public void OnSpawn(Vector2 direction, int damage)
   {
      this.direction = direction;
      this.damage    = damage;
      presentation.SetRotateByDirection(direction);
   }

   protected override void OnFixedUpdate()
   {
      base.OnFixedUpdate();
      var speed = asset.As<AssetMovement>().speed;
      presentation.AddPosition(direction * speed * Time.deltaTime);

      if (!G.camera.rect.Contains(transform.position))
      {
         G.spawner.Despawn(gameObject);  
      }
   }

   void OnTriggerEnter2D(Collider2D col)
   {
      if (col.gameObject.TryGetComponent<EnemyAScript>(out var enemyScript))
      {
         G.game.OnProjectileCollisionWithEnemy(this, enemyScript);
      }
   }
}