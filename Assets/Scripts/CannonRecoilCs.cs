using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonRecoilCs : MonoBehaviour
{
    float speed;
    float recoilSpeed;
    float recoilRestorarionSpeed;
    float startZ;


    void Start()
    {
        speed = 0;
        recoilSpeed = -50f;
        recoilRestorarionSpeed = 200f;

        startZ = transform.localPosition.z;
    }


    void Update()
    {
        if( speed != 0 )
        {
            transform.localPosition += Vector3.forward * speed * Time.deltaTime;

            float distance = startZ - transform.localPosition.z;

            speed = recoilRestorarionSpeed * distance * Time.deltaTime;
        }
    }

    public void Recoil()
    {
        // print("CannonRecoilCs.Recoil()");
        speed = recoilSpeed;
    }
}
