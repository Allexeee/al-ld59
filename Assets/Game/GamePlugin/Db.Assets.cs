using System;
using AssetsPlugin;

public enum AssetId
{
   None,
   Test,
   Main,
   AudioTest
}

public partial class Db
{
   void MakeAssets(AssetsService assetsService)
   {
      var audio   = G.audio;
      var content = assetsService.GetBuilder();

      content.AddAsset(AssetId.None);

      content.AddAsset(AssetId.AudioTest, new AssetAudio().Set(audio.Get("[Audio] Test")));

      content.AddAsset(AssetId.Test)
             .Join(new AssetPrefabs()
                     .Add(new AssetPrefab(String.Empty, default)))
             .BindAsChild(AssetId.None);
   }
}