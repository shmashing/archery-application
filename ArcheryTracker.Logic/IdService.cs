using System;

namespace ArcheryTracker.Logic
{
    public static class IdService
    {
        public static string GetId(Type type)
        {
            return $"{type.Name.Substring(0, 3).ToLowerInvariant()}_{Guid.NewGuid():N}";
        }
    }
}