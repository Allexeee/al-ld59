using PrimeTween;
using UnityEngine;

public class GameOverPresentation : MonoBehaviour
{
   [SerializeField] CanvasGroup canvasGroup;
   
   public void SetAlpha(float value)
   {
      canvasGroup.alpha = value;
   }
   
   public void DoFade(int direction)
   {
      Tween.Alpha(canvasGroup, direction == 1 ? 1f : 0f, 0.5f);
   }
}