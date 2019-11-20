using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bugSpawn : MonoBehaviour
{

    public GameObject bug;
    public GameObject treeBug;
    public GameObject spawnContainer;
    public Vector3 position;
    public GameObject floor;
    public int totalGroundBugs;
    public GameObject[] katydids;
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
        katydids = new GameObject[totalGroundBugs + spawnContainer.transform.childCount];

        while (bugCount < totalGroundBugs)
        {
            position = new Vector3(Random.Range(minFloorPoint.x, maxFloorPoint.x), 1f, Random.Range(minFloorPoint.z, maxFloorPoint.z));
            float yRot = Random.Range(-70, 70);

            if (!Physics.CheckSphere(position, 0.03f))
            {
                GameObject instance = Instantiate(bug, position, Quaternion.Euler(0, yRot, 0)) as GameObject;                
                instance.GetComponent<bugWander>().floor = floor;
                katydids[bugCount] = instance;
                bugCount += 1;
            }

            yield return new WaitForSeconds(0.1f);
        }

        foreach (Transform spawnpoint in spawnContainer.transform)
        {
            GameObject instance = Instantiate(treeBug, spawnpoint.transform.position, spawnpoint.transform.rotation);
            katydids[bugCount] = instance;
            bugCount += 1;
        }

    }
}
