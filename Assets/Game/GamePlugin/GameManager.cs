using System;
using PrimeTween;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
   public HeroScript             hero  { get; set; }
   public GameState              state { get; set; }
   public RadarScript            radar { get; set; }
   public CrossPresentation      crossPresentation;
   public HeroPresentation       heroPresentation;
   public HeroController         heroController = new();
   public SendSignalPresentation sendSignalPresentation;
   public GameRadarSendSignal    gameRadarSendSignal;
   public ScreenFadePresentation screenFadePresentation;
   public GameOverPresentation   gameOverPresentation;
   public GameWinPresentation    gameWinPresentation;
   public GameEndPresentation    gameEndPresentation;
   public CinematicD             cinematicD;
   public CinematicA             cinematicA;

   HandlerPauseGame handlerPauseGame;
   bool             paused;

   void Awake()
   {
      state = GameState.Default;
      crossPresentation.Hide();
      sendSignalPresentation.SetVisible(false);
      gameRadarSendSignal = new();
      screenFadePresentation.SetAlpha(0f);
      gameOverPresentation.SetAlpha(0f);
      handlerPauseGame = new();
      gameWinPresentation.SetAlpha(0f);
      gameEndPresentation.SetAlpha(0f);

      G.camera.Zoom(0.5f);
   }

   public void SceneLoading()
   {
   }

   public void SceneLoaded()
   {
      foreach (var e in G.events.Get<IEventSceneReady>().All)
         e.OnSceneReady();


      foreach (var e in G.events.Get<IGameCanBuildRadar>().All)
         e.OnRadarCanBuild();
   }

   void Update()
   {
      var dt = Time.deltaTime;
      G.timeline.Update(dt);

      heroController.OnInput();

      if (!paused)
         G.scheduler.OnUpdate(dt);

      switch (state)
      {
         case GameState.Default:
            var mouseWorldPos = GetMouseWorldPos();
            DoCrossFollowMouse(mouseWorldPos);
            DoHeroRotateMouse(mouseWorldPos);
            if (hero.state != HeroState.Stunned)
            {
               DoHeroInteract();
               DoHeroShoot();
            }

            if (radar != default)
            {
               gameRadarSendSignal.Progress(dt, 1f);
               var ratio = gameRadarSendSignal.progress / G.db.GetAsset(AssetId.RadarSignal).As<AssetRadarSignal>().max;
               sendSignalPresentation.SetProgress(ratio);

               if (ratio >= 1f)
               {
                  state = GameState.WinChoose;
                  SetGamePause(true);
                  gameWinPresentation.DoFade(1);
               }
            }

            break;
         case GameState.GameOver:
            break;
         case GameState.ReadyToRestart:
            if (Input.anyKey)
            {
               RestartGame();
            }

            break;

         case GameState.WinChoose:
            if (Input.GetKeyDown(KeyCode.A))
            {
               state = GameState.CinematicA;
               gameWinPresentation.DoFade(-1);
               SetGamePause(false);
               cinematicA.Play(this);
               sendSignalPresentation.SetVisible(false);
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
               state = GameState.CinematicD;
               gameWinPresentation.DoFade(-1);
               SetGamePause(false);
               cinematicD.Play(this);
               sendSignalPresentation.SetVisible(false);
            }

            break;
      }

      if (Input.GetKeyDown(KeyCode.P))
         RestartGame();
   }

   void FixedUpdate()
   {
      switch (state)
      {
         case GameState.Default:
            if (hero.state != HeroState.Stunned)
               DoHeroMove();

            break;
      }
   }

   void RestartGame()
   {
      SceneManager.LoadScene("Game");
   }

   void DoCrossFollowMouse(Vector3 mouseWorldPos)
   {
      crossPresentation.Show();
      crossPresentation.SetPosition(mouseWorldPos);
   }

   void DoHeroRotateMouse(Vector3 mouseWorldPos)
   {
      var direction = (Vector2) (mouseWorldPos - hero.transform.position).normalized;
      var angle     = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90f;
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
         // var filter  = FilterNearest.Get(heroPresentation.transform.position);
         var nearest = default(IInteractable);
         foreach (var e in G.events.Get<IInteractable>().All)
         {
            // var pos = e.GetInteractCenterPosition();
            // if (filter.IsMatch(pos, 2f))
            nearest = e;
         }

         nearest?.Interact();
      }
   }

   float timeShot;

   void DoHeroShoot()
   {
      // if (heroController.wantShot)
      if (heroController.wantShot || G.vars.timestampBuildRadar.WithSince(0f))
      {
         var asset = G.db.GetAsset(AssetId.AbilityA);
         if (Time.time - timeShot >= 60f / asset.As<AssetAbilityAttackA>().rateInMin)
         {
            timeShot = Time.time;
            var direction = GetDirectionToMouse();
            G.audio.Play(hero.transform.position, AssetId.HeroSfxShoot);
            G.spawner.SpawnProjectile(hero.transform.position, direction, asset.As<AssetDamage>().damage);
         }
      }
   }

   static Vector3 GetMouseWorldPos()
   {
      var mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
      mouseWorldPos.z = 0f; // Для 2D
      return mouseWorldPos;
   }

   Vector3 GetDirectionToMouse()
   {
      return (Vector2) (GetMouseWorldPos() - hero.transform.position).normalized;
   }

   public void RadarBuild(SpotRadarScript spotRadarScript)
   {
      G.spawner.SpawnRadar(spotRadarScript.transform.position);
      G.spawner.Despawn(spotRadarScript.gameObject);
      G.vars.timestampBuildRadar.Stamp();

      sendSignalPresentation.SetVisible(true);
      sendSignalPresentation.SetProgress(0f);

      var seq = Sequence.Create();
      seq.Chain(Tween.ShakeCamera(Camera.main, 5f, 0.2f, 100));
      seq.Chain(Tween.Custom(G.camera, G.camera.zoom, 1f, 0.5f, (service, f) => service.Zoom(f)));
   }

   public void RadarDamage(EnemyAScript source)
   {
      source.Kill();
      radar.GetDamage();
      Tween.ShakeCamera(Camera.main, 2.5f, 0.2f, 50);
   }

   public void RadarDestroy()
   {
      if (state != GameState.Default) return;
      Debug.Log("Radar Destroy");

      Tween.ShakeCamera(Camera.main, 2.5f, 5f, 100);
      G.scheduler.Schedule(PlayAudioCommand.Get(radar.transform.position, AssetId.EnemySfxDestroy), 0f, 5, 0.1f);

      state = GameState.GameOver;
      SetGamePause(true);

      var seq = Sequence.Create();
      seq.Chain(Tween.Delay(1f));
      seq.Chain(Tween.Custom(this, 0f, 1f, 0.5f, (t, val) => t.screenFadePresentation.DoFade(1)));
      seq.Chain(Tween.Custom(this, 0f, 1f, 0.5f, (t, val) => t.gameOverPresentation.DoFade(1)));
      seq.Chain(Tween.Custom(this, 0f, 1f, 1f,   (t, val) => t.state = GameState.ReadyToRestart));
   }

   public void OnProjectileCollisionWithEnemy(ProjectileScript source, EnemyAScript enemyScript)
   {
      var sourcePosition = source.transform.position;

      G.spawner.Despawn(source.gameObject);
      G.audio.Play(sourcePosition, AssetId.ProjectileAudioHit);
      G.spawner.SpawnUniversal(sourcePosition, AssetId.ProjectileVfxHit);
      enemyScript.PushBack(0.05f);
      enemyScript.GetDamage(source.damage);
   }

   void SetGamePause(bool value)
   {
      paused = value;
      foreach (var e in G.events.Get<IGamePaused>().All)
      {
         e.OnPaused(value);
      }
   }

   public void SetPause(bool val) => SetGamePause(val);
}

public enum GameState
{
   Default,
   GameOver,
   ReadyToRestart,
   WinChoose,
   CinematicA,
   CinematicD,
}