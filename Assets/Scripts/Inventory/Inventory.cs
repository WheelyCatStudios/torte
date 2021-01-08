using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// The Inventory System
/// </summary>
namespace InventorySystem {
    public class Inventory : MonoBehaviour {
        private List<InventoryItem> _inventory = new List<InventoryItem>();

        public void AddItem(InventoryItem item)
        {
            _inventory.Add(item);
        }

         private bool RemoveItem(InventoryItem item)
        {
            if (item.Removable) { _inventory.Remove(item); }
            return item.Removable;
        }
        private bool RemoveItem(int i)
        {
            if (_inventory[i].Removable) { 
                _inventory.Remove(_inventory[i]);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DropItem(InventoryItem item)
        {
            if (RemoveItem(item))
            {
                //create loose item on ground by location
                return true;
            }
            else
            {
                //"could not remove item. It's probably important"
                return false;
            }
        }
        public bool DropItem(int i)
        {
            if (RemoveItem(_inventory[i]))
            {
                //create loose item on ground
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DestroyItem(InventoryItem item)
        {
            if (RemoveItem(item))
            {
                return true;
            }
            else
            {
                //cant destroy item because you are a very weak little baby boy
                return false;
            }
        }
        public bool DestroyItem(int i)
        {
            if (RemoveItem(_inventory[i]))
            {
                return true;
            }
            else
            {
                //cant destroy, baby boy
                return false;
            }
        }

        public bool UseItem(InventoryItem item)
        {
            if (item.Usable)
            {
                //execute delegate
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool UseItem(int i)
        {
            if (_inventory[i].Usable)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}