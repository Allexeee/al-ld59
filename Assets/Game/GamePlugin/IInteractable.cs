using EventsPlugin;
using UnityEngine;

public interface IInteractable : IEventAbstract
{
   public Vector2 GetInteractCenterPosition();
   void           Interact();
}