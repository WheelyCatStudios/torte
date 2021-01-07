using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// The Inventory System
/// </summary>
namespace InventorySystem {
    public class Inventory : MonoBehaviour {
        private List<InventoryItem> _inventory = new List<InventoryItem>();

        [SerializeField]
        private GameObject _inventoryScreen;

        public void OpenInventory(){
            //open player inventory
            GameObject _openMenu = Instantiate(_inventoryScreen);
        }
        public void LootInventory(Inventory loot)
        {
            //open inventory to take item(s)
        }

    }
}