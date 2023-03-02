using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCannon : MonoBehaviour {
    public GameObject bulletPrefab;
    public Transform cannonFirePoint;

    public GameObject shotFX;

    float shotForce = 600;

    
    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {

    }

    public EnemyBullet Shot() {
        Debug.Log("Disparado "  + gameObject.name);
        GameObject bullet = Instantiate(bulletPrefab, cannonFirePoint.position, cannonFirePoint.rotation);
        bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * shotForce, ForceMode.Impulse);

        GetComponentInChildren<ParticleSystem>().Play();

        return bullet.GetComponent<EnemyBullet>();
    }

    
}
