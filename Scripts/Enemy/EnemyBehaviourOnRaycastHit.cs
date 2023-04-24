using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviourOnRaycastHit : MonoBehaviour
{
    public Weapon enemyWeapon = null;
    [SerializeField] Transform handTransfrom = null;

    PlayerMovement player;
    Rigidbody rigid;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        //player = FindObjectOfType<PlayerMovement>();
    }

    private void Start()
    {
        EquipWeapon(enemyWeapon);
    }

    void Update()
    {
        if (Magnet.instance.GetHit())
        {
            //Debug.Log("WasMaen");
            // Spawn Disappear ParticleEffect

            // Update Current Weapon with
            //Fighter.instance.EquipWeapon(enemyWeapon);

            // Destroy GameObject
            //Destroy(this.gameObject);
        }
    }

    public void EquipWeapon(Weapon weapon)
    {
        Animator anim = GetComponent<Animator>();
        weapon.SpawnEnemyWeapon(handTransfrom, anim);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, enemyWeapon.GetWeaponRange());

    }
}
