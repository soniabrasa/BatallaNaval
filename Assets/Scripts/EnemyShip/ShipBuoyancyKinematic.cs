using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipBuoyancyKinematic : MonoBehaviour
{
    float acceleration;
    float initialImpulse;
    float verticalSpeed;

    Vector3 startPosition;

    bool upward;

    void Start()
    {
        acceleration = -1;
        initialImpulse = 1.5f;
        verticalSpeed = 0;
        upward = false;

        // Impulso hacia arriba para iniciar el balanceo
        transform.position += transform.up * initialImpulse;
        startPosition = transform.position;
    }

    void Update()
    {
        float waterLevel = transform.position.y;

        verticalSpeed += acceleration * waterLevel * Time.deltaTime;

        transform.position += transform.up * verticalSpeed * Time.deltaTime;

        // Este simple cálculo matemático deja errores de redondeo que,
        // a la larga, hacen que la oscilación se vaya amortiguando
        // Se propone que, al final de cada subida,
        // se reinicie velocidad vertical y posición inicial

        if( upward && verticalSpeed < 0 )
        {
            verticalSpeed = 0;
            transform.position = startPosition;
            upward = false;
        }

        if( !upward && verticalSpeed > 0 )
        {
            upward = true;
        }
    }
}
