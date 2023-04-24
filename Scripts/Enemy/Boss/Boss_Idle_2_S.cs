using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Idle_2_S : StateMachineBehaviour
{
    BossFighter fighter;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        fighter = animator.GetComponent<BossFighter>();
        fighter.getTimer = true;

        fighter.OnEnter();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (fighter.finish)
        {
            animator.SetBool("secondRoar", true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        fighter.OnExit();
    }

    
}
