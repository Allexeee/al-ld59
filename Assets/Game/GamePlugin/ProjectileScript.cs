using System;
using AssetsPlugin;
using UnityEngine;

public class ProjectileScript : UnityScript
{
   [SerializeField] ProjectilePresentation presentation;
   AssetContainer                          asset;
   Vector2                                 direction;

   protected override void OnAwake()
   {
      base.OnAwake();
      asset = G.db.GetAsset(AssetId.Projectile);
   }

   public void OnSpawn(Vector2 direction)
   {
      this.direction = direction;
   }

   protected override void OnFixedUpdate()
   {
      base.OnFixedUpdate();
      var speed = asset.As<AssetMovement>().speed;
      presentation.AddPosition(direction * speed * Time.deltaTime);
      presentation.RotateByDirection(direction);
   }

   void OnCollisionEnter2D(Collision2D col)
   {
      if (col.gameObject.TryGetComponent<EnemyScript>(out var enemyScript))
         G.game.OnProjectileCollisionWithEnemy(gameObject, enemyScript);
   }
}