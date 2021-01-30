using UnityEngine;

namespace Interactable {
	/// <summary>
	/// An interactable object.
	/// This abstract cannot be used as a component alone, it must be extended by
	/// a script which adds an interaction behaviour.
	/// </summary>
	public abstract class Interactable : IInteractable
	{
		#region fields
		/// <summary>
		/// True if in contact with a PC.
		/// </summary>
		public bool InContact;

		/// <summary>
		/// If true, enables the player to interact multiple times.
		/// </summary>
		[Tooltip("If true, the player may interact with this multiple times.")]
		public bool IsRepeatable = false;

		[Header("Trigger collider variables")] 
		[Range(0 , 5f)]
		public float ColliderBoundsX;
		
		[Range(0, 5f)]
		public float ColliderBoundsY;
		
		[Range(0, 5f)]
		public float OffsetColliderBoundsX;
		
		[Range(0, 5f)]
		public float OffsetColliderBoundsY;
		
		private BoxCollider2D ObjCollider;

		[Header("physycal collider")]
		public bool HavePhysicalCollider = true;
		private BoxCollider2D colobj;
		
		[Header("Feedback")]
		public string feedbacktext = "press f to interact";

		private InputMaster input;
		#endregion

		#region config
		/// <summary>
		/// Prepares the 2d collider for this object upon creation
		/// </summary>
		public void ColliderSetup()
		{
			ObjCollider = gameObject.AddComponent<BoxCollider2D>();
			ObjCollider.size = new Vector2 ( ColliderBoundsX + 1 , ColliderBoundsY + 1 );
			ObjCollider.offset = new Vector2 ( OffsetColliderBoundsX , OffsetColliderBoundsY );
			ObjCollider.isTrigger = true;
			ObjCollider.enabled = true;

			if(HavePhysicalCollider == true)
				colobj = gameObject.AddComponent<BoxCollider2D>();
		}
		#endregion

		#region monobehaviour
		void Start() => ColliderSetup();
		
		void onEnable() => input.Enable();

		void Awake() {
			input = new InputMaster();			// Create a new input actions reader
			input.Enable();						// Enable the darn thing so that it actually works...
			input.Player.SecondaryInteraction.performed += ctx => requestAction(); // Have the secondary action bound to interactions.
		}

		#endregion

		#region contact trigger
		private void OnTriggerEnter2D(Collider2D collision)
		{
			if (collision.gameObject.CompareTag("Player"))	// If we're colliding with a player,
				InContact = true;							// Raise the 'in contact' flag.
		}

		private void OnTriggerExit2D(Collider2D collision)
		{
			if (collision.gameObject.CompareTag("Player"))	// If we lost collision with a player,
				InContact = false;							// Lower the 'in contact' flag/
		}
		#endregion

		#region interaction
		/// <summary>
		/// Indicate that the user has requested to interact with something.
		/// </summary>
		void requestAction()
		{
			if(InContact == true)			// If this object is in contact with a player,
				OnContactAndAction();		// Trigger contact interaction
		}

		/// <summary>
		/// Triggers an interaction for this interactable object
		/// </summary>
		public void OnContactAndAction()
		{
			Interact();				 // Trigger interaction
			Consume();				 // Consume interaction request
		}   

		/// <summary>
		/// After interaction is complete, 
		/// will consume the interaction request, and prevent further requests.
		/// <br/><br/> 
		/// Only takes effect if <a cref="IsRepeatable"/> is false.
		/// </summary>
		private void Consume() {
			if (IsRepeatable) return;
			ObjCollider.enabled = false; // If can only be activated once, disable collider.
			InContact = false;
		}
		#endregion
	}
}