using UnityEngine;

public class RollingAnimationStates : StateMachineBehaviour
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetInteger("Roll", 0);
    }
}
