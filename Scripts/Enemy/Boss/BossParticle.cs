using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossParticle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        BossHealth health = FindObjectOfType<BossHealth>();

        if (health.health <= 0)
            Destroy(this.gameObject);
    }
}
