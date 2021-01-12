using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Inventory Controller.
/// Attach this to the player object (Todd) so that the controls will work for inventory things
/// such as opening the inventory.
/// 
/// Eventually this should be merged into a monolithic Player Controller script?
/// </summary>
namespace InventorySystem
{
    [RequireComponent(typeof(Inventory))]
    public class InventoryController : MonoBehaviour
    {
        private Inventory _playerInventory;
        private InputMaster _controls;
        void Awake() {
            _controls = new InputMaster();
            _playerInventory = GetComponent<Inventory>() ?? this.gameObject.AddComponent<Inventory>();
            _controls.Player.Inventory.performed += ctx => InventoryUI.OpenInventory(_playerInventory);
        }
        private void OnEnable() { _controls.Enable(); }
        private void OnDisable() { _controls.Disable(); }

    }
}