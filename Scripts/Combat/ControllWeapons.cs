using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public  class ControllWeapons : MonoBehaviour
{
    public static ControllWeapons instance;

    [SerializeField] float timerToNormalForm;
    [SerializeField] Text text = null;

    float timer = Mathf.Infinity;

    bool fist;

    private void Start()
    {
        timer = timerToNormalForm = 30f;
    }

    [SerializeField] UnityEvent axeEvent;
    [SerializeField] UnityEvent kunaiEvent;
    [SerializeField] UnityEvent fistEvent;
    [SerializeField] UnityEvent particleEffektSorceress1Event;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
       // timer -= Time.deltaTime;
       //
       // //TimerToGetNormalForm();
       //
       // if (fist)
       // {
       //     timer = 30f;
       // }
       //
       // text.text = "<color=white>NormalForm:   " + timer.ToString("0") + "</color>";
    }

    public void AxeEvent()
    {
        axeEvent.Invoke();
        timer = timerToNormalForm;
        //fist = false;
    }

    public void KunaiEvent()
    {
        kunaiEvent.Invoke();
        timer = timerToNormalForm;
       // fist = false;
    }

    public void FistEvent()
    {
        fistEvent.Invoke();
        timer = timerToNormalForm;
        //fist = true;
    }

    public void MageEvent()
    {
        particleEffektSorceress1Event.Invoke();

        timer = timerToNormalForm;
        //fist = false;
    }

   // void TimerToGetNormalForm()
   // {
   //     if(timer <= 0)
   //     {
   //         fistEvent.Invoke();
   //         timer = timerToNormalForm;
   //     }
   // }
}
