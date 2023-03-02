using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShipCs : MonoBehaviour
{
    // Los niveles de potencia van del 0 al 6
    const int MAX_POWER = 6;
    const int MIN_POWER = 0;
    // Los niveles de timón van del -3 al +3
    const int MAX_RUDDER = 3;

    float baseSpeed, effectiveSpeed;
    float baseRotationSpeed, baseRotationResistance;
    int powerLevel, rudderLevel;

    void Start()
    {
        // velocidad base de avance es de 0.8 m/s
        // Es decir, 0.8m * Time.deltaTime
        baseSpeed = 0.8f;

        // velocidad base de giro es de 1.2º/s
        // Es decir, 1.2º * Time.deltaTime
        baseRotationSpeed = 1.2f;

        // La resistencia base al avance es de 0.2 m/s (cuando se gira)
        // Es decir, -0.2 * Time.deltaTime
        baseRotationResistance = 0.2f;

        powerLevel = 0;
        rudderLevel = 0;
    }


    void Update()
    {
        // Input.GetKeyDown es true sólo una vez
        // cuando se pulsa la tecla especificada
        // o cuando se suelta y se vuelve a pulsar

        // Pulsando "W" el jugador aumenta el nivel de potencia
        if( Input.GetKeyDown(KeyCode.W) )
        {
            powerLevel++;

            if( powerLevel > MAX_POWER )
            {
                powerLevel = MAX_POWER;
            }
        }

        // Pulsando "S" el jugador disminuye el nivel de potencia
        if( Input.GetKeyDown(KeyCode.S) )
        {
            powerLevel--;

            if( powerLevel < MIN_POWER )
            {
                powerLevel = MIN_POWER;
            }
        }

        // Pulsando "D" el jugador aumenta el nivel de timón
        if( Input.GetKeyDown(KeyCode.D) )
        {
            rudderLevel++;

            if( rudderLevel > MAX_RUDDER )
            {
                rudderLevel = MAX_RUDDER;
            }
        }

        // Pulsando "A" el jugador disminuye el nivel de timón
        if( Input.GetKeyDown(KeyCode.A) )
        {
            rudderLevel--;

            if( rudderLevel < -MAX_RUDDER )
            {
                rudderLevel = -MAX_RUDDER;
            }
        }


        // ----------------------------------------
        // Comprobando rotaciones de 90º

        if( Input.GetKeyDown(KeyCode.U) )
        {
            transform.Rotate( Vector3.up * 90 );
        }
        // ----------------------------------------


        // Si el barco está parado no puede girar
        // Ni hay resistencia al avance

        if ( powerLevel != 0 )
        {
            // Velocidad aplicada
            effectiveSpeed = baseSpeed * powerLevel;

            // Vector3.up es el movimiento de giro en sentido horario
            transform.Rotate( Vector3.up * baseRotationSpeed * rudderLevel * Time.deltaTime );

            // Resistencia al avance cuando gira el timóm
            effectiveSpeed -= Mathf.Abs(rudderLevel) * baseRotationResistance;
        }
        else {
            // Como no hay avance la velocidad permanecerá a cero
            effectiveSpeed = 0;
        }

        // transform.forward
        // vector normalizado que representa el eje Z (azul) del mundo
        transform.position += transform.forward * effectiveSpeed * Time.deltaTime;
    }

    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 200, 40), "Nivel de avance " + powerLevel);

        if( rudderLevel != 0 )
        {
            string hand = rudderLevel > 0 ? "R" : "L";
            int rl = Mathf.Abs( rudderLevel );
            GUI.Label(new Rect(220, 10, 200, 40), "Nivel de timón " + rl + " " + hand);
        }

        // Velocidad del barco en nudos
        GUI.Label(new Rect(420, 10, 200, 40), "Velocidad " + effectiveSpeed * 1.944f + " nudos");
    }
}
