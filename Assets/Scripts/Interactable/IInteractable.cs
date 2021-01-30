using UnityEngine;

namespace Interactable
{
	/// <summary>
	/// Abstract representation of an object which the player may interacted with
	/// </summary>
    public abstract class IInteractable : MonoBehaviour
    {
		public void Interact() => onInteract();
		public abstract void onInteract();
    }
}