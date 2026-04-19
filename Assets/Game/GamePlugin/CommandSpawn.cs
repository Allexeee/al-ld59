using System;
using UnityEngine;

public class CommandSpawn : ICommand
{
   public Vector2       position;
   public Func<Vector2> funcPosition;
   public AssetId       assetId;

   public static CommandSpawn Get(Vector2 position, AssetId assetId)
   {
      var obj = G.poolAnyObject.Get<CommandSpawn>();
      obj.position = position;
      obj.assetId  = assetId;
      return obj;
   }

   public static CommandSpawn Get(Func<Vector2> position, AssetId assetId)
   {
      var obj = G.poolAnyObject.Get<CommandSpawn>();
      obj.funcPosition = position;
      obj.assetId      = assetId;
      return obj;
   }

   public void Execute()
   {
      if (funcPosition != default)
         position = funcPosition();

      G.spawner.SpawnUniversal(position, assetId);
   }
}