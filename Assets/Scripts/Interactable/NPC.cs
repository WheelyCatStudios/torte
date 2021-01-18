using UnityEngine;

namespace Interactable {
	/// <summary>
	/// An example of an npc which may be interacted with. <br/>
	/// Shows a message window on interaction.
	/// </summary>
	public class NPC : Interactable
	{
		public override void onInteract() 
		{
			Debug.Log("NPC was interacted with.");
			TempDialogue.ShowMessage("NPC was interacted with.");
		}
	}
}