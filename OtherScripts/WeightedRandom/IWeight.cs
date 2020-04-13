using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// this interface and static class adds in the ability to make random weighted rolls
/// </summary>
namespace Rolls {
    public interface IWeight {
        int Weight { get; }
    }

    public static class WeightFuncs {
        public static string ListChances<T>(this IEnumerable<T> list, System.Func<T, int, int> Modifier = null) where T : IWeight {
            if (list == null) { return "Error"; }
            WeightList<T> weights = new WeightList<T>(list, Modifier);

            string text = "List";
            foreach(KeyValuePair<T, int> weight in weights.Items) {
                T item = weight.Key;
                text += "\n" + item.ToString() + " " + (weights.ChanceOf(item) *100)+"%";
            }
            return text;
        }

        public static float CalculateChance<T>(this T item, IEnumerable<T> list, System.Func<T, int, int> Modifier = null) where T : IWeight {
            if (item == null || list == null) { return 0; }

            WeightList<T> weights = new WeightList<T>(list, Modifier);
            return weights.ChanceOf(item);
        }

        //roll item
        public static T Roll<T>(this IEnumerable<T> list, System.Func<T, int, int> Modifier = null) where T : IWeight {
            //error check
            if (list == null) { return default(T); }
            WeightList<T> weights = new WeightList<T>(list, Modifier);
            return weights.Roll();
        }
    }
}
