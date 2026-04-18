using System;
using System.Collections.Generic;
using UnityEngine;

namespace AssetsPlugin
{
   public class AssetBuilder
   {
      List<Builder> builders = new();

      public Builder AddAsset(AssetId assetId)
      {
         var builder = new Builder();
         builder.assetId = (int) assetId;

         builders.Add(builder);

         return builder;
      }

      public Builder AddAsset<T>(AssetId assetId, T asset) where T : AssetAbstract
      {
         var builder = new Builder();
         builder.assetId = (int) assetId;
         builder.Join(asset);

         builders.Add(builder);

         return builder;
      }

      public IReadOnlyList<Builder> Builders => builders;

      public void Clear() => builders.Clear();
   }

   public class Builder
   {
      AssetContainer       container = new();
      public int           assetId;
      public AssetAbstract asset => ReturnContainer();

      List<int> children;
      List<int> bindings;

      public Builder Join<T>(T assetUwd) where T : AssetAbstract
      {
         container.Add(assetUwd);
         return this;
      }

      public Builder Bind(AssetId keyAsset)
      {
         bindings ??= new List<int>();
         bindings.Add((int) keyAsset);
         return this;
      }

      public Builder BindAsChild(AssetId keyAsset)
      {
         children ??= new List<int>();
         children.Add((int) keyAsset);
         return this;
      }

      public AssetContainer ReturnContainer()
      {
         return container;
      }

      public AssetAbstract[] ReturnChildren(AssetsService service)
      {
         if (children == default) return default;
         return service.GetCollection(children);
      }

      public AssetAbstract[] ReturnBindings(AssetsService service)
      {
         if (bindings == default) return default;
         return service.GetCollection(bindings);
      }
   }
}