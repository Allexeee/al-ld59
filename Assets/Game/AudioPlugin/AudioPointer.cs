using System;

namespace AudioPlugin
{
   public struct AudioPointer : IEquatable<AudioPointer>
   {
      public ulong id;

      static ulong ids;

      public static AudioPointer GetNext()
      {
         var nextId = unchecked(++ids);
         if (nextId == 0)
            nextId = 1;

         return new AudioPointer() {
            id = nextId
         };
      }

      public bool Equals(AudioPointer other)
      {
         return id == other.id;
      }

      public override bool Equals(object obj)
      {
         return obj is AudioPointer other && Equals(other);
      }

      public override int GetHashCode()
      {
         return id.GetHashCode();
      }
   }
}