// By Gamad on Youtube: https://www.youtube.com/watch?v=xlpHmxNtiT8
// Modified by Cathy and Amon
// Adjust focal length of DOF effect by moving the DOFObject

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DOFManager : MonoBehaviour {
    // What the raycast can hit
    [SerializeField]
    private LayerMask rayMask;

    // Maximum distance of the raycast
    [SerializeField]
    private float maxDistance;
    private RaycastHit hit;

    // Transform that represents focal length
    [SerializeField]
    private Transform DOFObject;

    // DOFObject's speed
    public float FocusSpeed = 1;
	
	// Update is called once per frame
	void Update () {
        // Send out a raycast
        if (Physics.Raycast(transform.position, transform.forward, out hit, maxDistance, rayMask))
        {
            // Use non-linear interpolation to move DOFObject closer to the new focal distance
            // Simulates the time it takes a tarsier to focus on new objects since they cannot move their eyes
            DOFObject.position += (hit.point - DOFObject.position) * Time.deltaTime * FocusSpeed;
        }
        else
        {
            // Relatively abitrary default focal length if the raycast hits nothing
            Vector3 DefaultPosition = transform.position + transform.forward * maxDistance * 0.2f;
            DOFObject.position += (DefaultPosition - DOFObject.position) * Time.deltaTime * FocusSpeed;
        }
		
	}
}
