using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KulpAssessment.Utilities.Extensions
{
    public static class CollectionExtensions
    {
        private static readonly Random _rnd = new Random();

        /// <summary>
        /// Returns a random element of an collection.  Returns default for type if collection is null or empty
        /// </summary>
        /// <typeparam name="T">Type of value in collection </typeparam>
        /// <param name="items">Collection of items</param>
        /// <returns>a random item from collection or default value for type if null or empty</returns>
        public static T GetRandom<T>(this List<T> items)
        {
            return (items.IsNullOrEmpty())
                ? default
                : items[_rnd.Next(items.Count)];
        }

        /// <summary>
        /// Indicates whether a collection is a null reference or contains no items
        /// </summary>
        /// <typeparam name="T">Type of value in collection</typeparam>
        /// <param name="items">The collection to test</param>
        /// <returns>true if collection is a null reference or contains no items</returns>
        public static bool IsNullOrEmpty<T>(this List<T> items)
        {
            return items == null || items.Count == 0;
        }
    }
}
