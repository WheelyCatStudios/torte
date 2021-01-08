using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Collections;

namespace InventorySystem
{
    public class InventoryController : MonoBehaviour
    {
        private InputMaster _controls;
        private void OnGotControls(UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<InputMaster> controls) { _controls = controls.Result; }
        void Awake() { Addressables.LoadAssetAsync<InputMaster>("Assets/Settings/InputMaster.inputactions").Completed += OnGotControls; } //Assets/Settings/InputMaster.inputactions

    }
}