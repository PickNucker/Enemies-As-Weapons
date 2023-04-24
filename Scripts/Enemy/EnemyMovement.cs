using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] GameObject particlePrefab = null;
    [SerializeField] Transform particleSpawn = null;

    [SerializeField] float aggroRadius = 2f;
    [SerializeField] float enemyDamage = 10f;

    EnemyHealth health;
    Animator anim;
    NavMeshAgent agent;
    EnemyBehaviourOnRaycastHit ownEnemy;

    float timer = Mathf.Infinity;

    bool isRunning;
    bool isTargeting;

    void Awake()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        ownEnemy = GetComponent<EnemyBehaviourOnRaycastHit>();
        health = GetComponent<EnemyHealth>();
    }

    private void Start()
    {

    }

    void Update()
    {
        if (health.GetDead()) return;

        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

        timer += Time.deltaTime;
        
        UpdateAnimation();

        if (AggroBehaviour())
        {
            isTargeting = true;
            
        }
        else
        {
            isTargeting = false;
        }

        if (!isTargeting) return;


        if (!GetIsInRange())
        {
            MoveTo(PlayerMovement.instance.transform.position);
        }
        else
        {
            StopMove();

            if (ownEnemy.enemyWeapon.GetCanShoot())
            {
                AttackBehaviour2();
            }
            else
            {
                AttackBehaviour();
            }
        }       
    }

    bool AggroBehaviour()
    {
        return Vector3.Distance(transform.position, PlayerMovement.instance.transform.position) < aggroRadius;
    }

    void AttackBehaviour()
    {
        
        if (PlayerHealth.instance.GetDead()) return;
        if(timer > ownEnemy.enemyWeapon.GetTimer())
        {
            transform.LookAt(PlayerHealth.instance.transform.position);
            timer = 0;
            TriggerAttack();
        }

    }
    void AttackBehaviour2()
    {
        
        if (PlayerHealth.instance.GetDead()) return;
        if (timer > ownEnemy.enemyWeapon.GetTimer())
        {
            transform.LookAt(PlayerHealth.instance.transform.position);
            timer = 0;
            TriggerAttack();
            StartCoroutine(ShootProjectile());
        }

    }

    IEnumerator ShootProjectile()
    {
        yield return new WaitForSeconds(0.5f);

        var particle = Instantiate(particlePrefab, particleSpawn) as GameObject;
        particle.transform.parent = null;
    }

    void MoveTo(Vector3 position)
    {
        agent.SetDestination(position);
        agent.isStopped = false;
    }

    void StopMove()
    {
        agent.stoppingDistance = ownEnemy.enemyWeapon.GetWeaponRange();
        agent.isStopped = true;
    }

    void TriggerAttack()
    {
        anim.SetTrigger("enemyAttack");
    }

    public void HitPlayer()
    {
        Debug.Log("Enemy Damage: " + enemyDamage);
        PlayerHealth.instance.TakeDamage(enemyDamage);
    }

    bool GetIsInRange()
    {
        return Vector3.Distance(transform.position, PlayerMovement.instance.transform.position) < ownEnemy.enemyWeapon.GetWeaponRange();
    }

    void UpdateAnimation()
    {
        if(agent.velocity.magnitude > 0)
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }

        anim.SetBool("isRunning", isRunning);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, aggroRadius);
    }
}
