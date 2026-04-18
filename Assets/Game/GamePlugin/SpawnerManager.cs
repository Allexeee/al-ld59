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
}