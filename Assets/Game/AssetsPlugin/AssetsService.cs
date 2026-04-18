using System;
using System.Collections.Generic;
using UnityEngine;

namespace AssetsPlugin
{
   public partial class AssetsService
   {
      AssetBuilder                   builder;
      Dictionary<int, AssetAbstract> assets;

      public AssetBuilder GetBuilder() => builder ??= new AssetBuilder();

      public AssetsService(int startCapacity)
      {
         assets = new Dictionary<int, AssetAbstract>(startCapacity);
      }

      public void RegisterAssets()
      {
         foreach (var item in builder.Builders)
            try
            {
               var asset    = item.ReturnContainer();
               var child    = item.ReturnChildren(this);
               var bindings = item.ReturnBindings(this);

               if (child != default && child.Length > 0)
               {
                  if (asset.Is<AssetChildren>(out var assetChildren))
                     assetChildren.list.AddRange(child);
                  else
                     asset.Add(new AssetChildren() {list = new List<AssetAbstract>(child)});
               }

               if (bindings != default)
                  foreach (var binding in bindings)
                     asset.Add(binding);

               assets.Add(item.assetId, asset);
            }
            catch (Exception e)
            {
               Debug.LogError($"{item.assetId} : {item.asset}");
               Debug.LogException(e);
            }

         builder.Clear();
      }

      public AssetAbstract[] GetCollection(params int[] assetIds)
      {
         var result = new AssetAbstract[assetIds.Length];
         var i      = 0;
         foreach (var next in assetIds)
         {
            result[i++] = Get(next);
         }

         return result;
      }

      public AssetAbstract[] GetCollection(IList<int> assetIds)
      {
         var result = new AssetAbstract[assetIds.Count];
         var i      = 0;
         foreach (var next in assetIds)
         {
            result[i++] = Get(next);
         }

         return result;
      }

      public AssetAbstract Get(AssetId    assetId)                         => assets[(int) assetId];
      public AssetAbstract Get(int        assetId)                         => assets[assetId];
      public T             Get<T>(AssetId assetId) where T : AssetAbstract => assets[(int) assetId].As<T>();
   }
}