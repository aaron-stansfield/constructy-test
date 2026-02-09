using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.ourAssets.scrips
{
    public static class helpy
    {
        public static IEnumerable<T> PickRandom<T>(this IEnumerable<T> list, int count)
        {
            return list.OrderBy(x => Guid.NewGuid()).Take(count);
        }

        public static T PickRandom<T>(this IEnumerable<T> list)
        {
            return list.PickRandom(1).Single();
        }
    }
}
