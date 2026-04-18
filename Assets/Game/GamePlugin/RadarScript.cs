using System;
using AssetsPlugin;

public class RadarScript : UnityScript
{
   public int countDamage;

   AssetContainer asset;

   protected override void OnAwake()
   {
      base.OnAwake();
      G.game.radar = this;
      asset        = G.db.GetAsset(AssetId.Radar);
   }

   public void GetDamage()
   {
      countDamage += 1;

      if (countDamage >= asset.As<AssetHealth>().maxHealth)
         G.game.RadarDestroy();
   }
}