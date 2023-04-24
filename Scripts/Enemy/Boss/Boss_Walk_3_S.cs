using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss_Walk_3_S : StateMachineBehaviour
{
    BossMovement movement;
    BossHealth health;
    BossFighter fighter;
    NavMeshAgent agent;

    float timer = Mathf.Infinity;
    float timerBtwAttacks = 1f;

    bool follow;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        movement = animator.GetComponent<BossMovement>();
        health = animator.GetComponent<BossHealth>();
        fighter = animator.GetComponent<BossFighter>();
        agent = animator.GetComponent<NavMeshAgent>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer += Time.deltaTime;

        agent.stoppingDistance = movement.attackRange;
        agent.speed = 7f;
        fighter.damage = 20;
       

        if (movement.GetAttackRange())
        {
            float randomValue = Random.Range(0, 100f);

            movement.StopMove();

            if (timer > timerBtwAttacks)
            {
                if (randomValue <= 33f)
                {
                    fighter.DoDamageToPlayer2();
                    animator.SetTrigger("attack1");
                    movement.LookAtPlayer();
                    timer = 0;
                }
                else if (randomValue > 33 && randomValue <= 66)
                {
                    fighter.DoDamageToPlayer2();
                    animator.SetTrigger("attack2");
                    movement.LookAtPlayer();
                    timer = 0;
                }
                else
                {
                    fighter.DoDamageToPlayer2();
                    animator.SetTrigger("attack3");
                    movement.LookAtPlayer();
                    timer = 0;
                }
            }
        }
        else
        {
            movement.MoveTo();
        }

        if (health.health <= 0)
        {
            animator.SetTrigger("isDead");
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
