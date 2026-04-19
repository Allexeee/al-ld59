using System;
using System.Collections.Generic;
using AssetsPlugin;
using PrimeTween;
using UnityEngine;
using Random = UnityEngine.Random;

public partial class Db
{
   void MakeTimeline(List<(Func<bool> condition, Action action)> triggers)
   {
      var vars = G.vars;

      triggers.Add((() => vars.timestampBuildRadar.WithSince(0f), () =>
      {
         var assetId = AssetId.EnemyA;
         G.scheduler.Schedule(CommandSpawn.Get(GetPositionOutCamera, assetId), 1f, 60, 0.5f);
      }));
      
      triggers.Add((() => vars.timestampBuildRadar.WithSince(15f), () =>
      {
         var assetId = AssetId.EnemyA;
         G.scheduler.Schedule(CommandSpawn.Get(GetPositionOutCamera, assetId), 1f, 100, 0.75f);
      }));
      
      triggers.Add((() => vars.timestampBuildRadar.WithSince(15f), () =>
      {
         G.scheduler.Schedule(CommandSpawn.Get(GetPositionOutCamera, AssetId.EnemyA), 0f, 10, 0f);
         Tween.Custom(G.camera, 1f, 1.5f, 0.5f, (service, f) => service.Zoom(f));
      }));
      
      triggers.Add((() => vars.timestampBuildRadar.WithSince(30f), () =>
      {
         G.scheduler.Schedule(CommandSpawn.Get(GetPositionOutCamera, AssetId.EnemyA), 0f, 10, 0f);
         Tween.Custom(G.camera, G.camera.zoom, 1.75f, 0.25f, (service, f) => service.Zoom(f));
      }));
      
      triggers.Add((() => vars.timestampBuildRadar.WithSince(30f), () =>
      {
         var assetId = AssetId.EnemyA;
         G.scheduler.Schedule(CommandSpawn.Get(GetPositionOutCamera, assetId), 1f, 20, 1f);
      }));

   }

   static Vector2 GetPositionOutCamera()
   {
      var rect = G.camera.rect;
      
      var pos = new Vector2(0f, 10f);
      pos.x += Random.Range(-15f, 15f);

      if (pos.x is < 10f or > 10f)
         pos.y -= Random.Range(1f, 5f);

      return pos;
   }

   static Vector2 GetPosition(Vector2 center, Vector2 range)
   {
      var pos = center;
      pos.x += Random.Range(-range.x, range.y);

      if (pos.x is < 10f or > 10f)
         pos.y -= Random.Range(1f, 5f);

      return pos;
   }
}