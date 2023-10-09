using System.Collections.Generic;

namespace Talent_Management.Utilities
{
    public static class ListUtilities
    {
        public static IList<T> Swap<T>(this IList<T> list, int firstIndex, int secondIndex)
        {
            var temporary = list[firstIndex];

            list[firstIndex] = list[secondIndex];
            list[secondIndex] = temporary;

            return list;
        }
    }
}
