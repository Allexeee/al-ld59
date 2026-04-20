using System;
using AssetsPlugin;
using PrimeTween;
using UnityEngine;

public class UpgradePresentation : MonoBehaviour
{
   [SerializeField] Transform view;

   Sequence seq;
   
   public void PlayIdle()
   {
      seq = Sequence.Create(-1);
      seq.Chain(Tween.PunchScale(view, Vector3.one, 0.2f));
      seq.ChainDelay(5f);
   }

   void OnDisable()
   {
      seq.Stop();
   }
}