using UnityEngine;

public class PlayAudioCommand : ICommand
{
   public Vector2 position;
   public AssetId soundId;

   public static PlayAudioCommand Get(Vector2 position, AssetId soundId)
   {
      var obj = G.poolAnyObject.Get<PlayAudioCommand>();
      obj.position = position;
      obj.soundId  = soundId;
      return obj;
   }

   public void Execute()
   {
      var preset = G.db.GetAudio(soundId);
      G.audio.Play(position, preset);
   }
}