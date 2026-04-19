using System;
using AssetsPlugin;
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
}