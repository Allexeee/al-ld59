using System;
using System.Collections;
using PrimeTween;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CinematicA : MonoBehaviour
{
   [SerializeField] Light2D light;

   GameObject hero;

   void Awake()
   {
      light.enabled = false;
   }

   public void Play(GameManager gameManager)
   {
      hero = G.game.hero.gameObject;

      StartCoroutine(Cinema(gameManager));
   }

   IEnumerator Cinema(GameManager gameManager)
   {
      light.enabled   = true;
      light.intensity = 1f;
      // light.pointLightOuterRadius = 1f;

      G.camera.inCinema = true;

      var t = Tween.ShakeCamera(Camera.main, 5f, 10f, 100);

      yield return new WaitForSeconds(0.5f);
      //
      // var seq1 = Sequence.Create();
      // seq1.ChainDelay(0.1f);
      // seq1.Group(Tween.Custom(this, light.pointLightOuterRadius, 4f,  4f, ((a, f) => a.light.pointLightOuterRadius = f)));
      // seq1.Group(Tween.Custom(this, light.pointLightInnerRadius, 2f,  4f, ((a, f) => a.light.pointLightInnerRadius = f)));
      // seq1.Group(Tween.Custom(this, light.intensity,             20f, 4f, ((a, f) => a.light.intensity             = f)));
      //
      // yield return seq1;

      var min = G.camera.zoom * 0.5f;
      if (min < 0.8f)
         min = 0.8f;

      var seq2 = Sequence.Create();
      seq2.ChainDelay(0.1f);
      seq2.Group(Tween.Custom(this,     light.pointLightOuterRadius, 2f,  1f,   ((a, f) => a.light.pointLightOuterRadius = f)));
      seq2.Group(Tween.Custom(this,     light.pointLightInnerRadius, 1f,  1f,   ((a, f) => a.light.pointLightInnerRadius = f)));
      seq2.Group(Tween.Custom(this,     light.intensity,             0f,  1.2f, ((a, f) => a.light.intensity             = f)));
      seq2.Group(Tween.Custom(G.camera, G.camera.zoom,               min, 1.6f, ((a, f) => a.CinemaZoom(f))));

      yield return seq2;
      yield return new WaitForSeconds(0.5f);

      gameManager.SetPause(true);
      gameManager.radar.transform.Find("Vfx").gameObject.SetActive(false);

      var max = 4f;

      var seq3 = Sequence.Create();
      seq3.ChainDelay(0.1f);
      seq3.Group(Tween.Custom(G.camera, G.camera.zoom,               max,  1f, ((a, f) => a.CinemaZoom(f))));
      
      // seq3.Group(Tween.Custom(this, light.intensity,             100f, 2f, ((a, f) => a.light.intensity             = f)));

      yield return seq3;

      var seq4 = Sequence.Create();
      seq4.ChainDelay(0.2f);
      seq4.Group(Tween.Custom(this,     light.pointLightOuterRadius, 60f, 0.5f,   ((a, f) => a.light.pointLightOuterRadius = f)));
      seq4.Group(Tween.Custom(this,     light.pointLightInnerRadius, 50f, 0.5f,   ((a, f) => a.light.pointLightInnerRadius = f)));
      yield return seq4;

      yield return new WaitForSeconds(1f);

      t.Stop();

      gameManager.gameEndPresentation.DoFade(1);
   }
}