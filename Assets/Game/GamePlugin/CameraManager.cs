using System;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
   public Camera camera;
   public Rect   rect { get; private set; }

   public Transform target; // Объект, который должен быть всегда в нижней границе камеры
   public float     zoom;
   public bool      inCinema;

   float orthographicSizeBase;

   void Awake()
   {
      orthographicSizeBase = camera.orthographicSize;
   }

   void Update()
   {
      Vector3 bottomLeft = camera.ViewportToWorldPoint(new Vector3(0, 0, camera.nearClipPlane));
      Vector3 topRight   = camera.ViewportToWorldPoint(new Vector3(1, 1, camera.nearClipPlane));

      rect = new Rect(
         bottomLeft.x,
         bottomLeft.y,
         topRight.x - bottomLeft.x,
         topRight.y - bottomLeft.y
      );
   }

   void LateUpdate()
   {
      if (target == null)
         return;

      // Устанавливаем новый зум
      camera.orthographicSize = orthographicSizeBase * zoom;

      if (!inCinema)
      {
         // Вычисляем, какой должна быть позиция камеры по Y 
         Vector3 camPos = camera.transform.position;
         camPos.y                  = target.position.y + camera.orthographicSize;
         camera.transform.position = camPos;
      }
   }

   public void Zoom(float zoom)
   {
      if (inCinema) return;

      this.zoom = zoom;
   }

   public void CinemaZoom(float zoom)
   {
      inCinema  = true;
      this.zoom = zoom;
   }

   void OnDrawGizmos()
   {
      Gizmos.color = Color.red;
      Gizmos.DrawWireCube(rect.center, rect.size);
   }
}