using System;
using System.Collections.Generic;
using UnityEngine;

namespace AssetsPlugin
{
   public abstract class AssetAbstract
   {
      public virtual bool Is<T>() where T : AssetAbstract
      {
         return this is T;
      }

      public virtual bool Is<T>(out T val) where T : AssetAbstract
      {
         if (this is T val1)
         {
            val = val1;
            return true;
         }

         val = default;
         return false;
      }

      public virtual T As<T>() where T : AssetAbstract
      {
         return this as T;
      }
   }

   public class AssetContainer : AssetAbstract
   {
      Dictionary<string, object> dictionary = new();
      AssetAbstract              first;

      public int Count => dictionary.Count;

      public AssetContainer Add<T>(T obj) where T : AssetAbstract
      {
         first ??= obj;

         var key = TypeId<T>.Id;

         dictionary.Add(key, obj);

         return this;
      }

      public override T As<T>()
      {
         var key = TypeId<T>.Id;
         if (dictionary.TryGetValue(key, out var value))
            return (T) value;

         throw new Exception($"Не найден объект по ключу {key}");
      }

      public override bool Is<T>()
      {
         var key = TypeId<T>.Id;
         if (dictionary.TryGetValue(key, out _))
            return true;

         return false;
      }

      public override bool Is<T>(out T val)
      {
         var key = TypeId<T>.Id;
         if (dictionary.TryGetValue(key, out var value))
         {
            val = (T) value;
            return true;
         }

         val = default;
         return false;
      }

      public AssetAbstract First() => first;
   }

   public class AssetPrefabs : AssetAbstract
   {
      Dictionary<string, AssetPrefab> dictionary = new();

      public AssetPrefab Main  { get; private set; }
      public int         Count => dictionary.Count;

      public AssetPrefabs Add(string key, AssetPrefab obj)
      {
         Main ??= obj;

         dictionary.Add(key, obj);

         return this;
      }

      public AssetPrefabs Add(AssetPrefab obj)
      {
         Main ??= obj;

         dictionary.Add("main", obj);

         return this;
      }
   }

   public class AssetPrefab : AssetAbstract
   {
      const string basePath = "Content/Prefabs/";

      public GameObject prefab;
      public bool       pooled;

      public AssetPrefab(GameObject prefab, bool pooled)
      {
         this.prefab = prefab;
         this.pooled     = pooled;
      }

      public AssetPrefab(string path, bool pooled)
      {
         prefab  = Resources.Load<GameObject>(basePath + path);
         this.pooled = pooled;
      }
   }

   public class AssetChildren : AssetAbstract
   {
      public List<AssetAbstract> list;
   }
}