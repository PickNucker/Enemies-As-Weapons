using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    [SerializeField] Slider healthBar = null;

    public UnityEvent dead;

    public float health = 1000f;

    [HideInInspector]
    public bool isInRage;
    [HideInInspector]
    public bool bossIsDead;

    float maxHealth;

    private void Start()
    {
        maxHealth = health;
        healthBar.maxValue = health;
    }

    public void TakeDamage(float dmg)
    {
        if (isInRage) return;

        Debug.Log(dmg);

        health = Mathf.Max(health - dmg, 0);

        if(health <= 0)
        {
            StartCoroutine(IsDead());
            bossIsDead = true;

            Animator anim = GetComponent<Animator>();
            anim.SetTrigger("isDead");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Damage"))
        {
            Fighter.instance.HitBoss();
        }
    }

    IEnumerator IsDead()
    {
        yield return new WaitForSeconds(1.5f);

        dead.Invoke();
    }

    private void Update()
    {
        healthBar.value = health;
    }
}
