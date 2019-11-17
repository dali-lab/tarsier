using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bugWander : MonoBehaviour {

    public float moveSpeed = 8f;
    public float rotSpeed = 100f;
    public GameObject floor;


    private bool executeMovement = true;
    private Collider floorCollider;


    // Use this for initialization
    void Start () {
        floorCollider = floor.GetComponent<Collider>();
        Debug.Log("Max of Collider: " + floorCollider.bounds.max);
        Debug.Log("Min of Collider: " + floorCollider.bounds.min);
        // StartCoroutine(Wander());
    }
    
    // Update is called once per frame
    void Update () {
        int minWaitFrames = 100;
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
            //isWandering = true;
            // print("wanderingggg");

            //yield return new WaitForSeconds(walkWait);
            // print("yield after walkWait");
            // isWalking = true;

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

        




        //yield return new WaitForSeconds(walkTime);
        //print("yield after walkTime");

        //isWalking = false;
        //yield return new WaitForSeconds(rotateWait);
        //print("yield after rotateWait");

        //if (rotateLorR == 1)
        //{
        //    isRotatingRight = true;
        //    //yield return new WaitForSeconds(rotTime);
        //    print("yield after rotTime _Right");

        //    isRotatingRight = false;
        //}

        //if (rotateLorR == 2)
        //{
        //    isRotatingLeft = true;
        //    //yield return new WaitForSeconds(rotTime);
        //    print("yield after rotTime_Left");

        //    isRotatingLeft = false;
        //}

        //isWandering = false;

        //if (isRotatingRight == true){
        //    print("Rotating right");
        //    transform.Rotate(transform.up * Time.deltaTime * rotSpeed);
        //}
        //if (isRotatingLeft == true)
        //{
        //    print("Rotate Left");
        //    transform.Rotate(transform.up * Time.deltaTime * -rotSpeed);
        //}
        //if (isWalking == true){
        //    print("Rotate walk");
        //    transform.position -= transform.forward * moveSpeed * Time.deltaTime;
        //}
    }

    //IEnumerator Wander()
    //{
    //    while (true)
    //    {
    //        if (isWandering == false)
    //        {

    //            int rotTime = Random.Range(1, 3);
    //            int rotateWait = Random.Range(1, 4);
    //            int rotateLorR = Random.Range(1, 2);
    //            int walkWait = Random.Range(1, 4);
    //            int walkTime = Random.Range(1, 5);

    //            isWandering = true;
    //            print("wanderingggg");

    //            yield return new WaitForSeconds(walkWait);
    //            print("yield after walkWait");
    //            isWalking = true;
    //            transform.position -= transform.forward * moveSpeed * Time.deltaTime;
    //            yield return new WaitForSeconds(walkTime);
    //            print("yield after walkTime");

    //            isWalking = false;
    //            yield return new WaitForSeconds(rotateWait);
    //            print("yield after rotateWait");

    //            if (rotateLorR == 1)
    //            {
    //                isRotatingRight = true;
    //                transform.Rotate(transform.up * Time.deltaTime * rotSpeed);
    //                yield return new WaitForSeconds(rotTime);
    //                print("yield after rotTime _Right");

    //                isRotatingRight = false;
    //            }

    //            if (rotateLorR == 2)
    //            {
    //                isRotatingLeft = true;
    //                transform.Rotate(transform.up * Time.deltaTime * -rotSpeed);
    //                yield return new WaitForSeconds(rotTime);
    //                print("yield after rotTime_Left");

    //                isRotatingLeft = false;
    //            }

    //            isWandering = false;
    //        }
    //    }
    //}
}
