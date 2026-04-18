using UnityEngine;

public class SpotStructurePresentation : MonoBehaviour
{
   [SerializeField] Transform place;

   GameObject btn;

   public void HideAll()
   {
      place.gameObject.SetActive(false);

      if (btn != default)
         G.spawner.Despawn(btn);
   }
   
   public void ShowPlace()
   {
      place.gameObject.SetActive(true);
   }
   
   public void ShowButton()
   {
      btn = G.spawner.SpawnUiBtnInteract(transform.position.WithOffsetY(-0.8f));
   }
}