using UnityEngine;

public class CrossPresentation : MonoBehaviour
{
   [SerializeField] Transform body;
   [SerializeField] Transform view;

   public void SetPosition(Vector3 position)
   {
      body.transform.position = position;
   }

   public void Hide()
   {
      view.gameObject.SetActive(false);
   }

   public void Show()
   {
      view.gameObject.SetActive(true);
   }
}