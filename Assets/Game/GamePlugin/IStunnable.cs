using EventsPlugin;
using UnityEngine;

public interface IStunnable : IEventAbstract
{
   Vector2 GetStunCenterPosition();
   void    Stun();
}