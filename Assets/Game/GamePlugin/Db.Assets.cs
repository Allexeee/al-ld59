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
   Radar
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
             .Join(new AssetMovement() {speed = 5f});

      content.AddAsset(AssetId.UiBtnInteract)
             .Join(new AssetPrefab("[Obj] UI World Button Interact", true));

      content.AddAsset(AssetId.Radar)
             .Join(new AssetPrefab("[Obj] Radar", true));
   }
}

public class AssetMovement : AssetAbstract
{
   public float speed;
}