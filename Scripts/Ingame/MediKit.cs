using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MediKit : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] float gettingHealth = 150f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, 1.5f + Mathf.Sin(-2 * Time.time * speed), transform.position.z);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth.instance.AddHealth(gettingHealth);
            Destroy(this.gameObject);
        }
    }


}
