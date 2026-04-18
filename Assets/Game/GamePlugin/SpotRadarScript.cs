using AssetsPlugin;
using UnityEngine;

public class SpotRadarScript : UnityScript, IGameCanBuildRadar, IInteractable
{
   public SpotStructurePresentation presentation;

   AssetContainer asset;

   protected override void OnAwake()
   {
      base.OnAwake();

      asset = G.db.GetAsset(AssetId.Radar);
      presentation.HideAll();
   }

   protected override void OnDisable()
   {
      base.OnDisable();
      presentation.HideAll();
   }

   public void OnRadarCanBuild()
   {
      presentation.ShowPlace();
      presentation.ShowButton();
   }

   public Vector2 GetInteractCenterPosition()
   {
      return transform.position;
   }

   public void Interact()
   {
      G.game.BuildRadar(this);
   }
}