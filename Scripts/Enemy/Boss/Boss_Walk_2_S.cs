using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss_Walk_2_S : StateMachineBehaviour
{
    NavMeshAgent agent;
    BossMovement movement;
    Combat targetPos;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.GetComponent<NavMeshAgent>();
        movement = animator.GetComponent<BossMovement>();
        targetPos = FindObjectOfType<Combat>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bool pos = agent.transform.position == targetPos.transform.position;

        if (movement.StopDistance())
        {
            animator.SetTrigger("idle2");
            return;
        }

        if (!pos)
        {
            //agent.stoppingDistance = 0;
            agent.isStopped = false;
            agent.SetDestination(targetPos.transform.position);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }


}
