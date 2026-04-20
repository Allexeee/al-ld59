using System;
using AssetsPlugin;
using PrimeTween;
using UnityEngine;
using UnityEngine.UI;

public class SendSignalPresentation : MonoBehaviour
{
   [SerializeField] GameObject root;
   [SerializeField] Image      progressBar;

   public void SetVisible(bool value)
   {
      root.SetActive(value);
   }

   public void SetProgress(float ratio)
   {
      progressBar.fillAmount = ratio;
   }
   
   public void Appear()
   {
      var seq = Sequence.Create();
      seq.Chain(Tween.PunchScale(root.transform, Vector3.one, 0.2f));
      seq.Group(Tween.UIAnchoredPosition(root.GetComponent<RectTransform>(), new Vector2(500f, 0f), Vector2.zero, 0.2f));
   }
}