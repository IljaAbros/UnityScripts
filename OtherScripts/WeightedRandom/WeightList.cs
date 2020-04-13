using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Rolls {
    public class WeightList<T> where T : IWeight {

        public WeightList(IEnumerable<T> list, System.Func<T, int, int> Modifier = null) {
            Items = new Dictionary<T, int>();
            Weights = new Dictionary<T, int>();

            Total = 0;

            foreach (T item in list) {
                //does not support multiple same item entries
                if (Items.ContainsKey(item)) { continue; }

                //get the weight
                int weight = item.Weight;
                //apply filter/modifier function
                if (Modifier != null) {
                    weight = Modifier(item, weight);
                }

                //we dont want add it to the list if the weight is 0
                if (weight == 0) { continue; }

                //add the item to list
                Items[item] = Total;
                Weights[item] = weight;

                Total += weight;
            }
        }

        public Dictionary<T, int> Items { get; private set; }
        public Dictionary<T, int> Weights { get; private set; }
        public int Total { get; private set; }

        public int Count {
            get { return Items.Count; }
        }

        public T First() {
            return Items.Keys.First();
        }

        public float ChanceOf(T item) {
            if (item == null || Weights.ContainsKey(item) == false) { return 0; }
            if (Count == 1) { return 1; }

            int weight = Weights[item];

            if(weight == 0) { return 0; }

            return (float)weight / Total;
        }

        public T Roll() {
            if (Count == 0) { return default(T); }

            T item = First();

            if (Count == 1) { return item; }

            int roll = Random.Range(0, Total+1);
            //we can skip if the roll is exactly 0
            if (roll > 0) {
                //iterate through each value
                foreach (KeyValuePair<T, int> weight in Items) {
                    if (roll <= weight.Value) { break; }
                    item = weight.Key;
                }
            }

            return item;
        }

    }
}
