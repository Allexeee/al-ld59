using System;
using AssetsPlugin;
using UnityEngine;

public enum AssetId
{
   None,
   Test,
   Main,
   AudioTest,
   Hero,
   HeroSfxShoot,
   UiBtnInteract,
   Radar,
   RadarSignal,
   EnemyLevel1,
   EnemyLevel2,
   EnemyLevel3,
   EnemyLevel4,
   EnemyBoss,
   EnemyB,
   EnemyVfxDestroy,
   EnemySfxDestroy,
   Projectile,
   ProjectileVfxHit,
   ProjectileAudioHit,
   AbilityA,
   AbilityB,
   ZoneAttackEnemy,
   UpgradeWeapon,
}

public partial class Db
{
   void MakeAssets(AssetsService assetsService)
   {
      var audio   = G.audio;
      var content = assetsService.GetBuilder();

      content.AddAsset(AssetId.None);
      //
      // content.AddAsset(AssetId.AudioTest, new AssetAudio().Set(audio.Get("[Audio] Test")));
      //
      // content.AddAsset(AssetId.Test)
      //        .Join(new AssetPrefabs()
      //                .Add(new AssetPrefab(String.Empty, default)))
      //        .BindAsChild(AssetId.None);
      
      content.AddAsset(AssetId.RadarSignal)
             .Join(new AssetRadarSignal() {max  = 165f});

      content.AddAsset(AssetId.Hero)
             .Join(new AssetMovement() {speed = 6f});

      content.AddAsset(AssetId.HeroSfxShoot)
             .Join(new AssetAudio().Set(audio.Get("[Audio] Hero Shoot")));

      content.AddAsset(AssetId.AbilityA)
             .Join(new AssetAbilityAttackA() {rateInMin = 999f})
             .Join(new AssetDamage() {damage            = 1})
         ;

      content.AddAsset(AssetId.UiBtnInteract)
             .Join(new AssetPrefab("[Obj] UI World Button Interact", true));

      content.AddAsset(AssetId.Radar)
             .Join(new AssetPrefab("[Obj] Radar", false))
             .Join(new AssetHealth() {maxHealth = 5});


      content.AddAsset(AssetId.EnemyLevel1)
             .Join(new AssetPrefab("[Obj] Enemy", true))
             .Join(new AssetMovement() {speed   = 2f})
             .Join(new AssetHealth() {maxHealth = 2})
             .Join(new AssetSize(){size         = 1f});

      content.AddAsset(AssetId.EnemyLevel2)
             .Join(new AssetPrefab("[Obj] Enemy", true))
             .Join(new AssetMovement() {speed   = 2f})
             .Join(new AssetHealth() {maxHealth = 10})
             .Join(new AssetSize(){size = 2f});

      content.AddAsset(AssetId.EnemyLevel3)
             .Join(new AssetPrefab("[Obj] Enemy", true))
             .Join(new AssetMovement() {speed   = 1.8f})
             .Join(new AssetHealth() {maxHealth = 30})
             .Join(new AssetSize(){size = 4f});

      content.AddAsset(AssetId.EnemyLevel4)
             .Join(new AssetPrefab("[Obj] Enemy", true))
             .Join(new AssetMovement() {speed   = 1.6f})
             .Join(new AssetHealth() {maxHealth = 50})
             .Join(new AssetSize(){size = 6f});

      content.AddAsset(AssetId.EnemyBoss)
             .Join(new AssetPrefab("[Obj] Enemy", false))
             .Join(new AssetMovement() {speed   = 1.2f})
             .Join(new AssetHealth() {maxHealth = 500})
             .Join(new AssetSize(){size = 14f});

      
      content.AddAsset(AssetId.EnemyB)
             .Join(new AssetPrefab("[Obj] Enemy B", true))
             .Join(new AssetMovement() {speed             = 2f})
             .Join(new AssetHealth() {maxHealth           = 15})
             .Join(new AssetAbilityThrowStun() {rateInMin = 2f})
         ;

      content.AddAsset(AssetId.EnemyVfxDestroy)
             .Join(new AssetPrefab("[Vfx] Enemy Destroy", true));

      content.AddAsset(AssetId.EnemySfxDestroy)
             .Join(new AssetAudio().Set(audio.Get("[Audio] Explosion Enemy")));

      content.AddAsset(AssetId.Projectile)
             .Join(new AssetPrefab("[Obj] Projectile", true))
             .Join(new AssetMovement() {speed = 8f})
         ;

      content.AddAsset(AssetId.ProjectileAudioHit)
             .Join(new AssetAudio().Set(audio.Get("[Audio] Hit Enemy")));

      content.AddAsset(AssetId.ProjectileVfxHit)
             .Join(new AssetPrefab("[Vfx] Projectile Hit", true));

      content.AddAsset(AssetId.UpgradeWeapon)
             .Join(new AssetPrefab("[Obj] Upgrade", false))
             .Join(new AssetAudio().Set(audio.Get("[Audio] Upgrade Pickup")));
   }
}

public class AssetMovement : AssetAbstract
{
   public float speed;
}

public class AssetHealth : AssetAbstract
{
   public int maxHealth;
}

public class AssetDamage : AssetAbstract
{
   public int damage;
}

public class AssetAbilityAttackA : AssetAbstract
{
   public float rateInMin;
}

public class AssetAbilityThrowStun : AssetAbstract
{
   public float rateInMin;

   public float rate => 60f / rateInMin;
}

public class AssetRadarSignal : AssetAbstract
{
   public float max;
}

public class AssetSize : AssetAbstract
{
   public float size;
}