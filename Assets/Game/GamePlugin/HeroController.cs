using UnityEngine;

public class HeroController
{
   public Vector2 movement;
   public bool    wantInteract;
   public bool    wantShot;
   
   public void OnInput()
   {
      movement = Vector2.zero;
      if (Input.GetKey(KeyCode.W))
      {
         movement.y += 1;
      }

      if (Input.GetKey(KeyCode.S))
      {
         movement.y -= 1;
      }

      if (Input.GetKey(KeyCode.A))
      {
         movement.x -= 1;
      }

      if (Input.GetKey(KeyCode.D))
      {
         movement.x += 1;
      }

      // Нормализация для одинаковой скорости по диагонали
      movement = movement.normalized;

      wantInteract = Input.GetKeyDown(KeyCode.Space);
      wantShot     = Input.GetKey(KeyCode.Mouse0);
   }
}