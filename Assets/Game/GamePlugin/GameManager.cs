using UnityEngine;

public class GameManager : MonoBehaviour
{
   public void SceneLoading()
   {
   }

   public void SceneLoaded()
   {
      foreach (var e in G.events.Get<IEventSceneReady>().All)
         e.OnSceneReady();
   }
}