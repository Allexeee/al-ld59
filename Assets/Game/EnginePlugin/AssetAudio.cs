using System.Collections.Generic;
using AssetsPlugin;
using AudioPlugin;

public class AssetAudio : AssetAbstract
{
   public Dictionary<AssetId, IAudioPreset> presets = new();

   public AssetAudio Set(IAudioPreset preset)
   {
      presets.Add(AssetId.Main, preset);
      return this;
   }

   public AssetAudio Set(AssetId key, IAudioPreset preset)
   {
      presets.Add(key, preset);
      return this;
   }

   public IAudioPreset Get(AssetId key) => presets.GetValueOrDefault(key);

   public IAudioPreset GetMain() => presets.GetValueOrDefault(AssetId.Main);
}