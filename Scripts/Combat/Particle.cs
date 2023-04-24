using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    [SerializeField] string name;
    [SerializeField] float speed = 5f;
    [SerializeField] float range = 7f;
    [SerializeField] LayerMask whatIsClickable;
    [SerializeField] Transform dir = null;

    float timer = Mathf.NegativeInfinity;

    RaycastHit hit;
    Rigidbody rigid;

    private void Start()
    {
        timer = 0;
    }

    void Update()
    {
        timer += Time.deltaTime;

        transform.Translate((Vector3.forward) * speed * Time.deltaTime);

        if(timer > range)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(name))
        {
            Destroy(this.gameObject);
        }
    }

    void LookAtMouse()
    {
        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, whatIsClickable);
    }
}
