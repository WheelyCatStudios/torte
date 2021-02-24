using UnityEngine;

public class BubbleDemo : StateMachineBehaviour
{
	public int MaxStates = 7;

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
		int v = animator.GetInteger("State");
		animator.SetBool("PlayerClose", !animator.GetBool("PlayerClose"));
		if (animator.GetBool("PlayerClose")) return;
		animator.SetInteger("State", (v == MaxStates) ? 0 : v + 1);
    }
}
