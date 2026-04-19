using System;
using System.Collections.Generic;
using AssetsPlugin;
using AudioPlugin;

public partial class Db
{
   AssetsService assets;

   public Db(AssetsService assets)
   {
      this.assets = assets;
   }

   public void Init()
   {
      MakeAssets(assets);
      assets.RegisterAssets();

      var triggers = new List<(Func<bool> condition, Action action)>();
      MakeTimeline(triggers);
      G.timeline.RegisterTriggers(triggers);
   }

   public IAudioPreset GetAudio(AssetId id)
   {
      return assets.Get<AssetAudio>(id).GetMain();
   }

   public AssetContainer GetAsset(AssetId assetId)
   {
      return assets.Get(assetId) as AssetContainer;
   }
   
   public T GetAsset<T>(AssetId assetId) where T : AssetAbstract => assets.Get<T>(assetId);
}