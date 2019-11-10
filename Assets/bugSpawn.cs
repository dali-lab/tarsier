﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bugSpawn : MonoBehaviour
{

    public GameObject bug;
    public GameObject treebug;
    public GameObject[] spawnpoints;
    public Vector3 position;
    public GameObject floor;
    //public List<Vector3> posList;
    public int xPos;
    public int zPos;
    public int yRot;
    public int maxBugs;
    private int bugCount = 0;

    private Vector3 maxFloorPoint;
    private Vector3 minFloorPoint;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(bugDrop());
        maxFloorPoint = floor.GetComponent<Collider>().bounds.max;
        minFloorPoint = floor.GetComponent<Collider>().bounds.min;
    }

    IEnumerator bugDrop()
    {
        System.Random random1 = new System.Random();
        int randomCounter = 0;
        int randomIndex = 0;

        while  (randomCounter < 2)
        { randomIndex = random1.Next(0, spawnpoints.Length);
            Instantiate(treebug, spawnpoints[randomIndex].transform.position, spawnpoints[randomIndex].transform.rotation);
            randomCounter++;
        }
        while (bugCount < maxBugs)
        {
            position = new Vector3(Random.Range(minFloorPoint.x, maxFloorPoint.x), 1f, Random.Range(minFloorPoint.z, maxFloorPoint.z));
            yRot = Random.Range(-70, 70);

            if (!Physics.CheckSphere(position, 0.03f))
            {
                Instantiate(bug, position, Quaternion.Euler(0, yRot, 0));
                bugCount += 1;
            }

            yield return new WaitForSeconds(0.1f);
        }

    }
}
