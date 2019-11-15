using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bugWander : MonoBehaviour {

    public float moveSpeed = 2f;
    public float rotSpeed = 100f;
    public GameObject floor;


    private bool executeMovement = true;
    private Collider floorCollider;


    // Use this for initialization
    void Start () {
        floorCollider = floor.GetComponent<Collider>();
    }
    
    // Update is called once per frame
    void Update () {
        int minWaitFrames = 100000;
        int framesPassed = 0;

        if (!executeMovement) {
            if (framesPassed < minWaitFrames) {
                framesPassed += 1;
            } else {
                framesPassed = 0;
                executeMovement = true;
            }
        }

        if (executeMovement) {
            int animationNumber = Random.Range(0, 3);

            if (animationNumber == 0)
            {
                Vector3 newPosition = transform.position - transform.forward * moveSpeed * Time.deltaTime;
                Vector3 newVector = new Vector3(newPosition.x, floor.transform.position.y, newPosition.z);
                //Debug.Log("new Position: " + newPosition);
                if (floorCollider.bounds.Contains(newVector)) {
                    transform.position -= transform.forward * moveSpeed * Time.deltaTime;
                }
                else {
                    Vector3 halfRotation = new Vector3(0f, 180f, 0f);
                    transform.Rotate(halfRotation);
                }
            }
            else if (animationNumber == 1)
            {
                transform.Rotate(transform.up * Time.deltaTime * rotSpeed);
            }
            else if (animationNumber == 2)
            {
                transform.Rotate(transform.up * Time.deltaTime * -rotSpeed);
            }
            else
            {
                executeMovement = false;
            }
        }
    }
   
}
