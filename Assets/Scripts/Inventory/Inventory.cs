using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// The Inventory System
/// </summary>
namespace InventorySystem {
    /// <summary>
    /// The Inventory class serves as a container for InventoryItem objects with methods to manage the contained objects.
    /// Attach an inventory to any GameObject that has an inventory i.e. Todd, chests, trash cans, NPCs, etc.
    /// </summary>
    public class Inventory : MonoBehaviour {
        /// <summary>
        /// This is a list of InventoryItem objects (AKA an inventory).
        /// </summary>
        [SerializeField]
        private List<InventoryItem> _inventory = new List<InventoryItem>();

        /// <summary>
        /// Add an item to the inventory.
        /// </summary>
        /// <param name="item">The item you want to add.</param>
        public void AddItem(InventoryItem item)
        {
            _inventory.Add(item);
        }

        /// <summary>
        /// Remove an item from the inventory.
        /// </summary>
        /// <param name="item">The item you want to remove.</param>
        /// <returns>Returns true if item was removed.</returns>
         private bool RemoveItem(InventoryItem item)
        {
            if (item.Removable) { _inventory.Remove(item); }
            return item.Removable;
        }
        /// <summary>
        /// Remove an item from inventory.
        /// </summary>
        /// <param name="i">index within the inventory of the item you want to remove from the inventory.</param>
        /// <returns>Returns true if item was removed.</returns>
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

        /// <summary>
        /// Drop an item from your inventory to the ground.
        /// </summary>
        /// <param name="item">The item you'd like to drop.</param>
        /// <returns>Returns true if dropped.</returns>
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
        /// <summary>
        /// Drop an item from the inventory to the ground.
        /// </summary>
        /// <param name="i">The index within the inventory of the item you'd like to drop</param>
        /// <returns>Returns true if dropped.</returns>
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

        /// <summary>
        /// Destroys an item within the inventory.
        /// This may seem similar to <a cref="RemoveItem(InventoryItem)"/>
        /// </summary>
        /// <param name="item">The item to destroy.</param>
        /// <returns>Returns true if item was destoryed.</returns>
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
        /// <summary>
        /// Destroys an item within the inventory.
        /// This may seem similar to <a cref="RemoveItem(int)"/>
        /// </summary>
        /// <param name="i">The index within the inventory of the item you'd like to destroy.</param>
        /// <returns>Returns true if the item was destroyed.</returns>
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

        /// <summary>
        /// Use an item.
        /// </summary>
        /// <param name="item">The item that will be used.</param>
        /// <returns>Returns true is item was used.</returns>
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
        /// <summary>
        /// Use an item.
        /// </summary>
        /// <param name="i">Index of the item within the inventory that you'd like to use.</param>
        /// <returns>Returns true if item was used.</returns>
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