using UnityEngine;

namespace Interactable {
	/// <summary>
	/// An example of an object which can be interacted with. <br/>
	/// Shows a dialog when interacted with.
	/// </summary>
	public class OBJ : Interactable
	{
		public override void onInteract()
		{
			Debug.Log("Object was interacted with.");
			TempDialogue.ShowMessage("Object was interacted with");
		}
	}
}