public class GameRadarSendSignal
{
   public float progress;

   public void Progress(float dt, float speed)
   {
      progress += speed * dt;
   }
}