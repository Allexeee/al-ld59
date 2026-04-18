using System;
using AudioPlugin;
using AssetsPlugin;
using EventsPlugin;
using PoolPlugin;
using UnityEngine;
using UntilWeDie.Game.Audio;

// Инициализирует все системы, сервисы и прочее
[DefaultExecutionOrder(BootPriority.GameMain)]
public class GameMainService : MonoBehaviour
{
   [SerializeField] SpawnerManager   spawnerManager;
   [SerializeField] GameManager      gameManager;
   [SerializeField] AudioManager     audioManager;
   [SerializeField] SchedulerManager schedulerManager;

   void Awake()
   {
      // Scene Loading
      G.main           = this;
      G.factory        = spawnerManager;
      G.game           = gameManager;
      G.audio          = new AudioService(audioManager, new AudioMixers("Audio/Mixers"));
      G.db             = new Db(new AssetsService(64));
      G.poolGameObject = new PoolGameObjectService();
      G.poolAnyObject  = new PoolAnyObject();
      G.events         = new EventsService();
      G.scheduler      = schedulerManager;

      var factoryGameObject = new AudioGameObjectFactory(G.poolGameObject);
      var factoryAudio      = new AudioFactory(factoryGameObject, "Content/Prefabs/[Sound] World Default");
      audioManager.Inject(factoryAudio);

      schedulerManager.Inject(onRelease: G.poolAnyObject.Add);

      G.db.Init();
      gameManager.SceneLoading();
   }

   void Start()
   {
      // Scene Loaded
      gameManager.SceneLoaded();
   }
}