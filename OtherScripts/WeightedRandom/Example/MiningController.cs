using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Rolls;
using UnityEngine.UI;

namespace WeightRandomExample {
    public class MiningController : MonoBehaviour {
        public Text TopText;
        public Text resultText;
        public Text oddsText;
        public ButtonList itemList;

        public Button copperMineButton;
        public Button silverMineButton;
        public Button goldMineButton;
        public Button gemMineButton;

        public Button sellAllButton;

        public int money = 0;

        public List<MineItem> items = new List<MineItem>() {
            new MineItem("copper nugget", 3, 1),
            new MineItem("silver nugget", 30, 1),
            new MineItem("gold nugget", 80, 1),
            new MineItem("platinum nugget", 120, 1),
            new MineItem("sapphire", 200, 12),
            new MineItem("emerald", 500, 8),
            new MineItem("ruby", 900, 5),
            new MineItem("diamond", 2000, 1)
        };

        public Dictionary<MineItem, int> Inventory { get; private set; }

        private void Awake() {
            Inventory = new Dictionary<MineItem, int>();

            copperMineButton.Set(()=> { Mine( (MineItem item, int weight) => {
                if(item.name == "copper nugget") {
                    return 200;
                }
                return weight;
                } );
            });
            silverMineButton.Set(() => { Mine((MineItem item, int weight) => {
                if (item.name == "silver nugget") {
                    return 200;
                }
                return weight;
                });
            });
            goldMineButton.Set(() => { Mine((MineItem item, int weight) => {
                if (item.name == "gold nugget") {
                    return 180;
                } else if (item.name == "platinum nugget") {
                    return 60;
                }
                return weight;
            });
            });
            gemMineButton.Set(() => { Mine(null); });

            sellAllButton.Set(SellAll);


            resultText.text = "Click the bottom buttons to mine materials.";
            oddsText.text = "";

            UpdateUI();
        }

        public void SellAll() {
            foreach (KeyValuePair<MineItem, int> stack in Inventory) {
                money += stack.Key.value * stack.Value;
            }

            Inventory.Clear();
            UpdateUI();
        }

        public void Mine(System.Func<MineItem, int, int> func) {

            oddsText.text = items.ListChances(func);
            MineItem item = items.Roll(func);

            //check
            if(item == null) {
                Debug.LogError("No items found");

                return;
            }

            //add the item
            if (Inventory.ContainsKey(item)) {
                Inventory[item] ++;
            } else {
                Inventory[item] = 1;
            }

            UpdateUI();

            resultText.text = "You found a " + item.name + " which has a value of " + item.value+"." ;

        }

        void UpdateUI() {
            TopText.text = "$" + money;

            itemList.Populate(Inventory.Keys.ToArray(), (Button button, MineItem item) => {
                int amount = Inventory[item];

                button.Set(item.name + " " + amount, () => {
                    Inventory[item]--;
                    if(Inventory[item] <= 0) {
                        Inventory.Remove(item);
                    }

                    money += item.value;

                    UpdateUI();
                });
            });

            sellAllButton.interactable = Inventory.Count > 0;
        }

    }

    [System.Serializable]
    public class MineItem : IWeight {
        public MineItem(string name, int value, int weight) {
            this.name = name;
            this.value = value;
            this.weight = weight;
        }

        public string name;
        public int value;
        public int weight;

        public int Weight { get { return weight; } }

        public override string ToString() {
            return name;
        }
    }

}

