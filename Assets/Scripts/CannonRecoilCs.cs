using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonRecoilCs : MonoBehaviour
{
    float speed;
    float recoilSpeed;
    float restorationAcceleration;
    float maxRestorationSpeed;
    Vector3 startPosition;
    bool cannonMoving;


    void Start()
    {
        speed = 0;
        recoilSpeed = -50f;
        restorationAcceleration = 200f;
        maxRestorationSpeed = 20f;
        cannonMoving = false;

        startPosition = transform.localPosition;
    }


    void Update()
    {
        if( cannonMoving )
        {
            float startZ = startPosition.z;
            float distance = startZ - transform.localPosition.z;

            if( speed < 0 )
            {
                // Fase de Retroceso
                speed += restorationAcceleration * distance * Time.deltaTime;
            }

            else {
                // Fase de recuperación
                speed = Mathf.Clamp( speed, 0, maxRestorationSpeed );

                if( distance > 0 )
                {
                    Stop();
                }
            }

            transform.localPosition += Vector3.forward * speed * Time.deltaTime;
        }
    }

    public void Recoil()
    {
        speed = recoilSpeed;
        cannonMoving = true;
    }

    void Stop() {
        speed = 0;
        cannonMoving = false;
        transform.localPosition = startPosition;
    }
}
