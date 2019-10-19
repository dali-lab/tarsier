// Class that simulates an infinite scene created from a prefab
// A grid is made out of a prefab, and a single GameObject is looped within the center grid square
// Similar to the lost woods, but with no escape

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopScene : MonoBehaviour {

    // CameraRig is the object whose position is kept within the central chunk (or in-between the 4 central chunks if GridSize is even)
    // Chunk is the GameObject prefab that the grid is created from
    // GridSize dictates both the number of rows and the number of columns of the prefab grid
    public GameObject CameraRig;
    public GameObject Chunk;
    public float GridSize = 5;

    // Floor is a child of Chunk, and must be the first one (child 0 of Chunk)
    // Size represents the dimensions of Floor, and is used to space out the grid and as the CameraRig's bounds
    GameObject Floor;
    Vector3 Size;

    // CameraTransform is the transform component of the CameraRig
    Transform CameraTransform;

	// Initialization
	void Start () {
        // Set initial values for variables
        CameraTransform = CameraRig.transform;
        Floor = Chunk.transform.GetChild(0).gameObject;
        Size = Floor.GetComponent<Renderer>().bounds.size;

        // Create a grid of Chunks
        for (int z = 0; z < GridSize; z++) // Rows
        {
            for (int x = 0; x < GridSize; x++) // Columns
            {
                Vector3 Position = Vector3.Scale(new Vector3(x-GridSize/2f+.5f, 0f, z-GridSize/2f+.5f), Size); // The position of the new chunk
                GameObject NewChunk = Instantiate(Chunk, Position, Quaternion.identity); // Instantiate a new chunk in the current grid position
                NewChunk.transform.SetParent(gameObject.transform); // Set the new chunk's parent as the object currently running this script
            }
        }
	}
	
	// Update
	void Update () {
        // Loop movement when necessary

        Vector3 CameraPosition = CameraTransform.position; // Get CameraRig's current position in the world (which changes when VRTK teleports it)
        Vector3 Offset = Vector3.zero; // Start the offset off at zero

        // Check to see if CameraRig's x position has exceeded its bounds
        if (CameraPosition.x > Size.x / 2f) // Positive
        {
            Offset.x -= Size.x;
        }
        else if (CameraPosition.x < -Size.x / 2f) // Negative
        {
            Offset.x += Size.x;
        }
        // Check to see if CameraRig's y position has exceeded its bounds
        if (CameraPosition.z > Size.z / 2f) // Positive
        {
            Offset.z -= Size.z;
        }
        else if (CameraPosition.z < -Size.z / 2f) // Negative
        {
            Offset.z += Size.z;
        }

        // Adjust CameraRig's position by the necessary offset
        CameraTransform.position += Offset;
    }
}
