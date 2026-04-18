using System;
using AssetsPlugin;
using UnityEngine;

public class HeroPresentation : MonoBehaviour
{
   [SerializeField]     Transform      body;
   [SerializeField] new Rigidbody2D    rigidbody;
   public               AssetContainer asset;

   void Awake()
   {
      asset = G.db.GetAsset(AssetId.Hero);
   }

   public void AddPosition(Vector2 position)
   {
      rigidbody.MovePosition(rigidbody.position + position);
      // body.transform.position += position;
   }

   public void RotateTo(Quaternion euler)
   {
      rigidbody.MoveRotation(euler);
      // body.rotation = euler;
   }
}