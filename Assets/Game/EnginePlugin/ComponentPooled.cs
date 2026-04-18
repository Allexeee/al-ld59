using PoolPlugin;
using UnityEngine;

public class ComponentPooled : MonoBehaviour, IPoolable
{
   public PoolGameObjectService.Pool pool { get; set; }
}