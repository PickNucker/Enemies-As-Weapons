using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    public static Fighter instance;

    [SerializeField] Transform handTransfrom = null;
    [SerializeField] Weapon defaultWeapon = null;

    [SerializeField] LayerMask whatIsClickable = default;

    [SerializeField] GameObject particlePrefab = null;
    public Transform particleSpawn = null;

    Transform target;
    Transform targets;
    Weapon currentweapon = null;
    Animator anim;

    float timer = Mathf.Infinity;
    float timerToMoveAgain = Mathf.Infinity;

    bool isAttacking;

    RaycastHit hit;

    private void Awake()
    {
        instance = this;
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        EquipWeapon(defaultWeapon);
    }

    private void Update()
    {
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

        if (PlayerHealth.instance.GetDead()) return;

        timer += Time.deltaTime;
        timerToMoveAgain += Time.deltaTime;
        Attack();
        LookAtMouse();
    }

    public void EquipWeapon(Weapon weapon)
    {
        currentweapon = weapon;

        anim = GetComponent<Animator>();
        weapon.Spawn(handTransfrom, anim);
    }

    void Attack()
    {
        if (timer > currentweapon.GetTimer() && timerToMoveAgain > currentweapon.GetCanMoveTimer())
        {
            if (Input.GetMouseButtonDown(0) && !currentweapon.GetCanShoot())
            {
                timer = 0;
                timerToMoveAgain = 0;

                PlayerMovement.instance.GetMovement(false);
                transform.LookAt(hit.point);

                anim.SetTrigger("attack");
                StartCoroutine(MoveAgain());

            }

            if (Input.GetMouseButtonDown(0) && currentweapon.GetCanShoot())
            {
                timer = 0;
                timerToMoveAgain = 0;
            
                PlayerMovement.instance.GetMovement(false);
                transform.LookAt(hit.point);
            
                anim.SetTrigger("attack");
                StartCoroutine(ShootProjectile());
            
                StartCoroutine(MoveAgain());
            }
            
        }

    }

    public void Hit()
    {
        var damage = currentweapon.GetDamage();

        EnemyHealth health = target.GetComponent<EnemyHealth>();

        health.TakeDamage(damage);
    }

    public void HitBoss()
    {
        var damage = currentweapon.GetDamage();

        BossHealth health = FindObjectOfType<BossHealth>();

        health.TakeDamage(damage);
    }

    public void GetTarget(EnemyHealth health)
    {
        target = health.transform;
    }

    public void GetTargetBoss(BossHealth health)
    {
        targets = health.transform;
    }

    void LookAtMouse()
    {
        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, whatIsClickable);
    }

    IEnumerator MoveAgain()
    {
        yield return new WaitForSeconds(currentweapon.GetCanMoveTimer());

        PlayerMovement.instance.GetMovement(true);
    }

    IEnumerator ShootProjectile()
    {
        yield return new WaitForSeconds(0.5f);

        var particle = Instantiate(particlePrefab, particleSpawn) as GameObject;
        particle.transform.parent = null;
    }

}
