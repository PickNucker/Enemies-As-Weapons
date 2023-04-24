using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth instance;

    [SerializeField] Weapon respawnWeapon = null;

    [SerializeField] Transform spawnPoint = null;
    [SerializeField] Slider healthBar = null;

    [SerializeField] float health = 100f;

    [SerializeField] Texture2D sprite;
    [SerializeField] UnityEvent respawnEvent;

    [SerializeField] bool canHeal;
    float maxHealth;

    Animator anim;

    bool isDead;
    


    public bool GetDead()
    {
        return isDead;
    }

    private void Awake()
    {
        //Cursor.SetCursor(sprite, Vector2.zero, CursorMode.Auto);
        maxHealth = health;
        healthBar.maxValue = health;
        instance = this;
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Damage2"))
        {
            TakeDamage(60f);
        }
    }

    public void TakeDamage(float dmg)
    {
        health = Mathf.Max(health - dmg, 0);

        if(health <= 0)
        {
            isDead = true;
            anim.SetTrigger("isDead");
            respawnEvent.Invoke();
            // Destroy
            // let something appear, like reset screen or abort screen
        }
    }

    public void AddHealth(float adding)
    {
        health += adding;

        if(health >= maxHealth)
        {
            health = maxHealth;
        }
    }

    public void Respawn()
    {
        transform.position = spawnPoint.position;
        Fighter.instance.EquipWeapon(respawnWeapon);
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        anim.SetTrigger("revive");
        yield return new WaitForSeconds(1.5f);
        isDead = false;
        health = maxHealth;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && canHeal)
            maxHealth = 1000f;

        healthBar.value = health;
    }
}
