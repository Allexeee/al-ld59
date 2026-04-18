using System;
using AssetsPlugin;

public enum AssetId
{
   None,
   Test,
   Main,
   AudioTest,
   Hero,
   UiBtnInteract,
   Radar,
   Enemy,
   Projectile
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

      content.AddAsset(AssetId.Hero)
             .Join(new AssetMovement() {speed = 6f});

      content.AddAsset(AssetId.UiBtnInteract)
             .Join(new AssetPrefab("[Obj] UI World Button Interact", true));

      content.AddAsset(AssetId.Radar)
             .Join(new AssetPrefab("[Obj] Radar", false))
             .Join(new AssetHealth() {maxHealth = 5});

      content.AddAsset(AssetId.Enemy)
             .Join(new AssetPrefab("[Obj] Enemy", true))
             .Join(new AssetMovement() {speed   = 4f})
             .Join(new AssetHealth() {maxHealth = 2});

      content.AddAsset(AssetId.Projectile)
             .Join(new AssetPrefab("[Obj] Projectile", true))
             .Join(new AssetMovement() {speed = 4f})
             .Join(new AssetDamage() {damage  = 1})
         ;
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