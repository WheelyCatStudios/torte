using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class BubbleController : MonoBehaviour
{
	/// <summary>
	/// The current bubble index to display.
	/// <br/>
	/// Requires an enabled script and animator to update.
	/// </summary>
	public int State = 1;

	/// <summary>
	/// The distance at which the player must be to display the bubble.
	/// </summary>
	public float distanceThreshold = 5;

	/// <summary>
	/// Fetched from the scene via the "player" tag, this object should be the player GO.
	/// <br/>
	/// This is the object whose distance is measured against the bubble's position.
	/// </summary>
	private GameObject Player;

	/// <summary>
	/// The players current distance from this bubble.
	/// </summary>
	private float distance;

	/// <summary>
	/// The Animator statemachine that controlls the bubbles.
	/// </summary>
	private Animator statemachine;

	/// <summary>
	/// The last known state index. Used to detect and trigger state changes when <a cref="State"/> is changed
	/// </summary>
	private int lastState;

	/// <summary>
	/// The last known state of PlayerInProximity. Used to detect and trigger state changes when the player moves into and out of the <a cref="distanceThreshold"/> detection range.
	/// </summary>
	private bool lastProximity;

	/// <summary>
	/// Configures local varables on creation.
	/// </summary>
	void Start()
    {
		Player = GameObject.FindGameObjectWithTag("Player");		
		statemachine = GetComponent<Animator>();
    }

    /// <summary>
	/// Monitors distance on every frame. Triggeres state changes, and manages the state machine.
	/// </summary>
    void Update()
    {
		if (lastState != State) SetState();

		lastProximity = statemachine.GetBool("PlayerInProximity");
        distance = Vector2.Distance(transform.position, Player.transform.position);
		setPlayerInProximity(distance < distanceThreshold);
    }

	/// <summary>
	/// Sets the 'PlayerInProximity' parameter, according to <paramref name="val"/>. If true, the state machine is permitted to open a bubble, false it should close the bubble.
	/// </summary>
	/// <param name="val">The value to set Player Close to.</param>
	private void setPlayerInProximity(bool val) {
		if (val == lastProximity) return;
		statemachine.SetBool("PlayerInProximity", val);
		lastProximity = val;
	}

	/// <summary>
	/// Sets the bubble state to according to <a cref="State"/>. <br/>
	/// Changes which bubble type is to be shown, according to thier index.
	/// </summary>
	public void SetState() => SetState(State);

	/// <summary>
	/// Changes which bubble type is to be shown, according to thier index.
	/// </summary>
	/// <param name="_state">The bubble index to show. Check the state machine for indexes.</param>
	public void SetState(int _state) {
		State     = _state;
		lastState =  State;
		statemachine.SetInteger("State", State);
	}

	/// <summary>
	/// Closes the bubble when the script is disabled.
	/// </summary>
	void OnDisable() => Close();

	/// <summary>
	/// Closes the bubble, by forcing PlayerInProximity parameter to false. <br/>
	/// If this script is still enabled, this will be overwritten on the next frame.
	/// </summary>
	public void Close() => statemachine.SetBool("PlayerInProximity", false);

}
