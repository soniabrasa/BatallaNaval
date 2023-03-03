using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipBuoyancyKinematic : MonoBehaviour
{
    float acceleration;
    float initialImpulse;
    float verticalSpeed;


    void Start()
    {
        acceleration = -1;
        initialImpulse = 1.5f;

        // Impulso hacia arriba para iniciar el balanceo
        transform.position += transform.up * initialImpulse;
    }

    void Update()
    {
        float waterLevel = transform.position.y;

        verticalSpeed += acceleration * waterLevel * Time.deltaTime;

        transform.position += transform.up * verticalSpeed * Time.deltaTime;
    }
}
