using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonShotCs : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform leftCannonShotPoint;
    public Transform rightCannonShotPoint;

    public CannonRecoilCs leftCannonRecoil;
    public CannonRecoilCs rightCannonRecoil;

    float shotForce;

    int timeRecharge;

    bool loaded;


    void Start()
    {
        // Fuerza de disparo 600 Newtons
        shotForce = 600;

        // Tiempo necesario para la recarga de 4 segundos
        timeRecharge = 4;

        // Se inicia el juego con los cañones cargados
        loaded = true;
    }


    void Update()
    {
        // Input.GetKeyDown es true sólo una vez
        // cuando se pulsa la tecla especificada
        // o cuando se suelta y se vuelve a pulsar

        if( Input.GetKeyDown(KeyCode.Space) )
        {
            // Sólo se puede disparar si el cañón está cargado
            if( loaded )
            {
                Shot( leftCannonShotPoint, leftCannonRecoil );
                Shot( rightCannonShotPoint, rightCannonRecoil );

                // Descargado
                loaded = false;

                // Tiempo de recarga
                Invoke("Load", timeRecharge );
            }
        }
    }

    void Shot( Transform shotPoint, CannonRecoilCs _cannonRecoil )
    {
        GameObject bulletGO = Instantiate(
            bulletPrefab, shotPoint.position, shotPoint.rotation);

        bulletGO.GetComponent<Rigidbody>().AddForce(shotPoint.forward * shotForce, ForceMode.Impulse);

        _cannonRecoil.Recoil();
    }

    void Load()
    {
        loaded = true;
        print("CannonShotCs. ¡Cargado!");
    }
}
