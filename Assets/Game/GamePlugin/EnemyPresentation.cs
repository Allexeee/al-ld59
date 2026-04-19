using AssetsPlugin;
using UnityEngine;

public class EnemyPresentation : MonoBehaviour
{
   [SerializeField] new Rigidbody2D rigidbody;

   public Rigidbody2D rb => rigidbody;
   
   private Vector2 pushOffset = Vector2.zero;
   public  Vector2 lastDirection;

   public void AddPush(Vector2 value)
   {
      pushOffset += value;
   }
   
   public void AddPosition(Vector2 position)
   {
      rigidbody.MovePosition(rigidbody.position + position + pushOffset);
      // После применения pushOffset затухает (например, экспоненциально)
      pushOffset = Vector2.Lerp(pushOffset, Vector2.zero, 8f * Time.deltaTime);
   }

   public void RotateByDirection(Vector2 direction)
   {
      lastDirection = direction;
      var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90f;
      rigidbody.MoveRotation(Quaternion.Euler(0, 0, angle));
   }
}