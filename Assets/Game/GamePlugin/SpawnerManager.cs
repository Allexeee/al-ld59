using AssetsPlugin;
using PoolPlugin;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
   public GameObject SpawnUiBtnInteract(Vector3 position)
   {
      var asset = G.db.GetAsset(AssetId.UiBtnInteract);
      var go    = SpawnInactive(asset.As<AssetPrefab>());

      go.transform.position = position;
      go.SetActive(true);
      return go;
   }

   GameObject SpawnInactive(AssetPrefab asset)
   {
      if (asset.pooled)
      {
         return G.poolGameObject.GetObjectInactive(asset.prefab);
      }

      asset.prefab.SetActive(false);
      var go = Instantiate(asset.prefab);
      asset.prefab.SetActive(true);
      return go;
   }

   public void Despawn(GameObject go)
   {
      if (go.TryGetComponent<IPoolable>(out var comp))
         comp.pool.ReturnObject(go);
      else
         Destroy(go);
   }

   public void SpawnRadar(Vector3 position)
   {
      var asset = G.db.GetAsset(AssetId.Radar);
      var go    = SpawnInactive(asset.As<AssetPrefab>());
      go.transform.position = position;
      go.SetActive(true);
   }

   public void SpawnEnemy(Vector2 position)
   {
      var asset = G.db.GetAsset(AssetId.EnemyA);
      var go    = SpawnInactive(asset.As<AssetPrefab>());
      go.transform.position = position;
      go.SetActive(true);
   }

   public void SpawnProjectile(Vector2 start, Vector2 direction, int damage)
   {
      var offset   = 0.5f;
      var position = start + direction * offset;

      var asset = G.db.GetAsset(AssetId.Projectile);
      var go    = SpawnInactive(asset.As<AssetPrefab>());
      go.transform.position = position;
      go.GetComponent<ProjectileScript>().OnSpawn(direction, damage);
      go.SetActive(true);
   }

   public GameObject SpawnUniversal(Vector2 position, AssetId assetId)
   {
      var asset = G.db.GetAsset(assetId);
      var go    = SpawnInactive(asset.As<AssetPrefab>());
      go.transform.position = position;
      go.SetActive(true);
      return go;
   }
}