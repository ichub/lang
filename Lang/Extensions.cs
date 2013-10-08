using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lang
{
    public static class Extensions
    {
        public static IEnumerable<U> Map<T, U>(this IEnumerable<T> collection, Func<T, U> function)
        {
            List<U> result = new List<U>(collection.Count());

            for (int i = 0; i < collection.Count(); i++)
            {
                result.Add(function.Invoke(collection.ElementAt(i)));
            }

            return result;
        }
    }
}
