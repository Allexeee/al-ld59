using AssetsPlugin;
using UnityEngine;

public class EnemyPresentation : MonoBehaviour
{
   [SerializeField] new Rigidbody2D rigidbody;
   [SerializeField]     Transform   body;

   public Rigidbody2D rb => rigidbody;

   private Vector2 pushOffset = Vector2.zero;
   [HideInInspector]
   public Vector2 lastDirection;
   float size;

   public void AddPush(Vector2 value)
   {
      // Минимальный коэффициент (например, даже у самого большого врага остается 10% от исходного пинка)
      float minMultiplier = 0.01f;

      // Чем больше size, тем меньше коэффициент
      float mul = 0.5f / size;
      // Но не даём ему уйти ниже минимума:
      mul = Mathf.Max(mul, minMultiplier);

      pushOffset = value * mul;
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

   public void SetSize(float size)
   {
      this.size       = size;
      rb.mass         = size * 10f;
      body.localScale = new Vector3(size, size, 1f);
   }
}