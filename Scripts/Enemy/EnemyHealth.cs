using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public static EnemyHealth instance;


    [SerializeField] Collider collider = null;
    [SerializeField] float health = 20f;

    Rigidbody rigid;
    Animator anim;

    bool isDead;

    public bool GetDead()
    {
        return isDead;
    }

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        collider.enabled = true;
        instance = this;
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Damage"))
        {
            Debug.Log("Hit");
            Fighter.instance.GetTarget(this);
            Fighter.instance.Hit();
            
        }
    }

    public void TakeDamage(float dmg)
    {
        health = Mathf.Max(health - dmg, 0);

        if(health <= 0)
        {
            collider.enabled = false;
            rigid.isKinematic = true;
            //Destroy(this.gameObject, 10f);
            // let something appear, like reset screen or abort screen

            if (isDead) return;
            anim.SetTrigger("isDead");

            isDead = true;
        }
    }


}
