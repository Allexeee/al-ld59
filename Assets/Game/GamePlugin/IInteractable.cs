using EventsPlugin;
using UnityEngine;

public interface IInteractable : IEventAbstract
{
   Vector2 GetInteractCenterPosition();
   void           Interact();
}