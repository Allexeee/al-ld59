using AudioPlugin;
using AssetsPlugin;
using EventsPlugin;
using PoolPlugin;

public static class G
{
   public static GameMainService       main;
   public static GameManager           game;
   public static SpawnerManager        spawner;
   public static AudioService          audio;
   public static Db                    db;
   public static PoolGameObjectService poolGameObject;
   public static PoolAnyObject         poolAnyObject;
   public static EventsService         events;
   public static SchedulerManager      scheduler;
}