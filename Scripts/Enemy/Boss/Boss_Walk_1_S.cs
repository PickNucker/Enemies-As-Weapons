using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss_Walk_1_S : StateMachineBehaviour
{
    BossMovement movement;
    BossHealth health;
    NavMeshAgent agent;
    BossFighter fighter;

    float timer = Mathf.Infinity;
    float timerBtwAttacks = 1f;

    bool follow;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        movement = animator.GetComponent<BossMovement>();
        health = animator.GetComponent<BossHealth>();
        fighter = animator.GetComponent<BossFighter>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer += Time.deltaTime;
        float randomValue = Random.Range(0, 100f);

        if (movement.GetAggro())
            follow = true;

        if (follow)
        {
            movement.MoveTo();
        }

        if (movement.GetAttackRange())
        {
            movement.StopMove();


            if (timer > timerBtwAttacks)
            {
                if (randomValue <= 33f)
                {
                    fighter.DoDamageToPlayer();
                    animator.SetTrigger("attack1");
                    movement.LookAtPlayer();
                    timer = 0;
                }
                else if (randomValue > 33 && randomValue <= 66)
                {
                    fighter.DoDamageToPlayer();
                    animator.SetTrigger("attack2");
                    movement.LookAtPlayer();
                    timer = 0;
                }
                else
                {
                    fighter.DoDamageToPlayer();
                    animator.SetTrigger("attack3");
                    movement.LookAtPlayer();
                    timer = 0;
                }
            }
        }

        if(health.health <= 250f)
        {
            health.isInRage = true;
            animator.SetBool("IsInRage", true);
        }

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("attack1");
        animator.ResetTrigger("attack2");
        animator.ResetTrigger("attack3");
    }


}
