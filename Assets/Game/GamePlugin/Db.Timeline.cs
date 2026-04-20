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

      // TestTimeline(triggers, vars);
      ReleaseTimeline(triggers, vars);
   }

   static void TestTimeline(List<(Func<bool> condition, Action action)> triggers, GameVars vars)
   {
      // G.db.GetAsset(AssetId.RadarSignal).As<AssetRadarSignal>().max = 20f;

      triggers.Add((() => vars.timestampBuildRadar.WithSince(0f), () =>
      {
         G.timeline.UpdateTime(110f);

         G.game.allowSpawnUpgrade++;
         G.game.allowSpawnUpgrade++;

         G.scheduler.Schedule(CommandSpawn.Get(GetPositionOutCamera, AssetId.EnemyLevel2), 0f, 5, 1f);
      }));

      triggers.Add((() => vars.timestampBuildRadar.WithSince(120f), () =>
      {
         G.scheduler.Schedule(CommandSpawn.Get(GetPositionOutCamera, AssetId.EnemyLevel1), 0f, 40,  1f);
         G.scheduler.Schedule(CommandSpawn.Get(GetPositionOutCamera, AssetId.EnemyLevel2), 5f, 10,  0.2f);
         G.scheduler.Schedule(CommandSpawn.Get(GetPositionOutCamera, AssetId.EnemyLevel3), 1f, 5,   0.2f);
         G.scheduler.Schedule(CommandSpawn.Get(GetPositionOutCamera, AssetId.EnemyLevel4), 8f, 0,   0f);
         G.scheduler.Schedule(CommandSpawn.Get(GetPositionOutCamera, AssetId.EnemyLevel1), 0f, 100, 0.2f);
         Tween.Custom(G.camera, G.camera.zoom, 1.75f, 0.25f, (service, f) => service.Zoom(f), startDelay: 2f);
      }));

      triggers.Add((() => vars.timestampBuildRadar.WithSince(150f), () =>
      {
         G.scheduler.Schedule(CommandSpawn.Get(GetPositionOutCamera, AssetId.EnemyLevel1), 0f, 100, 1f);
         G.scheduler.Schedule(CommandSpawn.Get(GetPositionOutCamera, AssetId.EnemyLevel2), 5f, 10,  0.5f);
         G.scheduler.Schedule(CommandSpawn.Get(GetPositionOutCamera, AssetId.EnemyLevel3), 0f, 5,   0.2f);
         G.scheduler.Schedule(CommandSpawn.Get(GetPositionOutCamera, AssetId.EnemyLevel4), 8f, 2,   1f);
         G.scheduler.Schedule(CommandSpawn.Get(GetPositionOutCamera, AssetId.EnemyLevel1), 0f, 100, 0.2f);

         G.scheduler.Schedule(CommandSpawn.Get(GetPositionOutCamera, AssetId.EnemyLevel1), 10f, 100, 0.1f);

         G.game.allowSpawnUpgrade++;
         G.scheduler.Schedule(CommandSpawn.Get(GetPositionOutCameraCenter, AssetId.EnemyBoss), 20f);
      }));
   }

   static void ReleaseTimeline(List<(Func<bool> condition, Action action)> triggers, GameVars vars)
   {
      triggers.Add((() => vars.timestampBuildRadar.WithSince(2f), () =>
      {
         G.scheduler.Schedule(CommandSpawn.Get(GetPositionOutCamera, AssetId.EnemyLevel1), 1f, 100, 0.5f);
         G.scheduler.Schedule(CommandSpawn.Get(GetPositionOutCamera, AssetId.EnemyLevel1), 5f, 5,   0.1f);
      }));

      triggers.Add((() => vars.timestampBuildRadar.WithSince(15f), () =>
      {
         G.scheduler.Schedule(CommandSpawn.Get(GetPositionOutCamera, AssetId.EnemyLevel1), 0f, 10,  0.2f);
         G.scheduler.Schedule(CommandSpawn.Get(GetPositionOutCamera, AssetId.EnemyLevel1), 0f, 5,   0.1f);
         G.scheduler.Schedule(CommandSpawn.Get(GetPositionOutCamera, AssetId.EnemyLevel1), 0f, 100, 1f);
      }));

      triggers.Add((() => vars.timestampBuildRadar.WithSince(30f), () =>
      {
         G.scheduler.Schedule(CommandSpawn.Get(GetPositionOutCamera, AssetId.EnemyLevel1), 0f, 20, 1f);
         G.scheduler.Schedule(CommandSpawn.Get(GetPositionOutCamera, AssetId.EnemyLevel1), 0f, 10, 1f);
         G.scheduler.Schedule(CommandSpawn.Get(GetPositionOutCamera, AssetId.EnemyLevel1), 0f, 5,  0.1f);
         G.scheduler.Schedule(CommandSpawn.Get(GetPositionOutCamera, AssetId.EnemyLevel2), 1f, 0,  0f);
         G.game.allowSpawnUpgrade++;
         Tween.Custom(G.camera, G.camera.zoom, 1.25f, 0.5f, (service, f) => service.Zoom(f), startDelay: 2f);
      }));

      triggers.Add((() => vars.timestampBuildRadar.WithSince(45f), () =>
      {
         G.scheduler.Schedule(CommandSpawn.Get(GetPositionOutCamera, AssetId.EnemyLevel1), 0f, 20, 0.2f);
         G.scheduler.Schedule(CommandSpawn.Get(GetPositionOutCamera, AssetId.EnemyLevel2), 0f, 3,  0f);
         G.scheduler.Schedule(CommandSpawn.Get(GetPositionOutCamera, AssetId.EnemyLevel2), 5f, 3,  0.2f);
      }));

      triggers.Add((() => vars.timestampBuildRadar.WithSince(60f), () =>
      {
         G.scheduler.Schedule(CommandSpawn.Get(GetPositionOutCamera, AssetId.EnemyLevel1), 0f,  10, 1f);
         G.scheduler.Schedule(CommandSpawn.Get(GetPositionOutCamera, AssetId.EnemyLevel2), 5f,  5,  0.2f);
         G.scheduler.Schedule(CommandSpawn.Get(GetPositionOutCamera, AssetId.EnemyLevel3), 7f,  0,  0f);
         G.scheduler.Schedule(CommandSpawn.Get(GetPositionOutCamera, AssetId.EnemyLevel1), 0f,  50, 0.5f);
         G.scheduler.Schedule(CommandSpawn.Get(GetPositionOutCamera, AssetId.EnemyLevel1), 10f, 10, 0.2f);
         Tween.Custom(G.camera, G.camera.zoom, 1.5f, 0.25f, (service, f) => service.Zoom(f), startDelay: 2f);
      }));

      triggers.Add((() => vars.timestampBuildRadar.WithSince(80f), () =>
      {
         G.scheduler.Schedule(CommandSpawn.Get(GetPositionOutCamera, AssetId.EnemyLevel1), 0f, 10,  1f);
         G.scheduler.Schedule(CommandSpawn.Get(GetPositionOutCamera, AssetId.EnemyLevel2), 2f, 5,   0.5f);
         G.scheduler.Schedule(CommandSpawn.Get(GetPositionOutCamera, AssetId.EnemyLevel3), 1f, 3,   0.5f);
         G.scheduler.Schedule(CommandSpawn.Get(GetPositionOutCamera, AssetId.EnemyLevel1), 0f, 100, 0.2f);
         G.scheduler.Schedule(CommandSpawn.Get(GetPositionOutCamera, AssetId.EnemyLevel1), 0f, 20,  0.75f);

         G.scheduler.Schedule(CommandSpawn.Get(GetPositionOutCamera, AssetId.EnemyLevel1), 20f, 10, 0.2f);
         G.scheduler.Schedule(CommandSpawn.Get(GetPositionOutCamera, AssetId.EnemyLevel1), 20f, 10, 0.2f);
      }));

      triggers.Add((() => vars.timestampBuildRadar.WithSince(82f), () => { G.game.allowSpawnUpgrade++; }));

      triggers.Add((() => vars.timestampBuildRadar.WithSince(100f), () =>
      {
         G.scheduler.Schedule(CommandSpawn.Get(GetPositionOutCamera, AssetId.EnemyLevel1), 0f, 40,  1f);
         G.scheduler.Schedule(CommandSpawn.Get(GetPositionOutCamera, AssetId.EnemyLevel2), 5f, 10,  0.2f);
         G.scheduler.Schedule(CommandSpawn.Get(GetPositionOutCamera, AssetId.EnemyLevel3), 1f, 5,   0.5f);
         G.scheduler.Schedule(CommandSpawn.Get(GetPositionOutCamera, AssetId.EnemyLevel4), 2f, 0,   0f);
         G.scheduler.Schedule(CommandSpawn.Get(GetPositionOutCamera, AssetId.EnemyLevel1), 0f, 100, 0.2f);

         G.scheduler.Schedule(CommandSpawn.Get(GetPositionOutCamera, AssetId.EnemyLevel1), 10f, 20, 0.2f);
         G.scheduler.Schedule(CommandSpawn.Get(GetPositionOutCamera, AssetId.EnemyLevel2), 10f, 5,  0.3f);

         Tween.Custom(G.camera, G.camera.zoom, 1.75f, 0.25f, (service, f) => service.Zoom(f), startDelay: 2f);
         G.game.allowSpawnUpgrade++;
      }));

      triggers.Add((() => vars.timestampBuildRadar.WithSince(130f), () =>
      {
         G.scheduler.Schedule(CommandSpawn.Get(GetPositionOutCamera, AssetId.EnemyLevel1), 0f, 100, 1f);
         G.scheduler.Schedule(CommandSpawn.Get(GetPositionOutCamera, AssetId.EnemyLevel2), 5f, 15,  0.5f);
         G.scheduler.Schedule(CommandSpawn.Get(GetPositionOutCamera, AssetId.EnemyLevel3), 0f, 10,  0.2f);
         G.scheduler.Schedule(CommandSpawn.Get(GetPositionOutCamera, AssetId.EnemyLevel4), 3f, 5,   1f);
         G.scheduler.Schedule(CommandSpawn.Get(GetPositionOutCamera, AssetId.EnemyLevel1), 0f, 20,  0.2f);

         G.scheduler.Schedule(CommandSpawn.Get(GetPositionOutCamera, AssetId.EnemyLevel1), 10f, 20,  0.1f);
         G.scheduler.Schedule(CommandSpawn.Get(GetPositionOutCamera, AssetId.EnemyLevel1), 0f,  100, 0.1f);
         G.scheduler.Schedule(CommandSpawn.Get(GetPositionOutCamera, AssetId.EnemyLevel1), 5f,  100, 0.1f);
         G.scheduler.Schedule(CommandSpawn.Get(GetPositionOutCamera, AssetId.EnemyLevel1), 15f, 20,   0.2f);
         G.scheduler.Schedule(CommandSpawn.Get(GetPositionOutCamera, AssetId.EnemyLevel2), 15f, 15,   0.3f);
         
         G.scheduler.Schedule(CommandSpawn.Get(GetPositionOutCameraB, AssetId.EnemyLevel1), 25f, 20, 0.1f);
      }));

      triggers.Add((() => vars.timestampBuildRadar.WithSince(160f), () =>
      {
         G.scheduler.Schedule(CommandSpawn.Get(GetPositionOutCameraA, AssetId.EnemyLevel1), 0f, 15, 0.1f);
         G.scheduler.Schedule(CommandSpawn.Get(GetPositionOutCameraA, AssetId.EnemyLevel2), 4f, 5,  0.4f);
         G.scheduler.Schedule(CommandSpawn.Get(GetPositionOutCameraA, AssetId.EnemyLevel3), 4f, 5,  0.4f);
         G.scheduler.Schedule(CommandSpawn.Get(GetPositionOutCameraA, AssetId.EnemyLevel4), 4f, 5,  0.4f);

         G.scheduler.Schedule(CommandSpawn.Get(GetPositionOutCameraA, AssetId.EnemyLevel1), 6f, 15, 0.1f);
         G.scheduler.Schedule(CommandSpawn.Get(GetPositionOutCameraA, AssetId.EnemyLevel2), 6f, 5,  0.1f);
         G.scheduler.Schedule(CommandSpawn.Get(GetPositionOutCameraA, AssetId.EnemyLevel3), 6f, 5,  0.1f);
         G.scheduler.Schedule(CommandSpawn.Get(GetPositionOutCameraA, AssetId.EnemyLevel4), 6f, 5,  0.1f);

         CommandSpawn.Get(GetPositionOutCameraCenter, AssetId.EnemyBoss).Execute();
      }));

      triggers.Add((() => vars.timestampBuildRadar.WithSince(162f), () => { Tween.Custom(G.camera, G.camera.zoom, 2.5f, 0.25f, (service, f) => service.Zoom(f)); }));
   }

   static Vector2 GetPositionOutCamera()
   {
      var rect = G.camera.rect;
      // sideExtra — насколько можно залезть левее/правее
      float sideExtra = 5f;
      // topOffset — насколько выше верхней границы
      float topOffset = 4f;

      float x = Random.Range(rect.xMin - sideExtra, rect.xMax + sideExtra);
      float y = rect.yMax + Random.Range(0.5f, topOffset);

      return new Vector2(x, y);
   }

   static Vector2 GetPositionOutCameraCenter()
   {
      var rect = G.camera.rect;
      // sideExtra — насколько можно залезть левее/правее
      float sideExtra = 3f;
      // topOffset — насколько выше верхней границы
      float topOffset = 15f;

      float x = Random.Range(rect.center.x - sideExtra, rect.center.x + sideExtra);
      float y = rect.yMax + Random.Range(0.5f, topOffset);

      return new Vector2(x, y);
   }

   static Vector2 GetPositionOutCameraA()
   {
      var rect = G.camera.rect;
      // sideExtra — насколько можно залезть левее/правее
      float sideExtra = 5f;
      // topOffset — насколько выше верхней границы
      float topOffset = 4f;

      float x = Random.value > 0.5f ? Random.Range(rect.xMin - sideExtra, rect.xMin - sideExtra * 0.5f) : Random.Range(rect.xMax + sideExtra, rect.xMax + sideExtra * 0.5f);
      float y = rect.yMax + Random.Range(0.5f,                            topOffset);

      return new Vector2(x, y);
   }
   
   static Vector2 GetPositionOutCameraB()
   {
      var rect = G.camera.rect;
      // sideExtra — насколько можно залезть левее/правее
      float sideExtra = 5f;
      // topOffset — насколько выше верхней границы
      float topOffset = 4f;

      float x = Random.value > 1f ? Random.Range(rect.xMin - sideExtra, rect.xMin - sideExtra * 0.5f) : Random.Range(rect.xMax + sideExtra, rect.xMax + sideExtra * 0.5f);
      float y = rect.yMax + Random.Range(0.5f,                            topOffset);

      return new Vector2(x, y);
   }
}