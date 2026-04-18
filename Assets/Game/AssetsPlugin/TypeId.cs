namespace AssetsPlugin
{
   static class TypeId<T>
   {
      public static string Id { get; } = typeof(T).Name;
   }
}