using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonRotateCs : MonoBehaviour
{
    public Transform tfCannonPivot;

    float yRotateSpeed, xRotateSpeed;
    int yRotateDirection, xRotateDirection;

    Vector3 rotateAngle;

    void Start()
    {
        // La velocidad de giro horizontal de la torre es de 80º/s
        // Es decir 80ª * Time.deltaTime
        yRotateSpeed = 80;

        // La velocidad de pivote de los cañones es de 60 º/s
        xRotateSpeed = 60;
    }


    void Update()
    {
        // Obtener el ángulo actual del cañón
        // float currentAngleX = tfCannonPivot.localRotation.eulerAngles.x;
        float currentAngleX = tfCannonPivot.localEulerAngles.x;

        // GetKey es true mientras se mantiene presionada la tecla

        // Mientras esté pulsada la flecha <-
        // este objeto gira sobre el eje Z en negativo

        if( Input.GetKey(KeyCode.LeftArrow) && yRotateDirection <= 0 )
        {
            // Sentido antihorario
            yRotateDirection = -1;

            // Vector3.up es el vector normalizado para girar horizontalm-
            rotateAngle = -1 * Vector3.up * yRotateSpeed * Time.deltaTime;

            transform.Rotate( rotateAngle );
        }

        // Mientras esté pulsada la flecha ->
        // este objeto gira sobre el eje Z en negativo

        else if( Input.GetKey(KeyCode.RightArrow) && yRotateDirection >= 0 )
        {
            // Sentido horario
            yRotateDirection = 1;

            // Vector3.up es el vector normalizado para girar horizontalm-
            rotateAngle = Vector3.up * yRotateSpeed * Time.deltaTime;

            transform.Rotate( rotateAngle );
        }

        else {
            yRotateDirection = 0;
        }

        // Mientras se presionen las flechas arriba y abajo
        // giramos los cañones en torno al eje X para subirlos y bajarlos

        if( Input.GetKey(KeyCode.UpArrow) && xRotateDirection <= 0 )
        {
            // Hacia arriba el transform.rotation es negativo
            xRotateDirection = -1;

            // Vector3.right es el vector normalizado para girar verticalm-
            rotateAngle = -1 * Vector3.right * xRotateSpeed * Time.deltaTime;

            // currentAngleX empieza en 0 (360ª) y bajando hacia arriba
            // Para obtener un ángulo máximo de 45º (360-45 = 315 )
            // Librando 5º la subida de la proa
            // Comenzamos asegurando la numeración positiva

            if( currentAngleX > 310f || currentAngleX <= 0f )
            {
                tfCannonPivot.Rotate( rotateAngle );
            }
            // print("CannonRotateCs.currentAngleX " + currentAngleX );
        }

        else if( Input.GetKey(KeyCode.DownArrow) && xRotateDirection >= 0 )
        {
            // Hacia abajo el transform.rotation es positivo
            xRotateDirection = 1;

            // Vector3.right es el vector normalizado para girar verticalm-
            rotateAngle = 1 * Vector3.right * xRotateSpeed * Time.deltaTime;

            // Limitando el límite de bajada a -5º
            if( currentAngleX > 180f && currentAngleX < 355f )
            {
                tfCannonPivot.Rotate( rotateAngle );
            }
            print("CannonRotateCs.currentAngleX " + currentAngleX );
        }

        else {
            xRotateDirection = 0;
        }
    }
}
