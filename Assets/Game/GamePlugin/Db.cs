using System;
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