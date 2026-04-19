using UnityEngine;

public class CommandDespawn : ICommand
{
   public GameObject gameObject;

   public static CommandDespawn Get(GameObject gameObject)
   {
      var obj = G.poolAnyObject.Get<CommandDespawn>();
      obj.gameObject = gameObject;
      return obj;
   }

   public void Execute()
   {
      G.spawner.Despawn(gameObject);
   }
}