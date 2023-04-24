using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSpawn : MonoBehaviour
{
    [SerializeField] GameObject particlePrefab = null;
    [SerializeField] Transform particleSpawn = null;

    public bool canShoot = true;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canShoot)
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                var particle = Instantiate(particlePrefab, particleSpawn) as GameObject;
                particle.transform.parent = null;
            }
        }        
    }

}
