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

   void FixedUpdate()
   {
      var height = camera.orthographicSize * 2f;
      var width  = height                  * camera.aspect;

      var pos = transform.position;

      var xMin = pos.x - width  * 0.5f;
      var yMin = pos.y - height * 0.5f;

      rect = new Rect(xMin, yMin, width, height);
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
}