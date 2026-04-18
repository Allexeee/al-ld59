using UnityEngine;

public struct FilterNearest
{
   Vector2 point;
   float   distanceSqr;

   public static FilterNearest Get(Vector2 point)
   {
      var obj = new FilterNearest();
      obj.point       = point;
      obj.distanceSqr = float.MaxValue;
      return obj;
   }

   public bool IsMatch(Vector2 nextPoint)
   {
      var vector = nextPoint - point;

      var distance = vector.sqrMagnitude;
      if (distance < distanceSqr)
      {
         distanceSqr = distance;
         return true;
      }

      return false;
   }
    
   public bool IsMatch(Vector2 nextPoint, float maxDistance)
   {
      var vector = nextPoint - point;

      var distance = vector.sqrMagnitude;

      if (distance > maxDistance * maxDistance)
         return false;
      
      if (distance < distanceSqr)
      {
         distanceSqr = distance;
         return true;
      }

      return false;
   }
}