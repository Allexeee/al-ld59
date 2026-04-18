using System;
using PrimeTween;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   public HeroScript        hero  { get; set; }
   public GameState         state { get; set; }
   public CrossPresentation crossPresentation;
   public HeroPresentation  heroPresentation;
   public HeroController    heroController = new();

   void Awake()
   {
      state = GameState.Default;
      crossPresentation.Hide();

      foreach (var e in G.events.Get<IGameCanBuildRadar>().All)
         e.OnRadarCanBuild();
   }

   public void SceneLoading()
   {
   }

   public void SceneLoaded()
   {
      foreach (var e in G.events.Get<IEventSceneReady>().All)
         e.OnSceneReady();
   }

   void Update()
   {
      heroController.OnInput();

      switch (state)
      {
         case GameState.Default:
            var mouseWorldPos = GetMouseWorldPos();
            DoCrossFollowMouse(mouseWorldPos);
            DoHeroRotateMouse(mouseWorldPos);
            DoHeroInteract();
            break;
         default:
            throw new ArgumentOutOfRangeException();
      }
   }

   void FixedUpdate()
   {
      switch (state)
      {
         case GameState.Default:
            DoHeroMove();
            break;
         default:
            throw new ArgumentOutOfRangeException();
      }
   }

   void DoCrossFollowMouse(Vector3 mouseWorldPos)
   {
      crossPresentation.Show();
      crossPresentation.SetPosition(mouseWorldPos);
   }

   void DoHeroRotateMouse(Vector3 mouseWorldPos)
   {
      var direction = (Vector2) (mouseWorldPos - hero.transform.position).normalized;
      var angle     = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
      heroPresentation.RotateTo(Quaternion.Euler(0, 0, angle));
   }

   void DoHeroMove()
   {
      var movement = heroController.movement;

      var speed = heroPresentation.asset.As<AssetMovement>().speed;
      heroPresentation.AddPosition(movement * speed * Time.deltaTime);
   }

   void DoHeroInteract()
   {
      if (heroController.wantInteract)
      {
         var filter = FilterNearest.Get(heroPresentation.transform.position);
         var nearest = default(IInteractable);
         foreach (var e in G.events.Get<IInteractable>().All)
         {
            var pos                              = e.GetInteractCenterPosition();
            if (filter.IsMatch(pos, 2f)) 
               nearest = e;
         }

         nearest?.Interact();
      }
   }

   static Vector3 GetMouseWorldPos()
   {
      var mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
      mouseWorldPos.z = 0f; // Для 2D
      return mouseWorldPos;
   }

   public void BuildRadar(SpotRadarScript spotRadarScript)
   {
      // todo: ээфекты и звуки установки объекта
      G.spawner.SpawnRadar(spotRadarScript.transform.position);
      G.spawner.Despawn(spotRadarScript.gameObject);

      Tween.ShakeCamera(Camera.main, 5f, 0.2f, 100);
   }
}

public enum GameState
{
   Default
}