using UnityEngine;

public class CameraService : MonoBehaviour
{
   public Camera camera;
   public Rect   rect { get; private set; }

   void FixedUpdate()
   {
      var height = camera.orthographicSize * 2f;
      var width  = height                  * camera.aspect;

      var pos = transform.position;

      var xMin = pos.x - width  * 0.5f;
      var yMin = pos.y - height * 0.5f;

      rect = new Rect(xMin, yMin, width, height);
   }
}