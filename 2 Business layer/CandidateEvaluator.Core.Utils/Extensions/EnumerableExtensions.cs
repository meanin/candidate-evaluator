using System;
using System.Collections.Generic;
using System.Linq;

namespace CandidateEvaluator.Core.Utils.Extensions
{
    public static class EnumerableExtensions
    {
        private static readonly Random Random = new Random();

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> root)
        {
            var origin = root.ToList();
            var rootCount = origin.Count;
            var result = new List<T>();

            while (result.Count < rootCount)
            {
                var element = origin.ElementAt(Random.Next(origin.Count));
                result.Add(element);
                origin.Remove(element);
            }

            return result;
        }
    }
}
