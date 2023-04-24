using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Roar_2_S : StateMachineBehaviour
{
    BossHealth health;
    BossFighter fighter;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        health = animator.GetComponent<BossHealth>();
        fighter = animator.GetComponent<BossFighter>();
        fighter.SpawnParticle();
        fighter.PlaySound();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        fighter.ChangeSkin();


    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        health.isInRage = false;
        fighter.SpawnParticle();
    }

    
}
