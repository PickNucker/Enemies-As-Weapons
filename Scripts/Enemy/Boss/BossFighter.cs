using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BossFighter : MonoBehaviour
{
    public float damage = 20f;

    [SerializeField] AudioClip clip;

    [SerializeField] GameObject particlePrefab = null;
    [SerializeField] Transform particleSpawnPoint = null;
    [SerializeField] Text timerText = null;
    public SkinnedMeshRenderer mesh = null;
    [SerializeField] Material newMat = null;


    public UnityEvent onEnter;
    public UnityEvent onExit;

    AudioSource source;

    public float timer = 10;
    [HideInInspector]
    public bool getTimer;
    [HideInInspector]
    public bool finish;

    public void OnEnter()
    {
        onEnter.Invoke();
    }

    public void OnExit()
    {
        onExit.Invoke();
    }

    public void PlaySound()
    {
        //source.Play
    }

    private void Update()
    {
        timerText.text = timer.ToString("0");

        if (getTimer)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                finish = true;
            }
        }
    }

    public void ChangeSkin()
    {
        mesh.material = newMat;
    }

    public void SpawnParticle()
    {
        Instantiate(particlePrefab, particleSpawnPoint);
    }

    public void DoDamageToPlayer()
    {
        StartCoroutine(DMG());
    }

    IEnumerator DMG()
    {
        yield return new WaitForSeconds(.5f);

        PlayerHealth.instance.TakeDamage(damage);
    }

    public void DoDamageToPlayer2()
    {
        StartCoroutine(DMG2());
    }

    IEnumerator DMG2()
    {
        yield return new WaitForSeconds(.4f);

        PlayerHealth.instance.TakeDamage(damage);
        yield return new WaitForSeconds(.3f);
        PlayerHealth.instance.TakeDamage(damage);
    }
}
