using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Magnet : MonoBehaviour
{
    public static Magnet instance;

    [SerializeField] Text text = null;
    [SerializeField] float timerBetweenRaycast = 5f;

    float currentTimer;
    float timer = Mathf.Infinity;

    bool raycastHit;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {

    }


    void Update()
    {
        timer += Time.deltaTime;

        if(timer > timerBetweenRaycast)
        {
            text.text = "<color=white>Switch :</color> <color=green>Ready </color>";
        }
        else
        {
            text.text = "<color=white>Switch :</color> <color=red> Not Ready </color>";
        }

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if(Physics.Raycast(ray, out hit))
        {
            if(hit.transform != null && timer > timerBetweenRaycast)
            {
                if (Input.GetMouseButtonDown(1) && hit.transform.CompareTag("Enemy"))
                {
                    var target = hit.transform.GetComponent<EnemyBehaviourOnRaycastHit>();
                    Weapon weapon = target.enemyWeapon;

                    Fighter.instance.EquipWeapon(weapon);
                    timer = 0;
                    raycastHit = true;
                }
                else
                {
                    raycastHit = false;
                }
            }
        }
    }

    public bool GetHit()
    {
        return raycastHit;
    }
}
