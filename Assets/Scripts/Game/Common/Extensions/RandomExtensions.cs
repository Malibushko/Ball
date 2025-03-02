using System;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

namespace Game.Common.Extensions
{
    public static class RandomExtensions
    {
        public static T[] SelectRandom<T>(this IEnumerable<T> source, int count)
        {
            if (source == null) 
                return Array.Empty<T>();
            
            if (count <= 0) 
                return Array.Empty<T>();

            T[] array = source.ToArray();
            if (array.Length == 0) 
                return Array.Empty<T>();
            
            if (count > array.Length)
                count = array.Length;

            for (int i = 0; i < count; i++)
            {
                int randomIndex = Random.Range(i, array.Length);
                (array[i], array[randomIndex]) = (array[randomIndex], array[i]);
            }

            T[] result = new T[count];
            Array.Copy(array, 0, result, 0, count);
            return result;
        }
    }
}