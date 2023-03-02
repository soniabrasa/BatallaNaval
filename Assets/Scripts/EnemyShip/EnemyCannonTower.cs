using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCannonTower : MonoBehaviour {
    GameObject playerShip;
    

    public EnemyCannon leftCannon;
    public EnemyCannon rightCannon;

    public Transform cannonPivot;
    public EnemyShipMovement esm;

    float targetElevation  = 0;
    public float distanceToAngleRatio = 40;
    float minShotDistance = 1000;   
    bool shotOnTargetElevation;
    float shortDistance = 300;
    float shortDistanceCorrection = 0.8f;

    bool canShot;

    float verticalSpeed = 40;
    // Start is called before the first frame update
    void Start() {
        playerShip = esm.playerShip;
        shotOnTargetElevation = false;
        canShot = true;
    }

    // Update is called once per frame
    void Update() {
        Vector3 playerShipVector = esm.playerShipVector;
        float playerShipDistance = esm.playerShipDistance;

        if(playerShipDistance < minShotDistance) {
            //Debug.Log("EnemyCannonTower.Update " + playerShipDistance);
            PointToPlayer(playerShipVector);
            if(canShot) {
                Shot(playerShipVector, playerShipDistance);
            }
        }

        Vector3 eulerAngles = cannonPivot.localEulerAngles;
        float elevationOffset = targetElevation - Angulo180(eulerAngles.x);
        //Debug.Log("EnemyCannonTower.Update elevationOffset " + elevationOffset);
        if(Mathf.Abs(elevationOffset) > 0.01f) {
            eulerAngles.x += Mathf.Sign(elevationOffset) * verticalSpeed * Time.deltaTime;
            cannonPivot.localEulerAngles = eulerAngles;
        } else if(shotOnTargetElevation && ! esm.BadShotDirection(playerShipVector, playerShipDistance)) {
            shotOnTargetElevation = false;
            EnemyBullet leftBullet = leftCannon.Shot();
            leftBullet.reachedPoint += ShotReachedPoint;
            rightCannon.Shot();
            Invoke("Load", 5);
        }
    }

    private void ShotReachedPoint(Vector3 shotReachedPointCoordinates) {
        Vector3 errorVector = shotReachedPointCoordinates - playerShip.transform.position;

        //Debug.Log("EnemyCannonTower.ShotReachedPoint distancia al blanco" + errorVector.magnitude);
    }
    private void PointToPlayer(Vector3 lookDirection) {
        //Debug.Log("PointToPlayer " + lookDirection);
        transform.rotation = Quaternion.LookRotation(lookDirection, Vector3.up);
        //transform.rotation = Quaternion.LookRotation(Vector3.left, Vector3.up);
    }

    private void Shot(Vector3 enemyPosition, float enemyDistance) {
        if(Random.Range(0, 1000.0f) < 2) {
            targetElevation = calculateShotElevation(enemyDistance);
            shotOnTargetElevation = true;
            canShot = false;           

            //Debug.Log("EnemyCannonTower.Shot targetElevation " + targetElevation);
            
        }
    }

    private float calculateShotElevation(float enemyDistance) {
        float shotElevation;
        //Proba moi básica. O cálculo haino que facer máis fino
        //tendo en conta a cuadrática e o drag co aire
        if(enemyDistance > shortDistance) {
            shotElevation = - enemyDistance / distanceToAngleRatio + Random.Range(-2.0f, 2.0f);
        } else {
            shotElevation  = - (enemyDistance / distanceToAngleRatio + Random.Range(-2.0f, 2.0f)) * shortDistanceCorrection;
        }

        Debug.Log("calculateShotElevation distancia " + enemyDistance + " elevacion " + shotElevation);
        return shotElevation;
    }

    private float Angulo180(float angulo) {
        while(angulo > 180) {
            angulo -= 360;
        }

        while(angulo < -180) {
            angulo += 360;
        }

        return angulo;
    }

    private void Load() {
        canShot = true;
    }
}
