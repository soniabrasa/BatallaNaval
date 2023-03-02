using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipMovement : MonoBehaviour {
    public GameObject playerShip;
    public float absoluteMaxTurnVelocity = 10;
    public float absoluteMinTurnVelocity = 2;
    Vector3 velocity;

    Vector3 targetPosition;

    public Transform cannonTower;

    public Vector3 playerShipVector;
    public Vector3 targetPositionVector;
    public Vector3 targetPositionOffset;
    public float playerShipDistance;
    public float targetPositionDistance;
    float acceleration = 3;
    float maxSpeed = 9;

    
    private bool canSelectDirection;

    public LayerMask playerLayer;

    // Start is called before the first frame update
    void Start() {
        canSelectDirection = true;
        targetPositionOffset = RandomPositionOffset();
    }

    // Update is called once per frame
    void Update() {
        playerShipVector = playerShip.transform.position - transform.position;
        targetPositionVector = targetPosition - transform.position;
        targetPositionDistance = targetPositionVector.magnitude;
        playerShipDistance = playerShipVector.magnitude;

        //Calculamos la dirección en la que se encuentra la posición de destino en relación 
        // al propio barco
        Vector3 targetLocalVector = transform.InverseTransformDirection(targetPositionVector);

        //Giramos hacia esa dirección
        //El signo de la coordenada x de targetLocalVector nos dice si por babor o por estribor
        Vector3 newRotation = transform.eulerAngles;
        newRotation.y += maxTurnVelocity() * Mathf.Sign(targetLocalVector.x) * Time.deltaTime;
        transform.eulerAngles = newRotation;

        if(targetPositionDistance < 500) {
            if(Random.Range(0f, 1f) < 0.002) {
                ChangeTargetPosition();
            } else if(RumboColision()) {
                ChangeTargetPosition();
            }
        }

        if(BadShotDirection(playerShipVector, playerShipDistance)) {
            ChangeTargetPosition();
        }


        
        if(targetPositionDistance > 200) {
            //velocity += targetPositionVector/targetPositionDistance * acceleration * Time.deltaTime;
            velocity += transform.forward * acceleration * Time.deltaTime;
            if(velocity.sqrMagnitude > maxSpeed*maxSpeed) {
                velocity = velocity / velocity.magnitude * maxSpeed;
            }
        } else  {
            //velocity -= targetPositionVector/targetPositionDistance * acceleration * Time.deltaTime;
            velocity -= transform.forward * acceleration * Time.deltaTime;
            if(velocity.sqrMagnitude < 0.001) {
                velocity = Vector3.zero;
            }
        }

        transform.position = transform.position + velocity * Time.deltaTime;
    }


    public bool BadShotDirection(Vector3 playerPosition, float playerDistance) {
        Vector3 playerDirection = playerPosition / playerDistance;

        if(Vector3.Dot(playerDirection, -transform.forward) > 0.85) {
            return true;
        }

        return false;
    }

    private void ChangeTargetPosition() {
        if(canSelectDirection) {            
            targetPosition = playerShip.transform.position + RandomPositionOffset() * 200;
            canSelectDirection = false;
            Invoke("RenewSelectDirection", 30);          
        } else {
            targetPosition += RandomPositionOffset() * Random.Range(0f, 20f);
        }
        //Debug.Log("EnemyShipMovement.ChangeTargetPosition new target position " + targetPosition);

    }

    private bool RumboColision() {
        RaycastHit hit;

        bool colision = Physics.SphereCast(transform.position + transform.forward * 30, 10,  transform.forward, out hit, 400, playerLayer);

        return colision;
    }

    private Vector3 RandomPositionOffset() {
        return new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }

    private void RenewSelectDirection() {
        canSelectDirection = true;
    }

    private float maxTurnVelocity() {
        return Mathf.Max(Mathf.Min(absoluteMaxTurnVelocity, absoluteMaxTurnVelocity - velocity.magnitude * 5), absoluteMinTurnVelocity);
    }

}
