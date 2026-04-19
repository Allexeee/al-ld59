using AssetsPlugin;
using UnityEngine;

public class ProjectilePresentation : MonoBehaviour
{
   [SerializeField] new Rigidbody2D rigidbody;

   public void AddPosition(Vector2 position)
   {
      rigidbody.MovePosition(rigidbody.position + position);
   }

   public void SetRotateByDirection(Vector2 direction)
   {
      var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
      transform.localRotation = Quaternion.Euler(0, 0, angle);
   }
}