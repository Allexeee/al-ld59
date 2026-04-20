using System;
using AssetsPlugin;
using PrimeTween;
using UnityEngine;

public class HeroPresentation : MonoBehaviour
{
   [SerializeField]     Transform      body;
   [SerializeField]     Transform      view;
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

   public void Punch()
   {
      Tween.PunchScale(view, Vector3.one * 3f, 0.2f);
   }
}