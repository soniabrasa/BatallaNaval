using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCs : MonoBehaviour
{
    public GameObject bulletTrailPrefab;
    public GameObject explosionEffect;


    void Start()
    {
    }


    void Update()
    {
        SpawnTrail();
    }

    void OnCollisionEnter( Collision other )
    {
        GameObject newEffect = Instantiate(
            explosionEffect,
            transform.position,
            Quaternion.identity );

        Destroy(
            newEffect,
            newEffect.GetComponent<ParticleSystem>().main.duration );

        Destroy( gameObject );
    }

    void SpawnTrail()
    {
        for( int i = 0; i < 20; i++ )
        {
            Instantiate(
                bulletTrailPrefab,
                transform.position - transform.forward * 2 + (Vector3)Random.insideUnitCircle * 0.3f,
                Quaternion.identity);
        }
    }
}
