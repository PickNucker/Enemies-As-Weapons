using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossMovement : MonoBehaviour
{
    public Transform movePoint;
    [SerializeField] float aggroRange = 10f;
    public float attackRange = 3f;

    Combat combat;
    Animator anim;
    NavMeshAgent agent;
    PlayerHealth target;

    private void Awake()
    {
        combat = FindObjectOfType<Combat>();
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        target = FindObjectOfType<PlayerHealth>();
    }

    private void Update()
    {
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
    }

    public void MoveTo()
    {
        anim.SetBool("isRunning", true);
        agent.isStopped = false;
        agent.SetDestination(target.transform.position);
    }

    public void StopMove()
    {
        anim.SetBool("isRunning", false);
        agent.isStopped = true;
    }

    public void LookAtPlayer()
    {
        transform.LookAt(target.transform.position);
    }

    public bool GetAggro()
    {
        return Vector3.Distance(transform.position, target.transform.position) <= aggroRange;
    }

    public bool GetAttackRange()
    {
        return Vector3.Distance(transform.position, target.transform.position) <= attackRange;
    }

    public bool StopDistance()
    {
        return Vector3.Distance(transform.position, combat.transform.position) <= .5;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, aggroRange);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
