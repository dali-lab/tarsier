using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bugSpawn : MonoBehaviour
{

    public GameObject bug;
    public Vector3 position;
    public GameObject floor;
    //public List<Vector3> posList;
    public int xPos;
    public int zPos;
    public int yRot;
    public int bugCount = 0;

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
        while (bugCount < 40)
        {
            position = new Vector3(Random.Range(minFloorPoint.x, maxFloorPoint.x), 1f, Random.Range(minFloorPoint.z, maxFloorPoint.z));
            // xPos = Random.Range(-4, 4);
            // zPos = Random.Range(-4, 4);
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
