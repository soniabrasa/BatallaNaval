using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTowerMovement : MonoBehaviour {
    public float horizontalRotationSpeed = 80;
    public float cannonElevationSpeed = 60;

    public Transform cannon;
    // Start is called before the first frame update
    void Start() {

        
        
    }

    // Update is called once per frame
    void Update() {
        if(Input.GetKey(KeyCode.LeftArrow)) {
            transform.Rotate (Vector3.down * horizontalRotationSpeed * Time.deltaTime);
        } else if(Input.GetKey(KeyCode.RightArrow)) {
            transform.Rotate (Vector3.up * horizontalRotationSpeed * Time.deltaTime);
        }

        if(Input.GetKey(KeyCode.UpArrow)) {
            cannon.Rotate (Vector3.left * cannonElevationSpeed * Time.deltaTime);
        } else if(Input.GetKey(KeyCode.DownArrow)) {
            cannon.Rotate (Vector3.right * cannonElevationSpeed * Time.deltaTime);
        }
    }
}
