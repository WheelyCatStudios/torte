using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InventorySystem;

public class TestInventory : MonoBehaviour
{
    Inventory _targetInventory;
    void Awake()
    {
        _targetInventory = gameObject.GetComponent<Inventory>();
        if (_targetInventory == null) { _targetInventory = gameObject.AddComponent<Inventory>(); }
    }
    void Start()
    {
        _targetInventory.InventoryItems = new List<InventoryItem>
        {
            new InventoryItem()
        };
    }
}
