using System.Collections;
using PrimeTween;
using UnityEngine;

public class CinematicD : MonoBehaviour
{
   [SerializeField] GameObject spaceShip;

   GameObject hero;

   public void Play(GameManager gameManager)
   {
      hero = G.game.hero.gameObject;

      StartCoroutine(Cinema(gameManager));
   }

   IEnumerator Cinema(GameManager gameManager)
   {
      var cameraRect = G.camera.rect;

      var startPos = new Vector3(cameraRect.xMin - 5f, hero.transform.position.y);
      var endPos   = new Vector3(cameraRect.xMax + 5f, hero.transform.position.y);

      hero.GetComponent<Collider2D>().enabled = false;
      spaceShip.transform.Find("View/C").gameObject.SetActive(false);
      
      yield return Tween.PositionAtSpeed(spaceShip.transform, startPos, hero.transform.position, 5f, Easing.Standard(Ease.OutSine));

      hero.SetActive(false);
      spaceShip.transform.Find("View/B").gameObject.SetActive(false);
      spaceShip.transform.Find("View/C").gameObject.SetActive(true);

      yield return Tween.PositionAtSpeed(spaceShip.transform, endPos, 7f, Easing.Standard(Ease.InSine));

      yield return new WaitForSeconds(1f);

      gameManager.SetPause(true);
      gameManager.gameEndPresentation.DoFade(1);
   }
}