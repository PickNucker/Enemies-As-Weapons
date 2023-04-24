using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/CreateAWeapon", order = 1)]
public class Weapon : ScriptableObject
{
    [SerializeField] float damage;
    [SerializeField] float canMoveTimer = 1f;
    [SerializeField] float timerBetweenAttacks;
    [SerializeField] float weaponRange = 2f;
    [SerializeField] GameObject weapon = default;
    [SerializeField] AnimatorOverrideController animatorOverride = default;

    [SerializeField] bool canShoot = false;

    string weaponName;
    string axe = "Axe";
    string kunai = "Kunai";
    string fist = "Fist";
    string particleEffektSorceress1 = "ParticleEffekt Sorceress 1";

    public void Spawn(Transform handTransfrom, Animator anim)
    {
        if (animatorOverride != null)
            anim.runtimeAnimatorController = animatorOverride;

        if (weapon != null)
        {
            weaponName = weapon.name;

            if(weaponName == axe)
            {
                ControllWeapons.instance.AxeEvent();
            }
            else if (weaponName == kunai)
            {
                ControllWeapons.instance.KunaiEvent();
            }
            else if (weaponName == fist)
            {
                ControllWeapons.instance.FistEvent();
            }
            else if (weaponName == particleEffektSorceress1)
            {
                ControllWeapons.instance.MageEvent();
            }

        }
    }

    public void SpawnEnemyWeapon(Transform handTransfrom, Animator anim)
    {
        if(weapon != null)
        {
           // Instantiate(weapon, handTransfrom);
        }

        if (animatorOverride != null)
            anim.runtimeAnimatorController = animatorOverride;
    }

    public float GetDamage()
    {
        return damage;
    }

    public float GetCanMoveTimer()
    {
        return canMoveTimer;
    }

    public float GetTimer()
    {
        return timerBetweenAttacks;
    }

    public float GetWeaponRange()
    {
        return weaponRange;
    }

    public bool GetCanShoot()
    {
        return canShoot;
    }
}