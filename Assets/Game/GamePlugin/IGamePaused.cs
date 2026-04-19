using EventsPlugin;

public interface IGamePaused : IEventAbstract
{
   void OnPaused(bool paused);
}